<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prjSearList.aspx.cs" Inherits="prjSearList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html>
<head runat="server"><title>OrderQuery</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<link href="../../../Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script language="javascript" type="text/javascript">
        addEvent(window, 'load', function () {

            //        var contractTable = new JustWinTable('grdModules');
            var jwTable = new JustWinTable('gvPrjInfo');
            replaceEmptyTable('emptyTable', 'gvPrjInfo');
            formateTable('gvPrjInfo', 0);
            showTooltip('tooltip', 25);
        })

        function clickRow(obj, moduleCode, isLeaf) {

            doClick(obj, 'grdModules'); //调用样式设置
        }
//        function openQuery(reportid) {
//            var url = "../../../EPC/Query/reportqueryset.aspx?reportid=" + reportid;
//            return window.showModalDialog(url, window, "dialogHeight:266px;dialogWidth:430px;center:1;help:0;status:0;");
//        }

        function rowQueryA(url) {
            parent.desktop.AddIncometContract = window;

            toolbox_oncommand(url, "查看项目");
        }
       
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    
    <table id="Table2" class="table-normal" height="100%" cellspacing="0" cellpadding="0"
        width="100%">
        <tr class="td-toolsbar" height="1">
            <td style="text-align: right; white-space: nowrap;">
                项目编号
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:TextBox ID="txtPrjCode" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right; white-space: nowrap;">
                项目名称
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:TextBox ID="txtprjName" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right; white-space: nowrap;">
                开始时间大于
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <JWControl:DateBox ID="txtStartDate" Width="120px" runat="server"></JWControl:DateBox>
            </td>
            <td style="text-align: right; white-space: nowrap;">
                结束时间小于
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <JWControl:DateBox ID="txtEndDate" Width="120px" runat="server"></JWControl:DateBox>
            </td>
        </tr>
        <tr>
            <td class="divFooter" colspan="8" style="text-align: left;" >
                &nbsp;&nbsp;<asp:Button ID="btnSet" Text="查询" OnClick="btnSearch_Click" runat="server" />
                <asp:Button ID="btnexecl" Text="导 出" OnClick="btnexecl_Click" runat="server" />
                <asp:Button ID="btnWord" Text="导出->Word" Width="0px" Visible="false" OnClick="btnWord_Click" runat="server" />
            </td>
        </tr>
        <tr>
            
            <td valign="top" align="center" colspan="11">
                <table id="Table1" class="tab" style="vertical-align: top;" runat="server"><tr runat="server"><td valign="top" align="center" colspan="3" runat="server">
                            <div id="dvModulesBox">
                                <asp:GridView ID="gvPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,i_ChildNum,PrjState" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyTable">
                                            <tr class="header">
                                                <th scope="col">
                                                    项目编号
                                                </th>
                                                <th scope="col">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    项目名称
                                                </th>
                                                <th scope="col">
                                                    建设单位
                                                </th>
                                                <th scope="col">
                                                    工程造价（万元）
                                                </th>
                                                <th scope="col">
                                                    项目地点
                                                </th>
                                                <th scope="col">
                                                    开始时间
                                                </th>
                                                <th scope="col">
                                                    结束时间
                                                </th>
                                                <th scope="col">
                                                    状态
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:BoundField DataField="TypeCode" HeaderText="序号" ItemStyle-HorizontalAlign="Left" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" ItemStyle-HorizontalAlign="Left" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                
                                                <span class="link tooltip" title="" onclick="OpenView('');">
                                                    
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价（万元）">
<ItemTemplate>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目地点" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                <span class="tooltip" title="">
                                                    
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始时间" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PrjState" HeaderText="项目状态" ItemStyle-HorizontalAlign="Left" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                            
                        </td></tr></table>
                <input id="Hdn1" type="hidden" name="Hdn1" style="width: 0px" runat="server" />

            </td>
        </tr>
    </table>
    </form>
</body>
</html>
