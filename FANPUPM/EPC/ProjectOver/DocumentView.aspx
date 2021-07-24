<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentView.aspx.cs" Inherits="DocumentView" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DocumentView</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<style type="text/css">
		   .txt
		   { border: solid 1px #B5CCDE;
        	 width:100%; 
        	 height: 15px; 
        	}
		</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="divContent2">
			    <table width="100%">
			        <tr>
						<td height="18" class="divHeader"><b>归档信息</b></td>
					</tr>
			    </table>
				<TABLE class="tableContent2" id="Table1" cellSpacing="5px" cellPadding="5px" width="100%"
					border="0">
					
					<TR>
						<TD class="word">文档类别：</TD>
						<TD colSpan="3"class="txt">
							<asp:Label ID="LblClass" runat="server"></asp:Label><input id="HdnDocCode" style="WIDTH: 10px" type="hidden" name="HdnDocCode" runat="server" />
<input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />
</TD>
					</TR>
					<TR>
						<TD class="word">归档名称：</TD>
						<TD colSpan="3" class="txt">
							<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
							<asp:Label ID="LblDocumentName" runat="server"></asp:Label></TD>
					</TR>
					<tr>
						<TD class="word">文 档 号：</TD>
						<TD colSpan="3" class="txt">
							<asp:Label ID="LblFileCode" runat="server"></asp:Label></TD>
					</tr>
					<tr>
						<TD class="word">卷 标 号：</TD>
						<TD colSpan="3" class="txt">
							<asp:Label ID="LblRollCode" runat="server"></asp:Label></TD>
					</tr>
					<TR>
						<TD class="word">存放位置：</TD>
						<TD colSpan="3" class="txt">
							<asp:Label ID="LblStorage" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="word">备 注：</TD>
						<TD colSpan="3" class="txt">
							<asp:Label ID="LblRemark" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="word">附 件：</TD>
						<TD colSpan="3" class="txt">
                            <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
