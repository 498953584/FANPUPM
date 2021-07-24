<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expertprojectedit.aspx.cs" Inherits="ExpertProjectEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>专项方案</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script language="javascript">
        window.onload = function () {
            document.getElementById('BtnSave').onclick = btnSaveClick;
        }

        function btnChecked() {
            var cbkmark = document.getElementById("cbkmark"); //获取元素
            if (cbkmark.status == false)//判断是否选中
                document.getElementById("TrGDType").style.display = "none"; //没有选中，隐藏元素
            else
                document.getElementById("TrGDType").style.display = "block"; //选中，显示元素
        }

        function btnSaveClick() {
            if (!document.getElementById('TxtExperName').value) {
                alert("系统提示：\n\n方案名称不能为空");
                return false;
            }
        }
        function openannexpage(RecordCode, type) {
            if (type != "View") {
                window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1722&rc=" + RecordCode + "&at=0&type=2", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');

            }
            else {
                window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1722&rc=" + RecordCode + "&at=0&type=1", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
            }

        }

        function PopDpt(obj1, obj2) {
            var ret = window.showModalDialog("/Tender/ProjectInfoManage/PopDpt.aspx", window, "dialogWidth:240px;dialogHeight:400px");

            obj1.value = ret[1];
            obj2.value = ret[0];
            //document.getElementById("HdnUnit").value = ret[0];
            //document.getElementById("TxtUnit").value = ret[1];			

        }
        function showSingleSelectMaster(txtchecker, yhdm) {
            txtchecker.value = "";
            var url = "../../common/SelectSingleMan.aspx";
            var Man = new Array(new Array(), new Array());
            window.showModalDialog(url, Man, "dialogHeight:475px;dialogWidth:480px;center:1;help:0;status:0;");

            if (Man[0].length > 0) {
                txtchecker.value = Man[0][0];
                yhdm.value = Man[1][0];
            }
        }
        //选择人员
        function selectUser() {
            document.getElementById("divFramPrj").title = "选择人员";
            document.getElementById("ifFramPrj").src = "/Common/DivSelectUser.aspx?Method=returnUser";
            selectnEvent('divFramPrj');
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('HdnWeaver').value = id;
            document.getElementById('TxtWeave').value = name;
        }
    </script>
</head>
<body scroll="auto">
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader">
                    <asp:Label ID="lb_change" Font-Bold="true" runat="server"></asp:Label>专项方案
                </td>
            </tr>
        </table>
        <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" width="100%"
            border="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    填报单位：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TxtUnit" ReadOnly="true" runat="server"></asp:TextBox><input id="HdnUnit" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    工程名称：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TxtPrjName" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    方案名称：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TxtExperName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    审核情况：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TxtAuditCircs" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件：
                </td>
                <td colspan="3" align="left" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
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
                    方案描述：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TxtScript" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    备注：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TxtRemark" TextMode="MultiLine" Rows="3" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    编制人：
                </td>
                <td class="txt">
                    <span id="span1" class="spanSelect" style="width: 95%;">
                        <asp:TextBox ID="TxtWeave" Style="width: 75%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
                        <img src="/images/icon.bmp" style="float: right;" alt="请双击此处选择" id="img1" onclick="selectUser();" />
                    </span>
                    <input id="HdnWeaver" type="hidden" name="Hidden2" runat="server" />

                </td>
                <td class="word" style="white-space: nowrap;">
                    编制日期：
                </td>
                <td class="txt">
                    <JWControl:DateBox ID="DateWeaverTime" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    填报人：
                </td>
                <td class="txt">
                    <asp:TextBox ID="TxtFillName" Width="95%" ReadOnly="true" runat="server"></asp:TextBox><input id="HdnFillName" type="hidden" name="Hidden1" runat="server" />

                </td>
                <td class="word" style="white-space: nowrap;">
                    填报日期：
                </td>
                <td class="txt">
                    <JWControl:DateBox ID="DateFillTime" Enabled="false" runat="server"></JWControl:DateBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td class="td-submit" colspan="4">
                        <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                        <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                        <input onclick="javascript:top.ui.closeTab();" type="button" value="取 消" id="BunClose" name="BunClose" runat="server" />

                    </td>
                </tr>
            </table>
        </div>
        <div id="divFram" title="" style="display: none">
            <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
        <div id="divFramPrj" title="" style="display: none">
            <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
    </div>
    <asp:HiddenField ID="hdnFlowGuid" runat="server" />
    </form>
</body>
</html>
