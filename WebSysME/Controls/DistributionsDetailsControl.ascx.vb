Imports BusinessLogic

Partial Class DistributionsDetailsControl
    Inherits System.Web.UI.UserControl

    Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString

#Region "Status Messages"

    Public Event Message(ByVal Message As String, ByVal MessageTypeEnum As MessageTypeEnum)

    Public Sub ShowMessage(ByVal Message As String, ByVal MessageTypeEnum As MessageTypeEnum, Optional ByVal LocalOnly As Boolean = False)

        lblError.Text = Message
        pnlError.CssClass = "msg" & [Enum].GetName(GetType(MessageTypeEnum), MessageTypeEnum)

        If Not LocalOnly Then RaiseEvent Message(Message, MessageTypeEnum)

    End Sub

    Public Sub ShowMessage(ByVal Message As Exception, ByVal MessageTypeEnum As MessageTypeEnum, Optional ByVal LocalOnly As Boolean = False)

            lblError.Text = Message.Message
            If Message.InnerException IsNot Nothing Then lblError.Text &= " - " & Message.InnerException.Message
            If Not LocalOnly Then RaiseEvent Message(Message.Message, MessageTypeEnum)

        pnlError.CssClass = "msg" & [Enum].GetName(GetType(MessageTypeEnum), MessageTypeEnum)

    End Sub

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            Dim objLookup As New BusinessLogic.CommonFunctions

            With cboDistributionType

                .DataSource = objLookup.Lookup("luDistributionTypes", "DistributionTypeID", "Description").Tables(0)
                .DataValueField = "DistributionTypeID"
                .DataTextField = "Description"
                .DataBind()

                .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                .SelectedIndex = 0

                If Not IsNothing(Request.QueryString("id")) Then

                    LoadDistributions(objUrlEncoder.Decrypt(Request.QueryString("id")))

                End If

            End With

        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Save()

    End Sub

    Public Function LoadDistributions(ByVal DistributionID As Long) As Boolean

        Try

            Dim objDistributions As New Distributions("Demo", 1)

            With objDistributions

                If .Retrieve(DistributionID) Then

                    txtDistributionID1.Text = .DistributionID
                    If Not IsNothing(cboDistributionType.Items.FindByValue(.DistributionTypeID)) Then cboDistributionType.SelectedValue = .DistributionTypeID
                    txtName.Text = .Name
                    txtDescription.Text = .Description
                    txtLocation.Text = .Location

                    ShowMessage("Distributions loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to loadDistributions: & .ErrorMessage", MessageTypeEnum.Error)
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

            Dim objDistributions As New Distributions("Demo", 1)

            With objDistributions

                .DistributionID = IIf(IsNumeric(txtDistributionID1.Text), txtDistributionID1.Text, 0)
                If cboDistributionType.SelectedIndex > -1 Then .DistributionTypeID = cboDistributionType.SelectedValue
                .Name = txtName.Text
                .Description = txtDescription.Text
                .Location = txtLocation.Text

                If .Save Then

                    If Not IsNumeric(txtDistributionID1.Text) OrElse Trim(txtDistributionID1.Text) = 0 Then txtDistributionID1.Text = .DistributionID
                    LoadDistributions(.DistributionID)
                    ShowMessage("Distribution saved successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Failed to save details...", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtDistributionID1.Text = ""
        If Not IsNothing(cboDistributionType.Items.FindByValue("")) Then
            cboDistributionType.SelectedValue = ""
        ElseIf Not IsNothing(cboDistributionType.Items.FindByValue(0)) Then
            cboDistributionType.SelectedValue = 0
        Else
            cboDistributionType.SelectedIndex = -1
        End If
        txtName.Text = ""
        txtDescription.Text = ""
        txtLocation.Text = ""

    End Sub

    Private Sub lnkBeneficiaries_Click(sender As Object, e As EventArgs) Handles lnkBeneficiaries.Click

        If IsNumeric(txtDistributionID1.Text) Then
            Response.Redirect("~/DistributionBeneficiaryPage.aspx?id=" & objUrlEncoder.Encrypt(txtDistributionID1.Text))
        Else
            ShowMessage("Please save Distribution first!...", MessageTypeEnum.Error)
        End If

    End Sub
End Class

