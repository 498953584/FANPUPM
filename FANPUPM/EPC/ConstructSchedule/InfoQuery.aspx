<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoQuery.aspx.cs" Inherits="TypeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>TypeList</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2S.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>

    <script language="javascript">
     window.onload = function() {
            var purchasePlanTable = new JustWinTable('dgClass');
        }
        function openClassEdit(LogId) {
            var pageTitle = "施工日志"; //document.getElementById('Td_Title').innerText ;	
           
            var url = "/epc/ConstructSchedule/ConstructionLogAdd.aspx?t=3&LogId=" + LogId + "&title=" + escape(pageTitle) + "&pmId= " + document.getElementById('hdnPmId').value;

            var title = "查看施工日志";
            parent.parent.desktop.flowclass = window;
            toolbox_oncommand(url, title);
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
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr id="header">
            <td>
                施工日志查询
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" width="100%">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtPart" Width="0px" Visible="false" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            记录人
                        </td>
                        <td>
                            <asp:TextBox ID="txtOperations" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            施工日期
                        </td>
                        <td>
                        <JWControl:DateBox ID="workdate" Width="95px" Height="18px" runat="server"></JWControl:DateBox>-<JWControl:DateBox ID="enddate" Width="95px" Height="18px" runat="server"></JWControl:DateBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                    </tr>
                </table>
                <input id="hdnPmId" type="hidden" runat="server" />

                <input type="hidden" id="hdnLogID" name="hdnClassID" runat="server" />

				<input id="hdnType" type="hidden" name="hdnType" runat="server" />

            </td>
        </tr>
        <tr>
            <td height="100%" valign="top">
                <asp:DataGrid ID="dgClass" DataKeyField="logid" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15" OnItemDataBound="dgClass_ItemDataBound" OnPageIndexChanged="dgClass_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                <%# this.dgClass.CurrentPageIndex * this.dgClass.PageSize + this.dgClass.Items.Count + 1 %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="编号">
<ItemTemplate>
                              <span class="link" onclick="openClassEdit('<%# Eval("logid").ToString() %>')">
      <%# Eval("code") %></span> 
                               
                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="天气">
<ItemTemplate>
                                <%# Eval("amweather") %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="daycontent" HeaderText="工作情况"></asp:BoundColumn><asp:BoundColumn DataField="beton" HeaderText="工作记要"></asp:BoundColumn><asp:TemplateColumn HeaderText="记录人">
<ItemTemplate>
                                <%# Eval("operations") %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="施工内容" Visible="false">
<ItemTemplate>
                                <%# Eval("daycontent") %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="thisDate" HeaderText="施工日期" DataFormatString="{0:yyyy年MM月dd日}"></asp:BoundColumn></Columns><PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
