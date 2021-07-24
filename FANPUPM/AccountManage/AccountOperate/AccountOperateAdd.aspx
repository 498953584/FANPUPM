<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountOperateAdd.aspx.cs" Inherits="AccountManage_AccountOperate_AccountOperateAdd" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金申请</title>
    <style type="text/css">
        #txtContractMoney
        {
            width: 97%;
        }
        .style1
        {
            width: 150px;
        }
        .style2
        {
            width: 237px;
        }
        .style3
        {
            width: 185px;
        }
        </style>
  <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../../Script/jw.js"></script>

    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            //设置项目的宽度
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }
        
            //Val.validate('form1');
            setAuditState();
        });
        //选择项目
        function openProjPicker() {
            document.getElementById("divFram").title = "选择项目";
            document.getElementById("ifFram").src = "/Common/DivSelectMyProject.aspx?Method=returnPrj";
            selectnEvent('divFram');
        }
        //选择项目返回值
        function returnPrj(id, name,acname, acouid) {
            document.getElementById('hdfproject').value = id;
            document.getElementById('txtProject').value = name;
            document.getElementById('txtaccount').value = acname;
            document.getElementById('hdfaccount').value = acouid;
        }

        //选择人员
        function selectUser() {
            var url = "/Common/DivSelectAllUser.aspx?Method=returnUser";
            document.getElementById("divUserCode").title = "选择人员";
            document.getElementById("ifUserCode").src = url;
            selectnEvent('divUserCode');
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('hdfusercode').value = id;
            document.getElementById('txtUsercode').value = name;
          
            
        }
        function selectContr() {
            document.getElementById("divFram").title = "选择合同";
             var addoper='addoper';
             document.getElementById("ifFram").src = "/Common/DivSelectContrtAccon.aspx?Method=returnContr&typevalue=" + addoper + "";
            selectnEvent('divFram');
        }

        //选择合同返回值
        function returnContr(id, name,acname, acouid) {
            document.getElementById('hdfcontrcn').value = id;
            document.getElementById('txtContr').value = name;
            document.getElementById('txtaccount').value = acname;
            document.getElementById('hdfaccount').value = acouid;
          
        }

        //表单验证
        function valForm() {
         
            if (document.getElementById('DateBox1').value == "") {
                alert("系统提示:\n\n请填写时间！");
                return false;
            }
          
            if (document.getElementById('hdfproject').value == "" && document.getElementById('hdfcontrcn').value == "") {
                alert("系统提示:\n\n请选择项目或合同");
                return false;
            }

            var str = document.getElementById('txtContractMoney').value;
            var c, i, checkresult, length;
           
             str = Trim(str);
                if (str == "") {
                    alert('金额不能为空');
                    return false;
                }
                length = str.length;
                checkresult = false;
                for (i = 0; i < length; i++) {
                    c = str.substr(i, 1);
                    if (i == 0 && c == '.') {
                        checkresult = false;
                        break;
                    }
                    if (i == length - 1 && c == '.') {
                        checkresult = false;
                        break;
                    }
                    if (((c >= '0') && (c <= '9')) || (c == '.')) {
                        checkresult = true;
                    }
                    else {
                        checkresult = false;
                        break;
                    }
                }
                if (!checkresult) {
                    alert('金额格式不对！');
                    return false;
                }
                else {
                    return true;
                }
          
   
        }
//        function checkInterest() {
//            var parn = "^\\d+(\\.\\d+)?$";
//            var str = document.getElementById("TextBox1").value;
//           var judge= parn.test(str)
//           if (judge) {
//               alert('vdgv');

//           }
//           else {
//               alert('分为各位'); 
//           }
        //        }
 function CheckUserName(){
      var xmlhttp;
        try{
          xmlhttp = new ActiveXObject('Msxml2.XMLHTTP');
      }
     catch(e){
           try{
               xmlhttp = new ActiveXObject('Microsoft.XMLHTTP');
          }
          catch(e){
               try{
                   xmlhttp = new XMLHttpRequest();
               }
               catch(e)
               {}
           }
       }
        
       xmlhttp.onreadystatechange=function(){
            if(xmlhttp.readyState == 4){
               if(xmlhttp.status == 200){
                   var showMes = document.getElementById("txtaccount");


                   showMes.innerHTML = xmlhttp.responseText.toString();
                 
             }
          }
          
       }
       xmlhttp.open("get","ReturnAccount.ashx?Name=" + document.getElementById("hdfcontrcn").value + "&rd=" + Math.random());
       xmlhttp.send(null);
   }

        function txtProject_onclick() {

        }
        
//        function $(e) {
//            return document.getElementById ? document.getElementById(e) : null;
//        }
//        var xmlHttp;
//        function createXMLHttpRequest() {
//            if (window.ActiveXObject) {
//                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
//            }
//            else if (window.XMLHttpRequest) {
//                xmlHttp = new XMLHttpRequest();
//            }
//        }
//        //处理方法
//        function CheckUserName() {
//            alert('fef');
//            createXMLHttpRequest();
//            var url = "ReturnAccount.ashx?Name=" + document.getElementById("hdfcontrcn").value + "&rd=" + Math.random(); ;
//            xmlHttp.open("GET", url, true);
//            alert(xmlHttp.responseText);
//            xmlHttp.onreadystatechange = ShowResult;
//            xmlHttp.send(null);
//           
//        }
//        //回调方法
//        function ShowResult() {
//            if (xmlHttp.readyState == 4) {
//                if (xmlHttp.status == 200) {
//                    alert(xmlHttp.responseText);
//                    document.getElementById("txtaccount").value = xmlHttp.responseText;
//                }
//            }
//        }
//      
      

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="入账操作" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2"><asp:HiddenField ID="hdfproject" runat="server" /><asp:HiddenField ID="hdfcontrcn" runat="server" />
        <table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
            <td class="word">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    入账类型 </td>
                <td style="padding-right: 1px;" class="style3">
                    <span id="spanIncomeContract" class="spanSelect" style="width: 100%;">
                        <asp:DropDownList ID="DropDownList1" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server"><asp:ListItem Text="启动资金" Value="0" /><asp:ListItem Text="合同款" Value="1 " /><asp:ListItem Text="拆借" Value="2" /><asp:ListItem Text="其它" Value="3" /></asp:DropDownList>
&nbsp;
                    </span>
                </td>
                <td class="word">
                   凭证编号
                </td>
              <td class="style2">
                    <asp:TextBox ID="txtContractCode" Height="15px" ReadOnly="true" Width="100%" CssClass="{required:true, messages:{required:'凭证编号必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
           
           <tr>
            <td class="word" id="pr1" runat="server">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    项目
                </td>
                <td class="style3" id="pr2" runat="server">
                    <span id="spanPrj" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" readonly="readonly" id="txtProject" class="{required:true, messages:{required:'项目必须输入'}}" runat="server" />

                        <img src="/images1/iconSelect.gif" alt="选择项目" id="imgPrj" onclick="openProjPicker();" />
                    </span>
                    
                </td>
                 <td class="word" id="pr3" runat="server">
                合同
                </td>
                <td class="style2" style="padding-right: 3px;" id="pr4" runat="server">
                    <span id="span2" class="spanSelect" style="width: 100%;">
                  
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" readonly="readonly" id="txtContr" class="{required:true, messages:{required:'合同必须输入'}}" runat="server" />

                        <img src="/images1/iconSelect.gif" alt="选择合同" id="img2" onclick="selectContr();" />
                    </span>
                      
                </td>
                 <td  class="word">
                  入账账号 </td>
               <td class="txt" >
                   <input type="text" id="txtaccount" height="15px" readonly="true" width="100%" cssclass="{required:true, messages:{required:'入账账号必须输入'}}" runat="server" />

                    <asp:HiddenField ID="hdfaccount" runat="server" />
                </td></tr>
           
           <tr>
                <td class="word">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;入账金额
                </td>
                <td class="style3">
                <input id="txtContractMoney" style="width:100%" cssclass="{required:true,number:true, messages:{required:'入账金额必须输入',number:'入账金额格式错误'}}" height="15px" runat="server" />

                </td>
                <td class="word">
                    申请时间</td>
                <td class="style2">
                    <JWControl:DateBox ID="DateBox1" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'申请时间必须输入'}}" runat="server"></JWControl:DateBox>
                </td>
            </tr>
        
             <tr>
              <td class="word">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请人员</td>
                <td class="style3">  
               <span id="span1" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" readonly="readonly" id="txtUsercode" runat="server" />

                        
                    </span>
                    <asp:HiddenField ID="hdfusercode" runat="server" />
                </td>
              
              
            </tr>
                <tr>
                <td class="noteTd">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;备注</td>
                
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
              </table>
    </div>
    <div id="divFooter2" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm();" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AccountOperateAdd','AccountOperate.aspx',false)" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divUserCode" title="选择申请人员" style="display: none">
        <iframe id="ifUserCode" frameborder="0" width="100%" height="100%" >
        </iframe>
    </div>
    <asp:HiddenField ID="hfldContractId" runat="server" />
    </form>
</body>
</html>
