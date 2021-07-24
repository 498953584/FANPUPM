<%@ Page Language="C#" AutoEventWireup="true" CodeFile="maintainview.aspx.cs" Inherits="MaintainView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server"><title>ά����Ϣ�鿴</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"/>
		<script language="javascript">
		    function checklen(e, maxlength) {
		        if (e.value.length > maxlength) {
		            alert('���볤�Ȳ��ܴ���' + maxlength);
		            e.focus();
		            return false;
		        }

		    }
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td valign="bottom" height="20">
						<iewc:TabStrip ID="tabContract" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TargetID="mpContract" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" runat="server"><Items><iewc:TabSeparator ID="TabSeparator1" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab1" Text="��ͬ����" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator2" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab2" Text="�����Ϣ" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator3" DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip></td>
				</tr>
				<tr>
					<td class="mp_frame_top" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"
						valign="top"><iewc:MultiPage ID="mpContract" runat="server"><iewc:PageView ID="PageView1" runat="server">
								<table id="Table2" class="table-form" width="100%" cellspacing="1" cellpadding="1" border="1">
									<tr>
										<td class="td-label" width="15%">��&nbsp;&nbsp;&nbsp;&nbsp;����</td>
										<td colspan="3"><asp:Label ID="lblEquipmentCode" runat="server"></asp:Label>
											<asp:Label ID="lblEquipmentName" runat="server"></asp:Label>
											<asp:Label ID="lblEquipmentSpec" runat="server"></asp:Label></td>
									</tr>
									<tr>
										<td class="td-label" width="15%">ά�����ͣ�</td>
										<td width="35%">
											<asp:TextBox ID="txtMaintainType" ReadOnly="true" runat="server"></asp:TextBox></td>
										<td class="td-label" width="15%">�������ã�</td>
										<td width="35%">
											<asp:TextBox ID="txtMaintainCost" ReadOnly="true" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">�������⣺</td>
										<td colspan="3">
											<asp:TextBox ID="txtFault" TextMode="MultiLine" Rows="3" Columns="70" ReadOnly="true" onkeyup="checklen(this,200);" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">ά&nbsp;��&nbsp;�ˣ�</td>
										<td>
											<asp:TextBox ID="txtMaintainceMan" ReadOnly="true" runat="server"></asp:TextBox></td>
										<td class="td-label">ά��ʱ�䣺</td>
										<td>
											<asp:TextBox ID="txtMaintainDate" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">ά�����ݣ�</td>
										<td colspan="3">
											<asp:TextBox ID="txtMaintainContent" TextMode="MultiLine" Rows="3" Columns="70" ReadOnly="true" onkeyup="checklen(this,200);" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">��&nbsp;��&nbsp;�ˣ�</td>
										<td>
											<asp:TextBox ID="txtAppraiser" ReadOnly="true" runat="server"></asp:TextBox></td>
										<td class="td-label">����ʱ�䣺</td>
										<td>
											<asp:TextBox ID="txtAppraisalDate" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">���������</td>
										<td colspan="3">
											<asp:TextBox ID="txtAppraisal" TextMode="MultiLine" Rows="3" Columns="70" ReadOnly="true" onkeyup="checklen(this,200);" runat="server"></asp:TextBox></td>
									</tr>
								</table>
							</iewc:PageView><iewc:PageView ID="PageView2" runat="server">
								<table class="table-normal" id="Table3" style="TABLE-LAYOUT: fixed" height="100%" cellspacing="0"
									cellpadding="0" width="100%" border="1">
									<tr>
										<td valign="top">
											<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
												<asp:DataGrid ID="dgdFittings" Width="100%" CssClass="grid" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="�������">
<ItemTemplate>
																<asp:Label ID="EptName" Width="100%" Text='<%# Convert.ToString(Eval("FittingsName")) %>' runat="server"></asp:Label>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="����ͺ�">
<ItemTemplate>
																<asp:Label ID="EptType" Width="100%" Text='<%# Convert.ToString(Eval("Spec")) %>' runat="server"></asp:Label>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="�������">
<ItemTemplate>
																<asp:Label ID="EptNum" Width="100%" Text='<%# Convert.ToString(Eval("Quality")) %>' runat="server"></asp:Label>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="�������">
<ItemTemplate>
																<asp:Label ID="Textbox1" Width="100%" Text='<%# Convert.ToString(Eval("Operation")) %>' runat="server"></asp:Label>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="�����ע">
<ItemTemplate>
																<asp:Label ID="EptRemark" Width="100%" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
															</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></div>
										</td>
									</tr>
									<tr>
										<td height="22">
											<asp:Label ID="lblState" runat="server"></asp:Label><input id="hdnAnnexCode" style="WIDTH: 10px" type="hidden" name="hdnAnnexCode" runat="server" />
<input id="hdnContractCode" style="WIDTH: 10px" type="hidden" name="hdnContractCode" runat="server" />
</td>
									</tr>
								</table>
							</iewc:PageView></iewc:MultiPage></td>
				</tr>
				<tr>
					<td class="td-submit"><input id="btnOK" type="button" value="ȷ  ��" onclick="window.close();">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="BtnRepairClose" type="button" value="�� ��" onclick="window.close();"><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
				</tr>
			</table>
		</form>
	</body>
</html>
