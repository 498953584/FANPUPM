<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmMaterialBackAddList.aspx.cs" Inherits="StockManage_MaterialBack_SmMaterialBackAddList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>无标题页</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var chk = new JustWinTable('gvSm_MaterialBack');

        }
    </script>
    <script type="text/javascript">
        function openWindow() {

            var pname = document.getElementById('hdnProname').value;
            var pcode = document.getElementById('hndProCode').value;
            var url = "SmMaterialBackAdd.aspx?optype=add&pname=" + pname + "&pcode=" + pcode;
            winopen(url);
        }
        //编辑查看验证
        function checkCount() {
            var chk = new JustWinTable('gvSm_MaterialBack');
            if (chk.getCheckedChk().length > 1) {
                alert('系统提示：\n\n编辑或者查看时，只能选择单行！');
                return false;
            }
            if (chk.getCheckedChk().length == 0) {
                alert('系统提示：\n\n请选择要操作的行！');
                return false;
            }
            return true;
        }
        //删除验证
        function delCheck() {
            var chk = new JustWinTable('gvSm_MaterialBack');
            if (chk.getCheckedChk().length == 0) {
                alert('系统提示：\n\n请选择要操作的行！');
                return false;
            }
            else {
                return confirm("确定删除吗？");
            }

        }
        function getProName() {
            return document.getElementById('hdnProname').value;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="headerrow">
                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                <input id="hdnProname" style="width: 7px" type="hidden" runat="server" />

                <input id="hndProCode" style="width: 7px" type="hidden" runat="server" />

            </td>
        </tr>
        <tr class="bottonrow">
            <td>
                退库编号<input id="txtCode" type="text" style="width: 130px" runat="server" />
退库时间<JWControl:DateBox ID="DateIn" Width="88px" runat="server"></JWControl:DateBox><input id="btnSearch" type="button" value="查询" OnServerClick="btnSearch_ServerClick" runat="server" />

                <input id="hdnSwid" style="width: 7px" type="hidden" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="bottonrow">
                <input id="btnAdd" type="button" value="新增" onclick="openWindow();" />
                <input id="btnUpdate" type="button" value="编辑" OnServerClick="btnUpdate_ServerClick" runat="server" />

                <input id="btnDel" type="button" value="删除" OnServerClick="btnDel_ServerClick" runat="server" />

                <input id="btnLook" type="button" value="查看" OnServerClick="btnLook_ServerClick" runat="server" />

                <input id="btnCheck" type="button" value="提交审核" />
                <input id="btnLookCheck" type="button" value="查看审核" />
                <input id="btnRecord" type="button" value="审核记录" />
                <asp:Button ID="btnMakeSure" Text="确认退库" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div class="pagediv">
                    <asp:GridView ID="gvSm_MaterialBack" AutoGenerateColumns="false" CssClass="gvdata" runat="server">
<EmptyDataTemplate>
                            <table width="100%">
                                <tr class="header">
                                    <td style="width: 30px">
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td style="width: 100px">
                                        材料编号
                                    </td>
                                    <td style="width: 150px">
                                        材料名称
                                    </td>
                                    <td>
                                        规格
                                    </td>
                                    <td>
                                        单位
                                    </td>
                                    <td>
                                        单价
                                    </td>
                                    <td>
                                        供应商
                                    </td>
                                    <td>
                                        出库数量
                                    </td>
                                    <td>
                                        退库数量
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" Width="20px" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chkBox" Width="20px" ToolTip='<%# Convert.ToString(Eval("rcode")) %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %></ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="退库单号"><ItemTemplate>
                                    <span style="color: Blue; cursor: hand; text-decoration: underline" onclick="winopen('SmMaterialBackAdd.aspx?optype=look&rcode=<%# Eval("rcode") %>&pname=<%# getProName() %>')">
                                        <%# Eval("rcode") %></span></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流转状态"><ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "flowstate") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="退库确认"><ItemTemplate>
                                    <%# string.Format("{0:d}", DataBinder.Eval(Container.DataItem, "isin")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="退库时间"><ItemTemplate>
                                    <%# string.Format("{0:d}", DataBinder.Eval(Container.DataItem, "intime")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "explain") %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
