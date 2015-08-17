<%@ Control Language="vb" AutoEventWireup="false" Codebehind="FindUsers.ascx.vb"
    Inherits="WebSysME.FindUsers" %>
<%@ Register Src="Userpermissions.ascx" TagName="Userpermissions" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="Panel1" runat="server" GroupingText="Search Users" Width="100%">
<table width="100%">
    <tr>
        <td colspan="4" rowspan="1">
            <asp:Panel ID="pnlFindUsers" runat="server" Height="100px" Width="100%">
                <table width="100%">
                    <tr>
                        <td>
                            Username</td>
                        <td>
                            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                        <td>
                            Surname</td>
                        <td>
                            <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Firstname</td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                        <td>
                            Email Address</td>
                        <td>
                            <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="cmdFind" runat="server" Text="Find User(s)" CssClass="submit" /></td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
    </tr>
    <tr>
            <td class="PageTitle" style="width: 100%">
               <asp:Label ID="lblCurrentUser" runat="server"></asp:Label></td>
        </tr>
    <tr>
        <td colspan="4">
            <telerik:RadGrid ID="rdResults" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                AllowMultiRowSelection="True" CellSpacing="0" GridLines="None" >
                <ExportSettings>
                    <Pdf>
                        <PageHeader>
                            <LeftCell Text="" />
                            <MiddleCell Text="" />
                            <RightCell Text="" />
                        </PageHeader>
                        <PageFooter>
                            <LeftCell Text="" />
                            <MiddleCell Text="" />
                            <RightCell Text="" />
                        </PageFooter>
                    </Pdf>
                </ExportSettings>
                <MasterTableView>
                    <EditFormSettings>
                        <EditColumn CancelImageUrl="~/Images/Cancel.gif" EditImageUrl="~/Images/Edit.gif" InsertImageUrl="~/Images/Insert.gif" UniqueName="EditCommandColumn" UpdateImageUrl="~/Images/Update.gif">
                        </EditColumn>
                    </EditFormSettings>
                    <ExpandCollapseColumn ButtonType="ImageButton" Display="False" UniqueName="ExpandColumn">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn Display="False" UniqueName="RowIndicator">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="UserID" Display="False" HeaderText="UserID" UniqueName="UserID">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandArgument="UserID" CommandName="Select" DataTextField="Username" HeaderText="Username" UniqueName="Username">
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="UserFirstname" HeaderText="Firstname" UniqueName="Firstname">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UserSurname" HeaderText="Surname" UniqueName="Surname">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="EmailAddress" UniqueName="EmailAddress">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandArgument="UserID" CommandName="DeactivateUser" Text="Deactivate" UniqueName="DeactivateUser" FilterControlAltText="Filter DeactivateUser column">
                        </telerik:GridButtonColumn>
                    </Columns>
                    <BatchEditingSettings EditType="Cell" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                </MasterTableView>
                <PagerStyle PageSizeControlType="RadComboBox" />
                <FilterMenu HoverBackColor="LightSteelBlue" HoverBorderColor="Navy" NotSelectedImageUrl="~/Images/NotSelectedMenu.gif"
                    SelectColumnBackColor="Control" SelectedImageUrl="~/Images/SelectedMenu.gif"
                    TextColumnBackColor="Window"></FilterMenu>
            </telerik:RadGrid></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblStatus" runat="server" Height="24px" Width="100%" CssClass="Error"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4" >
        </td>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="4" align="right">
            <asp:Button ID="cmdSavePermissions" runat="server" Text="Save Permissions" Display="False" /></td>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label></td>
        <td colspan="3">
        </td>
    </tr>
</table>
</asp:Panel>
