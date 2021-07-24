<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrjMemberQuery.aspx.cs" Inherits="PrjManager_PrjMemberQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目小组成员查询</title>

    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>

    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />


    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <link type="text/css" rel="stylesheet" href="/Script/themes/base/jquery.ui.all.css" />

    
    <script type="text/javascript">
        addEvent(window, 'load', function() {
            replaceEmptyTable('emptyProject', 'gvwPrjInfo');
            var table = new JustWinTable('gvwPrjInfo');
            formateTable('gvwPrjInfo', 3);
            setButton(table, 'hfldEmptyBtn', 'hfldEmptyBtn', 'btnQuery', 'hfldCheckedIds');
            showTooltip('tooltip', 25);
        });
        function setAllBttons(disabled) {
            if (disabled != undefined && disabled != '') {
                $('#btnMember').attr('disabled', 'disabled');
                $('#btnQuery').attr('disabled', 'disabled');
            }
            else {
                arguments.callee('disabled');
                $('#btnMember').removeAttr('disabled');
                $('#btnQuery').removeAttr('disabled');
            }
        }
        //建设单位(甲方)名称
        function pickCorp(param) {
            var corp = new Array();
            corp[0] = "";
            corp[1] = "";
            var url = "/CommonWindow/PickCorp.aspx";
            window.showModalDialog(url, corp, "dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
            if (corp[0] != "") {
                if (param == undefined) {
                    document.getElementById('hfldOwner').value = corp[0];
                    document.getElementById('txtName').value = corp[1];
                } else {
                    var txtID = param.id.replace('txt', 'hfld');
                    document.getElementById(txtID).value = corp[0];
                    param.value = corp[1];
                }
            }
        }

        //选择人员返回值
        function returnUser(id, name) {
            var val = document.getElementById("hfldTenderPrjManager").value.split(',');
            document.getElementById(val[0]).value = id;
            document.getElementById(val[1]).value = name;

        }
        //选择人员
        function selectUser(id, name) {
            document.getElementById("hfldTenderPrjManager").value = id + "," + name;
            var url = "/Common/DivSelectUser.aspx?type=tender&Method=returnUser";
            document.getElementById("IframePerson").src = url;
            selectnEvent('divFramPerson');
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
                            项目编号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjCode" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目名称:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目经理:
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtTenderPrjManager" Style="width: 97px; height: 15px;
                                    border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                <img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
                                    onclick="selectUser('hfldTenderPrjManager','txtTenderPrjManager');" />
                            </span>
                            
                            <input id="hfldTenderPrjManager" type="hidden" style="width: 1px" runat="server" />

                        </td>
                        <td class="descTd">
                            项目类型:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            流程状态:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropWFState" Width="100px" runat="server"><asp:ListItem Text="" Value="" Selected="true" /><asp:ListItem Text="未提交" Value="-1" /><asp:ListItem Text="审核中" Value="0" /><asp:ListItem Text="已审核" Value="1" /><asp:ListItem Text="驳回" Value="-2" /><asp:ListItem Text="重报" Value="-3" /></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            开始日期:
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtStartTime" Width="100%" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            结束日期:
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTime" Width="100%" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            建设单位:
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtName" Style="width: 97px; height: 15px; border: none; line-height: 16px;
                                    margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                <img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            
                            <asp:HiddenField ID="hfldOwner" runat="server" />
                        </td>
                        <td class="descTd">
                            项目状态:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:Button ID="brnQuery" Width="80px" Text="查询" OnClick="brnQuery_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left; padding-left: 2px;">
                <input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="openQuery()" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvwPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="gvwPrjInfo_RowDataBound" DataKeyNames="PrjGuid,MemberFlowState,TypeCode" runat="server">
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
                                    建设单位
                                </th>
                                <th class="header">
                                    项目类型
                                </th>
                                <th class="header">
                                    小组成员
                                </th>
                                <th class="header">
                                    流程状态
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
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="PrjKindName" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                <asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
                                    color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/PrjManager/PrjMemberView.aspx?&ic=<%# Eval("PrjGuid") %>', '项目小组成员查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                            </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="项目经理" DataField="PrjMangerName" /><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                <%# Eval("Owner") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目类型"><ItemTemplate>
                                <%# Eval("PrjKindName") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小组成员"><ItemTemplate>
                                <a class="tooltip" style="text-decoration: none;" title='<%# Eval("MemberNames") %>'>
                                    <%# StringUtility.GetStr(Eval("MemberNames").ToString(), 25) %>
                                </a>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                <%# Common2.GetState(Eval("MemberFlowState").ToString()) %>
                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
        
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hfldEmptyBtn" runat="server" />
    </form>
</body>
</html>
