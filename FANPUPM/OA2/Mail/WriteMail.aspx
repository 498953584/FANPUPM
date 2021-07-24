<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WriteMail.aspx.cs" Inherits="OA2_WriteMail" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!doctype html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>写邮件</title><link type="text/css" rel="Stylesheet" href="../../UserControl/FileUpload/uploadify/uploadify.css" />
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
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/Kindeditor/kindeditor-min.js"></script>
    <script type="text/javascript" src="/Script/Kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="/Script/Kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="/UserControl/FileUpload/uploadify/jquery.uploadify.min.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/JavaScript">
        $(document).ready(function () {
            $('#txtTo').attr('readOnly', true);
            $('#txtCopyto').attr('readOnly', true);

            //按钮的点击后的样式
            function funonclickCss(name) {
                parent.document.getElementById(name).style.background = 'url(/images1/email/over.gif) repeat-x';
                parent.document.getElementById(name).style.fontWeight = 'bold';
                parent.document.getElementById(name).style.color = '#004592';
            }

            //按钮的点击前的样式
            function funfrontCss(name) {
                parent.document.getElementById(name).style.background = 'url(/images1/email/understood.gif) repeat-x';
                parent.document.getElementById(name).style.fontWeight = 'normal';
                parent.document.getElementById(name).style.color = '#333';
            }

            funonclickCss('spanwrite_mail');
            funonclickCss('write_mail');
            funfrontCss('spaninbox');
            funfrontCss('inbox');
            funfrontCss('spandraft');
            funfrontCss('draft');
            funfrontCss('spanrepeal');
            funfrontCss('repeal');
            funfrontCss('spanoutbox');
            funfrontCss('outbox');
            funfrontCss('spandeleted');
            funfrontCss('deleted');

            // 上传路径
            var folder = $('#hfldFolder').val();
            //判断上传是否正确
            var success = true;
            $(function () {
                $('#uploadify').uploadify({
                    buttonImage: '/images1/email/addfile.jpg',
                    swf: '/UserControl/FileUpload/uploadify/uploadify.swf',
                    uploader: '/UserControl/FileUpload/FileUpload3.ashx?folder=' + folder,
                    height: 17,
                    width: 76,
                    onUploadStart: function (file) {
                        document.getElementById('btnSend').setAttribute('disabled', 'disabled');
                        document.getElementById('btnSend1').setAttribute('disabled', 'disabled');
                    },
                    onUploadSuccess: function (file) {
                        document.getElementById('btnSend').removeAttribute('disabled');
                        document.getElementById('btnSend1').removeAttribute('disabled');
                        success = true;
                    },
                    onUploadError: function (file) {
                        document.getElementById('btnSend').removeAttribute('disabled');
                        document.getElementById('btnSend1').removeAttribute('disabled');
                        success = false;
                    },
                    onUploadComplete: uploadComplete
                });
            });

            // 删除完成事件
            function uploadComplete(file) {
                if (success == true) {
                    var fileN = getName(file.name, 1).split(';');

                    var $tr = $('<tr></tr>');
                    if (fileN[fileN.length - 1].length > 10) {
                        var $tdName = $('<td style="width: ' + (100 + (file.name.length - 10) * 100) + 'px;">' + fileN[fileN.length - 1] + '</td>');
                    }
                    else {
                        var $tdName = $('<td style="width: 100px;">' + fileN[fileN.length - 1] + '</td>');
                    }
                    var $tdSize = $('<td style="width: 50px;">' + (file.size / 1048576).toFixed(2) + 'M</td>');
                    var $tdDel = $('<td></td>');
                    var $img = $('<img class="deleteAnnex" style="cursor:pointer" src="/images/cancel_2.png" alt="删除" />')
                    $img.click(function () {
                        deleteAnnex(this, folder);
                    });
                    $tdDel.append($img);

                    $tr.append($tdName);
                    $tr.append($tdSize);
                    $tr.append($tdDel);
                    $('#annexTab').append($tr);
                }
            }

            function getName(filename, n) {
                var T = true;
                var table = document.getElementById('annexTab');
                for (var i = 0; i < table.rows.length; i++) {
                    var tdValue = table.rows[i].cells[0].innerText;
                    if (tdValue == filename) {
                        var files = tdValue.split('.');
                        filename = files[0];
                        for (var k = 1; k < files.length; k++) {
                            if (k == files.length - 1) {
                                if (n > 1) {
                                    filename = filename.substr(0, filename.length - 2);
                                    filename = filename + n.toString() + ").";
                                }
                                else {
                                    filename = filename + "(1).";
                                }
                            }
                            filename = filename + files[k];
                        }
                        T = false;
                        n = n + 1;

                    }
                }
                if (n == 1) {
                    return filename;
                }
                else {
                    if (!T) {
                        filename = filename + ";" + getName(filename, n);
                    }
                    return filename;
                }
            }

            // 删除附件
            function deleteAnnex(img) {
                var $tr = $(img).parent().parent();
                var annexName = $tr.find('td').first().html();

                $.get('/UserControl/FileUpload/DeleteFile.ashx?' + new Date().getTime(), { File: folder + '/' + annexName }, function (data) {
                    if (data == '1') {
                        $tr.remove();
                    }
                    else {
                        alert('系统提示：\n\n删除失败！');
                    }
                });
            }

            //截取字符串
            function SetString(str, len) {
                var strlen = 0;
                var s = "";
                for (var i = 0; i < str.length; i++) {
                    if (str.charAt(i) == '.' && i >= len - 4) {
                        return s = s + str.charAt(str.length - 1) + str.charAt(str.length - 2) + str.charAt(str.length - 3) + str.charAt(str.length - 4);
                    }
                    if (str.charCodeAt(i) > 128) {
                        strlen += 2;
                    } else {
                        strlen++;
                    }
                    s += str.charAt(i);
                    if (strlen >= len) {
                        s = s + str.charAt(str.length - 1) + str.charAt(str.length - 2) + str.charAt(str.length - 3) + str.charAt(str.length - 4);
                        return s;
                    }
                }
                return s;
            }
        });

        //后台绑定附件
        function uploadbind(file, fileSize, filepath) {
            var $tr = $('<tr></tr>');
            if (file.length > 10) {
                var $tdName = $('<td style="width: ' + (100 + (file.length - 10) * 100) + 'px;">' + file + '</td>');
            }
            else {
                var $tdName = $('<td style="width: 100px;">' + file + '</td>');
            }
            var $tdSize = $('<td style="width: 50px;">' + fileSize + '</td>');
            var $tdDel = $('<td></td>');
            var $img = $('<img class="deleteAnnex" style="cursor:pointer" src="/images/cancel_2.png" alt="删除" />')
            $img.click(function () {
                deleteAnnexBind(this, filepath);
            });
            $tdDel.append($img);
            $tr.append($tdName);
            $tr.append($tdSize);
            $tr.append($tdDel);
            $('#annexTab').append($tr);

        }

        // 后台绑定附件删除
        function deleteAnnexBind(img) {
            var $tr = $(img).parent().parent();
            var annexName = $tr.find('td').first().html();
            $.get('/UserControl/FileUpload/DeleteFile.ashx?' + new Date().getTime(), { File: $('#hfldFolder').val() + '/' + annexName }, function (data) {
                if (data == '1') {
                    $tr.remove();
                }
                else {
                    alert('系统提示：\n\n删除失败！');
                }
            });
        }


        //发送成功的提示信息
        function selectSend() {
            parent.parent.desktop.ViewMail = window;
            var url = '/OA2/Mail/MailView.aspx?zt=1&&mailId=' + document.getElementById("hfldMailId").value;
            this.location.href = url;
        }

        //保存成功的提示信息
        function selectSave() {
            parent.parent.desktop.ViewMail = window;
            var url = '/OA2/Mail/MailView.aspx?zt=0';
            this.location.href = url;
        }

        //发送前的判断
        function selectIsSend() {
            if ((document.getElementById('txtTo').value) == "" && (document.getElementById('txtCopyto').value) == "") {
                alert("没有收件人!");
                return false;
            }
            else {
                if ((document.getElementById('txtName').value) == "") {
                    return confirm('主题为空,确定要发送吗？');
                }
            }
        }

        var editor1;
        KindEditor.ready(function (K) {
            editor1 = K.create('#txtContent', {
                cssPath: '/Script/Kindeditor/plugins/code/prettify.css',
                uploadJson: '/Script/Kindeditor/upload_json.ashx',
                fileManagerJson: '/Script/Kindeditor/file_manager_json.ashx',
                //allowFileManager : true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                },
                items: [
                    'source', '|', 'undo', 'redo', '|',
                    'preview', 'print', 'code', 'cut', 'copy', 'paste', 'plainpaste', 'wordpaste', '|',
                    'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
                    'superscript', 'clearhtml', 'quickformat', 'selectall', '|',
                    'fullscreen', 'formatblock', 'fontname', 'fontsize', '|',
                    'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|',
                    'image', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
                    'anchor', 'link', 'unlink',
                    ]

            });
            prettyPrint();
            // 自适应高度
            var height = K.IE ? editor1.edit.doc.body.scrollHeight : editor1.edit.doc.body.offsetHeight + 20;
            if (height < 200) {
                height = 200;
            }
            editor1.edit.setHeight(height);
        });

        function select() {
            $('#TRCopy').show();
            $('#img').hide();
        }

    </script>
</head>
<body class="body_frame" style="height: 100%">
    <form id="form11" runat="server">
    <div style="height: 95%;" class="divContent2">
        <table id="tabMail" width="100%" cellpadding="0" cellspacing="0">
            <tr style="width: 100%">
                <td colspan="2" style="text-align: left; padding: 3px 0px; background: #E8F2FC; border-bottom: 1px solid #A9BCE6;
                    margin: 0px; vertical-align: middle;">
                    <asp:Button ID="btnSend" Style="margin-left: 1%; padding: 0px; border: 1px solid #98bae7;" Text="发送" OnClientClick=" return selectIsSend();" OnClick="btnSend_Click" runat="server" />
                    <asp:Button ID="btnSaveDraft" Style="margin: 0px; padding: 0px; border: 1px solid #98bae7;" Width="70px" Text="存草稿" OnClick="btnSaveDraft_Click" runat="server" />
                    &nbsp;&nbsp
                    <asp:Label ID="lblsave" ForeColor="#2B61A2" Font-Bold="true" Font-Size="12px" Text="" Visible="false" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 6%; text-align: center;">
                    收件人
                </td>
                <td style="width: 94%;">
                    <span class="spanSelect" style="width: 90%">
                        <asp:TextBox ID="txtTo" Style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images1/email/add.jpg" style="float: right; border: none;" alt="选择" id="imgName" onclick="jw.selectMultiUser({ nameinput: 'txtTo', codeinput: 'hfldTo' });" runat="server" />

                    </span>
                    <input id="hfldTo" type="hidden" style="width: 1px" runat="server" />

                </td>
            </tr>
            <tr id="TRCopy" style="display: none; width: 6%; text-align: center;" runat="server"><td runat="server">
                    抄&nbsp;&nbsp;&nbsp;&nbsp;送
                </td><td runat="server">
                    <span class="spanSelect" style="width: 90%;">
                        <asp:TextBox ID="txtCopyto" Style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images1/email/add.jpg" style="float: right;" alt="选择" id="img1" onclick="jw.selectMultiUser({ nameinput: 'txtCopyto', codeinput: 'hfldCopyto' });" runat="server" />

                    </span>
                    <input id="hfldCopyto" type="hidden" style="width: 1px" runat="server" />

                </td></tr>
            <tr>
                <td style="width: 6%; text-align: center;">
                    主&nbsp;&nbsp;&nbsp;&nbsp;题
                </td>
                <td>
                    <asp:TextBox ID="txtName" MaxLength="200" Width="90%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 6%;">
                <td valign="top">
                </td>
                <td>
                    <table style="width: 92%; vertical-align: top;">
                        <tr>
                            <td width="300px;">
                                <input type="file" name="uploadify" id="uploadify" />
                            </td>
                            <td align="right" style="display: inline; vertical-align: top;">
                                <img src="../../images1/email/addcopy.jpg" style="float: left;" id="img" onclick="select()" runat="server" />

                                <asp:CheckBox ID="chkMobileMsg" Style="margin-right: 10px;" Checked="false" Text="手机短信" runat="server" />
                                <asp:CheckBox ID="chkDbsj" Checked="true" Text="待办工作" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table id="annexTab" border="0" cellpadding="0" cellspacing="0" style="width: 500px;" runat="server"></table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <textarea id="txtContent" cols="100" rows="50" style="width: 92%; visibility: hidden;" runat="server"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 5%;" class="divFooter2">
        <table class="tableFooter2" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; padding: 3px 0px; background: #E8F2FC; border: 1px solid #A9BCE6;
                    margin: 0px; vertical-align: middle;">
                    <asp:Button ID="btnSend1" Style="margin-left: 1%; padding: 0px; border: 1px solid #98bae7;" Text="发送" OnClientClick=" return selectIsSend();" OnClick="btnSend_Click" runat="server" />
                    <asp:Button ID="btnSaveDraft1" Style="margin: 0px; padding: 0px; border: 1px solid #98bae7;" Text="存草稿" Width="70px" OnClick="btnSaveDraft_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldAnnexId" runat="server" />
    <asp:HiddenField ID="hfldMailId" runat="server" />
    <asp:HiddenField ID="hfldFolder" runat="server" />
    </form>
</body>
</html>
