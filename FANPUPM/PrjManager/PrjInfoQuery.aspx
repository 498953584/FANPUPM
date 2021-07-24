<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrjInfoQuery.aspx.cs" Inherits="PrjManager_PrjInfoQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目信息管理</title><link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>   
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var jwTable = new JustWinTable('gvPrjInfo');
            replaceEmptyTable('emptyTable', 'gvPrjInfo');
            showMoreName();
			formateTable('gvPrjInfo', 3);
        });
 
        // 甲方名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtOwner', idinput: 'hfldOwner' });
        }

        // 名称信息提示
        function showMoreName() {
            var tab = document.getElementById('gvPrjInfo');
            if (tab != null) {
                for (i = 1; i < tab.rows.length; i++) {
                    var cells = tab.rows[i].cells;
                    if (cells.length == 1) return;
                    if (cells[3].children.length == 0) return;
                    var imgId = cells[3].children[0].id;
                    var altLength = document.getElementById(imgId).title.length;
                    if (altLength > 25) {
                        $('#' + imgId).css('display', '');
                        $('#' + imgId).tooltip({
                            track: true,
                            delay: 0,
                            showURL: false,
                            fade: true,
                            showBody: " - ",
                            extraClass: "solid 1px blue",
                            fixPNG: true,
                            left: -200
                        });
                    } else {
                        document.getElementById(imgId).title = '';
                    }
                }
            }
        }

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
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
                            <td class="descTd" style="white-space: nowrap;">
                                项目编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjCode" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目经理
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px;">
                                    <asp:TextBox ID="txtName" Style="width: 97px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
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
                                        margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                    <img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                                </span>
                                <asp:HiddenField ID="hfldOwner" runat="server" />
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目状态
                            </td>
                            <td>
                                <asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap;">
                                <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" class="tab">
                        <tr>
                            <td class="divFooter" style="text-align: left">
                                <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" valign="top">
                                <div>
                                    <asp:GridView ID="gvPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        项目状态
                                                    </th>
                                                    <th scope="col">
                                                        项目编号
                                                    </th>
                                                    <th scope="col">
                                                        项目名称
                                                    </th>
                                                    <th scope="col">
                                                        项目经理
                                                    </th>
                                                    <th scope="col">
                                                        建设单位
                                                    </th>
                                                    <th scope="col">
                                                        工程造价
                                                    </th>
                                                    <th scope="col">
                                                        项目立项日期
                                                    </th>
                                                    <th scope="col">
                                                        开始日期
                                                    </th>
                                                    <th scope="col">
                                                        结束日期
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="PrjStateName" HeaderText="项目状态" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/PrjManager/PrjInfoView.aspx?&ic=<%# Eval("PrjGuid") %>')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目经理"><ItemTemplate>
                                                    <span class="tooltip" title="<%# Eval("PrjMangerName") %>">
                                                        <%# StringUtility.GetStr(Eval("PrjMangerName").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                                    <span class="tooltip" title="<%# Eval("Owner") %>">
                                                        <%# StringUtility.GetStr(Eval("Owner").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("StartDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("EndDate")) %>
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
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    </form>
</body>
</html>
