<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquAcceptanceList.aspx.cs" Inherits="Equ_EquAcceptance_EquAcceptanceList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备验收报告</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />
<link rel="stylesheet" type="text/css" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/wf.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwEquAcceptance');
            replaceEmptyTable('emptyEquAcceptance', 'gvwEquAcceptance');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldId');
            $('#btnAdd').click(rowAdd);
            $('#btnEdit').click(rowEdit);
            $('#btnQuery').click(queryEquAcceptance);
            // 新增
            function rowAdd() {
                parent.desktop.EquAcceptanceEdit = window;
                var url = "/Equ/EquAcceptance/EquAcceptanceEdit.aspx?action=add";
                toolbox_oncommand(url, "新增设备验收");
            }
            function rowEdit() {
                parent.desktop.EquAcceptanceEdit = window;
                var url = "/Equ/EquAcceptance/EquAcceptanceEdit.aspx?action=edit&id=" + $('#hfldId').val();
                toolbox_oncommand(url, "编辑设备验收");
            }
            function queryEquAcceptance() {
                viewopen('/Equ/EquAcceptance/EquAcceptanceView.aspx?ic=' + $('#hfldId').val(), 820, 500, '查看设备验收');
            }
            // 审核
            setWfButtonState2(jwTable, 'WF1');
            // 显示被截取的信息
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
                                固定资产采购单编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPurchaseCode" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                验收日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtAcceptDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
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
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="166" BusiClass="001" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table class="tab">
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div>
                                    <asp:GridView ID="gvwEquAcceptance" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwEquAcceptance_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyEquAcceptance" class="gvdata">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                        <input id="chk1" type="checkbox" />
                                                    </th>
                                                    <th scope="col" style="width: 25px;">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        固定资产采购单编号
                                                    </th>
                                                    <th scope="col">
                                                        验收人
                                                    </th>
                                                    <th scope="col">
                                                        验收日期
                                                    </th>
                                                    <th scope="col">
                                                        流程状态
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="固定资产采购单编号"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="验收人" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="验收日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
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
