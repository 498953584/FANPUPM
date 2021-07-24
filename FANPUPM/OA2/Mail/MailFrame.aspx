<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailFrame.aspx.cs" Inherits="OA2_MailFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>我的邮箱</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ((document.getElementById("hfldEmailID").value) != '') {
                // 查看邮件内容
                $('#iframeMail').attr('src', 'ViewMail.aspx?mailId=' + document.getElementById("hfldEmailID").value + "&reply=1");
            }

            //按钮的点击后的样式
            function funonclickCss(name) {
                document.getElementById(name).style.background = 'url(/images1/email/over.gif) repeat-x';
                document.getElementById(name).style.fontWeight = 'bold';
                document.getElementById(name).style.color = '#004592';
            }

            //按钮的点击前的样式
            function funfrontCss(name) {
                document.getElementById(name).style.background = 'url(/images1/email/understood.gif) repeat-x';
                document.getElementById(name).style.fontWeight = 'normal';
                document.getElementById(name).style.color = '#333';
            }

            // 写邮件
            $('#write_mail').click(function () {
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
                funfrontCss('deleted'); ;
                $('#iframeMail').attr('src', 'WriteMail.aspx');
            });
            // 收件箱
            $('#inbox').click(function () {
                funfrontCss('spanwrite_mail');
                funfrontCss('write_mail');
                funonclickCss('spaninbox');
                funonclickCss('inbox');
                funfrontCss('spandraft');
                funfrontCss('draft');
                funfrontCss('spanrepeal');
                funfrontCss('repeal');
                funfrontCss('spanoutbox');
                funfrontCss('outbox');
                funfrontCss('spandeleted');
                funfrontCss('deleted');
                $('#iframeMail').attr('src', 'Inbox.aspx');
            });
            // 草稿箱
            $('#draft').click(function () {
                funfrontCss('spanwrite_mail');
                funfrontCss('write_mail');
                funfrontCss('spaninbox');
                funfrontCss('inbox');
                funonclickCss('spandraft');
                funonclickCss('draft');
                funfrontCss('spanrepeal');
                funfrontCss('repeal');
                funfrontCss('spanoutbox');
                funfrontCss('outbox');
                funfrontCss('spandeleted');
                funfrontCss('deleted');
                $('#iframeMail').attr('src', 'Draft.aspx');
            });
            //撤回邮件
            $('#repeal').click(function () {
                funfrontCss('spanwrite_mail');
                funfrontCss('write_mail');
                funfrontCss('spaninbox');
                funfrontCss('inbox');
                funfrontCss('spandraft');
                funfrontCss('draft');
                funonclickCss('spanrepeal');
                funonclickCss('repeal');
                funfrontCss('spanoutbox');
                funfrontCss('outbox');
                funfrontCss('spandeleted');
                funfrontCss('deleted');
                $('#iframeMail').attr('src', 'Repeal.aspx');
            })
            // 已发送邮件
            $('#outbox').click(function () {
                funfrontCss('spanwrite_mail');
                funfrontCss('write_mail');
                funfrontCss('spaninbox');
                funfrontCss('inbox');
                funfrontCss('spandraft');
                funfrontCss('draft');
                funfrontCss('spanrepeal');
                funfrontCss('repeal');
                funonclickCss('spanoutbox');
                funonclickCss('outbox');
                funfrontCss('spandeleted');
                funfrontCss('deleted');
                $('#iframeMail').attr('src', 'Outbox.aspx');
            });
            // 已删除邮件
            $('#deleted').click(function () {
                funfrontCss('spanwrite_mail');
                funfrontCss('write_mail');
                funfrontCss('spaninbox');
                funfrontCss('inbox');
                funfrontCss('spandraft');
                funfrontCss('draft');
                funfrontCss('spanrepeal');
                funfrontCss('repeal');
                funfrontCss('spanoutbox');
                funfrontCss('outbox');
                funonclickCss('spandeleted');
                funonclickCss('deleted');
                $('#iframeMail').attr('src', 'Deleted.aspx');
            });

            showMailCount();
            setInterval('showMailCount();', 3000);
        });

        // 显示邮件数量
        function showMailCount() {
            try {
                $.ajax({
                    type: 'GET',
                    async: true,
                    url: 'Handler/GetMailCount.ashx?' + new Date().getTime(),
                    success: function (data) {
                        var obj = eval('(' + data + ')');
                        $('#inbox').val('收件箱(' + obj[0] + ')')
                        $('#draft').val('草稿箱(' + obj[1] + ')')
                        $('#repeal').val('撤回邮件(' + obj[2] + ')')
                        $('#outbox').val('已发送邮件(' + obj[3] + ')')
                        $('#deleted').val('已删除邮件(' + obj[4] + ')')
                    }
                });
            }
            catch (e) {
            }
        }

        //写邮件
        function funimgwritemail() {
            $('#write_mail').click();
        }

        //收件箱
        function funimginbox() {
            $('#inbox').click();
        }

        //草稿箱
        function funimgdraft() {
            $('#draft').click();
        }

        //撤回邮件
        function funimgrepeal() {
            $('#repeal').click();
        }

        //已发送邮件
        function funimgoutbox() {
            $('#outbox').click();
        }

        //已删除邮件
        function funimgdeleted() {
            $('#deleted').click();
        }       
    </script>
</head>
<body style="margin: 0;">
    <form id="form1" runat="server">
    <div style="width: 16%; height: 100%; margin: 0; position: absolute; border-right: 1px solid #B5CCDE;">
        <span class="spanSelect" id="spanwrite_mail" style="width: 100%; height: 25px; border: 0;
            background: url(/images1/email/understood.gif) repeat-x;">
            <img src="../../images1/email/1.gif" style="float: left; margin: 8px 2px 0 5%;" id="imgwritemal" onclick="funimgwritemail();" runat="server" />

            <input type="button" id="write_mail" value="写邮件" style="line-height: 25px; border: 0px;
                height: 25px; font-size: 12px; text-align: left; color: #333; float: left; width: 77%;
                background: url(/images1/email/understood.gif) repeat-x;" />
        </span><span class="spanSelect" id="spaninbox" style="width: 100%; height: 25px;
            border: 0; background: url(/images1/email/over.gif) repeat-x;">
            <img src="../../images1/email/2.gif" style="float: left; margin: 8px 2px 0 5%;" id="imginbox" onclick="funimginbox();" runat="server" />

            <input type="button" id="inbox" value="收件箱" style="line-height: 25px;
                border: 0px; height: 25px; color: #004592; font-weight: bold; text-align: left;
                width: 77%; background: url(/images1/email/over.gif) repeat-x;" runat="server" />

        </span><span class="spanSelect" id="spandraft" style="width: 100%; height: 25px;
            border: 0; background: url(/images1/email/understood.gif) repeat-x;">
            <img src="../../images1/email/3.gif" style="float: left; margin: 8px 2px 0 5%;" id="imgdraft" onclick="funimgdraft();" runat="server" />

            <input type="button" id="draft" value="草稿箱" style="line-height: 25px;
                border: 0px; width: 77%; height: 25px; text-align: left; color: #333; background: url(/images1/email/understood.gif) repeat-x;" runat="server" />

        </span><span class="spanSelect" id="spanrepeal" style="width: 100%; height: 25px;
            border: 0; background: url(/images1/email/understood.gif) repeat-x;">
            <img src="../../images1/email/4.gif" style="float: left; margin: 8px 2px 0 5%;" id="imgrepeal" onclick="funimgrepeal();" runat="server" />

            <input type="button" id="repeal" value="撤回邮件" style="line-height: 25px;
                border: 0px; width: 77%; height: 25px; text-align: left; color: #333; background: url(/images1/email/understood.gif) repeat-x;" runat="server" />

        </span><span class="spanSelect" id="spanoutbox" style="width: 100%; height: 25px;
            border: 0; background: url(/images1/email/understood.gif) repeat-x;">
            <img src="../../images1/email/5.gif" style="float: left; margin: 8px 2px 0 5%;" id="imgoutbox" onclick="funimgoutbox();" runat="server" />

            <input type="button" id="outbox" value="已发送邮件" style="line-height: 25px;
                border: 0px; width: 77%; height: 25px; text-align: left; color: #333; background: url(/images1/email/understood.gif) repeat-x;" runat="server" />

        </span><span class="spanSelect" id="spandeleted" style="width: 100%; height: 25px;
            border: 0; background: url(/images1/email/understood.gif) repeat-x;">
            <img src="../../images1/email/6.gif" style="float: left; margin: 8px 2px 0 5%;" id="imgdeleted" onclick="funimgdeleted();" runat="server" />

            <input type="button" id="deleted" value="已删除邮件" style="line-height: 25px;
                border: 0px; width: 77%; height: 25px; text-align: left; color: #333; background: url(/images1/email/understood.gif) repeat-x;" runat="server" />

        </span>
    </div>
    <div style="width: 84%; height: 98%; position: absolute; right: 0px; border-left: 1px solid #A9BCE6;">
        <iframe id="iframeMail" width="100%" height="100%" frameborder="0" src="Inbox.aspx">
        </iframe>
    </div>
    <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    <asp:HiddenField ID="hfldEmailID" runat="server" />
    </form>
</body>
</html>
