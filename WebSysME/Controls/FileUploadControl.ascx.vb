Imports BusinessLogic
Imports System.IO
Imports Telerik.Web.UI

Public Class FileUploadControl
    Inherits System.Web.UI.UserControl

    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
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

            Session("Files") = Nothing

            Dim objFiles As New BusinessLogic.Files("Demo", 1)

            LoadGrid(objFiles:=objFiles)

            Dim objLookup As New BusinessLogic.CommonFunctions

            With drpFileType

                .DataSource = objLookup.Lookup("luFileTypes", "FileTypeID", "Description").Tables(0)
                .DataValueField = "FileTypeID"
                .DataTextField = "Description"
                .DataBind()

                .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                .SelectedIndex = 0

            End With

            If Not IsNothing(Request.QueryString("id")) Then

                LoadFiles(objUrlEncoder.Decrypt(Request.QueryString("id")))

            End If

        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If Not IsNumeric(txtFileID.Text) Then

            If fuFileUpload.HasFile Then

                Try

                    Dim FilePath As String = Path.GetFileName(fuFileUpload.FileName)
                    Dim Ext As String = Path.GetExtension(FilePath)

                    fuFileUpload.SaveAs(Server.MapPath("~/FileUploads/") & FilePath)

                    Save(FilePath, Ext)

                Catch ex As Exception

                    log.Error(ex)
                    ShowMessage("Error while uplading file...", MessageTypeEnum.Error)

                End Try

            Else

                ShowMessage("Please select a file before saving!!", MessageTypeEnum.Error)

            End If

        Else

            Save(txtFilePath.Text, Path.GetExtension(txtFilePath.Text))

        End If

    End Sub

    Public Function LoadFiles(ByVal FileID As Long) As Boolean

        Try

            Dim objFiles As New BusinessLogic.Files("Demo", 1)

            With objFiles

                If .Retrieve(FileID) Then

                    txtFileID.Text = .FileID
                    radDate.SelectedDate = .FileDate
                    If Not IsNothing(drpFileType.Items.FindByValue(.FileTypeID)) Then drpFileType.SelectedValue = .FileTypeID
                    txtTitle.Text = .Title
                    txtAuthor.Text = .Author
                    txtDescription.Text = .Description
                    txtFilePath.Text = .FilePath
                    txtAuthorOrg.Text = .AuthorOrganization

                    ShowMessage("Files details loaded successfully...", MessageTypeEnum.Information)
                    Return True

                Else

                    ShowMessage("Failed to load file details...", MessageTypeEnum.Error)
                    Return False

                End If

            End With

        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Public Function Save(ByVal FilePath As String, ByVal FileExt As String) As Boolean

        Try

            Dim objFiles As New BusinessLogic.Files("Demo", 1)

            With objFiles

                .FileID = IIf(IsNumeric(txtFileID.Text), txtFileID.Text, 0)
                .FileDate = radDate.SelectedDate
                .FileTypeID = drpFileType.SelectedValue
                .Title = txtTitle.Text
                .Author = txtAuthor.Text
                .Description = txtDescription.Text
                .FilePath = FilePath
                .FileExtension = FileExt
                .AuthorOrganization = txtAuthorOrg.Text

                If .Save Then

                    If Not IsNumeric(txtFileID.Text) OrElse Trim(txtFileID.Text) = 0 Then txtFileID.Text = .FileID
                    LoadGrid(objFiles)
                    ShowMessage("Files uploaded successfully...", MessageTypeEnum.Information)

                    Return True

                Else

                    ShowMessage("Error while uploading File to server", MessageTypeEnum.Error)
                    Return False

                End If

            End With


        Catch ex As Exception

            ShowMessage(ex, MessageTypeEnum.Error)
            Return False

        End Try

    End Function

    Private Sub LoadGrid(ByVal objFiles As BusinessLogic.Files)

        Session("Files") = Nothing

        With radFileListing

            .DataSource = objFiles.GetAllFiles()
            .DataBind()

            Session("Files") = .DataSource

        End With

    End Sub

    Public Sub Clear()

        txtFileID.Text = ""
        radDate.Clear()
        If Not IsNothing(drpFileType.Items.FindByValue("")) Then
            drpFileType.SelectedValue = ""
        ElseIf Not IsNothing(drpFileType.Items.FindByValue(0)) Then
            drpFileType.SelectedValue = 0
        Else
            drpFileType.SelectedIndex = -1
        End If
        txtAuthor.Text = ""
        txtDescription.Text = ""
        txtAuthorOrg.Text = ""
        txtFilePath.Text = ""
        txtTitle.Text = ""

    End Sub

    Private Sub radFileListing_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radFileListing.ItemCommand

        If e.CommandName = "Download" Then

            Dim index As Integer = Convert.ToInt32(e.Item.ItemIndex.ToString)
            Dim item As GridDataItem = radFileListing.Items(index)
            Dim FilePath As String

            FilePath = Server.HtmlDecode(item("FilePath").Text)

            With Response

                .Clear()
                .ClearContent()
                .ClearHeaders()
                .BufferOutput = True

            End With

            If File.Exists(Request.PhysicalApplicationPath & FilePath) Or File.Exists(Server.MapPath("~/FileUploads/" & FilePath)) Then

                Dim oFileStream As FileStream
                Dim fileLen As Long

                Try

                    oFileStream = File.Open(Server.MapPath("~/FileUploads/" & FilePath), FileMode.Open, FileAccess.Read, FileShare.None)
                    fileLen = oFileStream.Length

                    Dim ByteFile(fileLen - 1) As Byte

                    If fileLen > 0 Then
                        oFileStream.Read(ByteFile, 0, oFileStream.Length - 1)
                        oFileStream.Close()

                        With Response

                            .AddHeader("Content-Disposition", "attachment; filename=" & FilePath.Replace(" ", "_"))
                            .ContentType = "application/octet-stream"
                            .BinaryWrite(ByteFile)
                            '.WriteFile(Server.MapPath("~/FileUploads/" & FilePath))
                            .End()
                            HttpContext.Current.ApplicationInstance.CompleteRequest()

                        End With

                    Else
                        log.Error("Empty File...")
                    End If

                Catch ex As Exception

                End Try

            Else

                ShowMessage("Error: File not found!!!", MessageTypeEnum.Error)

            End If
        End If

        If e.CommandName = "View" Then

            Dim index1 As Integer = Convert.ToInt32(e.Item.ItemIndex.ToString)
            Dim item1 As GridDataItem = radFileListing.Items(index1)
            Dim FileID As Integer

            FileID = Server.HtmlDecode(item1("FileID").Text)

            LoadFiles(FileID)

        End If

    End Sub

    Private Sub radFileListing_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles radFileListing.NeedDataSource

        radFileListing.DataSource = Session("Files")

    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click

        Clear()

    End Sub
End Class