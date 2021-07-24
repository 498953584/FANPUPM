<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudModifyView.aspx.cs" Inherits="BudgetManage_Budget_BudModifyView" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
        table tr
        {
            border-color: Black;
            background-color: Black;
        }
        .fontsize
        {
            font-size: 13px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyInTask', 'gvInTask');
            var table = new JustWinTable('gvInTask');
            replaceEmptyTable('emptyOutTask', 'gvOutTask');
            var table = new JustWinTable('gvOutTask');
            displayLookAdjuncts();
            showTooltip('tooltip', 25);
            if ($('#hfldIsWBSRelevance').val() == '1') {
                $('#trQueryRes').hide();
            } else {
                $('#trQueryRes').show();
            }
            if ($('#gvOutTask')[0] == undefined) {
                $('#trOutTask').hide();
            }
            if ($('#gvInTask')[0] == undefined) {
                $('#trInTask').hide();
            }
        });
        //点击清单外/清单内
        function clickWBSType(type) {
            if (type == "out") {
                $('#spOutWBS').attr('class', 'xxkd');
                $('#spInWBS').attr('class', 'xxk');
                $('#divOutTask').show();
                $('#divInTask').hide();
            } else {
                $('#spOutWBS').attr('class', 'xxk');
                $('#spInWBS').attr('class', 'xxkd');
                $('#divOutTask').hide();
                $('#divInTask').show();
            }
        }
        //添加行进行显示资源信息
        var prevId;
        function showInfo(modifyTaskId) {
            var tab_Resource = null;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/ModifyTask.ashx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val() + '&spType=out&modifyTaskId=' + modifyTaskId + '&type=getRes',
                success: function (data) {
                    tab_Resource = data;
                }
            });
            var isDisplay = $('#' + modifyTaskId + '1').get(0);
            if (isDisplay == undefined) {
                $('#' + modifyTaskId).after('<tr id="' + modifyTaskId + '1"><td align="center" colspan="12" style="border:solid 1px #000000;">' + tab_Resource + '</td></tr>');
                if (prevId != undefined && prevId != modifyTaskId) {
                    $('#' + prevId + '1').remove();
                }
                prevId = modifyTaskId;

            }
            else {
                $('#' + modifyTaskId + '1').remove();
                isDisplay = undefined;
            }
        }
        //资源列表
        function showResList() {
            if (parent.desktop != undefined) {
                parent.desktop.BudModifyResList = window;
                var url = '/BudgetManage/Budget/BudModifyResList.aspx?' + new Date().getTime() + '&modifyId=' + $('#hfldModifyId').val() + '&isAllowEditRes=0';
                toolbox_oncommand(url, "查看资源");
            } else {
                var url = '/BudgetManage/Budget/BudModifyResList.aspx?' + new Date().getTime() + '&modifyId=' + $('#hfldModifyId').val() + '&isAllowEditRes=0';
                viewopen(url, 820, 500);
            }
        }
        //查看附件
        function queryAdjunct(id) {
            var path = $('#hfldAdjunctPath').val() + '/' + id;
            showFiles(path, 'divOpenAdjunct');
        }
        //是否显示附件显示
        function displayLookAdjuncts() {
            var tab = document.getElementById('gvInTask');
            if (tab != null) {
                if (tab.rows.length > 0) {
                    for (i = 1; i < tab.rows.length; i++) {
                        var id = tab.rows[i].id;
                        var imgLink = "<SPAN class=link onclick=\"queryAdjunct('" + id + "')\"><IMG style='BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none; CURSOR: pointer' src='/images1/icon_att0b3dfa.gif'> </SPAN>";
                        var path = $('#hfldAdjunctPath').val() + '/' + id;
                        var showCount = getFilesCount(path);
                        if (showCount == 0)
                            tab.rows[i].cells[9].innerText = '';
                        else
                            tab.rows[i].cells[9].innerHTML = imgLink;
                    }
                }
            }
            var tabOut = document.getElementById('gvOutTask');
            if (tabOut != null) {
                if (tabOut.rows.length > 0) {
                    for (i = 1; i < tabOut.rows.length; i++) {
                        var id = tabOut.rows[i].id;
                        var imgLink = "<SPAN class=link onclick=\"queryAdjunct('" + id + "')\"><IMG style='BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none; CURSOR: pointer' src='/images1/icon_att0b3dfa.gif'> </SPAN>";
                        var path = $('#hfldAdjunctPath').val() + '/' + id;
                        var showCount = getFilesCount(path);
                        if (showCount == 0)
                            tabOut.rows[i].cells[13].innerText = '';
                        else
                            tabOut.rows[i].cells[13].innerHTML = imgLink;
                    }
                }
            }
        }
        //获得附件个数
        function getFilesCount(path) {
            var count = 0;
            $.ajax({
                type: "GET",
                url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
                async: false,
                dataType: "JSON",
                success: function (data) {
                    count = data.length;
                }
            });
            return count;
        }
    </script>
    <style type="text/css">
        #bllProducer
        {
        }
        #bllProducer td
        {
        }
    </style>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                预算变更查看
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            编号
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtModifyCode" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            变更内容
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtModifyContent" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            变更文件编号
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtModifyFileCode" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            项目名称
                        </td>
                        <td class="elemTd" style="word-break: break-all;">
                            <asp:Literal ID="txtPrjName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            预算成本
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtBudAmount" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            报审价
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtReportAmount" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            核准价
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtApprovalAmount" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            核准时间
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtApprovalDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Literal ID="txtNotes" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="td-content" id="FileUpload1" colspan="3" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trQueryRes" style="height: 1px;">
            <td>
                <span class="link" onclick="showResList();">查看资源</span>
            </td>
        </tr>
        <tr id="trOutTask" style="height: 1px;">
            <td style="vertical-align: top; padding-top: 10px;">
                <div id="divOutTitle" style="position: relative; font-size: 13px;
                    font-weight: bold; text-align: center;" runat="server">
                    清单外
                </div>
                <div id="divOutTask" runat="server">
                    <table class="tableContent2" cellpadding="5px" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="gvOutTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvOutTask_RowDataBound" DataKeyNames="ModifyTaskId" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyOutTask" class="tab" width="100%">
                                            <tr class="header">
                                                <td style="width: 25px;">
                                                    序号
                                                </td>
                                                <td align="center">
                                                    上级任务
                                                </td>
                                                <td align="center">
                                                    清单编码
                                                </td>
                                                <td align="center">
                                                    项目名称
                                                </td>
                                                <td align="center">
                                                    项目特征描述
                                                </td>
                                                <td align="center">
                                                    单位
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    数量
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    综合单价
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    合价
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    类型
                                                </td>
                                                <td align="center">
                                                    备注
                                                </td>
                                                <td align="center">
                                                    附件
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上级任务" HeaderStyle-Width="80px"><ItemTemplate>
                                                <%# GetTaskCode(Eval("ParentId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="清单编码" HeaderStyle-Width="80px"><ItemTemplate>
                                                <%# Eval("ModifyTaskCode") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                                <%# Eval("ModifyTaskContent") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述"><ItemTemplate>
                                                <%# Eval("FeatureDescription") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                                <%# Eval("Unit") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
                                                <%# Eval("Quantity") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价">
<ItemTemplate>
                                                <%# Eval("UnitPrice") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价">
<ItemTemplate>
                                                <%# Eval("Total2") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="65px"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("StartDate")) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="65px"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("EndDate")) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"><ItemTemplate>
                                                <%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                <%# Eval("Note") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr id="trInTask" style="height: 1px;">
            <td style="vertical-align: top; padding-top: 10px;">
                <div id="div1" style="position: relative; font-size: 13px; font-weight: bold;
                    text-align: center;" runat="server">
                    清单内
                </div>
                <div id="divInTask">
                    <table class="tableContent2" cellpadding="5px" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="gvInTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvInTask_RowDataBound" DataKeyNames="ModifyTaskId" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyInTask" class="tab" width="100%">
                                            <tr class="header">
                                                <td style="width: 25px;">
                                                    序号
                                                </td>
                                                <td align="center">
                                                    变更任务
                                                </td>
                                                <td align="center">
                                                    变更内容
                                                </td>
                                                <td align="center">
                                                    单位
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    变更数量
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    综合单价
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    合价
                                                </td>
                                                <td align="center">
                                                    类型
                                                </td>
                                                <td align="center">
                                                    备注
                                                </td>
                                                <td align="center">
                                                    附件
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hfldInModifyTaskId" Value='<%# System.Convert.ToString(Eval("ModifyTaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更任务" HeaderStyle-Width="80px"><ItemTemplate>
                                                <%# GetTaskCode(Eval("TaskId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更内容" HeaderStyle-Width="122px"><ItemTemplate>
                                                <%# Eval("ModifyTaskContent") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                                <%# Eval("Unit") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
                                                <%# Eval("Quantity") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价">
<ItemTemplate>
                                                <%# Eval("UnitPrice") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价">
<ItemTemplate>
                                                <%# Eval("Total2") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                <%# Eval("Note") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="132" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    <asp:HiddenField ID="hfldAdjunctPath" runat="server" />
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    <!-- 放到此处解决与上传附件的用户控件样式冲突的问题-->
    </form>
    <!-- 放到此处解决与上传附件的用户控件样式冲突的问题-->
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
</body>
</html>
