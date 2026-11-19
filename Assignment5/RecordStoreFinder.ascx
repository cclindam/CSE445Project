<%@ Control Language="C#" 
    AutoEventWireup="true"
    CodeBehind="WebUserControl1.ascx.cs"
    Inherits="Assignment5.WebUserControl1" %>

<asp:Label ID="txtZipcode" runat="server" Text="Zipcode: " />
<asp:TextBox ID="IbIZipcode" runat="server" />
<asp:Label ID="txtStorename" runat="server" Text="Record Store: "/>
<asp:TextBox ID="IbIStorename" runat="server" />
<asp:Button ID="btnFindStore" runat="server" Text="Find Nearest Store" OnClick="btnFindStore_Click"/>

<asp:Label ID="txtResult" runat="server"
    Text="Nearest Store"/>
