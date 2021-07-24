<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkclasslist.aspx.cs" Inherits="CheckClassList" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>CheckClassList</title>
    <script src="../../../../Script/jquery.js" type="text/javascript"></script>
    <link href="../../../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script src="../../../../Script/jquery.tooltip/jquery.tooltip.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="../../../../Script/jw.js" type="text/javascript"></script>
    <script language="javascript">
        var pk = null;
        window.onload = function () {
            var purchasePlanTable = new JustWinTable('DataGrid1');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
            jw.tooltip();
        }
        function setvalue(obj) {
            pk = obj;
            document.getElementById("TextBox_pk").value = obj;
            if (document.getElementById("Button_edit") != null)
                document.getElementById("Button_edit").disabled = false;
            if (document.getElementById("Button_del") != null)
                document.getElementById("Button_del").disabled = false;
        }
        function ShowInsertWindow() {
            var url;
            var title;
            url = "/EPC/17/Entpm/PrjCheck/checkclassmanage.aspx";
            title = "新增检查类别";
			top.ui._checkclasslist = window;
            toolbox_oncommand(url, title);		
        }
        function ShowEditWindow() {
            if (pk == null) {
                window.alert('请选择记录！');
            }
            else {
                var url;
                var title;
                url = "/EPC/17/Entpm/PrjCheck/checkclassmanage.aspx?pk=" + pk + "";
                title = "编辑检查类别";
				top.ui._checkclasslist = window;
                toolbox_oncommand(url, title);
            }
        }
        $(function () {
            $("#DataGrid1 tr").each(function () {
                $(this).click(function () {
                    var _VID = $(this).attr("id");
                    $("#hidenSortID").val(_VID);
                });
            });
        });
    </script>
    <style type="text/css">
        .dgheader
        {
            background-color: #EEF2F5;
            white-space: nowrap;
            overflow: hidden;
            font-weight: normal;
            text-align: center;
            border-color: #CADEED;
            color: #727FAA;
            padding-left: 6px;
            padding-right: 6px;
        }
    </style>
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="hidenSortID" runat="server" />
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="Button_add" Text="添加" runat="server" />
                <asp:Button ID="Button_edit" Text="编辑" Enabled="false" runat="server" />
                <asp:Button ID="Button_del" Text="删除" Enabled="false" OnClick="Button_del_Click" runat="server" />
                <asp:TextBox ID="TextBox_pk" Width="0px" Enabled="false" BorderStyle="None" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div style="width: 100%; height: 100%">
                    <asp:DataGrid ID="DataGrid1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><SelectedItemStyle HorizontalAlign="Left"></SelectedItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="编号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px"><ItemTemplate>
                                    <%# Container.ItemIndex + 1 + this.DataGrid1.CurrentPageIndex * this.DataGrid1.PageSize %>
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="项目检查名称"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("ItemInspectSortName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("ItemInspectSortName").ToString(), 20) %>
                                    </span>
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("Remark").ToString(), 40) %>
                                    </span>
                                </ItemTemplate></asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
