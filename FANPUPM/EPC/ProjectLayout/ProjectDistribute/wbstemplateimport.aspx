<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wbstemplateimport.aspx.cs" Inherits="WbsTemplateImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD  >
		<title>任务分解导入</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<base target="_self">
		<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
	</HEAD>
	<BODY bottomMargin="0" leftMargin="2" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD class="td-title">数据导入处理</TD>
				</TR>
				<TR>
					<td class="td-search" align="right" runat="server"><input id="File1" style="WIDTH: 515px; HEIGHT: 22px" type="file" size="66" name="File1" runat="server" />

					</td>
				</TR>
				<TR>
					<TD class="td-toolsbar" align="right"><asp:Button ID="Btn_excel" Text="连接数据" OnClick="Btn_excel_Click" runat="server" /><asp:Button ID="Btn_imp" Text="导入数据" Enabled="false" OnClick="Btn_imp_Click" runat="server" /></TD>
				</TR>
				<TR>
					<TD>
						<table id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="bottom" colSpan="2" height="20"><asp:DataGrid ID="Datagrid2" HeaderStyle-HorizontalAlign="Center" CssClass="grid" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle></asp:DataGrid><iewc:TabStrip ID="tabContract" TabIndex="-1" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:180;height:20;font-size:12px;text-align:center;" TargetID="mpContract" TabHoverStyle="background-color:#777777;" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:180;height:20;text-align:center;" runat="server"><Items><iewc:Tab Text="分部分项工程量清单计价表" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="单价构成表" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip></td>
							</tr>
							<tr>
								<td class="mp_frame_top" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"
									vAlign="top" colSpan="2"><iewc:MultiPage ID="mpContract" Height="100%" runat="server"><iewc:PageView runat="server">
											<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD>
														<DIV style="OVERFLOW: auto; HEIGHT:100%;width:100%;">
															<asp:DataGrid ID="dgd_TaskList" HeaderStyle-HorizontalAlign="Center" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="F2" HeaderText="项目编号"></asp:BoundColumn><asp:BoundColumn DataField="F3" HeaderText="项目名称"></asp:BoundColumn><asp:BoundColumn DataField="F4" HeaderText="计量单位"></asp:BoundColumn><asp:BoundColumn DataField="F5" HeaderText="工程数量"></asp:BoundColumn><asp:BoundColumn DataField="F6" HeaderText="综合单价(元)"></asp:BoundColumn><asp:BoundColumn DataField="F7" HeaderText="合价(元)"></asp:BoundColumn><asp:BoundColumn DataField="F8" HeaderText="备注"></asp:BoundColumn></Columns></asp:DataGrid>
														</DIV>
													</TD>
												</TR>
											</TABLE>
										</iewc:PageView><iewc:PageView runat="server">
											<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD>
														<DIV style="OVERFLOW: auto; HEIGHT:100%;width:100%;">
															<asp:DataGrid ID="dgd_List" HeaderStyle-HorizontalAlign="Center" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="清单编号" HeaderText="清单编号"></asp:BoundColumn><asp:BoundColumn DataField="清单名称" HeaderText="清单名称"></asp:BoundColumn><asp:BoundColumn DataField="费用类别" HeaderText="费用类别"></asp:BoundColumn><asp:BoundColumn DataField="资源名称" HeaderText="资源名称"></asp:BoundColumn><asp:BoundColumn DataField="单位" HeaderText="单位"></asp:BoundColumn><asp:BoundColumn DataField="单价" HeaderText="单价(元)"></asp:BoundColumn><asp:BoundColumn DataField="数量" HeaderText="数量"></asp:BoundColumn><asp:BoundColumn DataField="金额" HeaderText="金额(元)"></asp:BoundColumn><asp:BoundColumn DataField="综合费率" HeaderText="综合费率"></asp:BoundColumn></Columns></asp:DataGrid>
														</DIV>
													</TD>
												</TR>
											</TABLE>
										</iewc:PageView></iewc:MultiPage></td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>
