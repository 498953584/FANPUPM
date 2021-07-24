<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentDrawApplyAssTabEdit.aspx.cs" Inherits="oa_WorkManage_StorageManage_DepartmentDrawApplyAssTabEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>部门领用申请登记 </title>
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
		    var url= "MattrialSelect.aspx?type=1";
		    var ref = window.showModalDialog(url,Matterial,"dialogHeight:360px;dialogWidth:620px;center:1;help:0;status:0;");
		    if(Matterial[0] != "")
		    {
		        document.getElementById('txtResName').value = Matterial[0];
		        var ddlUseType = document.getElementById('DDLUseType');
		        ddlUseType.selectedIndex = Matterial[1];
		        document.getElementById('txtUnit').value = Matterial[3];
		        document.getElementById('txtInStoragePrice').value = Matterial[4];
		        document.getElementById('HdnPrice').value = Matterial[4];
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
			<td class="td-head" colSpan="4" height="20">
                <div align="center" class="style1">
                    部门领用申请登记
                </div>
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="15%">
                部门定额总数
            </td>
			<td style="width: 25%">
			    <asp:TextBox ID="txtPersonRation" CssClass="text-input" ReadOnly="true" runat="server"></asp:TextBox></td>
			<td class="td-label" width="15%">名 &nbsp;&nbsp; 称</td>
			<td>
			    <asp:TextBox ID="txtResName" CssClass="text-input" style="width:85%" ReadOnly="true" runat="server"></asp:TextBox>
			    <img src="../../../StyleCss/add.gif" id="imgsel" style="cursor:hand" onclick="PickMatterial();" runat="server" />

			    <input id="HdnMatterialID" value="0" style="width: 20px" type="hidden" runat="server" />

			</td>
		</tr> 
		<tr>
			<td class="td-label" width="15%">
                分 &nbsp;&nbsp; 类</td>
            <td style="width: 25%">
                <asp:DropDownList ID="DDLUseType" Enabled="false" runat="server"><asp:ListItem Value="0" Text="不回收" /><asp:ListItem Value="1" Text="以旧换新" /></asp:DropDownList></td>
            <td class="td-label" width="15%">
                单 &nbsp;&nbsp; 位</td>
			<td style="width: 30%">
			    <asp:TextBox ID="txtUnit" CssClass="text-input" ReadOnly="true" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                单 &nbsp;&nbsp; 价</td>
            <td style="width: 25%">
                <asp:TextBox ID="txtInStoragePrice" CssClass="text-input" Enabled="false" runat="server"></asp:TextBox>
                <input id="HdnPrice" value="0" style="width: 20px" type="hidden" runat="server" />

                </td>
            <td class="td-label" width="15%">
                数 &nbsp;&nbsp; 量</td>
            <td style="width: 30%">
                <asp:TextBox ID="txtNumber" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="td-submit" colSpan="4" height="20">
			    <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			    <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
			    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
