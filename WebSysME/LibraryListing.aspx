<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LibraryListing.aspx.vb" Inherits="WebSysME.LibraryListing" MasterPageFile="~/Site.Master"%>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div style="margin-left:2%">
    <h4>Library</h4>
        <asp:Button ID="cmdNew" runat="server" CssClass="btn btn-default" Text="AddNew" />
    <br />
    <telerik:RadGrid ID="radFileListing" runat="server" GridLines="None" Height="80%" 
                    CellPadding="0" Width="100%">
                    <MasterTableView AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" 
                       AllowMultiColumnSorting="true" AllowSorting="true" PagerStyle-Mode="NextPrevNumericAndAdvanced">
                        <Columns>
                            <telerik:GridBoundColumn DataField="FileID" UniqueName="FileID" HeaderText="FileID"
                                Display="false">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="FilePath" UniqueName="FilePath" HeaderText="FilePath" Display="false" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Title" UniqueName="Title" HeaderText="Title">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FileType" UniqueName="FileType" HeaderText="FileType">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Author" UniqueName="Author" HeaderText="Author">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AuthorOrganization" UniqueName="AuthorOrganization" HeaderText="AuthorOrganization">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Date" UniqueName="DateUploaded" HeaderText="DateUploaded">
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
                        <PagerStyle Position="Top" AlwaysVisible="true"/>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid><br />
    </div>
</asp:Content>


