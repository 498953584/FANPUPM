<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ApprovalQuery.aspx.cs" Inherits="TenderManage_ApprovalQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>立项信息一览</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyTable', 'gvDataInfo');
            var jwTable = new JustWinTable('gvDataInfo');
            showTooltip('tooltip', 25);
        });

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        //甲方名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtOwner', idinput: 'hfldOwner' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="vertical-align: top;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">
                                项目编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjCode" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                项目名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                立项申请人
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px">
                                    <asp:TextBox ID="txtName" Style="width: 95px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 2px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldName','txtName');" runat="server" />

                                </span>
                                <input id="hfldName" type="hidden" style="width: 1px" runat="server" />

                            </td>
                            <td class="descTd">
                                项目类型
                            </td>
                            <td>
                                <asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                立项申请日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                至
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
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
                            <td colspan="2">
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
                                <asp:Button ID="btnExport" Text="导出Excel" Width="80px" OnClick="btnExport_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                    </th>
                                                    <th scope="col">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        项目状态
                                                    </th>
                                                    <th scope="col">
                                                        立项申请人
                                                    </th>
                                                    <th scope="col">
                                                        项目编号
                                                    </th>
                                                    <th scope="col">
                                                        项目名称
                                                    </th>
                                                    <th scope="col">
                                                        建设单位
                                                    </th>
                                                    
                                                    <th scope="col">
                                                        工程造价
                                                    </th>
                                                    <th scope="col">
                                                        工程工期
                                                    </th>
                                                    <th scope="col">
                                                        立项申请日期
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:BoundField HeaderText="立项申请人" DataField="Person" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="tooltip link" onclick="viewopen('/TenderManage/InfoView.aspx?&&ic=<%# Eval("PrjGuid") %>', '项目信息查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("WorkUnitName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("InputDate")) %>
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
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    </form>
</body>
</html>
