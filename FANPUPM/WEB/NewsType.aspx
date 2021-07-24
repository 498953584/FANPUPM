<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsType.aspx.cs" Inherits="WEB_NewsType" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>新闻类型列表</title>
    <script language="javascript" type="text/javascript">
    function ClickRow(ClassCode)
	{
		document.getElementById('btnEdit').disabled = false;
        document.getElementById('btnDel').disabled = false;

		document.getElementById('HdnID').value = ClassCode;
	}
	function OpenWin(op)
	{
	    var ClassCode = document.getElementById('HdnID').value;
		var url= "NewsTypeEdit.aspx?t=" + op + "&typeCode=" + ClassCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:160px;dialogWidth:350px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
    </script>
</head>
<body scroll="no" class="body_clear">
    <form id="form1" runat="server">
    <div>
    <table class="table-none" id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%"
				border="1">
				<tr>
					<td class="td-title" colSpan="3">
                        新闻类型管理
					</td>
				</tr>
				<tr style="height: 20px">
					<td class="td-toolsbar" align="right" colSpan="3" style="height: 20px"><asp:Button ID="btnAdd" CssClass="button-normal" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;
						<asp:Button ID="btnEdit" CssClass="button-normal" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;
						<asp:Button ID="btnDel" CssClass="button-normal" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                        <asp:HiddenField ID="HdnID" runat="server" />
                        &nbsp;
					</td>
				</tr>
				<tr>
					<td width="100%" colSpan="3" style="height:400px">
						<div class="div-scroll" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<asp:GridView ID="Dbg_item" CssClass="grid" AutoGenerateColumns="false" Width="100%" DataSourceID="SqlDataSource1" OnRowDataBound="Dbg_item_RowDataBound" runat="server"><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="c_xwlxdm" HeaderText="新闻类型代码" SortExpression="c_xwlxdm" /><asp:BoundField DataField="c_xwlxmc" HeaderText="新闻类型名称" SortExpression="c_xwlxmc" /></Columns><RowStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row" VerticalAlign="Middle"></RowStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle></asp:GridView>
						</div>
                        <asp:SqlDataSource ID="SqlDataSource1" SelectCommand="SELECT [i_lxid], [c_xwlxdm], [c_xwlxmc], [c_parentid] FROM [Web_NewsCategories] ORDER BY [c_xwlxdm]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
					</td>
				</tr>
				
			</table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
