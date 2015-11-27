Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            lblUser.Text = CookiesWrapper.thisUserFullName
            lblDayDesc.Text = Now.ToString("dddd")
            lblDay.Text = Now.ToString("dd")
            lblMonth.Text = Now.ToString("MMMM")
            lblYear.Text = Now.ToString("yyyy")

            LoadCompanyLogo()

        End If

    End Sub

    Private Sub LoadCompanyLogo()

        imgCompanyLogo.ImageUrl = "~/Settings/" & CookiesWrapper.thisConnectionName & "/Images/CompanyLogo.jpg"

    End Sub

End Class