<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="List1" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>List</title><link href="../../../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script src="../../../../Script/jquery.tooltip/jquery.tooltip.js" type="text/javascript"></script>
    <script src="../../../../Script/jw.js" type="text/javascript"></script>
    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var purchasePlanTable = new JustWinTable('DataGrid1');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
            jw.tooltip();
        });
        function CheckDelete() {
            var ClientCode = document.getElementById('hdnClientCode').value;
            if (ClientCode == '0') {
                alert('请选择要删除的列！');
                return false;
            }
            else {
                return confirm('您确定要删除选定的记录吗？');
            }
        }
        function ClickRow(obj) {
            document.getElementById("hdnClientCode").value = obj;

            document.getElementById("Button1").disabled = false;
            document.getElementById("Button2").disabled = false;
            document.getElementById("Button3").disabled = false;
        }
        function OpAdd() {
            var PrjCode=jw.getRequestParam('PrjCode');
           
            var url = "/EPC/17/Ppm/Prog/Add.aspx?prjCode=" + PrjCode + "";
			top.ui._proglist = window;
            toolbox_oncommand(url, "新增信息"); //引用js
        }
        function OpEdit(opEdit) {
            var ClientCode = document.getElementById('hdnClientCode').value;
            if (ClientCode == '0') {
                alert('请选择要编辑的列！');
                return;
            }
            var url = "/EPC/17/Ppm/Prog/Edit.aspx?id=" + ClientCode + "";
            top.ui._proglist = window;
            toolbox_oncommand(url, "编辑信息"); //引用js
        }

        function OpQuery(ClientCode) {
            if (ClientCode == '0') {
                alert('请选择要查看的列！');
                return;
            }
            var url = "/EPC/17/Ppm/Prog/Edit.aspx?op=query&id=" + ClientCode + "";
            top.ui._proglist = window;
            toolbox_oncommand(url, "查看信息"); //引用js
        }

        function OpView() {
            var ClientCode = document.getElementById('hdnClientCode').value;
            if (ClientCode == '0') {
                alert('请选择要编辑的列！');
                return;
            }
            var url = "/EPC/17/Ppm/Prog/Edit.aspx?op=query&id=" + ClientCode + "";
            parent.desktop.flowclass = window; //不可少
            toolbox_oncommand(url, "查看信息"); //引用js
            //window.open("Edit.aspx?op=&id="+ClientCode,"_blank",'width=500,height=240,left=150,top=150');
        }
        function openannexpage() {
            var ClientCode = document.getElementById('hdnClientCode').value;
            //alert(ClientCode);
            if (ClientCode == '0') {
                alert('请选择要查看的列！');
                return;
            }
            window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1743&rc=" + ClientCode + "&at=0&type=1", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
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
    <table id="Table1" width="100%">
        <tr>
            <td class="divFooter" style="text-align: left">
                <input type="button" value="新 增" onclick="OpAdd()">&nbsp;<input type="button" value="编 辑"
                    onclick="OpEdit()" disabled="disabled" id="Button2" name="Button2">&nbsp;<asp:Button ID="Button1" Text="删 除" Enabled="false" OnClick="Button1_Click" runat="server" />
                <input type="button" value="查 看" onclick="OpView()" disabled="disabled" id="Button3"
                    name="Button3">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div style="overflow: auto; width: 100%; height: 100%">
                    <asp:DataGrid ID="DataGrid1" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" DataKeyField="ReportId" AllowPaging="true" PageSize="15" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px"><ItemTemplate>
                                    <%# Container.ItemIndex + 1 + this.DataGrid1.CurrentPageIndex * this.DataGrid1.PageSize %>
                                </ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="ReportTime" HeaderText="曝光时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="曝光内容"><ItemTemplate>
                                    <span  class="link tooltip" title='<%# Eval("Remark").ToString() %>' onclick="OpQuery('<%# Eval("ReportId") %>')">
                                        <%# StringUtility.GetStr(Eval("Remark").ToString(), 43) %>
                                    </span>
                                    
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="曝光部门" HeaderStyle-Width="110px"><ItemTemplate>
                                    <span  class="tooltip" title='<%# Eval("ReportDpt").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("ReportDpt").ToString(), 15) %>
                                    </span>   
                                </ItemTemplate></asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hdnClientCode" value="0" runat="server" />

    </form>
</body>
</html>
