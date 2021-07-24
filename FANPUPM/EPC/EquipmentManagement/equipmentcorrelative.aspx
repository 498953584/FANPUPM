<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentcorrelative.aspx.cs" Inherits="EquipmentCorrelative" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title><%=EquipmentCorrelative.showTitle %></title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		    function clickEquipmentRow(equipmentCode) {
		        document.getElementById('hdnEquipmentCode').value = equipmentCode;
		        document.getElementById('btnNav').disabled = false;
		    }
		    function goWhere(url, op) {
		        var equipmentCode;
		        var url;

		        equipmentCode = document.getElementById('hdnEquipmentCode').value;
		        if (op == 1) {
		            url += "?ec=" + equipmentCode + "&t=0";
		            window.showModalDialog(url, window, "dialogHeight:400px;dialogWidth:720px;center:1;help:0;status:0;");
		            //window.open(url);
		            return false;
		        }
		        else {
		            return true;
		        }
		    }
		    function pickEquipmentType() {
		        var res = new Array();
		        res[0] = "";
		        res[1] = "";
		        var url = "/EPC/Basic/Resource/ResourceTypeFrame.aspx";
		        window.showModalDialog(url, res, "dialogHeight:320px;dialogWidth:500px;center:1;help:0;status:0;");
		        if (res[0] != "") {
		            document.getElementById('hdnEquipmentType').value = res[0];
		            document.getElementById('txtEquipType').value = res[1];
		        }
		    }
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%">
				<tr>
					<td class="td-title"><%=EquipmentCorrelative.showTitle %> </td>
				</tr>
				<tr>
					<td class="td-search" align="right">&nbsp;&nbsp;编号：
						<asp:TextBox ID="txtEquipCode" Width="104px" runat="server"></asp:TextBox>名称：
						<asp:TextBox ID="txtEquipName" Width="88px" runat="server"></asp:TextBox>&nbsp;类型：
						<input type="text" readonly="true" class="txtreadonly" style="BACKGROUND-COLOR: #ffffc0" id="txtEquipType" runat="server" />
&nbsp;<span><img src="../../../images/pickRation.gif" border="0" onclick="pickEquipmentType();" height="16"
								width="16" style="CURSOR:hand;valign:bottom"></span> <input id="hdnEquipmentType" style="WIDTH: 10px" type="hidden" name="hdnEquipmentType" size="1" runat="server" />

						<asp:Button ID="btnSearch" CssClass="button_search" OnClick="btnSearch_Click" runat="server" />&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td class="td-toolsbar" align="right"><input id="hdnEquipmentCode" style="WIDTH: 10px" type="hidden" name="hdnEquipmentCode">
						<asp:Button ID="btnNav" Enabled="false" Width="85px" runat="server" />&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td valign="top">
						<div id="dvEquipment" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:DataGrid ID="dgEquipment" Width="100%" CssClass="grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><SelectedItemStyle CssClass="grid_row"></SelectedItemStyle><EditItemStyle CssClass="grid_row"></EditItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<asp:Label ID="Label1" runat="server"><%# Convert.ToString(Container.ItemIndex + 1) %></asp:Label>
										</ItemTemplate>

<EditItemTemplate>
										</EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="EquipmentManCode" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="EquipmentName" HeaderText="名称"></asp:BoundColumn><asp:BoundColumn DataField="Spec" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn DataField="ThePrecision" HeaderText="精度"></asp:BoundColumn><asp:BoundColumn DataField="Manufacturer" HeaderText="制造厂家"></asp:BoundColumn><asp:BoundColumn DataField="FactoryCode" HeaderText="出厂编号"></asp:BoundColumn><asp:BoundColumn DataField="PurchaseDate" HeaderText="购置日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="CheckCycle" HeaderText="检定周期" DataFormatString="{0}月"></asp:BoundColumn><asp:BoundColumn HeaderText="所属单位"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
