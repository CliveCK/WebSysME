﻿Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports BusinessLogic
Imports Telerik.Web.UI

Partial Class TrainingAttendantsDetailsControl
    Inherits System.Web.UI.UserControl

    Dim ds As DataSet
    Private db As Database = New DatabaseProviderFactory().Create("Demo")
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString

    Private ReadOnly Property TrainingID As Long
        Get
            Return objUrlEncoder.Decrypt(Request.QueryString("id"))
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

            If Not IsNothing(Request.QueryString("id")) Then

                Dim objLookup As New BusinessLogic.CommonFunctions

                With cboTraining

                    .DataSource = objLookup.Lookup("tblTrainings", "TrainingID", "Name", , "TrainingID = " & objUrlEncoder.Decrypt(Request.QueryString("id"))).Tables(0)
                    .DataValueField = "TrainingID"
                    .DataTextField = "Name"
                    .DataBind()

                End With

                With cboBeneficiaryType

                    .DataSource = objLookup.Lookup("luBeneficiaryType", "BeneficiaryTypeID", "Description").Tables(0)
                    .DataValueField = "BeneficiaryTypeID"
                    .DataTextField = "Description"
                    .DataBind()

                    .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    .SelectedIndex = 0

                End With

            End If

        End If

    End Sub

    Protected Function GetSelectedBeneficiaryIDs() As String

        Dim BeneficiaryIDArray As New List(Of String)

        For Each gridRow As Telerik.Web.UI.GridDataItem In radBeneficiaries.SelectedItems
            BeneficiaryIDArray.Add(gridRow.Item("ObjectID").Text.ToString())
        Next

        Return String.Join(",", BeneficiaryIDArray.ToArray())

    End Function

    Private Function Map() As Boolean

        Dim Beneficiary() As String = GetSelectedBeneficiaryIDs.Split(",")

        If Beneficiary.Length > 0 Then

            For i As Long = 0 To Beneficiary.Length - 1

                Dim objAttendants As New BusinessLogic.TrainingAttendants("Demo", 1)

                With objAttendants

                    .BeneficiaryTypeID = cboBeneficiaryType.SelectedValue
                    .TrainingID = objUrlEncoder.Decrypt(Request.QueryString("id"))
                    .BeneficiaryID = Beneficiary(i)

                    If Not .Save Then

                        log.Error("Error saving...")
                        Return False

                    End If

                End With

            Next

        End If

        Return True

    End Function

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If Map() Then

            Dim sql As String = "SELECT * FROM tblTrainingAttendants WHERE TrainingID = " & objUrlEncoder.Decrypt(Request.QueryString("id")) & " AND BeneficiaryTypeID =" & cboBeneficiaryType.SelectedValue

            ds = db.ExecuteDataSet(CommandType.Text, sql)

            LoadGrid()
            ShowMessage("Attendants saved successfully...", MessageTypeEnum.Information)

        Else

            ShowMessage("Failed to save attendants...", MessageTypeEnum.Error)

        End If

    End Sub

    Private Sub LoadGrid()

        Dim sql As String = DertemineBeneficiaryTypeSQL(cboBeneficiaryType.SelectedItem.Text)
        ViewState("Beneficiaries") = Nothing

        If sql <> "" Then

            With radBeneficiaries

                .DataSource = db.ExecuteDataSet(CommandType.Text, sql).Tables(0)
                .DataBind()

                ViewState("Beneficiaries") = .DataSource

            End With

        End If

    End Sub

    'Public Function LoadTrainingAttendants(ByVal TrainingAttendantID As Long) As Boolean

    '    Try

    '        Dim objTrainingAttendants As New TrainingAttendants(CookiesWrapper.ConnectionName, CookiesWrapper.UserID)

    '        With objTrainingAttendants

    '            If .Retrieve(TrainingAttendantID) Then

    '                If Not IsNothing(cboTrainingAttendant.Items.FindByValue(.TrainingAttendantID)) Then cboTrainingAttendant.SelectedValue = .TrainingAttendantID
    '                If Not IsNothing(cboTraining.Items.FindByValue(.TrainingID)) Then cboTraining.SelectedValue = .TrainingID
    '                If Not IsNothing(cboBeneficiaryType.Items.FindByValue(.BeneficiaryTypeID)) Then cboBeneficiaryType.SelectedValue = .BeneficiaryTypeID
    '                If Not IsNothing(cboBeneficiary.Items.FindByValue(.BeneficiaryID)) Then cboBeneficiary.SelectedValue = .BeneficiaryID

    '                ShowMessage("TrainingAttendants loaded successfully...", MessageType.Information)
    '                Return True

    '            Else

    '                ShowMessage("Failed to loadTrainingAttendants: & .ErrorMessage", MessageType.Error)
    '                Return False

    '            End If

    '        End With

    '    Catch ex As Exception

    '        ShowMessage(ex, MessageType.Error)
    '        Return False

    '    End Try

    'End Function

    Public Function DertemineBeneficiaryTypeSQL(ByVal Type As String) As String

        Dim sql As String = ""

        Select Case Type

            Case "Community"
                sql = "SELECT CommunityID As ObjectID, Name, Description FROM tblCommunities"

            Case "Group"
                sql = "SELECT GroupID As ObjectID, GroupName, Description FROM tblGroups"

            Case "Organization"
                sql = "SELECT OrganizationID As ObjectID, Name FROM tblOrganization"

            Case "School"
                sql = "SELECT SchoolID As ObjectID, Name FROM tblSchools"

            Case "HealthCenter"
                sql = "SELECT HealthCenterID as ObjectID, Name, Description FROM tblHealthCenters"

            Case "Household"
                sql = "SELECT BeneficiaryID as ObjectID, ISNULL(FirstName, '') + ' ' + ISNULL(Surname,'') As Name FROM tblBeneficiaries WHERE Suffix = 1"

            Case "Individual"
                sql = "SELECT BeneficiaryID as ObjectID, ISNULL(FirstName, '') + ' ' + ISNULL(Surname,'') As Name FROM tblBeneficiaries"

        End Select

        Return sql

    End Function

    Private Sub radBeneficiaries_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles radBeneficiaries.ItemCommand

        If TypeOf e.Item Is GridDataItem Then

            Dim index As Integer = Convert.ToInt32(e.Item.ItemIndex.ToString)
            Dim item As GridDataItem = radBeneficiaries.Items(index)

            Select Case e.CommandName

                Case "Delete"

                    Dim objAttendants As New BusinessLogic.TrainingAttendants("Demo", 1)

                    With objAttendants

                        .TrainingID = objUrlEncoder.Decrypt(Request.QueryString("id"))
                        .BeneficiaryID = Server.HtmlDecode(e.CommandArgument)

                        If .DeleteEntries() Then

                            LoadGrid()
                            ShowMessage("entry deselected successfully...", MessageTypeEnum.Information)

                        End If

                    End With

            End Select

        End If


    End Sub

    Private Sub radBeneficiaries_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles radBeneficiaries.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim gridItem As GridDataItem = e.Item

            Dim btnImage As ImageButton = DirectCast(gridItem.FindControl("imgEdit"), ImageButton)

            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Select("BeneficiaryID = " & gridItem("ObjectID").Text).Length > 0 Then

                Dim chkbx As CheckBox = DirectCast(gridItem("chkRowSelect").Controls(0), CheckBox)

                chkbx.Enabled = False
                chkbx.ToolTip = "Already selected..."
                btnImage.Visible = True

            End If

        End If

    End Sub

    Private Sub radBeneficiaries_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radBeneficiaries.NeedDataSource

        radBeneficiaries.DataSource = DirectCast(ViewState("Beneficiaries"), DataTable)

    End Sub

    Private Sub cboBeneficiaryType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBeneficiaryType.SelectedIndexChanged

        Dim sql As String = "SELECT * FROM tblTrainingAttendants WHERE TrainingID = " & objUrlEncoder.Decrypt(Request.QueryString("id")) & " AND BeneficiaryTypeID =" & cboBeneficiaryType.SelectedValue

        ds = db.ExecuteDataSet(CommandType.Text, sql)
        LoadGrid()

    End Sub

    Private Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click

        Response.Redirect("~/TrainingDetails.aspx?id=" & Server.HtmlEncode(objUrlEncoder.Encrypt(TrainingID)))

    End Sub
End Class

