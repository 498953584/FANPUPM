<%@ Page Language="C#" AutoEventWireup="true" CodeFile="entstandardedit.aspx.cs" Inherits="EntStandardEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>技术标准</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
        <script type="text/javascript" src="../../../../Script/jquery.js"></script>
		<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
		<script language="javascript">
			
			function checklength(theField,theAlert,maxlen)
			{
				var theValue = theField.value;
				if(theValue.length=0) return true;
				var bytelen=0;
				var flag= 0;
				var re=/[^\x00-\xff]/g;
				re.compile();
				for(var i=0;i<theValue.length;i++)
				{
					if (theValue.charCodeAt(i)>255) 
					{
						bytelen+=4; 
						flag=1;
					}
					else 
					{
						bytelen+=2;
						flag=0;
					}
				}
				
				if(bytelen>maxlen)
				{
					if(flag==0)
					{
					alert(theAlert+'输入字符串长度不能大于'+maxlen/2+',请重新输入');
					}
					else if(flag==1)
					{
						alert(theAlert+'输入长度不能大于'+Math.round(maxlen/4)+',请重新输入');
					}
					theField.select();
					theField.focus();
					return false;	
				}
				return true;
			}
		</script>
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
		  <div class="divContent2">
		     <table width="100%">
		        <TR>
					<TD class="divHeader" colSpan="2"><asp:Label ID="lb_change" Font-Bold="true" runat="server">Lable1</asp:Label>企业技术标准<input id="HdnType" style="WIDTH: 56px; HEIGHT: 22px" type="hidden" size="4" name="Hidden1" runat="server" />
</TD>
				</TR>
		     </table>
			<TABLE id="Table1" cellSpacing="0" cellPadding="5px" width="100%" border="0" class="tableContent2">			
				<TR>
					<TD class="word" style="white-space:nowrap;">技术标准编号：</TD>
					<TD colSpan="3" class="txt"><asp:TextBox ID="TxtStandCode" onblur="checklength(this,'技术标准编号',100);" MaxLength="50" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="技术标准编号不能为空！" Display="None" ControlToValidate="TxtStandCode" runat="server"></asp:RequiredFieldValidator></TD>
				</TR>
				<TR>
					<TD class="word" style="white-space:nowrap;">标准名称：</TD>
					<TD colSpan="3" class="txt"><asp:TextBox ID="TxtStandName" onblur="checklength(this,'标准名称',500);" MaxLength="50" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="标准名称不能为空！" Display="None" ControlToValidate="TxtStandName" runat="server"></asp:RequiredFieldValidator></TD>
				</TR>
				<TR>
					<TD class="word" style="white-space:nowrap;">年号：</TD>
					<TD colSpan="3" class="txt">
						<asp:TextBox ID="DatePublicTime" onblur="checklength(this,'年号',100);" MaxLength="25" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="word" style="white-space:nowrap;">标准分类：</TD>
					<TD colSpan="3" class="txt">
						<asp:Label ID="lbPlanSort" runat="server"></asp:Label><input id="hidIsSave" type="hidden" value="true" name="Hidden1" runat="server" />
</TD>
				</TR>
				<TR>
					<TD class="word" style="white-space:nowrap;">附件：</TD>
					<TD colSpan="3" class="txt">
                        <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </TD>
				</TR>
				<TR>
					<TD class="word" style="white-space:nowrap;">备注：</TD>
					<TD colSpan="3" class="txt"><asp:TextBox ID="TxtRemark" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox></TD>
				</TR>
				</TABLE>
				<div class="divFooter2">
				<table class="tableFooter2">
				<TR>
					<TD class="td-submit" colSpan="2"><asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();" type="button" value="取 消" id="BunClose" runat="server" />

						<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
						<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary></TD>
				</TR>
			   </table>
			   </div>
			</div>
            <asp:HiddenField ID="hdnEnterGuid" runat="server" />
		</form>
		
	</body>
</HTML>
