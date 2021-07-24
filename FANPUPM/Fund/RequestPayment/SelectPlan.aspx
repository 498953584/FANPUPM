<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPlan.aspx.cs" Inherits="Fund_RequestPayment_SelectPlan" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划选择</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            replaceEmptyTable('emptyContractType', 'gvwWebLineList');
            var jwTable = new JustWinTable('gvwWebLineList');
            
            $('#btnSave').click(function() {
//                var plan = new Array();
//                $('#gvwWebLineList tr[id]').each(function() {
//                    plan.push(this.id);
//                });
//                $(parent.document).find('#hdnUID').val(array1dToJson(plan));
                $(parent.document).find('#hdnUID').val(jwTable.getCheckedChkIdJson(jwTable.getCheckedChk()));
                $(parent.document).find('.ui-icon-closethick').each(function() {
                    this.click();
                });
                parent.document.getElementById('btnPlan').click();
            });

            //取消按钮事件
            $('#btnCancel').click(function() {
                $(parent.document).find('.ui-icon-closethick').each(function() {
                    this.click();
                });
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">

    <div class="pagediv"  style="height: 95%; overflow-y:auto;">
        <asp:GridView ID="gvwWebLineList" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
                <table id="emptyContractType" class="gvdata" width="100%">
                    <tr class="header">
                        <th scope="col" style="width: 25px;">
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </th>
                        <th scope="col" style="width: 25px;">
                            序号
                        </th>
                        <th scope="col" style="width: 80px;">
                            依据合同
                        </th>
                        <th scope="col" style="width: 80px;">
                            所属科目
                        </th>
                        <th scope="col" style="width: 80px;">
                            计划额度
                        </th>
                        <th scope="col" style="width: 80px;">
                            已用额度
                        </th>
                        <th scope="col" style="width: 80px;">
                            可用额度
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" />
                    </HeaderTemplate>

<ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="依据合同" DataField="contractOutName" /><asp:BoundField HeaderText="依据合同" DataField="contractInName" /><asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" HeaderText="所属科目" DataField="plansubject" /><asp:BoundField HeaderText="计划额度" DataField="thisBalance" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" /><asp:BoundField HeaderText="已用额度" DataField="RPayMoney" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" /><asp:TemplateField HeaderText="可用额度" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                        <%# Eval("PlanMoney") %>
                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </div>
    <div style="text-align: right">
        <input type="button" id="btnSave" value="保存" runat="server" />

        <input type="button" id="btnCancel" value="取消" />
    </div>
    </form>
</body>
</html>
