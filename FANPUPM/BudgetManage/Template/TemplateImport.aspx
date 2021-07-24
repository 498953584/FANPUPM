<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateImport.aspx.cs" Inherits="BudgetManage_Template_TemplateImport" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>模板导入</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/jscript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            $('#gvBudget').treetable(true, 'SelectTemplate');
            $('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
            $('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
            setControlButton('hfldCheckedIds', undefined, undefined, 'btnImport');
            $('#gvBudget tr:gt(0)').each(function (index) {
                var $tr = $(this);
                $(this).find(':checkbox').click(function () {
                    var $chk = $(this);
                    $(getAllChildren($tr)).each(function () {
                        $(this).find(':checkbox').attr('checked', $chk.attr('checked'));
                    });
                });
            });
            setWidthHeight();
        }
        function setWidthHeight() {
            $("#divBudget").width(($("#editBudget").width()) - ($("#td_Left").width()) - 5);
            $("#divBudget").height($(this).height() - $("#editBudget").height() - 15);
            $("#divContent2").height($(this).height() - $("#editBudget").height() - 10);
            $('#div_project').height($(this).height() - $('#editBudget').height() - 30);
        }

        function getAllChildren($tr) {
            var children = new Array();
            var layer = parseInt($tr.attr('layer'));
            var $nextAll = $tr.nextAll();
            for (var i = 0; i < $nextAll.length; i++) {
                if (parseInt($nextAll[i].layer) > layer) {
                    children.push($nextAll[i]);
                } else {
                    return children;
                }
            }
            return children;
        }
    </script>
    <style type="text/css">
        .descTd
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader" style="display: none;">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="模板导入" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2" style="height: 73%; padding: 0px; overflow: hidden;">
        <table style="height: 100%; width: 100%;">
            <tr>
                <td style="vertical-align: top; width: 100%;">
                    <table style="width: 100%;">
                        <tr>
                            <td id="td_Left" style="width: 194px; vertical-align: top; height: 100%;">
                                <div>
                                    <asp:DropDownList ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div id="div_project" class="pagediv" style="width: 194px;">
                                    <asp:TreeView ID="tvBudget" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="vertical-align: top; border-left: solid 2px #CADEED;">
                                <table border="0" class="tab">
                                    <tr>
                                        <td style="height: 100%;">
                                            <div id="divBudget">
                                                <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TemplateItemId,OrderNumber,SubCount" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                                            </HeaderTemplate><ItemTemplate>
                                                                <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("TemplateItemId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                                <%# Eval("No") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                                                                <%# Eval("ItemCode") %>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                                                <%# Eval("ItemName") %>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
                                                                <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                                                                    <%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
                                                                </span>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                                                                <%# Eval("TypeName") %>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                                                <%# Eval("Unit") %>
                                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="editBudget" style="height: 15px; vertical-align: bottom; width: 100%;">
        <table class="divFooter" style="width: 100%;">
            <tr>
                <td align="right" style="width: 100%;">
                    <asp:Button ID="btnImport" Text="导入" disabled="disabled" OnClick="btnImport_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="parent.desktop.BudgetPlaitList=null;top.frameWorkArea.window.desktop.getActive().close();" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    </form>
</body>
</html>
