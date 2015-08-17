Imports Telerik.Web.UI

Public Class Scheduler
    Inherits System.Web.UI.UserControl

    Private mUser As List(Of Integer)
    Private mActivity As List(Of Integer)
    Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString
    Protected ReadOnly Property Activity() As List(Of Integer)
        Get
            mActivity = New List(Of Integer)

            For Each Actitem As ListItem In lstActivity.Items

                If lstActivity.SelectedIndex > -1 Then

                    If Actitem.Selected Then

                        mActivity.Add(Actitem.Value)

                    End If

                End If

            Next

            Return mActivity

        End Get
    End Property

    Protected ReadOnly Property User() As List(Of Integer)
        Get
            mUser = New List(Of Integer)

            For Each item As ListItem In lstUsers.Items

                If lstUsers.SelectedIndex > -1 Then

                    If item.Selected Then

                        mUser.Add(item.Value)

                    End If

                End If

            Next

            Return mUser
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If Not IsNothing(Request.QueryString("staffid")) Then

                Dim objLookup As New BusinessLogic.CommonFunctions

                With lstUsers

                    .DataSource = objLookup.Lookup("tblStaffMembers", "StaffID", "FirstName", , "StaffID = " & objUrlEncoder.Decrypt(Request.QueryString("staffid")))
                    .DataTextField = "FirstName"
                    .DataValueField = "StaffID"
                    .DataBind()

                    .SelectedIndex = 0

                End With

                With lstActivity
                    .Width = 8000
                    .DataSource = objLookup.Lookup("tblActivities", "ActivityID", "Description")
                    .DataTextField = "Description"
                    .DataValueField = "ActivityID"
                    .DataBind()

                End With


                RadScheduler1.Rebind()

            Else

                Dim objLookup As New BusinessLogic.CommonFunctions

                With lstUsers

                    .DataSource = objLookup.Lookup("tblStaffMembers", "StaffID", "FirstName")
                    .DataTextField = "FirstName"
                    .DataValueField = "StaffID"
                    .DataBind()

                End With

                With lstActivity

                    .DataSource = objLookup.Lookup("tblActivities", "ActivityID", "Description")
                    .DataTextField = "Description"
                    .DataValueField = "ActivityID"
                    .DataBind()

                End With

            End If

        End If

    End Sub

    Private Sub SqlDataSource1_Inserting(sender As Object, e As SqlDataSourceCommandEventArgs) Handles SqlDataSource1.Inserting
        Dim subject = e.Command.Parameters(0).Value

        If subject Is Nothing Then
            e.Cancel = True
        End If
    End Sub

    Protected Sub CompletedStatusCheckBox_CheckedChanged(sender As Object, e As EventArgs)
        Dim CompletedStatusCheckBox As CheckBox = DirectCast(sender, CheckBox)
        'Find the appointment object to directly interact with it
        Dim appContainer As SchedulerAppointmentContainer = DirectCast(CompletedStatusCheckBox.Parent, SchedulerAppointmentContainer)
        Dim appointment As Appointment = appContainer.Appointment
        Dim appointmentToUpdate As Appointment = RadScheduler1.PrepareToEdit(appointment, RadScheduler1.EditingRecurringSeries)
        appointmentToUpdate.Attributes("Completed") = CompletedStatusCheckBox.Checked.ToString()
        RadScheduler1.UpdateAppointment(appointmentToUpdate)
        RadScheduler1.Rebind()
    End Sub

    Private Sub FilterAppointment(appointment As Appointment, UserList As List(Of Integer), ActivityList As List(Of Integer))

        If lstUsers.SelectedIndex > -1 And lstActivity.SelectedIndex < 0 Then

            If UserList.Contains(appointment.Attributes("UserID")) Then
                appointment.Visible = True
            End If

        ElseIf lstUsers.SelectedIndex < 0 And lstActivity.SelectedIndex > -1 Then

            If ActivityList.Contains(appointment.Attributes("ActivityID")) Then
                appointment.Visible = True
            End If

        ElseIf lstUsers.SelectedIndex > -1 And lstActivity.SelectedIndex > -1 Then

            If UserList.Contains(appointment.Attributes("UserID")) And ActivityList.Contains(appointment.Attributes("ActivityID")) Then
                appointment.Visible = True
            End If

        End If

    End Sub

    Protected Sub RadScheduler1_AppointmentCreated(sender As Object, e As AppointmentCreatedEventArgs)
        If e.Appointment.RecurrenceState = RecurrenceState.Master OrElse e.Appointment.RecurrenceState = RecurrenceState.Occurrence Then
            Dim recurrenceStatePanel As Panel = TryCast(e.Container.FindControl("RecurrencePanel"), Panel)
            recurrenceStatePanel.Visible = True
        End If
        If e.Appointment.RecurrenceState = RecurrenceState.Exception Then
            Dim recurrenceExceptionPanel As Panel = TryCast(e.Container.FindControl("RecurrenceExceptionPanel"), Panel)
            recurrenceExceptionPanel.Visible = True
        End If
        If e.Appointment.Reminders.Count <> 0 Then
            Dim reminderPanel As Panel = TryCast(e.Container.FindControl("ReminderPanel"), Panel)
            reminderPanel.Visible = True
        End If
    End Sub

    Protected Sub RadScheduler1_AppointmentDataBound(sender As Object, e As SchedulerEventArgs)
        If e.Appointment.Attributes("Completed") = "True" Then
            e.Appointment.BackColor = System.Drawing.Color.CornflowerBlue
        End If
    End Sub

    Private Sub lstUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUsers.SelectedIndexChanged
        RadScheduler1.Rebind()
    End Sub

    Private Sub RadScheduler1_AppointmentDataBound1(sender As Object, e As SchedulerEventArgs) Handles RadScheduler1.AppointmentDataBound

        If lstActivity.SelectedIndex > -1 Or lstUsers.SelectedIndex > -1 Then

            e.Appointment.Visible = False

            FilterAppointment(e.Appointment, User, Activity)

        End If

    End Sub

    Private Sub lstActivity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstActivity.SelectedIndexChanged
        RadScheduler1.Rebind()
    End Sub

End Class