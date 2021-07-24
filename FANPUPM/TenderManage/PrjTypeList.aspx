<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjTypeList.aspx.cs" Inherits="TenderManage_PrjTypeList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>模板列表</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var gvPrjType = new JustWinTable('gvPrjType');
            setButton(gvPrjType, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
            if (document.getElementById('hdPrjTypeID').value == '') {
                document.getElementById('btnAdd').disabled = "disabled";
            } else {
                document.getElementById('btnAdd').disabled = "";
            }
            function rowEdit() {
                parent.desktop.PrjTypeEdit = window;
                var url = "/TenderManage/PrjTypeEdit.aspx?itemCode=" + document.getElementById("hfldPurchaseChecked").value + "&prjType=" + document.getElementById("hdPrjTypeID").value;
                toolbox_oncommand(url, "编辑工程类型");
            }
            function rowAdd() {
                parent.desktop.PrjTypeEdit = window;
                var url = "/TenderManage/PrjTypeEdit.aspx?&prjType=" + document.getElementById("hdPrjTypeID").value;
                toolbox_oncommand(url, "新增工程类型");
            }
        }       
  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 95%;">
        <tr>
            <td style="width: 195px; vertical-align: top; height: 100%;">
                <div class="pagediv" style="width: 195px; height: 105%; vertical-align: top; position: relative;">
                    <asp:TreeView ID="tvPrjType" Font-Size="12px" ShowLines="true" Style="top: 0px; position: absolute;" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; vertical-align: top;">
                            <table border="0" class="tab">
                                <tr>
                                    <td class="divFooter" style="text-align: left;">
                                        <input type="button" id="btnAdd" value="新增" runat="server" />

                                        <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                                        <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('系统提示：\n\n您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                                        <input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%; vertical-align: top;">
                                        <div>
                                            <asp:GridView ID="gvPrjType" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPrjType_RowDataBound" DataKeyNames="ItemCode" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                            <asp:CheckBox ID="cbAllBox" runat="server" />
                                                        </HeaderTemplate><ItemTemplate>
                                                            <asp:CheckBox ID="cbBox" ToolTip="" runat="server" />
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程类型名称"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Eval("ItemName") %>
                                                            </span>
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
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hdPrjTypeID" runat="server" />
    </form>
</body>
</html>
