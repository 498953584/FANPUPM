<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFlowEnlist.aspx.cs" Inherits="oa_UserDefineFlow_MyFlowEnlist" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>我参与的流程</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('GVEnlist');
            replaceEmptyTable('emptyTable', 'GVEnlist');
        });

        function ClickRow(recordid, BusinessCode, BusinessClass) {
            document.getElementById('HdnRecoreId').value = recordid;
            document.getElementById('btnViewWF').disabled = false;
            document.getElementById('btnWFPrint').disabled = false;
            document.getElementById('HdnBusinessCode').value = BusinessCode;
            document.getElementById('HdnBusinessClass').value = BusinessClass;

        }

        //多人选择
        function pickMutiPerson() {
            jw.selectMultiUser({ nameinput: 'txtjoiner', codeinput: 'hidjoiner' });
        }

        //单人选择
        function pickSinglePerson() {
            jw.selectOneUser({ nameinput: 'txtUserCode', codeinput: 'hidorganizer' });
        }

        ///查看审核
        function OpenViewWF() {
            var BusinessCode = document.getElementById('HdnBusinessCode').value;
            var BusinessClass = document.getElementById('HdnBusinessClass').value;
            var rid = document.getElementById('HdnRecoreId').value;
            var url = "/epc/Workflow/AuditViewWF.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "left=100,top=50,width=600,height=460,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
        }
        //查看审核记录
        function OpenPrintWF() {
            var BusinessCode = document.getElementById('HdnBusinessCode').value;
            var BusinessClass = document.getElementById('HdnBusinessClass').value;
            var rid = document.getElementById('HdnRecoreId').value;
            var url = "/epc/Workflow/AuditViewPrint.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
            window.open(url, '', "left=100,top=50,width=800,height=600,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
        }

        //查看
        function OpenLock(strurl, rid) {
            var url = strurl + "?ic=" + rid;
			window.open(url, '', "width=600,height=260,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
        }
        //查看
        function OpenLook(strurl) {
            return window.open(strurl, '_blank', "left=100,top=50,width=600,height=400,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1", false);
        }

        function setHeight() {
            var height = document.getElementById("td-dept").clientHeight;
            document.getElementById("dvDeptBox").style.height = height;
        }
    </script>
</head>
<body style="margin: 0 0 0 0">
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="90%" border="0">
                    <tr>
                        <td>
                            <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                <tr>
                                    <td class="descTd">
                                        流程类别
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDLBusinessClass" Width="130px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="descTd">
                                        模板名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="descTd">
                                        审核时间
                                    </td>
                                    <td>
										<asp:TextBox ID="DBDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="descTd">
                                        审核状态
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDLSing" Style="width: 130px" runat="server"><asp:ListItem Value="" Text="全部" /><asp:ListItem Value="0" Text="到达未审核" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-2" Text="已超时" /><asp:ListItem Value="2" Text="未处理已通过" /></asp:DropDownList>
                                    </td>
                                    <td class="descTd">
                                        发起人
                                    </td>
                                    <td>
                                        <span class="spanSelect" style="width: 158px; float: left">
                                            <input type="text" id="txtUserCode" readonly="readonly" style="width: 133px;
                                                height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                            <img alt="选择人员" onclick="pickSinglePerson();" src="../../images/icon.bmp" style="float: right" />
                                        </span>
                                        <input type="hidden" id="hidorganizer" runat="server" />

                                    </td>
                                    <td class="descTd">
                                        参与审核人
                                    </td>
                                    <td style="padding-right: 1px">
                                        <span class="spanSelect" style="width: 158px; float: left">
                                            <input type="text" id="txtjoiner" readonly="readonly" style="width: 133px;
                                                height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                            <img alt="选择人员" onclick="pickMutiPerson();" src="../../images/icon.bmp" style="float: right" />
                                        </span>
                                        <input type="hidden" id="hidjoiner" runat="server" />

                                    </td>
                                    <td colspan="4">
                                        <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; text-align: left" class="divFooter">
                <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />&nbsp;
                <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />&nbsp;
                <asp:HiddenField ID="HdnRecoreId" runat="server" />
                <asp:HiddenField ID="HdnBusinessCode" runat="server" />
                <asp:HiddenField ID="HdnBusinessClass" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 100%" id="td-dept">
                <div id="divgv">
                    <asp:GridView ID="GVEnlist" AllowPaging="false" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="GVEnlist_RowDataBound" OnPageIndexChanging="GVEnlist_PageIndexChanging" DataKeyNames="NoteID" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable">
                                <tr class="header">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        模板名称
                                    </th>
                                    <th scope="col">
                                        流程类别
                                    </th>
                                    <th scope="col">
                                        节点名称
                                    </th>
                                    <th scope="col">
                                        发起人
                                    </th>
                                    <th scope="col">
                                        参与审核人
                                    </th>
                                    <th scope="col">
                                        审核状态
                                    </th>
                                    <th scope="col">
                                        审核时间
                                    </th>
                                    <th scope="col">
                                        审核内容
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="模板名称" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("TemplateName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("TemplateName"), 20) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程类别" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("BusinessClassName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("BusinessClassName"), 20) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="NodeName" HeaderText="节点名称" SortExpression="NodeName" /><asp:BoundField DataField="organigerName" HeaderText="发起人" SortExpression="organigerName" /><asp:TemplateField HeaderText="参与的审核人" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("OperatorPerson").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("OperatorPerson"), 20) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Status" HeaderText="审核状态" SortExpression="Status" /><asp:BoundField DataField="AuditDate" HeaderText="审核时间" HtmlEncode="false" SortExpression="AuditDate" DataFormatString="{0:yyyy-MM-dd}" /><asp:TemplateField HeaderText="审核内容">
<ItemTemplate>
                                    <asp:HyperLink ID="HLConter" CssClass="firstpage" Style="color: Blue;" runat="server">HyperLink</asp:HyperLink>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
