<%@ Page Language="C#" AutoEventWireup="true" CodeFile="maintainedit.aspx.cs" Inherits="MaintainEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server"><title>机械器具维护信息编辑</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function clickRow(maintainUniqueCode)
			{
			      document.getElementById('btnDel').disabled = false;
			      document.getElementById('btnDel').className = "button_del";
				  document.getElementById('hdnSelectedIndex').value = maintainUniqueCode;
			}
			function checkQuantity(sourObj)
			{
				if (sourObj.value!="")
				{
					if (!(new RegExp(/^\d+$/).test(sourObj.value)))
					{
						alert('数量应该是整数！');
						sourObj.focus();
						return;
					}
				}
				else
				{
					sourObj.value = 0;
				}
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<asp:CompareValidator ID="CompareValidator1" Display="None" ErrorMessage="保养费用必须大于0！" ControlToValidate="txtMaintainCost" Type="Double" ValueToCompare="0" Operator="GreaterThan" runat="server"></asp:CompareValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="保养费用不能为空！" Display="None" ControlToValidate="txtMaintainCost" runat="server"></asp:RequiredFieldValidator>
			<table height="95%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td valign="bottom" height="20"><iewc:TabStrip ID="tabContract" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TargetID="mpContract" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="合同主体" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="配件信息" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip></td>
				</tr>
				<tr>
					<td class="mp_frame_top" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"
						valign="top"><iewc:MultiPage ID="mpContract" runat="server"><iewc:PageView runat="server">
								<table id="Table2" class="table-form" width="100%" cellspacing="1" cellpadding="1" border="1">
									<tr>
										<td class="td-label" width="15%">机械器具名称：</td>
										<td colspan="3"><asp:Label ID="lblEquipmentCode" runat="server"></asp:Label>
											<asp:Label ID="lblEquipmentName" runat="server"></asp:Label>
											<asp:Label ID="lblEquipmentSpec" runat="server"></asp:Label></td>
									</tr>
									<tr>
										<td class="td-label" width="15%">维修类型：</td>
										<td width="35%">
											<asp:TextBox ID="txtMaintainType" runat="server"></asp:TextBox></td>
										<td class="td-label" width="15%">保养费用：</td>
										<td width="35%">
											<asp:TextBox ID="txtMaintainCost" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">出现问题：</td>
										<td colspan="3">
											<asp:TextBox ID="txtFault" TextMode="MultiLine" Rows="3" Columns="70" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">维护人员：</td>
										<td>
											<asp:TextBox ID="txtMaintainceMan" runat="server"></asp:TextBox></td>
										<td class="td-label">维护时间：</td>
										<td>
											<JWControl:DateBox ID="dtxtMaintainDate" ReadOnly="true" runat="server"></JWControl:DateBox></td>
									</tr>
									<tr>
										<td class="td-label">维护内容：</td>
										<td colspan="3">
											<asp:TextBox ID="txtMaintainContent" TextMode="MultiLine" Rows="3" Columns="70" runat="server"></asp:TextBox></td>
									</tr>
									<tr>
										<td class="td-label">鉴定人员：</td>
										<td>
											<asp:TextBox ID="txtAppraiser" runat="server"></asp:TextBox></td>
										<td class="td-label">鉴定时间：</td>
										<td>
											<JWControl:DateBox ID="dtxtAppraisalDate" ReadOnly="true" runat="server"></JWControl:DateBox></td>
									</tr>
									<tr>
										<td class="td-label">鉴定结果：</td>
										<td colspan="3">
											<asp:TextBox ID="txtAppraisal" TextMode="MultiLine" Rows="3" Columns="70" runat="server"></asp:TextBox></td>
									</tr>
								</table>
							</iewc:PageView><iewc:PageView runat="server">
								<table class="table-normal" id="Table3" height="100%" cellspacing="0" cellpadding="0" width="100%"
									border="1" style="TABLE-LAYOUT:fixed">
									<tr>
										<td valign="top" colspan="3">
											<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
												<asp:DataGrid ID="dgdFittings" AutoGenerateColumns="false" CssClass="grid" Width="100%" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="配件名称">
<ItemTemplate>
																<asp:TextBox ID="EptName" Width="100%" Text='<%# Convert.ToString(Eval("FittingsName")) %>' runat="server"></asp:TextBox>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="配件型号">
<ItemTemplate>
																<asp:TextBox ID="EptType" Width="100%" Text='<%# Convert.ToString(Eval("Spec")) %>' runat="server"></asp:TextBox>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="配件数量">
<ItemTemplate>
																<asp:TextBox ID="EptNum" Width="100%" Text='<%# Convert.ToString(Eval("Quality")) %>' runat="server"></asp:TextBox>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="配件操作">
<ItemTemplate>
																<asp:TextBox ID="Textbox1" Width="100%" Text='<%# Convert.ToString(Eval("Operation")) %>' runat="server"></asp:TextBox>
															</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="配件备注">
<ItemTemplate>
																<asp:TextBox ID="EptRemark" Width="100%" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:TextBox>
															</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid>
											</div>
										</td>
									</tr>
									<tr>
										<td height="22" width="80%"><input id="hdnSelectedIndex" type="hidden" name="hdnSelectedIndex" runat="server" />

											<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
											<asp:Label ID="lblState" runat="server"></asp:Label><input id="hdnAnnexCode" style="WIDTH: 10px" type="hidden" name="hdnAnnexCode" runat="server" />
<input id="hdnContractCode" style="WIDTH: 10px" type="hidden" name="hdnContractCode" runat="server" />
</td>
										<td align="center" width="10%">
											<asp:Button ID="btnDel" CssClass="button_delu" Enabled="false" runat="server" /></td>
										<td align="center" width="10%">
											<asp:Button ID="btnAdd" CssClass="button_add" runat="server" /></td>
									</tr>
								</table>
							</iewc:PageView></iewc:MultiPage></td>
				</tr>
				<tr>
					<td class="td-submit"><asp:Button ID="BtnRepairEdit" Text="保 存" OnClick="BtnRepairEdit_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="BtnRepairClose" type="button" onclick="window.close()" value="关 闭">
						<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary></td>
				</tr>
			</table>
		</form>
	</body>
</html>
