<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="NewsSite.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Lewis' News Site </title>
    <link rel="stylesheet" type="text/css" href="style.css" />

</head>
<body>
    <form id="form1" method="post" runat="server" EncType="multipart/form-data" action="index.aspx">
    <div id="banner">
        <img src="images/banner.jpg" />
    </div>
    <table align="center" cellspacing="25px">
        <td>
            Image: <input id="oFile" type="file" runat="server" name="oFile" />
        </td>
        <td>
            URL: <asp:TextBox ID="txtUrl" runat="server" ></asp:TextBox>  
        </td>
        <td>
            Name: <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>  
        </td>
        <tr>
        <td>
            <asp:Button runat="server" id="btnAdd" Text="Add Page" OnClick="btnAdd_Click" />
            <asp:Label runat="server" ID="lblUploadResult" />
        </td>
        </tr>
    </table>
    <div id="newstable">
        <% Response.Write(renderTable()); %>
    </div>
    </form>
</body>
</html>
