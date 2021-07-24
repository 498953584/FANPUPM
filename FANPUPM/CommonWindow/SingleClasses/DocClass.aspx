<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocClass.aspx.cs" Inherits="DocClass" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html >
<html>
<head runat="server"><title>公文分类</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            showTooltip('tooltip', 20);
        });

        function doClickRow(classId) {
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnDel').disabled = false;
            document.getElementById('hdnClassID').value = classId;
        }
        function openClassEdit(op, classTypeCode, userCode) {
            var classId = 0;
            var pageTitle = $('#Td_Title').text();

            var types = document.getElementById('hdnTypes').value;
            if (op == 2) {
                classId = document.getElementById('hdnClassID').value;
            }
            var url = "/CommonWindow/SingleClasses/DocClassEdit.aspx?t=" + op + "&ctc=" + classTypeCode + "&code=" + userCode + "&cid=" + classId + "&f=" + types + "&title=" + escape(pageTitle);
            top.ui.openWin({ title: '人员类别', url: url, width: 400, height: 220 });
        }
    </script>
</head>
<body class="body_frame" scroll="no">
    <form id="form1" runat="server">
    
    <table width="100%" height="100%" border="0" id="table1" class="table-normal">
        <tr style="display: none">
            <td id="Td_Title" class="td-title" align="center" height="28" runat="server">
                公文分类
            </td>
        </tr>
        <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
                <input type="hidden" id="hdnClassID" name="hdnClassID" runat="server" />

                <input type="hidden" id="hdnTypes" name="hdnTypes" runat="server" />

                <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
            </td></tr>
        <tr>
            <td valign="top">
                <div style=" width: 100%; height: 100%">
                    
                    <asp:DataGrid ID="dgClass" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgClass_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="分类编号">
<ItemTemplate>
                                    <asp:Label ID="TxtClassCode" Width="100%" Text='<%# Convert.ToString(Eval("ClassCode")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="分类名称"><ItemTemplate>
                                    <asp:Label ID="TxtClassName" Width="100%" Text='<%# Convert.ToString(Eval("ClassName")) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="备注">
<ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Remark") %>'>
                                        <asp:Label ID="TxtRemark" Width="100%" Text='<%# Convert.ToString(StringUtility.GetStr(Eval("Remark"), 20)) %>' runat="server"></asp:Label>
                                    </span>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="有效">
<ItemTemplate>
                                    <asp:CheckBox ID="CkbIsValid" Enabled="false" Checked='<%# (Eval("IsValid").ToString() == "1") %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
                    
                </div>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
