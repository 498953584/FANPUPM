<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowList.aspx.cs" Inherits="EPC_Workflow_WorkFlowList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyTable', 'gvWorkFlow');
            var jwTable = new JustWinTable('gvWorkFlow');
            showTooltip('tooltip', 30);
        });

        function ClickRow(recordid, BusinessCode, BusinessClass) {
            document.getElementById('HdnRecoreId').value = recordid;
            document.getElementById('btnViewWF').disabled = false;
            document.getElementById('btnWFPrint').disabled = false;
            document.getElementById('HdnBusinessCode').value = BusinessCode;
            document.getElementById('HdnBusinessClass').value = BusinessClass;
        }
        ///查看审核
        function OpenViewWF() {
            var BusinessCode = document.getElementById('HdnBusinessCode').value;
            var BusinessClass = document.getElementById('HdnBusinessClass').value;
            var rid = document.getElementById('HdnRecoreId').value;
            var url = "/EPC/Workflow/AuditViewWF.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
            window.open(url, '', "left=100,top=50,width=600,height=460,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
        }
        //查看审核记录
        function OpenPrintWF() {
            var BusinessCode = document.getElementById('HdnBusinessCode').value;
            var BusinessClass = document.getElementById('HdnBusinessClass').value;
            var rid = document.getElementById('HdnRecoreId').value;
            var url = "/EPC/Workflow/AuditViewPrint.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
            window.open(url, '', "left=100,top=50,width=800,height=760,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
        }

        function query(url, ic) {
            if (url.indexOf('?') != -1) {
                url += "&";
            }
            else {
                url += "?";
            }
            url += "ic=" + ic;
            viewopen(url, "审核内容查看");
        }

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ codeinput: id, nameinput: name });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0px" cellspacing="0px" width="100%">
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            流程事项
                        </td>
                        <td>
                            <asp:TextBox ID="txtclassName" Width="150px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            模板名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtTemplateName" Width="150px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            发起人
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtOrganizer" Style="width: 97px; height: 15px; border: none;
                                    line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldOrganizer','txtOrganizer');" runat="server" />

                            </span>
                            <input id="hfldOrganizer" type="hidden" runat="server" />

                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            发起时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime" Width="150px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            流程类别
                        </td>
                        <td>
                            <asp:DropDownList ID="dropBusinessClass" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            流程状态
                        </td>
                        <td>
                            <asp:DropDownList ID="dropWFState" Width="100%" runat="server"><asp:ListItem Value="" Text="所有" /><asp:ListItem Value="0" Text="审核中" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-2" Text="驳回" /><asp:ListItem Value="-3" Text="重报" /></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                        </td>
                        <td colspan="3">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <input type="button" id="btnViewWF" value="查看审核" disabled="disabled" style="width: 80px"
                                onclick="OpenViewWF();return false;" />
                            <input type="button" id="btnWFPrint" value="查看审核记录" disabled="disabled" style="width: 100px"
                                onclick="OpenPrintWF();return false;" />
                            <asp:HiddenField ID="HdnRecoreId" runat="server" />
                            <asp:HiddenField ID="HdnBusinessCode" runat="server" />
                            <asp:HiddenField ID="HdnBusinessClass" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:GridView ID="gvWorkFlow" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvWorkFlow_RowDataBound" runat="server">
<EmptyDataTemplate>
                                    <table id="emptyTable">
                                        <tr class="header">
                                            <th scope="col">
                                                序号
                                            </th>
                                            <th scope="col">
                                                流程事项
                                            </th>
                                            <th scope="col">
                                                模板名称
                                            </th>
                                            <th scope="col">
                                                发起人
                                            </th>
                                            <th scope="col">
                                                参与审核人
                                            </th>
                                            <th scope="col">
                                                流程状态
                                            </th>
                                            <th scope="col">
                                                发起时间
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号">
<ItemTemplate>
                                            <%# Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %></ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程事项"><ItemTemplate>
                                            <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
                        <span class="link tooltip" title='<%# Eval("wfname").ToString() %>' onclick=" query('<%# Eval("DoWithUrl").ToString() %>','<%# Eval("InstanceCode").ToString() %>')">
                        <%# StringUtility.GetStr(Eval("wfname").ToString(), 30) %>
                       </span></asp:HyperLink>
                                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="模板名称" DataField="TemplateName" /><asp:BoundField HeaderText="发起人" DataField="OrganigerName" /><asp:BoundField HeaderText="参与审核人" DataField="auditor" /><asp:TemplateField HeaderText="流程状态" ItemStyle-Width="50px"><ItemTemplate>
                                            <%# Common2.GetState(Eval("state").ToString()) %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发起时间" ItemStyle-Width="80px"><ItemTemplate>
                                            <%# Eval("StartTime") %>
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                            </webdiyer:AspNetPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    </form>
</body>
</html>
