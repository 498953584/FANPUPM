<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BidFailInfo.aspx.cs" Inherits="TenderManage_BidFailInfo" %>
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
            var prjId = getRequestParam('PrjId');
            $('#hfldPrjId').val(prjId);
            $.ajax({
                type: "GET",
                async: false,
                url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=6&userCode=' + $('#hfldUserCode').val(),
                success: function (datas) {
                    var data = datas[0];
                    $('#txtOutBidDate').val(data.OutBidDate);
                    if (data.OutBidIsReturn != null) {
                        var index = data.OutBidIsReturn ? "1" : "0";
                        $('#RblOutBidIsReturn_' + index).get(0).checked = true;
                    }
                    $('#txtOutBidRemark').val(data.OutBidRemark);
                    setFileOutBitPath();
                }
            });
        });
        function setFileOutBitPath() {
            var recordCode = $('#hfldPrjId').val() + '_' + jw.ProjectParameter.OutBid;
            updateRecordCode('FileUpload_6', recordCode);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divbtnLost">
        <table id="table4" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    落标日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtOutBidDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    落标保证金是否退还
                </td>
                <td class="txt">
                    <asp:RadioButtonList ID="RblOutBidIsReturn" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="0" Text="是" /><asp:ListItem Value="1" Text="否" /></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    落标备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtOutBidRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    相关附件
                </td>
                <td colspan="4" class="adjunct">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload_6" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
        </table>
        <div style="position: absolute; right: 10px; bottom: 10px;">
            <asp:Button Text="保存" ID="btnSave" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    </form>
</body>
</html>
