<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountPayoutEdit.aspx.cs" Inherits="Fund_AccountPayout_AccountPayoutEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账编辑</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript">
        addEvent(window,'load',function(){
            registerCancelEvent();
            setAuditState();
            document.getElementById('btnSave').onclick = btnSaveClick;
        });
        
         //选择项目
        function openProjPicker() {
            var ZHID = document.getElementById('hdnZHID').value;
            top.ui.callback = function (obj) {
                returnPrj(obj.id,obj.name);
            }
            top.ui._DivSelectProject = window;
            var url = '/Fund/AccounIncome/DivSelectProject.aspx?Method=returnPrj&ZHID=' + ZHID;
            top.ui.openWin({ title: '选择项目', url:url});
        }
        //选择项目返回值
        function returnPrj(id, name) {
            document.getElementById('hdnProjectCode').value = id;
            document.getElementById('txtPrjName').value = name;
        }
        
        function btnSaveClick() {
            if (!validate()) {
                return false;
            }
        }

        function validate() {
            if (!document.getElementById('txtAccCode').value) {
                    top.ui.alert('入账单编码不能为空');
                    return false;
            }
            if (!document.getElementById('hdnRPUID').value) {
                    top.ui.alert('费用名称或者资金支付申请不能为空');
                    return false;
            }
            if (!document.getElementById('txtInMoney').value||document.getElementById('txtInMoney').value<1) {
                top.ui.alert('本次记账金额不能为空或者零');
                return false;
            }
            var cc=document.getElementById('txtInMoney').value;
            var rr=document.getElementById('txtJianMoney').value;
            var yu=document.getElementById('hdnyue').value;
            if(parseFloat(cc)>parseFloat(rr))
            {
                top.ui.alert('本次记账金额不能大于未记账金额！');
                return false;
            }
            
            if(parseFloat(cc)>parseFloat(yu))
            {
                top.ui.alert('本次记账金额大于账户余额！');
            }
            
            return true;
        }
        
        function pickRPayment() {
            if(document.getElementById("hdnProjectCode").value=="")
            {
                top.ui.alert("请先选择项目！");
                return ;
            }
            var prjguid=document.getElementById("hdnProjectCode").value;//this.hdnProjectCode.Value
            var sub = document.getElementById("HdnSub").value;
            var title = null;
            var url = null;
            if (sub == "0") {
                top.ui.callback = function (guid) {
                    $('#hdnRPUID').val(guid);
                    $('#btnPlan').click();
                }
                 title = '选择合同支付信息';
                 url = '/Fund/AccountPayOut/SelectRP.aspx?prjguid=' + prjguid + '&type=payout&pp=' + $("#txtPayOut").val();
            }
             else {
                 top.ui.callback = function (guid) {
                     $('#hdnRPUID').val(guid);
                     $('#btnPlan').click();
                 }
                 title = '选择项目费用名称';
                 url = '/Fund/AccountPayOut/SelectSub.aspx?prjguid=' + prjguid + '&type=income&pp=' + $("#txtPayOut").val();
             }
             top.ui.openWin({ title: title, url: url,width:800, height:500});
        }
        
        function registerCancelEvent(){
            var btnCancel = document.getElementById('btnCancel');
            addEvent(btnCancel,'click',function(){
            winclose('ModifyEdit', 'AccountPayout.aspx', false)
            })
        }
       //选择人员
        function selectUser() {
            //选择人员返回值
            top.ui.callback = function (id,name) {
                returnUser(id,name);
            }
            top.ui._DivSelectUser = window;
            var url = '/Common/DivSelectUser.aspx'; //?Method=returnUser
            top.ui.openWin({ title: '选择经手人', url: url});
            
        }
         function returnUser(id, name) {
            document.getElementById('hdnHandler').value = id;
            document.getElementById('txtHandler').value = name;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="入账单编辑" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2">
        <table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    入账单编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtAccCode" Height="15px" CssClass="{required:true, messages:{required:'变更编号必须输入'}}" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox ID="txtPrjName" ContentEditable="false" CssClass="{required:true, messages:{required:'项目必须输入'}}" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hdnProjectCode" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    <asp:Label ID="lblSel" Text="" runat="server"></asp:Label>
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtRPCode" ContentEditable="false" Style="width: 90%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择合同支付" onclick="pickRPayment();" src="/images/icon.bmp" style="float: right;" />
                    </span>
                    <input type="hidden" id="hdnRPUID" runat="server" />

                </td>
                <td class="word">
                    应支付金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtPayMoney" ContentEditable="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    已记账金额
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtPayOut" ContentEditable="false" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    未记账金额
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtJianMoney" ContentEditable="false" runat="server"></asp:TextBox>
                     
                </td>
            </tr>
            <tr>
                <td class="word">本次记账金额</td>
                <td>
                   <asp:TextBox ID="txtInMoney" CssClass=" decimal_input  {required:true, number:true, messages:{required:'入账金额必须输入', number:'入账金额格式错误'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    经手人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtHandler" ContentEditable="false" Style="width: 90%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择经手人" onclick="selectUser();" src="/images/icon.bmp" style="float: right;" />
                    </span>
                    <input type="hidden" id="hdnHandler" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                     <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    入账人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtInPeople" ContentEditable="false" Height="15px" Width="100%" runat="server"></asp:TextBox>
                    <input type="hidden" id="hdnPeopleCode" runat="server" />

                </td>
                <td class="word">
                    入账时间
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="txtInDate" Enabled="false" Width="100%" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSelectFromPlan" title="从请款单中选择" style="display: none">
        <iframe id="ifSelectFromPlan" frameborder="0" width="100%" height="100%" runat="server">
        </iframe>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdnAccountID" runat="server" />
    <asp:HiddenField ID="HdnSub" runat="server" />
    <input id="hdnZHID" type="hidden" runat="server" />

    <input id="hdnyue" type="hidden" runat="server" />

    <asp:Button ID="btnPlan" Width="0px" Text="Button" OnClick="btnPlan_Click" runat="server" />
    </form>
</body>
</html>
