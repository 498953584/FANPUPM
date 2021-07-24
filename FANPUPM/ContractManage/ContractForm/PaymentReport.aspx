<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentReport.aspx.cs" Inherits="ContractManage_ContractForm_PaymentReport" %>
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
            replaceEmptyTable('emptyContract', 'gvwContract');
            var table = new JustWinTable('gvwContract');
            formateTable('gvCost', 2, true);
            showMoreName();
            // 添加验证
            $('#btn_Search')[0].onclick = function () {
                if (!$('#form1').form('validate')) {
                    return false;
                }
            }
        });

        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtPrjName' });
        }

        function IncometDetail(path) {
            parent.desktop.PayoutContractEdit = window;
            toolbox_oncommand(path, "查看收款列表");
        }
        function PayoutDetail(path) {
            parent.desktop.PayoutContractEdit = window;
            toolbox_oncommand(path, "查看付款列表");
        }
        //名称信息提示
        function showMoreName() {
            var tab = document.getElementById('gvwContract');
            if (tab != null) {
                for (i = 1; i < tab.rows.length - 1; i++) {
                    var cells = tab.rows[i].cells;
                    if (cells.length == 1) return;
                    if (cells[2].children.length == 0) return;
                    var imgId = cells[2].children[0].id;
                    var altLength = document.getElementById(imgId).title.length;
                    if (altLength > 25) {
                        $('#' + imgId).css('display', '');
                        $('#' + imgId).tooltip({
                            track: true,
                            delay: 0,
                            showURL: false,
                            fade: true,
                            showBody: " - ",
                            extraClass: "solid 1px blue",
                            fixPNG: true,
                            left: -200
                        });
                    } else {
                        document.getElementById(imgId).title = '';
                    }
                }
            }
        }

        // 选择合同类型
        function selectConType() {
            jw.selectOneConType({ nameinput: 'txtConType', idinput: 'hfldConType' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">
                                合同编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtContractCode" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                合同名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtContractName" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                合同类型
                            </td>
                            <td>
                                <asp:TextBox ID="txtConType" Width="120px" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="selectConType" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hfldConType" runat="server" />
                            </td>
                            <td class="descTd">
                                项目名称
                            </td>
                            <td>
                                <input type="text" style="width: 120px;" id="txtPrjName" class="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="openProjPicker" runat="server" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" class="tab">
                        <tr>
                            <td class="divFooter" style="text-align: left">
                                <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                                <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" valign="top">
                                <asp:GridView ID="gvwContract" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID" runat="server">
<EmptyDataTemplate>
                                        <table class="gvdata" cellspacing="0" id="emptyContract" rules="all" border="1" id="gvwContract"
                                            style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <th class="header">
                                                    序号
                                                </th>
                                                <th class="header">
                                                    合同编号
                                                </th>
                                                <th class="header">
                                                    合同名称
                                                </th>
                                                <th class="header">
                                                    合同金额
                                                </th>
                                                <th class="header">
                                                    收款累计
                                                </th>
                                                <th class="header">
                                                    挂靠付款累计
                                                </th>
                                                <th class="header">
                                                    差额
                                                </th>
                                                <th class="header">
                                                    合同类型
                                                </th>
                                                <th class="header">
                                                    项目名称
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Num").ToString()) %></ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编码" /><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="150px"><ItemTemplate>
                                                <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("ContractName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><span class="link" onclick="viewopen('/ContractManage/IncometContract/IncometContractQuery.aspx?ic=<%# Eval("ContractId") %>', '收入合同查看')">
              <%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %>
            </span></asp:HyperLink>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractAmount" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="收款累计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                <span class="link" onclick="IncometDetail('/ContractManage/IncometPayment/ViewPaymentList.aspx?ContractID=<%# Eval("ContractId") %>')">
                                                    <%# Eval("PayAmount") %></span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="挂靠付款累计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                <span class="link" onclick="PayoutDetail('/ContractManage/PaymentApply/ViewPaymentApplyList.aspx?contractID=<%# Eval("ContractId") %>&flowstate=1')">
                                                    <%# Eval("ApplyAmount") %></span>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="DiffAmount" HeaderText="差额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="TypeName" HeaderText="合同类型" /><asp:BoundField DataField="PrjName" HeaderText="项目名称" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
