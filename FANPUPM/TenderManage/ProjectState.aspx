<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectState.aspx.cs" Inherits="TenderManage_ProjectState" %>

<!DOCTYPE html>
<html>
<head runat="server"><title>项目状态</title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            var prjStateTable = new JustWinTable('tableSubProject');

            var btnUpdate = document.getElementById('btnUpdate');
            addEvent(btnUpdate, 'click', function() {
                
            });
            prjStateTable.registClickTrListener(function() {
                //btnUpdate.removeAttribute('disabled');
                btnUpdate.itemCode = this.id;
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;" class="pagediv">
        <table class="tab">
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <asp:Repeater ID="rptProjectState" runat="server">
<HeaderTemplate>
                            <table id="tableSubProject" class="gvdata" cellspacing="0" rules="all" border="1"
                                style="border-collapse: collapse; margin: auto;">
                                <tr class="header">
                                    <td style="width: 25px;">
                                        序号
                                    </td>
                                    <td style="width: 100px;">
                                        编码
                                    </td>
                                    <td style="text-align: left;">
                                        名称
                                    </td>
                                </tr>
                        </HeaderTemplate>

<ItemTemplate>
                            <tr class="rowa" id='<%# Eval("ItemCode") %>'>
                                <td>
                                    <%# Container.ItemIndex + 1 %>
                                </td>
                                <td>
                                    <asp:Label ID="lbl" Text='<%# System.Convert.ToString(Eval("ItemCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label1" Text='<%# System.Convert.ToString(Eval("ItemName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>

<AlternatingItemTemplate>
                            <tr class="rowb" id='<%# Eval("ItemCode").ToString() %>'>
                                <td>
                                    <%# Container.ItemIndex + 1 %>
                                </td>
                                <td>
                                    <asp:Label ID="lbl" Text='<%# System.Convert.ToString(Eval("ItemCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label1" Text='<%# System.Convert.ToString(Eval("ItemName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>

<FooterTemplate>
                            </table>
                        </FooterTemplate>
</asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
