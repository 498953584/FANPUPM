<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkPlanMange.aspx.cs" Inherits="oa_WorkPlanAndSummary_WorkPlanMange" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工作计划管理</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            addEvent(document.getElementById('btnAdd'), 'click', addPlan);
            addEvent(document.getElementById('btnEdit'), 'click', EditPlan);
            addEvent(document.getElementById('btnSumm'), 'click', WriteSumm);
            var jwtable = new JustWinTable('GvShowPlan');
            setButton(jwtable, 'btnDel', 'btnEdit', 'btnQuery', 'hdnwkpId');
            replaceEmptyTable('emptyShowPlan', 'GvShowPlan');
            showTooltip('tooltip', 15);
            setWfButtonState2(jwtable, 'WF1');
        });

        function addPlan() {
            top.ui._WorkPlanMange = window;
            var url = '/oa/WorkPlanAndSummary/AboutWPlan.aspx?Action=Add&planType=' + getRequestParam("planType");
            toolbox_oncommand(url, "新增工作计划");
        }
        function EditPlan() {
            top.ui._WorkPlanMange = window;
            var url = '/oa/WorkPlanAndSummary/AboutWPlan.aspx?Action=Edit&wkpid=' + document.getElementById('hdnwkpId').value + "&planType=" + getRequestParam("planType");
            toolbox_oncommand(url, "编辑工作计划");
        }
        function OpenViewDiv() {
            top.ui._WorkPlanMange = window;
            var url = "/oa/WorkPlanAndSummary/ExplainPlan.aspx?Action=View&wkpid=" + document.getElementById('hdnwkpId').value + "&planType=" + getRequestParam("planType");
            toolbox_oncommand(url, "查看工作计划");
        }
        function WriteSumm() {
            top.ui._WorkPlanMange = window;
            var IsWorE = document.getElementById('btnSumm').value == "写总结" ? "write" : "edit";
            var url = "/oa/WorkPlanAndSummary/SummPlan.aspx?WkpId=" + document.getElementById('hdnwkpId').value + "&Action=" + IsWorE + "&planType=" + getRequestParam("planType");
            toolbox_oncommand(url, "计划总结录入");
        }
        function rowClick(wkpid, IsOk, count) {
            document.getElementById("btnAdd").disabled = false;
            if (IsOk == "1") {
                if (count > 0) {
                    document.getElementById("btnSumm").disabled = false;
                    document.getElementById("btnSumm").value = "编辑总结";
                } else {
                    document.getElementById("btnSumm").disabled = false;
                    document.getElementById("btnSumm").value = "写总结";
                }
            } else {
                document.getElementById("btnSumm").disabled = true;
                document.getElementById("btnSumm").value = "写总结";
            }
            if (IsOk == "0" || IsOk == "-1") {
                document.getElementById("btnSumm").disabled = true;
                document.getElementById("btnSumm").value = "写总结";
            }
            document.getElementById('hdnwkpId').value = wkpid;
        }

        function openAdd() {
            var url = "";
        }
        function btnDel() {
            var dateCompare = document.getElementById('hdnTimeSpan').value;
            alert(dateCompare);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tab">
        <tr>
            <td class="divHeader">
                工作计划管理
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" class="divFooter">
                <input type="button" id="btnAdd" value="新增" />
                <input type="button" id="btnEdit" value="编辑" disabled="disabled" />
                <asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                <input type="button" id="btnQuery" value="查看" style="display: none;" />
                <input id="btnSumm" type="button" value="写总结" disabled="true" style="width: 80px;" runat="server" />

                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div class="pagediv">
                    <asp:GridView ID="GvShowPlan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" Width="100%" PageSize="25" OnRowDataBound="GvShowPlan_RowDataBound" OnPageIndexChanging="GvShowPlan_PageIndexChanging" DataKeyNames="WkpId,WkpIsReport" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" id="emptyShowPlan" cellspacing="0" rules="all" border="0" cellpadding="0"
                                style="border-collapse: collapse;">
                                <tr class="header">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        计划编号
                                    </th>
                                    <th>
                                        填报人
                                    </th>
                                    <th>
                                        填报人部门
                                    </th>
                                    <th>
                                        计划类型
                                    </th>
                                    <th>
                                        上报状态
                                    </th>
                                    <th>
                                        是否合格
                                    </th>
                                    <th>
                                        填报日期
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="" Text='<%# Convert.ToString(Eval("wkpusercode")) %>' runat="server"></asp:HyperLink>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="填报人"><ItemTemplate>
                                    <%# BackUserName(Eval("WkpReportUser").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="填报人部门"><ItemTemplate>
                                    <%# BackDept(Eval("WkpDeptId").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
                                    <%# Common2.GetStateNullString(Eval("WkpIsTrue").ToString()) %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="填报日期"><ItemTemplate>
                                    <center>
                                        <%# DateTime.Parse(Eval("WkpRecordDate").ToString()).ToShortDateString() %></center>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><SelectedRowStyle BackColor="Red"></SelectedRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input id="hdnwkpId" type="hidden" runat="server" />

    <input id="hdnTimeSpan" type="hidden" runat="server" />

    </form>
</body>
</html>
