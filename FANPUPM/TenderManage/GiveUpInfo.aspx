<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GiveUpInfo.aspx.cs" Inherits="TenderManage_GiveUpInfo" %>
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Val.validate('form1');
            var prjGuid = getRequestParam('prjId');
            $('#hfldPrjId').val(prjGuid);
            var url = '/TenderManage/Handler/GetPrjInfoZTB.ashx?prjId=' + prjGuid + '&datetime=' + (new Date()).toString();
            $.ajax({
                type: "GET",
                dataType: "json",
                async: false,
                url: url,
                success: function (data) {
                    if (data != null) {
                        $('#txtGiveUpTime').val(data.GiveUpTime);
                        $('#hfldOperator').val(data.Operator);
                        $('#txtOperator').val(data.OperatorName);
                        $('#txtGiveUPReason').val(data.GiveUpReason);
                        $('#txtNote').val(data.GiveUpNote);
                    }
                    else {
                        var dateTime = window["jw"].dateToStr(new Date(), "-");
                        $('#txtGiveUpTime').val(dateTime);
                    }
                },
                error: function (errorMsg) {
                    alert(errorMsg);
                }
            });
        });
        function selectUser() {
            var url = '/EPC/Workflow/SelectUser.aspx';
            var _callback;
            if (typeof top.ui.callback == 'function') {
                _callback = top.ui.callback;
            }

            top.ui.callback = function (user) {
                $('#hfldOperator').val(user.code);
                $('#txtOperator').val(user.name);
                top.ui.callback = _callback;
            }
            top.ui.openWin({ title: '选择人员', url: url, winNo: 2 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div id="div4" title="项目放弃基本信息" runat="server">
        <table id="table2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    放弃时间
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtGiveUpTime" CssClass="{required:true, messages:{required:'放弃日期不能为空！'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    操作人
                </td>
                <td class="txt">
                    <span class="spanSelect" style="width: 99%">
                        <asp:TextBox ID="txtOperator" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser('hfldOperator','txtOperator');" runat="server" />

                    </span>
                    <input type="hidden" id="hfldOperator" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    放弃原因
                </td>
                <td class="txt mustInput" colspan="3">
                    <asp:TextBox ID="txtGiveUPReason" TextMode="MultiLine" Height="50px" CssClass="{required:true, messages:{required:'放弃原因不能为空！'}}" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" id="Td2" style="white-space: nowrap" runat="server">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="fileGiveUp" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
        </table>
        <div style="text-align:right; padding-right:50px; ">
            <asp:Button Text="保存" ID="btnSave" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hlfPrjId" runat="server" />
    </form>
</body>
</html>
