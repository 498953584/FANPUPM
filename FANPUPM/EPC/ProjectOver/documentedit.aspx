<%@ Page Language="C#" AutoEventWireup="true" CodeFile="documentedit.aspx.cs" Inherits="DocumentEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DocumentEdit</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
			function UpFile()
			{
				var RecordCode = document.getElementById('HdnDocCode').value;//编号
				var url = "/CommonWindow/Annex/AnnexList.aspx?mid=26&rc="+RecordCode+"&at=0&type=2";			
				window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');	
			}
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
		  <div class="divContent2">
		    <table width="100%">
		       <tr>
					<td height="18" class="divHeader"><b>编辑归档信息</b></td>
			   </tr>
		    </table>
			<TABLE class="tableContent2" id="Table1" height="100%" cellSpacing="0" cellPadding="5px" width="100%"
				border="0">
				
				<TR>
					<TD class="word" width="90" height="20">文档类别：</TD>
					<TD colSpan="3" class="txt">
						<JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree><input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />
<input id="HdnDocCode" style="WIDTH: 10px" type="hidden" name="HdnDocCode" runat="server" />
</TD>
				</TR>
				<TR>
					<TD class="word" height="20">归档名称：</TD>
					<TD colSpan="3" class="txt">
						<asp:TextBox ID="TxtDocumentName" Columns="60" MaxLength="100" runat="server"></asp:TextBox><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></TD>
				</TR>
				<TR>
					<TD class="word" height="20">文 档 号：</TD>
					<TD class="txt" colSpan="3">
						<asp:TextBox ID="TxtFileCode" MaxLength="50" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="word" width="85" height="20">卷 标 号：</TD>
					<TD class="txt" colSpan="3">
						<asp:TextBox ID="TxtRollCode" MaxLength="50" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="word" height="20">存放位置：</TD>
					<TD colSpan="3" class="txt">
						<asp:TextBox ID="TxtStorage" Columns="60" MaxLength="100" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="word" height="20">备&nbsp;&nbsp;&nbsp; 注：</TD>
					<TD colSpan="3" class="txt">
						<asp:TextBox ID="TxtRemark" Columns="60" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="word" height="20">附&nbsp;&nbsp;&nbsp; 件：</TD>
					<TD colSpan="2" class="txt">
						<asp:Button ID="BtnAnnex" Text="附 件" OnClick="BtnAnnex_Click" runat="server" /></TD>
					<TD align="center">
						<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" /></TD>
				</TR>
			</TABLE>
		 </div>
		</form>
	</body>
</HTML>
