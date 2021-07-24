<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptset.aspx.cs" Inherits="DeptSet" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>DeptSet</title>
    <script type="text/javascript">
        // 打开管理模块模态窗口
        function showDept(deptCode) {
            var url = "broker.aspx?go=1&bmdm=" + deptCode;
            window.showModalDialog(url, window, "dialogHeight:500px;dialogWidth:600px;center:1;help:0;status:0;");
        }
        function mouseOver(obj) {
            obj.style.backgroundColor = 'e9e9e9';
            obj.style.color = '000000';
        }
        function mouseOut(obj) {
            obj.style.backgroundColor = 'f6f6ff';
            obj.style.color = '333333';
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <br />
    </font>
    </form>
</body>
</html>
