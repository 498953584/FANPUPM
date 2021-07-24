<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressimplementmain.aspx.cs" Inherits="ProgressImplementMain" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ProgressImplementMain</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script language="javascript">
        window.onload = function () {
            replaceEmptyTable();
            var purchasePlanTable = new JustWinTable('dgMain');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
        }

        function ClickRow(obj, tbName, planId) {
            //doClick(obj,tbName);
            var url = "ProgressImplementItem.aspx?planId=" + planId;
            url += "&Type=" + document.getElementById("hidType").value;
            document.getElementById("frameImp").src = url;
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
            <div>
		        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="height: 20px;">
                        <td class="divHeader">
                            <asp:Label CssClass="divHeader" ID="lbltitle" runat="server"></asp:Label></td>
                    </tr>
                 </table>
		    </div>
            <table class="tab">
                <tr>
                    <td style="vertical-align:top;">
                    <div class="pagediv">
                        <asp:DataGrid ID="dgMain" CssClass="gvdata" AutoGenerateColumns="false" DataKeyField="PlanId" AllowPaging="true" PageSize="12" OnPageIndexChanged="dgMain_PageIndexChanged" runat="server"><Columns><asp:BoundColumn DataField="PlanCode" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="PrjName" HeaderText="填报单位"></asp:BoundColumn><asp:BoundColumn DataField="ResultsName" HeaderText="成果名称"></asp:BoundColumn><asp:BoundColumn DataField="ResultsHolders" HeaderText="执行成果人姓名"></asp:BoundColumn><asp:BoundColumn DataField="ApplicationName" HeaderText="应用工程名称"></asp:BoundColumn><asp:BoundColumn DataField="strAuditState" HeaderText="状态"></asp:BoundColumn></Columns><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                        </div>
                   </td>
                </tr>
                <tr height="49%">
                    <td>
                        <iframe id="frameImp" src="ProgressImplementItem.aspx" frameborder="0" width="100%"
                            height="100%"></iframe>
                        <input id="hidType" type="hidden" name="Hidden1" runat="server" />

                    </td>
                </tr>
            </table>
    </form>
</body>
</html>
