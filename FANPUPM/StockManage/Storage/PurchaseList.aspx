<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseList.aspx.cs" Inherits="StockManage_Storage_PurchaseList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>采购单</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //隐藏刷新按钮
            $('#btnRefresh').hide();
            showTooltip('tooltip', 5);
            //采购表
            var purchaseTable = new JustWinTable('gvwPurchase');
            purchaseTable.registClickTrListener(function () {
                if (!this.id) return false;
                if (!$('#gvwPurchase').attr('page')) {
                    $('#hfldOldCodes').val('');
                    $('#gvwPurchase').attr('page', false);
                }
                $('#hfldCurCodes').val(this.id);
                $('#btnRefresh').click();
            });

            purchaseTable.registClickSingleCHKListener(function () {
                var curPlanCode = this.parentNode.parentNode.id;
                if (this.checked) {
                    $('#hfldCurCodes').val(curPlanCode);
                }
                else {
                    var arr = eval($('#hfldOldCodes').val());
                    $(arr).each(function (i) {
                        if (arr[i] == curPlanCode) {
                            delete arr[i];
                        }
                    });
                    $('#hfldOldCodes').val(array1dToJson(arr));
                    $('#hfldCurCodes').val('');
                }
                $('#btnRefresh').click();
            });

            purchaseTable.registClickAllCHKLitener(function () {
                var curPlanCodes = eval(purchaseTable.getCheckedChkIdJson()) || new Array();
                var oldPlanCodes = eval($('#hfldOldCodes').val()) || new Array();
                if (this.checked) {
                    //全选
                    $(curPlanCodes).each(function (i) {
                        var index = isExist(oldPlanCodes, curPlanCodes[i]);
                        if (index == -1) {
                            oldPlanCodes.push(curPlanCodes[i]);
                        }
                    });
                }
                else {
                    //全不选
                    $(curPlanCodes).each(function (i) {
                        var index = isExist(oldPlanCodes, curPlanCodes[i]);
                        if (index > -1) {
                            delete oldPlanCodes[index];
                        }
                    });
                }
                $('#hfldOldCodes').val(array1dToJson(oldPlanCodes));
                $('#hfldCurCodes').val('');
                $('#btnRefresh').click();
            });

            //采购物资表
            var purchaseStockTable = new JustWinTable('gvwPurchaseStock'); //hfldResourceCode

            $('#gvwPurchase a').click(function () {
                $('#gvwPurchase').attr('page', true);
            });
        });

        //返回元素在数组中的位置
        function isExist(els, e) {
            for (var i = 0; i < els.length; i++) {
                if (els[i] == e) {
                    return i;
                }
            }
            return -1;
        }

        // 保存
        function saveEvent() {
            if (typeof top.ui.callback == 'function') {
                top.ui.callback({ code: $('#hfldOldCodes').val() });
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent">
        <table class="tableContent" cellpadding="2px" cellspacing="0" style="margin-left: auto;
            width: 100%;">
            <tr>
                <td>
                    <table style="border-collapse: separate; border-spacing: 0px 0px; border-width: 0px;">
                        <tr style="height: 25px;">
                            <td class="queryTd">
                                起始时间:
                                <asp:TextBox ID="txtStartTime" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="queryTd">
                                结束时间:
                                <asp:TextBox ID="txtEndTime" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="queryTd">
                                采购编号:
                                <asp:TextBox ID="txtPcode" Width="80px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="height: 350px; width: 100%;">
                    <div class="pagediv" style="height: 185px;">
                        <asp:GridView ID="gvwPurchase" AutoGenerateColumns="false" AllowPaging="true" CssClass="gvdata" OnRowDataBound="gvwPurchase_RowDataBound" DataKeyNames="pcode" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                        <asp:CheckBox ID="chkAllPurchase" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chkPurchase" runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购编号"><ItemTemplate>
                                        <asp:Label ID="lblPpcode" Text='<%# System.Convert.ToString(Eval("pcode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购合同"><ItemTemplate>
                                        <%# StringUtility.GetStr(Eval("ContractName").ToString()) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
                                        <%# Eval("person") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
                                        <%# Common2.GetTime(Eval("intime").ToString()) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否受理"><ItemTemplate>
                                        <%# (System.Convert.ToInt32(Eval("acceptstate")) == 0) ? "否" : "是" %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                        <%# Eval("annx") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# StringUtility.GetStr(Eval("explain").ToString(), 3) %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageIndexChanged" runat="server">
                        </webdiyer:AspNetPager>
                    </div>
                    <br />
                    <div class="pagediv" style="height: 165px;">
                        <asp:GridView ID="gvwPurchaseStock" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPurchaseStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
<EmptyDataTemplate>
                                <table class="gvdata">
                                    <tr class="header">
                                        <td style="width: 20px;">
                                        </td>
                                        <td style="width: 25px;">
                                            序号
                                        </td>
                                        <td>
                                            物资编号
                                        </td>
                                        <td>
                                            规格
                                        </td>
                                        <td>
                                            型号
                                        </td>
                                        <td>
                                            品牌
                                        </td>
                                        <td>
                                            技术参数
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            采购价
                                        </td>
                                        <td>
                                            数量
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                        <%# Eval("ResourceCode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 5) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 5) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 5) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("brand").ToString(), 5) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 5) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                        <%# Eval("UnitName") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价"><ItemTemplate>
                                        <%# Eval("sprice") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                        <%# Eval("number") %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right">
        <input type="button" id="btnSave" value="保存" onclick="saveEvent();" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    <asp:HiddenField ID="hfldResourceCode" runat="server" />
    <asp:Button ID="btnRefresh" Text="Button" OnClick="btnRefresh_Click" runat="server" />
    <asp:HiddenField ID="hfldOldCodes" runat="server" />
    <asp:HiddenField ID="hfldCurCodes" runat="server" />
    </form>
</body>
</html>
