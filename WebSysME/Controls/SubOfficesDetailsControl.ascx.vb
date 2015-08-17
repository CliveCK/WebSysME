Imports Universal.CommonFunctions
Imports BusinessLogic

Partial Class SubOfficesDetailsControl
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

            If Not IsNothing(Request.QueryString("id")) Then

                Dim objOrganization As New BusinessLogic.Organization("Demo", 1)

                lblOrgName.Text = Catchnull(objOrganization.GetOrganization(objUrlEncoder.Decrypt(Request.QueryString("id"))).Tables(0).Rows(0)("Name"), "No Name")

            End If

        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Save()

    End Sub

    Public Function LoadSubOffices(ByVal SubOfficeID As Long) As Boolean

        Try

            Dim objSubOffices As New SubOffices("Demo", 1)

            With objSubOffices

                If .Retrieve(SubOfficeID) Then

                    txtSubOfficeID.Text = .SubOfficeID
                    txtOrganizationID.Text = .OrganizationID
                    txtContactNo.Text = .ContactNo
                    txtFax.Text = .Fax
                    txtName.Text = .Name
                    txtEmail.Text = .Email
                    txtPhysicalAddress.Text = .PhysicalAddress

                    ShowMessage("SubOffices loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to loadSubOffices: & .ErrorMessage", MessageTypeEnum.Error)
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

            Dim objSubOffices As New SubOffices("Demo", 1)

            With objSubOffices

                .SubOfficeID = IIf(IsNumeric(txtSubOfficeID.Text), txtSubOfficeID.Text, 0)
                If Not IsNothing(Request.QueryString("id")) AndAlso IsNumeric(objUrlEncoder.Decrypt(Request.QueryString("id"))) Then .OrganizationID = objUrlEncoder.Decrypt(Request.QueryString("id")) Else ShowMessage("No Organization. Error!", MessageTypeEnum.Error) : Exit Function
                .ContactNo = txtContactNo.Text
                .Fax = txtFax.Text
                .Name = txtName.Text
                .Email = txtEmail.Text
                .PhysicalAddress = txtPhysicalAddress.Text

                If .Save Then

                    If Not IsNumeric(txtSubOfficeID.Text) OrElse Trim(txtSubOfficeID.Text) = 0 Then txtSubOfficeID.Text = .SubOfficeID
                    ShowMessage("SubOffices saved successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Error while saving...", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtSubOfficeID.Text = ""
        txtOrganizationID.Text = ""
        txtContactNo.Text = 0
        txtFax.Text = 0
        txtName.Text = ""
        txtEmail.Text = ""
        txtPhysicalAddress.Text = ""

    End Sub

End Class

