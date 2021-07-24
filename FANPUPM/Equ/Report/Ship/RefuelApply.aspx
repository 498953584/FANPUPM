<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefuelApply.aspx.cs" Inherits="Equ_Report_Ship_RefuelApply" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>加油申请单</title><link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwRefuelApply');
            replaceEmptyTable('emptyRefuelApply', 'gvwRefuelApply');
            // 显示被截取的信息
            jw.tooltip();
        })
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
                            申请加油日期
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
                    <asp:GridView ID="gvwRefuelApply" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvwRefuelApply_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="emptyRefuelApply">
                                <tr class="header">
                                    <th rowspan="2">
                                        序号
                                    </th>
                                    <th colspan="10">
                                        加油概况
                                    </th>
                                    <th colspan="3">
                                        审核栏
                                    </th>
                                </tr>
                                <tr class="header">
                                    <th>
                                        船机编号
                                    </th>
                                    <th>
                                        开工至今该项目<br />
                                        该船累计加油数量（吨）
                                    </th>
                                    <th>
                                        现有库存量（吨）
                                    </th>
                                    <th>
                                        本次申请数量(吨）
                                    </th>
                                    <th>
                                        本项目<br />
                                        该船预计完成工程量（m3)
                                    </th>
                                    <th>
                                        开工至今该船累计完成<br />
                                        该项目工程量（m3)/累计施工时间
                                    </th>
                                    <th>
                                        挖深（m)
                                    </th>
                                    <th>
                                        申请加油地点
                                    </th>
                                    <th>
                                        申请加油时间
                                    </th>
                                    <th>
                                        是否委托采购
                                    </th>
                                    <th>
                                        申请船主
                                    </th>
                                    <th>
                                        本次批准加油数量<br />
                                        （吨，船机部复核）
                                    </th>
                                    <th>
                                        船机部审核人员
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="船机编号"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开工至今该项目<br />该船累计加油数量（吨）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="现有库存量（吨）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次申请数量(吨）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本项目<br />该船预计完成工程量（m3）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开工至今该船累计完成<br />该项目工程量（m3）/累计施工时间" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="挖深（m）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请加油地点"><ItemTemplate>
                                    <span class="tooltip" title=''>
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请加油时间" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否委托采购" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请船主"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次批准加油数量<br />（吨，船机部复核）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="船机部审核人员" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    
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
