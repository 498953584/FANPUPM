<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipOilWearAnalysis.aspx.cs" Inherits="Equ_Report_Ship_ShipOilWearAnalysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>单船油耗分析</title><link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/wf.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwOilWear');
            replaceEmptyTable('emptyOilWear', 'gvwOilWear');
            // 显示被截取的信息
            jw.tooltip();
        })
        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hfldProjectId', nameinput: 'txtProjectName' });
        }
        // 选择设备
        function selectEquipmentCode() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            top.ui.callback = function (equInfo) {
                $('#hfldShipId').val(equInfo.id);
                $('#txtShipCode').val(equInfo.code);
            };
            top.ui.openWin({ title: '选择设备', url: url, width: 1000, height: 500 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            施工日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            至:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" Width="100%" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 130px">
                                <asp:TextBox ID="txtProjectName" data-options="validType:'validQueryChars[50]'" Style="width: 100px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                <img alt="选择项目" onclick="openProjPicker();" src="../../../images/icon.bmp" style="float: right;" />
                            </span>
                            <asp:HiddenField ID="hfldProjectId" runat="server" />
                        </td>
                        <td class="descTd">
                            船机编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtShipCode" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv" style="width: 100%">
                    <asp:GridView ID="gvwOilWear" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvwOilWear_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="emptyOilWear">
                                <tr class="header">
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th>
                                        项目名称
                                    </th>
                                    <th>
                                        分部分项
                                    </th>
                                    <th>
                                        施工地点
                                    </th>
                                    <th>
                                        施工日期
                                    </th>
                                    <th>
                                        挖泥数量
                                    </th>
                                    <th>
                                        挖深（m）
                                    </th>
                                    <th>
                                        定额油耗
                                    </th>
                                    <th>
                                        预算油耗
                                    </th>
                                    <th>
                                        施工船只编号
                                    </th>
                                    <th>
                                        实际加油数量
                                    </th>
                                    <th>
                                        差异数量
                                    </th>
                                    <th>
                                        原因分析
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分部分项"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工地点"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="定额油耗" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="挖泥数量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="挖深（m）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算油耗" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工船只编号"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际加油数量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="差异数量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="原因分析"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
