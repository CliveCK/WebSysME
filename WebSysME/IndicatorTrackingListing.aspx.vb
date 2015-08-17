Public Class IndicatorTrackingListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objTracking As New BusinessLogic.Indiactor("Demo", 1)

        With radFileListing

            .DataSource = objTracking.GetTracking
            .DataBind()

        End With

    End Sub

End Class