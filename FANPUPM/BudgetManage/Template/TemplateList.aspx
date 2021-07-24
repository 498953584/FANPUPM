<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateList.aspx.cs" Inherits="BudgetManage_Template_TemplateList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>模板列表</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var gvTemplate = new JustWinTable('gvTemplate');
            setButton(gvTemplate, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);

            function rowEdit() {
                top.ui._TemplateList = window;
                var url = "/BudgetManage/Template/TemplateEdit.aspx?id=" + document.getElementById("hfldPurchaseChecked").value + "&tempTypeId=" + document.getElementById("hfldTempTypeId").value;
                toolbox_oncommand(url, "编辑模板");
            }
            function rowAdd() {
                top.ui._TemplateList = window;
                var url = "/BudgetManage/Template/TemplateEdit.aspx?tempTypeId=" + document.getElementById("hfldTempTypeId").value;
                toolbox_oncommand(url, "新增模板");
            }
            function rowQuery() {
                var url = "AddIncometBalance.aspx?t=1&id=" + document.getElementById("hfldPurchaseChecked").value;
                winopen(url)
            }

        }       
  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader" style="display: none;">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="模板列表" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%; height: 95%;">
        <tr>
            <td style="width: 195px; vertical-align: top; height: 100%;">
                <div class="pagediv" style="width: 195px; height: 105%;">
                    <asp:TreeView ID="tvTempType" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; vertical-align: top;">
                            <table border="0" class="tab">
                                <tr>
                                    <td class="divFooter" style="text-align: left;">
                                        <input type="button" id="btnAdd" disabled="true" value="新增" runat="server" />

                                        <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                                        <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('系统提示：\n\n您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                                        <input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%; vertical-align: top;">
                                        <div>
                                            <asp:GridView ID="gvTemplate" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                            <asp:CheckBox ID="cbAllBox" runat="server" />
                                                        </HeaderTemplate><ItemTemplate>
                                                            <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("Id"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模板名称"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Eval("Name") %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模板类型"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Eval("BudTempType.Name") %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                            <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                            </webdiyer:AspNetPager>
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
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hdContractID" runat="server" />
    <asp:HiddenField ID="hfldTempTypeId" runat="server" />
    </form>
</body>
</html>
