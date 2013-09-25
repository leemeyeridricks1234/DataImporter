<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportFromDatabase.aspx.cs" Inherits="ImportFromDatabase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import from database</title>
    <link href="styles/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <div id="header"><h1>Export IDNumber from Production</h1></div>
            <div>
                <table>
                    <tr>
                        <td>Please enter IDNumber:</td>
                        <td><asp:TextBox runat="server" ID="txtIdNumber"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button runat="server" ID="btnSUbmit" OnClick="Submit" Text="Export Query"/></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ClientIDMode="Static" runat="server" ID="lblQueries" Height="100%" Width="100%"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="footer"></div>
        </div>
    </form>
</body>
</html>
