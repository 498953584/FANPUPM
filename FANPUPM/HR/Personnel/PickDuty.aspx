<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickDuty.aspx.cs" Inherits="PickDuty" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Microsoft.Web.UI.WebControls"%>


<html xmlns="http://www.w3.org/1999/xhtml" >

<html>
<head runat="server"><title>上级岗位选择</title>
    <base target="_self" />
    <script language="javascript" type="text/javascript">
	<!--
	
	window.name = "win";
	function ClickRow(DutyID,DeptID)
	{
		document.getElementById('hdnDutyID').value = DutyID;
		document.getElementById('hdnDeptID').value = DeptID;
	}
	function confirmDuty()
	{
	    var duty = window.dialogArguments;
		duty[0] = document.getElementById('hdnDeptID').value ;
		duty[1] = document.getElementById('hdnDutyID').value ;
		window.returnvalue= duty;
		window.close();
	}
	 function clicknode(DeptID)
    {   
        document.getElementById("hdnDeptID").value = DeptID;  
        
        document.getElementById("FraDuty").src =  "DutyList.aspx?DeptID=" + DeptID;
        //alert(document.getElementById("FraDuty").src);
    }
	-->
	</script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" target="win" runat="server">
        <table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td vAlign="top" width="30%" height="100%">
			    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <iewc:TreeView ID="TViewDept" Width="190px" CssClass="scr_flat" ExpandLevel="1" Target="FraDuty" runat="server"></iewc:TreeView>
                </div>
			</td>
			<td vAlign="top" width="70%">
            <table width="100%"  height="100%" border="0" id="table2" class="table-normal">
            <tr>
                <TD><iframe id="FraDuty" name="FraDuty" frameborder="0" width="100%" height="100%" runat="server"></iframe></TD>
            </tr>
            <TR>
	            <TD class="td-submit" colspan="8" height="20"><asp:Button ID="BtnOk" Text="确  定" OnClick="BtnOk_Click" runat="server" />&nbsp;
		            <INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
		            <input type="hidden" id="hdnDeptID" name="hdnDeptID" style="WIDTH: 10px" runat="server" />

		            <input type="hidden" id="hdnDutyID" name="hdnDutyID" style="WIDTH: 10px" runat="server" />

	            </TD>
            </TR>
            </table>
			</td>
	    </tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
