<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutModifyEdit.aspx.cs" Inherits="ContractManage_PayoutModify_PayoutModifyEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同变更</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyContractType', 'gvwContract');
            var contractTable = new JustWinTable('gvwContract');
            if (document.getElementById("hdContractID").value == "") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
                document.getElementById("btnQuery").setAttribute('disabled', 'disabled');
            }
            else {
                setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldModifyId');
                setWfButtonState2(contractTable, 'WF1');
                //document.getElementById("btnAdd").removeAttribute('disabled');               
            }
            registerAddEvent();
            registerDeleteEvent();
            registerUpdateEvent();
            registerQueryEvent();
        });

        function registerAddEvent() {
            var btnAdd = document.getElementById('btnAdd');
            addEvent(btnAdd, 'click', function () {
                var contractId = document.getElementById('hdContractID').value;
                top.ui.ModifyEdit = window;
                var url = '/ContractManage/PayoutModify/ModifyEditTest.aspx?Action=Add&ContractId=' + contractId + '&Random=' + new Date().getTime();
                toolbox_oncommand(url, "新增支出合同变更");
            });
        }

        function registerDeleteEvent() {
            var btnDelete = document.getElementById('btnDelete');
            btnDelete.onclick = function () {
                if (!confirm("确定要删除吗?")) {
                    return false;
                }
            }
        }

        function registerUpdateEvent() {
            var btnUpdate = document.getElementById('btnUpdate');
            addEvent(btnUpdate, 'click', function () {
                var contractId = document.getElementById('hdContractID').value;
                var modifyId = document.getElementById('hfldModifyId').value;
                top.ui.ModifyEdit = window;
                var url = '/ContractManage/PayoutModify/ModifyEditTest.aspx?Action=Update&ContractId=' + contractId + '&ModifyId=' + modifyId + '&Random=' + new Date().getTime();
                toolbox_oncommand(url, "编辑支出合同变更");

            });
        }

        function registerQueryEvent() {
            var btnQuery = document.getElementById('btnQuery');
            addEvent(btnQuery, 'click', function () {
                var modifyId = document.getElementById('hfldModifyId').value;
                parent.parent.desktop.ModifyEdit = window;
                //                var url = '/ContractManage/PayoutModify/ModifyQuery.aspx?Action=Query&ContractId=' + contractId + '&ModifyId=' + modifyId + '&ic=' + modifyId + '&Random=' + new Date().getTime();
                //                toolbox_oncommand(url, "查看支出合同变更");
                rowQueryA(modifyId);
            });
        }

        //查看支出合同变更
        function rowQueryA(modifyId) {
            //parent.parent.desktop.ModifyEdit = window;
            var contractId = document.getElementById('hdContractID').value;
            var path = '/ContractManage/PayoutModify/ModifyQuery.aspx?Action=Query&ContractId=' + contractId + '&ModifyId=' + modifyId + '&ic=' + modifyId + '&Random=' + new Date().getTime();
            //toolbox_oncommand(path, "查看支出合同变更");
            viewopen(path, "查看支出合同变更");
        }
        //审核通过的变更不能进行修改，是在bud_Task 表中进行汇总
        function upAdminPrivilege() {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="vertical-align: top;">
            <td class="divFooter" style="text-align: left;">
                
                <asp:Button ID="btnAdd" Enabled="false" Text="新增" runat="server" />
                <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="082" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ModifyID,ModifyCode" runat="server">
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
                                        流程状态
                                    </th>
                                    <th scope="col">
                                        附件
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更编号"><ItemTemplate>
                                    <span class="link" onclick="rowQueryA('<%# Eval("ModifyID") %>')">
                                        <%# Eval("ModifyCode") %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("ModifyDate").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifyMoney" ItemStyle-HorizontalAlign="Right" HeaderText="变更金额" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Reason" HeaderText="变更原因" /><asp:BoundField DataField="ModifyPerson" HeaderText="办理人" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                    <%# GetAnnx(Convert.ToString(Eval("ModifyID"))) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdContractID" runat="server" />
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    </form>
</body>
</html>
