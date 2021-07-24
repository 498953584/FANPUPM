<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDiaryQueryOneself.aspx.cs" Inherits="BudgetManage_Cost_CostDiaryQueryOneself" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本日记账-费用查询</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            setWidthHeight();
            var jwTable = new JustWinTable('gvBudget');
            setButton(jwTable, '', '', 'btnSearch', 'hfldPurchaseChecked');
            showDetails();
            if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/))
                document.getElementById('div_project').scrollTop = parseInt(arr[1]);
            showTooltip('tooltip', 20);
            //添加点击行事件
            $('#gvBudget tr').live('click', function () {
                showDetails();
            });
            $('#txtStartDate').attr('read')
        });


        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 105);
            $('#divDetails').height(180);
            $('#divDiaries').height($('#divBudget').height() - $('#divDetails').height());
            $('#div_project').height($(this).height() - 20);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
        }

        //复选框选择
        function setButton(jwTable, edit, del, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }

            jwTable.registClickTrListener(function () {
                if (hdChkId != '') {
                    $('#hfldPurchaseChecked').val(this.id);
                }
                if (this.id != '')
                    showDetails();
            });
        };

        //点击行绑定日记明细
        function showDetails() {
            var src = 'IFCostDetails.aspx?id=' + $('#hfldPurchaseChecked').val() + '&year=' + jw.getRequestParam('year');
            $('#ifDetails').attr('src', src);
        }

        //合计
        function fillTotalAmount(total) {
            var trText = '<tr><td align="center">合计</td><td></td><td></td><td></td><td></td><td></td><td></td><td align="right">' + total + '</td><td></td><td></td><td></td><td></td></tr>';
            if ($('#gvBudget')) {
                var tab = $('#gvBudget').get(0);
                var lastRowId = tab.rows[tab.rows.length - 1].id;
                $('#' + lastRowId).after(trText);
            }
        }

        function openDetails(id, title) {
            var ddlValue = jw.getRequestParam('year');
            var url = '/BudgetManage/Cost/CostVerifyRecord.aspx?costType=P&ic=';
            if (ddlValue == 'zzjg')
                url = '/BudgetManage/Cost/OrgVerifyRecord.aspx?ic=';
            url += id;
            title += '明细';
            toolbox_oncommand(url, title);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table>
                                    <tr>
                                        <td>
                                            <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                                <tr>
                                                    <td class="descTd">
                                                        发生单位
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDeparment" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        经手人
                                                        <asp:DropDownList ID="ddlSelectPerson" Visible="false" runat="server"><asp:ListItem Text="填报人" Value="1" Selected="true" /><asp:ListItem Text="经手人" Value="0" /></asp:DropDownList>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPerson" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        费用名称
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="descTd">
                                                        开始时间
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        结束时间
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        审核状态
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFlowState" Width="120px" runat="server"><asp:ListItem Text="" Value="" Selected="true" /><asp:ListItem Text="未提交" Value="-1" /><asp:ListItem Text="审核中" Value="0" /><asp:ListItem Text="已审核" Value="1" /><asp:ListItem Text="驳回" Value="-2" /><asp:ListItem Text="重报" Value="-3" /></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        金额
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalAmount" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Width="120px" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd" style="width: 50px;">
                                                        至
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEndCash" CssClass="easyui-numberbox" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnSearch" Text="查 询" OnClick="btnSearch_Click" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="divFooter" style="text-align: right;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; height: 100%;">
                                            <div id="divBudget" style="overflow: hidden;">
                                                <div id="divDiaries" style="overflow: auto;">
                                                    <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="InDiaryId" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="费用名称"><ItemTemplate>
                                                                    <span class="tooltip" title='<%# Eval("Name") %>'>
                                                                        <%# StringUtility.GetStr(Eval("Name"), 20) %>
                                                                    </span>
                                                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="发生单位" DataField="Department" /><asp:TemplateField HeaderText="发生时间"><ItemTemplate>
                                                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="填报人" DataField="InputUser" /><asp:BoundField HeaderText="经手人" DataField="IssuedBy" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                                                    <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                                    <%# Eval("TotalAmount") %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="月度差额比例" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                                                                    <span class="link" onclick="openDetails('<%# Eval("InDiaryId") %>','<%# Eval("Name") %>')">
                                                                         <%# (decimal.Parse(Eval("MonthBudgetAmount").ToString()) > 0m) ? ((decimal.Parse(Eval("MonthBudgetAmount").ToString()) - decimal.Parse(Eval("MothDiaryAlreadyAmount").ToString())) / decimal.Parse(Eval("MonthBudgetAmount").ToString())).ToString("P2") : 0m.ToString("P2") %>
                                                                    </span>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="所属项目"><ItemTemplate>
                                                                    <%# this.hfldPrjName.Value %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="核销金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                                    <%# GetAuditAmountSum(Eval("InDiaryId").ToString()).ToString("#,##0.000") %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否核销"><ItemTemplate>
                                                                   <%# IsAudit(Eval("InDiaryId").ToString()) %>
                                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                                    </webdiyer:AspNetPager>
                                                </div>
                                                <div id="divDetails" style="border-style: none; border-top: solid 1px #CADEED; margin-top: 1px;">
                                                    <iframe id="ifDetails" style="width: 100%; height: 98%;" frameborder="none" border="0">
                                                    </iframe>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldPrjName" runat="server" />
    </form>
</body>
</html>
