Imports Microsoft.Practices.EnterpriseLibrary
Imports System.Security.Cryptography
Imports System.Web.Security
Imports Universal.CommonFunctions
Imports SecurityPolicy
Imports SecurityPolicy.SecurityEnum

Partial Public Class CreateNewUserControl
    Inherits System.Web.UI.UserControl
    Private EditMode As Boolean = False
    Private Shared ReadOnly SecurityLog As log4net.ILog = log4net.LogManager.GetLogger("SecurityLogger")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim objLookup As New BusinessLogic.CommonFunctions()

            With chkUserGroup

                .DataValueField = "UserGroupID"

                .DataTextField = "Description"

                .DataSource = objLookup.Lookup("luUserGroups", "UserGroupID", "Description", "Description")

                .DataBind()

            End With

            lblStatus.Text = ""
            Dim myAdmin As New AdminSettings("Demo", CookiesWrapper.UserID)

            With myAdmin

                If .Retrieve(CookiesWrapper.UserID) Then
                    txtMinLength.Text = .MinPasswordLength
                    txtPasswordTemplateID.Text = .PasswordTemplateID
                Else
                    txtMinLength.Text = 8
                    txtPasswordTemplateID.Text = 1
                End If


                Select Case .PasswordTemplateID
                    Case PasswordTemplate.AlphaNumeric
                        lblPasswordPolicyMsg.Text = "Password should have alphanumeric characters"
                    Case PasswordTemplate.AlphaNumericWithSpecialChar
                        lblPasswordPolicyMsg.Text = "Password should be Aphanumeric and also contains special characters like ($#@!%^&*) "
                    Case PasswordTemplate.AlphaNumericWithSpecialChar
                        lblPasswordPolicyMsg.Text = "Password should be Aphanumeric, also starting and ending with a special characters like ($#@!%^&*)"
                    Case Else

                End Select

            End With





            If Request.QueryString("op") = "eu" Then

                'we need to load the user details
                EditMode = True
                Dim objUser As New SecurityPolicy.UserManager("Demo", CookiesWrapper.UserID)

                Dim objUserID As Long = Session("UserID")

                If objUserID > 0 Then

                    Dim ds As DataSet = objUser.FindUser(objUserID)

                    If Not IsNothing(ds) Then

                        'disable RequiredFieldValidator1 in edit mode to allow the administrator to be able to change user group info or other relevant info.
                        RequiredFieldValidator1.Enabled = Not (EditMode)

                        With ds.Tables(0).Rows(0)

                            txtUserID.Text = .Item("UserID")
                            txtUsername.Text = Catchnull(.Item("Username"), "")
                            txtPassword.Text = Catchnull(.Item("Password"), "")
                            txtFirstname.Text = Catchnull(.Item("UserFirstname"), "")
                            txtSurname.Text = Catchnull(.Item("UserSurname"), "")
                            txtMobileNo.Text = Catchnull(.Item("MobileNo"), "")
                            txtEmailAddress.Text = IIf(.Item("EmailAddress") Is DBNull.Value, "", .Item("EmailAddress"))
                            txtPasswordValidityPeriod.Text = Catchnull(.Item("PasswordExpirationDays"), 0)
                            chkPasswordExpires.Checked = Catchnull(.Item("PasswordExpires"), False)
                            chkIsLockedOut.Checked = Catchnull(.Item("IsLockedOut"), False)

                            txtUsername.Enabled = False

                            ViewState("Current.Active") = Catchnull(.Item("Deleted"), 0)

                        End With

                    End If

                End If

            End If

        End If

    End Sub

    Private Function isValidPassword() As Boolean

        Dim result As Boolean

        If txtPasswordTemplateID.Text = 1 Then
            result = PasswordValidator.isAphaNumeric(txtPassword.Text, txtMinLength.Text, 20)
        ElseIf txtPasswordTemplateID.Text = 2 Then
            result = PasswordValidator.isAphaNumericWithSpecialChar(txtPassword.Text, txtMinLength.Text, 20)
        ElseIf txtPasswordTemplateID.Text = 3 Then
            result = PasswordValidator.isAphaNumericStartEndSpecialChar(txtPassword.Text, txtMinLength.Text, 20)
        End If

        isValidPassword = result

    End Function
    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim myUser As New SecurityPolicy.UserManager("Demo", CookiesWrapper.UserID)

        lblStatus.Text = ""

        ' If Page.IsValid Then
        If (IIf(IsNumeric(txtUserID.Text), txtUserID.Text, 0) <> 0 AndAlso Trim(txtPassword.Text) <> "" AndAlso isValidPassword() = False) OrElse (IIf(IsNumeric(txtUserID.Text), txtUserID.Text, 0) = 0 AndAlso Trim(txtPassword.Text) <> "" AndAlso isValidPassword() = False) Then
            lblStatus.Text = "The password supplied does not meet the password requirements"
            Exit Sub
        End If

        With myUser

            .UserID = IIf(IsNumeric(txtUserID.Text), txtUserID.Text, 0)
            .Username = txtUsername.Text

            If Request.QueryString("op") = "eu" Then

                If Not Trim(txtPassword.Text) = "" Then
                    'Edit the password only when a new value has been supplied

                    .Password = .PasswordHash(txtUsername.Text, txtPassword.Text)

                End If

            Else

                ' The password value that is passed to the database should be hashed first
                .Password = .PasswordHash(txtUsername.Text, txtPassword.Text)

            End If
            .EmailAddress = txtEmailAddress.Text
            .UserFirstName = txtFirstname.Text
            .UserSurname = txtSurname.Text
            .MobileNo = txtMobileNo.Text

            If chkPasswordExpires.Checked Then

                If IsNumeric(txtPasswordValidityPeriod.Text) Then

                    If txtPasswordValidityPeriod.Text > 0 Then
                        .PasswordExpirationDays = txtPasswordValidityPeriod.Text
                        .LastPasswordChangeDate = Date.Today

                    Else
                        lblStatus.CssClass = "Warning"
                        lblStatus.Text = "Please enter number of days greater than 0....."
                        Exit Sub

                    End If

                Else

                    lblStatus.CssClass = "Warning"
                    lblStatus.Text = "Please specify a number...."
                    Exit Sub

                End If
            End If

            .PasswordExpires = chkPasswordExpires.Checked
            .IsLockedOut = chkIsLockedOut.Checked
            Try

                If .CreateUser() Then

                    txtUserID.Text = .UserID
                    'cmdSave.Enabled = False
                    'cmdAddUser.Visible = True

                    .DeleteUserUserGroups()

                    For i As Integer = 0 To chkUserGroup.Items.Count - 1

                        If chkUserGroup.Items(i).Selected Then

                            myUser.SaveUserUserGroup(chkUserGroup.Items(i).Value)

                        End If

                    Next
                    SecurityLog.Info(txtUsername.Text.ToUpper() & "New User created:")

                    lblStatus.Text = "Details Saved.."

                End If

            Catch ex As Exception

                lblStatus.CssClass = "Error"
                lblStatus.Text = "Username already exists. User has not been created!"

            End Try

        End With

        'End If

    End Sub

    Protected Sub cmdAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddUser.Click

        Clear()
        cmdSave.Enabled = True
        cmdAddUser.Visible = False

    End Sub

    Public Shadows Sub Clear()

        txtFirstname.Text = ""
        txtSurname.Text = ""
        txtMobileNo.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtConfirmPassword.Text = ""
        txtEmailAddress.Text = ""
        txtUserID.Text = ""
        lblStatus.Text = ""
        txtPasswordValidityPeriod.Text = ""
        chkPasswordExpires.Checked = False

        chkUserGroup.ClearSelection()

    End Sub

    'Private Sub cvMinLength_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvMinLength.ServerValidate
    '    Dim strDesc As String = Me.txtPassword.Text
    '    Dim myAdmin As New AdminSettings(CookiesWrapper.ConnectionName, CookiesWrapper.UserID)

    '    With myAdmin

    '        If .Retrieve(CookiesWrapper.UserID) Then
    '            txtMinLength.Text = .MinPasswordLength
    '            If Len(strDesc) > txtMinLength.Text Then
    '                args.IsValid = False
    '            Else
    '                args.IsValid = True
    '            End If
    '        End If

    '    End With

    'End Sub

    Protected Sub chkUserGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkUserGroup.SelectedIndexChanged

    End Sub

    Protected Sub chkIsLockedOut_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsLockedOut.CheckedChanged

    End Sub
End Class