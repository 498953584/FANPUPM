<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitiateFail.aspx.cs" Inherits="TenderManage_InitiateFail" %>
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var prjId = getRequestParam('prjId');
            $('#hfldPrjId').val(prjId);
            Val.validate('form1');
            $.ajax({
                type: "GET",
                async: false,
                url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=19&userCode=' + $('#hfldUserCode').val(),
                success: function (datas) {
                    var data = datas[0];
                    $('#txtApplyDate1').val(data.ProjApplyDate);
                    $('#txtProjStartDate1').val(data.ProjStartDate);
                    $('#txtStartRemark1').val(data.ProjStartRemark);
                    setFilelUpPath();
                }
            });
        });

        //设置上传附件路径
        function setFilelUpPath() {
            var recordCode = $('#hfldPrjId').val() + '_19';
            updateRecordCode('FileIniateFail', recordCode);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div title="报名不通过基本资料" runat="server">
        <table id="table5" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    报名日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtApplyDate1" CssClass="{required:true, messages:{required:'报名日期不能为空！'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    报名不通过日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtProjStartDate1" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    因由
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStartRemark1" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    相关附件
                </td>
                <td colspan="3" id="file_qd1" style="white-space: nowrap" runat="server">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileIniateFail" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
        </table>
        <div style="text-align: right; padding-right: 50px;">
            <asp:Button Text="保存" ID="btnSave" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    </form>
</body>
</html>
