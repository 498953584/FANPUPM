<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometInvoiceReport.aspx.cs" Inherits="ContractManage_ContractReport_IncometInvoiceReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同发票报表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
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
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvConract');
            formateTable('gvConract', 2, true);
            addPregressBar('percent');
            showTooltip('tooltip', 10);
            // 添加验证
            $('#btn_Search')[0].onclick = function () {
                if (!$('#form2').form('validate')) {
                    return false;
                }
            }
        });

        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtPrjName' });
        }

        // 往来单位
        function pickCorp() {
            jw.selectOneCorp({ idinput: 'hdParty', nameinput: 'txtParty' });
        }

        //选择人员
        function selectUser() {
            jw.selectOneUser({ codeinput: 'hfldSignPeople', nameinput: 'txtSignPeople' });
        }

        // 选择合同类型
        function selectConType() {
            jw.selectOneConType({ nameinput: 'txtConType' });
        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        
        <tr style="height: 30px;">
            <td style="height: 20px;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            合同类型
                        </td>
                        <td>
                            <asp:TextBox ID="txtConType" Width="120px" CssClass="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="selectConType" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td>
                            <input type="text" style="width: 120px;" id="txtPrjName" class="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="openProjPicker" runat="server" />

                        </td>
                        <td class="descTd">
                            合同编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            合同名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            签约时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            签订人
                        </td>
                        <td>
                            <input type="text" style="width: 120px;" id="txtSignPeople" class="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="selectUser" runat="server" />

                            <input id="hfldSignPeople" type="hidden" style="width: 1px" runat="server" />

                        </td>
                        <td class="descTd">
                            甲 方
                        </td>
                        <td>
                            <input type="text" title="双击选择甲方" style="width: 120px;" id="txtParty" class="select_input easyui-validatebox" data-options="validType:'validChars[50]'" imgclick="pickCorp" runat="server" />

                            <asp:HiddenField ID="hdParty" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            项目类型
                        </td>
                        <td>
                            <asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="bottonrow">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                            <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" valign="top">
                            <div class="" style="width: 1700px">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,isFContract" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Num").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
                                                <%# Eval("ContractCode") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen('/ContractManage/IncometContract/IncometContractQuery.aspx?ic=<%# Eval("ContractId") %>', '收入合同查看')">
                                                    <%# StringUtility.GetStr(Eval("ContractName").ToString(), 10) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# Eval("EndPrice") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                <asp:Label ID="Label1" ToolTip='<%# System.Convert.ToString(Eval("prjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(StringUtility.GetStr(Eval("prjName").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("prjTypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订人"><ItemTemplate>
                                                <%# Eval("SignPeopleName") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignedTime").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲方"><ItemTemplate>
                                                <asp:Label ID="Label1" ToolTip='<%# System.Convert.ToString(base.GetParty(Eval("Party").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(StringUtility.GetStr(base.GetParty(Eval("Party").ToString())), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发票累计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# Eval("InvoicePrice") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际发票累计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# WebUtil.SapRealContractMoney(Eval("ContractCode").ToString(), "A") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已开发票收款比率" ItemStyle-CssClass="percent" ItemStyle-Width="120px"><ItemTemplate>
                                                <%# WebUtil.GetInvoiceContrast(WebUtil.GetPaymentSum(Eval("ContractID").ToString(), "Con_Incomet_Payment", "CllectionPrice"), Eval("InvoicePrice").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
                                                <%# WebUtil.GetConState(Eval("conState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
