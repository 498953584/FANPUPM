<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="affairedit.aspx.cs" Inherits="AffairEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<html>
<head id="Head1" runat="server"><title>
        <%=AffairEdit._showtitle %></title><link href="../../StockManage/skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

    <script src="../../Script/jquery.js" type="text/javascript"></script>
    <script src="../../StockManage/script/jquery.wysiwyg.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script language="javascript">
        window.onload = function () {
            var mark = $('hdnmark').val(); // document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
        }
        function GetClass() {
            var Class = document.getElementById('hdnDacumClass').value;
            var Arr = new Array();
            var url = "Main.aspx?type=" + Class + "";
            Arr = window.showModalDialog(url, "", 'dialogHeight:434px;dialogWidth:700px;center:1;help:0;status:0;');
            if (Arr.length != 0) {
                document.getElementById('txtClass').value = Arr[1];
                document.getElementById('hdnClass').value = Arr[0];
            }
        }
        function checklen(e, maxlength) {
            if (e.value.length > maxlength) {
                alert('输入长度不能大于' + maxlength);
                e.focus();
                return false;
            }
        }
        $(function () {
            var temFl = $("#cbkmark").attr("checked");
            if (temFl) {
                $("#c1").show();
                $("#c2").show();
            } else {
                $("#c1").hide();
                $("#c2").hide();
            }
            var page_type = request("Type");
            if (page_type.toLowerCase() == "add") {
                $("#showText").hide();
            }
            if (page_type.toLowerCase() == "see") {
                $("#FileLink1_But_UpFile").attr("disabled", "disabled");
                //$("#FileLink1_Btn_Upload").attr("disabled", "disabled");
                $("#FileLink1_txtFile").attr("disabled", "disabled");
            }
            $("#DDTClass").val($("#hidenClass").val());
            $("#cbkmark").click(function () {
                var flag = $(this).attr("checked");
                if (flag) {
                    $("#c1").show();
                    $("#c2").show();
                } else {
                    $("#c1").hide();
                    $("#c2").hide();
                }
            });
        });
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
        function checkSubmitForm() {
            //		        var tf = true;
            //		        if ($.trim($("#TxtAffairTitle").val()) == "") {
            //		            $("#TxtAffairTitle").focus();
            //		            var tt = $("#TxtAffairTitle");
            //		            var mes = $(tt).parent().prev(".word").text();
            //		            mes = mes.replace(":", "");
            //		            alert(mes + "不能为空！");
            //		            tf = false;
            //		        }
            // return tf;
        }
        // 取消
        function btnCancelClick() {
            top.ui.closeTab();
        } 
    </script>
    <style type="text/css">
        .style1
        {
            width: 203px;
            text-align: left;
            white-space: nowrap;
        }
        .showText
        {
            border: 1px solid;
            width: 100%;
            height: 210px;
            color: #B6B6B6;
        }
    </style>
</head>
<body scroll="no">
    <form id="Form1" method="post" onsubmit="return checkSubmitForm();" runat="server">
    <div class="divContent2">
        <table width="100%" style="display: none;">
            <tr>
                <td class="divHeader">
                    <asp:Label ID="LblHead" runat="server"></asp:Label>
                    <%=AffairEdit._showtitle %>
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellspacing="0" width="100%" cellpadding="5px" border="0">
            <tr>
                <td class="word">
                    <%=AffairEdit._showAffairTitle %>：
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="TxtAffairTitle" Width="100%" MaxLength="150" runat="server"></asp:TextBox>
                    
                    <input id="hdnClass" type="hidden" name="hdnClass" runat="server" />

                </td>
                <td class="word">
                    发生日期：
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="CalDate" Width="99%" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    作为归档资料：
                </td>
                <td class="txt" colspan="0">
                    <asp:CheckBox ID="cbkmark" runat="server" />
                </td>
                <td class="word">
                    <div id="c1">
                        归档类别：
                    </div>
                </td>
                <td class="txt">
                    <div id="c2">
                        <asp:HiddenField ID="hidenClass" runat="server" />
                        <JWControl:DropDownTree ID="DDTClass" Width="100%" runat="server"></JWControl:DropDownTree>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件：
                </td>
                <td class="style2" colspan="3">
                    &nbsp;<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="vertical-align: top;">
                    内容：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TxtContext" CssClass="wysiwyg" Width="103%" TextMode="MultiLine" Height="210px" onkeyup="checklen(this,1800);" runat="server"></asp:TextBox>
                    <div id="showText" style="word-wrap: break-word; word-break: break-all;
                        display: block; width: 900px;" class="lineTable showText" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="word" style="vertical-align: top;">
                    备注：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Txtremark" Width="100%" TextMode="MultiLine" Height="60px" onkeyup="checklen(this,290);" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                        <input id="Button1" onclick="btnCancelClick();" type="button" value="取 消" runat="server" />

                        &nbsp;
                        <input id="hdnDacumClass" type="hidden" runat="server" />

                    </td>
                </tr>
            </table>
        </div>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    <div id="cen" runat="server">
        <script type="text/javascript">
            (function ($) {
                $('.wysiwyg').wysiwyg({
                    controls: {
                        strikeThrough: { visible: true },
                        underline: { visible: true },

                        separator00: { visible: true },

                        justifyLeft: { visible: true },
                        justifyCenter: { visible: true },
                        justifyRight: { visible: true },
                        justifyFull: { visible: true },

                        separator01: { visible: true },

                        indent: { visible: true },
                        outdent: { visible: true },

                        separator02: { visible: true },

                        subscript: { visible: true },
                        superscript: { visible: true },

                        separator03: { visible: true },

                        undo: { visible: false },
                        redo: { visible: false },

                        separator04: { visible: false },

                        insertOrderedList: { visible: true },
                        insertUnorderedList: { visible: true },
                        insertHorizontalRule: { visible: true },

                        h4mozilla: { visible: false && $.browser.mozilla, className: 'h4', command: 'heading', arguments: ['h4'], tags: ['h4'], tooltip: "Header 4" },
                        h5mozilla: { visible: false && $.browser.mozilla, className: 'h5', command: 'heading', arguments: ['h5'], tags: ['h5'], tooltip: "Header 5" },
                        h6mozilla: { visible: false && $.browser.mozilla, className: 'h6', command: 'heading', arguments: ['h6'], tags: ['h6'], tooltip: "Header 6" },

                        h4: { visible: false && !($.browser.mozilla), className: 'h4', command: 'formatBlock', arguments: ['<H4>'], tags: ['h4'], tooltip: "Header 4" },
                        h5: { visible: false && !($.browser.mozilla), className: 'h5', command: 'formatBlock', arguments: ['<H5>'], tags: ['h5'], tooltip: "Header 5" },
                        h6: { visible: false && !($.browser.mozilla), className: 'h6', command: 'formatBlock', arguments: ['<H6>'], tags: ['h6'], tooltip: "Header 6" },

                        separator05: { visible: false },
                        separator06: { visible: false },
                        separator07: { visible: false },

                        cut: { visible: false },
                        copy: { visible: false },
                        insertImage: { visible: false },
                        paste: { visible: false }
                    }
                });
            })(jQuery);
        </script>
        <asp:HiddenField ID="hdnmark" runat="server" />
    </div>
    </form>
</body>
</html>
