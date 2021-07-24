<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserModelList.aspx.cs" Inherits="TableTop_UserModelList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择桌面显示栏目</title>
    <script src="/Web_Client/Tree.js" type="text/javascript"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="../Web_Client/TreeNew.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var contractTable = new JustWinTable('gvwModelList');
        });
        function colOpen() {
            top.ui.closeWin();
        }
        function successed() {
            top.ui.show( '添加成功!');
            top.ui.closeWin();
            top.ui.reloadTab();
        }
    </script>
</head>
<body>
    <form id="form1" class="body_frame" scroll="no" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="">
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <asp:HiddenField ID="hdName" runat="server" />
                            <asp:HiddenField ID="hdCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:GridView ID="gvwModelList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="c_mkdm" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="chk1" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 50px;">
                                                    模块编号
                                                </th>
                                                <th scope="col" style="width: 80px;">
                                                    模块名称
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模块编号" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                <%# Eval("c_mkdm") %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="v_mkmc" HeaderText="模块名称" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="width: 100%; height: 25px; float: left; text-align: right; vertical-align: bottom;">
        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="colOpen()" />
    </div>
    </form>
</body>
</html>
