<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prjStateAdjust.aspx.cs" Inherits="TenderManage_prjStateAdjust" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <title></title>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
        });
        function selectUser() {
            var url = '/EPC/Workflow/SelectUser.aspx';
            var _callback;
            if (typeof top.ui.callback == 'function') {
                _callback = top.ui.callback;
            }

            top.ui.callback = function (user) {
                $('#hfldUserCode').val(user.code);
                $('#txtAdjustMan').val(user.name);
                top.ui.callback = _callback;
            }
            top.ui.openWin({ title: '选择人员', url: url, winNo: 2 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divbtnChange" title="项目状态调整申请" runat="server">
        <table id="table" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    项目编码
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="prjCode" Style="width: 120px;" CssClass="easyui-validatebox" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="prjName" Style="width: 120px;" CssClass="easyui-validatebox" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
             <td class="word">
                    项目状态
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPrjState" Width="120px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    调整状态
                </td>
                <td class="txt">
                    <asp:DropDownList ID="drpDLstState" Style="min-width: 120px; width: auto;" runat="server"></asp:DropDownList>
                </td>  
            </tr>
            <tr>
                <td class="word">
                    调整人
                </td>
                <td class="txt">
                    <span class="spanSelect" style="width: 122px">
                        <asp:TextBox ID="txtAdjustMan" Style="width: 95px; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                        <img alt="选择调整人" onclick="selectUser();" src="../../images/icon.bmp" style="float: right;" id="img3" runat="server" />

                        <asp:HiddenField ID="hfldUserCode" runat="server" />
                    </span>
                </td>
                 <td class="word">
                    调整时间
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="adjTime" Width="120px" class="{required:true, messages:{required:'项目编号必须输入'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    调整原因
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="adjReason" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="remark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div style="width: 96%; text-align: right;">
        <asp:Button ID="btnSaveData" Text="保存" OnClick="btnSaveData_Click" runat="server" />
        <span style="padding-right: 30px;">
            <input type="button" id="btnClose" value="取 消" onclick="top.ui.closeWin();" />
        </span>
    </div>
    </form>
</body>
</html>
