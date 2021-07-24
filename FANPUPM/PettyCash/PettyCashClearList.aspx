<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashClearList.aspx.cs" Inherits="PettyCash_PettyCashClearList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金清理</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var cvJtbl = new JustWinTable('gvwPettyCash');
            setWfButtonState2(cvJtbl, 'WF1');
            cvJtbl.registClickTrListener(function () {
                $('#hfldPettyCashIds').val(this.id);
                $('#btnClearCash').attr('disabled', false);
                $('#hfldRepayCashId').val(this.id);
                $('#btnQuery').attr('disabled', false);

                addLinkEvent();   //添加超链接事件
                replaceEmptyTable('gvwPettyCashEmpty', 'gvwPettyCash');
            });
        });

        // 清理备用金
        function ClearCash() {
            top.ui._PClearedit = window;
            var url = '/PettyCash/PettyCashClearEdit.aspx?ic=' + $('#hfldPettyCashIds').val();
            top.ui.openWin({ title: '清理备用金', url: url, width: 500, height: 260 });
        }

        // 添加超链接事件
        function addLinkEvent() {
            $('.link').click(function () {
                var $tr = $(this).parents('tr')
                var id = $tr.attr('id');
                var state = $tr.attr('state');
                var url = '/PettyCash/PettyCashClearDetail.aspx?state=0&ic=';
                if (state == '0')
                    url = '/PettyCash/PettyCashClearDetail.aspx?ic=';
                url += id;
                top.ui.openTab({ title: '清理备用金', url: url });
            });
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 101px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd" style="width: 50px;">
                                申请日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="width: 50px;">
                                至
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="width: 50px;">
                                申请金额
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartCash" CssClass="easyui-numberbox" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="width: 50px;">
                                至
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndCash" CssClass="easyui-numberbox" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                事项
                            </td>
                            <td>
                                <asp:TextBox ID="txtMatter" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="width: 50px;">
                                清理状态
                            </td>
                            <td>
                                <asp:DropDownList ID="dropState" runat="server"><asp:ListItem Value="" Text="" /><asp:ListItem Value="0" Text="未清理" /><asp:ListItem Value="1" Text="已清理" /></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="td_buttons" class="divFooter" style="text-align: left;">
                    <span id="span_buttons">
                        <input type="button" id="btnClearCash" value="清理" onclick="ClearCash()" disabled="disabled" />
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwPettyCash" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwPettyCash_RowDataBound" DataKeyNames="Id,State" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" cellspacing="0" rules="all" border="1" id="gvwPettyCashEmpty"
                                style="width: 100%; border-collapse: collapse;">
                                <tr class="header">
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        申请日期
                                    </th>
                                    <th scope="col" style="width: 200px;">
                                        申请金额
                                    </th>
                                    <th scope="col" style="width: 100px;">
                                        事项
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        用款日期
                                    </th>
                                    <th scope="col">
                                        项目
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        预报金额
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        核销金额
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        未报金额
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        欠款金额
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        是否清理
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
                                    <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请日期" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <span class="link">
                                        <%# System.Convert.ToDateTime(Eval("ApplicationDate")).ToString("yyyy-MM-dd") %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Cash" HeaderText="申请金额" ItemStyle-Width="120px" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Matter" HeaderText="事项" /><asp:TemplateField HeaderText="用款日期" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# System.Convert.ToDateTime(Eval("CashDate")).ToString("yyyy-MM-dd") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                    <span class="tooltip" title='<%# (Eval("Project") == null) ? "" : Eval("Project.PrjName").ToString() %>'>
                                        <%# (Eval("Project") == null) ? "" : StringUtility.GetStr(Eval("Project.PrjName"), 20) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预报金额" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input"><ItemTemplate>
                                     <%# GetAmountPCash(Eval("Id").ToString(), true) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="核销金额" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# GetAmountPCash(Eval("Id").ToString(), false) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="未报金额" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input"><ItemTemplate>
                                   <%# System.Convert.ToDecimal(Eval("Cash")) - base.GetAmountPCash(Eval("Id").ToString(), true) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="欠款金额" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input"><ItemTemplate>
                                   <%# System.Convert.ToDecimal(Eval("Cash")) - base.GetAmountPCash(Eval("Id").ToString(), false) - System.Convert.ToDecimal(Eval("RepayCash")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否清理" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# (Eval("State").ToString() == "0") ? "未清理" : "已清理" %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPettyCashIds" runat="server" />
    <asp:HiddenField ID="hfldRepayCashId" runat="server" />
    </form>
    <link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
    <link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
</body>
</html>
