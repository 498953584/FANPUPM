<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptset2.aspx.cs" Inherits="oa_SysAdmin_DeptSet_deptset2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>组织结构</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/json2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var depTable = new JustWinTable('gvwDep');
            depTable.registClickTrListener(function () {
                $('#hfldDepId').val(this.getAttribute('id'));
                $('#btnUpdate').removeAttr('disabled');
                //$('#btnDelete').removeAttr('disabled');
            });
            $('#btnAdd').click(addDep);
            $('#btnUpdate').click(editDep);
//            $('#btnDelete').get(0).onclick = deleteDep
            

        });

        function addDep() {
            var sjdm = getRequestParam('sid');
            top.ui.deptset2 = window;
            top.ui.openWin({
                title: '添加组织机构',
                url: '/oa/SysAdmin/DeptSet/depedit.aspx?action=add&sjdm=' + sjdm,
                width: 600, height: 30
            });
        }

        function editDep() {
            top.ui.deptset2 = window;
            var sjdm = getRequestParam('sid');
            var id = $('#hfldDepId').val();
            var url = '/oa/SysAdmin/DeptSet/depedit.aspx?action=edit&sjdm=' +
                sjdm + '&id=' + id;
            top.ui.openWin({
                title: '编辑组织机构',
                url: url,
                width: 400, height: 220
            });
        }

//        // 删除组织机构
//        function deleteDep() {
//            var id = $('#hfldDepId').val();
//            $.ajax({
//                url: '../../../PTdbmService.svc/Dep/' + id,
//                type: 'DELETE',
//                contentType: 'application/json',
//                success: function (data) {
//                    if (data == '1') {
//                        top.ui.show('删除成功');
//                        var sjdm = getRequestParam('sid');
//                        top.ui._OrgManager.location.href = jw.modifyParam({ url: top.ui._OrgManager.location.href, name: 'sel_id', value: sjdm });
//                    } else {
//                        top.ui.show('删除失败');
//                    }
//                },
//                error: function (ex) {
//                    top.ui.show('删除失败');
//                }
//            });
//        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="">
            <td class="divFooter" style="text-align: left">
                <input type="button" id="btnAdd" value="新增" />
                <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                
                <asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click1" runat="server" />
            </td>
        </tr>
        <tr style="vertical-align: top;">
            <td>
                <div>
                    <asp:GridView ID="gvwDep" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwDep_RowDataBound" DataKeyNames="i_bmdm" runat="server">
<EmptyDataTemplate>
                        </EmptyDataTemplate>
<Columns><asp:BoundField DataField="i_xh" HeaderText="序号" HeaderStyle-Width="200px" /><asp:BoundField DataField="V_BMMC" HeaderText="部门名称" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldDepId" runat="server" />
    </form>
</body>
</html>
