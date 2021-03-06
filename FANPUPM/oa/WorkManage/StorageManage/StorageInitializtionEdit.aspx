<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInitializtionEdit.aspx.cs" Inherits="oa_WorkManage_StorageManage_StorageInitializtionEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>物质清单信息</title>
	<script language ="javascript">
	<!--
	    window.name = "win";
	    function checkDecimal(sourObj)
	    {
		    if (sourObj.value=="")
		    {
		    	sourObj.value = 0;
		    }
		    if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
		    {
		    	alert('数据类型不正确！');
		    	sourObj.focus();
		    	return;
		    }
	    }
	    function IsInteger(sourObj)
	    {
		    if (sourObj.value == "")
		    {
		    	sourObj.value = 0;
		    }
		    else
		    {
		       if (!(new RegExp(/^\d+$/).test(sourObj.value)))
		    	{
		    	    alert('数据类型不正确！');
		    	    sourObj.focus();
		    	    return;
			     }
		    }
	    }
	    function PickMatterial()
	    {
	        var Matterial = new Array();
	        Matterial[0] = "";
	        Matterial[1] = "";
	        Matterial[2] = "";
	        Matterial[3] = "";
	        Matterial[4] = "";
		    var url= "MattrialSelect.aspx";
		    var ref = window.showModalDialog(url,Matterial,"dialogHeight:360px;dialogWidth:620px;center:1;help:0;status:0;");
		    if(Matterial[0] != "")
		    {
		        document.getElementById('txtResName').value = Matterial[0];
		        var ddlUseType = document.getElementById('DDLUseType');
		        ddlUseType.selectedIndex = Matterial[1];
		        var ddlGetType = document.getElementById('DDLGetType');
		        ddlGetType.selectedIndex = Matterial[2];
		        document.getElementById('txtUnit').value = Matterial[3];
		        document.getElementById('txtPlanFee').value = Matterial[4];
		        document.getElementById('HdnMatterialID').value = Matterial[5];
		    }
	    }
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" target="win" runat="server">
        <table class="table-normal" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                物质清单信息</td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                名 &nbsp;&nbsp; 称</td>
			<td>
			    <asp:TextBox ID="txtResName" CssClass="text-input" style="width:70%" Enabled="false" runat="server"></asp:TextBox>
			    <img src="../../../StyleCss/add.gif" id="imgsel" style="cursor:hand" onclick="PickMatterial();" runat="server" />

			    <input id="HdnMatterialID" value="0" style="width: 20px" type="hidden" runat="server" />

			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                分 &nbsp;&nbsp; 类</td>
            <td>
                <asp:DropDownList ID="DDLUseType" Enabled="false" runat="server"><asp:ListItem Value="0" Text="不回收" /><asp:ListItem Value="1" Text="以旧换新" /></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                领用类别</td>
			<td>
                <asp:DropDownList ID="DDLGetType" Enabled="false" runat="server"><asp:ListItem Value="0" Text="个人领用" /><asp:ListItem Value="1" Text="部门公共" /></asp:DropDownList></td>
		</tr>   
		<tr>
			<td class="td-label" width="20%">
                单 &nbsp;&nbsp; 位</td>
            <td>
                <asp:TextBox ID="txtUnit" CssClass="text-input" Enabled="false" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                计划成本</td>
            <td>
                <asp:TextBox ID="txtPlanFee" CssClass="text-input" Enabled="false" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                数 &nbsp;&nbsp; 量</td>
            <td>
                <asp:TextBox ID="txtNumber" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="td-submit" colSpan="2" height="20">
			    <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			    <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
			    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
