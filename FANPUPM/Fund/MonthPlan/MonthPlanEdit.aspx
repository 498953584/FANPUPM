<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthPlanEdit.aspx.cs" Inherits="Fund_MonthPlan_MonthPlanEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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
        var _mpid = "";
        var tf = true;
        $(function() {
            Val.validate('form1');
            _mpid = request("mpid");
           // en();
        });              
        function selectContr() {
                    var addoper = 'addoper';
            if (request("plantype") == "payout") {
                document.getElementById("divFram").title = "选择支出合同";
                document.getElementById("ifFram").src = "/Fund/MonthPlan/DivSelectContrtAccon.aspx?Method=returnAccon&typevalue=" + addoper + "&prjcode=" + request("prjcode");
            } else {
                document.getElementById("divFram").title = "选择收入合同";
                document.getElementById("ifFram").src = "/Fund/MonthPlan/DivSelectIncometCont.aspx?Method=returnContr&typevalue=" + addoper + "&prjcode=" + request("prjcode");
            }
            selectnEvent('divFram');
            $('#divFram').dialog({ width: 800, height: 420, modal: true });
        }
        function returnAccon(id, name) {
            document.getElementById('hdfcontrcn').value = id;
            document.getElementById('txtContr').value = name;
            $.ajax({
                type: "POST",
                url: "verificationHandler.ashx?e=" + Math.random(),
                data: "mpid=" + _mpid + "&ContractID=" + id + "",
                success: function(msg) {
                    if (msg == "true") {
                        tf = false;
                        $("#btnSave").attr("disabled", "disabled");
                        alert("此合同资金计划信息已存在!");
                    } else if (msg == "false") {
                        $("#btnSave").removeAttr("disabled");
                        tf = true;
                    } else {
                        alert("系统错误!");
                    }
                }
            });
        }
        function returnContr(id, name) {
            document.getElementById('hdfcontrcn').value = id;
            document.getElementById('txtContr').value = name;
            $.ajax({
                type: "POST",
                url: "verificationHandler.ashx?e=" + Math.random(),
                data: "mpid=" + _mpid + "&ContractID=" + id + "",
                success: function(msg) {
                    if (msg == "true") {
                        tf = false;
                        $("#btnSave").attr("disabled", "disabled");
                        alert("此合同资金计划信息已存在!");
                    } else if (msg == "false") {
                        $("#btnSave").removeAttr("disabled");
                        tf = true;
                    } else {
                        alert("系统错误!");
                    }
                }
            });
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
       
        function checkFormState() {
          
            return tf;
        }
        function btnCancel_onclick() {
            top.frameWorkArea.window.desktop.getActive().close();
        }
        function divCloses() {
            divClose(parent);
        }
    </script>

</head>
<body>
    <form id="form1" onsubmit="return checkFormState();" runat="server">
    <div class="divHeader2">
        
    </div>
    <div class="divContent2">
        <table class="" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    依据合同
                </td>
                <td class="txt mustInput">
                    <span id="span2" class="spanSelect">
                        <input type="text" style="width: 95%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" contenteditable="false" id="txtContr" class="{required:true, messages:{required:'依据合同必须输入'}}" runat="server" />

                        <img alt="选择依据合同" onclick="selectContr();" src="/images/icon.bmp" style="float: right;" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    上期结余
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtOldBalance" class="{number:true, messages:{number:'本期金额格式错误'}}" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    本期金额
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtPlanMoney" class="{number:true, messages:{number:'本期金额格式错误'}}" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="word">
                    所属科目
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtPlansubject" Style="width: 95%; height: 15px;
                        border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hdfcontrcn" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    备 注
                </td>
                <td class="txt">
                    <asp:TextBox ID="TextBox2" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附 件
                </td>
                <td class="txt">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="IndirectCost" Visible="true" runat="server" />
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" Visible="false" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="return btnCancel_onclick()" />
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
