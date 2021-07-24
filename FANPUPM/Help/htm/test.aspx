<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Help_htm_test" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title></head>
<body  >
    <form id="form1" runat="server">
    <div>
		<asp:TreeView ID="TreeView1" Height="100%" Width="200px" ExpandDepth="2" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" runat="server"><Nodes /><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView>
        &nbsp;&nbsp;

    </div>
    </form>
    <script language =javascript >
//        function ss()
//        {}
  //        __doPostBack("tvProject", "sb\a1");
	    
    </script>
</body >
</html>
