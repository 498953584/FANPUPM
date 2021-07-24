<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseplanList.aspx.cs" Inherits="StockManage_Purchase_PurchaseplanList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>物资采购计划</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            //隐藏刷新按钮
            $('#btnRefresh').hide();
            showTooltip('tooltip', 10);
            var purchasePlanTable = new JustWinTable('gvwPurchaseplan');
            purchasePlanTable.registClickTrListener(function () {
                if (!this.id) return false;
                if (!$('#gvwPurchaseplan').attr('page')) {
                    $('#hfldPurchasePlanCodes').val('');
                    $('#gvwPurchaseplan').attr('page', false);
                }
                $('#hfldCurPurchasePlanCodes').val(this.id);
                $('#btnRefresh').click();
            });

            purchasePlanTable.registClickSingleCHKListener(function () {
                var curPlanCode = this.parentNode.parentNode.parentNode.id;
                if (this.checked) {
                    $('#hfldCurPurchasePlanCodes').val(curPlanCode);
                }
                else {
                    var arr = eval($('#hfldPurchasePlanCodes').val());
                    $(arr).each(function (i) {
                        if (arr[i] == curPlanCode) {
                            delete arr[i];
                        }
                    });
                    $('#hfldPurchasePlanCodes').val(array1dToJson(arr));
                    $('#hfldCurPurchasePlanCodes').val('');
                }
                $('#btnRefresh').click();
            });

            purchasePlanTable.registClickAllCHKLitener(function () {
                var curPlanCodes = eval(purchasePlanTable.getCheckedChkIdJson()) || new Array();
                var oldPlanCodes = eval($('#hfldPurchasePlanCodes').val()) || new Array();
                //alert(oldPlanCodes.toString());
                if (this.checked) {
                    //全选
                    $(curPlanCodes).each(function (i) {
                        var index = isExist(oldPlanCodes, curPlanCodes[i]);
                        //alert(oldPlanCodes.toString() + '\n' + curPlanCodes[i] + '\n' + index);
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
                $('#hfldPurchasePlanCodes').val(array1dToJson(oldPlanCodes));
                $('#hfldCurPurchasePlanCodes').val('');
                $('#btnRefresh').click();
            });

            $('#gvwPurchaseplan a').click(function () {
                $('#gvwPurchaseplan').attr('page', true);
            });
        });

        function saveEvent() {
            var resources = new Array();
            $('#gvwPurchaseplanStock tr[id]').each(function () {
                resources.push(this.id);
            });
            var numberArr = new Array();
            $('#gvwPurchaseplanStock tr[number]').each(function () {
                numberArr.push($(this).attr('number'));
            });
            var obj = {};
            obj.id = array1dToJson(resources);
            obj.number = array1dToJson(numberArr);
            obj.code = $("#hfldPurchasePlanCodes").val();
            obj.code2 = $('#hidenppcode').val();

            if (typeof top.ui.callback == 'function') {
                top.ui.callback(obj);
                top.ui.callback == null;
            }
            top.ui.closeWin();
        }

        //返回元素在数组中的位置
        function isExist(els, e) {
            for (var i = 0; i < els.length; i++) {
                if (els[i] == e) {
                    return i;
                }
            }
            return -1;
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
                    <table style="border-collapse: separate; border-spacing: 0px; border-width: 0px;">
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
                                单据号:
                                <asp:TextBox ID="txtPpcode" Width="80px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
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
                    <div class="pagediv" style="height: 165px;">
                        <asp:GridView ID="gvwPurchaseplan" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPurchaseplan_RowDataBound" DataKeyNames="ppcode" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                        <asp:CheckBox ID="chkAllBox" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chkBox" ToolTip='<%# System.Convert.ToString(Eval("ppcode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * 6 + Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单据号"><ItemTemplate>
                                        <asp:Label ID="lblPpcode" Text='<%# System.Convert.ToString(Eval("ppcode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
                                        <%# Eval("person") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
                                        <%# Eval("intime") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否受理"><ItemTemplate>
                                        <%# (System.Convert.ToInt32(Eval("acceptstate")) == 0) ? "否" : "是" %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                        <%# Eval("annx") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
                                        <%# Eval("explain") %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" NumericButtonCount="8" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                    <br />
                    <div class="pagediv" style="height: 165px;">
                        <asp:GridView ID="gvwPurchaseplanStock" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPurchaseplanStock_RowDataBound" DataKeyNames="ResourceId,number" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                        <%# Eval("ResourceCode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                        <%# Eval("Name") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
                                        <%# Eval("number") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="80px">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Remark").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right">
        <input type="button" id="btnSave" value="保存" onclick="saveEvent();" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    <asp:HiddenField ID="hidenppcode" runat="server" />
    <asp:HiddenField ID="hfldPurchasePlanCodes" runat="server" />
    <asp:HiddenField ID="hfldCurPurchasePlanCodes" runat="server" />
    <asp:Button ID="btnRefresh" Text="" OnClick="btnRefresh_Click" runat="server" />
    </form>
</body>
</html>
