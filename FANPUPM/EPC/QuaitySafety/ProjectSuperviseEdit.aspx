<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ProjectSuperviseEdit.aspx.cs" Inherits="ProjectSuperviseEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<html>
<head id="HEAD1" runat="server"><title>
        <%=ProjectSuperviseEdit._showtitle %></title><link href="/StockManage/skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script src="/StockManage/script/jquery.wysiwyg.js" type="text/javascript"></script>
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
        // 取消
        function btnCancelClick() {
            top.ui.closeTab();
        } 
    </script>
    <style type="text/css">
        .showText
        {
            border: 1px solid;
            width: 100%;
            height: 210px;
            color: #B6B6B6;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="divHeader2">
        <table width="100%">
            <tr>
                <td class="divHeader" style="height: 28px;">
                    <asp:Label ID="LblHead" runat="server"></asp:Label>
                    <%=ProjectSuperviseEdit._showtitle %>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2">
        <table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    <%=ProjectSuperviseEdit._showAffairTitle %>：
                </td>
                <td class="txt">
                    <asp:TextBox ID="TxtAffairTitle" Width="100%" MaxLength="150" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Width="95px" ControlToValidate="TxtAffairTitle" ErrorMessage="RequiredFieldValidator" runat="server">*
					        不能为空
                    </asp:RequiredFieldValidator>
                    <input id="hdnClass" type="hidden" name="hdnClass" runat="server" />

                </td>
                <td class="word">
                    发生日期：
                </td>
                <td class="txt">
                    <JWControl:DateBox ID="CalDate" Width="99%" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    作为归档资料：
                </td>
                <td class="txt">
                    <asp:CheckBox ID="cbkmark" runat="server" />
                </td>
                <td class="word">
                    <div id="c1" runat="server">
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
                <td class="txt" colspan="3">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="vertical-align: top;">
                    内容：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TxtContext" TextMode="MultiLine" Width="100%" Height="210px" onkeyup="checklen(this,1800);" runat="server"></asp:TextBox>
                    <div id="showText" runat="server">
                        <asp:TextBox ID="TextBox1" TextMode="MultiLine" Width="100%" Height="210px" onkeyup="checklen(this,1800);" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="word" style="vertical-align: top;">
                    备注：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Txtremark" TextMode="MultiLine" Height="60px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
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
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
