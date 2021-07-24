<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projects.aspx.cs" Inherits="demo_Projects" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目列表</title></head>
<body>
    <form id="form1" runat="server">
    <a href="ImportProject.aspx">导入项目</a>  <a href="NewProject.aspx">新建项目</a><br /><br />
    <div >
    <%=GetProjectListHTML() %>
    </div>
    </form>
</body>
</html>
