<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatMain.aspx.cs" Inherits="HR_Leave_StatMain" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>请休假统计</title>
    <script language=javascript>
    function selrow()
	{
	//hxp
	 try
	 {	 window.document.getElementById('TVCorpCodet0').click(); }
	 catch(e){}
	}
    </script>
</head>
<body class="body_clear" scroll="no" onload ="selrow()">
    <form id="form1" runat="server">
    <div> 
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%"  border="1" class="table-normal" height="100%" >
            <tr>               
                <td style="width:20%" valign="top" >
                    <div style="width:233;height:100%;overflow:auto"> 
                        <asp:TreeView ID="TVCorpCode" ShowLines="true" Width="100%" Target="frmStat" SelectedStyle="color:red;background-color:#ffffff;" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>
                    </div>
                 </td>  
                <td>
                    <iframe id="frmStat" name="frmStat" src="StatCountList.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                 </td>
            </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
