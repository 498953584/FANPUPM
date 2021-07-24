<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectpop.aspx.cs" Inherits="PrjSel" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目信息</title>
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script src="../../../web_client/tree.js" language="javascript" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function clickRow(obj, moduleCode, isLeaf, theCode, theName) {

            document.getElementById('hdnModuleCode').value = moduleCode;
            //待办按钮
            if (moduleCode == "") {
                document.getElementById("btnSave").disabled = isLeaf;
            }
            else {
                document.getElementById("btnSave").disabled = !isLeaf;

            }
            /*在这之前添加你的处理代码*/
            doClick(obj, 'grdModules'); //调用样式设置
            if (window.location.href.indexOf('sm') == -1) {
                if (isLeaf == true) {
                    var prj = window.dialogArguments;
                    prj[0] = theCode;
                    prj[1] = theName;
                }
            }
        }
        function outRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOut(obj); //调用样式设置
        }
        function overRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOver(obj); //调用样式设置
        }

        function switchDisplay(obj, t1, t2) {
            /*在这之前添加你的处理代码*/
            doSwitchDisplay(obj, 'grdModules', 'hdnModuleCodeList', t1, t2, '../../../'); //调用样式设置
        }

        function dbClickRow(obj, theCode, theName, isLeaf) {
            //sm是定义的一个变量，用来标识窗体是用window.open();或是用Window.showModelDialog()的形式打开
            if (window.location.href.indexOf('sm') != -1) {

                window.opener.document.getElementById('txtProName').value = theName;
                window.opener.document.getElementById('hdnProjectCode').value = theCode;
                window.close();
            }
            else {
                if (isLeaf == true) {
                    var prj = window.dialogArguments;
                    prj[0] = theCode;
                    prj[1] = theName;
                    window.close();
                }
            }

        }

        function setnull() {
            var prj = window.dialogArguments;
            prj[0] = "";
            prj[1] = "";
        }
			
		
		
    </script>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" class="table-nomal" height="100%" cellspacing="0" cellpadding="0"
        width="100%">
        <tr class="td-title">
            <td width="20" colspan="2">
                <input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

            </td>
        </tr>
        <tr class="td-title">
            <td colspan="2" style="text-align: left">
                项目编号：<asp:TextBox ID="prjcode" Width="78px" runat="server"></asp:TextBox>&nbsp;
                项目名称：<asp:TextBox ID="prjname" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" colspan="2">
                <div id="dvModulesBox" class="gridBox">
                    <asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="项目编号">
<ItemTemplate>
                                    <asp:Label ID="Label1" name="Label1" Text='<%# System.Convert.ToString(Eval("HeadImg"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    <asp:Label ID="Label2" name="Label2" Text='<%# System.Convert.ToString(Eval("PrjCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TypeCode" HeaderText="序号"></asp:BoundColumn><asp:HyperLinkColumn DataTextField="prjName" HeaderText="项目名称"></asp:HyperLinkColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="工程造价" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地点" DataField="PrjPlace"></asp:BoundColumn><asp:BoundColumn DataField="PrjState" HeaderText="状态"></asp:BoundColumn></Columns></asp:DataGrid>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
        <tr class="td-submit">
            <td align="right">
                <input type="hidden" id="hdn1" name="hdn1" style="width: 10px" runat="server" />

                <input type="hidden" id="hdn2" name="hdn2" style="width: 10px" runat="server" />

                <input type="hidden" id="hdn3" name="hdn3" style="width: 10px" runat="server" />

                <input id="btnSave" type="button" value="确 定" onclick="window.close();" disabled="disabled" />
                <input id="btnCancel" type="button" value="取 消" onclick="setnull();window.close();" />&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <JWControl:PersistScrollPosition ID="PersistScrollPosition2" ControlToPersist="dvModulesBox" runat="server">
    </JWControl:PersistScrollPosition>
    </form>
</body>
</html>
