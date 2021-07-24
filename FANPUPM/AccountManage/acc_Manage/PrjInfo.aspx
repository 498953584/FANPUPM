<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjInfo.aspx.cs" Inherits="PrjInfo" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>项目信息</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />


    <script src="../../../web_client/tree.js" language="javascript" type="text/javascript"></script>

    

    <script language="JavaScript" type ="text/javascript" >

        function clickRow(obj, moduleCode, isLeaf) {
            doClick(obj, 'grdModules'); //调用样式设置

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
        function ClickBtn(op) {
            var moduleCode = document.getElementById('hdnModuleCode').value;
            var url = ""
            var re = true;
            switch (op.toLowerCase()) {
                case "add":
                    url = "PrjInfoEdit.aspx?TypeCode=" + moduleCode + "&op=add";
                    re = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;');
                    break;
                case "upd":
                    url = "PrjInfoEdit.aspx?TypeCode=" + moduleCode + "&op=upd";
                    re = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;');
                    break;
                case "view":
                    url = "PrjInfoEdit.aspx?TypeCode=" + moduleCode + "&op=upd";
                    re = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;');
                    break;
                case "del":
                    re = confirm("确定要删除该项目吗？");
                    break;
                case "user":

                    url = "Popedomdis.aspx?PrjCode=001&PrjGuid=&subpc=" + moduleCode;
                    //re = window.showModalDialog(url,window,'dialogHeight:520px;dialogWidth:600px;center:1;help:0;status:0;');
                    window.open(url, '', 'left=150,top=50,width=600,height=500,toolbar=no,status=yes,scrollbars=yes,resizable=no');
                    break;
            }
            return re;
        }
   
    </script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no" onload="selrow1('grdModules')">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" class="table-normal" height="100%" cellspacing="0" cellpadding="0"
        width="100%">
        <tr class="td-toolsbar">
            <td width="20">
                <input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

            </td>
            <td valign="middle" align="right" height="24">
                <asp:Button ID="Button1" Text="确定" OnClick="btnOK_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" colspan="2">
                <div id="dvModulesBox" class="gridBox">
                    <asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chkAcc" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="项目编号">
<ItemTemplate>
                                    <asp:Label ID="Label1" name="Label1" Text='<%# Convert.ToString(Eval("DataItem.HeadImg")) %>' runat="server"></asp:Label>
                                    <asp:Label ID="Label2" name="Label2" Text='<%# Convert.ToString(Eval("DataItem.PrjCode")) %>' runat="server"></asp:Label>
                                    
                                    <asp:HiddenField ID="hdfPrjNum" Value='<%# Convert.ToString(Eval("prjGuid")) %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TypeCode" HeaderText="序号"></asp:BoundColumn><asp:HyperLinkColumn DataTextField="prjName" HeaderText="项目名称"></asp:HyperLinkColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="工程造价"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地点" DataField="PrjPlace"></asp:BoundColumn><asp:BoundColumn DataField="PrjState" HeaderText="状态"></asp:BoundColumn></Columns></asp:DataGrid></div>
            </td>
        </tr>
        <tr height="0" style="display: none">
            <td valign="top" colspan="2">
                <iframe id="FraFlow" name="FraFlow" src="" frameborder="no" width="100%" height="100%">
                </iframe>
            </td>
        </tr>
    </table>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <JWControl:PersistScrollPosition ID="PersistScrollPosition2" ControlToPersist="dvModulesBox" runat="server">
    </JWControl:PersistScrollPosition>
     <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>

    </form>
</body>
</html>
