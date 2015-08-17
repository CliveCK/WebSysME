Imports BusinessLogic

Partial Class HealthCenterDetailsControl
    Inherits System.Web.UI.UserControl

    Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString

#Region "Status Messages"

    Public Event Message(ByVal Message As String, ByVal MessageType As MessageTypeEnum)

    Public Sub ShowMessage(ByVal Message As String, ByVal MessageType As MessageTypeEnum, Optional ByVal LocalOnly As Boolean = False)

        lblError.Text = Message
        pnlError.CssClass = "msg" & [Enum].GetName(GetType(MessageTypeEnum), MessageType)

        If Not LocalOnly Then RaiseEvent Message(Message, MessageType)

    End Sub

    Public Sub ShowMessage(ByVal Message As Exception, ByVal MessageType As MessageTypeEnum, Optional ByVal LocalOnly As Boolean = False)

        lblError.Text = Message.Message
        If Message.InnerException IsNot Nothing Then lblError.Text &= " - " & Message.InnerException.Message
        If Not LocalOnly Then RaiseEvent Message(Message.Message, MessageType)

        pnlError.CssClass = "msg" & [Enum].GetName(GetType(MessageTypeEnum), MessageType)

    End Sub

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            If Not IsNothing(Request.QueryString("id")) Then

                LoadHealthCenter(objUrlEncoder.Decrypt(Request.QueryString("id")))

            Else

                Dim objLookup As New BusinessLogic.CommonFunctions

                With cboProvince

                    .DataSource = objLookup.Lookup("tblProvinces", "ProvinceID", "Name").Tables(0)
                    .DataValueField = "ProvinceID"
                    .DataTextField = "Name"
                    .DataBind()

                    .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    .SelectedIndex = 0

                End With

                With cboHealthCenterType

                    .DataSource = objLookup.Lookup("luHealthCenterTypes", "HealthCenterTypeID", "Description").Tables(0)
                    .DataValueField = "HealthCenterTypeID"
                    .DataTextField = "Description"
                    .DataBind()

                    .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    .SelectedIndex = 0

                End With

            End If

        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Save()

    End Sub

    Public Function LoadHealthCenter(ByVal HealthCenterID As Long) As Boolean

        Try

            Dim objHealthCenter As New HealthCenter("Demo", 1)

            With objHealthCenter

                If .Retrieve(HealthCenterID) Then

                    txtHealthCenterID.Text = .HealthCenterID
                    If Not IsNothing(cboWard.Items.FindByValue(.WardID)) Then cboWard.SelectedValue = .WardID
                    If Not IsNothing(cboHealthCenterType.Items.FindByValue(.HealthCenterTypeID)) Then cboHealthCenterType.SelectedValue = .HealthCenterTypeID
                    txtNoOfDoctors.Text = .NoOfDoctors
                    txtNoOfNurses.Text = .NoOfNurses
                    txtNoOfBeds.Text = .NoOfBeds
                    chkHasAmbulance.Checked = .HasAmbulance
                    txtLongitude.Text = .Longitude
                    txtLatitude.Text = .Latitude
                    txtName.Text = .Name
                    txtDescription.Text = .Description

                    ShowMessage("HealthCenter loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to loadHealthCenter: & .ErrorMessage", MessageTypeEnum.Error)
                    Return False

                End If

            End With

        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Function Save() As Boolean

        Try

            Dim objHealthCenter As New HealthCenter("Demo", 1)

            With objHealthCenter

                .HealthCenterID = IIf(IsNumeric(txtHealthCenterID.Text), txtHealthCenterID.Text, 0)
                If cboWard.SelectedIndex > -1 Then .WardID = cboWard.SelectedValue
                If cboHealthCenterType.SelectedIndex > -1 Then .HealthCenterTypeID = cboHealthCenterType.SelectedValue
                .NoOfDoctors = txtNoOfDoctors.Text
                .NoOfNurses = txtNoOfNurses.Text
                .NoOfBeds = txtNoOfBeds.Text
                .HasAmbulance = chkHasAmbulance.Checked
                .Longitude = txtLongitude.Text
                .Latitude = txtLatitude.Text
                .Name = txtName.Text
                .Description = txtDescription.Text

                If .Save Then

                    If Not IsNumeric(txtHealthCenterID.Text) OrElse Trim(txtHealthCenterID.Text) = 0 Then txtHealthCenterID.Text = .HealthCenterID
                    ShowMessage("HealthCenter saved successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Error loading details...", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtHealthCenterID.Text = ""
        If Not IsNothing(cboWard.Items.FindByValue("")) Then
            cboWard.SelectedValue = ""
        ElseIf Not IsNothing(cboWard.Items.FindByValue(0)) Then
            cboWard.SelectedValue = 0
        Else
            cboWard.SelectedIndex = -1
        End If
        If Not IsNothing(cboHealthCenterType.Items.FindByValue("")) Then
            cboHealthCenterType.SelectedValue = ""
        ElseIf Not IsNothing(cboHealthCenterType.Items.FindByValue(0)) Then
            cboHealthCenterType.SelectedValue = 0
        Else
            cboHealthCenterType.SelectedIndex = -1
        End If
        txtNoOfDoctors.Text = 0
        txtNoOfNurses.Text = 0
        txtNoOfBeds.Text = 0
        chkHasAmbulance.Checked = False
        txtLongitude.Text = 0.0
        txtLatitude.Text = 0.0
        txtName.Text = ""
        txtDescription.Text = ""

    End Sub

    Private Sub cboProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvince.SelectedIndexChanged

        Dim objLookup As New BusinessLogic.CommonFunctions

        If cboProvince.SelectedIndex > 0 Then

            With cboDistrict

                .DataSource = objLookup.Lookup("tblDistricts", "DistrictID", "Name", , "ProvinceID = " & cboProvince.SelectedValue).Tables(0)
                .DataValueField = "DistrictID"
                .DataTextField = "Name"
                .DataBind()

            End With

        End If

    End Sub

    Private Sub cboDistrict_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrict.SelectedIndexChanged

        Dim objLookup As New BusinessLogic.CommonFunctions

        If cboDistrict.SelectedIndex > 0 Then

            With cboWard

                .DataSource = objLookup.Lookup("tblWards", "WardID", "Name", , "DestrictID = " & cboDistrict.SelectedValue).Tables(0)
                .DataValueField = "WardID"
                .DataTextField = "Name"
                .DataBind()

            End With

        End If

    End Sub
End Class
 
