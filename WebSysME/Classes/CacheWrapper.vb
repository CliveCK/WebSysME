'Imports SpectrumITS.DataManagement.SQLHeirarchies
'Imports SpectrumITS.MemberManagement.BusinessLogic

''**THIS FILE IS SHARED AMONGST MANY PROJECTS**

Partial Public Class CacheWrapper

    '    Private objCategories As NestedSetManager

    '    Public Shared Property MainMenuItemsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MainMenuItemsCache") Is Nothing Then

    '                Dim myMenuItems As New UserMenu("ConnectionString")
    '                HttpContext.Current.Cache("ConnectionString" & "MainMenuItemsCache") = myMenuItems.GetContextMenu("MainMenu", False)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MainMenuItemsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MainMenuItemsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MainMenuItemsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property QuickIconsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "QuickIconsCache") Is Nothing Then

    '                Dim myQuickIcons As New QuickIconMenuItem("ConnectionString", CookiesWrapper.UserID)
    '                HttpContext.Current.Cache("ConnectionString" & "QuickIconsCache") = myQuickIcons.GetQuickIcons

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "QuickIconsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MainMenuItemsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MainMenuItemsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property MenuItemsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MenuItemsCache") Is Nothing Then
    '                Dim myMenuItems As New UserMenu("ConnectionString")
    '                HttpContext.Current.Cache("ConnectionString" & "MenuItemsCache") = myMenuItems.GetContextMenu(False)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MenuItemsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MenuItemsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MenuItemsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property MenuActionItemsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MenuActionItemsCache") Is Nothing Then
    '                Dim myMenuItems As New UserMenu("ConnectionString")
    '                HttpContext.Current.Cache("ConnectionString" & "MenuActionItemsCache") = myMenuItems.GetContextMenu(False, "MainMenu,Dashboard")

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MenuActionItemsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MenuActionItemsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MenuActionItemsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property MenuRightsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MenuRightsCache") Is Nothing Then

    '                Dim objMenulevelPermisions As New SpectrumITS.PermissionsManager.BusinessLogic.MenuLevelAccess("ConnectionString", CookiesWrapper.UserID)
    '                HttpContext.Current.Cache("ConnectionString" & "MenuRightsCache") = objMenulevelPermisions.GetSelectedUserMenuRights(CookiesWrapper.UserID)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MenuRightsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MenuRightsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MenuRightsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property FunctionalityRightsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "FunctionalityRightsCache") Is Nothing Then

    '                Dim objFunctionalityPermisions As New SpectrumITS.PermissionsManager.BusinessLogic.Functionality("ConnectionString", CookiesWrapper.UserID)
    '                HttpContext.Current.Cache("ConnectionString" & "FunctionalityRightsCache") = objFunctionalityPermisions.GetSelectedUserFunctionalityRights(CookiesWrapper.UserID)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "FunctionalityRightsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "FunctionalityRightsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "FunctionalityRightsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property PageRightsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "PageRightsCache") Is Nothing Then
    '                Dim objMenulevelPermisions As New SpectrumITS.PermissionsManager.BusinessLogic.MenuLevelAccess("ConnectionString", CookiesWrapper.UserID)
    '                HttpContext.Current.Cache("ConnectionString" & "PageRightsCache") = objMenulevelPermisions.GetUserPageRights(CookiesWrapper.UserID)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "PageRightsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "PageRightsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "PageRightsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property MailReceipientsCache() As DataSet

    '        Get

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MailReceipientsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MailReceipientsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MailReceipientsCache") = value
    '            End If

    '        End Set

    '    End Property

    Public Shared Property ReportsCache() As DataSet

        Get

            Dim db As Microsoft.Practices.EnterpriseLibrary.Data.Database = New Microsoft.Practices.EnterpriseLibrary.Data.DatabaseProviderFactory().Create("Demo")

            Return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM tblReports")

        End Get

        Set(ByVal value As DataSet)

            If IsNothing(value) Then
                HttpContext.Current.Cache.Remove("ConnectionString" & "ReportsCache")
            Else
                HttpContext.Current.Cache("ConnectionString" & "ReportsCache") = value
            End If

        End Set

    End Property

    '    Public Shared Property MemberTypeCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MemberTypeCache") Is Nothing Then

    '                Dim db As Microsoft.Practices.EnterpriseLibrary.Data.Database = New Microsoft.Practices.EnterpriseLibrary.Data.DatabaseProviderFactory().Create("ConnectionString")
    '                Dim myMemberTypes As System.Data.DataSet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM luMemberTypes")

    '                HttpContext.Current.Cache("ConnectionString" & "MemberTypeCache") = myMemberTypes

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MemberTypeCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MemberTypeCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MemberTypeCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property BillingGroupCache() As DataSet

    '        Get

    '            'If HttpContext.Current.Cache("ConnectionString" & "BillingGroupCache") Is Nothing Then

    '            Dim db As Microsoft.Practices.EnterpriseLibrary.Data.Database = New Microsoft.Practices.EnterpriseLibrary.Data.DatabaseProviderFactory().Create("ConnectionString")
    '            Dim myBillingGroups As System.Data.DataSet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM luBillingGroups")

    '            HttpContext.Current.Cache("ConnectionString" & "BillingGroupCache") = myBillingGroups

    '            'End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "BillingGroupCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "BillingGroupCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "BillingGroupCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property DistributionGroupsCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "DistributionGroupsCache") Is Nothing Then

    '                Dim objCategories As NestedSetManager = New NestedSetManager("ConnectionString", "tblCategories", "LeftValue", "RightValue", "CategoryID", "Description", "ParentID", "TreeID", "Code")
    '                Dim myMenuItems As New UserMenu("ConnectionString")
    '                HttpContext.Current.Cache("ConnectionString" & "DistributionGroupsCache") = objCategories.GetChildren(Options.Categories.DistributionGroupsID, False)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "DistributionGroupsCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "DistributionGroupsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "DistributionGroupsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property SegregatedFundsCache() As DataTable

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "SegregatedFundsCache") Is Nothing Then

    '                Dim objSegregatedFunds As New SegregatedFund("ConnectionString", CookiesWrapper.UserID)

    '                Dim dt As DataTable = objSegregatedFunds.GetAllSegregatedFunds().Tables(0)

    '                HttpContext.Current.Cache("ConnectionString" & "SegregatedFundsCache") = dt

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "SegregatedFundsCache"), DataTable)

    '        End Get

    '        Set(ByVal value As DataTable)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "SegregatedFundsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "SegregatedFundsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property ActiveSegregatedFundsCache() As DataTable

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "ActiveSegregatedFundsCache") Is Nothing Then

    '                Dim objSegregatedFunds As New SegregatedFund("ConnectionString", CookiesWrapper.UserID)

    '                Dim dt As DataTable = objSegregatedFunds.GetAllActiveSegregatedFunds().Tables(0)

    '                HttpContext.Current.Cache("ConnectionString" & "ActiveSegregatedFundsCache") = dt

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "ActiveSegregatedFundsCache"), DataTable)

    '        End Get

    '        Set(ByVal value As DataTable)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "ActiveSegregatedFundsCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "ActiveSegregatedFundsCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property CategoriesCache() As DataTable

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "CategoriesCache") Is Nothing Then

    '                Dim objCategories As NestedSetManager = New NestedSetManager("ConnectionString", "tblCategories", "LeftValue", "RightValue", "CategoryID", "Description", "ParentID", "TreeID", "Code")

    '                Dim dt As DataTable = objCategories.GetTrees.Tables(0)

    '                HttpContext.Current.Cache("ConnectionString" & "CategoriesCache") = dt

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "CategoriesCache"), DataTable)

    '        End Get

    '        Set(ByVal value As DataTable)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "CategoriesCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "CategoriesCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property MySavedSearchesCache() As DataSet

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MySavedSearchesCache") Is Nothing Then

    '                Dim myMenuItems As New UserMenu("ConnectionString")

    '                'HttpContext.Current.Cache("ConnectionString" &"MySavedSearchesCache") = objSavedSearches.GetMySavedSearches(CookiesWrapper.UserID)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MySavedSearchesCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MySavedSearchesCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MySavedSearchesCache") = value
    '            End If

    '        End Set

    '    End Property

    '    Public Shared Property StatusCache() As DataTable

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "StatusCache") Is Nothing Then

    '                Dim objCategories As NestedSetManager = New NestedSetManager("ConnectionString", "tblCategories", "LeftValue", "RightValue", "CategoryID", "Description", "ParentID", "TreeID", "Code")

    '                Dim dt As DataTable = objCategories.GetTrees.Tables(0)

    '                HttpContext.Current.Cache("ConnectionString" & "StatusCache") = dt

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "StatusCache"), DataTable)

    '        End Get

    '        Set(ByVal value As DataTable)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "StatusCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "StatusCache") = value
    '            End If

    '        End Set
    '    End Property

    '    Public Shared ReadOnly Property RegisteredCompany()
    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "RegisteredCompany") Is Nothing Then

    '                Dim objLicense As New SpectrumITS.Licensing.License(Options.LicenseFilePath)

    '                Try

    '                    If objLicense.LoadLicenseFile Then

    '                        HttpContext.Current.Cache("ConnectionString" & "RegisteredCompany") = objLicense.Company

    '                    Else

    '                        HttpContext.Current.Cache("ConnectionString" & "RegisteredCompany") = "UNREGISTERED"

    '                    End If

    '                Catch ex As Exception

    '                    HttpContext.Current.Cache("ConnectionString" & "RegisteredCompany") = "UNREGISTERED"

    '                End Try

    '            End If

    '            Return HttpContext.Current.Cache("ConnectionString" & "RegisteredCompany")

    '        End Get
    '    End Property

    '    Public Shared Property MenuItemsAllCache() As DataSet

    '        ' This cache stores all the menu items that are in the database lunmenu table. 

    '        Get

    '            If HttpContext.Current.Cache("ConnectionString" & "MenuItemsAllCache") Is Nothing Then
    '                Dim myMenuItems As New UserMenu("ConnectionString")
    '                HttpContext.Current.Cache("ConnectionString" & "MenuItemsAllCache") = myMenuItems.GetAllContextMenu(False)

    '            End If

    '            Return CType(HttpContext.Current.Cache("ConnectionString" & "MenuItemsAllCache"), DataSet)

    '        End Get

    '        Set(ByVal value As DataSet)

    '            If IsNothing(value) Then
    '                HttpContext.Current.Cache.Remove("ConnectionString" & "MenuItemsAllCache")
    '            Else
    '                HttpContext.Current.Cache("ConnectionString" & "MenuItemsAllCache") = value
    '            End If

    '        End Set

    '    End Property

End Class
