<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractSumReport.aspx.cs" Inherits="ContractManage_ContractReport_ContractSumReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#btnSearch')[0].onclick = function () {
                if (!$('#form1').form('validate')) {
                    return false;
                }
            }
            showTooltip('tooltip', 10);
        })

        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtProject' });
        }

        // 选择合同类型
        function selectConType() {
            jw.selectOneConType({ nameinput: 'txtConType' });
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
                            合同编码:
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" align="center">
                            &nbsp;合同名称:
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
							&nbsp;项目名称:
						</td>
						<td>
							<asp:TextBox ID="txtProject" Width="120px" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="openProjPicker" runat="server"></asp:TextBox>
						</td>
                        <td class="descTd">
							&nbsp;合同类型:
						</td>
						<td>
							<asp:TextBox ID="txtConType" Width="120px" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="selectConType" runat="server"></asp:TextBox>
						</td>
                    </tr>
                    <tr>
                        <td class="descTd">
							签约时间:
						</td>
                        <td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							&nbsp;至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
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
                    <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractCode,IsMainContract" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContract" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 5%">
                                        序号
                                    </th>
                                    <th scope="col">
                                        合同编号
                                    </th>
                                    <th scope="col">
                                        合同名称
                                    </th>
                                    <th scope="col">
                                        项目名称
                                    </th>
                                    <th scope="col">
                                        签订时间
                                    </th>
                                    <th scope="col">
                                        合同类型
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
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="5%"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" ItemStyle-HorizontalAlign="Left" HeaderText="合同编号" ItemStyle-Width="10%" /><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="100px"><ItemTemplate>
                                    <span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
                                        <%# StringUtility.GetStr(Eval("ContractName").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PrjName" ItemStyle-HorizontalAlign="Left" HeaderText="项目名称" ItemStyle-Width="8%" /><asp:TemplateField HeaderText="签订时间" HeaderStyle-Width="9%"><ItemTemplate>
                                    <%# string.Format("{0:d}", Eval("SignDate")) %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="TypeName" ItemStyle-HorizontalAlign="Left" HeaderText="合同类型" ItemStyle-Width="8%" /><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="合同金额" ItemStyle-Width="10%" /><asp:BoundField DataField="BalanceMoney" ItemStyle-HorizontalAlign="Right" HeaderText="累计结算金额" ItemStyle-Width="10%" /><asp:BoundField DataField="PaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="累计已付款金额" ItemStyle-Width="10%" /><asp:BoundField DataField="NoPaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="应付未付金额" ItemStyle-Width="10%" /><asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderText="已提供发票金额" ItemStyle-Width="10%" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
