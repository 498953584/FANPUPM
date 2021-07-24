<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostIndirectOrgList.aspx.cs" Inherits="BudgetManage_Cost_CostIndirectOrgList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>间接成本汇总表</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var JwTable = new JustWinTable('gvCostInOrgList');
            replaceEmptyTable('emptyCostInOrgList', 'gvCostInOrgList');
        });
        //选择部门
        function selDept() {
            jw.selectOneDep({ idinput: 'hflDepCode', nameinput: 'txtDepmentName' });
        }
        //选择人员
        function selUser() {
            jw.selectOneUser({ idinput: 'hflUserCode', nameinput: 'txtUserName' });
        }
        function validateCheck() {
            if ($('#txtInputDate').val() == "") {
                top.ui.alert("年份月份必须输入！");
                return false;
            }
            return true;
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
                            人员
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px">
                                <input name="txtUserName" type="text" id="txtUserName" style="width: 97px;
                                    height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                <img alt="选择签订单位" onclick="selUser();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            <input name="hflUserCode" type="hidden" id="hflUserCode" runat="server" />

                        </td>
                        <td class="descTd">
                            年份月份
                        </td>
                        <td class="txt mustInput">
                            <asp:TextBox ID="txtInputDate" onclick="WdatePicker({dateFmt:'yyyy-MM'});" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClientClick="return validateCheck();" OnClick="btnSearch_Click" runat="server" />
                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div>
                    <asp:GridView ID="gvCostInOrgList" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" runat="server">
<EmptyDataTemplate>
                            <table id="emptyCostInOrgList" class="gvdata">
                                <tr class="header">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        部门
                                    </th>
                                    <th scope="col">
                                        姓名
                                    </th>
                                    <th scope="col">
                                        岗位
                                    </th>
                                    <th scope="col">
                                        成本编码
                                    </th>
                                    <th scope="col">
                                        成本名称
                                    </th>
                                    <th scope="col">
                                        选中年月
                                    </th>
                                    <th scope="col">
                                        月预报销金额
                                    </th>
                                    <th scope="col">
                                        月预报销合计
                                    </th>
                                    <th scope="col">
                                        月核销金额
                                    </th>
                                    <th scope="col">
                                        月核销合计
                                    </th>
                                    <th scope="col">
                                        选中年
                                    </th>
                                    <th scope="col">
                                        年预报销金额
                                    </th>
                                    <th scope="col">
                                        年预报销合计
                                    </th>
                                    <th scope="col">
                                        年核销金额
                                    </th>
                                    <th scope="col">
                                        年核销合计
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" DataField="Num" HeaderStyle-Width="25px" /><asp:BoundField HeaderText="部门" DataField="V_BMMC" /><asp:BoundField HeaderText="姓名" DataField="IssuedBy" /><asp:BoundField HeaderText="岗位" DataField="DutyName" /><asp:BoundField HeaderText="成本编码" DataField="CBSCode" /><asp:BoundField HeaderText="成本名称" DataField="CBSName" /><asp:TemplateField HeaderText="选中年月"><ItemTemplate>
                                    <%# GetTime(Eval("YearMonthdate")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="月预报销金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumItemMonthAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="月预报销合计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumMonthAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="月核销金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumItemMonthAuditAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="月核销合计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumMonthAuditAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="选中年" DataField="Yeardate" /><asp:TemplateField HeaderText="年预报销金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumItemYearAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="年预报销合计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumYearAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="年核销金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumItemYearAuditAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="年核销合计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("SumYearAuditAmount") %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
