<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BCorpNamePeport.aspx.cs" Inherits="ContractManage_ContractReport_BCorpNamePeport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var gvwContract = new JustWinTable('gvwContract');
            replaceEmptyTable('emptyContract', 'gvwContract');
            showTooltip('tooltip', 10);
            // 添加验证
            $('#btnSearch')[0].onclick = function () {
                if (!$('#form1').form('validate')) {
                    return false;
                }
            }
        });

        function viewopen_n(Bname, CorpId) {
            parent.desktop.ContractSumReport = window;
            var url = "/ContractManage/ContractReport/ContractSumReport.aspx?bc=081&bcl=001&ic=" + CorpId;
            toolbox_oncommand(url, Bname + "明细表");
        }
        function pickCorp() {
            jw.selectOneCorp({ idinput: 'hfldBName', nameinput: 'txtBName' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        
        <tr>
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" align="center">
                            供应商名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtBName" Style="width: 120px;" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="pickCorp" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hfldBName" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv" style="width: 100%">
                    <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="CorpName" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContract" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 5%;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        供应商名称
                                    </th>
                                    <th scope="col">
                                        合同金额
                                    </th>
                                    <th scope="col">
                                        累计结算金额
                                    </th>
                                    <th scope="col">
                                        累计已付款金额
                                    </th>
                                    <th scope="col">
                                        应付未付金额
                                    </th>
                                    <th scope="col">
                                        已提供发票金额
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" DataField="Num" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="供应商名称" HeaderStyle-Width="20%"><ItemTemplate>
                                    <span class="link tooltip" title='<%# Eval("CorpName").ToString() %>' onclick="viewopen_n ('<%# Eval("CorpName") %>',<%# Eval("CorpId") %>)">
                                        <%# StringUtility.GetStr(Eval("CorpName").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="合同金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="15%" /><asp:BoundField DataField="BalanceMoney" ItemStyle-HorizontalAlign="Right" HeaderText="累计结算金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="15%" /><asp:BoundField DataField="PaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="累计已付款金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="15%" /><asp:BoundField DataField="NoPaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="应付未付金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="15%" /><asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderText="已提供发票金额" ItemStyle-CssClass="decimal_input" ItemStyle-Width="15%" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
