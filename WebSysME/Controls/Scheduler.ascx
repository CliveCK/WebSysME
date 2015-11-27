<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Scheduler.ascx.vb" Inherits="WebSysME.Scheduler" %>

<div class="demo-container no-bg" style="margin-left:2%">
<table>
    <tr>
        <td colspan="2">
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadScheduler1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="lstUsers">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="lstActivity">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                </telerik:RadAjaxLoadingPanel>
        </td>
    </tr>
    <tr>
        <td>
            Users:<br />
            <asp:ListBox ID="lstUsers" runat="server" SelectionMode="Multiple" AutoPostBack="true"  Width="200px" CssClass="form-control"></asp:ListBox><br /><br />
        </td>
        <td>
            Activity:<br />
            <asp:ListBox ID="lstActivity" runat="server" SelectionMode="Multiple" AutoPostBack="true"  Width="200px" CssClass="form-control"></asp:ListBox><br /><br />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <telerik:RadScheduler id="RadScheduler1" runat="server" CustomAttributeNames="Completed,UserID,ActivityID"
			DataDescriptionField="Description" DataEndField="End" DataKeyField="ID" SelectedView="WeekView" 
			DataRecurrenceField="RecurrenceRule" OnAppointmentDataBound="RadScheduler1_AppointmentDataBound"
            OnAppointmentCreated="RadScheduler1_AppointmentCreated" 
			DataRecurrenceParentKeyField="RecurrenceParentID" DataReminderField="Reminder" 
			DataSourceID="SqlDataSource1" DataStartField="Start" DataSubjectField="Subject" 
			EnableDescriptionField="True" Height="" >
			<Reminders Enabled="True" />
                 <AdvancedForm Modal="true"></AdvancedForm>
                <ResourceTypes>
                    <telerik:ResourceType KeyField="StaffID" Name="User" TextField="StaffFullName" ForeignKeyField="UserID"
                        DataSourceID="UsersDataSource"></telerik:ResourceType>
                    <telerik:ResourceType KeyField="ActivityID" Name="Activity" TextField="Description" ForeignKeyField="ActivityID"
                        DataSourceID="ActivityDataSource"></telerik:ResourceType>
                </ResourceTypes>
            <AppointmentTemplate>
                <div class="appointmentHeader">
                    <asp:Panel ID="RecurrencePanel" CssClass="rsAptRecurrence" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="RecurrenceExceptionPanel" CssClass="rsAptRecurrenceException" runat="server"
                        Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="ReminderPanel" CssClass="rsAptReminder" runat="server" Visible="false">
                    </asp:Panel>
                    <%# Eval("Subject")%>
                </div>
                <div>
                     Activity: <strong>
                        <asp:Label ID="ActivityLabel" runat="server" Text='<%# If(Container.Appointment.Resources.GetResourceByType("Activity") Is Nothing, "None", Container.Appointment.Resources.GetResourceByType("Activity").Text)%>'></asp:Label>
                    </strong>
                </div>
                <div>
                    Assigned to: <strong>
                        <asp:Label ID="UserLabel" runat="server" Text='<%# If (Container.Appointment.Resources.GetResourceByType("User") is Nothing, "None" , Container.Appointment.Resources.GetResourceByType("User").Text) %>'></asp:Label>
                    </strong>
                    <br />
                    <asp:CheckBox ID="CompletedStatusCheckBox" runat="server" Text="Completed? " TextAlign="Left"
                        Checked='<%#  If (String.IsNullOrEmpty(Container.Appointment.Attributes("Completed")), False, Boolean.Parse(Container.Appointment.Attributes("Completed"))) %>'
                        AutoPostBack="true" OnCheckedChanged="CompletedStatusCheckBox_CheckedChanged"></asp:CheckBox>
                </div>
            </AppointmentTemplate>
              <ResourceStyles>
                <telerik:ResourceStyleMapping Type="User" Key="1" BackColor="YellowGreen"></telerik:ResourceStyleMapping>
                <telerik:ResourceStyleMapping Type="User" Key="2" BackColor="Pink"></telerik:ResourceStyleMapping>
                <telerik:ResourceStyleMapping Type="User" Key="3" BackColor="Azure"></telerik:ResourceStyleMapping>
            </ResourceStyles>
		</telerik:RadScheduler>
    	<asp:SqlDataSource ID="SqlDataSource1" runat="server"
			DeleteCommand="DELETE FROM [Appointments] WHERE [ID] = @ID" 
			InsertCommand="INSERT INTO [Appointments] ([Subject], [Start], [End], [UserID], [RoomID], [RecurrenceRule], [ActivityID], [RecurrenceParentID], [Annotations], [Description], [Reminder], [LastModified]) VALUES (@Subject, @Start, @End, @UserID, @RoomID, @RecurrenceRule, @ActivityID, @RecurrenceParentID, @Annotations, @Description, @Reminder, @LastModified)" 
			SelectCommand="SELECT * FROM [Appointments]" 
			UpdateCommand="UPDATE [Appointments] SET [Subject] = @Subject, [Start] = @Start, [Completed] = @Completed, [End] = @End, [UserID] = @UserID, [RoomID] = @RoomID, [RecurrenceRule] = @RecurrenceRule, [RecurrenceParentID] = @RecurrenceParentID, [Annotations] = @Annotations, [ActivityID] = @ActivityID, [Description] = @Description, [Reminder] = @Reminder, [LastModified] = @LastModified WHERE [ID] = @ID">
			<DeleteParameters>
				<asp:Parameter Name="ID" Type="Int32" />
			</DeleteParameters>
			<InsertParameters>
				<asp:Parameter Name="Subject" Type="String" />
				<asp:Parameter Name="Start" Type="DateTime" />
				<asp:Parameter Name="End" Type="DateTime" />
				<asp:Parameter Name="UserID" Type="Int32" />
				<asp:Parameter Name="RoomID" Type="Int32" />
				<asp:Parameter Name="RecurrenceRule" Type="String" />
				<asp:Parameter Name="RecurrenceParentID" Type="Int32" />
				<asp:Parameter Name="Annotations" Type="String" />
				<asp:Parameter Name="Description" Type="String" />
				<asp:Parameter Name="Reminder" Type="String" />
				<asp:Parameter Name="LastModified" Type="String" />
                <asp:Parameter Name="ActivityID" Type="Int32" />
			</InsertParameters>
			<UpdateParameters>
				<asp:Parameter Name="Subject" Type="String" />
				<asp:Parameter Name="Start" Type="DateTime" />
				<asp:Parameter Name="End" Type="DateTime" />
				<asp:Parameter Name="UserID" Type="Int32" />
				<asp:Parameter Name="RoomID" Type="Int32" />
				<asp:Parameter Name="RecurrenceRule" Type="String" />
				<asp:Parameter Name="RecurrenceParentID" Type="Int32" />
				<asp:Parameter Name="Annotations" Type="String" />
				<asp:Parameter Name="Description" Type="String" />
				<asp:Parameter Name="Reminder" Type="String" />
				<asp:Parameter Name="LastModified" Type="String" />
				<asp:Parameter Name="ID" Type="Int32" />
                <asp:Parameter Name="ActivityID" Type="Int32" />
                <asp:Parameter Name="Completed" Type="String" />
			</UpdateParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="UsersDataSource" runat="server"
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT * FROM [tblStaffMembers]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ActivityDataSource" runat="server"
                ProviderName="System.Data.SqlClient"
                SelectCommand="SELECT * FROM [tblActivities]">
        </asp:SqlDataSource>
        </td>
    </tr>
</table>
<//div> 
