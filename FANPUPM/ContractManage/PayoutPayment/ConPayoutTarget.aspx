<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConPayoutTarget.aspx.cs" Inherits="ContractManage_PayoutPayment_ConPayoutTarget" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>添加控制指标</title>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvBudget');
            setButton(jwTable, '', '', '', 'hfldCheckedIds');
            replaceEmptyTable('emptygvBudget', 'gvBudget');
            $('#btnCancel').click(function () {
                closeDiv();
            });
        });

        function saveData() {
            var targetIds = $('#hfldCheckedIds').val();
            if (typeof top.ui.callback == 'function') {
                top.ui.callback(targetIds);
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }

        function closeDiv() {
            top.ui.closeWin();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2" style="width: 100%; height: 90%;">
        <div id="divBudget" class="pagediv">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TargetId" runat="server">
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
                                编码
                            </th>
                            <th scope="col">
                                名称
                            </th>
                            <th scope="col" style="width: 80px;">
                                总金额
                            </th>
                            <th scope="col" style="width: 80px;">
                                合同签订金额
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                            <asp:CheckBox ID="cbAllBox" runat="server" />
                        </HeaderTemplate>

<ItemTemplate>
                            <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("TaskId")) %>' runat="server" />
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# Eval("TaskCode") %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# Eval("TaskName") %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="总金额" HeaderStyle-Width="80px">
<ItemTemplate>
                            <%# Eval("BudTotal") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同签订金额" HeaderStyle-Width="80px">
<ItemTemplate>
                            <%# Eval("SignAmount") %>
                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
    </div>
    <div id="divFooter2">
        <table style="width: 100%;">
            <tr>
                <td class="bottonrow2" style="height: 30px" align="right">
                    <input type="button" id="btnSave1" value="保存" onclick="saveData()" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldSendCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
