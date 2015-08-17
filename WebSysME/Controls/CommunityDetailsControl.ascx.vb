Imports BusinessLogic

    Partial Class CommunityDetailsControl
        Inherits System.Web.UI.UserControl

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

                Dim objLookup As New BusinessLogic.CommonFunctions

                With cboProvince

                    .DataSource = objLookup.Lookup("tblProvinces", "ProvinceID", "Name").Tables(0)
                    .DataValueField = "ProvinceID"
                    .DataTextField = "Name"
                    .DataBind()

                    .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    .SelectedIndex = 0

                End With
            End If

        End Sub

        Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

            Save()

        End Sub

        Public Function LoadCommunity(ByVal CommunityID As Long) As Boolean

            Try

                Dim objCommunity As New Community("Demo", 1)

                With objCommunity

                    If .Retrieve(CommunityID) Then

                        txtCommunityID.Text = .CommunityID
                        If Not IsNothing(cboWard.Items.FindByValue(.WardID)) Then cboWard.SelectedValue = .WardID
                        txtNoOfHouseholds.Text = .NoOfHouseholds
                        txtNoOfIndividualAdultMales.Text = .NoOfIndividualAdultMales
                        txtNoOfIndividualAdultFemales.Text = .NoOfIndividualAdultFemales
                        txtNoOfMaleYouths.Text = .NoOfMaleYouths
                        txtNoOfFemaleYouth.Text = .NoOfFemaleYouth
                        txtNoOfChildren.Text = .NoOfChildren
                        txtName.Text = .Name
                        txtDescription.Text = .Description

                        ShowMessage("Community loaded successfully...", MessageTypeEnum.Information)
                        Return True

                    Else

                        ShowMessage("Failed to load Community: & .ErrorMessage", MessageTypeEnum.Error)
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

                Dim objCommunity As New Community("Demo", 1)

                With objCommunity

                    .CommunityID = IIf(txtCommunityID.Text <> "", txtCommunityID.Text, 0)
                    If cboWard.SelectedIndex > -1 Then .WardID = cboWard.SelectedValue
                    .NoOfHouseholds = txtNoOfHouseholds.Text
                    .NoOfIndividualAdultMales = txtNoOfIndividualAdultMales.Text
                    .NoOfIndividualAdultFemales = txtNoOfIndividualAdultFemales.Text
                    .NoOfMaleYouths = txtNoOfMaleYouths.Text
                    .NoOfFemaleYouth = txtNoOfFemaleYouth.Text
                    .NoOfChildren = txtNoOfChildren.Text
                    .Name = txtName.Text
                    .Description = txtDescription.Text

                    If .Save Then

                        If Not IsNumeric(txtCommunityID.Text) OrElse Trim(txtCommunityID.Text) = 0 Then txtCommunityID.Text = .CommunityID
                        ShowMessage("Community saved successfully...", MessageTypeEnum.Information)

                        Return True

                    Else

                        ShowMessage("Error: Failed to save community", MessageTypeEnum.Error)
                        Return False

                    End If

                End With


            Catch ex As Exception

                ShowMessage(ex, MessageTypeEnum.Error)
                Return False

            End Try

        End Function

        Public Sub Clear()

            txtCommunityID.Text = ""
            If Not IsNothing(cboWard.Items.FindByValue("")) Then
                cboWard.SelectedValue = ""
            ElseIf Not IsNothing(cboWard.Items.FindByValue(0)) Then
                cboWard.SelectedValue = 0
            Else
                cboWard.SelectedIndex = -1
            End If
            txtNoOfHouseholds.Text = 0
            txtNoOfIndividualAdultMales.Text = 0
            txtNoOfIndividualAdultFemales.Text = 0
            txtNoOfMaleYouths.Text = 0
            txtNoOfFemaleYouth.Text = 0
            txtNoOfChildren.Text = 0
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