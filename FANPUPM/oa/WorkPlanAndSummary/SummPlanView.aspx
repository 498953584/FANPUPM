<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummPlanView.aspx.cs" Inherits="oa_WorkPlanAndSummary_SummPlanView" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            addEvent(document.getElementById('btnCheck'), 'click', CheckPage);
            var jwtable = new JustWinTable('GvShowPlan');
            replaceEmptyTable('emptyShowPlan', 'GvShowPlan');
        });
        function CheckPage() {
            parent.desktop.CheckPlan = window;
            var url = '/oa/WorkPlanAndSummary/CheckPlan.aspx?WkpId=' + document.getElementById('hdnWkpId').value;
            toolbox_oncommand(url, "工作计划审核");
        }
        function OpenViewDiv() {
            parent.desktop.AboutWPlan = window;
            var url = "/oa/WorkPlanAndSummary/ExplainPlan.aspx?Action=View&wkpid=" + document.getElementById('hdnWkpId').value;
            toolbox_oncommand(url, "查看工作计划");
        }
        function rowClick(result, WkpId) {
            if (result != "1") {
                document.getElementById('btnBack').disabled = true;
                document.getElementById('btnPass').disabled = true;
            } else {
                document.getElementById('btnBack').disabled = false;
                document.getElementById('btnPass').disabled = false;
                document.getElementById('hdnWkpId').value = WkpId;
            }
            //alert(document.getElementById('hdnWkpId').value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="divHeader">
                <asp:Label ID="LblHeader" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style=" height:1px;">
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                           填报人
                        </td>
                        <td>
                            <asp:TextBox ID="txtWkpReportUser" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            填报日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtWkpRecordDate" onclick="WdatePicker({isShowClear: false})" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            计划类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlanType" Width="120px" runat="server"><asp:ListItem Value="" /><asp:ListItem Value="0" Text="周计划" /><asp:ListItem Value="1" Text="月计划" /></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr_audit" runat="server"><td style="text-align: left;" class="divFooter" runat="server">
                <input type="button" id="btnCheck" value="审核" style="display: none;" />
                <input type="button" id="btnPass" value="通过" disabled="true" OnServerClick="btnPass_Click" runat="server" />

                <input type="button" id="btnBack" value="驳回" disabled="true" OnServerClick="btnBack_Click" runat="server" />

                <input type="button" id="btnComment" value="点评" disabled="disabled" style="display: none;" />
            </td></tr>
        <tr>
            <td valign="top">
                <div class="pagediv">
                    <asp:GridView ID="GvShowPlan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" Width="100%" PageSize="15" OnRowDataBound="GvShowPlan_RowDataBound" OnPageIndexChanging="GvShowPlan_PageIndexChanging" DataKeyNames="WkpId,WkpIsReport" runat="server">
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
                                    <%# new MainPlanAndAction().BackUserName(Eval("wkpreportuser").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="填报人部门"><ItemTemplate>
                                    <%# new MainPlanAndAction().BackDept(Eval("WkpDeptId").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划类型"><ItemTemplate>
                                    <%# (Eval("WkpReportType").ToString() == "0") ? "周计划" : "月计划" %>
                                </ItemTemplate><ItemTemplate>
                                    <%# (Eval("WkpReportType").ToString() == "0") ? "周计划" : "月计划" %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
                                    <%# Common2.GetStateNullString(Eval("WkpIsTrue").ToString()) %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="填报日期"><ItemTemplate>
                                    <center>
                                        <%# DateTime.Parse(Eval("WkpRecordDate").ToString()).ToShortDateString() %></center>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input id="hdnWkpId" type="hidden" runat="server" />

    <input id="hdnname" type="hidden" runat="server" />

    </form>
</body>
</html>
