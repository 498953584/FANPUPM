<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryVersions.aspx.cs" Inherits="ProgressManagement_Modify_HistoryVersions" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>历史版本</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwPlans');
            showTooltip("tooltip", 25);
            replaceEmptyTable('tblEmpty', 'gvwPlans');
        });
        //查看计划申请
        function lookApply(verId, verName) {
            parent.desktop.ApplyView = window;
            var url = '/ProgressManagement/Modify/ApplyView.aspx?ic=' + verId;
            var titleText = verName + '-查看计划';
            toolbox_oncommand(url, titleText);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divBudget" class="pagediv" style="overflow: hidden;">
        <asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressVersionId,ProgressId" runat="server">
<EmptyDataTemplate>
                <table id="tblEmpty">
                    <tr class="header">
                        <th scope="col" style="width: 20px;">
                            <input type="checkbox" />
                        </th>
                        <th scope="col" style="width: 20px;">
                            序号
                        </th>
                        <th scope="col">
                            计划名称
                        </th>
                        <th scope="col">
                            版本号
                        </th>
                        <th scope="col">
                            执行
                        </th>
                        <th scope="col">
                            主计划
                        </th>
                        <th scope="col">
                            原计划名称
                        </th>
                        <th scope="col">
                            原版本号
                        </th>
                        <th scope="col">
                            申请人
                        </th>
                        <th scope="col">
                            录入日期
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                        <input type="checkbox" id="chkAll" />
                    </HeaderTemplate><ItemTemplate>
                        <input type="checkbox" id="chkItem" />
                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
                        <a class="link tooltip" title='<%# Eval("VersionName") %>' onclick="lookApply('<%# Eval("ProgressVersionId") %>','<%# StringUtility.GetStr(Eval("VersionName"), 10) %>')">
                            <%# StringUtility.GetStr(Eval("VersionName"), 25) %></a>
                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="执行" DataField="Latest" HeaderStyle-Width="30px" /><asp:BoundField HeaderText="主计划" DataField="Main" HeaderStyle-Width="30px" /><asp:TemplateField HeaderText="原计划名称" HeaderStyle-Width="320px"><ItemTemplate>
                        <a class="link tooltip" title='<%# Eval("PVersionName") %>' onclick="lookApply('<%# Eval("ParentVerId") %>','<%# StringUtility.GetStr(Eval("PVersionName"), 10) %>')">
                            <%# StringUtility.GetStr(Eval("PVersionName"), 25) %></a>
                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="原版本号" DataField="PVersionCode" /><asp:BoundField HeaderText="申请人" DataField="UserName" /><asp:BoundField HeaderText="录入日期" HeaderStyle-Width="80px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
