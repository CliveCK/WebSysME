Imports Microsoft.Practices.EnterpriseLibrary.Data 
Imports Universal.CommonFunctions 

Public Class Beneficiary

#region "Variables"

    Protected mBeneficiaryID As long
    Protected mSuffix As long
    Protected mMaritalStatus As long
    Protected mHealthStatus As long
    Protected mDisabilityStatus As long
    Protected mLevelOfEducation As long
    Protected mRegularity As long
    Protected mVillageID As long
    Protected mStreetID As long
    Protected mCreatedBy As long
    Protected mUpdatedBy As long
    Protected mOpharnhood As long
    Protected mMajorSourceIncome As long
    Protected mContactNo As long
    Protected mCondition As long
    Protected mAttendance As Long
    Protected mIsDependant As Integer
    Protected mDisability As long
    Protected mDateOfBirth As string
    Protected mCreatedDate As string
    Protected mUpdatedDate As string
    Protected mIsUrban As boolean
    Protected mMemberNo As string
    Protected mFirstName As string
    Protected mSurname As string
    Protected mSex As string
    Protected mNationlIDNo As string
    Protected mHouseNo As string
    Protected mSerialNo As String
    Protected mParentID As Long
    Protected mRelationshipID As Long

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

    Public  Property BeneficiaryID() As long
        Get
		return mBeneficiaryID
        End Get
        Set(ByVal value As long)
		mBeneficiaryID = value
        End Set
    End Property

    Public Property Suffix() As Long
        Get
            Return mSuffix
        End Get
        Set(ByVal value As Long)
            mSuffix = value
        End Set
    End Property

    Public Property RelationshipID() As Long
        Get
            Return mRelationshipID
        End Get
        Set(ByVal value As Long)
            mRelationshipID = value
        End Set
    End Property

    Public  Property MaritalStatus() As long
        Get
		return mMaritalStatus
        End Get
        Set(ByVal value As long)
		mMaritalStatus = value
        End Set
    End Property

    Public  Property HealthStatus() As long
        Get
		return mHealthStatus
        End Get
        Set(ByVal value As long)
		mHealthStatus = value
        End Set
    End Property

    Public  Property DisabilityStatus() As long
        Get
		return mDisabilityStatus
        End Get
        Set(ByVal value As long)
		mDisabilityStatus = value
        End Set
    End Property

    Public  Property LevelOfEducation() As long
        Get
		return mLevelOfEducation
        End Get
        Set(ByVal value As long)
		mLevelOfEducation = value
        End Set
    End Property

    Public  Property Regularity() As long
        Get
		return mRegularity
        End Get
        Set(ByVal value As long)
		mRegularity = value
        End Set
    End Property

    Public  Property VillageID() As long
        Get
		return mVillageID
        End Get
        Set(ByVal value As long)
		mVillageID = value
        End Set
    End Property

    Public  Property StreetID() As long
        Get
		return mStreetID
        End Get
        Set(ByVal value As long)
		mStreetID = value
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

    Public Property IsDependant() As Integer
        Get
            Return mIsDependant
        End Get
        Set(ByVal value As Integer)
            mIsDependant = value
        End Set
    End Property

    Public  Property Opharnhood() As long
        Get
		return mOpharnhood
        End Get
        Set(ByVal value As long)
		mOpharnhood = value
        End Set
    End Property

    Public  Property MajorSourceIncome() As long
        Get
		return mMajorSourceIncome
        End Get
        Set(ByVal value As long)
		mMajorSourceIncome = value
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

    Public  Property Condition() As long
        Get
		return mCondition
        End Get
        Set(ByVal value As long)
		mCondition = value
        End Set
    End Property

    Public  Property Attendance() As long
        Get
		return mAttendance
        End Get
        Set(ByVal value As long)
		mAttendance = value
        End Set
    End Property

    Public  Property Disability() As long
        Get
		return mDisability
        End Get
        Set(ByVal value As long)
		mDisability = value
        End Set
    End Property

    Public  Property DateOfBirth() As string
        Get
		return mDateOfBirth
        End Get
        Set(ByVal value As string)
		mDateOfBirth = value
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

    Public  Property IsUrban() As boolean
        Get
		return mIsUrban
        End Get
        Set(ByVal value As boolean)
		mIsUrban = value
        End Set
    End Property

    Public  Property MemberNo() As string
        Get
		return mMemberNo
        End Get
        Set(ByVal value As string)
		mMemberNo = value
        End Set
    End Property

    Public  Property FirstName() As string
        Get
		return mFirstName
        End Get
        Set(ByVal value As string)
		mFirstName = value
        End Set
    End Property

    Public  Property Surname() As string
        Get
		return mSurname
        End Get
        Set(ByVal value As string)
		mSurname = value
        End Set
    End Property

    Public  Property Sex() As string
        Get
		return mSex
        End Get
        Set(ByVal value As string)
		mSex = value
        End Set
    End Property

    Public  Property NationlIDNo() As string
        Get
		return mNationlIDNo
        End Get
        Set(ByVal value As string)
		mNationlIDNo = value
        End Set
    End Property

    Public  Property HouseNo() As string
        Get
		return mHouseNo
        End Get
        Set(ByVal value As string)
		mHouseNo = value
        End Set
    End Property

    Public Property ParentID() As Long
        Get
            Return mParentID
        End Get
        Set(ByVal value As Long)
            mParentID = value
        End Set
    End Property

    Public  Property SerialNo() As string
        Get
		return mSerialNo
        End Get
        Set(ByVal value As string)
		mSerialNo = value
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

    BeneficiaryID = 0
    mSuffix = 0
    mMaritalStatus = 0
    mHealthStatus = 0
    mDisabilityStatus = 0
    mLevelOfEducation = 0
    mRegularity = 0
    mVillageID = 0
    mStreetID = 0
    mCreatedBy = mObjectUserID
    mUpdatedBy = 0
    mOpharnhood = 0
    mMajorSourceIncome = 0
    mContactNo = 0
        mCondition = 0
        mIsDependant = 0
    mAttendance = 0
    mDisability = 0
    mDateOfBirth = ""
    mCreatedDate = ""
    mUpdatedDate = ""
    mIsUrban = FALSE
    mMemberNo = ""
    mFirstName = ""
    mSurname = ""
    mSex = ""
    mNationlIDNo = ""
    mHouseNo = ""
        mSerialNo = ""
        mParentID = 0
        mRelationshipID = 0

    End Sub

#Region "Retrieve Overloads" 

    Public Overridable Function Retrieve() As Boolean 

        Return Me.Retrieve(mBeneficiaryID) 

    End Function 

    Public Overridable Function Retrieve(ByVal BeneficiaryID As Long) As Boolean 

        Dim sql As String 

        If BeneficiaryID > 0 Then 
            sql = "SELECT * FROM tblBeneficiaries WHERE BeneficiaryID = " & BeneficiaryID
        Else 
            sql = "SELECT * FROM tblBeneficiaries WHERE BeneficiaryID = " & mBeneficiaryID
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

                log.Warn("Beneficiary not found.")

                Return False 

            End If 

        Catch e As Exception 

            log.Error(e)
            Return False 

        End Try 

    End Function 

    Public Overridable Function GetBeneficiary() As System.Data.DataSet

        Return GetBeneficiary(mBeneficiaryID) 

    End Function 

    Public Overridable Function GetBeneficiary(ByVal BeneficiaryID As Long) As DataSet 

        Dim sql As String 

        If BeneficiaryID > 0 Then 
            sql = "SELECT * FROM tblBeneficiaries WHERE BeneficiaryID = " & BeneficiaryID
        Else 
            sql = "SELECT * FROM tblBeneficiaries WHERE BeneficiaryID = " & mBeneficiaryID
        End If 

        Return GetBeneficiary(sql) 

    End Function

    Public Function GetAllBeneficiaries() As DataSet

        Dim sql As String = "SELECT * FROM tblBeneficiaries"

        Return GetBeneficiary(sql)

    End Function

    Public Function GetBeneficiaryHousehold(ByVal BeneficiaryID As Long) As DataSet

        Dim sql As String = "SELECT * FROM tblBeneficiaries WHERE ParentID = " & BeneficiaryID & " OR BeneficiaryID = " & BeneficiaryID

        Return GetBeneficiary(sql)

    End Function

    Protected Overridable Function GetBeneficiary(ByVal sql As String) As DataSet 

        Return db.ExecuteDataSet(CommandType.Text, sql) 

    End Function 

#End Region 

    Protected Friend Overridable Sub LoadDataRecord(ByRef Record As Object) 

        With Record 

            mBeneficiaryID = Catchnull(.Item("BeneficiaryID"), 0)
            mSuffix = Catchnull(.Item("Suffix"), 0)
            mMaritalStatus = Catchnull(.Item("MaritalStatus"), 0)
            mHealthStatus = Catchnull(.Item("HealthStatus"), 0)
            mDisabilityStatus = Catchnull(.Item("DisabilityStatus"), 0)
            mLevelOfEducation = Catchnull(.Item("LevelOfEducation"), 0)
            mRegularity = Catchnull(.Item("Regularity"), 0)
            mVillageID = Catchnull(.Item("VillageID"), 0)
            mStreetID = Catchnull(.Item("StreetID"), 0)
            mIsDependant = Catchnull(.Item("IsDependant"), 0)
            mCreatedBy = Catchnull(.Item("CreatedBy"), 0)
            mUpdatedBy = Catchnull(.Item("UpdatedBy"), 0)
            mOpharnhood = Catchnull(.Item("Opharnhood"), 0)
            mMajorSourceIncome = Catchnull(.Item("MajorSourceIncome"), 0)
            mContactNo = Catchnull(.Item("ContactNo"), 0)
            mCondition = Catchnull(.Item("Condition"), 0)
            mAttendance = Catchnull(.Item("Attendance"), 0)
            mDisability = Catchnull(.Item("Disability"), 0)
            mDateOfBirth = Catchnull(.Item("DateOfBirth"), "")
            mCreatedDate = Catchnull(.Item("CreatedDate"), "")
            mUpdatedDate = Catchnull(.Item("UpdatedDate"), "")
            mIsUrban = Catchnull(.Item("IsUrban"), FALSE)
            mMemberNo = Catchnull(.Item("MemberNo"), "")
            mFirstName = Catchnull(.Item("FirstName"), "")
            mSurname = Catchnull(.Item("Surname"), "")
            mSex = Catchnull(.Item("Sex"), "")
            mNationlIDNo = Catchnull(.Item("NationlIDNo"), "")
            mHouseNo = Catchnull(.Item("HouseNo"), "")
            mSerialNo = Catchnull(.Item("SerialNo"), "")
            mParentID = Catchnull(.Item("ParentID"), 0)
            mRelationshipID = Catchnull(.Item("RelationshipID"), 0)

        End With 

    End Sub 

#region "Save" 

    Public Overridable Sub GenerateSaveParameters(ByRef db As Database, ByRef cmd As System.Data.Common.DbCommand) 

        db.AddInParameter(cmd, "@BeneficiaryID", DBType.Int32, mBeneficiaryID) 
        db.AddInParameter(cmd, "@Suffix", DBType.Int32, mSuffix) 
        db.AddInParameter(cmd, "@MaritalStatus", DBType.Int32, mMaritalStatus) 
        db.AddInParameter(cmd, "@HealthStatus", DBType.Int32, mHealthStatus) 
        db.AddInParameter(cmd, "@DisabilityStatus", DBType.Int32, mDisabilityStatus) 
        db.AddInParameter(cmd, "@LevelOfEducation", DBType.Int32, mLevelOfEducation) 
        db.AddInParameter(cmd, "@Regularity", DBType.Int32, mRegularity) 
        db.AddInParameter(cmd, "@VillageID", DBType.Int32, mVillageID) 
        db.AddInParameter(cmd, "@StreetID", DBType.Int32, mStreetID) 
        db.AddInParameter(cmd, "@UpdatedBy", DBType.Int32, mObjectUserID) 
        db.AddInParameter(cmd, "@Opharnhood", DBType.Int32, mOpharnhood) 
        db.AddInParameter(cmd, "@MajorSourceIncome", DBType.Int32, mMajorSourceIncome) 
        db.AddInParameter(cmd, "@ContactNo", DBType.Int32, mContactNo) 
        db.AddInParameter(cmd, "@Condition", DBType.Int32, mCondition) 
        db.AddInParameter(cmd, "@Attendance", DBType.Int32, mAttendance) 
        db.AddInParameter(cmd, "@Disability", DBType.Int32, mDisability) 
        db.AddInParameter(cmd, "@DateOfBirth", DBType.String, mDateOfBirth) 
        db.AddInParameter(cmd, "@IsUrban", DBType.Boolean, mIsUrban) 
        db.AddInParameter(cmd, "@MemberNo", DBType.String, mMemberNo) 
        db.AddInParameter(cmd, "@FirstName", DBType.String, mFirstName) 
        db.AddInParameter(cmd, "@Surname", DbType.String, mSurname)
        db.AddInParameter(cmd, "@IsDependant", DbType.String, mIsDependant)
        db.AddInParameter(cmd, "@Sex", DBType.String, mSex) 
        db.AddInParameter(cmd, "@NationlIDNo", DBType.String, mNationlIDNo) 
        db.AddInParameter(cmd, "@HouseNo", DBType.String, mHouseNo) 
        db.AddInParameter(cmd, "@SerialNo", DbType.String, mSerialNo)
        db.AddInParameter(cmd, "@ParentID", DbType.Int32, IIf(mParentID > 0, mParentID, DBNull.Value))
        db.AddInParameter(cmd, "@RelationshipID", DbType.Int32, mRelationshipID)

    End Sub 

Public Overridable Function Save() As Boolean 

        Dim cmd As System.Data.Common.DbCommand = db.GetStoredProcCommand("sp_Save_Beneficiary") 

        GenerateSaveParameters(db, cmd)

        Try 

            Dim ds As DataSet = db.ExecuteDataSet(cmd) 

            If ds isnot nothing andalso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then 

                mBeneficiaryID = ds.Tables(0).Rows(0)(0) 

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

        'Return Delete("UPDATE tblBeneficiaries SET Deleted = 1 WHERE BeneficiaryID = " & mBeneficiaryID) 
        Return Delete("DELETE FROM tblBeneficiaries WHERE BeneficiaryID = " & mBeneficiaryID) 

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

#End Region

#Region "Miscellaneous"

    Public Function GenerateMemberNo() As String

        Return mSurname.Substring(0, 2) & mBeneficiaryID.ToString().PadLeft(4, "0")

    End Function

    Public Function GetNextSuffix() As Long

        Try

            Dim cmd As Common.DbCommand = db.GetStoredProcCommand("sp_GenerateNextSuffix")

            db.AddInParameter(cmd, "@ParentID", DbType.String, IIf(mIsDependant = 0, mBeneficiaryID, mParentID))

            Return db.ExecuteScalar(cmd)

        Catch ex As Exception

            log.Error(ex)
            Return 0

        End Try

    End Function

#End Region

End Class