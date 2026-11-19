<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment5.Default" %>
<%@ Register TagPrefix ="RS" TagName="RecordStoreFinder" src="RecordStoreFinder.ascx"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Temp</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Album ranker</h1>
        <p> This application is used to rate the music that you enjoy, discover new music, keep track of your favorite albums, and find record stores where you can purchase new music
        </p>
        <h3>Test with Arizona record stores</h3>
        <RS:RecordStoreFinder
            ID="RecordStoreFinder1"
            runat="server" />
    </form>
    <table border="1">
    <tr>
        <th>Provider</th>
        <th>Page / Component Type</th>
        <th>Component Description</th>
        <th>Resources / Methods &amp; Where Used</th>
    </tr>

    <tr>
        <td>Cole Lindaman</td>
        <td>ASPX - Default.aspx</td>
        <td>
            Public page containing application information,
            component directory, and component test interfaces.
        </td>
        <td>
            ASP.NET Web Forms and C# code-behind.
        </td>
    </tr>

    <tr>
        <td>Cole Lindaman</td>
        <td>SVC - Record Store Web Service</td>
        <td>
            findNearestRecordStore(zipcode, storeName)<br />
            Input: zipcode (string), storeName (string)<br />
            Output: result message (string)<br />
            Finds the requested record store nearest to the
            supplied ZIP code.
        </td>
        <td>
            WCF Web Service written in C#.
            Uses the Geoapify API to obtain location information.
        </td>
    </tr>

    <tr>
        <td>Cole Lindaman</td>
        <td>User Control - RecordStoreFinder.ascx</td>
        <td>
            Provides an interface for entering a ZIP code and
            record store and displays the nearest store.
        </td>
        <td>
            ASP.NET User Control with an UpdatePanel.
            Used on Default.aspx.
        </td>
    </tr>
    <tr>
        <td>Cole Lindaman</td>
        <td>Cookie session</td>
        <td>
            Saves last used zipcode for finding a record store
        </td>
        <td>
            ASP.NET
            Used on Default.aspx.
        </td>
    </tr>
    <tr>
        <td>Michael Lau</td>
        <td>SVC - Album Search Web Service</td>
        <td>Finds album based on keyword. </td>
        <td>TBD</td>
    </tr>
    <tr>
        <td>Michael Lau</td>
        <td>DLL Class Library</td>
        <td>TBD</td>
        <td>TBD</td>
    </tr>
    <tr>
        <td>Peter Liang</td>
        <td>SVC - Recommendation Service</td>
        <td>TBD</td>
        <td>TBD</td>
    </tr>
    <tr>
        <td>Peter Liang</td>
        <td>Gloabal.astax</td>
        <td>Application Event handler</td>
        <td>TBD</td>
    </tr>
     <tr>
        <td>Peter Liang</td>
        <td>Cookie</td>
        <td>Stores information TBD</td>
        <td>TBD</td>
    </tr>
</table>
</body>
</html>
