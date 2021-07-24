<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologyproposallist.aspx.cs" Inherits="TechnologyProposalList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>TechnologyProposalList</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
		 <div class="divContent2">
		<table width="100%">
		  <TR>
			<TD class="divHeader">审核意见列表</TD>
		  </TR>
		</table>
			<TABLE id="Table1" cellSpacing="0" cellPadding="5px" width="100%" border="0" class="tableContent2">
				
				<TR>
					<TD vAlign="top" height="98%">
						<asp:DataGrid ID="dgList" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle Height="22px" CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="TechDate" HeaderText="评论时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="评论人"></asp:BoundColumn><asp:BoundColumn DataField="TechProposal" HeaderText="意见内容"></asp:BoundColumn></Columns></asp:DataGrid></TD>
				</TR>
			</TABLE>
			</div>
		</form>
	</body>
</HTML>
