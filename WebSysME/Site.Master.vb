Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String
    Private db As Database = New DatabaseProviderFactory().Create("Demo")

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' The code below helps to protect against XSRF attacks
        'Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        'Dim requestCookieGuidValue As Guid
        'If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
        '    ' Use the Anti-XSRF token from the cookie
        '    _antiXsrfTokenValue = requestCookie.Value
        '    Page.ViewStateUserKey = _antiXsrfTokenValue
        'Else
        '    ' Generate a new Anti-XSRF token and save to the cookie
        '    _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
        '    Page.ViewStateUserKey = _antiXsrfTokenValue

        '    Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With { _
        '         .HttpOnly = True, _
        '         .Value = _antiXsrfTokenValue _
        '    }
        '    If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
        '        responseCookie.Secure = True
        '    End If
        '    Response.Cookies.[Set](responseCookie)
        'End If

        'AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        'If Not IsPostBack Then
        '    ' Set Anti-XSRF token
        '    ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
        '    ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        'Else
        '    ' Validate the Anti-XSRF token
        '    If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
        '        Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
        '    End If
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            hypUser.Text = CookiesWrapper.UserFullName

            With radmMainMenu

                .DataSource = db.ExecuteDataSet(CommandType.Text, "Select * FROM luMenu WHERE MenuType = 'MainMenu'")
                .DataTextField = "MenuName"
                .DataValueField = "MenuID"
                .DataFieldID = "MenuID"
                .DataFieldParentID = "ParentID"
                .DataNavigateUrlField = "URL"
                .DataBind()

            End With

        End If

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut()
    End Sub

    Private Sub cmdLogout_Click(sender As Object, e As EventArgs) Handles cmdLogout.Click

        Dim myUser As New SecurityPolicy.UserManager(CookiesWrapper.ConnectionName, CookiesWrapper.UserID)

        If CookiesWrapper.LogID <> 0 AndAlso CookiesWrapper.LogID <> -1 Then

            myUser.LogID = CookiesWrapper.LogID
            myUser.SaveUserLog(CookiesWrapper.UserID)

        End If

        FormsAuthentication.SignOut()

        CookiesWrapper.ClearCookies()

        Session.Abandon()

        Dim authCookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, "")
        authCookie.Expires = DateTime.Now.AddYears(-1)
        Response.Cookies.Add(authCookie)

        Dim sessionCookie As HttpCookie = New HttpCookie("ASP.NET_SessionId", "")
        sessionCookie.Expires = DateTime.Now.AddYears(-1)
        Response.Cookies.Add(sessionCookie)

        FormsAuthentication.RedirectToLoginPage()

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

    End Sub
End Class