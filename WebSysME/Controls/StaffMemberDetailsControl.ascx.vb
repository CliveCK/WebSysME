Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports BusinessLogic

Partial Class StaffMemberDetailsControl
    Inherits System.Web.UI.UserControl

    Private db As Database = New DatabaseProviderFactory().Create("Demo")
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

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

                If Not LoadStaffMember(Request.QueryString("id")) Then

                    ShowMessage("Staff member not found", MessageTypeEnum.Error)

                End If

            Else

                Dim objLookup As New BusinessLogic.CommonFunctions

                With cboOrganizationType

                    .DataSource = objLookup.Lookup("luOrganizationTypes", "OrganizationTypeID", "Description").Tables(0)
                    .DataValueField = "OrganizationTypeID"
                    .DataTextField = "Description"
                    .DataBind()

                    .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    .SelectedIndex = 0

                End With

                With cboPosition

                    .DataSource = objLookup.Lookup("luStaffPosition", "PositionID", "Description").Tables(0)
                    .DataValueField = "PositionID"
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

    Public Function LoadStaffMember(ByVal StaffID As Long) As Boolean

        Try

            Dim objStaffMember As New StaffMember("Demo", 1)

            With objStaffMember

                If .Retrieve(StaffID) Then

                    txtStaffID.Text = .StaffID
                    If Not IsNothing(cboOrganization.Items.FindByValue(.OrganizationID)) Then cboOrganization.SelectedValue = .OrganizationID
                    txtContactNo.Text = .ContactNo
                    txtName.Text = .Name
                    txtFirstName.Text = .FirstName
                    txtSurname.Text = .Surname
                    txtSex.Text = .Sex
                    If Not IsNothing(cboPosition.Items.FindByValue(.PositionID)) Then cboPosition.SelectedValue = .PositionID
                    txtAddress.Text = .Address
                    txtEmailAddress.Text = .EmailAddress

                    ShowMessage("StaffMember loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to loadStaffMember: & .ErrorMessage", MessageTypeEnum.Error)
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

            Dim objStaffMember As New StaffMember("Demo", 1)

            With objStaffMember

                .StaffID = IIf(txtStaffID.Text <> "", txtStaffID.Text, 0)
                If cboOrganization.SelectedIndex > -1 Then .OrganizationID = cboOrganization.SelectedValue
                .ContactNo = txtContactNo.Text
                .Name = txtName.Text
                .FirstName = txtFirstName.Text
                .Surname = txtSurname.Text
                .Sex = txtSex.Text
                If cboPosition.SelectedIndex > -1 Then .PositionID = cboPosition.SelectedValue
                .Address = txtAddress.Text
                .EmailAddress = txtEmailAddress.Text

                If .Save Then

                    If Not IsNumeric(txtStaffID.Text) OrElse Trim(txtStaffID.Text) = 0 Then txtStaffID.Text = .StaffID
                    ShowMessage("Staff Member saved successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Error Saving Staff Member", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtStaffID.Text = ""
        If Not IsNothing(cboOrganization.Items.FindByValue("")) Then
            cboOrganization.SelectedValue = ""
        ElseIf Not IsNothing(cboOrganization.Items.FindByValue(0)) Then
            cboOrganization.SelectedValue = 0
        Else
            cboOrganization.SelectedIndex = -1
        End If
        txtContactNo.Text = 0
        txtName.Text = ""
        txtFirstName.Text = ""
        txtSurname.Text = ""
        txtSex.Text = ""
        If Not IsNothing(cboPosition.Items.FindByValue("")) Then
            cboPosition.SelectedValue = ""
        ElseIf Not IsNothing(cboPosition.Items.FindByValue(0)) Then
            cboPosition.SelectedValue = 0
        Else
            cboPosition.SelectedIndex = -1
        End If
        txtAddress.Text = ""
        txtEmailAddress.Text = ""

    End Sub

    Private Sub cboOrganizationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOrganizationType.SelectedIndexChanged

        Dim objLookup As New BusinessLogic.CommonFunctions

        If cboOrganizationType.SelectedIndex > 0 Then

            With cboOrganization

                .DataSource = objLookup.Lookup("tblOrganization", "OrganizationID", "Name", , "OrganizationTypeID = " & cboOrganizationType.SelectedValue)
                .DataValueField = "OrganizationID"
                .DataTextField = "Name"
                .DataBind()

                .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                .SelectedIndex = 0

            End With

        End If

    End Sub

    Private Sub lnkIOP_Click(sender As Object, e As EventArgs) Handles lnkIOP.Click

        If IsNumeric(txtStaffID.Text) Then

            Response.Redirect("~/SchedulerPortal?staffid=" & txtStaffID.Text)

        Else

            ShowMessage("No staff member has been selected/saved yet!...", MessageTypeEnum.Warning)

        End If

    End Sub

    Private Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click

        If IsNumeric(txtStaffID.Text) Then

            If fupCV.HasFile Then

                Dim ObjectTypeID As Integer = db.ExecuteScalar(CommandType.Text, "SELECT TOP 1 ObjectTypeID FROM luObjectTypes WHERE Description = 'Staff'")

                Try

                    Dim FilePath As String = IO.Path.GetFileName(fupCV.FileName)
                    Dim Ext As String = IO.Path.GetExtension(FilePath)

                    fupCV.SaveAs(Server.MapPath("~/FileUploads/") & FilePath)

                    Save(FilePath, Ext, ObjectTypeID)

                Catch ex As Exception

                    log.Error(ex)
                    ShowMessage("Error while uplading file...", MessageTypeEnum.Error)

                End Try

            Else

                ShowMessage("No file selected...", MessageTypeEnum.Error)

            End If

        Else

            ShowMessage("No staff member has been selected/saved yet!...", MessageTypeEnum.Warning)

        End If

    End Sub

    Private Sub Save(ByVal FilePath As String, ByVal Ext As String, ByVal ObjectTypeID As Long)

        Dim FileType As Long = db.ExecuteScalar(CommandType.Text, "SELECT TOP 1 FileTypeID FROM luFileTypes where Description = 'CV'")

        Dim objFiles As New BusinessLogic.Files("Demo", 1)

        With objFiles

            .FileID = 0
            .FileDate = Now
            .FileTypeID = FileType
            .Title = "Cariculum Vitae"
            .Author = txtFirstName.Text & " " & txtSurname.Text
            .Description = "CV"
            .FilePath = FilePath
            .FileExtension = Ext
            .AuthorOrganization = cboOrganization.SelectedItem.Text

            If .Save() Then

                Dim objObjects As New BusinessLogic.DocumentObjects("Demo", 1)

                With objObjects

                    .ObjectTypeID = ObjectTypeID
                    .ObjectID = txtStaffID.Text
                    .DocumentID = objFiles.FileID

                    If .Save() Then

                        ShowMessage("CV uploaded successfully...", MessageTypeEnum.Information)

                    Else

                        ShowMessage("CV upload failed...", MessageTypeEnum.Error)

                    End If

                End With

            Else

                ShowMessage("Operation failed!...", MessageTypeEnum.Error)

            End If

        End With

    End Sub

End Class

