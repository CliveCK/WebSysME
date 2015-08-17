Imports BusinessLogic
Imports Telerik.Web.UI

Partial Class BeneficiaryDetailsControl
    Inherits System.Web.UI.UserControl

    Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString

    Public ReadOnly Property HouseholdID As Long
        Get
            Return txtBeneficiaryID1.Text
        End Get
    End Property

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

            Dim objBeneficiary As New BusinessLogic.Beneficiary("Demo", 1)

            If Not IsNothing(Request.QueryString("id")) Then

                LoadBeneficiary(objUrlEncoder.Decrypt(Request.QueryString("id")))

            End If

            InitializeComponents()
            LoadGrid(objBeneficiary)

        End If

    End Sub

    Private Sub InitializeComponents()

        LoadCombo(cboMaritalStatus, "luMaritalStatus", "Description", "ObjectID")
        LoadCombo(cboHealthStatus, "tblHealthStatus", "Description", "ObjectID")
        LoadCombo(cboDisabilityStatus, "tblDisabilityStatus", "Description", "ObjectID")
        LoadCombo(cboLevelOfEducation, "tblLevelOfEducation", "Level", "ObjectID")
        LoadCombo(cboRegularity, "tblRegularity", "Description", "ObjectID")
        LoadCombo(cboOpharnhood, "tblOrphanhood", "Description", "ObjectID")
        LoadCombo(cboMajorSourceIncome, "tblSourceOfIncome", "Description", "ObjectID")
        LoadCombo(cboCondition, "tblCondition", "Description", "ObjectID")
        LoadCombo(cboAttendance, "tblAttendance", "Description", "ObjectID")
        LoadCombo(cboDisability, "tblDisability", "Description", "ObjectID")

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Save()

    End Sub

    Public Function LoadBeneficiary(ByVal BeneficiaryID As Long) As Boolean

        Try

            Dim objBeneficiary As New BusinessLogic.Beneficiary("Demo", 1)

            With objBeneficiary

                If .Retrieve(BeneficiaryID) Then

                    txtBeneficiaryID1.Text = .BeneficiaryID
                    txtParentID.Text = .ParentID
                    txtSuffix.Text = .Suffix
                    If Not IsNothing(cboMaritalStatus.Items.FindByValue(.MaritalStatus)) Then cboMaritalStatus.SelectedValue = .MaritalStatus
                    If Not IsNothing(cboHealthStatus.Items.FindByValue(.HealthStatus)) Then cboHealthStatus.SelectedValue = .HealthStatus
                    If Not IsNothing(cboDisabilityStatus.Items.FindByValue(.DisabilityStatus)) Then cboDisabilityStatus.SelectedValue = .DisabilityStatus
                    If Not IsNothing(cboLevelOfEducation.Items.FindByValue(.LevelOfEducation)) Then cboLevelOfEducation.SelectedValue = .LevelOfEducation
                    If Not IsNothing(cboRegularity.Items.FindByValue(.Regularity)) Then cboRegularity.SelectedValue = .Regularity
                    If Not IsNothing(cboOpharnhood.Items.FindByValue(.Opharnhood)) Then cboOpharnhood.SelectedValue = .Opharnhood
                    If Not IsNothing(cboMajorSourceIncome.Items.FindByValue(.MajorSourceIncome)) Then cboMajorSourceIncome.SelectedValue = .MajorSourceIncome
                    txtContactNo.Text = .ContactNo
                    If Not IsNothing(cboCondition.Items.FindByValue(.Condition)) Then cboCondition.SelectedValue = .Condition
                    If Not IsNothing(cboAttendance.Items.FindByValue(.Attendance)) Then cboAttendance.SelectedValue = .Attendance
                    If Not IsNothing(cboDisability.Items.FindByValue(.Disability)) Then cboDisability.SelectedValue = .Disability
                    radDateofBirth.SelectedDate = .DateOfBirth
                    txtMemberNo.Text = .MemberNo
                    txtFirstName.Text = .FirstName
                    txtSurname.Text = .Surname
                    If Not IsNothing(cboSex.Items.FindByValue(.Sex)) Then cboSex.SelectedValue = .Sex
                    txtNationlIDNo.Text = .NationlIDNo

                    If .IsDependant = 1 Then

                        cmdAddDependant.Text = "View Principal"

                    End If

                    ShowMessage("Beneficiary loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to loadBeneficiary: & .ErrorMessage", MessageTypeEnum.Error)
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

            Dim objBeneficiary As New BusinessLogic.Beneficiary("Demo", 1)

            With objBeneficiary

                .BeneficiaryID = IIf(IsNumeric(txtBeneficiaryID1.Text), txtBeneficiaryID1.Text, 0)
                .Suffix = IIf(txtSuffix.Text <> "", txtSuffix.Text, 0)
                .MaritalStatus = cboMaritalStatus.SelectedValue
                .HealthStatus = cboHealthStatus.SelectedValue
                .DisabilityStatus = cboDisabilityStatus.SelectedValue
                .LevelOfEducation = cboLevelOfEducation.SelectedValue
                .Regularity = cboRegularity.SelectedValue
                .Opharnhood = cboOpharnhood.SelectedValue
                .MajorSourceIncome = cboMajorSourceIncome.SelectedValue
                .ContactNo = txtContactNo.Text
                .Condition = cboCondition.SelectedValue
                .Attendance = cboAttendance.SelectedValue
                .Disability = cboDisability.SelectedValue
                .DateOfBirth = radDateofBirth.SelectedDate
                .MemberNo = txtMemberNo.Text
                .FirstName = txtFirstName.Text
                .Surname = txtSurname.Text
                .Sex = cboSex.SelectedValue
                .NationlIDNo = txtNationlIDNo.Text

                If chkAddDependant.Checked Then

                    If IsNumeric(txtParentID.Text) AndAlso txtParentID.Text <> 0 Then

                        .ParentID = txtParentID.Text
                        .MemberNo = txtMemberNo.Text
                        .Suffix = .GetNextSuffix()
                        .IsDependant = 1

                        If .Save Then

                            LoadBeneficiary(.BeneficiaryID)
                            LoadGrid(objBeneficiary)
                            ShowMessage("Dependant saved successfully...", MessageTypeEnum.Information)

                            Return True

                        Else

                            ShowMessage("Error: failed to save Dependant", MessageTypeEnum.Error)
                            Return False

                        End If

                    Else

                        ShowMessage("Please load head household details first before adding a a dependant...", MessageTypeEnum.Error)

                    End If

                Else

                    If .Save Then

                        If Not IsNumeric(txtBeneficiaryID1.Text) OrElse Trim(txtBeneficiaryID1.Text) = 0 Then txtBeneficiaryID1.Text = .BeneficiaryID

                        .MemberNo = IIf(IsNumeric(txtMemberNo.Text), .GenerateMemberNo(), txtMemberNo.Text)
                        .Suffix = IIf(Not IsNumeric(txtSuffix.Text), .GetNextSuffix(), txtSuffix.Text)
                        .IsDependant = 0

                        If .MemberNo <> "" AndAlso .Suffix <> 0 Then .Save()

                        LoadBeneficiary(.BeneficiaryID)

                        ShowMessage("Beneficiary saved successfully...", MessageTypeEnum.Information)

                        Return True

                    Else

                        ShowMessage("Error: failed to save Beneficiary", MessageTypeEnum.Error)
                        Return False

                    End If

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Sub Clear()

        txtBeneficiaryID1.Text = ""
        txtSuffix.Text = ""
        If Not IsNothing(cboMaritalStatus.Items.FindByValue("")) Then
            cboMaritalStatus.SelectedValue = ""
        ElseIf Not IsNothing(cboMaritalStatus.Items.FindByValue(0)) Then
            cboMaritalStatus.SelectedValue = 0
        Else
            cboMaritalStatus.SelectedIndex = -1
        End If
        If Not IsNothing(cboHealthStatus.Items.FindByValue("")) Then
            cboHealthStatus.SelectedValue = ""
        ElseIf Not IsNothing(cboHealthStatus.Items.FindByValue(0)) Then
            cboHealthStatus.SelectedValue = 0
        Else
            cboHealthStatus.SelectedIndex = -1
        End If
        If Not IsNothing(cboDisabilityStatus.Items.FindByValue("")) Then
            cboDisabilityStatus.SelectedValue = ""
        ElseIf Not IsNothing(cboDisabilityStatus.Items.FindByValue(0)) Then
            cboDisabilityStatus.SelectedValue = 0
        Else
            cboDisabilityStatus.SelectedIndex = -1
        End If
        If Not IsNothing(cboLevelOfEducation.Items.FindByValue("")) Then
            cboLevelOfEducation.SelectedValue = ""
        ElseIf Not IsNothing(cboLevelOfEducation.Items.FindByValue(0)) Then
            cboLevelOfEducation.SelectedValue = 0
        Else
            cboLevelOfEducation.SelectedIndex = -1
        End If
        If Not IsNothing(cboRegularity.Items.FindByValue("")) Then
            cboRegularity.SelectedValue = ""
        ElseIf Not IsNothing(cboRegularity.Items.FindByValue(0)) Then
            cboRegularity.SelectedValue = 0
        Else
            cboRegularity.SelectedIndex = -1
        End If
        If Not IsNothing(cboOpharnhood.Items.FindByValue("")) Then
            cboOpharnhood.SelectedValue = ""
        ElseIf Not IsNothing(cboRegularity.Items.FindByValue(0)) Then
            cboOpharnhood.SelectedValue = 0
        Else
            cboOpharnhood.SelectedIndex = -1
        End If
        If Not IsNothing(cboMajorSourceIncome.Items.FindByValue("")) Then
            cboMajorSourceIncome.SelectedValue = ""
        ElseIf Not IsNothing(cboMajorSourceIncome.Items.FindByValue(0)) Then
            cboMajorSourceIncome.SelectedValue = 0
        Else
            cboMajorSourceIncome.SelectedIndex = -1
        End If
        txtContactNo.Text = ""
        If Not IsNothing(cboCondition.Items.FindByValue("")) Then
            cboCondition.SelectedValue = ""
        ElseIf Not IsNothing(cboCondition.Items.FindByValue(0)) Then
            cboCondition.SelectedValue = 0
        Else
            cboCondition.SelectedIndex = -1
        End If
        If Not IsNothing(cboAttendance.Items.FindByValue("")) Then
            cboAttendance.SelectedValue = ""
        ElseIf Not IsNothing(cboAttendance.Items.FindByValue(0)) Then
            cboAttendance.SelectedValue = 0
        Else
            cboAttendance.SelectedIndex = -1
        End If
        If Not IsNothing(cboDisability.Items.FindByValue("")) Then
            cboDisability.SelectedValue = ""
        ElseIf Not IsNothing(cboDisability.Items.FindByValue(0)) Then
            cboDisability.SelectedValue = 0
        Else
            cboDisability.SelectedIndex = -1
        End If
        radDateofBirth.Clear()
        txtMemberNo.Text = ""
        txtFirstName.Text = ""
        txtSurname.Text = ""
        cboSex.SelectedValue = ""
        txtNationlIDNo.Text = ""
        txtParentID.Text = ""
        chkAddDependant.Checked = False

    End Sub

    Public Sub LoadCombo(ByVal cboCombo As DropDownList, ByVal Table As String, ByVal TextField As String, ByVal ValueField As String)

        Dim objLookup As New BusinessLogic.CommonFunctions

        With cboCombo

            .DataSource = objLookup.Lookup(Table, ValueField, TextField).Tables(0)
            .DataValueField = ValueField
            .DataTextField = TextField
            .DataBind()

            .Items.Insert(0, New ListItem(String.Empty, String.Empty))
            .SelectedIndex = 0

        End With

    End Sub

    Private Sub LoadGrid(ByVal objBeneficiaries As BusinessLogic.Beneficiary)

        With radBenListing

            .DataSource = objBeneficiaries.GetAllBeneficiaries()
            .DataBind()

            ViewState("Beneficiaries") = .DataSource

        End With

    End Sub

    Private Sub radBenListing_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radBenListing.ItemCommand

        If e.CommandName = "View" Then

            Dim index As Integer = Convert.ToInt32(e.Item.ItemIndex.ToString)
            Dim item As GridDataItem = radBenListing.Items(index)
            Dim BeneficiaryID As Integer

            BeneficiaryID = Server.HtmlDecode(item("BeneficiaryID").Text)

            LoadBeneficiary(BeneficiaryID)

        End If

    End Sub

    Private Sub cmdAddDependant_Click(sender As Object, e As EventArgs) Handles cmdAddDependant.Click

        If cmdAddDependant.Text = "Add Dependant" Then

            If IsNumeric(txtParentID.Text) AndAlso txtParentID.Text > 0 Then

                chkAddDependant.Checked = True

                txtBeneficiaryID1.Text = ""
                txtSuffix.Text = ""
                If Not IsNothing(cboMaritalStatus.Items.FindByValue("")) Then
                    cboMaritalStatus.SelectedValue = ""
                ElseIf Not IsNothing(cboMaritalStatus.Items.FindByValue(0)) Then
                    cboMaritalStatus.SelectedValue = 0
                Else
                    cboMaritalStatus.SelectedIndex = -1
                End If
                If Not IsNothing(cboHealthStatus.Items.FindByValue("")) Then
                    cboHealthStatus.SelectedValue = ""
                ElseIf Not IsNothing(cboHealthStatus.Items.FindByValue(0)) Then
                    cboHealthStatus.SelectedValue = 0
                Else
                    cboHealthStatus.SelectedIndex = -1
                End If
                If Not IsNothing(cboDisabilityStatus.Items.FindByValue("")) Then
                    cboDisabilityStatus.SelectedValue = ""
                ElseIf Not IsNothing(cboDisabilityStatus.Items.FindByValue(0)) Then
                    cboDisabilityStatus.SelectedValue = 0
                Else
                    cboDisabilityStatus.SelectedIndex = -1
                End If
                If Not IsNothing(cboLevelOfEducation.Items.FindByValue("")) Then
                    cboLevelOfEducation.SelectedValue = ""
                ElseIf Not IsNothing(cboLevelOfEducation.Items.FindByValue(0)) Then
                    cboLevelOfEducation.SelectedValue = 0
                Else
                    cboLevelOfEducation.SelectedIndex = -1
                End If
                If Not IsNothing(cboRegularity.Items.FindByValue("")) Then
                    cboRegularity.SelectedValue = ""
                ElseIf Not IsNothing(cboRegularity.Items.FindByValue(0)) Then
                    cboRegularity.SelectedValue = 0
                Else
                    cboRegularity.SelectedIndex = -1
                End If
                If Not IsNothing(cboOpharnhood.Items.FindByValue("")) Then
                    cboOpharnhood.SelectedValue = ""
                ElseIf Not IsNothing(cboRegularity.Items.FindByValue(0)) Then
                    cboOpharnhood.SelectedValue = 0
                Else
                    cboOpharnhood.SelectedIndex = -1
                End If
                If Not IsNothing(cboMajorSourceIncome.Items.FindByValue("")) Then
                    cboMajorSourceIncome.SelectedValue = ""
                ElseIf Not IsNothing(cboMajorSourceIncome.Items.FindByValue(0)) Then
                    cboMajorSourceIncome.SelectedValue = 0
                Else
                    cboMajorSourceIncome.SelectedIndex = -1
                End If
                txtContactNo.Text = ""
                If Not IsNothing(cboCondition.Items.FindByValue("")) Then
                    cboCondition.SelectedValue = ""
                ElseIf Not IsNothing(cboCondition.Items.FindByValue(0)) Then
                    cboCondition.SelectedValue = 0
                Else
                    cboCondition.SelectedIndex = -1
                End If
                If Not IsNothing(cboAttendance.Items.FindByValue("")) Then
                    cboAttendance.SelectedValue = ""
                ElseIf Not IsNothing(cboAttendance.Items.FindByValue(0)) Then
                    cboAttendance.SelectedValue = 0
                Else
                    cboAttendance.SelectedIndex = -1
                End If
                If Not IsNothing(cboDisability.Items.FindByValue("")) Then
                    cboDisability.SelectedValue = ""
                ElseIf Not IsNothing(cboDisability.Items.FindByValue(0)) Then
                    cboDisability.SelectedValue = 0
                Else
                    cboDisability.SelectedIndex = -1
                End If
                radDateofBirth.Clear()
                txtFirstName.Text = ""
                txtSurname.Text = ""
                cboSex.SelectedValue = ""
                txtNationlIDNo.Text = ""

                cmdAddDependant.Text = "View Principal"

            Else

                ShowMessage("Please save household principal before attempting to save a dependant!", MessageTypeEnum.Error)


            End If

        Else

            Response.Redirect("~/Beneficiary.aspx?id=" & objUrlEncoder.Encrypt(txtParentID.Text))

        End If

    End Sub
End Class


