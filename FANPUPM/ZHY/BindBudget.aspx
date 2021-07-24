<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BindBudget.aspx.cs" Inherits="ZHY_BindBudget" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title></head>
<body style="margin:0;height:100%; width:100%;">
    <form id="form1" runat="server">
    <div>
    <iframe id="BindTop" frameborder="0" height="300px" scrolling="auto" width="100%" src="costframeanalyze.aspx?type=see&PrjGuid=<%=prjguid %>" runat="server"></iframe>
    <iframe id="BindButtom" frameborder="0" width="100%" scrolling="auto" src="gonggongbufen.aspx" runat="server"></iframe>
    </div>
    </form>
</body>
</html>
