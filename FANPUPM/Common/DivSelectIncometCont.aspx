<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectIncometCont.aspx.cs" Inherits="Common_DivSelectIncometCont" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同列表</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/json2.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvConract');
            jw.tooltip();
        });

        function saveEvent() {
            if (typeof top.ui.callback == 'function') {
                var con = JSON.parse($('#hfldConInfo').val());
                top.ui.callback(con);
                top.ui.callback == null;
            }
            top.ui.closeWin();
        }

        // TR单击事件
        function trClick(id, name, code, signDate, price) {
            var con = { id: id, name: name, code: code, signDate: signDate, price: price };
            $('#hfldConInfo').val(JSON.stringify(con));
        }
        // TR双击事件
        function trdbClick() {
            saveEvent();
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr style="height: 20px;" class="divHeader2">
            <td class="divHeader2">
                <asp:Label ID="lblTitle" Text="合同列表" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            合同编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            合同名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table class="tab">
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,ContractName,ContractCode,SignedTime,ContractPrice" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("ContractCode").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("ContractName").ToString() %>'>
                                                    <%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约日期"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignedTime").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                <%# WebUtil.GetEnPrice(Eval("ContractPrice").ToString(), Eval("ContractId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交底状态"><ItemTemplate>
                                                <%# GetFeedbackState(Eval("contractId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态"><ItemTemplate>
                                                <%# (Eval("sign").ToString() == "0") ? "未签订" : "已签订" %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="divFooter2" style="background-image: url();">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input type="button" id="btnSave" value="确定" onclick="saveEvent();" runat="server" />

                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdId" runat="server" />
    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hfldConInfo" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
