<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="StopMsgEdit.aspx.cs" Inherits="StartStopReturnWork_StopMsgEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑开工报告</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            Val.validate('form1');
            $('#txtStopDate').blur(function () {
                controlDate(this);
            })
            $('#txtSupervisorSignDate').blur(function () {
                controlDate(this);
            })
            $('#txtGeneralSignDate').blur(function () {
                controlDate(this);
            })
        });
        //施工单位
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtConstructionUnit', idinput: 'hfldConstructionUnit' });
        }
        //限制输入字符长度
        function limitTextLenth(txtId) {
            var txtValue = txtId.value;
            if (txtValue.length > 50) {
                txtId.value = txtValue.substring(0, 50);
                $.messager.alert('系统提示', '输入的字数不能大于50个字');
            }
        }
        //管理日期
        function controlDate(para) {
            var stopDates = document.getElementById('txtStopDate').value;
            var stopDateList = stopDates.split('-');
            var stopDate = new Date(stopDateList[0], stopDateList[1] - 1, stopDateList[2]);

            var generalSignDates = document.getElementById('txtGeneralSignDate').value;
            var generalSignDatesList = generalSignDates.split('-');
            var generalSignDate = new Date(generalSignDatesList[0], generalSignDatesList[1] - 1, generalSignDatesList[2]);

            var supervisorSignDates = document.getElementById('txtSupervisorSignDate').value;
            var supervisorSignDatesList = supervisorSignDates.split('-');
            var supervisorSignDate = new Date(supervisorSignDatesList[0], supervisorSignDatesList[1] - 1, supervisorSignDatesList[2]);

            if (stopDates != '') {
                if (stopDate == 'NaN') {
                    $.messager.alert('系统提示', '停工日期输入格式不正确！');
                    para.value = '';
                    return;
                }
            }
            if (supervisorSignDates != '') {
                if (supervisorSignDate == 'NaN') {
                    $.messager.alert('系统提示', '监理工程师签名日期输入格式不正确！');
                    para.value = '';
                    return;
                }
            }
            if (generalSignDates != '') {
                if (generalSignDate == 'NaN') {
                    $.messager.alert('系统提示', '总监理工程师签名日期输入格式不正确！');
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
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="停工通知单" runat="server"></asp:Label>
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
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="StopMsg" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    工程名称
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtPrjName" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'项目名称必须输入'}}" ReadOnly="true" disabled="disabled" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
                </td>
                <td class="word" style="white-space: nowrap;">
                    施工地段
                </td>
                <td class="txt" style="padding-right: 3px;">
                    <asp:TextBox ID="txtConstArea" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
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
                <td class="word" style="white-space: nowrap;">
                    工程里程
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtProjectMileage" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    停工日期
                </td>
                <td class="txt mustInput" colspan="3">
                    <asp:TextBox ID="txtStopDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'停工日期必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    停工原因
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStopReason" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    停工主要内容
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtMainContent" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    工程产生的问题
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtProjectProblem" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    产生问题的原因
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtProblemReason" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    影响及预计损失程度
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtImpactLossDegree" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    建议补救措施
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemedialMeasure" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    监理工程师
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtSupervisorSign" Width="100%" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    签名日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtSupervisorSignDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    总监理工程师
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtGeneralSign" Width="100%" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    签名日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtGeneralSignDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('StopMsgEdit','StopMsgList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldStopMsgId" runat="server" />
    <asp:HiddenField ID="hfldInputUser" runat="server" />
    <asp:HiddenField ID="hfldInputDate" runat="server" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
