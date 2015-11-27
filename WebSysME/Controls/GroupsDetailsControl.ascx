<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="GroupsDetailsControl.ascx.vb" Inherits="WebSysME.GroupsDetailsControl" %>
<table cellpadding="4" cellspacing="0" border="0" style="width:100%;margin-left:2%"> 
	<tr> 
		<td colspan="4" class="PageTitle"><h4>Group Details</h4><br /></td> 
	</tr> 
    <tr> 
		<td >Province</td> 
        	<td ><asp:dropdownlist id="cboProvince" runat="server" CssClass="form-control" AutoPostBack="true"></asp:dropdownlist> </td> 
	</tr> <tr> 
		<td >District</td> 
        	<td ><asp:dropdownlist id="cboDistrict" runat="server" CssClass="form-control" AutoPostBack="true"></asp:dropdownlist> </td> 
	</tr> 
	<tr> 
		<td >Ward</td> 
        	<td ><asp:dropdownlist id="cboWard" runat="server" CssClass="form-control"></asp:dropdownlist> </td> 
	</tr> 
	<tr> 
		<td >Group Type</td> 
        	<td ><asp:dropdownlist id="cboGroupType" runat="server" CssClass="form-control"></asp:dropdownlist> </td> 
		<td >Description</td> 
        	<td ><asp:textbox id="txtDescription" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td >Group Size</td> 
        	<td ><asp:textbox id="txtGroupSize" runat="server" CssClass="form-control"></asp:textbox> </td> 
		<td >Group Name</td> 
        	<td ><asp:textbox id="txtGroupName" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
            		<asp:Panel id="pnlError" width="95%" runat="server" EnableViewState="False"><asp:label id="lblError" Width="100%" runat="server" CssClass="Error" EnableViewState="False"></asp:label></asp:Panel> 
     </td> 
	</tr> 
	<tr> 
		<td colspan="2"> 
            		<asp:button id="cmdSave" runat="server" Text="Save" CssClass="btn btn-default"></asp:button> 
     </td>
        <td>
                    <asp:button id="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default"></asp:button>
        </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
			<asp:TextBox  id="txtGroupID" runat="server" Visible="false"></asp:TextBox> 
		</td> 
	</tr>
    <tr>
        <td><br />Group Membership</td>
    </tr> 
    <tr>
        <td colspan="4">
            <telerik:RadGrid ID="radGroupMembership" runat="server" GridLines="None" Height="100%" AllowMultiRowSelection="True"
                      CellPadding="0" Width="90%">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    <MasterTableView AllowFilteringByColumn="True" AllowMultiColumnSorting="True" AllowPaging="True"
                    AllowSorting="True" CommandItemDisplay="Top" PagerStyle-Mode="NextPrevNumericAndAdvanced">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn DataField="BeneficiaryID" UniqueName="BeneficiaryID" HeaderText="BeneficiaryID"
                                Display="false">                            
                            </telerik:GridBoundColumn>
                        </Columns>
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>                   
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
        </td>
    </tr>
</table> 
