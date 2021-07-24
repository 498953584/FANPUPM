<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDuty.aspx.cs" Inherits="HR_Personnel_AddDuty" %>

<html>
<head id="Headx" runat="server"><title>管理信息</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language ="javascript">
	function SelectDuty()
	{
	    var p = new Array();
		p[0] = "";
		p[1] = "";
        var url = "";
        url = "PickSinglePerson.aspx?cc=0";
        window.showModalDialog(url,p,"dialogHeight:500px;dialogWidth:550px;center:1;help:0;status:0;");
        //window.open(url,"wi");
        if (p[0]!="")
        {
		    document.getElementById('HdnDeptID').value = p[0];
		    document.getElementById('HdnDutyID').value = p[1];
			document.getElementById('txtDutyName').value = p[2];
		}
	}
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-normal" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                管理信息</td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                岗 &nbsp;&nbsp; 位</td>
			<td><asp:TextBox ID="txtDutyName" CssClass="text-input" style="width:80%" Enabled="false" runat="server"></asp:TextBox>
                <input id="btnSel" type="button" value="选择" onclick="SelectDuty();" />
                <input id="HdnDutyID" type="hidden" style="width:20px" runat="server" />

                <input id="HdnDeptID" type="hidden" style="width:20px" runat="server" />

			</td>
		</tr>
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			<asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="取 消">
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
