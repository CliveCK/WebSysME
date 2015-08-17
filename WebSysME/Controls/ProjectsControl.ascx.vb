Imports BusinessLogic

Public Class ProjectsControl
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

            LoadComboBoxes()
            If Not IsNothing(Request.QueryString("id")) Then

                LoadProjects(objUrlEncoder.Decrypt(Request.QueryString("id")))

            End If

        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Save()

    End Sub

    Private Sub LoadComboBoxes()

        Dim objLookup As New BusinessLogic.CommonFunctions

        With cboProgram

            .DataSource = objLookup.Lookup("tblProgrammes", "ProgramID", "Name").Tables(0)
            .DataValueField = "ProgramID"
            .DataTextField = "Name"
            .DataBind()

            .Items.Insert(0, New ListItem(String.Empty, String.Empty))
            .SelectedIndex = 0

        End With

        With cboSector

            .DataSource = objLookup.Lookup("tblSector", "SectorID", "Name").Tables(0)
            .DataValueField = "SectorID"
            .DataTextField = "Name"
            .DataBind()

            .Items.Insert(0, New ListItem(String.Empty, String.Empty))
            '.SelectedIndex = 0

        End With

        With cboProjectManager

            .DataSource = objLookup.Lookup("tblStaffMembers", "StaffID", "FirstName").Tables(0)
            .DataValueField = "StaffID"
            .DataTextField = "FirstName"
            .DataBind()

            .Items.Insert(0, New ListItem(String.Empty, String.Empty))
            '.SelectedIndex = 0

        End With

    End Sub

    Private Sub ClearSelection()

        cboProgram.ClearSelection()
        cboProjectManager.ClearSelection()
        cboSector.ClearSelection()

    End Sub

    Public Function LoadProjects(ByVal Project As Long) As Boolean

        Try

            Dim objProjects As New BusinessLogic.Projects("Demo", 1)

            With objProjects

                ClearSelection()

                If .Retrieve(Project) Then

                    txtProject.Text = .Project
                    txtProjectCode.Text = .ProjectCode
                    cboSector.Items.FindByValue(.Sector).Selected = True
                    cboProjectManager.Items.FindByValue(.ProjectManager).Selected = True
                    txtTargetedNoOfBeneficiaries.Text = .TargetedNoOfBeneficiaries
                    txtActualBeneficiaries.Text = .ActualBeneficiaries
                    radStartDate.SelectedDate = .StartDate
                    radFinalEvlDate.SelectedDate = .FinalEvlDate
                    RadEndDate.SelectedDate = .EndDate
                    txtProjectBudget.Text = .ProjectBudget
                    txtName.Text = .Name
                    cboProgram.Items.FindByValue(.Program).Selected = True
                    txtAcronym.Text = .Acronym
                    txtFinalGStatement.Text = .FinalGStatement
                    txtObjective.Text = .Objective
                    txtBenDescription.Text = .BenDescription
                    txtStakeholderDescription.Text = .StakeholderDescription

                    ShowMessage("Project loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to loadProjects: & .ErrorMessage", MessageTypeEnum.Error)
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

            Dim objProjects As New BusinessLogic.Projects("Demo", 1)

            With objProjects

                .Project = IIf(txtProject.Text = "", 0, txtProject.Text)
                .ProjectCode = txtProjectCode.Text
                .Sector = cboSector.SelectedValue
                .ProjectManager = cboProjectManager.SelectedValue
                .TargetedNoOfBeneficiaries = txtTargetedNoOfBeneficiaries.Text
                .ActualBeneficiaries = txtActualBeneficiaries.Text
                .StartDate = radStartDate.SelectedDate
                .FinalEvlDate = radFinalEvlDate.SelectedDate
                .EndDate = RadEndDate.SelectedDate
                .ProjectBudget = txtProjectBudget.Text
                .Name = txtName.Text
                .Program = cboProgram.SelectedValue
                .Acronym = txtAcronym.Text
                .FinalGStatement = txtFinalGStatement.Text
                .Objective = txtObjective.Text
                .BenDescription = txtBenDescription.Text
                .StakeholderDescription = txtStakeholderDescription.Text

                If .Save Then

                    If Not IsNumeric(txtProject.Text) OrElse Trim(txtProject.Text) = 0 Then txtProject.Text = .Project
                    ShowMessage("Projects saved successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Error saving data", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtProject.Text = 0
        txtProjectCode.Text = 0
        txtTargetedNoOfBeneficiaries.Text = 0
        txtActualBeneficiaries.Text = 0
        radStartDate.SelectedDate = ""
        radFinalEvlDate.SelectedDate = ""
        RadEndDate.SelectedDate = ""
        txtProjectBudget.Text = 0.0
        txtName.Text = ""
        cboProgram.SelectedValue = 0
        txtAcronym.Text = ""
        txtFinalGStatement.Text = ""
        txtObjective.Text = ""
        txtBenDescription.Text = ""
        txtStakeholderDescription.Text = ""
        cboProjectManager.SelectedValue = 0
        cboSector.SelectedValue = 0

    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click

        Response.Redirect("~/Projects.aspx")

    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Dim objProjects As New BusinessLogic.Projects("Demo", 1)

        If IsNumeric(txtProject.Text) Then

            With objProjects
                .Project = txtProject.Text
                If .Delete() Then
                    Clear()
                    ShowMessage("Project deleted successfully...", MessageTypeEnum.Information)
                End If
            End With
        End If

    End Sub
End Class
