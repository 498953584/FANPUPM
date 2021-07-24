<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmWantPlanList.aspx.cs" Inherits="StockManage_basicset_SmWantPlanList" EnableEventValidation="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>物资需求计划</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var wantPlanTable = new JustWinTable('gvSm_Wantplan');
            replaceEmptyTable('emptySm_Wantplan', 'gvSm_Wantplan');
            addEvent(document.getElementById('btnLook'), 'click', queryWantplan);
            setButton(wantPlanTable, "btnDel", "btnUpdate", "btnLook", "hfldWantPlanCheckedID")
            setWfButtonState2(wantPlanTable, 'WF1');
            showTooltip('tooltip', 15);
            $('#txtStartTime').blur(function () {
                controlDate(this);
            })
            $('#txtEndTime').blur(function () {
                controlDate(this);
            })
        });

        function openWindow() {
            var pname = document.getElementById('hdnProname').value;
            var pcode = document.getElementById('hndProCode').value;
            var swcode = document.getElementById('hdnSwid').value;
            parent.parent.desktop.SmWantPlan = window;
            var url = "/StockManage/basicset/SmWantPlan.aspx?pcode=" + pcode + "&swcode=" + swcode + "&optype=add";
            toolbox_oncommand(url, "新增需求计划单");
        }
        //编辑查看验证
        function checkCount() {
            var chk = new JustWinTable('gvSm_Wantplan');
            if (chk.getCheckedChk().length > 1) {
                top.ui.alert('编辑或者查看时，只能选择单行！');
                return false;
            }
            if (chk.getCheckedChk().length == 0) {
                top.ui.alert('请选择要操作的行！');
                return false;
            }
            return true;
        }
        //删除验证
        function delCheck() {
            var chk = new JustWinTable('gvSm_Wantplan');
            if (chk.getCheckedChk().length == 0) {
                top.ui.alert('请选择要操作的行！');
                return false;
            }
            else {
                return confirm("确定删除吗？");
            }

        }

        function openPerson() {
            winopen("/CommonWindow/PickSinglePerson.aspx?sm");
        }

        function queryWantplan() {
            var id = $('#hfldWantPlanCheckedID').val();
            $('#' + id).find('.al').click();
            //			viewopen('WantplanView.aspx?ic=' + this.guid, 820, 500);
        }

        //选择人员
        function selectUser() {
            jw.selectOneUser({ nameinput: 'txtPeople' });
        }

        //管理时间
        function controlDate(para) {
            var startDates = document.getElementById('txtStartTime').value;
            var startDateList = startDates.split('-');
            var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);

            var endDates = document.getElementById('txtEndTime').value;
            var endDatesList = endDates.split('-');
            var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);
            if (startDates != '') {
                if (startDate == 'NaN') {
                    top.ui.alert('起始日期输入时间格式不正确！');
                    para.value = '';
                    return;
                }
            }
            if (endDates != '') {
                if (endDate == 'NaN') {
                    top.ui.alert('结束日期输入时间格式不正确！');
                    para.value = '';
                    return;
                }
            }
        }

        function show(swid) {
            var url = 'WantplanView.aspx?ic=' + swid + '&prjId=' + $('#hndProCode').val();
            viewopen(url, 820, 500);
        }
  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="divHeader" style="letter-spacing: normal">
                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 96%; width: 100%;">
                <input id="hdnSwid" type="hidden" runat="server" />

                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            录入日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            计划编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtSwcode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入人
                        </td>
                        <td>
                            <asp:TextBox ID="txtPeople" Style="width: 120px;" CssClass="easyui-validatebox select_input" data-options="validType:'validChars[50]'" imgclick="selectUser" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <input id="btnAdd" type="button" value="新增" onclick="openWindow();" />
                <input id="btnUpdate" type="button" value="编辑" disabled="true" OnServerClick="btnUpdate_ServerClick" runat="server" />

                <input id="btnDel" type="button" value="删除" disabled="true" OnServerClick="btnDel_ServerClick" runat="server" />

                <input id="btnLook" type="button" value="查看" disabled="disabled" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="071" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div class="">
                    <asp:GridView ID="gvSm_Wantplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnRowDataBound="gvSm_Wantplan_RowDataBound" OnPageIndexChanging="gvSm_Wantplan_PageIndexChanging" DataKeyNames="swcode,swid,procode" runat="server">
<EmptyDataTemplate>
                            <table width="100%" id="emptySm_Wantplan">
                                <tr class="header">
                                    <td style="width: 20px">
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </td>
                                    <td style="width: 25px">
                                        序号
                                    </td>
                                    <td style="width: 100px">
                                        计划编号
                                    </td>
                                    <td style="width: 150px">
                                        录入时间
                                    </td>
                                    <td>
                                        流程状态
                                    </td>
                                    <td>
                                        附件
                                    </td>
                                    <td>
                                        说明
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" Width="20px" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chkBox" Width="20px" ToolTip='<%# System.Convert.ToString(Eval("swcode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                                    <span class="al" onclick="show('<%# Eval("swid") %>')">
                                        <%# Eval("swcode") %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="录入人" DataField="person" /><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("intime").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("flowstate").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
                                    <%# GetAnnx(System.Convert.ToString(Eval("swid"))) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("explain") %>'>
                                        <%# StringUtility.GetStr(System.Convert.ToString(Eval("explain"))) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input id="hdnProname" type="hidden" runat="server" />

    <input id="hndProCode" type="hidden" runat="server" />

    <asp:HiddenField ID="hfldWantPlanCheckedID" runat="server" />
    </form>
</body>
</html>
