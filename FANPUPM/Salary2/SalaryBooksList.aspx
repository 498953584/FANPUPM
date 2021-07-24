<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryBooksList.aspx.cs" Inherits="Salary2_SalaryBooksList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>帐套管理</title><link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />
<link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="Script/SalaryBooksList.js"></script>
    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
   
    <style type="text/css">
        .descTd
        {
            width: 40px;
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
                    <input type="button" id="btnQuery" value="查看" style="display: none;" />
                    <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 50%; vertical-align:top;">
                    <asp:GridView ID="gvSalaryBooks" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvSalaryBooks_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" id="emptySalaryBooks" cellspacing="0" rules="all" border="1"
                                style="width: 100%; border-collapse: collapse;">
                                <tr class="header">
                                    <td scope="col" style="width: 20px;">
                                        <input type="checkbox" />
                                    </td>
                                    <td scope="col" style="width: 25px;">
                                        序号
                                    </td>
                                    <td scope="col" style="width: 400px;">
                                        名称
                                    </td>
                                    <td scope="col" style="width: 50px;">
                                        是否可用
                                    </td>
                                    <td scope="col">
                                        录入日期
                                    </td>
                                    <td scope="col">
                                        备注
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    <input type="checkbox" id="chkAll" />
                                </HeaderTemplate><ItemTemplate>
                                    <input type="checkbox" id="chkSelectSingle" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><HeaderTemplate>
                                    序号
                                </HeaderTemplate><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="400px"><HeaderTemplate>
                                    帐套名称
                                </HeaderTemplate><ItemTemplate>
                                    <span class="link tooltip" title="<%# Eval("Name") %>" onclick="viewopen('<%# Eval("Id") %>')">
                                        <%# StringUtility.GetStr(Eval("Name").ToString(), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    是否可用
                                </HeaderTemplate><ItemTemplate>
                                    <%# ((bool)Eval("IsValid")) ? "是" : "否" %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    录入日期
                                </HeaderTemplate><ItemTemplate>
                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField><HeaderTemplate>
                                    备注
                                </HeaderTemplate><ItemTemplate>
                                    <span class="tooltip" title="<%# Eval("Note") %>">
                                        <%# StringUtility.GetStr(Eval("Note"), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        明细
                                    </HeaderTemplate>

<ItemTemplate>
                                        <input id="btnDetail" type="button" value="编制明细" style="width: 80px;" onclick="editSaBooksItem('<%# Eval("Id") %>');" />
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSalaryBooksEdit" style="display: none" runat="server">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="descTd">
                    <span>* </span>名称
                </td>
                <td>
                    <asp:TextBox ID="txtSABookName" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[200]'" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    &nbsp;&nbsp;&nbsp;可用
                </td>
                <td>
                    <asp:CheckBox ID="chkIsValid" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    &nbsp;&nbsp;&nbsp;备注
                </td>
                <td>
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Height="50px" CssClass="easyui-validatebox" data-options="validType:'validChars[4000]'" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnSaveData" style=" display:none;" OnClick="btnSaveData_Click" runat="server" />
    <asp:HiddenField ID="hfldCheckedId" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
