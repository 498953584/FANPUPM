<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelTemplateList.aspx.cs" Inherits="BudgetManage_Resource_ExcelTemplateList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>Excel模板维护</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        window.onload = function() {
            var aa = new JustWinTable('gvResource');
            setButton2(aa, 'btnDel', 'btnReturn', 'btnLook', 'hfldPurchaseChecked')
            document.getElementById("btnReturn").disabled = document.getElementById("btnDel").disabled;
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
        }
        function rowEdit() {
            var url = "UnitEdit.aspx?id=" + document.getElementById("hfldPurchaseChecked").value;
            winopen(url)
        }
        function rowAdd() {
            var url = "UnitEdit.aspx";
            winopen(url)
        }
        function rowQuery() {
            var url = "AddIncometBalance.aspx?t=1&id=" + document.getElementById("hfldPurchaseChecked").value;
            winopen(url)
        }


        function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function() {
                document.getElementById(btnDel).removeAttribute('disabled');
                document.getElementById(btnUpdate).removeAttribute('disabled');
            });

            jwTable.registClickSingleCHKListener(function() {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');

                }
                else
                    if (checkedChk.length == 1) {
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnUpdate).removeAttribute('disabled');

                }
                else {
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnUpdate).removeAttribute('disabled');

                }
            });
            jwTable.registClickAllCHKLitener(function() {
                if (this.checked) {
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnUpdate).removeAttribute('disabled');
                }
                else {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                }
                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnUpdate).removeAttribute('disabled');
                }
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="Excel模板维护" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <input type="button" id="btnAdd" value="新增" style="display: none;" />
                            <input type="button" id="btnEdit" disabled="disabled" style="display: none;" value="编辑" />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDel" Width="80px" Text="删  除" disabled="disabled" OnClick="btnDel_Click" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReturn" Text="恢  复" disabled="disabled" OnClientClick="return confirm('您确定要恢复吗??')" OnClick="btnReturn_Click" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div>
                                <asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TemplateId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("TemplateId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模板名称"><ItemTemplate>
                                                <%# Eval("TemplateName") %>
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
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hdContractID" runat="server" />
    </form>
</body>
</html>
