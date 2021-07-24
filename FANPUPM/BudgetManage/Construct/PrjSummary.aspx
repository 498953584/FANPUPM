<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjSummary.aspx.cs" Inherits="BudgetManage_Construct_PrjSummary" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工程汇总表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../../Script/lightbox/jquery.lightbox-0.5.css" media="screen" />

    <style type="text/css">
        #gallery img
        {
            border: 1px solid #3e3e3e;
            border-width: 5px 5px 20px;
        }
    </style>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/lightbox/jquery.lightbox-0.5.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var gvConstruct = new JustWinTable('gvConstruct');
            replaceEmptyTable('emptygvConstruct', 'gvConstruct');
            setWidthAndHeight();
            showTooltip('tooltip', 25);
            //页面加载时从cookie取滚动条位置信息，然后附值给滚动条
            var arr;
            if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/))
                document.getElementById('div_project').scrollTop = parseInt(arr[1]);
            if (document.getElementById('gvConstruct') != null) {

                var tb = document.getElementById('gvConstruct');
                var trs = tb.getElementsByTagName('tr');
                for (var i = 0; i < trs.length; i++) {
                    if ($(trs[i]).attr('budLevel') == "2") {
                        registerTrClick(trs[i]);
                    }
                }
            }
        });

        function registerTrClick(tr) {
            var prjName = document.getElementById('hfldPrjName').value;
            var month = document.getElementById('hfldMonth').value;
            var taskName = $(tr).attr('taskName');
            var title = prjName + taskName + month + '月完成工作量月报';
            var tds = tr.getElementsByTagName('td');
            var sp = tds[1].getElementsByTagName('span')[0];
            sp.className = 'link';
            sp.onclick = function () {
                viewopen(tr.id, title);
            }
        }

        //设置div高度和宽度
        function setWidthAndHeight() {
            $('#divBudget').height($(this).height() - $('#divTop').height() - $('#divTop2').height() - 2);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
            $('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
        }

        function viewopen(taskId, title) {
            var url = '/BudgetManage/Construct/CompletedMonthlyReport.aspx?taskId=' + taskId + '&date=' + document.getElementById('hfldDate').value + '&title=' + encodeURI(title);
            toolbox_oncommand(url, title);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="width: 100%">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <div id="div1" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
                                    <table style="width: 100%; height: 88%;">
                                        <tr id="divTop">
                                            <td>
                                                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                                    <tr>
                                                        <td class="descTd">
                                                            日期
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="divTop2">
                                            <td class="divFooter" style="text-align: left; width: 100%;">
                                                <asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 100%;">
                                                <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
                                                    <asp:GridView ID="gvConstruct" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="TaskId,BudLevel,TaskName" runat="server">
<EmptyDataTemplate>
                                                            <table class="gvdata" cellspacing="0" rules="all" border="1" id="emptygvConstruct"
                                                                style="width: 100%; border-collapse: collapse;">
                                                                <tr>
                                                                    <th class="header">
                                                                        序号
                                                                    </th>
                                                                    <th class="header">
                                                                        工程项目名称
                                                                    </th>
                                                                    <th class="header">
                                                                        清单金额
                                                                    </th>
                                                                    <th class="header">
                                                                        上月末完成金额
                                                                    </th>
                                                                    <th class="header">
                                                                        本月完成金额
                                                                    </th>
                                                                    <th class="header">
                                                                        本月末完成金额
                                                                    </th>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="50px">
<HeaderTemplate>
                                                                    序号
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# Eval("Num") %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
                                                                    工程项目名称
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("TaskName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                                                                    <span id="spName" >
                                                                        <%# StringUtility.GetStr(Eval("TaskName"), 25) %>
                                                                    </span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                                    清单金额
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                                    上月末完成金额
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# System.Convert.ToDecimal(Eval("LastMonthConsTotal")).ToString("0.000") %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                                    本月完成金额
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# System.Convert.ToDecimal(Eval("CurrentMonthConsTotal")).ToString("0.000") %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                                    本月末完成金额
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# System.Convert.ToDecimal(Eval("SumCons")).ToString("0.000") %>
                                                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldPrjName" runat="server" />
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfldMonth" runat="server" />
    <asp:HiddenField ID="hfldDate" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
