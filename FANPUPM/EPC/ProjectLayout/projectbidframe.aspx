<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectbidframe.aspx.cs" Inherits="ProjectBidFrame" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>框架</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript">
		//部门选择
		function pickDept()
		{
			var dept = new Array();
			dept[0] = "";
			dept[1] = "";
			var url= "../../CommonWindow/PickDept.aspx";
			alert(url);
			window.showModalDialog(url,dept,"dialogHeight:434px;dialogWidth:300px;center:1;help:0;status:0;");
			if (dept[0]!="")
			{
				document.getElementById('HdnDeptID').value = dept[0];
				document.getElementById('tbBidDept').value = dept[1];
			}						
		}
		//中标公司
		function BidCorp()
		{
			var corp = new Array();
			corp[0] = "";
			corp[1] = "";
			var url= "CommonWindow/PickCorp.aspx";
			
			window.showModalDialog(url,corp,"dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
			if (corp[0]!="")
			{
				document.getElementById('HdnBingoCorp').value = corp[0];
				document.getElementById('tbBingoCorp').value = corp[1];
			}
			
		}
		</script>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="80" class="td-search">
						<TABLE class="table-none" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="left">
									<asp:Label ID="lblDate" runat="server">添加时间：</asp:Label><JWControl:DateBox ID="dbBegin" Width="70px" runat="server"></JWControl:DateBox>-<JWControl:DateBox ID="dbEnd" Width="70px" runat="server"></JWControl:DateBox>
									信息编号：<asp:TextBox ID="tbInfoCode" Width="120px" runat="server"></asp:TextBox>
									项目名称：<asp:TextBox ID="tbPrjName" Width="120px" runat="server"></asp:TextBox>
									<asp:Label ID="lblStatus" runat="server">状      态：</asp:Label>
									<asp:DropDownList ID="ddlStatus" Width="60px" runat="server"><asp:ListItem Value="%" Selected="true" Text="全部" /><asp:ListItem Value="1" Text="前期" /><asp:ListItem Value="3" Text="招标" /><asp:ListItem Value="4" Text="投标" /><asp:ListItem Value="5" Text="开标" /></asp:DropDownList>
								</TD>
								<td rowSpan="3" width="70" valign="middle" align="center"><asp:Button ID="btnSearch" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" /></td>
							</TR>
							<TR>
								<TD align="left">
									项目单位：<asp:DropDownList ID="ddlCorp" Width="142px" runat="server"></asp:DropDownList>
									项目类型：<JWControl:DropDownTree ID="ddlPrjType" Width="120px" runat="server"></JWControl:DropDownTree>
									<asp:Label ID="lblPrice" runat="server">项目估价：</asp:Label><asp:DropDownList ID="ddlPrice" Width="120px" runat="server"><asp:ListItem Value="0-*" Selected="true" Text="全部" /><asp:ListItem Value="0-100" Text="100万" /><asp:ListItem Value="100-500" Text="100万-500万" /><asp:ListItem Value="500-1000" Text="500万-1000万" /><asp:ListItem Value="1000-5000" Text="1000万-5000万" /><asp:ListItem Value="5000-*" Text="5000万以上" /></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD align="left">
									招标类型：<JWControl:DropDownTree ID="ddlInviteBidType" Width="142px" runat="server"></JWControl:DropDownTree>
									招标方式：<JWControl:DropDownTree ID="ddlInviteBidMode" Width="120px" runat="server"></JWControl:DropDownTree>
									<asp:Label ID="lblBingoCorp" Visible="false" runat="server">中标公司：</asp:Label><asp:TextBox ID="tbBingoCorp" Width="120px" Visible="false" runat="server"></asp:TextBox><img id="ImgBingoCorp" align="absmiddle" onclick="BidCorp();" alt="" src="../../Images/contact.gif" name="ImgBingoCorp" style="DISPLAY: none" runat="server" />
 <input id="HdnBingoCorp" style="WIDTH: 10px" type="hidden" name="HdnBingoCorp" runat="server" />

									<asp:Label ID="lblBidDept" Visible="false" runat="server">投标部门：</asp:Label><asp:TextBox ID="tbBidDept" Width="102px" Visible="false" runat="server"></asp:TextBox><img id="IMG1" align="absmiddle" onclick="pickDept();" alt="" src="../../Images/contact.gif" style="DISPLAY: none" runat="server" />
<input id="HdnDeptID" style="WIDTH: 10px" type="hidden" name="HdnDeptID" runat="server" />

									项目地点：
									<JWControl:DropDownTree ID="ddlAddress" Width="96px" runat="server"></JWControl:DropDownTree>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><iframe id="iframeMain" frameborder="0" width="100%" height="100%" src='<%# Convert.ToString(base.Url) %>' runat="server"></iframe>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
