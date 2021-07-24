<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zgsgl_right.aspx.cs" Inherits="oa_System_zgsgl_right" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>子公司制度列表</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		function showEditWin(op)
		{
		    var url ;
		    var cid=document.getElementById('Label1').value;
		    var rid=document.getElementById('hfRecord').value;
		    if(op == 'add')
		        url ="zgsSystemInfoEdit.aspx?Op=add&cid="+cid+"&RecordID="+rid;
		    if(op == 'edit')
		        url = "zgsSystemInfoEdit.aspx?Op=edit&RecordID="+rid;
		    if(op=='see')
		        url ="zgsSystemInfoEdit.aspx?Op=see&RecordID="+rid;    
		   

		    return window.showModalDialog(url,window,'resizable:yes;dialogHeight:300px;dialogWidth:400px;center:1;help:0;status:0;');
		}
		
		function getRecordID(RecordID,AuditState)
		{
		    window.document.getElementById('hfRecord').value = RecordID;
		    if(AuditState<0)
		    {
		    document.getElementById('btnAdd').disabled=true;
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		    document.getElementById('btnStartWF').disabled = false;
		    }
		     if(AuditState>=0)
		    {
		    document.getElementById('btnAdd').disabled=true;
		    document.getElementById('btnEdit').disabled = true;
		    document.getElementById('btnDel').disabled = true;
		    document.getElementById('btnStartWF').disabled = true;
		    }
		}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" headerstyle-cssclass="grid_head" runat="server">
			<FONT face="宋体">
				<TABLE id="tablex" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				    <TR>
						<td colspan="2" class="td-title"><input type="hidden" id="Label1" style="width:1px" runat="server" />
集团制度管理</td>
					</TR>
					<TR>
						<td colspan="2" class="td-toolsbar">
                            &nbsp;<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                            <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                            <asp:Button ID="btnStartWF" Enabled="false" Text="提交审核" OnClick="btnStartWF_Click" runat="server" />
                            <asp:HiddenField ID="hfRecord" runat="server" />
                        </td>
					</TR>
					<TR>
						<TD colSpan="2" style="width: 100%; height: 100%" valign="top">
						<div id="Box" style="width:100%;height:100%">
						<asp:DataGrid ID="DataGrid1" Width="100%" CssClass="grid" ItemStyle-CssClass="grid_row" HeaderStyle-CssClass="grid_head" AutoGenerateColumns="false" PageSize="20" AllowPaging="true" OnItemDataBound="DataGrid1_ItemDataBound" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:ButtonColumn DataTextField="SystemName" HeaderText="制度名称"></asp:ButtonColumn><asp:BoundColumn DataField="RecordDate" HeaderText="制订日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="制订人"></asp:BoundColumn><asp:BoundColumn DataField="AuditState" HeaderText="状态"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn DataField="RecordID" Visible="false"></asp:BoundColumn></Columns><PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle></asp:DataGrid></div></TD>
					</TR>
				</TABLE>
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</FONT>
		</form>
	</body>
</HTML>
