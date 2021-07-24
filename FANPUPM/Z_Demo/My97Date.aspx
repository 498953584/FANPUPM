<%@ Page Language="C#" AutoEventWireup="true" CodeFile="My97Date.aspx.cs" Inherits="Z_Demo_My97Date" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../Script/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtAllocationDateEnd" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
        <input type="text" id="bbbbbbbbb" onclick="WdatePicker()" />
    </div>
    </form>
</body>
</html>
