<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumadd.aspx.cs" Inherits="DatumAdd" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_knowledgedata_usercontrol_datuminfo_ascx" Src="~/EPC/KnowledgeData/UserControl/datuminfo.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_knowledgedata_usercontrol_fileinfo_ascx" Src="~/EPC/KnowledgeData/UserControl/fileinfo.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>资料管理</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"/>
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" src="../../web_client/validator.js" type="text/javascript"></script>
		<script language="javascript">
		
		function OpType()
		 {
			
			
			var Code		= document.getElementById('hdnCode').value;
			var PrjCode		= document.getElementById('hdnPrjCode').value;
			var url;
			var refresh = false;
			
			url = "FileAdd.aspx?optype=ADD&DatumCode="+Code;
		    refresh = window.parent.showModalDialog(url,window,'dialogHeight:160px;dialogWidth:400px;center:1;help:0;status:0;');	
		    if (refresh==true)
			{  	   
				return true;
			}
			else
			{
				return false;
			}
			     
		 }
		 
		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD vAlign="bottom" height="22"><iewc:TabStrip ID="TabDatum" TargetID="MPTabDatum" TabDefaultStyle="background-image:url(../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TabSelectedStyle="background-image:url(../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="资料信息" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="附件信息" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip></TD>
				</tr>
				<tr>
					<td class="mp_frame_top" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"
						vAlign="top" height="100%"><iewc:MultiPage ID="MPTabDatum" Height="100%" runat="server"><iewc:PageView runat="server">
								<DIV id="div1" style="HEIGHT: 100%: width:100%">
									<MyUserControl:epc_knowledgedata_usercontrol_datuminfo_ascx ID="DatumInfo" runat="server" />
								</DIV>
							</iewc:PageView><iewc:PageView runat="server">
								<DIV id="div2" style="HEIGHT: 100%: width:100%">
									<MyUserControl:epc_knowledgedata_usercontrol_fileinfo_ascx ID="FileInfo" runat="server" />
								</DIV>
							</iewc:PageView></iewc:MultiPage></td>
				</tr>
				<TR>
					<TD class="td-submit"><asp:Button ID="Btn_Save" CssClass="button-normal" Text="确 定" OnClick="Btn_Save_Click" runat="server" />&nbsp;
						<input class="button-normal" id="btnClose" type="button" value="取 消" onclick="javascript:window.close();returnValue=false;">&nbsp;
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><input id="hdnPrjCode" type="hidden" runat="server" />
</TD>
				</TR>
			</table>
			<input id="hdnFilePath" type="hidden" name="hdnFilePath" runat="server" />
<input id="hdnCode" type="hidden" runat="server" />
<input id="hdnFileCode" type="hidden" name="Hidden1" runat="server" />

		</form>
	</body>
</HTML>
