<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResTypeCostAllocation.aspx.cs" Inherits="Construct_ResTypeCostAllocation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源类型成本归集</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            hideControls();
            var typeTable = new JustWinTable('gvwResType');
            replaceEmptyTable();
        });

        function CheckType(typeId) {
            parent.desktop.ContractTypeEdit = window;
            var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Query&TypeID=' + typeId;
            toolbox_oncommand(url, "查看合同类型");
        }

        function replaceEmptyTable() {
            //当数据量为空时，修改样式
            if (!document.getElementById('gvwResType')) return;
            if (!document.getElementById('emptyResType')) return;
            var gvwResType = document.getElementById('gvwResType');
            var emptyResType = document.getElementById('emptyResType');
            if (gvwResType.firstChild.childNodes.length == 1) {
                gvwResType.replaceChild(emptyResType.firstChild, gvwResType.firstChild);
            }
        }

        function hideControls() {
            if (!document.getElementById('btnDataBind')) return;
            document.getElementById('btnDataBind').style.display = 'none';
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2">
        <table style="vertical-align: top; width: 100%; height: 100%;">
            <tr style="display: none;">
                <td class="divHeader" colspan="2">
                    资源类型成本归集
                </td>
            </tr>
            <tr>
                <td style="width: 100%; vertical-align: top;">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvwResType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwResType_RowDataBound" DataKeyNames="Id,CBSCode" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyResType" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    资源分类编码
                                                </th>
                                                <th scope="col">
                                                    资源分类名称
                                                </th>
                                                <th scope="col">
                                                    直接成本
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Code" HeaderText="资源分类编码" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="资源分类名称" HeaderStyle-Width="200px"><ItemTemplate>
                                                <%# Eval("Name") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="直接成本" HeaderStyle-Width="200px"><ItemTemplate>
                                                <asp:DropDownList ID="ddlCBS" runat="server"></asp:DropDownList>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldContractTypeGuid" runat="server" />
    </form>
</body>
</html>
