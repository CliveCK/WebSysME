Imports Telerik.Web.UI

Public Class HouseholdList
    Inherits System.Web.UI.Page

    Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            LoadGrid()

        End If

    End Sub
    Private Sub LoadGrid()

        Dim objBeneficiaries As New BusinessLogic.Beneficiary(CookiesWrapper.thisConnectionName, CookiesWrapper.thisUserID)

        With radBenListing

            .DataSource = objBeneficiaries.GetAllBeneficiaries().Tables(0)
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

            CookiesWrapper.BeneficiaryID = BeneficiaryID

            Response.Redirect("~/Beneficiary.aspx?id=" & objUrlEncoder.Encrypt(BeneficiaryID))

        End If

    End Sub

    Private Sub radBenListing_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles radBenListing.NeedDataSource

        radBenListing.DataSource = DirectCast(ViewState("Beneficiaries"), DataTable)

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        CookiesWrapper.BeneficiaryID = 0
        Response.Redirect("~/Beneficiary.aspx")

    End Sub
End Class