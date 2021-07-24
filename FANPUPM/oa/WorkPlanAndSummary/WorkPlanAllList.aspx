<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkPlanAllList.aspx.cs" Inherits="oa_WorkPlanAndSummary_WorkPlanAllList" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>周计划汇总一览表</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var JwTable = new JustWinTable('gvWorkPlanList');
            replaceEmptyTable('gvWorkPlanList', 'emptyWorkPlanList');
        });
        //选择部门
        function selDept() {
            jw.selectOneDep({ idinput: 'hflDepCode', nameinput: 'txtDepmentName' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            部门名称
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px">
                                <input name="txtDepmentName" type="text" id="txtDepmentName" style="width: 97px;
                                    height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                <img alt="选择签订单位" onclick="selDept();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            <input name="hflDepCode" type="hidden" id="hflDepCode" runat="server" />

                        </td>
                        <td class="descTd">
                            开始时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtWkplanDate" onclick="WdatePicker({firstDayOfWeek:1});" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div class="pagediv">
                    <asp:GridView ID="gvWorkPlanList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvWorkPlanList_RowDataBound" DataKeyNames="Num" runat="server">
<EmptyDataTemplate>
                            <table id="emptyWorkPlanList" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        部门
                                    </th>
                                    <th scope="col">
                                        人员
                                    </th>
                                    <th scope="col">
                                        岗位
                                    </th>
                                    <th scope="col">
                                        开始日期
                                    </th>
                                    <th scope="col">
                                        提交计数
                                    </th>
                                    <th scope="col">
                                        提交比率
                                    </th>
                                    <th scope="col">
                                        晚交计数
                                    </th>
                                    <th scope="col">
                                        晚交比率
                                    </th>
                                    <th scope="col">
                                        未提交计数
                                    </th>
                                    <th scope="col">
                                        未提交比率
                                    </th>
                                    <th scope="col">
                                        月份
                                    </th>
                                    <th scope="col">
                                        该月按时提交
                                    </th>
                                    <th scope="col">
                                        该月晚交
                                    </th>
                                    <th scope="col">
                                        该月份总比率
                                    </th>
                                    <th scope="col">
                                        该月前三个月
                                    </th>
                                    <th scope="col">
                                        按时提交
                                    </th>
                                    <th scope="col">
                                        晚交
                                    </th>
                                    <th scope="col">
                                        该月前三个月提交比率
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Eval("Num") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="部门"><ItemTemplate>
                                <span class="tooltip" title='<%# Eval("V_BMMC") %>'>
                                    <%# StringUtility.GetStr(Eval("V_BMMC"), 15) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="人员" DataField="v_xm" /><asp:BoundField HeaderText="岗位" DataField="DutyName" /><asp:TemplateField HeaderText="开始时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("WkpRecordDate")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="提交计数"><ItemTemplate>
                                    <%# (Eval("IsCommit").ToString() == "0") ? string.Empty : Eval("IsCommit").ToString() %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="提交比率"><ItemTemplate>
                                    <%# (Eval("IsCommitRate") != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(Eval("IsCommitRate")), 5)) : string.Empty %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="晚交计数"><ItemTemplate>
                                    <%# (Eval("LateCommit").ToString() == "0") ? string.Empty : Eval("LateCommit").ToString() %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="晚交比率"><ItemTemplate>
                                    <%# (Eval("LateCommitRate") != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(Eval("LateCommitRate")), 5)) : string.Empty %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="未提交计数"><ItemTemplate>
                                    <%# (Eval("NoCommit").ToString() == "0") ? string.Empty : Eval("NoCommit").ToString() %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="未提交比率"><ItemTemplate>
                                    <%# (Eval("CommitRate") != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(Eval("CommitRate")), 5)) : string.Empty %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="月份" DataField="WkpMonth" /><asp:BoundField HeaderText="该月按时提交" DataField="MonthCommit" /><asp:BoundField HeaderText="该月晚交" DataField="MonthLate" /><asp:TemplateField HeaderText="该月份总比率"><ItemTemplate>
                                    <%# (Eval("MonthRate") != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(Eval("MonthRate")), 5)) : string.Empty %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="该月前三个月" DataField="DuringMonths" /><asp:BoundField HeaderText="按时提交" DataField="TreeMonthCommit" /><asp:BoundField HeaderText="晚交" DataField="TreeMonthLate" /><asp:TemplateField HeaderText="该月前三个月提交比率"><ItemTemplate>
                                    <%# (Eval("TreeMonthRate") != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(Eval("TreeMonthRate")), 5)) : string.Empty %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
