<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquOilEnter.aspx.cs" Inherits="Equ_OilWearManage_EquOilEnter" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>油量入库登记</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwOilEnter');
            replaceEmptyTable('emptyOilEnter', 'gvwOilEnter');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldId');
            $('#btnAdd').click(rowAdd);
            $('#btnEdit').click(rowEdit);
            $('#btnQuery').click(queryOilEnter);
            function rowAdd() {
                parent.desktop.EquOilEnterEdit = window;
                var url = "/Equ/OilWearManage/EquOilEnterEdit.aspx?action=add";
                toolbox_oncommand(url, "新增入库单");
            }
            function rowEdit() {
                parent.desktop.EquOilEnterEdit = window;
                var url = "/Equ/OilWearManage/EquOilEnterEdit.aspx?action=edit&id=" + $('#hfldId').val();
                toolbox_oncommand(url, "编辑入库单");
            }
            function queryOilEnter() {
                viewopen('/Equ/OilWearManage/EquOilEnterView.aspx?id=' + $('#hfldId').val(), 820, 500, '查看入库单');
            }
            //显示被截取的信息
            jw.tooltip();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
            height: 91%; vertical-align: top;">
            <tr style="height: 1px">
                <td style="vertical-align: top;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">
                                入库单号
                            </td>
                            <td>
                                <asp:TextBox ID="txtEnterCode" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                入库日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtEnterDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <input type="button" id="btnAdd" value="新增" />
                    <input type="button" id="btnEdit" value="编辑" disabled="disabled" />
                    <input type="button" id="btnQuery" value="查看" disabled="disabled" />
                    <asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table class="tab">
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div>
                                    <asp:GridView ID="gvwOilEnter" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwOilEnter_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                            <table id="emptygvwOilEnter" class="gvdata">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                        <input id="chk1" type="checkbox" />
                                                    </th>
                                                    <th scope="col" style="width: 25px;">
                                                        序号
                                                    </th>
                                                    <th scope="col" style="width: 25px;">
                                                        入库单号
                                                    </th>
                                                    <th scope="col">
                                                        入库日期
                                                    </th>
                                                    <th scope="col">
                                                        项目
                                                    </th>
                                                    <th scope="col">
                                                        单价
                                                    </th>
                                                    <th scope="col">
                                                        数量
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入库单号"><ItemTemplate>
                                                    <span class="link" onclick="toolbox_oncommand('/Equ/OilWearManage/EquOilEnterView.aspx?id=','查看入库单')">
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入库日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价（元/L）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量（L）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额（元）" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldId" runat="server" />
    </form>
</body>
</html>
