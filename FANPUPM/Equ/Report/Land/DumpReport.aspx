<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DumpReport.aspx.cs" Inherits="Equ_Report_Land_DumpReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>自卸车产量表</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />
<link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var gvBudget = new JustWinTable('gvReport');
            replaceEmptyTable('empetyFillTable', 'gvReport');
            //setWidthHeight();
            jw.tooltip();
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 70);
            $('#divBudget').width($('#divContent').width() - 2);
        }
        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: '', nameinput: 'txtPrjName' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">
                                上报日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" Height="15px" Width="130px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                至
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" Height="15px" Width="130px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                自卸车编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtEquCode" Height="15px" Width="130px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                自卸车名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtEquName" Height="15px" Width="130px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                项目
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 131px">
                                    <asp:TextBox ID="txtPrjName" Style="width: 105px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                    <img alt="选择项目" onclick="openProjPicker();" src="../../../images/icon.bmp" style="float: right;" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                    <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
                        <asp:GridView ID="gvReport" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvReport_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="empetyFillTable">
                                    <tr class="header">
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            上报日期
                                        </th>
                                        <th scope="col">
                                            施工日期
                                        </th>
                                        <th scope="col">
                                            项目名称
                                        </th>
                                        <th scope="col">
                                            自卸车编号
                                        </th>
                                        <th scope="col">
                                            自卸车名称
                                        </th>
                                        <th scope="col">
                                            地磅房
                                        </th>
                                        <th scope="col">
                                            流水号
                                        </th>
                                        <th scope="col">
                                            车号
                                        </th>
                                        <th scope="col">
                                            抛石船编号
                                        </th>
                                        <th scope="col">
                                            货名
                                        </th>
                                        <th scope="col">
                                            毛重
                                        </th>
                                        <th scope="col">
                                            空重
                                        </th>
                                        <th scope="col">
                                            净重
                                        </th>
                                        <th scope="col">
                                            方数
                                        </th>
                                        <th scope="col">
                                            车数
                                        </th>
                                        <th scope="col">
                                            过磅员
                                        </th>
                                        <th scope="col">
                                            毛重日期
                                        </th>
                                        <th scope="col">
                                            毛重时间
                                        </th>
                                        <th scope="col">
                                            抛弃地点
                                        </th>
                                        <th scope="col">
                                            备注
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报日期">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="施工日期">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目名称">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="自卸车编号">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="自卸车名称">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="地磅房">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流水号">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="车号">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="抛石船编号">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="货名">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="毛重" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="空重" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="净重" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="方数" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="车数" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="毛重日期">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                        </webdiyer:AspNetPager>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    </form>
</body>
</html>
