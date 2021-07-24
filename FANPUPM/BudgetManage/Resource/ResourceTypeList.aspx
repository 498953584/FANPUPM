<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceTypeList.aspx.cs" Inherits="BudgetManage_Resource_ResourceTypeList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源类型设置</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var jwTable = new JustWinTable('gvResource');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
            function rowEdit() {
                var url = "/BudgetManage/Resource/ResourceTypeEdit.aspx?id=" + document.getElementById("hfldPurchaseChecked").value + "&parentId=" + document.getElementById("hdParentId").value;
                //winopen(url)
                top.ui.openWin({title: '资源类型', url: url, width: 600, height: 370});
            }
            function rowAdd() {
                var url = "/BudgetManage/Resource/ResourceTypeEdit.aspx?parentId=" + document.getElementById("hdParentId").value;
                top.ui.openWin({title: '资源类型', url: url, width: 600, height: 370});
            }
            function rowQuery() {
                var url = "/BudgetManage/Resource/ResourceTypeEdit.aspx?t=1&id=" + document.getElementById("hfldPurchaseChecked").value;
                winopen(url)
            }

            //页面加载时从cookie取滚动条位置信息，然后附值给滚动条
            var arr = getCookie('scrollTop');
            document.getElementById('div1').scrollTop = parseInt(arr);
        }


        //页面刷新前保存滚动条位置信息到cookie
        window.onbeforeunload = function () {
            var scrollPos;
            scrollPos = document.getElementById('div1').scrollTop;
            document.cookie = "scrollTop=" + scrollPos;
        }

        //获取Cookie
        function getCookie(name) {
            var start = document.cookie.indexOf(name + "=");
            var len = start + name.length + 1;
            if ((!start) && (name != document.cookie.substring(0, name.length))) {
                return null;
            }
            if (start == -1) return null;
            var end = document.cookie.indexOf(';', len);
            if (end == -1) end = document.cookie.length;
            return unescape(document.cookie.substring(len, end));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="资源类型设置" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%; height: 89%;">
        <tr>
            <td style="width: 220px; vertical-align: top; height: 100%;">
                <div class="pagediv" style="width: 220px; height: 105%;" id="div1" runat="server">
                    <asp:TreeView ID="tvResource" ShowLines="true" ExpandDepth="2" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" OnTreeNodePopulate="TreeView_Populate" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <input type="button" id="btnAdd" value="新增" />
                            <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                            <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                            <input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="pagediv">
                                <asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="resourcetypeid" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("resourcetypeid"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源分类名称"><ItemTemplate>
                                                <%# Eval("ResourceTypeName") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源分类编码"><ItemTemplate>
                                                <%# Eval("ResourceTypeCode") %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hdParentId" runat="server" />
    <asp:HiddenField ID="hfldPriceTypeId" runat="server" />
    </form>
</body>
</html>
