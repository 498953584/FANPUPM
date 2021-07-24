<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="StartWorkReportEdit.aspx.cs" Inherits="StartStopReturnWork_StartWorkReportEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑开工报告</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
    	$(function () {
    		$("#btnSave")[0].onclick = function () {
    			if (!$('#form1').form('validate')) {
    				return false;
    			}
    		}
		});
        addEvent(window, 'load', function () {
            //如果项目状态是中标的项目显示上级项目及实施项目部，否则隐藏
            if ($('#hfldPrjState').val() == "5") {
                $('#trWinBid').show();
            } else {
                $('#trWinBid').hide();
            }
            $('#txtApplyStartDate').blur(function () {
                controlDate(this);
            });
            $('#txtRealityStartDate').blur(function () {
                controlDate(this);
            });

        });

        //施工单位
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtConstructionUnit', idinput: 'hfldConstructionUnit' });
        }
        //管理日期
        function controlDate(para) {
            var applyStartDates = document.getElementById('txtApplyStartDate').value;
            var applyStartDateList = applyStartDates.split('-');
            var applyStartDate = new Date(applyStartDateList[0], applyStartDateList[1] - 1, applyStartDateList[2]);

            var realityStartDates = document.getElementById('txtRealityStartDate').value;
            var realityStartDatesList = realityStartDates.split('-');
            var realityStartDate = new Date(realityStartDatesList[0], realityStartDatesList[1] - 1, realityStartDatesList[2]);
            if (applyStartDates != '') {
                if (applyStartDate == 'NaN') {
                    $.messager.alert('系统提示', '申请开工日期输入日期格式不正确！');
                    para.value = '';
                    return;
                }
            }
            if (realityStartDates != '') {
                if (realityStartDate == 'NaN') {
                    $.messager.alert('系统提示', '实际开工日期输入日期格式不正确！');
                    para.value = '';
                    return;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="开工报告" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word" id="td1" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3" style="text-align: left; padding-right: 0px; width: 100%">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="StartReport" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    建设项目名称
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtPrjName" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'项目名称必须输入'}}" ReadOnly="true" disabled="disabled" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
                </td>
                <td class="word" style="white-space: nowrap;">
                    单项工程名称
                </td>
                <td class="txt" style="padding-right: 3px;">
                    <asp:TextBox ID="txtSingleProjectName" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    工程地点
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtProjectPlace" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    施工单位
                </td>
                <td class="txt">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox ID="txtConstructionUnit" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择施工单位" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldConstructionUnit" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    申请开工日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtApplyStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    实际开工日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtRealityStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    实际负责人
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtActualPrincipal" Height="15px" Width="100%" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    主要负责人
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtMainPrincipal" Height="15px" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="trWinBid">
                <td class="word" style="white-space: nowrap;">
                    实施项目部:
                </td>
                <td class="txt">
                    <asp:DropDownList ID="dropImplDep" Style="width: 100%;" runat="server"></asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    开工项目的<br>
                    主要<br />
                    工程内容
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtMainContent" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    准备工作情况
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtPrepareCondition" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    存在问题
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtExistenceProblem" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    监理单位意见
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtSupervisorUnitOpinion" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    建设单位意见
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtBuildUnitOpinion" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    申请开工单位
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtApplyUnit" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    审批单位
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtAuditUnit" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddIncometContract','IncometContractList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPrjState" runat="server" />
    <asp:HiddenField ID="hfldStartReportId" runat="server" />
    <asp:HiddenField ID="hfldInputUser" runat="server" />
    <asp:HiddenField ID="hfldInputDate" runat="server" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
