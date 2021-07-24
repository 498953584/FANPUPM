<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAdjunct.aspx.cs" Inherits="PrjManager_SetAdjunct" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
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
            $('#hfldPrjMemberID').val(getRequestParam('PrjMemberId'))
            $.ajax({
                type: "GET",
                url: "/PrjManager/Handler/GetyhmcInfo.ashx?" + new Date().getTime() + '&id=' + $('#hfldPrjMemberID').val(),
                async: false,
                dataType: "json",
                success: function (data) {
                    setFilelUpPath();
                    if (data != '0') {
                        data = data[0];
                        $('#txtMName').val(data.v_xm);
                        $('#txtMPost').val(data.Post);
                        $('#txtMEducationalBackground').val(data.EducationalBackground);
                        $('#txtMTechnical').val(data.Technical);
                        $('#txtMPostAndCompetency').val(data.PostAndCompetency);
                        $('#txtMTrainingInformation').val(data.TrainingInformation);
                        $('#txtMPastPerformance').val(data.PastPerformance);
                    }
                }
            });
            // 设置文件上传路径
            function setFilelUpPath() {
                var recordCode = $('#hfldPrjMemberID').val();
                updateRecordCode('FileUploadAdjunct', recordCode);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSetAdjunct" title="编辑信息">
        <table class="tableContent2" cellpadding="5" cellspacing="5">
            <tr>
                <td class="word">
                    姓名
                </td>
                <td>
                    <asp:TextBox ID="txtMName" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    岗位
                </td>
                <td>
                    <asp:TextBox ID="txtMPost" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    职称
                </td>
                <td>
                    <asp:TextBox ID="txtMTechnical" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    学历及专业
                </td>
                <td>
                    <asp:TextBox ID="txtMEducationalBackground" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    资格证书
                </td>
                <td>
                    <asp:TextBox ID="txtMPostAndCompetency" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    上岗培训情况
                </td>
                <td>
                    <asp:TextBox ID="txtMTrainingInformation" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    以往工作表现
                </td>
                <td>
                    <asp:TextBox ID="txtMPastPerformance" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    相关附件
                </td>
                <td style="white-space: nowrap;">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUploadAdjunct" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
        </table>
        <div style="text-align: right; padding-right: 50px;">
            <asp:Button Text="保存" ID="btnSave" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldPrjMemberID" runat="server" />
    </form>
</body>
</html>
