Imports Microsoft.Practices.EnterpriseLibrary.Data 
Imports Universal.CommonFunctions 

Public Class Organization

#region "Variables"

    Protected mOrganizationID As long
    Protected mWardID As long
    Protected mOrganizationTypeID As long
    Protected mLongitude As decimal
    Protected mLatitude As decimal
    Protected mContactNo As long
    Protected mName As string
    Protected mDescription As string
    Protected mContactName As String
    Protected mAddress As String
    Protected mEmail As String

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

    Public  Property OrganizationID() As long
        Get
		return mOrganizationID
        End Get
        Set(ByVal value As long)
		mOrganizationID = value
        End Set
    End Property

    Public  Property WardID() As long
        Get
		return mWardID
        End Get
        Set(ByVal value As long)
		mWardID = value
        End Set
    End Property

    Public  Property OrganizationTypeID() As long
        Get
		return mOrganizationTypeID
        End Get
        Set(ByVal value As long)
		mOrganizationTypeID = value
        End Set
    End Property

    Public  Property Longitude() As decimal
        Get
		return mLongitude
        End Get
        Set(ByVal value As decimal)
		mLongitude = value
        End Set
    End Property

    Public  Property Latitude() As decimal
        Get
		return mLatitude
        End Get
        Set(ByVal value As decimal)
		mLatitude = value
        End Set
    End Property

    Public  Property ContactNo() As long
        Get
		return mContactNo
        End Get
        Set(ByVal value As long)
		mContactNo = value
        End Set
    End Property

    Public  Property Name() As string
        Get
		return mName
        End Get
        Set(ByVal value As string)
		mName = value
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

    Public Property Address() As String
        Get
            Return mAddress
        End Get
        Set(value As String)
            mAddress = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return mEmail
        End Get
        Set(value As String)
            mEmail = value
        End Set
    End Property

    Public  Property ContactName() As string
        Get
		return mContactName
        End Get
        Set(ByVal value As string)
		mContactName = value
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

    OrganizationID = 0
    mWardID = 0
    mOrganizationTypeID = 0
    mLongitude = 0.0
    mLatitude = 0.0
    mContactNo = 0
    mName = ""
    mDescription = ""
        mContactName = ""
        mEmail = ""
        mAddress = ""

End Sub

#Region "Retrieve Overloads" 

    Public Overridable Function Retrieve() As Boolean 

        Return Me.Retrieve(mOrganizationID) 

    End Function 

    Public Overridable Function Retrieve(ByVal OrganizationID As Long) As Boolean 

        Dim sql As String 

        If OrganizationID > 0 Then 
            sql = "SELECT * FROM tblOrganization WHERE OrganizationID = " & OrganizationID
        Else 
            sql = "SELECT * FROM tblOrganization WHERE OrganizationID = " & mOrganizationID
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

                log.Warn("Organization not found.")

                Return False 

            End If 

        Catch e As Exception 

            log.Error(e)
            Return False

        End Try

    End Function

    Public Function RetrieveAll() As DataSet

        Dim sql As String = "Select T.Description As OrganizationType, O.* from tblOrganization O inner join luOrganizationTypes T on O.OrganizationTypeID = T.OrganizationTypeID"

        Return GetOrganization(sql)

    End Function

    Public Overridable Function GetOrganization() As System.Data.DataSet

        Return GetOrganization(mOrganizationID)

    End Function

    Public Overridable Function GetOrganization(ByVal OrganizationID As Long) As DataSet

        Dim sql As String

        If OrganizationID > 0 Then
            sql = "SELECT * FROM tblOrganization WHERE OrganizationID = " & OrganizationID
        Else
            sql = "SELECT * FROM tblOrganization WHERE OrganizationID = " & mOrganizationID
        End If

        Return GetOrganization(sql)

    End Function

    Protected Overridable Function GetOrganization(ByVal sql As String) As DataSet

        Return db.ExecuteDataSet(CommandType.Text, sql)

    End Function

#End Region

    Protected Friend Overridable Sub LoadDataRecord(ByRef Record As Object)

        With Record

            mOrganizationID = Catchnull(.Item("OrganizationID"), 0)
            mWardID = Catchnull(.Item("WardID"), 0)
            mOrganizationTypeID = Catchnull(.Item("OrganizationTypeID"), 0)
            mLongitude = Catchnull(.Item("Longitude"), 0.0)
            mLatitude = Catchnull(.Item("Latitude"), 0.0)
            mContactNo = Catchnull(.Item("ContactNo"), 0)
            mName = Catchnull(.Item("Name"), "")
            mDescription = Catchnull(.Item("Description"), "")
            mContactName = Catchnull(.Item("ContactName"), "")
            mAddress = Catchnull(.Item("Address"), "")
            mEmail = Catchnull(.Item("EmailAddress"), "")

        End With

    End Sub

#Region "Save"

    Public Overridable Sub GenerateSaveParameters(ByRef db As Database, ByRef cmd As System.Data.Common.DbCommand)

        db.AddInParameter(cmd, "@OrganizationID", DBType.Int32, mOrganizationID)
        db.AddInParameter(cmd, "@WardID", DBType.Int32, mWardID)
        db.AddInParameter(cmd, "@OrganizationTypeID", DBType.Int32, mOrganizationTypeID)
        db.AddInParameter(cmd, "@Longitude", DbType.Decimal, mLongitude)
        db.AddInParameter(cmd, "@Latitude", DbType.Decimal, mLatitude)
        db.AddInParameter(cmd, "@ContactNo", DBType.Int64, mContactNo)
        db.AddInParameter(cmd, "@Name", DBType.String, mName)
        db.AddInParameter(cmd, "@Description", DBType.String, mDescription)
        db.AddInParameter(cmd, "@ContactName", DbType.String, mContactName)
        db.AddInParameter(cmd, "@Address", DbType.String, mAddress)
        db.AddInParameter(cmd, "@EmailAddress", DbType.String, mEmail)

    End Sub

    Public Overridable Function Save() As Boolean

        Dim cmd As System.Data.Common.DbCommand = db.GetStoredProcCommand("sp_Save_Organization")

        GenerateSaveParameters(db, cmd)

        Try

            Dim ds As DataSet = db.ExecuteDataSet(cmd)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

                mOrganizationID = ds.Tables(0).Rows(0)(0)

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

        'Return Delete("UPDATE tblOrganization SET Deleted = 1 WHERE OrganizationID = " & mOrganizationID) 
        Return Delete("DELETE FROM tblOrganization WHERE OrganizationID = " & mOrganizationID)

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