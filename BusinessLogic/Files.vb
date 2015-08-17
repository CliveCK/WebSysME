Imports Microsoft.Practices.EnterpriseLibrary.Data 
Imports Universal.CommonFunctions 

Public Class Files

#region "Variables"

    Protected mFileID As long
    Protected mCreatedBy As long
    Protected mUpdatedBy As long
    Protected mDate As string
    Protected mCreatedDate As string
    Protected mUpdatedDate As string
    Protected mFileTypeID As Long
    Protected mAuthor As string
    Protected mDescription As string
    Protected mFilePath As string
    Protected mFileExtension As string
    Protected mAuthorOrganization As String
    Protected mTitle As String

    Protected db As Database 
    Protected mConnectionName As String 
    Protected mObjectUserID As Long

    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

#end region

#region "Properties"

    Public ReadOnly Property Database() As Database 
        Get 
            Return db 
        End Get 
    End Property 
     
    Public ReadOnly Property OwnerType() As String 
        Get 
            Return Me.GetType.Name 
        End Get 
    End Property 

    Public ReadOnly Property ConnectionName() As String 
        Get 
            Return mConnectionName 
        End Get 
    End Property

    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property

    Public  Property FileID() As long
        Get
		return mFileID
        End Get
        Set(ByVal value As long)
		mFileID = value
        End Set
    End Property

    Public  Property CreatedBy() As long
        Get
		return mCreatedBy
        End Get
        Set(ByVal value As long)
		mCreatedBy = value
        End Set
    End Property

    Public  Property UpdatedBy() As long
        Get
		return mUpdatedBy
        End Get
        Set(ByVal value As long)
		mUpdatedBy = value
        End Set
    End Property

    Public Property FileDate() As String
        Get
            Return mDate
        End Get
        Set(ByVal value As String)
            mDate = value
        End Set
    End Property

    Public  Property CreatedDate() As string
        Get
		return mCreatedDate
        End Get
        Set(ByVal value As string)
		mCreatedDate = value
        End Set
    End Property

    Public  Property UpdatedDate() As string
        Get
		return mUpdatedDate
        End Get
        Set(ByVal value As string)
		mUpdatedDate = value
        End Set
    End Property

    Public Property FileTypeID() As Long
        Get
            Return mFileTypeID
        End Get
        Set(ByVal value As Long)
            mFileTypeID = value
        End Set
    End Property

    Public  Property Author() As string
        Get
		return mAuthor
        End Get
        Set(ByVal value As string)
		mAuthor = value
        End Set
    End Property

    Public  Property Description() As string
        Get
		return mDescription
        End Get
        Set(ByVal value As string)
		mDescription = value
        End Set
    End Property

    Public  Property FilePath() As string
        Get
		return mFilePath
        End Get
        Set(ByVal value As string)
		mFilePath = value
        End Set
    End Property

    Public  Property FileExtension() As string
        Get
		return mFileExtension
        End Get
        Set(ByVal value As string)
		mFileExtension = value
        End Set
    End Property

    Public  Property AuthorOrganization() As string
        Get
		return mAuthorOrganization
        End Get
        Set(ByVal value As string)
		mAuthorOrganization = value
        End Set
    End Property

#end region

#region "Methods"

#Region "Constructors" 
 
    Public Sub New(ByVal ConnectionName As String, ByVal ObjectUserID As Long) 

        mObjectUserID = ObjectUserID 
        mConnectionName = ConnectionName 
        db = New DatabaseProviderFactory().Create(ConnectionName)

    End Sub 

#End Region 

Public Sub Clear()  

    FileID = 0
    mCreatedBy = mObjectUserID
    mUpdatedBy = 0
    mDate = ""
    mCreatedDate = ""
    mUpdatedDate = ""
        mFileTypeID = 0
        mTitle = ""
    mAuthor = ""
    mDescription = ""
    mFilePath = ""
    mFileExtension = ""
    mAuthorOrganization = ""

End Sub

#Region "Retrieve Overloads" 

    Public Overridable Function Retrieve() As Boolean 

        Return Me.Retrieve(mFileID) 

    End Function 

    Public Overridable Function Retrieve(ByVal FileID As Long) As Boolean 

        Dim sql As String 

        If FileID > 0 Then 
            sql = "SELECT T.Description AS FileType, F.* FROM tblFiles F INNER JOIN luFileTypes T on F.FileTypeID = T.FileTypeID WHERE FileID = " & FileID
        Else 
            sql = "SELECT T.Description AS FileType, F.* FROM tblFiles F INNER JOIN luFileTypes T on F.FileTypeID = T.FileTypeID WHERE FileID = " & mFileID
        End If 

        Return Retrieve(sql) 

    End Function 

    Protected Overridable Function Retrieve(ByVal sql As String) As Boolean 

        Try 

            Dim dsRetrieve As DataSet = db.ExecuteDataSet(CommandType.Text, sql) 

            If dsRetrieve IsNot Nothing AndAlso dsRetrieve.Tables.Count > 0 AndAlso dsRetrieve.Tables(0).Rows.Count > 0 Then 

                LoadDataRecord(dsRetrieve.Tables(0).Rows(0)) 

                dsRetrieve = Nothing 
                Return True 

            Else 

                log.Warn("Files not found.")

                Return False 

            End If 

        Catch e As Exception 

            log.Error(e)
            Return False 

        End Try 

    End Function

    Public Function GetFileTypes() As DataSet

        Dim sql As String = "SELECT * FROM luFileTypes"

        Return db.ExecuteDataSet(CommandType.Text, sql)

    End Function

    Public Function GetFileTypeByDescription(ByVal Description As String) As DataSet

        Dim sql As String = "SELECT * FROM luFileTypes WHERE Description = '" & Description & "'"

        Return db.ExecuteDataSet(CommandType.Text, sql)

    End Function

    Public Overridable Function GetFiles() As System.Data.DataSet

        Return GetFiles(mFileID) 

    End Function

    Public Overridable Function GetFiles(ByVal FileID As Long) As DataSet

        Dim sql As String

        If FileID > 0 Then
            sql = "SELECT * FROM tblFiles WHERE FileID = " & FileID
        Else
            sql = "SELECT * FROM tblFiles WHERE FileID = " & mFileID
        End If

        Return GetFiles(sql)

    End Function

    Public Overridable Function GetAllFiles() As DataSet

        Dim sql As String

        sql = "SELECT * FROM tblFiles"

        Return GetFiles(sql)

    End Function

    Public Overridable Function GetFiles(ByVal sql As String) As DataSet

        Return db.ExecuteDataSet(CommandType.Text, sql)

    End Function

    Public Function GetFilesByFileType(ByVal FileType As String, ByVal ObjectType As String, ProjectID As Long)

        Dim sql As String

        sql = "SELECT F.* FROM tblFiles F inner join luFileTypes T on F.FileTypeID = T.FileTypeID "
        sql &= "INNER JOIN tblDocumentObjects O on O.DocumentID = F.FileID "
        sql &= " INNER JOIN luObjectTypes OT on OT.ObjectTypeID = O.ObjectTypeID "
        sql &= " where T.Description = '" & FileType & "' AND OT.Description = '" & ObjectType & "' AND O.ObjectID =  " & ProjectID

        Return GetFiles(sql)

    End Function

#End Region 

    Protected Friend Overridable Sub LoadDataRecord(ByRef Record As Object) 

        With Record 

            mFileID = Catchnull(.Item("FileID"), 0)
            mCreatedBy = Catchnull(.Item("CreatedBy"), 0)
            mUpdatedBy = Catchnull(.Item("UpdatedBy"), 0)
            mDate = Catchnull(.Item("Date"), "")
            mCreatedDate = Catchnull(.Item("CreatedDate"), "")
            mUpdatedDate = Catchnull(.Item("UpdatedDate"), "")
            mFileTypeID = Catchnull(.Item("FileTypeID"), 0)
            mTitle = Catchnull(.Item("Title"), "")
            mAuthor = Catchnull(.Item("Author"), "")
            mDescription = Catchnull(.Item("Description"), "")
            mFilePath = Catchnull(.Item("FilePath"), "")
            mFileExtension = Catchnull(.Item("FileExtension"), "")
            mAuthorOrganization = Catchnull(.Item("AuthorOrganization"), "")

        End With 

    End Sub 

#region "Save" 

    Public Overridable Sub GenerateSaveParameters(ByRef db As Database, ByRef cmd As System.Data.Common.DbCommand) 

        db.AddInParameter(cmd, "@FileID", DBType.Int32, mFileID) 
        db.AddInParameter(cmd, "@UpdatedBy", DBType.Int32, mObjectUserID) 
        db.AddInParameter(cmd, "@Date", DBType.String, mDate) 
        db.AddInParameter(cmd, "@FileTypeID", DbType.String, mFileTypeID)
        db.AddInParameter(cmd, "@Title", DbType.String, mTitle)
        db.AddInParameter(cmd, "@Author", DBType.String, mAuthor) 
        db.AddInParameter(cmd, "@Description", DBType.String, mDescription) 
        db.AddInParameter(cmd, "@FilePath", DBType.String, mFilePath) 
        db.AddInParameter(cmd, "@FileExtension", DBType.String, mFileExtension) 
        db.AddInParameter(cmd, "@AuthorOrganization", DBType.String, mAuthorOrganization) 

    End Sub 

Public Overridable Function Save() As Boolean 

        Dim cmd As System.Data.Common.DbCommand = db.GetStoredProcCommand("sp_Save_Files") 

        GenerateSaveParameters(db, cmd)

        Try 

            Dim ds As DataSet = db.ExecuteDataSet(cmd) 

            If ds isnot nothing andalso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then 

                mFileID = ds.Tables(0).Rows(0)(0) 

            End If 

            Return True 

        Catch ex As Exception 

            log.Error(ex)
            Return False 

        End Try 

    End Function 

#End Region 

#Region "Delete" 

    Public Overridable Function Delete() As Boolean 

        'Return Delete("UPDATE tblFiles SET Deleted = 1 WHERE FileID = " & mFileID) 
        Return Delete("DELETE FROM tblFiles WHERE FileID = " & mFileID) 

    End Function 

    Protected Overridable Function Delete(ByVal DeleteSQL As String) As Boolean 

        Try 

            db.ExecuteNonQuery(CommandType.Text, DeleteSQL) 
            Return True 

        Catch e As Exception 

            log.Error(e)
            Return False 

        End Try 

    End Function 

#End Region 

#end region

End Class