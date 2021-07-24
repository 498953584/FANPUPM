<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ContractReportDetail.aspx.cs" Inherits="BudgetManage_Construct_ContractReportDetail" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>产值上报</title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwReport');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldRptId');
            //            alert(setWfButtonState2);
            setWfButtonState2(jwTable, 'WF1');
            jw.tooltip();
        });

        // 新增施工报量
        function add() {
            if ($('#hfldFlowState').val() != '1') {
                top.ui.alert('该项目的合同预算没有审核通过，不能进行产值上报');
                return false;
            }

            var prjId = getRequestParam('prjId');
            var url = '/BudgetManage/Construct/ContractReportTask.aspx?type=add&prjId=' + prjId;
            top.ui._ContractReportTask = window;
            toolbox_oncommand(url, "新增产值上报");
        }

        // 编辑施工报量
        function edit() {
            var prjId = getRequestParam('prjId');
            var url = '/BudgetManage/Construct/ContractReportTask.aspx?type=edit&prjId=' + prjId +
                '&rptId=' + $('#hfldRptId').val();
            top.ui._ContractReportTask = window;
            toolbox_oncommand(url, "编辑产值上报");
        }

        // 查看施工报量
        function query() {
            var prjId = getRequestParam('prjId');
            var url = '/BudgetManage/Construct/ContractReportTaskQuery.aspx?prjId=' + prjId +
                '&ic=' + $('#hfldRptId').val();
            toolbox_oncommand(url, "查看产值上报");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr id="divTop">
            <td class="divFooter" style="text-align: left; width: 100%;">
                <input type="button" id="btnAdd" value="新增" onclick="add();" />
                <input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="edit();" />
                <asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="query();" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="133" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 100%; vertical-align: top;">
                <div id="divBudget" class="pagediv" style="overflow:scroll ;">
                    <asp:GridView ID="gvwReport" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwReport_RowDataBound" DataKeyNames="RptId,PrjId,FlowState" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="chkAllBox" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="InputUser" HeaderText="记录人" HeaderStyle-Width="80px" /><asp:TemplateField HeaderText="上报日期" HeaderStyle-Width="80px"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="安全工作记录"><ItemTemplate>
                                    <span class="tooltip" title='<%# StringUtility.StripTagsCharArray(Eval("Note").ToString()) %>'>
                                        <%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("Note").ToString()), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px"><ItemTemplate>
                                    <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    <!-- 中标预算的审核状态-->
    <asp:HiddenField ID="hfldFlowState" runat="server" />
    <!-- 存储选中的施工报量ID-->
    <asp:HiddenField ID="hfldRptId" runat="server" />
    </form>
</body>
</html>
