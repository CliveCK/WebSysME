Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Universal.CommonFunctions

Public Class Address

#Region "Variable"

    Private mAddressID As Integer
    Private mCityID As Integer
    Private mSuburbID As Integer
    Private mSectionID As Integer
    Private mStreetID As Integer
    Private mOwnerID As Integer
    Private mDistrictID As Integer
    Private mVillageID As Integer
    Private mWardID As Integer
    Private mSerialNo As String
    Private mOwnerType As String
    Private mHouseNo As String

    Protected db As Database
    Protected mConnectionName As String
    Protected mObjectUserID As Long

    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

#End Region

#Region "Properties"

    Public Property AddressID() As Integer
        Get
            Return mAddressID
        End Get
        Set(value As Integer)
            mAddressID = value
        End Set
    End Property

    Public Property CityID() As Integer
        Get
            Return mCityID
        End Get
        Set(value As Integer)
            mCityID = value
        End Set
    End Property

    Public Property SectionID() As Integer
        Get
            Return mSectionID
        End Get
        Set(value As Integer)
            mSectionID = value
        End Set
    End Property

    Public Property SuburbID() As Integer
        Get
            Return mSuburbID
        End Get
        Set(value As Integer)
            mSuburbID = value
        End Set
    End Property

    Public Property StreetID() As Integer
        Get
            Return mStreetID
        End Get
        Set(value As Integer)
            mStreetID = value
        End Set
    End Property

    Public Property Village() As Integer
        Get
            Return mVillageID
        End Get
        Set(value As Integer)
            mVillageID = value
        End Set
    End Property

    Public Property WardID() As Integer
        Get
            Return mWardID
        End Get
        Set(value As Integer)
            mWardID = value
        End Set
    End Property

    Public Property DistrictID() As Integer
        Get
            Return mDistrictID
        End Get
        Set(value As Integer)
            mDistrictID = value
        End Set
    End Property

    Public Property OwnerID() As Integer
        Get
            Return mOwnerID
        End Get
        Set(value As Integer)
            mOwnerID = value
        End Set
    End Property

    Public Property OwnerType() As String
        Get
            Return mOwnerType
        End Get
        Set(value As String)
            mOwnerType = value
        End Set
    End Property

    Public Property SerialNo() As String
        Get
            Return mSerialNo
        End Get
        Set(value As String)
            mSerialNo = value
        End Set
    End Property

    Public Property HouseNo() As String
        Get
            Return mHouseNo
        End Get
        Set(value As String)
            mHouseNo = value
        End Set
    End Property
#End Region

#Region "Constructors"

    Public Sub New(ByVal ConnectionName As String, ByVal ObjectUserID As Long)

        mObjectUserID = ObjectUserID
        mConnectionName = ConnectionName
        db = New DatabaseProviderFactory().Create(ConnectionName)

    End Sub

#End Region

#Region "Retrieve Overloads"

    Public Overridable Function Retrieve() As Boolean

        Return Me.Retrieve(mAddressID)

    End Function

    Public Overridable Function Retrieve(ByVal BeneficiaryID As Long) As Boolean

        Dim sql As String

        If AddressID > 0 Then
            sql = "SELECT * FROM tblAddresses WHERE AddressID = " & AddressID
        Else
            sql = "SELECT * FROM tblAddresses WHERE AddressID = " & mAddressID
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

                log.Error("Address not found.")

                Return False

            End If

        Catch e As Exception

            log.Error(e)
            Return False

        End Try

    End Function

    Public Overridable Function GetAddress() As System.Data.DataSet

        Return GetBeneficiary(mAddressID)

    End Function

    Public Overridable Function GetAddress(ByVal AddressID As Long) As DataSet

        Dim sql As String

        If AddressID > 0 Then
            sql = "SELECT * FROM tblAddresses WHERE AddressID = " & AddressID
        Else
            sql = "SELECT * FROM tblAddresses WHERE AddressID = " & mAddressID
        End If

        Return GetBeneficiary(sql)

    End Function

    Protected Overridable Function GetBeneficiary(ByVal sql As String) As DataSet

        Return db.ExecuteDataSet(CommandType.Text, sql)

    End Function

#End Region

    Protected Friend Overridable Sub LoadDataRecord(ByRef Record As Object)

        With Record

            mAddressID = Catchnull(.Item("AddressID"), 0)
            mCityID = Catchnull(.Item("CityID"), 0)
            mDistrictID = Catchnull(.Item("DistrictID"), 0)
            mSectionID = Catchnull(.Item("SectionID"), 0)
            mSuburbID = Catchnull(.Item("SuburbID"), 0)
            mWardID = Catchnull(.Item("WardID"), 0)
            mOwnerID = Catchnull(.Item("OwnerID"), 0)
            mVillageID = Catchnull(.Item("VillageID"), 0)
            mStreetID = Catchnull(.Item("StreetID"), 0)
            mOwnerType = Catchnull(.Item("OwnerType"), 0)
            mHouseNo = Catchnull(.Item("HouseNo"), "")
            mSerialNo = Catchnull(.Item("SerialNo"), "")
            mSerialNo = Catchnull(.Item("SerialNo"), "")

        End With

    End Sub

#Region "Save"

    Public Overridable Sub GenerateSaveParameters(ByRef db As Database, ByRef cmd As System.Data.Common.DbCommand)

        db.AddInParameter(cmd, "@AddressID", DbType.Int32, mAddressID)
        db.AddInParameter(cmd, "@OwnerID", DbType.Int32, mOwnerID)
        db.AddInParameter(cmd, "@VillageID", DbType.Int32, mVillageID)
        db.AddInParameter(cmd, "@StreetID", DbType.Int32, mStreetID)
        db.AddInParameter(cmd, "@VillageID", DBType.Int32, mVillageID)
        db.AddInParameter(cmd, "@StreetID", DBType.Int32, mStreetID)
        db.AddInParameter(cmd, "@UpdatedBy", DBType.Int32, mObjectUserID)
        db.AddInParameter(cmd, "@OwnerType", DbType.String, mOwnerType)
        db.AddInParameter(cmd, "@HouseNo", DbType.String, mHouseNo)
        db.AddInParameter(cmd, "@SerialNo", DbType.String, mSerialNo)
        db.AddInParameter(cmd, "@SerialNo", DbType.String, mSerialNo)

    End Sub

    Public Overridable Function Save() As Boolean

        Dim cmd As System.Data.Common.DbCommand = db.GetStoredProcCommand("sp_Save_Address")

        GenerateSaveParameters(db, cmd)

        Try

            Dim ds As DataSet = db.ExecuteDataSet(cmd)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

                mAddressID = ds.Tables(0).Rows(0)(0)

            End If

            Return True

        Catch ex As Exception

            log.Error(ex)
            Return False

        End Try

    End Function

#End Region

End Class
