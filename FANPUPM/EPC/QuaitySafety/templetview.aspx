<%@ Page Language="C#" AutoEventWireup="true" CodeFile="templetview.aspx.cs" Inherits="TempletView" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>模板列表</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script language="javascript">
        window.onload = function () {
            var purchasePlanTable = new JustWinTable('DGrdList');
        }
        function clickRow(id) {
            document.getElementById('hdnRecordID').value = id;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnDel').disabled = false;
        }
        function OpenTempletEdit(Flag, OpType) {
            var RID = "";
            RID = document.getElementById('hdnRecordID').value;
            var url = "/EPC/QuaitySafety/TempletEdit.aspx?Flag=" + Flag + "&OpType=" + OpType + "&RID=" + RID;
            top.ui._templetview = window; //不可少
            toolbox_oncommand(url, "质量台账模板"); //引用js
            //return window.showModalDialog(url,window,'dialogHeight:200px;dialogWidth:500px;center:1;help:0;status:0;');	
        }
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
<body>
    <form id="Form1" method="post" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        
        <tr>
            <td class="divFooter" style="text-align: left">
                <input id="hdnRecordID" style="width: 10px" type="hidden" name="hdnRecordID" runat="server" />
<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;<asp:Button ID="btnEdit" Text="编 辑" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div style="overflow: auto; width: 100%; height: 100%">
                    <asp:DataGrid ID="DGrdList" DataKeyField="ID" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="rowa"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Left" CssClass="dgheader" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="模板名称" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <asp:Label CssClass="link" ID="lable1" runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
