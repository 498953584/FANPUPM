<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleCorpUser.aspx.cs" Inherits="EPC_Workflow_RoleCorpUser" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link Href="/Script/jquery.treeview/jquery.treeview.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyTable', 'gvCorpUser');
            var jwTable = new JustWinTable('gvCorpUser');
        });

        function doClickRow(roleId) {
            document.getElementById('hdnUserID').value = roleId;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnDel').disabled = false;

        }
        function corpRoleAdd(action) {
            var url = '';
            if (action == 'add') {
                url = '/EPC/Workflow/RoleCorpEdit.aspx?action=add&rid=' + document.getElementById('hdnRoleID').value;
            }
            else {
                url = '/EPC/Workflow/RoleCorpEdit.aspx?action=update&uid=' + document.getElementById('hdnUserID').value + '&rid=' + document.getElementById('hdnRoleID').value;
            }
            top.ui._rolecorpuser = window;
            window.parent.parent.desktop._rolecorpuser = window;
            toolbox_oncommand(url, "部门相关角色信息维护");
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="height: 99%" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="divHeader" style="height: 22px">
                部门相关角色设置
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 24px">
                
                <input id="hdnRoleID" style="width: 10px" type="hidden" name="hdnRoleID" runat="server" />

                <input id="hdnUserID" style="width: 10px" type="hidden" name="hdnUserID" runat="server" />

                <input type="button" id="btnAdd" value="新增" onclick=" corpRoleAdd('add')" runat="server" />

                <input type="button" id="btnEdit" value="修改" disabled="true" onclick=" corpRoleAdd('edit')" runat="server" />

                <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" id="td-user">
                <div style="overflow: auto; width: 100%;" id="divUser">
                    <asp:GridView ID="gvCorpUser" CssClass="gvdata" AutoGenerateColumns="false" AutoGenerateSelectButton="false" OnRowDataBound="gvCorpUser_RowDataBound" DataKeyNames="Role_User_ID" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable">
                                <tr class="header">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        部门负责人
                                    </th>
                                    <th scope="col">
                                        所负责部门
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="20px">
<ItemTemplate>
                                    
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="部门负责人" DataField="v_xm" /><asp:BoundField HeaderText="所负责部门" DataField="CorpCode" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
