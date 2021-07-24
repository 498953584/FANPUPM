<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="Search1" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>Search</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script type="text/javascript" src="../../../../Script/jquery.js"></script>
        <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
        <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
		<script language="javascript">
		    window.onload = function() {
		        var purchasePlanTable = new JustWinTable('DataGrid1');
		    }
           function ClickRow(obj) {
               document.getElementById("hdnClientCode").value = obj;
           }
		function openannexpage() {
		    var ClientCode = document.getElementById('hdnClientCode').value;
		    if (ClientCode == '0') {
		        alert('请选择要查看的列！');
		        return;
		    }
		    window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1743&rc=" + ClientCode + "&at=0&type=1", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
		}
		function OpView() {
		    var ClientCode = document.getElementById('hdnClientCode').value;
		    if (ClientCode == '0') {
		        alert('请选择要编辑的列！');
		        return;
		    }
		    var url = "/EPC/17/Ppm/Prog/Edit.aspx?op=&id=" + ClientCode + "";
		    parent.desktop.flowclass = window; //不可少
		    toolbox_oncommand(url, "查看曝光内容"); //引用js
		    //window.open("Edit.aspx?op=&id="+ClientCode,"_blank",'width=500,height=240,left=150,top=150');
		}
		function OpQuery(ClientCode) {
		    //var ClientCode = document.getElementById('hdnClientCode').value;
		    if (ClientCode == '0') {
		        alert('请选择要查看的列！');
		        return;
		    }
		    var url = "/EPC/17/Ppm/Prog/Edit.aspx?op=query&id=" + ClientCode + "";
		    parent.desktop.flowclass = window; //不可少
		    toolbox_oncommand(url, "查看曝光内容"); //引用js
		    //window.open("Edit.aspx?op=&id="+ClientCode,"_blank",'width=500,height=240,left=150,top=150');
		}
        </script>
		<style type="text/css">
        .dgheader
        {
	        background-color: #EEF2F5;
	        white-space: nowrap;
	        overflow: hidden;
	        font-weight: normal;
	        text-align: center;
	        border-color: #CADEED;
	        color: #727FAA;
	        padding-left: 6px;
	        padding-right: 6px;

	        }
    </style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="100%" align="center">
				<tr>
					<td class="divHeader"><asp:Label ID="Label2" runat="server">曝光台</asp:Label></td>
				</tr>
				<tr height="25px">
					<td class="descTd">曝光部门:
						<asp:TextBox ID="TextBox1" Width="120px" runat="server"></asp:TextBox><asp:Button ID="btn_Search" Text="查询" OnClick="Button1_Click" runat="server" /></td>
				</tr>
				<tr>
					<td class="divFooter" style="text-align:left"><INPUT onclick="OpView()" id="btnView" type="button" value="查 看"></td>
				</tr>
				<tr>
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="DataGrid1" CssClass="gvdata" DataKeyField="ReportId" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="ReportTime" HeaderText="曝光时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="曝光内容"><ItemTemplate>
                                    <asp:Label ID="Label1" CssClass="link" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="ReportDpt" HeaderText="曝光部门"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
					</td>
				</tr>
			</TABLE>
			<input id="hdnClientCode" type="hidden" value="0" name="hdnClientCode" runat="server" />

		</form>
	</body>
</HTML>
