<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutModify.aspx.cs" Inherits="ContractManage_PayoutModify_PayoutModify" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同变更</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.extension.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyContractType', 'gvwContract');
            var contractTable = new JustWinTable('gvwContract');
        });
        //查看支出合同变更
        function rowQueryA(modifyId) {
            //parent.parent.desktop.ModifyEdit = window;
            var contractId = document.getElementById('hdContractID').value;
            var path = '/ContractManage/PayoutModify/ModifyQuery.aspx?Action=Query&ContractId=' + contractId + '&ModifyId=' + modifyId + '&ic=' + modifyId + '&Random=' + new Date().getTime();
            //toolbox_oncommand(path, "查看支出合同变更");
            viewopen(path, "查看支出合同变更");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv">
        <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" DataKeyNames="ModifyID" runat="server">
<EmptyDataTemplate>
                <table id="emptyContractType" class="gvdata">
                    <tr class="header">
                        <th scope="col" style="width: 20px;">
                            <input id="chk1" type="checkbox" />
                        </th>
                        <th scope="col" style="width: 25px;">
                            序号
                        </th>
                        <th scope="col">
                            变更编号
                        </th>
                        <th scope="col">
                            变更时间
                        </th>
                        <th scope="col">
                            变更金额
                        </th>
                        <th scope="col">
                            变更原因
                        </th>
                        <th scope="col">
                            办理人
                        </th>
                        <th scope="col">
                            附件
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更编号"><ItemTemplate>
                        <span class="link" onclick="rowQueryA('<%# Eval("ModifyID") %>')">
                            <%# Eval("ModifyCode") %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更时间"><ItemTemplate>
                        <%# Common2.GetTime(Eval("ModifyDate").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifyMoney" ItemStyle-HorizontalAlign="Right" HeaderText="变更金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="变更原因"><ItemTemplate>
                        <span class="tooltip" title='<%# Eval("Reason").ToString() %>'>
                            <%# StringUtility.GetStr(Eval("Reason").ToString(), 35) %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="办理人"><ItemTemplate>
                        <span class="tooltip" title='<%# Eval("ModifyPerson").ToString() %>'>
                            <%# StringUtility.GetStr(Eval("ModifyPerson").ToString(), 35) %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                        <%# GetAnnx(Convert.ToString(Eval("ModifyID"))) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <asp:HiddenField ID="hdContractID" runat="server" />
    </form>
</body>
</html>
