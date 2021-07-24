<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBSJTop.aspx.cs" Inherits="SysFrame2_DBSJ_DBSJTop" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	
	<style type="text/css">
		body
		{
			font-size: 12px;
		}
		
		.desktop_panel
		{
			height: 160px;
		}
		
		.desktop_panel td
		{
			border-bottom: 1px dotted #94b5ef;
		}
		
		#td_footer
		{
			text-align: right;
			border-bottom-style: none;
		}
		
		.td_content
		{
			cursor: pointer;
		}
	</style>
	<script type="text/javascript">
//		addEvent(window, 'load', function(){
//			alert(11);
//		});
		
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:GridView ID="gvwTask" AutoGenerateColumns="false" ShowHeader="false" runat="server"><Columns><asp:BoundField DataField="V_Content" /><asp:BoundField DataField="DTM_DBSJ" /></Columns></asp:GridView>
		<asp:Repeater ID="rptTask" runat="server">
<HeaderTemplate>
				<table class="desktop_panel">
			</HeaderTemplate>

<ItemTemplate>
				<tr>
					<td class="td_content" id='<%# Eval("I_XGID").ToString() %>'>
						<%# Eval("V_Content") %>
					</td>
					<td>
						<%# Eval("DTM_DBSJ") %>
					</td>
				</tr>
			</ItemTemplate>

<FooterTemplate>
				<tr>
					<td id="td_footer" colspan="2">
						更多>>
					</td>
				</tr>
				</table>
			</FooterTemplate>
</asp:Repeater>
	</div>
	</form>
</body>
</html>
