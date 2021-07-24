<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="InfoQuery.aspx.cs" Inherits="TenderManage_InfoQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>立项申请一览</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyProject', 'gvwProject');
            var table = new JustWinTable('gvwProject');
            showTooltip('tooltip', 25);
        });
        //甲方名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtOwner', idinput: 'hfldOwner' });
        }
        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        function setSummarizingInfo() {
            var SummarizingInfo = $('#spSummarizingInfo');
            var summarText = SummarizingInfo[0].innerText;
            $('#hfldSummarizingInfo').val(summarText);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                项目编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjCode" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                立项申请人
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px;">
                                    <asp:TextBox ID="txtName" Style="width: 95px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 2px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldName','txtName');" runat="server" />

                                </span>
                                <input id="hfldName" type="hidden" style="width: 1px" runat="server" />

                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目类型
                            </td>
                            <td>
                                <asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                立项申请日期
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
                            <td class="descTd" style="white-space: nowrap;">
                                建设单位
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px;">
                                    <asp:TextBox ID="txtOwner" Style="width: 97px; height: 15px; border: none; line-height: 16px;
                                        margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                    <img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                                </span>
                                <asp:HiddenField ID="hfldOwner" runat="server" />
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                流程状态
                            </td>
                            <td>
                                <asp:DropDownList ID="dropWFState" Width="100%" runat="server"><asp:ListItem Value="" Text="" /><asp:ListItem Value="-1" Text="未提交" /><asp:ListItem Value="0" Text="审核中" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-2" Text="驳回" /><asp:ListItem Value="-3" Text="重报" /></asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap;">
                                <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
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
                                <asp:Button ID="btnExport" Text="导出Excel" OnClientClick="setSummarizingInfo()" Width="80px" OnClick="btnExport_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvwProject" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwProject_RowDataBound" DataKeyNames="PrjGuid" runat="server">
<EmptyDataTemplate>
                                            <table class="gvdata" cellspacing="0" id="emptyProject" rules="all" border="1" style="width: 100%;
                                                border-collapse: collapse;">
                                                <tr>
                                                    <th class="header">
                                                        序号
                                                    </th>
                                                    <th class="header">
                                                        项目状态
                                                    </th>
                                                    <th class="header">
                                                        立项申请人
                                                    </th>
                                                    <th class="header">
                                                        项目编号
                                                    </th>
                                                    <th class="header">
                                                        项目名称
                                                    </th>
                                                    <th class="header">
                                                        建设单位
                                                    </th>
                                                    
                                                    <th class="header">
                                                        工程造价
                                                    </th>
                                                    <th class="header">
                                                        工程工期
                                                    </th>
                                                    <th class="header">
                                                        立项申请日期
                                                    </th>
                                                    <th class="header">
                                                        流程状态
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:BoundField HeaderText="立项申请人" DataField="Person" /><asp:BoundField HeaderText="项目编号" DataField="PrjCode" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/TenderManage/InfoView.aspx?&&ic=<%# Eval("PrjGuid") %>', '项目信息查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("WorkUnitName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                    <%# Common2.GetState(Eval("ProjFlowSate").ToString()) %></ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 5px">
                                <span id="spSummarizingInfo">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hfldSummarizingInfo" runat="server" />
    </form>
</body>
</html>
