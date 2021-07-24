<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestPaymentView.aspx.cs" Inherits="Fund_RequestPayment_RequestPaymentView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>请款计划明细</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window,'load',function(){
            setAnnxReadOnly('flAnnx');
            replaceEmptyTable('emptyPlan', 'gvwPlan');
        });
    </script>

</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                请款计划明细
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblPrintPeople" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            请款单编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblcode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProject" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            请款人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lbluser" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            请款日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="DateInTime" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd" colspan="3">
                            <MyUserControl:epc_usercontrol1_filelink_ascx ID="flAnnx" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblExplain" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr style="vertical-align: top">
                        <td style="text-align: right">
                            <div id="divBudget" class="pagediv">
                                <asp:GridView ID="gvwPlan" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPlan_RowDataBound" DataKeyNames="RPayuid" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyPlan" class="tab">
                                            <tr class="header">
                                                <th scope="col" style="width: 25px;">
                                                    序号 </td>
                                                <th scope="col">
                                                    所属计划
                                                </th>
                                                <th scope="col">
                                                    依据合同
                                                </th>
                                                <th scope="col">
                                                    所属科目
                                                </th>
                                                <th scope="col">
                                                    计划额度
                                                </th>
                                                 <th scope="col">
                                                    已用额度
                                                </th>
                                                <th scope="col">
                                                    可用额度
                                                </th>
                                                 <th scope="col">
                                                    请款金额
                                                </th>
                                                <th scope="col">
                                                    经手人
                                                </th>
                                                <th scope="col">
                                                    备注
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="所属计划"><ItemTemplate>
                                                <asp:Label ID="lblPlanName" Text='<%# Convert.ToString(Eval("PlanName")) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="依据合同">
<ItemTemplate>
                                                <asp:Label ID="lblCont" runat="server"><%# Convert.ToString(Eval("ContractOutName")) %></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="所属科目">
<ItemTemplate>
                                                <asp:Label ID="lblsubject" runat="server"><%# Convert.ToString(Eval("RPaysubject")) %></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划额度"><ItemTemplate>
                                                <asp:Label ID="lblBalance" Text='<%# Convert.ToString(Eval("ThisBalance")) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已用额度"><ItemTemplate>
                                                <asp:Label ID="lblAllowMoney" Text='<%# Convert.ToString(Eval("AllowMoney")) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="可用额度">
<ItemTemplate>
                                                <asp:Label ID="lblSurplus" Text="" runat="server"></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="请款金额"><ItemTemplate>
                                                <asp:Label ID="lblRPMoney" Text='<%# Convert.ToString(Eval("RPayMoney")) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人">
<ItemTemplate>
                                                <asp:Label ID="lblPerson" runat="server"><%# Convert.ToString(Eval("RPayUser")) %></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                                <asp:Label ID="lblremark" runat="server"><%# Convert.ToString(Eval("ReMark")) %></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="RPayUID" Visible="false"><ItemTemplate>
                                                <asp:Label ID="lbluid" Text='<%# Convert.ToString(Eval("RPayUID")) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="092" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
                <input id="hdnCode" type="hidden" runat="server" />

            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
