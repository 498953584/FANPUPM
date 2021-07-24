<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjinfoChangeFlowQuery.aspx.cs" Inherits="TenderManage_PrjinfoChangeFlowQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvDataInfo');
            //jw.tooltip('gvDataInfo');
            replaceEmptyTable('emptyTable', 'gvDataInfo');
            if (getRequestParam('isShowQ') != '1') {
                $("#queryTabel").hide();
            }
        });
    </script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <div id="queryTabel">
        <table class="queryTable" cellpadding="3px" cellspacing="0px">
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    调整日期
                </td>
                <td>
                    <asp:TextBox ID="txtStartTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="descTd" style="white-space: nowrap;">
                    至
                </td>
                <td>
                    <asp:TextBox ID="txtEndTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td style="white-space: nowrap;">
                    <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent" style="overflow: auto;">
        <asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="Id,PrjId,FlowState" runat="server">
<EmptyDataTemplate>
                <table id="emptyTable">
                    <tr>
                        <th class="header">
                            序号
                        </th>
                        <th class="header">
                            调整状态
                        </th>
                        <th class="header">
                            调整原因
                        </th>
                        <th class="header">
                            调整时间
                        </th>
                        <th class="header">
                            项目名称
                        </th>
                        <th class="header">
                            备注
                        </th>
                        <th class="header">
                            调整人
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调整状态"><ItemTemplate>
                        <%# GetStateName(Eval("OldState").ToString()) + "->" + base.GetStateName(Eval("ChangeState").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="调整原因"><ItemTemplate>
                        <%# StringUtility.GetStr(Eval("ChangeReason").ToString(), 25) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="调整时间">
<ItemTemplate>
                        <%# Common2.GetTime(Eval("ChangeTime")) %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                        <span class="tooltip" title="<%# Eval("Note") %>">
                            <%# StringUtility.GetStr(Eval("Note").ToString(), 25) %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="调整人"><ItemTemplate>
                        <%# WebUtil.GetUserNames(Eval("ChangeUser").ToString()) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle></asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
