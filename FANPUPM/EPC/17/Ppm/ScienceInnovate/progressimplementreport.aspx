<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressimplementreport.aspx.cs" Inherits="ProgressImplementReport" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ProgressImplementReport</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
		function Upfile()
		{
			var MainId = document.getElementById("hidMainId").value;
			var url = "/CommonWindow/Annex/AnnexList.aspx?mid=1724&rc="+MainId+"&at=0&type=2";	
			window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');	
		}
		</script>
	</head>
	<body class="body_popup">
		<form id="Form1" method="post" runat="server">
		    <div>
		        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="height: 20px;">
                        <td class="divHeader">
                            上报实施情况</td>
                    </tr>
                 </table>
		    </div>
			<TABLE id="Table1"class="tableContent2"cellpadding="5px" cellspacing="0">
					<TR height="10">
						<TD class="word">审核人</TD>
						<TD class="txt">
							<asp:TextBox ID="txtExaminePeople" runat="server"></asp:TextBox></TD>
						<TD class="word">审核日期</TD>
						<TD class="txt">
							<JWControl:DateBox ID="txtExamineTime" ReadOnly="true" runat="server"></JWControl:DateBox></TD>
					</TR>
					<TR>
						<TD class="word">相关附件</TD>
						<TD colspan="3" class="txt">
							
							<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></TD>
					</TR>
					<TR>
						<TD class="word">实施情况</TD>
						<TD colspan="3" class="txt">
							<asp:TextBox ID="txtActualizeCircs" TextMode="MultiLine" Height="100px" CssClass="textarea" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word">备注</TD>
						<TD colspan="3" class="txt">
							<asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="30px" runat="server"></asp:TextBox></TD>
					</TR>
										<tr>
					    <TD class="word">填报人</TD>
						<TD class="txt">
							<asp:TextBox ID="txtFillPeople" runat="server"></asp:TextBox></TD>
						<TD class="word">填报日期</TD>
						<TD class="txt">
							<JWControl:DateBox ID="txtFillTime" ReadOnly="true" runat="server"></JWControl:DateBox></TD>
					</TR>
				</TABLE>
				<div class="divFooter2">
                <table class="tableFooter2">
			        <TR class="tableFooter2">
						<TD class="td-submit" colspan="4"><input id="hidMainID" type="hidden" name="Hidden1" runat="server" />

							<asp:Button ID="btnSave" Text="保  存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />&nbsp;<INPUT type="button" value="取  消" onclick="top.ui.closeTab();" class="button-normal"></TD>
					</TR>
				</table>
			</div>
			<input type="hidden" id="hidMainId" />
            <asp:HiddenField ID="hdnProgressGuid" runat="server" />
		</form>
	</body>
</HTML>
