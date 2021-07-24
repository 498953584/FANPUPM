<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expertprojectview.aspx.cs" Inherits="expertprojectview" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="HEAD1" runat="server"><title>施工组织</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
        <script type="text/javascript" src="../../../../Script/jquery.js"></script>
		<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
        <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>

        <script type="text/javascript">
           window.onload = function() {
             var mark=document.getElementById("hdnmark").value;
             if(mark==1)
               {
                 GetWaterMarked(window,'/images/yinzh.jpg','this');
               }
          }
        </script>
	</head>
	<body scroll="no" style="height:100%;">
		<form id="Form1" method="post" runat="server">
			<div class="divContent2">
			  <table width="100%">
			  <TR>
						<td class="divHeader" style="width:100%">专项方案</td>
					</TR>
			  </table>
			  <TABLE border="0px" cellpadding="0" cellspacing="0" class="viewTable">
				
				<TR>
					<TD class="descTd" style=" white-space:nowrap;">填报单位</TD>
					<TD colspan="3" class="elemTd">
                        <asp:Label ID="lblUnit" Text="" runat="server"></asp:Label>
                    </TD>
				</TR>
				<tr>	
					<TD class="descTd" style=" white-space:nowrap;">工程名称</TD>
					<TD colspan="3" class="elemTd">
                        <asp:Label ID="lblName" Text="" runat="server"></asp:Label>
                    </TD>
				</tr>
				<tr>	
					<TD class="descTd" style=" white-space:nowrap;">方案名称</TD>
					<TD colspan="3" class="elemTd">
                        <asp:Label ID="lblfangan" Text="" runat="server"></asp:Label>
                    </TD>
				</tr>
				<tr>	
					<TD class="descTd" style=" white-space:nowrap;">审核情况</TD>
					<TD colspan="3" class="elemTd">
                        <asp:Label ID="lblshenpi" Text="" runat="server"></asp:Label>
                    </TD>
				</tr>
				<TR>
					<TD class="descTd" style=" white-space:nowrap;">附件</TD>
					<TD colSpan="3" class="elemTd">					
                       <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </TD>
				</TR>
				<tr id="TrGDType" style="display:none;" runat="server"><td class="word" runat="server">
                        文档类别
                    </td><td class="txt" colspan="3" runat="server">
                    <asp:Label ID="lblmarkType" Text="" runat="server"></asp:Label>
                    <JWControl:DropDownTree ID="DDTClass" Visible="false" runat="server"></JWControl:DropDownTree>
                    <input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />

                    <input id="HdnDocCode" style="WIDTH: 10px" type="hidden" name="HdnDocCode" runat="server" />

                    </td></tr>
				<TR>
					<TD class="descTd" style=" white-space:nowrap;">方案描述</TD>
					<TD colSpan="3">
                        <asp:Label ID="lblshuoming" Text="" runat="server"></asp:Label>
                    </TD>
				</TR>
				<TR>
					<TD class="descTd" style=" white-space:nowrap;">备注</TD>
					<TD colSpan="3">
                        <asp:Label ID="lblRemark" Text="" runat="server"></asp:Label>
                    </TD>
				</TR>
				<TR>
					<TD class="descTd" style=" white-space:nowrap;">编制人</TD>
					<TD class="elemTd"  style=" padding-right:3px">
					    <asp:Label ID="lblpeople" Text="" runat="server"></asp:Label>
					    </TD>
					<TD class="descTd" style=" white-space:nowrap;">编制日期</TD>
					<TD class="elemTd">
                        <asp:Label ID="lblDate" Text="Label" runat="server"></asp:Label>
                    </TD>		
				</TR>	
				<TR>
					<TD class="descTd" style=" white-space:nowrap;">填报人</TD>
					<TD class="elemTd"  style=" padding-right:3px">
					    <asp:Label ID="lblTianPeople" Text="" runat="server"></asp:Label>
					    </TD>
					<TD class="descTd" style=" white-space:nowrap;">填报日期</TD>
					<TD class="elemTd">
                        <asp:Label ID="lblTianDate" Text="Label" runat="server"></asp:Label>
                    </TD>		
				</TR>			
				</TABLE>
				<input type="hidden" id="hdnmark" runat="server" />

		</form>
	</body>
</HTML>
