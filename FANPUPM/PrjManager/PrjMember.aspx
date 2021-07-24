<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrjMember.aspx.cs" Inherits="PrjManager_PrjMember" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目成员编制</title><link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyProject', 'gvwPrjInfo');
            var table = new JustWinTable('gvwPrjInfo');
            setButton2(table, 'hfldCheckedIds');
            setWfButtonState2(table, 'WF1');
            formateTable('gvwPrjInfo', 4);
            showTooltip('tooltip', 25)
        });
        //设置按钮
        function setButton2(jwTable, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.getElementsByTagName('TR').length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '') {
                    document.getElementById(hdChkId).value = this.id;
                }
                if (document.getElementById(hdChkId).value != '') {
                    setAllBttons('');
                }
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = "";
                    }
                    setAllBttons('disalbed');
                }
                else {
                    if (hdChkId != '') {
                        var taskId = jwTable.getCheckedChkIdJson(checkedChk);
                        document.getElementById(hdChkId).value = taskId;
                    }
                    if (checkedChk.length == 1)
                        setAllBttons('');
                    else
                        setAllBttons('disalbed');
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    if (checkedChks.length == 1) {
                        setAllBttons('');
                    }
                    else {
                        setAllBttons('disalbed');
                    }
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    setAllBttons('disalbed');
                }
            });
        }
        function setAllBttons(disabled) {
            if (disabled != undefined && disabled != '') {
                $('#btnMember').attr('disabled', 'disabled');
                $('#btnQuery').attr('disabled', 'disabled');
            }
            else {
                arguments.callee('disabled');
                $('#btnQuery').removeAttr('disabled');
                var selPrjRow = $('#' + $('#hfldCheckedIds').val()).get(0);
                if (selPrjRow.getAttribute('primit') == '1' && (selPrjRow.getAttribute('flowState') == -1 ||
                selPrjRow.getAttribute('flowState') == -3)) {
                    $('#btnMember').removeAttr('disabled');
                }
            }
        }

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        //设置小组成员
        function openSetPrjMemberTabPage() {
            parent.desktop.PrjMemberList = window;
            var id = $('#hfldCheckedIds').val();
            var url = "/PrjManager/PrjMemberList.aspx?id=" + id;
            toolbox_oncommand(url, "项目小组成员");
        }
        //查看小组成员
        function openQuery() {
            viewopen('/PrjManager/PrjMemberView.aspx?&ic=' + $('#hfldCheckedIds').val(), '项目小组成员查看')
        }
    </script>
    <style type="text/css">
        #queryTable tr td
        {
            white-space: nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjCode" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目经理
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtTenderPrjManager" Style="width: 97px; height: 15px;
                                    border: none; line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                <img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
                                    onclick="selectUser('hfldTenderPrjManager','txtTenderPrjManager');" />
                            </span>
                            <input id="hfldTenderPrjManager" type="hidden" style="width: 1px" runat="server" />

                        </td>
                        <td class="descTd">
                            流程状态
                        </td>
                        <td>
                            <asp:DropDownList ID="dropWFState" Width="100px" runat="server"><asp:ListItem Text="" Value="" Selected="true" /><asp:ListItem Text="未提交" Value="-1" /><asp:ListItem Text="审核中" Value="0" /><asp:ListItem Text="已审核" Value="1" /><asp:ListItem Text="驳回" Value="-2" /><asp:ListItem Text="重报" Value="-3" /></asp:DropDownList>
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
                            项目状态
                        </td>
                        <td>
                            <asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left; padding-left: 2px;">
                <input type="button" id="btnMember" value="成员编制" disabled="disabled" onclick="openSetPrjMemberTabPage()"
                    style="width: auto;" />
                <input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="openQuery()" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="100" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvwPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="gvwPrjInfo_RowDataBound" DataKeyNames="PrjGuid,MemberFlowState,TypeCode,Primit" runat="server">
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
                                    项目编号
                                </th>
                                <th class="header">
                                    项目名称
                                </th>
                                <th class="header">
                                    项目经理
                                </th>
                                <th class="header">
                                    小组成员
                                </th>
                                <th class="header">
                                    流程状态
                                </th>
                                <th class="header">
                                    立项申请日期
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                <asp:CheckBox ID="cbAllBox" runat="server" />
                            </HeaderTemplate><ItemTemplate>
                                <asp:CheckBox ID="cbBox" runat="server" />
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="PrjStateName" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                <asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
                                    color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/PrjManager/PrjMemberView.aspx?&ic=<%# Eval("PrjGuid") %>', '项目小组成员查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目经理"><ItemTemplate>
                                <span class="tooltip" title="<%# Eval("PrjMangerName") %>">
                                    <%# StringUtility.GetStr(Eval("PrjMangerName").ToString(), 25) %>
                                </span>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小组成员"><ItemTemplate>
                                <a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("MemberNames") %>'>
                                    <%# StringUtility.GetStr(Eval("MemberNames").ToString(), 25) %>
                                </a>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                <%# Common2.GetState(Eval("MemberFlowState").ToString(), Eval("Primit").ToString()) %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                <%# Common2.GetTime(Eval("StartDate")) %>
                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    </form>
</body>
</html>
