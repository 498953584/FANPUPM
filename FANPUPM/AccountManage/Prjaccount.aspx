<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prjaccount.aspx.cs" Inherits="AccountManage_Prjaccount" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>账户设置</title>
    <script type="text/javascript" src="../Script/jquery.js"></script>

    <script src="../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../StockManage/script/TemplateTable.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td class="divHeader">
                账户设置
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <input type="button" id="btnAdd" value="新增" />
                <input type="button" id="btnUpdate" value="编辑" />
                <asp:Button ID="btnDelete" Text="删除" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div class="pagediv">
                    <asp:GridView ID="gvwAccount" Width="100%" CssClass="gvdata" runat="server">
<EmptyDataTemplate>
                            <table class="tab" width="100%">
                                <tr class="header">
                                <td>
                                    全选
                                </td>
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        项目编号
                                    </td>
                                    <td>
                                        项目名称
                                    </td>
                                    <td>
                                        部门名称
                                    </td>
                                    <td>
                                        账号编码
                                    </td>
                                    <td>
                                        创建人员
                                    </td>
                                    <td>
                                        创建日期
                                    </td>
                                    <td>
                                        是否激活
                                    </td>
                                    <td>
                                        激活人员
                                    </td>
                                    <td>
                                        激活日期
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
