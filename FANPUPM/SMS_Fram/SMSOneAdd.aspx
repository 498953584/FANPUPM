<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSOneAdd.aspx.cs" Inherits="SMS_Fram_SMSOneAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
 
    <style type="text/css">
    .textBox
    {
    	overflow: hidden;
    }
    .btnStyle
    {
    	 background-image:url(images/duanxin_r3_c6.gif) repeat-x;
    	 width:64px;
    	 height:25px;
    }
    
        #third
        {
            height: 440px;
        }
        .scrolly
        {
        	scrollBar-face-width:10px;
        	scrollBar-face-color:#d1e0f8; 
        	scrollBar-hightLight-color:#d1e0f8;  
        	scrollBar-3dLight-color:#d1e0f8; 
    	    scrollBar-darkshadow-color:#d1e0f8;  
    	    scrollBar-shadow-color:#d1e0f8;  
    	    scrollBar-arrow-color:#d1e0f8;  
    	    scrollBar-track-color:lightgray;
    	    scrollBar-track-width:10px;  
    	    scrollBar-base-color:#d1e0f8;
    	    scrollBar-base-width:10px;
        }
    
    </style>
     <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript">
        function openHalf() 
        {
            var hddValue = document.getElementById('hddStyle').value;
             switch (hddValue) {
                 case "0":
                     document.getElementById("headerTab").style.background = "url(images/duanxin_r1_c00y.jpg) repeat-x";
                     var tdnew = document.getElementById("needTd");
                     tdnew.style.background = "url(images/testMyPic.GIF) repeat-y";
                     document.getElementById("sec").style.display = "block";
                     document.getElementById('hddStyle').value = "1";
                     break;
                 case "1":
                     document.getElementById("headerTab").style.background = "url(images/duanxin_r1_c0.jpg) repeat-x";
                     var tdnew = document.getElementById("needTd");
                     tdnew.style.background = "url(images/duanxin_r12_c2.gif) repeat-y";
                     document.getElementById("sec").style.display = "none";
                     document.getElementById('hddStyle').value = "0";
                     break;
             }
             returnBack();
         }
         function returnBack() {
             var wordValue = document.getElementById('hddStyle').value;
             var Method = getRequestParam('Method');
         }
        function selectUser() {
//            document.getElementById("divFramPrj").title = "选择人员";
//            document.getElementById("divFramPrj").style.display.bold;
//            document.getElementById('divFramPrj').target = '_parent';
//            document.getElementById("ifFramPrj").src = "/Common/DivSelectAllUser.aspx?Method=returnUser&type=Sms";
//            $('#divFramPrj').dialog({ width: 580, height: 485, modal: true });
            var url = "/Common/DivSelectAllUser.aspx?type=Sms";
            var userInfo = { url: url, title: '选择人员' };
            top.ui.callback = returnUser;
            top.ui.openWin(userInfo);
        }
        //选择人员返回值
        function returnUser(idAndName) {
            if (document.getElementById('txtPersons').value != "") {
                document.getElementById('txtPersons').value += idAndName;
            } else {
                document.getElementById('txtPersons').value = idAndName;
            }

        }
        //选择通讯录人员返回值
        function returnLink(cellname, cellnumber) {
            document.getElementById('hdnumbers').value = cellnumber+,;
            document.getElementById('hddPeoples').value = name;
            if (document.getElementById('txtPersons').value != "") 
            {
                document.getElementById('txtPersons').value += cellname+,;
            } 
            else 
            {
                document.getElementById('txtPersons').value = cellname+,;
            }
        }
        function getDivPoint() {
            var smalldiv = document.getElementById('newDivSms');
            smalldiv.title = '短信记录查询';
            smalldiv.style.display.bold;
            document.getElementById('newFram').src = "MoreInfoShoe.aspx";
            $('#newDivSms').dialog({ width: 570,
                height: 460,
                modal: true,
                position:'center',
                buttons: { "关闭": function() { $(this).dialog("close"); } }
            });
        }
        function downClose() {
            document.getElementById('idRemark').innerHTML = " ";
            document.getElementById('idRemark').innerText = " ";

        }
        function addOverFlow() {
            var getHeight=document.getElementById('txtWriteText').style.height;
            if (getHeight > 120) {
                document.getElementById('txtWriteText').style.overflow = "auto";
            }
        }
        function showTextAreaStyle() {
            var wordCount = document.getElementById("txtWriteText").value.length;
            //alert(wordCount);
            if (wordCount > 100) {
                document.getElementById('lblClienNumer').innerText = "您的输入已经超出字数限制"+(wordCount-100)+"个,消息将无法发送";
                return false;
            }
            else {
                //alert(document.getElementById('lblClienNumer').innerText);
                document.getElementById('lblClienNumer').innerText = "您最多还可以输入"+(100-wordCount)+"个字符";
                //alert(document.getElementById('lblClienNumer').innerText);
                return true;
            }
        }
        function checkText() {
            //alert(document.getElementById("txtPersons").value);
            document.getElementById("txtPersons").value=document.getElementById("txtPersons").value.replace(/，/g, ,);
            //alert(document.getElementById("txtPersons").value);
            
            var newstr = document.getElementById("txtPersons").value.replace(document.getElementById('hddPeoples').value, "");
            //alert(newstr)
            if (newstr == document.getElementById('txtPersons').value && document.getElementById('hddPeoples').value != "") {
                document.getElementById('hddPeoples').value = document.getElementById('txtPersons').value;
            }
             else 
            {
                if (newstr != "") 
                {
                    var textArr = newstr.split("");
                    //alert(textArr);
                    if (textArr[textArr.length - 1] != ,) {
                        //newstr += ,;
                        document.getElementById("txtPersons").value = newstr + "," + document.getElementById('hddPeoples').value;
                    }
                    //alert(document.getElementById("txtPersons").value);
                    var myArr = newstr.split(,);
                    //alert(myArr);
                    for (var i = 0; i < myArr.length - 1; i++) {
                        //alert(parseInt(myArr[i]));
                        if (myArr[i].length != 11 || parseInt(myArr[i]) == NaN || parseInt(myArr[i]).toString().length != 11) { alert("请输入正确的手机号码!"); return false; }
                    }
                } 
            }
        }
        function fuckManager() {
            var sCount;
            var RemName="";
            document.getElementById("txtPersons").value = document.getElementById("txtPersons").value.replace(/，/g, ,);
            var getPhone = document.getElementById("txtPersons").value.split(,);
            var tempData = document.getElementById("txtPersons").value.split("");
            if (tempData[tempData.length - 1] == ,) {
                //document.getElementById("txtPersons").value = document.getElementById("txtPersons").value.substr(0,tempData.length-1);
                sCount = getPhone.length - 1;
            } else {
            sCount = getPhone.length;
            document.getElementById("txtPersons").value = document.getElementById("txtPersons").value + ",";
            }
            //alert(sCount);
            for (var i = 0; i < sCount; i++) 
            {
                //alert(getPhone[i]);
                if (getPhone[i].indexOf(":") != -1) 
                {
                    var relPhone = getPhone[i].split(":");
                    if (relPhone[1].length != 11) 
                    {
                        //alert("请输入正确的手机号!");
                        RemName += relPhone[0] + ",";
                        //alert(relPhone[0]);
                        document.getElementById("txtPersons").value = document.getElementById("txtPersons").value.replace((getPhone[i]+,),"");                        
                    }
                }
                else 
                {
                    if (getPhone[i].length != 11) 
                    {
                        alert("请输入正确的手机号!");
                        return false;
                    }
                }
            }
            //alert(RemName);
            if (RemName != "") 
            {
                alert("姓名为 " + RemName + " 的手机号码格式不正确!已从接收人栏中删除.(再次点击\"发送\"可发送信息至保留下的号码里)");
                return false;
            }
        }
        function checkOper() {
            if (document.getElementById("txtPersons").value != "") {
                document.getElementById("txtPersons").value = document.getElementById("txtPersons").value.replace(/，/g, ,);
                var tempData = document.getElementById("txtPersons").value.split("");
                if (tempData[tempData.length - 1] != ,) {
                    document.getElementById("txtPersons").value = document.getElementById("txtPersons").value + ",";
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="float:left;padding-left:120px;" id="first">
             <table width="380px" style=" background: url(images/duanxin_r1_c0.jpg) repeat-x;" id="headerTab" runat="server"><tr runat="server"><td style=" padding-left:15px;text-align:left;font-weight:bold;padding-bottom:10px;width:100%;" colspan="3" runat="server">
                        <img id="headerimg" alt="" src="images/new.gif"/>手机短信
                    </td></tr><tr runat="server"><td style=" padding-left:15px;text-align:left;vertical-align:top;" runat="server">
                        <span><img alt="" src="images/new1.gif"/>&nbsp;&nbsp;&nbsp;<span style="vertical-align:middle;">接收人:</span></span>
                    </td><td style="vertical-align:middle;" runat="server">
                        <asp:TextBox ID="txtPersons" Width="200px" Height="15px" ToolTip="手动输入11位的手机号码,多个号码请用逗号隔开" runat="server"></asp:TextBox>
                    </td><td style="padding-right:15px;vertical-align:middle;" runat="server">
                        <input id="btnLoad" type="button" value="载 入" style=" background:url(images/duanxin_r3_c6.gif) repeat-x;width:64px;height:25px;" onclick="checkOper();selectUser()"/>
                        <asp:HiddenField ID="hddPeoples" runat="server" />
                    </td></tr></table>
            <table width="373px" cellpadding="1" cellspacing="0" style="margin-top:0px;">
                <tr>
                    <td style="height:199px;border:1px solid #d1e0f8;">
                        <div style="background:url(/images/duanxin_r7_c1.gif) repeat-x;width:100%; height:20px; border-left-width:1px;border-right:1px solid #d1e0f8" id="idRemark" runat="server">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        </div>
                        <div style="overflow:auto;height:206px;
                        	width:370px; margin-left:7px; line-height:20px; vertical-align:top;" class="scrolly">
                            <asp:Literal ID="LtlCont" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="background-color:#D1E0F8;height:25px;">
                        <table width="100%">
                            <tr>
                                <td style="width:20%;">
                                    <img alt="" style=" vertical-align:middle;" src="images/duanxin_r9_c1.gif"/>&nbsp;&nbsp;<span style="">发送内容</span>
                                </td>
                                <td align="right" style="width:80%;padding-right:15px;">
                                    <span onclick="openHalf()" onmouseover="javascript:this.style.cursor='hand';" style="height:25px"><img style=" vertical-align:middle;"  alt="" src="images/duanxin_r8_c5.gif"/>&nbsp;&nbsp;短信记录</span>
                                    <input type="hidden" id="hddStyle" value="0" runat="server" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:100%">                            
                                    <asp:TextBox ID="txtWriteText" Height="110px" BorderWidth="0px" Width="373px" TextMode="MultiLine" CssClass="textBox" Rows="8" runat="server"></asp:TextBox>                           
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="380px">
                <tr>
                    <td style="background:url(images/duanxin_r12_c1.gif) repeat-y;height:32px;width:8px; border-bottom-width:0px;">                    
                    </td>
                    <td style="background-color:#d1e0f8;height:32px;width:240px">
                       <span id="lblClienNumer">您最多还可以输入100个字符</span> 
                    </td>
                    <td style="background-color:#d1e0f8;">                        
                        <asp:Button ID="BtnAgin" Text="发 送" style="background:url(images/duanxin_r3_c6.gif) repeat-x;width:63px;height:25px;" OnClick="BtnAgin_Click" runat="server" />                        
                    </td>
                    <td style="background-color:#d1e0f8;">
                        <input id="btnCancel" type="button" value="关 闭" style="background:url(images/duanxin_r3_c6.gif) repeat-x;width:63px;height:25px;" onclick="javascript:winclose('SMSOneAdd', 'Purchase.aspx', false);"/>
                    </td>
                    <td style="background:url(images/duanxin_r12_c2.gif) repeat-y;height:32px;width:8px; border:0px;" id="needTd" runat="server">
                    </td>
                </tr>
            </table>
        </div>
         <div style="float:left;width:200px;display:none;" id="sec" runat="server">
             <div>
                <table width="200px" style="background:url(images/duanxin_r1_c01y.jpg) repeat-x;">
                    <tr>
                        <td style="padding-left:15px;vertical-align:bottom;text-align:left;width:110px;height:70px">
                            短信记录:
                        </td>     
                        <td style="padding-right:10px;vertical-align:bottom;text-align:left;">
                            <a href="#" style=" text-decoration:none;"><span onclick="getDivPoint()"><img alt="更多记录查询" title="更多记录查询" src="images/duanxin_r4_c15.gif" style="vertical-align:middle;border:0px;"/>&nbsp;&nbsp;更多</span></a>
                        </td>           
                    </tr>
                    <tr>
                        <td style="height:3px" colspan="2">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="height:357px;border:2px solid #d1e0f8;width:195px;">
                            <div style="overflow-y:auto;overflow-x:hidden;height:357px;width:195px; vertical-align:top;" class="scrolly">
                                <asp:DataList ID="DataList1" Width="195px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" RepeatColumns="1" RepeatLayout="Table" RepeatDirection="Vertical" CellPadding="2" CellSpacing="0" style="margin-right: 0px;margin-left:0px; line-height:20px;" runat="server">
<ItemTemplate>
                                        <asp:Label ID="Label1" ForeColor="Blue" runat="server"><%# Convert.ToString(Eval("msg")) %></asp:Label><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server"><%# Convert.ToString(Eval("msg")) %></asp:Label>
                                    </ItemTemplate>
</asp:DataList>
                            </div>
                        </td>
                    </tr>
                </table>
                <table style="height:32px;">
                    <tr>
                        <td style="background-color:#d1e0f8;width:91px;">
                            <JWControl:DateBox ID="txtDateDown" Height="15px" Width="50px" runat="server"></JWControl:DateBox>&nbsp;<a href="javascript:void(0)" id="CanSearch" OnServerClick="CanSearch_Click" runat="server" />
<img alt="点击搜索!" src="images/duanxin_r13_c9.gif" style="vertical-align:middle;border:0px;" title="点击搜索!!"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:10px">
                            <a href="#" id="FirPage" OnServerClick="FirPage_Click" runat="server" />
<img alt="" title="第一页" src="images/duanxin_r14_c11.gif" style="border:0px;"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:10px">
                            <a href="#" id="PrePage" OnServerClick="PrePage_Click" runat="server" />
<img alt="" title="上一页" src="images/duanxin_r14_c13.gif" style="border:0px;"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:50px;text-align:center;">
                            第<asp:Label ID="CurPageIndex" runat="server"></asp:Label>/<asp:Label ID="lblPageCounts" runat="server">1</asp:Label>页
                        </td>
                        <td style="background-color:#d1e0f8;width:10px">
                            <a href="#" id="NextPage" OnServerClick="NextPage_Click" runat="server" />
<img alt="" title="下一页" src="images/duanxin_r14_c17.gif" style=" border:0px;"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:10px">
                            <a href="#" id="LastPage" OnServerClick="LastPage_Click" runat="server" />
<img alt="" title="最后一页" src="images/duanxin_r14_c19.gif" style="border:0px"/>
                        </td>
                        <td style="background:url(images/duanxin_r12_c2.gif) repeat-x;width:8px;height:32px">                    
                        </td>
                    </tr>
                </table>
            </div>
             <div style="background-color:#d1e0f8;width:300px;display:none;border:1px solid teal;position:absolute;" id="divMySearch" >
                <table cellpadding="5" cellspacing="0" >
                    <tr>
                        <td style="text-align:right;width:120px;">
                        选择发送日期：
                        </td>
                        <td style="text-align:left;">
                            <JWControl:DateBox ID="txtDateSend" Height="15px" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;width:120px;">
                        选择短信接收人：
                        </td>
                        <td style="text-align:left;">
                            <span id="span1" class="spanSelect" style="width: 122px;background-color:White;">
                                <input type="text" style="width: 98px; height: 15px; border: none; line-height: 16px;
                                    margin: 1px 2px" id="txtPeoples" runat="server" />

                                <img src="/images1/iconSelect.gif" alt="请双击此处选择" id="img1" onclick="selectUser();" />
                            </span>
                            <input id="hdnPeople" type="hidden" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;width:120px;">
                        填写短信关键字：
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtKeyWord" ToolTip="所有文本将被视为一个关键字" Height="15px" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;">
                            <asp:Button ID="BtnSure" Text="确 定" runat="server" />
                        </td>
                        <td style="text-align:center;">
                            <asp:Button ID="Button1" Text="取 消" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divFramPrj" title="" style="display: none">
                <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
            </div>
            <div id="newDivSms" title="" style="display: none">
                <iframe id="newFram" frameborder="0" width="100%" height="100%" src=""></iframe>
            </div>            
         </div>
    </div>
    <asp:HiddenField ID="hddPersonName" runat="server" />
    <asp:HiddenField ID="hddMinId" runat="server" />
    <asp:HiddenField ID="hddMaxId" runat="server" />
    <asp:HiddenField ID="hdIds" runat="server" />
    <asp:HiddenField ID="hdnumbers" runat="server" />
    </form>    
</body>
</html>
