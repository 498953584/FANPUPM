<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryBooksItem.aspx.cs" Inherits="Salary2_SalaryBooksItem" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>帐套明细</title><link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />
<link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="Script/SalaryBooksItemList.js"></script>
    <style type="text/css">
        .descTd
        {
            width: 60px;
        }
        .descTd span
        {
            margin: 0px;
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <input type="button" id="btnAdd" value="新增" onclick="edit('add');" />
                    <input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="edit('edit');" />
                    <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                    <input type="button" id="btnUp" value="上移" disabled="disabled" onclick="MoveTREvent('Up');" />
                    <input type="button" id="btnDown" value="下移" disabled="disabled" onclick="MoveTREvent('Down');" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvSalaryBooksItem" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvSalaryBooksItem_RowDataBound" DataKeyNames="Id,No" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" id="emptySalaryBooksItem" cellspacing="0" rules="all" border="1"
                                style="width: 100%; border-collapse: collapse;">
                                <tr class="header">
                                    <td scope="col" style="width: 20px;">
                                        <input type="checkbox" />
                                    </td>
                                    <td scope="col" style="width: 25px;">
                                        序号
                                    </td>
                                    <td scope="col">
                                        名称
                                    </td>
                                    <td scope="col" align="center" style="width: 80px;">
                                        默认值
                                    </td>
                                    <td scope="col" align="center" style="width: 50px;">
                                        是否公式
                                    </td>
                                    <td scope="col" align="center" style="width: 50px;">
                                        是否显示
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    <input type="checkbox" id="chkAll" />
                                </HeaderTemplate><ItemTemplate>
                                    <input type="checkbox" id="chkSelectSingle" />
                                    <asp:HiddenField ID="hfldNum" Value='<%# System.Convert.ToString(Container.DataItemIndex + 1, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><HeaderTemplate>
                                    序号
                                </HeaderTemplate><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="150px"><HeaderTemplate>
                                    名称
                                </HeaderTemplate><ItemTemplate>
                                    <span class="tooltip" title="<%# GetSaItemName(Eval("ItemId").ToString()) %>">
                                        <%# StringUtility.GetStr(base.GetSaItemName(Eval("ItemId").ToString()), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="默认值" ItemStyle-CssClass="decimal_input" HeaderStyle-Width="80px">
<ItemTemplate>
                                    <%# Eval("DefaultValue") %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Left"><HeaderTemplate>
                                    公式
                                </HeaderTemplate><ItemTemplate>
                                <span class="tooltip" title="<%# ConvertFormula((Eval("Formula") == null) ? "" : Eval("Formula").ToString()) %>">
                                    <%# StringUtility.GetStr(base.ConvertFormula((Eval("Formula") == null) ? "" : Eval("Formula").ToString()), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    是否显示
                                </HeaderTemplate><ItemTemplate>
                                    <%# ((bool)Eval("Isshow")) ? "是" : "否" %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSalaryItemEdit" style="display: none" runat="server">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="descTd">
                    &nbsp;&nbsp;&nbsp;工资项
                </td>
                <td>
                    <asp:DropDownList ID="ddlSalaryItem" Style="width: 100%;" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;默认值
                </td>
                <td>
                    <asp:TextBox ID="txtDefaultValue" CssClass="easyui-validatebox easyui-numberbox" data-options="min:0,max:9999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;启用公式
                </td>
                <td>
                    <asp:CheckBox ID="chkIsFormula" runat="server" />
                    &nbsp;&nbsp;&nbsp;<input type="button" id="btnFormula" value="计算公式" style="width: 70px;"
                        disabled="disabled" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;是否显示
                </td>
                <td>
                    <asp:CheckBox ID="chkIsDisplay" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFormula" style="display: none">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtFormula" Width="100%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="width: 252px; margin: auto; padding:  0px 0px 0px 11px;">
                        <legend>工资项名称</legend>
                        <div id="divSalaryItem">
                        </div>
                    </fieldset>
                </td>
                <td valign="top">
                    <fieldset style="width: 152px; height: 145px; margin: auto; padding: 0px 0px 0px 10px;
                        text-align: center;">
                        <legend>计算符号</legend>
                        <table id="tabOperator" cellpadding="3">
                            <tr>
                                <td>
                                    <input type="button" id="btnPlus" value="+" style="width: 40px;" />
                                </td>
                                <td>
                                    <input type="button" id="btnMinus" value="-" style="width: 40px;" />
                                </td>
                                <td>
                                    <input type="button" id="btnProduct" value="*" style="width: 40px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" id="btnDivision" value="/" style="width: 40px;" />
                                </td>
                                <td>
                                    <input type="button" id="btnLeft" value="(" style="width: 40px;" />
                                </td>
                                <td>
                                    <input type="button" id="btnRight" value=")" style="width: 40px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" id="btnClear" value="清空" style="width: 40px;" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldCheckedId" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <asp:HiddenField ID="hfldSaBooks" runat="server" />
    <asp:HiddenField ID="hfldFormula" runat="server" />
    <asp:Button ID="btnSaveData" Style="display: none;" OnClick="btnSaveData_Click" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
