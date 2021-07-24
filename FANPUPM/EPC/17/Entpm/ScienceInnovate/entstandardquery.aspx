<%@ Page Language="C#" AutoEventWireup="true" CodeFile="entstandardquery.aspx.cs" Inherits="EntStandardQuery" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>EntStandardQuery</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script type="text/javascript" src="../../../../Script/jquery.js"></script>

    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>

    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script language="javascript">
        window.onload = function() {
            var purchasePlanTable = new JustWinTable('DGrdStandard');
        }
        function clickRow(Id, type, levels) {
            document.getElementById("HdnId").value = Id;
            if (type == "1") {
                if (levels != "1") {

                    document.getElementById("BtnUpd").disabled = false;
                    document.getElementById("BtnDel").disabled = false;
                }
                document.getElementById("BtnView").disabled = false;
            }
        }
        function rowQuery(path, _title) {
            parent.parent.desktop.AddIncometBalance = window;
            toolbox_oncommand(path, _title);
        }
        function openEdit(opType) {
            var ClassId = document.getElementById("HdnClassId").value;
            var Id = document.getElementById("HdnId").value;
            var url;
            var title;
            switch (opType) {
                case 'Add':
                    url = "/EPC/17/Entpm/ScienceInnovate/EntStandardEdit.aspx?Type=Add&ClassId=" + ClassId + "&Id=" + Id;
                    title = "新增企业技术标准";
                    break;
                case 'Upd':
                    url = "/EPC/17/Entpm/ScienceInnovate/EntStandardEdit.aspx?Type=Upd&ClassId=" + ClassId + "&Id=" + Id;
                    title = "编辑企业技术标准";
                    break;
                case 'View':
                    url = "/EPC/17/Entpm/ScienceInnovate/EntStandardEdit.aspx?Type=View&ClassId=" + ClassId + "&Id=" + Id;
                    title = "查看企业技术标准";
                    break;
            }
            //return window.showModalDialog(url,window,'dialogHeight:270px;dialogWidth:500px;center:1;help:0;status:0;');
            parent.parent.desktop.flowclass = window; //不可少
            toolbox_oncommand(url, title); //引用js
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
<body scroll="no" id="id">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server"><tr visible="false" runat="server"><td class="divHeader" runat="server">
                    <asp:Label ID="lblTitle" Text="Label" runat="server"></asp:Label>
                </td></tr><tr runat="server"><td class="divFooter" style="text-align: left; width: 100%" runat="server">
                    <asp:Button ID="BtnAdd" Text="新 增" runat="server" />
                    <asp:Button ID="BtnUpd" Text="编 辑" Enabled="false" runat="server" />
                    <asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" /><input id="HdnClassId" style="width: 72px; height: 22px" type="hidden" size="6" name="Hidden1" runat="server" />
<input id="HdnId" style="width: 72px; height: 22px" type="hidden" size="6" runat="server" />

                    <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                    <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭" name="Button2" runat="server" />

                    <asp:Button ID="BtnView" Text="查 看" Enabled="false" runat="server" />
                </td></tr><tr runat="server"><td valign="top" height="100%" runat="server">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="DGrdStandard" DataKeyField="MainKey" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" OnPageIndexChanged="DGrdStandard_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        &nbsp;<asp:Label ID="Label1" Text='<%# Convert.ToString(Container.ItemIndex + 1) %>' runat="server"></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TechnologyCriterionID" HeaderText="标准代号"></asp:BoundColumn><asp:BoundColumn DataField="TechnologyPromulgateTime" HeaderText="年号"></asp:BoundColumn><asp:HyperLinkColumn DataTextField="TechnologyName" HeaderText="标准名称"></asp:HyperLinkColumn><asp:BoundColumn DataField="TechnologyClassify" HeaderText="标准分类"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:TemplateColumn HeaderText="选择"><ItemTemplate>
                                        <asp:CheckBox ID="CHK" runat="server" />
                                    </ItemTemplate></asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
                </td></tr><tr runat="server"></tr></table>
    </font>
    </form>
</body>
</html>
