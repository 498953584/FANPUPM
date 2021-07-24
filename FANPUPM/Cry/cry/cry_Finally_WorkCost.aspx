<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cry_Finally_WorkCost.aspx.cs" Inherits="Finally_WorkCost" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>cry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body class="body_frame" topMargin="2">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" cellSpacing="0" cellPadding="0" width="1300">
				<TR>
					<TD class="td-title" height="28"><FONT face="宋体"><FONT class="font7">最终成本控制报告表</FONT><FONT class="font6"><SPAN style="mso-spacerun: yes">&nbsp;&nbsp;</SPAN></FONT>&nbsp;
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD align="left" height="22"><FONT face="宋体">单位工程名称：<asp:DropDownList ID="ddl_pn" Width="144px" AutoPostBack="true" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:DropDownList ID="ddl_year" AutoPostBack="true" runat="server"></asp:DropDownList>年
							<asp:DropDownList ID="ddl_month" AutoPostBack="true" runat="server"></asp:DropDownList>月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							单位：元</FONT></TD>
				</TR>
				<TR>
					<TD><FONT face="宋体">
							<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" runat="server"><asp:DataGrid ID="dg_ManPower" Width="100%" Height="90%" CssClass="grid" AutoGenerateColumns="false" AllowCustomPaging="true" PageSize="6" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head" BackColor="#ECE9D8"></HeaderStyle><Columns><asp:BoundColumn DataField="xh" HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="xm" HeaderText="项目"></asp:BoundColumn><asp:BoundColumn DataField="ljys" HeaderText="预算成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="ljsj" HeaderText="实际成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="ljjde" HeaderText="降低额" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="ljjdl" HeaderText="降低率" DataFormatString="{0:0.00%}"></asp:BoundColumn><asp:BoundColumn DataField="jlys" HeaderText="预算成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="jlsj" HeaderText="实际成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="jljde" HeaderText="降低额" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="jljdl" HeaderText="降低率" DataFormatString="{0:0.00%}"></asp:BoundColumn><asp:BoundColumn DataField="ys" HeaderText="预算成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="sj" HeaderText="实际成本" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="jde" HeaderText="降低额" DataFormatString="{0:0.00}"></asp:BoundColumn><asp:BoundColumn DataField="jdl" HeaderText="降低率" DataFormatString="{0:0.00%}"></asp:BoundColumn></Columns></asp:DataGrid></div>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD height="22"><FONT face="宋体">
							<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 184px" width="184">企业负责人：<U>&nbsp;&nbsp;&nbsp;&nbsp; 
											&nbsp;&nbsp;&nbsp;&nbsp; </U>
									</TD>
									<TD style="WIDTH: 222px" width="222">财会负责人：<U>&nbsp;&nbsp;&nbsp;&nbsp; 
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </U>
									</TD>
									<TD style="WIDTH: 120px"></TD>
									<TD align="right">制表人：
										<asp:Label ID="Lb_userName" Font-Underline="true" runat="server">Label</asp:Label>&nbsp; 
										&nbsp;
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
