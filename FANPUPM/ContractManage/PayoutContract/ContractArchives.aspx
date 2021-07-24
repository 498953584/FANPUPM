<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractArchives.aspx.cs" Inherits="ContractManage_ContractExecution_ContractArchives" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同档案</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/tab.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyContractType', 'gvwContract');
            formateTable('gvwContract', 2);
            var contractTable = new JustWinTable('gvwContract');
            showTooltip('tooltip', 20);
            //控制iframe
            setSp('hfldContractId', 'Module=Archives');
            contractTable.registClickTrListener(function () {
                document.getElementById("hfldContractId").value = this.id;
                setbkImg('spPayoutModify', 'hfldContractId', 'Module=Archives');
            });

            //trFrame 为存放Frame的TR
            //必需将整个Table的高度设置为100%，且第二个TR的高度为1px
            var trWidth = document.getElementById('trFrame').offsetHeight;
            document.getElementById('framContract').style.height = (trWidth - 34) + 'px';
        });

        function Change(SCheckBox) {
            var objs = document.getElementsByTagName('input');
            for (var i = 0; i < objs.length; i++) {
                if (objs[i].type.toLowerCase() == 'checkbox') {
                    objs[i].checked = false;
                }
            }
            var SelectCheckBoxId = SCheckBox.id;
            document.getElementById(SelectCheckBoxId).checked = true;
            document.getElementById("hfldContractId").value = SCheckBox.parentNode.parentNode.id;
            setbkImg('spPayoutModify', 'hfldContractId', 'Module=Archives');
        }

        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtProject' });
        }

        function rowQueryA(path) {
            parent.desktop.PayoutContractEdit = window;
            toolbox_oncommand(path, "查看收入合同");
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
        
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
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
                            起始金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartMoney" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结束金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndMoney" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
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
                        <td class="descTd">
                            合同类型
                        </td>
                        <td>
                            <asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td>
                            <asp:TextBox ID="txtProject" CssClass="select_input" Width="120px" imgclick="openProjPicker" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 160px; width: 100%;">
                <table class="tab">
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="">
                                <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID,IsMainContract" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata">
                                            <tr class="header">
                                                
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    合同编号
                                                </th>
                                                <th scope="col">
                                                    合同名称
                                                </th>
                                                <th scope="col">
                                                    最终金额
                                                </th>
                                                <th scope="col">
                                                    合同类型
                                                </th>
                                                <th scope="col">
                                                    乙方
                                                </th>
                                                <th scope="col">
                                                    结算方式
                                                </th>
                                                <th scope="col">
                                                    付款方式
                                                </th>
                                                <th scope="col">
                                                    流程状态
                                                </th>
                                                <th scope="col">
                                                    合同状态
                                                </th>
                                                <th scope="col">
                                                    签约时间
                                                </th>
                                                <th scope="col">
                                                    虚拟合同
                                                </th>
                                                <th scope="col">
                                                    项目
                                                </th>
                                                <th scope="col">
                                                    附件
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Num").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen ('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
                                                    <%# StringUtility.GetStr(Eval("ContractName").ToString(), 20) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="最终金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("CorpName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BalanceModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PayModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# WebUtil.GetConState(Eval("conState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignDate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="虚拟合同" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <asp:Label ID="labfictitious" Text='<%# System.Convert.ToString((Eval("fictitious").ToString() == "1") ? "否" : "是", System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                <%# GetAnnx(System.Convert.ToString(Eval("ContractID"))) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trFrame">
            <td style="width: 100%; vertical-align: top; padding-top: 10px;">
                <div>
                    <span id="spPayoutModify" style="margin-left: 0px;" class="xxk" onclick="framSrc('/ContractManage/PayoutModify/PayoutModify.aspx?Module=Archives')" runat="server">
                        合同变更</span> <span id="spPayoutBalance" class="xxk" onclick="framSrc('/ContractManage/PayoutBalance/PayoutBalance.aspx?Module=Archives')" runat="server">
                            合同结算 </span><span id="spPayoutPayment" class="xxk" onclick="framSrc('/ContractManage/PayoutPayment/PayoutPayment.aspx?Module=Archives')" runat="server">
                                合同付款 </span><span id="spPayoutInvoice" class="xxk" onclick="framSrc('/ContractManage/PayoutInvoice/PayoutInvoice.aspx?Module=Archives')" runat="server">
                                    合同发票</span>
                </div>
                <iframe id="framContract" frameborder="0" src="/ContractManage/PayoutModify/PayoutModify.aspx?Module=Archives?ContractID=''" width="100%" runat="server"></iframe>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldContractId" runat="server" />
    <asp:HiddenField ID="hdframsrc" runat="server" />
    <asp:HiddenField ID="hdSpId" runat="server" />
    </form>
</body>
</html>
