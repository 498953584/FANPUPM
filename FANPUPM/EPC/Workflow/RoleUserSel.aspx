<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleUserSel.aspx.cs" Inherits="ModuleSet_Workflow_RoleUserSel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择兼职人员</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        window.name = "win";
        $(document).ready(function () {
            replaceEmptyTable('emptyTable', 'GVDuty');
            var jwTable = new JustWinTable('GVDuty');
            $('#btnUserCode').css('display', 'none');
        });

        function ClickRow(usercode, username) {
            document.getElementById('hdnUserId').value = usercode;
            document.getElementById('hdnUserName').value = username;
            document.getElementById('btnDel').disabled = false;
        }

        // 选择人员
        function pickPerson() {
            var url = "/EPC/Workflow/SelectAllUser.aspx";

            top.ui.openWin({ title: '选择人员', url: url });
            top.ui.callback = function (user) {
                document.getElementById('hdnUserCode').value = user.code;
                document.getElementById('txtUserCode').value = user.name;
                $('#btnUserCode').click();
            }
        }

        function success() {
            top.ui.show( '保存成功');
            if (top.ui.parent) {
                top.ui.parent.location.href = top.ui.parent.location.href;
                top.ui.parent = null;
            }
            top.ui.closeTab();
        }

        function error() {
            top.ui.show( '保存失败');
            top.ui.closeTab();
        }

    </script>
</head>
<body>
    <form id="form1" target="win" runat="server">
    <div>
        <table cellspacing="0" width="100%">
            <tr>
                <td class="divHeader" colspan="2">
                    选择人员
                </td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    姓名&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtUserCode" Width="70%" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hdnUserCode" runat="server" />
                    <input type="button" id="btnPickUser" value="选 择" onclick="pickPerson();" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:HiddenField ID="hdnUserId" runat="server" />
                    <asp:HiddenField ID="hdnUserName" runat="server" />
                    <asp:Button ID="btnDel" Enabled="false" Text="删 除" OnClick="btnDel_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView CssClass="gvdata" ID="GVDuty" AllowSorting="true" AutoGenerateColumns="false" Width="100%" OnRowDataBound="GVDuty_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable">
                                <tr align="center" class="header">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        用户名
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        部门
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        岗位
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="v_xm" HeaderText="用户名" /><asp:BoundField DataField="V_BMMC" HeaderText="部门" HtmlEncode="false" /><asp:BoundField DataField="RoleTypeName" HeaderText="岗位" HtmlEncode="false" /><asp:BoundField DataField="CorpCode" Visible="false" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle></asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                    <input id="BtnClose" onclick="top.ui.closeTab();" type="button" value="关 闭" />&nbsp;&nbsp;&nbsp;
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserCode" Display="None" ErrorMessage="姓名不能为空！" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnUserCode" Text="选 择" Width="0px" OnClick="btnUserCode_Click" runat="server" />
    </form>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
