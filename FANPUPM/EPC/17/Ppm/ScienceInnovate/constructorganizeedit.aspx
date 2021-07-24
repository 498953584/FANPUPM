<%@ Page Language="C#" AutoEventWireup="true" CodeFile="constructorganizeedit.aspx.cs" Inherits="ConstructOrganizeEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>施工组织</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self"></base>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script language="javascript">
        function btnChecked() {
            var cbkmark = document.getElementById("cbkmark"); //获取元素
            if (cbkmark.status == false)//判断是否选中
                document.getElementById("TrGDType").style.display = "none"; //没有选中，隐藏元素
            else
                document.getElementById("TrGDType").style.display = "block"; //选中，显示元素
        }

        window.onload = function () {
            document.getElementById('BtnSave').onclick = btnSaveClick;
        }
        function checklen(e, maxlength) {
            if (e.value.length > maxlength) {
                alert('输入长度不能大于' + maxlength);
                e.focus();
                return false;
            }
        }
        function btnSaveClick() {
            if (!document.getElementById('TxtPrjName').value) {
                alert("系统提示：\n\n施组名称不能为空");
                return false;
            }
        }

        //选择人员
        function selectUser() {
            document.getElementById("divFramPrj").title = "选择人员";
            document.getElementById("ifFramPrj").src = "/Common/DivSelectUser.aspx?Method=returnUser";
            selectnEvent('divFramPrj');
            //选择人员返回值
        }
        function returnUser(id, name) {
            document.getElementById('hdnPeople').value = id;
            document.getElementById('TxtWeave').value = name;
        }
    </script>
</head>
<body scroll="auto">
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader" style="width: 100%">
                    施工组织<input id="HdnPc" style="width: 72px; height: 22px" type="hidden" size="6" name="Hidden1" runat="server" />
<input id="HdnFileId" style="width: 64px; height: 22px" type="hidden" size="5" name="Hidden1" runat="server" />

                </td>
            </tr>
        </table>
        <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" border="0"
            width="100%">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    填报单位：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TxtUnit" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    施组名称：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TxtPrjName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    做为归档资料：
                </td>
                <td class="txt" colspan="3">
                    <input id="cbkmark" type="checkbox" onclick="btnChecked();" runat="server" />

                </td>
            </tr>
            <tr id="TrGDType" style="display: none;" runat="server"><td class="word" runat="server">
                    文档类别：
                </td><td class="txt" colspan="3" runat="server">
                    <JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree>
                    <input id="HdnProjectCode" style="width: 10px" type="hidden" name="HdnProjectCode" runat="server" />

                    <input id="HdnDocCode" style="width: 10px" type="hidden" name="HdnDocCode" runat="server" />

                </td></tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件：
                </td>
                <td colspan="3" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    主要描述：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TxtScript" TextMode="MultiLine" Rows="5" onkeyup="checklen(this,180);" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TxtRemark" TextMode="MultiLine" Rows="3" onkeyup="checklen(this,180);" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    编制人：
                </td>
                <td class="txt" style="padding-right: 3px">
                    <span id="span1" class="spanSelect" style="width: 99%; background-color: #FEFEF4;">
                        <input type="text" style="width: 89%; background-color: #FEFEF4; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" id="TxtWeave" runat="server" />

                        <img id="Img1" src="~/images/icon.bmp" style="float: right;" onclick="selectUser()" alt="选择编制人" runat="server" />

                        <input id="hdnPeople" type="hidden" name="hdnPeople" runat="server" />

                    </span>
                </td>
                <td class="word" style="white-space: nowrap;">
                    编制日期：
                </td>
                <td class="txt">
                    <JWControl:DateBox ID="DateWeave" runat="server"></JWControl:DateBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                        <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                        <input id="btnClose" onclick="javascript:top.ui.closeTab();" type="button" value="取 消" name="btnClose" runat="server" />
&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFramPrj" title="" style="display: none">
            <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
    </div>
    <input type="hidden" id="hdnGuid" runat="server" />

    <input type="hidden" id="hdnType" runat="server" />

    <input type="hidden" id="hdnFlowGuid" runat="server" />

    </form>
</body>
</html>
