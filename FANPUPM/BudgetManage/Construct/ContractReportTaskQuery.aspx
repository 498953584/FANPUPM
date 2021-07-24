<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractReportTaskQuery.aspx.cs" Inherits="BudgetManage_Construct_ContractReportTaskQuery" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>中标预算施工报量</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwConsTask');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: auto;">
        <table style="border: 0px; width: 99.8%;">
            <tr>
                <td style="text-align: left;" class="divFooter">
                    项目名称：
                    <asp:Label ID="lblPrjName" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; 上报日期：
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; vertical-align: top;">
                    <div class="pagediv">
                        <asp:GridView ID="gvwConsTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwConsTask_RowDataBound" DataKeyNames="ConsTaskId" runat="server"><Columns><asp:TemplateField HeaderText="任务编码" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <%# Eval("ContractTask.TaskCode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分项工作 " ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <%# Eval("ContractTask.TaskName") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <%# Eval("ContractTask.Unit") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <span class="decimal_input total">
                                            <%# Eval("ContractTask.Total") %></span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申报量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <span class="decimal_input total">
                                            <%# Eval("Amount") %></span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Right"><HeaderTemplate>
                                        <span title="申报量 / 总工作量">完成百分比</span>
                                    </HeaderTemplate><ItemTemplate>
                                        <span class="decimal_input total">
                                            <%# CalcCompletePercent(Eval("Amount"), Eval("ContractTask.Total")) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="核准量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <span class="decimal_input total">
                                            <%# Eval("ApproveAmount") %></span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="形象进度" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <%# Eval("Note") %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding-top: 10px;">
                    <table cellspacing="0" cellpadding="0" border="1px" style="width: 100%; border-right: 0px; border-color:rgb(202,222,237)">
                        <tr>
                            <td class="td-label">
                                技术质量<br />
                                安全工作纪录：
                            </td>
                            <td class="td-content">
                                <textarea id="txtNode" readonly="readonly" cols="80" disabled="true" style="width: 100%;" runat="server"></textarea>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
