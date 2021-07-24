<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectTemplate.aspx.cs" Inherits="BudgetManage_Template_SelectTemplate" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>保存为模板</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    
    
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/jscript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvBudget').treetable(false, 'SelectTemplate');
            setControlButton('hfldCheckedIds', undefined, undefined, 'btnSave');
            $("#btnSaveAs").click(function () {
                $("#divSaveTem").dialog({
                    open: function () {
                        $(this).parent().appendTo("form:first");
                        $('#txtTypeName').focus();
                        $(this).bind("keypress.ui-dialog", function (event) {
                            if (event.keyCode == $.ui.keyCode.ENTER) {
                                saveTemName();
                                return false;
                            }
                        });
                    },
                    focus: function () {
                        $('#txtTypeName').focus();
                    },
                    width: 390,
                    height: 200,
                    modal: true,
                    buttons: {
                        "保存": function () {
                            saveTemName();
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });
            setWidthHeight();
        });
        function setWidthHeight() {
            $("#divBudget").width(($("#editBudget").width()) - ($("#td_Left").width()) - 5);
            $("#divBudget").height($(this).height() - $("#editBudget").height() - 15);
            $("#divContent2").height($(this).height() - $("#editBudget").height() - 10);
            $('#div_project').height($(this).height() - $('#editBudget').height() - 30);
        }
        //保存
        function saveTemName() {
            $("#hdtemplateName").val($("#txtTypeName").val());
            var check = $("#hdtemplateName").val();
            if (jw.trim(check) == "") {
                alert("系统提示：\n模板名称不能为空！");
                $('#txtTypeName').focus();
            }
            else {
                if (checkTemplateName(check)) {
                    alert("系统提示：\n模板名称重复！");
                    $('#txtTypeName').focus();
                }
                else {
                    $('#btnSaveTemplate').click();
                }
            }
        }
        //模板名称是否重复
        function checkTemplateName(checkName) {
            var ishave = true;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=TemplateName&Id=' + escape(checkName),
                success: function (data) {
                    if (data == 1) {
                        ishave = false;
                    }
                }

            });
            return ishave;
        }
        function colseWindow() {
            winclose('SelectTemplate', 'BudgetPlaitList.aspx', false);
        }
    </script>
</head>
<body>
    <form id="form1" target="" runat="server">
    <div class="divHeader" style="display: none">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="保存为模板" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2" style="height: 73%; padding: 0pc; overflow: hidden;">
        <table style="width: 100%; vertical-align: top;">
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
                                        <td style="vertical-align: top;">
                                            <div class="pagediv" id="divBudget">
                                                <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TemplateItemId,OrderNumber,SubCount" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
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
        <div class="divFooter">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right;">
                        <input type="button" id="btnSaveAs" value="另存为新模板" style="width: 90px;" runat="server" />

                        <asp:Button ID="btnSave" Text="保存" disabled="disabled" Style="cursor: pointer;" OnClick="btnSave_Click1" runat="server" />
                        <input type="button" id="btnCancel" value="取消" onclick="colseWindow()" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divSaveTem" title="另存为模板" style="display: none">
        <table>
            <tr>
                <td class="word">
                    类型名称
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTypeName" BackColor="#FEFEF4" Height="15px" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSaveTemplate" Text="保存" Style="display: none;" OnClick="btnSaveTemplate_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hdDropVal" runat="server" />
    <asp:HiddenField ID="hdtemplateName" runat="server" />
    </form>
</body>
</html>
