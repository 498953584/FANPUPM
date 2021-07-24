<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ViewMail.aspx.cs" Inherits="OA2_ViewMail" EnableEventValidation="false" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看邮件</title><link href="../../Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link Href="../../Script/jquery.treeview/jquery.treeview.css" />
<link rel="stylesheet" href="/Script/Kindeditor/themes/default/default.css" />
<link rel="stylesheet" href="/Script/Kindeditor/plugins/code/prettify.css" />

    <style type="text/css">
        #tabMail
        {
        }
        #tabMail tr
        {
            height: 20px;
        }
        
        #tabMail td
        {
            padding: 5px;
        }
        .uploadify-button
        {
            background-color: transparent;
            border: 0;
            padding: 0;
        }
        
        .uploadify:hover .uploadify-button
        {
            background-color: transparent;
            border: 0;
            padding: 0;
        }
    </style>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <!-- jQuery.EasyUI-->
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <!-- jQuery.EasyUI-->
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
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
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/Script/Kindeditor/kindeditor-min.js"></script>
    <script type="text/javascript" src="/Script/Kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="/Script/Kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript">
        var editor1;
        KindEditor.ready(function (K) {
            editor1 = K.create('#txtContent', {
                cssPath: '/Script/Kindeditor/plugins/code/prettify.css',
                uploadJson: '/Script/Kindeditor/upload_json.ashx',
                fileManagerJson: '/Script/Kindeditor/file_manager_json.ashx',
                items: []
            });
            prettyPrint();

            // 自适应高度
            var height = K.IE ? editor1.edit.doc.body.scrollHeight : editor1.edit.doc.body.offsetHeight + 20;
            if (height < 300) {
                height = 300;
            }
            editor1.edit.setHeight(height);
            editor1.readonly();
        });

        //回复邮件内容
        function UpdateMail() {
            parent.parent.desktop.PrjInfoAdd = window;
            var url = "/OA2/Mail/WriteMail.aspx?mailId=" + document.getElementById('hfldMailId').value;
            this.location.href = url;
        }
        //转发邮件
        function Transmit() {
            parent.parent.desktop.PrjInfoAdd = window;
            var url = "/OA2/Mail/WriteMail.aspx?mailId=" + document.getElementById('hfldMailId').value + "&Transmit=" + "1";
            this.location.href = url;
        }

        //页面跳转到收件箱
        function selectSend() {
            parent.parent.desktop.ViewMail = window;
            var url;
            if (document.getElementById('hfldMailTY').value == "O") {
                url = '/OA2/Mail/Outbox.aspx?';
            }
            else if (document.getElementById('hfldMailTY').value == "L") {
                url = '/OA2/Mail/Deleted.aspx?';
            }
            else {
                url = '/OA2/Mail/Inbox.aspx?';
            }
            this.location.href = url;
        }

        //后台绑定附件
        function uploadbind(file, fileSize) {
            var $tr = $('<tr></tr>');
            if (file.length > 10) {
                var $tdName = $('<td style="width: ' + (100 + (file.length - 10) * 100) + 'px;">' + file + '</td>');
            }
            else {
                var $tdName = $('<td style="width: 100px;">' + file + '</td>');
            }
            var $tdSize = $('<td style="width: 25%;">' + fileSize + '</td>');
            if (file.length > 10) {
                var $tdDel = $('<td style="width: ' + (50 - (file.length - 10) / 100) + '%;"></td>');
            }
            else {
                var $tdDel = $('<td style="width: 50%;"></td>');
            }
            $tr.append($tdName);
            $tr.append($tdSize);
            $tr.append($tdDel);
            $('#annexTab').append($tr);
        }
    </script>
</head>
<body class="body_frame">
    <form id="form1" runat="server">
    <div style="height: 95%;" class="divContent2">
        <table id="tabMail" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="divFooter" style="text-align: left; padding: 3px 0px; background: #E8F2FC;
                    border-bottom: 1px solid #A9BCE6; margin: 0px; vertical-align: middle;">
                    <input type="button" id="btnReturn1" value="返回" style="border: 1px solid #98bae7;
                        margin-left: 1%;" onclick="selectSend()" />
                    <input type="button" id="btnEdit1" value="回复" style="border: 1px solid #98bae7;" onclick="UpdateMail()" runat="server" />

                    <input type="button" id="btnTransmit1" value="转发" style="border: 1px solid #98bae7;" onclick="Transmit() " runat="server" />

                    <asp:Button ID="btnDelete1" Text="删除" Style="border: 1px solid #98bae7;" OnClick="btnDelete_Click" runat="server" />
                    <asp:Button ID="btnFront1" Width="70px" Style="border: 1px solid #98bae7;" Text="上一封" OnClick="btnFront_Click" runat="server" />
                    <asp:Button ID="btnbehind1" Width="70px" Style="border: 1px solid #98bae7;" Text="下一封" OnClick="btnbehind_Click" runat="server" />
                    <asp:Button ID="btnMove1" Text="转移到" Style="border: 1px solid #98bae7;" OnClick="btnMove_Click" runat="server" />
                    <asp:DropDownList ID="DDLtBox1" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 100%;">
                <td style="height: 100%; vertical-align: top;">
                    <div id="divScroll" runat="server">
                        <table width="100%" height="100%" cellpadding="0" cellspacing="0" id="tb">
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblTitle" Font-Bold="true" Font-Size="20px" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    发件人：
                                </td>
                                <td align="left" style="width: 100%;">
                                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    时&nbsp;&nbsp;&nbsp;&nbsp;间：
                                </td>
                                <td style="width: 700px;">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    收件人：
                                </td>
                                <td>
                                    <asp:Label ID="lblTo" Width="700px" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="TRMailTo" runat="server"><td runat="server">
                                    抄送人：
                                </td><td runat="server">
                                    <asp:Label ID="lblMailTo" Width="700px" runat="server"></asp:Label>
                                </td></tr>
                            <tr id="TRUpload" runat="server"><td valign="top" runat="server">
                                    附&nbsp;&nbsp;&nbsp;&nbsp;件：
                                </td><td id="upload" runat="server">
                                    <table id="annexTab" border="0" cellpadding="0" cellspacing="0" style="width: 500px;" runat="server"></table>
                                </td></tr>
                            <tr>
                                <td colspan="2" id="KE" height="100%" runat="server">
                                    <asp:Label ID="lblContent" Text="" Height="100%" Visible="false" runat="server"></asp:Label>
                                    <textarea id="txtContent" rows="1" cols="1" style="width: 99%; visibility: hidden;" runat="server"></textarea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 5%;" class="divFooter2">
        <table class="tableFooter2" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; padding: 3px 0px; background: #E8F2FC; border: 1px solid #A9BCE6;
                    margin: 0px; vertical-align: middle;">
                    <input type="button" id="btnReturn" value="返回" style="border: 1px solid #98bae7;
                        margin-left: 1%;" onclick="selectSend()" />
                    <input type="button" id="btnEdit" value="回复" style="border: 1px solid #98bae7;" onclick="UpdateMail()" runat="server" />

                    <input type="button" id="btnTransmit" value="转发" style="border: 1px solid #98bae7;" onclick="Transmit()" runat="server" />

                    <asp:Button ID="btnDelete" Text="删除" Style="border: 1px solid #98bae7;" OnClick="btnDelete_Click" runat="server" />
                    <asp:Button ID="btnFront" Width="70px" Style="border: 1px solid #98bae7;" Text="上一封" OnClick="btnFront_Click" runat="server" />
                    <asp:Button ID="btnbehind" Width="70px" Style="border: 1px solid #98bae7;" Text="下一封" OnClick="btnbehind_Click" runat="server" />
                    <asp:Button ID="btnMove" Text="转移到" Style="border: 1px solid #98bae7;" OnClick="btnMove_Click" runat="server" />
                    <asp:DropDownList ID="DDLtBox" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldMailId" runat="server" />
    <asp:HiddenField ID="hfldMailTY" runat="server" />
    </form>
</body>
</html>
