<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometContractQuery.aspx.cs" Inherits="ContractManage_IncometContract_IncometContractQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
        table tr
        {
            border-color: Black;
            background-color: Black;
        }
        .fontsize
        {
            font-size: 13px;
        }
          .viewTable
        {
            text-align:center;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            if (document.getElementById('hldfIsExamineApprove').value == '0') {
                document.getElementById('trAudit').style.display = 'none';
            }
            var table = new JustWinTable('gvBudget');
            replaceEmptyTable('emptyBudget', 'gvBudget');
        });
    </script>
    <style type="text/css">
        .style1
        {
            height: 29px;
        }
        .style2
        {
            height: 338px;
        }
        .style3
        {
            height: 149px;
        }
    </style>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align:top;">
        <tr>
            <td class="divHeader">
                收入合同
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
					value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none; text-align:left; margin-left:60px">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
               <table class="viewTable" style="height: 100%; " cellpadding="0px" 
                    cellspacing="0">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            甲方
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtAName" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            乙方
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtBName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            丙方
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtCParty" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            返还日期
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtRefundDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            合同编号
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtContractCode" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            项目名称
                        </td>
                        <td class="elemTd" style="padding-right: 3px;">
                            <asp:Literal ID="txtProject" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            资金来源
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="lblPrjFundInfo" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            落实情况
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="lblPrjFundWorkable" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            工程质量要求
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="lblQualityClass" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            预测利润率
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="lblForecastProfitRate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            项目类型
                        </td>
                        <td>
                            <asp:Literal ID="lblPrjType" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            合同类型
                        </td>
                        <td class="elemTd" style="padding-right: 1px;">
                            <asp:Literal ID="contractType" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    
                    <tr>
                        
                        <td class="descTd" style="white-space: nowrap;">
                            合同名称
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtContractName" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            合同金额
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtContractMoney" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            结算方式
                        </td>
                        <td class="elemTd" style="padding-right: 1px;">
                            <asp:Literal ID="txtBalanceMode" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            付款方式
                        </td>
                        <td class="elemTd" style="padding-right: 1px;">
                            <asp:Literal ID="txtdropPayMode" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            开始日期
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtStartDate" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            结束日期
                        </td>
                        <td class="elemTd" style="padding-right: 1px;">
                            <asp:Literal ID="txtEndDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            签约日期
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtSignDate" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            合同状态
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtConState" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            质保期
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtQualityPeriod" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            签订人
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtSignPeople" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            签约地点
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Literal ID="txtAddress" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            收款条件
                        </td>
                        <td colspan="3">
                            <asp:Literal ID="txtCllectionCondition" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            主要条款
                        </td>
                        <td colspan="3">
                            <asp:Literal ID="txtMainItem" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Literal ID="txtNotes" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            附件
                        </td>
                        <td colspan="3">
                            <asp:Literal ID="ltlAnnex" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trPurchase" style="vertical-align: top; height: 1px;">
            <td style="vertical-align: top; padding-top: 10px;">
                <div style="font-size: 13px; font-weight: bold; text-align: center;">
                    <asp:Label ID="lblTitalbudContract" Font-Size="13px" Font-Bold="true" runat="server"></asp:Label></div>
                <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="TaskId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyBudget" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            任务名称
                                        </th>
                                        <th scope="col">
                                            任务编码
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
                                            工期（天）
                                        </th>
                                        <th scope="col">
                                            综合单价
                                        </th>
                                        <th scope="col">
                                            小计
                                        </th>

                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务名称">
<ItemTemplate>
                                        <%# Eval("TaskName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务编码">
<ItemTemplate>
                                        <%# Eval("TaskCode") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="类型">
<ItemTemplate>
                                        <%# Eval("TypeName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
                                        <%# Eval("Unit") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工程量">
<ItemTemplate>
                                        <%# Eval("Quantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间">
<ItemTemplate>
                                        <%# Common2.GetTime(Eval("StartDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="结束时间">
<ItemTemplate>
                                        <%# Common2.GetTime(Eval("EndDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工期">
<ItemTemplate>
                                        <%# Eval("ConstructionPeriod") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价">
<ItemTemplate>
                                        <%# Eval("UnitPrice") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计">
<ItemTemplate>
                                        <%# Eval("Total") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top; margin-left:170px">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="103" BusiClass="001" runat="server" />
            </td>
        </tr>
        </table>
    <asp:HiddenField ID="hfldWantPlanGuid" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
