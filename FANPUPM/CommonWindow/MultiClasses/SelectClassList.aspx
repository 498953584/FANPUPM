<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectClassList.aspx.cs" Inherits="CommonWindow_MultiClasses_SelectClassList" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>类别选择</title><meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
    <script language="javascript" type="text/javascript">
    
    function selClass()
    {
        var classid = document.getElementById("HdnClassID").value;        
	    var classname	= document.getElementById("HdnClassName").value;
		var clist = window.dialogArguments;
		clist[0] = classid;
		clist[1] = classname;
		window.returnvalue= clist;
		window.close();
    }
    function clicknode(classid,classname)
    {
        document.getElementById("HdnClassID").value = classid;
        document.getElementById("HdnClassName").value = classname;  
    }

    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
        &nbsp;&nbsp;&nbsp;
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="1" class="table-normal">
        <tr>               
            <td class="td-title">
                类别列表</td>
        </tr>  
        <tr>
            <td > 
             <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">               
            <asp:TreeView ID="TVClassList" ImageSet="Simple" NodeIndent="10" ShowLines="true" ForeColor="Blue" runat="server"><HoverNodeStyle BackColor="DodgerBlue" BorderColor="#C0FFFF" ForeColor="White" /><SelectedNodeStyle BackColor="#C000C0" ForeColor="#8080FF" /></asp:TreeView>            
            </div>
            </td>
        </tr>  
        <tr>
            <td class="td-submit">
               <input id="BtnSave" name="BtnSave" type="button" value="确  定" onclick ="selClass();" />&nbsp;
                <asp:HiddenField ID="HdnClassID" runat="server" />
                        <asp:HiddenField ID="HdnClassName" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
