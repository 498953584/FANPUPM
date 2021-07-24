<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoFlowStartWf.aspx.cs" Inherits="oa_UserDefineFlow_MoFlowStartWf" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>自定义流程起动</title>
    <script language ="javascript">
	<!--
	window.name = "win"
    function pickPerson()
    {
        var p = new Array();
		p[0] = "";
		p[1] = "";
		var url = "";
		url = "../../CommonWindow/PickSinglePerson.aspx";
		window.showModalDialog(url,p,"dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
		if (p[0]!=""){
			document.getElementById('hdnReceiver').value = p[0];
			document.getElementById('txtReceiver').value = p[1];
			return true;
		}
		else
		{
		    return false;
		}
		//alert(document.getElementById('hdnReceiver').value);
	
		
    }
    function selectTemplate()
    {
        document.getElementById('hdnTemplateID').value = document.getElementById("ddltTemplate").value; 
    }
    -->
	</script>
</head>
<body>
    <form id="form1" target="win" runat="server">
    <div>
      <table class="table-form" id="TableVersion" cellspacing="0" cellpadding="0" width="100%" border="1">
            <tr><td class="td-head" colspan="4" height="20">流程启动选择</td></tr>
            <tr>
                <td class="td-label" width="25%">流程模板</td>
                <td>
                    <asp:Label ID="LbTemName" runat="server"></asp:Label>
                    <input id="hdnNodeId" style="WIDTH: 10px" type="hidden" name="hdnNodeId" runat="server" />

                <input id="hdnOrderNumber" style="WIDTH: 10px" type="hidden" name="hdnOrderNumber" runat="server" />

                    </td>
             </tr>
                      
            <tr><td class="td-label" width="25%">选择接收人</td>
                <td>
                    <asp:TextBox ID="txtReceiver" CssClass="td-input" Width="161px" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBPick" ImageUrl="/images/contact1.gif" OnClick="IBPick_Click" runat="server" />
                    <input id="hdnReceiver" style="WIDTH: 10px" type="hidden" name="hdnReceiver" runat="server" />

                 </td>
            </tr>
            
            <tr>
                <td class="td-label">选择接收人</td>
                <td>
                    <asp:DropDownList ID="DDLSuperordinateDuty" Width="140px" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
				<td class="td-submit" colspan="4" height="20">
				    <asp:Button ID="BtnAdd" Text="确  认" OnClick="BtnAdd_Click" runat="server" />&nbsp;
				    <input id="BtnClose" onclick="window.close();returnValue=true;" type="button" value="关  闭" name="BtnClose"/>
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
