<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometBudget.aspx.cs" Inherits="ContractManage_IncometContract_IncometBudget" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var wintable = new JustWinTable('gvBudget');
            replaceEmptyTable('emptygvBudget', 'gvBudget');

            wintable.registClickTrListener(function () {
                $('#hfldCheckedId').val($(this).attr('id'));
                $('#btnOk').attr('disabled', false);
            });

            wintable.registDbClickListener(function () {
                btnok_click();
            });
            jw.formatTreegrid('gvBudget', 1);
        });

        function btnok_click() {
            if (typeof top.ui.callback == 'function') {
                var taskid = $('#hfldCheckedId').val();
                top.ui.callback({ taskId: $('#hfldCheckedId').val() });
                top.ui.callback == null;
            }
            top.ui.closeTab();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <div id="divBudget" class="pagediv">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvContractBudget_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server">
<EmptyDataTemplate>
                    <table id="emptygvBudget" class="gvdata">
                        <tr class="header">
                            <th scope="col" style="width: 20px;">
                                <input id="chk1" type="checkbox" />
                            </th>
                            <th scope="col" style="width: 25px;">
                                序号
                            </th>
                            <th scope="col">
                                名称
                            </th>
                            <th scope="col">
                                编码
                            </th>
                            <th scope="col">
                                类型
                            </th>
                            <th scope="col">
                                单位
                            </th>
                            <th scope="col">
                                工程量
                            </th>
                            <th scope="col">
                                开始时间
                            </th>
                            <th scope="col">
                                结束时间
                            </th>
                            <th scope="col">
                                工程工期（天）
                            </th>
                            <th scope="col">
                                综合单价
                            </th>
                            <th scope="col">
                                小计
                            </th>
                            <th scope="col">
                                备注
                            </th>
                            <td id="Th1" scope="col" visible="false" runat="server">
                                排序
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# Eval("TaskName") %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
                            <%# Eval("TaskCode") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Eval("TypeName") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Eval("Unit") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# Eval("Quantity") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("StartDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("EndDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程工期" HeaderStyle-Width="70px"><ItemTemplate>
                            <%# Eval("ConstructionPeriod") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                            <asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
                            <%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
    </div>
    <div id="divFooter2" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input type="button" id="btnOk" value="保存" onclick="btnok_click()" disabled="disabled" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldCheckedId" runat="server" />
    </form>
</body>
</html>
