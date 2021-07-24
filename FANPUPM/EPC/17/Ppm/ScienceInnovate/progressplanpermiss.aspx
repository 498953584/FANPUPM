<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressplanpermiss.aspx.cs" Inherits="ProgressPlanPermiss" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>项目部评审</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<base target="_self">
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
				<div>
		            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 20px;">
                            <td class="divHeader">
                                项目部审核</td>
                        </tr>
                     </table>
		        </div>
		        <div>
				<TABLE id="Table1"class="tableContent2"cellpadding="5px" cellspacing="0">
					<TR>
						<TD class="word">编号</TD>
						<TD class="txt">
							<asp:TextBox ID="txtPlanCode" runat="server"></asp:TextBox></TD>
						<TD class="word">审核意见</TD>
						<TD class="txt">
							<asp:DropDownList ID="ddlAuditState" runat="server"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="word" style="white-space:nowrap;">编制单位/人</TD>
						<TD colspan="3" class="txt">
							<asp:TextBox ID="txtDeclareUnitView" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word" style="white-space:nowrap;">工程技术部审核意见</TD>
						<TD colspan="3" class="txt">
							<asp:TextBox ID="txtPanelView" TextMode="MultiLine" Height="60px" Rows="6" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word" style="white-space:nowrap;">总工程师审核意见</TD>
						<TD colspan="3" class="txt">
							<asp:TextBox ID="txtVettingCommitteeView" Height="60px" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word">备注</TD>
						<TD colspan="3" class="txt">
							<asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="30px" Rows="3" runat="server"></asp:TextBox></TD>
					</TR>					
				</TABLE>
				</div>
				<div class="divFooter2">
                 <table class="tableFooter2">
			        <TR class="tableFooter2">
					      <TD>
							<asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />
							<INPUT type="button" value="退  出" onclick="top.ui.closeTab();" class="button-normal">
						</TD>
					</TR>
				 </table>
			    </div>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
