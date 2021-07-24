<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cry_WorkCost.aspx.cs" Inherits="WorkCost" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>cry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">

			<tr class="td-toolsbar">
                <td height="24">
                    <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出->Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                    <asp:Button ID="btnWord" CssClass="button-normal" Text="导出->Word" Width="80px" OnClick="btnWord_Click" runat="server" />
                </td>
            </tr>
   				<TR>
					<TD class="td-title"><FONT face="宋体">工程成本表 </FONT>
					</TD>
				</TR>         
				<TR>
					<TD height="22">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR class="report_grid_head" id="TrHeader" bgColor="Gainsboro" >
									<TD height="22"><FONT face="宋体">单位工程名称</FONT>：<asp:DropDownList ID="ddl_pn" AutoPostBack="true" Width="144px" runat="server"></asp:DropDownList></TD>
									<TD align="right" height="22">
										<asp:DropDownList ID="ddl_year" AutoPostBack="true" runat="server"></asp:DropDownList>年</TD>
									<TD align="left" height="22">
										<asp:DropDownList ID="ddl_month" AutoPostBack="true" runat="server"></asp:DropDownList>月</TD>
									<TD align="right" height="22">&nbsp;单位：元</TD>
								</TR>
							</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
					<table class="table-normal" id="Table4" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
					<asp:DataGrid ID="dg_ManPower" Width="100%" PageSize="6" AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="xm" HeaderText="项目"></asp:BoundColumn><asp:BoundColumn DataField="xh" HeaderText="行次"></asp:BoundColumn><asp:BoundColumn DataField="bqys" HeaderText="预算成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="bqsj" HeaderText="实际成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="bqjde" HeaderText="降低额" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="bqjdl" HeaderText="降低率" DataFormatString="{0:0.00%}"></asp:BoundColumn><asp:BoundColumn DataField="ljys" HeaderText="预算成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="ljsj" HeaderText="实际成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="ljjde" HeaderText="降低额" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="ljjdl" HeaderText="降低率" DataFormatString="{0:0.00%}"></asp:BoundColumn></Columns></asp:DataGrid>
					</td></tr></table>
							</TD>
				</TR>
				<TR>
					<TD><FONT face="宋体">
							<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD width="30%">企业负责人：<U>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </U>&nbsp;</TD>
									<TD width="30%">财会负责人：<U>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </U>
									</TD>
									<TD style="WIDTH: 205px"></TD>
									<TD align="right" width="20%">制表人：
										<asp:Label ID="Lb_userName" Font-Underline="true" runat="server">Label</asp:Label><U>
										</U>
									</TD>
								</TR>
							</TABLE>
						</FONT>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
