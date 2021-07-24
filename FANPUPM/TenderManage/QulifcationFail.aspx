<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QulifcationFail.aspx.cs" Inherits="TenderManage_QulifcationFail" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script language='javascript' type="text/javascript">
        $(document).ready(function () {
            var prjId = getRequestParam('prjId');
            $('#hfldPrjId').val(prjId);
            Val.validate('form1');
            $.ajax({
                type: "GET",
                async: false,
                url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=16&userCode=' + $('#hfldUserCode').val(),
                success: function (datas) {
                    var data = datas[0];
                    $('#txtApplyDate1').val(data.ProjApplyDate);
                    $('#txtApprovalDate1').val(data.ProjApprovalDate);
                    $('#txtTenderDate1').val(data.ProjTenderDate);
                    $('#txtRegistDeadline1').val(data.ProjRegistDeadline);
                    $('#txtAgent1').val(data.ProgAgentName);
                    $('#hfldAgent1').val(data.ProgAgent);
                    $('#txtQualificationReadOne1').val(data.QReadOneName);
                    $('#hfldQualificationReadOne').val(data.QualificationReadOne);
                    //$('#txtDutyPerson1').val(data.PrjDutyPersonName);
                    $('#txtPrequalificationRequire1').val(data.PrequalificationRequire);
                    $('#txtFailDate').val(data.QualificationFailData);
                    $('#txtFailReason').val(data.QualificationFailReason);
                    $('#txtQualificationMargin1').val(data.QualificationMargin);    // 预审保证金
                    setFileFailPath();
                }
            });
        });

            function selectUser(id, name) {
                var url = '/EPC/Workflow/SelectUser.aspx';
                var _callback;
                if (typeof top.ui.callback == 'function') {
                    _callback = top.ui.callback;
                }

                top.ui.callback = function (user) {
                    $('#' + id).val(user.code);
                    $('#' + name).val(user.name);
                    top.ui.callback = _callback;
                }
                top.ui.openWin({ title: '选择人员', url: url, winNo: 2 });
            }
            //设置上传附件路径
            function setFileFailPath() {
                var recordCode = $('#hfldPrjId').val() + '_' + jw.ProjectParameter.QualificationFail;
                updateRecordCode('FileFail', recordCode);
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div id="div_ysfail" runat="server">
        <table id="table2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    资审日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtApprovalDate1" CssClass="{required:true, messages:{required:'资审日期不能为空！'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    资审保证金(元)
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtQualificationMargin1" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    投标日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderDate1" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    登记期限(天)
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtRegistDeadline1" onkeyup="this.value=this.value.replace(/[^\d]/g,'') ; var txtValue = this.value; if (txtValue.length > 10) {this.value = txtValue.substring(0, 10);$.messager.alert('系统提示', '输入的字数不能大于10个字');}" onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    经办人
                </td>
                 <td class="txt">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox ID="txtAgent1" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img2" onclick="selectUser('hfldAgent1','txtAgent1');" runat="server" />

                    </span>
                    <input id="hfldAgent1" type="hidden" style="width: 1px" runat="server" />

                </td>
                <td class="word">
                    未通过日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtFailDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
               <td class="word">
                    阅知人
                </td>
                <td class="txt ">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtQualificationReadOne1" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择阅知人" onclick="selectUser('hfldQualificationReadOne1', 'txtQualificationReadOne1');" src="../../images/icon.bmp" style="float: right;" id="img4" runat="server" />

                    </span>
                    <asp:HiddenField ID="hfldQualificationReadOne1" runat="server" />
                </td>
                <td class="word">
                    报名日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtApplyDate1" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    资审要求
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtPrequalificationRequire1" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    理由
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtFailReason" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" id="Td3" style="white-space: nowrap" runat="server">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileFail" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
        </table>
        <div style="position: absolute; right: 10px; bottom: 10px;">
            <asp:Button Text="保存" ID="btnSave" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消"  onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    </form>
</body>
</html>
