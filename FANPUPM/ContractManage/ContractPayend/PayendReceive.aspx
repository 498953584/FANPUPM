<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayendReceive.aspx.cs" Inherits="ContractManage_ContractPayend_PayendReceive" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>合同交底收件</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvConract');
            setButton(jwTable, 'btnRe', 'btnRe', 'btnLook', 'hfldPurchaseChecked');
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
            jwTable.registClickTrListener(function () {
                document.getElementById('hfldPurchaseChecked').value = this.id;
                if (this.getAttribute('guid')) {
                    document.getElementById('btnLook').guid = this.guid;
                }
                document.getElementById('btnDel').removeAttribute('disabled');
                document.getElementById('btnEdit').removeAttribute('disabled');
                document.getElementById('btnLook').removeAttribute('disabled');
                setDisabled(jwTable, this.id, 'btnEdit', 'btnDel');
            });
        });


        function rowEdit() {
            var url = "AddPayendFeedback.aspx?pid=" + document.getElementById("hfldPurchaseChecked").value;
            location = url;
        }
        function rowAdd() {
            var url = "AddContractPayend.aspx"
            location = url;
        }
        function rowQuery() {
            var url = "ViewPayendFeedback.aspx?t=1&pid=" + document.getElementById("hfldPurchaseChecked").value;
            location = url;
        }

        function setDisabled(jwTable, trId, btnUpdate, btnDel) {
            var flowstateIndex = getFlowIndex(jwTable, '反馈状态');
            if (flowstateIndex == 0)
                return;
            if (trId != '') {
                var cellVal = Trim(document.getElementById(trId).childNodes[flowstateIndex].innerText);
                if (cellVal == '已反馈') {
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <input type="button" id="btnAdd" value="新增交底信息" style="width: 100px; display: none;" />
                            <input type="button" id="btnEdit" disabled="disabled" style="width: 100px;" value="交底反馈" />
                            <asp:Button ID="btnDel" Text="删除反馈信息" style="display:none; width: 100px;" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" runat="server" />
                            <input type="button" id="btnLook" disabled="disabled" style="width: 100px;" value="查看反馈信息" />
                            <input type="button" id="btnRe" style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="PayendID" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交底人"><ItemTemplate>
                                                <%# PageHelper.QueryUser(this, Eval("WasPerson").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交底主题"><ItemTemplate>
                                                <a class="al" href="ViewPayendFeedback.aspx?t=1&pid=<%# Eval("payendId") %>">
                                                    <%# Eval("PayendTopics") %>
                                                </a>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="被交底人"><ItemTemplate>
                                                <%# GetBPerson(Eval("BWasPerson").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交底时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("InTime").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="反馈状态"><ItemTemplate>
                                                <%# GetFeedbackState(Eval("ContractId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
