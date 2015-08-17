Imports BusinessLogic

Partial Class TrainingDetailsControl
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

                Dim objLookup As New BusinessLogic.CommonFunctions

                With cboTrainingType

                    .DataSource = objLookup.Lookup("luTrainingTypes", "TrainingTypeID", "Description").Tables(0)
                    .DataValueField = "TrainingTypeID"
                    .DataTextField = "Description"
                    .DataBind()

                    .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    .SelectedIndex = 0

                End With

                LoadTraining(objUrlEncoder.Decrypt(Request.QueryString("id")))

            Else

                Dim objLookup As New BusinessLogic.CommonFunctions

                With cboTrainingType

                    .DataSource = objLookup.Lookup("luTrainingTypes", "TrainingTypeID", "Description").Tables(0)
                    .DataValueField = "TrainingTypeID"
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

    Public Function LoadTraining(ByVal TrainingID As Long) As Boolean

        Try

            Dim objTraining As New Training("Demo", 1)

            With objTraining

                If .Retrieve(TrainingID) Then

                    txtTrainingID.Text = .TrainingID
                    If Not IsNothing(cboTrainingType.Items.FindByValue(.TrainingTypeID)) Then cboTrainingType.SelectedValue = .TrainingTypeID
                    txtName.Text = .Name
                    txtDescription.Text = .Description
                    txtLocation.Text = .Location
                    txtFacilitator.Text = .Facilitators

                    ShowMessage("Training details loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to load Training details", MessageTypeEnum.Error)
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

            Dim objTraining As New Training("Demo", 1)

            With objTraining

                .TrainingID = IIf(IsNumeric(txtTrainingID.Text), txtTrainingID.Text, 0)
                If cboTrainingType.SelectedIndex > -1 Then .TrainingTypeID = cboTrainingType.SelectedValue
                .Name = txtName.Text
                .Description = txtDescription.Text
                .Location = txtLocation.Text
                .Facilitators = txtFacilitator.Text

                If .Save Then

                    If Not IsNumeric(txtTrainingID.Text) OrElse Trim(txtTrainingID.Text) = 0 Then txtTrainingID.Text = .TrainingID
                    LoadTraining(.TrainingID)
                    ShowMessage("Training details saved successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Failed to save Training details", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtTrainingID.Text = ""
        If Not IsNothing(cboTrainingType.Items.FindByValue("")) Then
            cboTrainingType.SelectedValue = ""
        ElseIf Not IsNothing(cboTrainingType.Items.FindByValue(0)) Then
            cboTrainingType.SelectedValue = 0
        Else
            cboTrainingType.SelectedIndex = -1
        End If
        txtName.Text = ""
        txtDescription.Text = ""
        txtLocation.Text = ""
        txtFacilitator.Text = ""

    End Sub

    Private Sub lnkBeneficiaries_Click(sender As Object, e As EventArgs) Handles lnkBeneficiaries.Click

        If IsNumeric(txtTrainingID.Text) Then

            Response.Redirect("~/TrainingAttendantsPage.aspx?id=" & objUrlEncoder.Encrypt(txtTrainingID.Text))

        End If

    End Sub

    Private Sub lnkFiles_Click(sender As Object, e As EventArgs) Handles lnkFiles.Click

        If IsNumeric(txtTrainingID.Text) Then

            Response.Redirect("~/TrainingDocuments.aspx?id=" & objUrlEncoder.Encrypt(txtTrainingID.Text))

        End If

    End Sub

    Private Sub lnkInputs_Click(sender As Object, e As EventArgs) Handles lnkInputs.Click

        If IsNumeric(txtTrainingID.Text) Then

            Response.Redirect("~/TrainingInputsPage?id=" & objUrlEncoder.Encrypt(txtTrainingID.Text))

        End If

    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click

        Clear()

    End Sub
End Class

