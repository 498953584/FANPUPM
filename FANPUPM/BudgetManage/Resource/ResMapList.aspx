<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResMapList.aspx.cs" Inherits="BudgetManage_Resource_ResMapList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源映射</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var mapTable = new JustWinTable('gvwResMap');
            setButton(mapTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldParentId');

            // 新增
            $('#btnAdd').click(function () {
                top.ui._ResMap = window;
                var url = "/BudgetManage/Resource/ResMap.aspx?action=add";
                toolbox_oncommand(url, "添加资源映射");
            });

            // 编辑
            $('#btnUpdate').click(function () {
                top.ui._ResMap = window;
                var pid = $('#hfldParentId').val();
                var url = "/BudgetManage/Resource/ResMap.aspx?action=update&parentId=" + pid;
                toolbox_oncommand(url, "编辑资源映射");
            });

            // 删除
            $('#btnDelete').click(function () {
                return jw.confirm('系统提示', '您确定要删除吗？', 'btnDelete');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0px" cellspacing="0px" style="width: 100%; vertical-align: top;">
            <tr style="height: 30px;">
                <td>
                    资源编码<asp:TextBox ID="txtResCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                    资源名称<asp:TextBox ID="txtResName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                    型号<asp:TextBox ID="txtModelNumber" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSertch" Text="查询" OnClick="btnSertch_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <input type="button" id="btnAdd" value="新增" />
                    <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                    <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                </td>
            </tr>
            <tr style="vertical-align: top; height: 70%">
                <td>
                    <asp:GridView ID="gvwResMap" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResMap_RowDataBound" DataKeyNames="ResourceId" runat="server">
<EmptyDataTemplate>
                            <table id="empty_gvwResMap" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col" style="width: 300px">
                                        资源编号
                                    </th>
                                    <th scope="col">
                                        资源名称
                                    </th>
                                    <th scope="col">
                                        型号
                                    </th>
                                    <th scope="col">
                                        品牌
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="25px" /><asp:BoundField HeaderText="资源编号" DataField="ResourceCode" /><asp:BoundField HeaderText="资源名称" DataField="ResourceName" /><asp:BoundField HeaderText="型号" DataField="ModelNumber" /><asp:BoundField HeaderText="品牌" DataField="Brand" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldParentId" runat="server" />
    </form>
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
</body>
</html>
