﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HealthCenterListing.aspx.vb" Inherits="WebSysME.HealthCenterListing"  Title="Health Centers" MasterPageFile ="~/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="margin-left:2%">
        <tr>
            <td>
                <h4>Health Centers</h4><br />
            </td> 
        </tr>
        <tr> 
		<td >
            		<asp:Panel id="pnlError" width="95%" runat="server" EnableViewState="False"><asp:label id="lblError" Width="100%" runat="server" CssClass="Error" EnableViewState="False"></asp:label></asp:Panel> <br />
     </td> 
	</tr> 
        <tr>
            <td>
                <asp:Button runat="server" ID="cmdNew" Text="Add New" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="radHealthCenterListing" runat="server" GridLines="None" Height="100%" 
                    CellPadding="0" Width="100%">
                    <MasterTableView AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" 
                       AllowMultiColumnSorting="true" AllowSorting="true" PagerStyle-Mode="NextPrevNumericAndAdvanced">
                        <PagerStyle AlwaysVisible="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="HealthCenterID" UniqueName="HealthCenterID" HeaderText="HealthCenterID"
                                Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ButtonType="LinkButton" Text="Edit Details" UniqueName="column"
                                CommandName="View">
                            </telerik:GridButtonColumn>
                             <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="Name" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NoOfDoctors" UniqueName="NoOfDoctors" HeaderText="NoOfDoctors">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NoOfNurses" UniqueName="NoOfNurses" HeaderText="NoOfNurses">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NoOfBeds" UniqueName="NoOfBeds" HeaderText="NoOfBeds">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="HasAmbulance" HeaderText="HasAmbulance">
                                 <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHasAmbulance" Text='<%# IIf(Not IsNothing(Eval("HasAmbulance")) AndAlso Convert.ToString(Eval("HasAmbulance")) = "1", "Yes", "No")%>'></asp:Label>
                             </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" AlternateText="Cancel" CausesValidation="False"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "HealthCenterID")%>'
                                    CommandName="Delete" ImageUrl="~/images/Delete.png" OnClientClick="javascript:return confirm('Are you sure you want to delete?')"
                                    ToolTip="Click to delete " />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
                        <PagerStyle Position="TopAndBottom" />
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true">
                    </ClientSettings>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>