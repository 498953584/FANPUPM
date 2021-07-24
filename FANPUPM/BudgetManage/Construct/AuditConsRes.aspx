<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditConsRes.aspx.cs" Inherits="BudgetManage_Construct_AuditConsRes" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectrestask_ascx" Src="~/StockManage/UserControl/SelectResTask.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量审核</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/json2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var gvResource = new JustWinTable('gvResource');
            showTooltip('tooltip', 9);

            //判断是否有任务节点
            if (parent.document.getElementById('hfldResInfo').value != "") {
                //更新资源信息的审核数量和cbsCode
                var resInfo = '[';
                var hfldResInfo = parent.document.getElementById('hfldResInfo').value;
                if (hfldResInfo.charAt(0) != '[') {
                    resInfo += hfldResInfo;
                    resInfo = resInfo.substr(0, resInfo.length - 1);
                    resInfo += ']';
                } else {
                    resInfo = hfldResInfo;
                }
                var resArr = eval(resInfo);
                for (var i = 0; i < resArr.length; i++) {
                    var res = resArr[i];
                    if (res.Code == document.getElementById('hfldConsTaskId').value) {
                        //判断是否有资源
                        if (document.getElementById('gvResource') != null) {
                            for (var j = 0; j < res.resources.length; j++) {
                                var resDetail = res.resources[j];
                                tb = document.getElementById('gvResource');
                                var cells = tb.rows[parseFloat(j) + 1].cells;
                                if (isNaN(parseFloat(resDetail.num))) {
                                    cells[8].children[0].value = "0.000";
                                } else {
                                    cells[8].children[0].value = toFixed(parseFloat(resDetail.num), 3);
                                }
                                var ddlCBS = cells[12].children[0];
                                for (var k = 0; k < ddlCBS.options.length; k++) {
                                    if (ddlCBS.options[k].value == resDetail.cbsCode) {
                                        ddlCBS.options[k].selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });

        function theMonenyChange(index, isOnblur) {
            tb = document.getElementById('gvResource');
            var cells = tb.rows[parseFloat(index) + 1].cells;
            var reportQuantity = parseFloat(cells[7].innerText);
            var AccountingQuantity = parseFloat(cells[8].children[0].value);
            if (isOnblur == '1') {
                if (AccountingQuantity < 0) {
                    alert("系统提示：\n\n审核工程量不能小于0");
                    AccountingQuantity = parseFloat(0);
                }
                else {
                    saveRes(index);
                }
                cells[8].children[0].value = toFixed(AccountingQuantity, 3); // getFormat(AccountingQuantity);
            }
            //相差金额
            var differ = parseFloat(cells[5].innerText) * toFixed(AccountingQuantity, 3); // getFormat(AccountingQuantity);
            var differValue = toFixed(differ, 3); // getFormat(differ);
            if (isNaN(differValue))
                differValue = "0.000";
            cells[11].innerText = differValue;
        }

        //保存资源信息
        function saveRes(index) {
            var resInfo = '[';
            var hfldResInfo = parent.document.getElementById('hfldResInfo').value;
            if (hfldResInfo.charAt(0) != '[') {
                resInfo += hfldResInfo;
                resInfo = resInfo.substr(0, resInfo.length - 1);
                resInfo += ']';
            } else {
                resInfo = hfldResInfo;
            }
            var resArr = eval(resInfo);
            for (var i = 0; i < resArr.length; i++) {
                var res = resArr[i];
                if (res.Code == document.getElementById('hfldConsTaskId').value) {
                    //alert(index)
                    var resDetail = res.resources[index];
                    tb = document.getElementById('gvResource');
                    var cells = tb.rows[parseFloat(index) + 1].cells;
                    var AccountingQuantity = parseFloat(cells[8].children[0].value);
                    var ddlCBS = cells[12].children[0];
                    var cbs = ddlCBS.options[ddlCBS.selectedIndex].value;
                    resDetail.num = AccountingQuantity;
                    resDetail.cbsCode = cbs;
                }
            }
            parent.document.getElementById('hfldResInfo').value = JSON.stringify(resArr);

        }

        //格式化三位小数
        function getFormat(num) {
            var numFormat;
            if (num.toFixed) {
                numFormat = num.toFixed(3);
            } else {
                var f3 = Math.pow(10, 3);
                numFormat = Math.round(num * f3) / f3;
            }
            return numFormat;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="border: 0px; width: 100%;">
        <tr style="width: 100%;">
            <td style="text-align: left;" class="divFooter">
                资源信息
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvResource_RowDataBound" DataKeyNames="Id,CBSCode" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号" HeaderStyle-Width="20%"><ItemTemplate>
                                <asp:Label ID="lblCode" Text='<%# System.Convert.ToString(Eval("ResourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称" HeaderStyle-Width="30%"><ItemTemplate>
                                
                                <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("ResourceName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("ResourceName").ToString(), 9) %></asp:HyperLink>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="40%"><ItemTemplate>
                                
                                <asp:HyperLink ID="hlinkShowName1" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("Specification").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Specification").ToString(), 9) %></asp:HyperLink>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="90px"><ItemTemplate>
                                
                                <%# DataBinder.Eval(Container.DataItem, "UnitName") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                <%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                <%# System.Convert.ToDecimal(Eval("BudQuantity")).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上报数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                <%# System.Convert.ToDecimal(Eval("Quantity")).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="审核数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
                                
                                <asp:TextBox Style="text-align: right;" Width="50px" Height="15px" ID="txtAccountingQuantity" CssClass="decimal_input" onblur="theMonenyChange(this.No,'1')" onkeypress="theMonenyChange(this.No,'0')" onkeyup="theMonenyChange(this.No,'0')" Text='<%# System.Convert.ToString(System.Convert.ToDecimal(Eval("AccountingQuantity")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                <%# ((System.Convert.ToDecimal(Eval("UnitPrice")) * System.Convert.ToDecimal(Eval("BudQuantity"))).ToString() == "") ? "0.000" : (System.Convert.ToDecimal(Eval("UnitPrice")) * System.Convert.ToDecimal(Eval("BudQuantity"))).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上报金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                <%# ((System.Convert.ToDecimal(Eval("UnitPrice")) * System.Convert.ToDecimal(Eval("Quantity"))).ToString() == "") ? "0.000" : (System.Convert.ToDecimal(Eval("UnitPrice")) * System.Convert.ToDecimal(Eval("Quantity"))).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="审核金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                <%# ((System.Convert.ToDecimal(Eval("UnitPrice")) * System.Convert.ToDecimal(Eval("AccountingQuantity"))).ToString() == "") ? "0.000" : (System.Convert.ToDecimal(Eval("unitPrice")) * System.Convert.ToDecimal(Eval("AccountingQuantity"))).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="成本分类" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
<ItemTemplate>
                                <asp:DropDownList ID="ddlCBS" onchange="saveRes(this.No);" No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:DropDownList>
                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                
            </td>
        </tr>
    </table>
    
    
    
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <!-- 保存选择的资源-->
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    <asp:HiddenField ID="hfldConsTaskId" runat="server" />
    </form>
</body>
</html>
