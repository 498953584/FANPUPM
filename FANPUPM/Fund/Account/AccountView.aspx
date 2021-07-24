<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountView.aspx.cs" Inherits="AccountView" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script language="javascript" type="text/javascript">
    // <!CDATA[
        $(function() {
            $("td").attr("style", "white-space:nowrap;");
            Val.validate('form1', 'btnSave');
            if (request("Action") == "query") {
                $("#FileLink1_But_UpFile").attr("disabled", "disabled");
                $("#FileLink1_Btn_Upload").attr("disabled", "disabled");
                $("#FileLink1_txtFile").attr("disabled", "disabled");
            }
        });
        //选择人员
        function btnSelectPerson_onclick() {
            var url = "/AccountManage/acc_Manage/Consignee.aspx";
            var human = new Array();
            window.showModalDialog(url, human, "dialogHeight:475px;dialogWidth:480px;center:1;help:0;status:0;");
            var ce = "";
            for(var i=0;i<human.length;i++) {
                ce += human[i].toString() + ",";
            }
            $("#txtauthorizer").val(ce);
        }
        //取消
        function btnCancel_onclick() {
            top.frameWorkArea.window.desktop.getActive().close();
        }
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
        function CheckInputIsFloat(keyCode, e) {
            if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13) {
            }
            else if (keyCode == 110 || keyCode == 190) {
                if (e.value == "" || e.value.indexOf(".") != -1) {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        //可以输入负数的验证
        function CheckInputIsFloat1(keyCode, e) {
            if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13 || keyCode == 189 || keyCode == 109) {
            }
            else if (keyCode == 110 || keyCode == 190) {
                if (e.value == "" || e.value.indexOf(".") != -1) {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <span id="lblTitle" style="font-weight:bold;">项目账户</span>
                </td>
            </tr>
        </table>
    </div>
       <div id="divContent2" class="divContent2">
        <table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
        <tr>
            <td class="word">附件</td>
            <td class="txt"><MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" /></td>
            <td></td>
            <td></td>
        </tr>
            <tr>
                <td class="word">
                    账户编号</td>
                <td class="txt mustInput">
                    <input id="txtaccountNum" type="text" class="{required:true, messages:{required:'账户编号必须输入'}}" style="height:15px;width:100%;" runat="server" />
</td>
                <td class="word">
                    账户名称</td>
                <td class="txt mustInput">
                    <input id="txtacountName" type="text" class="{required:true, messages:{required:'账户名称必须输入'}}" style="height:15px;width:100%;" runat="server" />
</td>
            </tr>
            <tr>
                <td class="word">
                    启动资金</td>
                <td class="txt">
                    <input id="txtinitialFund" type="text" style="height:15px;width:100%;" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" class="decimal_input   {number:true, messages:{number:'启动资金格式错误'}}" runat="server" />
</td>
                <td class="">
                    </td>
                <td class="">
                    </td>
            </tr>
            <tr>
                <td class="word">
                    权限人员</td>
                <td colspan="3" class="txt">                    
                    <textarea id="txtauthorizer" cols="20" name="S1" rows="2" style="width:100%" readonly="readonly" runat="server"></textarea>
                    <br />
                    <input id="btnSelectPerson" style="width:70px;" type="button" value="选择人员" onclick="return btnSelectPerson_onclick()" runat="server" />
</td>
            </tr>
            <tr>
                <td class="word">
                    备注</td>
                <td colspan="3" class="txt">
                    <textarea id="txtRemark" cols="20" name="S1" rows="2" style="width:100%" runat="server"></textarea></td>
            </tr>           
        </table>    
    </div>
           <div id="divFooter" class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                        <input type="button" id="btnCancel" value="取消"  onclick="return btnCancel_onclick()" />
                    </td>
                </tr>
            </table>
        </div>
      <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
