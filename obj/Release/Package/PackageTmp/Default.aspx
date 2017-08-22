<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RoomReserve._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 626px" class="dropdown">
            <div ID="calDiv">
            <asp:Calendar ID="Calendar1" runat="server" SelectedDate="04/23/2016 19:53:27" VisibleDate="2016-04-23" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
        </div>
        <div ID="tableDiv">
            <asp:Table ID="Table1" runat="server" Height="358px" Width="721px">
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
            </asp:Table>
            <div Id="legDiv">
                <h1 style="color: black; font-size: 24px; font-weight: bold; position: relative; top: 9px; left: 16px;">Free</h1>
                <asp:Button ID="Button2" runat="server" Height="45px" Width="45px" />
                <h2 style="color: black; font-size: 24px; font-weight: bold; position: relative; top: 6px; left: 4px;">Booked</h2>
                <asp:Button ID="Button3" runat="server" Height="45px" Width="45px" />
            </div>
        </div>
        <div ID="header">
            <h1>Office Bookings</h1>
        </div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Book!" />
    </div>

</asp:Content>
