<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InStoreroomEdit.aspx.cs" Inherits="oa_WorkManage_StorageManage_InStoreroomEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>入库单登记</title>
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
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" target="win" runat="server">
        <table class="table-normal" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
                入库单登记</td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                仓 &nbsp;&nbsp; 库</td>
			<td>
                <asp:DropDownList ID="DDLStorage" DataSourceID="SQLDataSource" DataTextField="DepotName" DataValueField="DepotID" runat="server"><asp:ListItem Value="0" Text="不回收" /><asp:ListItem Value="1" Text="以旧换新" /></asp:DropDownList>
                <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_OfficeRes_Depot] ORDER BY [CorpCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
            </td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                入库单号</td>
            <td>
                <asp:TextBox ID="txtBillCode" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                入库日期</td>
			<td>
                <JWControl:DateBox ID="txtInDate" CssClass="text-input" runat="server"></JWControl:DateBox></td>
		</tr>   
		<tr>
			<td class="td-label" width="20%">
                办 理 人</td>
            <td>
                <asp:TextBox ID="txtTransactor" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                备 &nbsp; &nbsp; 注</td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" style="width:99%" Rows="6" TextMode="MultiLine" runat="server"></asp:TextBox></td>
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
