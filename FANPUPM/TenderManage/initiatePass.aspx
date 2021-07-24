<%@ Page Language="C#" AutoEventWireup="true" CodeFile="initiatePass.aspx.cs" Inherits="TenderManage_initiatePass" %>
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
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1');
            var prjGuid = getRequestParam('prjId');
            $('#hfldPrjId').val(prjGuid);
            var url = '/TenderManage/Handler/GetPrjInfoZTB.ashx?prjId=' + prjGuid + '&datetime=' + (new Date()).toString();
            $.ajax({
                type: "GET",
                async: false,
                url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=3&userCode=' + $('#hfldUserCode').val(),
                success: function (datas) {
                    var data = datas[0];
                    $('#txtApplyDate').val(data.ProjApplyDate);
                    $('#txtProjStartDate').val(data.ProjStartDate);
                    $('#txtStartRemark').val(data.ProjStartRemark);
                    setFilelUpPath();
                }
            });

        });
        //设置上传附件路径
        function setFilelUpPath() {
            var recordCode = $('#hfldPrjId').val() + '_3';
            updateRecordCode('FileIniatePass', recordCode);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div title="报名通过基本资料">
        <table id="table1" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    报名日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtApplyDate" CssClass="{required:true, messages:{required:'报名日期不能为空！'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    报名通过日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtProjStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStartRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    相关附件
                </td>
                <td colspan="3" id="file_qd" style="white-space: nowrap" runat="server">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileIniatePass" Name="附件" Class="ProjectFile" runat="server" />
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
