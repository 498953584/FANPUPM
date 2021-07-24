<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsFace.aspx.cs" Inherits="SMS_Fram_SmsFace" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <style type="text/css">
    .textBox
    {
    	overflow: hidden;
    }
    .btnStyle
    {
    	 background:url(images/duanxin_r3_c6.gif) repeat-x;
    	 width:64px;
    	 height:25px;
    }
    
        #third
        {
            height: 440px;
        }
    
    </style>
     <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
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
        window.onload = function() { //alert(document.getElementById('hddStyle').value) ;
            
        }
        function openHalf() 
        {
            var hddValue = document.getElementById('hddStyle').value;
             //alert(hddValue);
             switch (hddValue) {
                 case "0":
                     document.getElementById("headerTab").style.background = "url(images/duanxin_r1_c00y.jpg) repeat-x";
                     var tdnew = document.getElementById("needTd");
                     tdnew.style.background = "url(images/testMyPic.GIF) repeat-y";
                     document.getElementById("sec").style.display = "block";
                     //alert(document.getElementById('sec').style.display);
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
             //alert("document.getElementById('hddStyle').value = " + document.getElementById('hddStyle').value);
        }
        function selectUser() {
            document.getElementById("divFramPrj").title = "选择人员";
            document.getElementById("divFramPrj").style.display.bold;
            document.getElementById("ifFramPrj").src = "/Common/DivSelectUser.aspx?Method=returnUser";
            //selectnEvent('divFramPrj');
            $('#divFramPrj').dialog({ width: 600, height: 485, modal: true });
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('hdnPeople').value = id;
            document.getElementById('txtPeoples').value = name;
        }
        function getDivPoint() {
            var maxWidth = document.body.offsetWidth;
            var maxHeight = document.body.offsetHeight;
            var smalldiv = document.getElementById('newDivSms');
            smalldiv.style.left = maxWidth / 3;
            smalldiv.style.top = maxHeight / 3;
            smalldiv.title = '短信记录管理';
            smalldiv.style.display.bold;
            document.getElementById('newFram').src = "MoreInfoShoe.aspx";
            $('#newDivSms').dialog({ width: 600, height: 485, modal: true });
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
            if (wordCount > 100) {
                alert('对不起,最多输入100字');
            }
            else {
                document.getElementById('lblClienNumer').value = "您最多还可以输入" + 100 - wordCount + "个字符";    
            }
        }
        function checkText() {
            document.getElementById("txtPersons").value.replace(/，/g, ,);
            var textArr = document.getElementById("txtPersons").value.substring("");
            if (textArr[textArr.length - 1] == ,) {
                textArr[textArr.length - 1].replace(',',"");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panel1" ChildrenAsTriggers="true" UpdateMode="Conditional" runat="server">
<ContentTemplate>
            <div style="float:left;" id="first">
             <table width="540px" style="background: url(images/duanxin_r1_c0.jpg) repeat-x;" id="headerTab">
                <tr>
                    <td style=" padding-left:15px;text-align:left;font-weight:bold;padding-bottom:10px;width:100%;" colspan="3" >
                        <img id="headerimg" alt="" src="images/new.gif"/>手机短信
                    </td>
                </tr>
                <tr>
                    <td style=" padding-left:15px;text-align:left;vertical-align:top;">
                        <span><img alt="" src="images/new1.gif"/>&nbsp;&nbsp;&nbsp;<span style="vertical-align:middle;">选择接收人:</span></span>
                    </td>
                    <td style="vertical-align:middle;">
                        <asp:TextBox ID="txtPersons" Width="320px" Height="15px" ToolTip="手动输入11位的手机号码,多个号码请用逗号隔开" runat="server"></asp:TextBox>
                    </td>
                    <td style="padding-right:15px;vertical-align:middle;">
                        <input id="btnLoad" type="button" value="载 入" style=" background:url(images/duanxin_r3_c6.gif) repeat-x;width:64px;height:25px;" onclick="selectUser()"/>
                        <asp:HiddenField ID="hddPeoples" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="534px">
                <tr>
                    <td style="background:url(images/duanxin_r7_c1.gif) repeat-x;width:534px; border-left-width:1px;border-right:1px solid #d1e0f8" id="idRemark" runat="server">
                        &nbsp;          
                    </td>
                </tr>
                <tr>
                    <td style="height:200px;border:1px solid #d1e0f8;">
                        <asp:DataList ID="dlRecorde" Width="534px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" Height="200px" RepeatColumns="1" CellPadding="5" CellSpacing="0" style="margin-right: 0px;margin-left:0px" runat="server">
<ItemTemplate>
                                <asp:Label ID="Label3" runat="server"><%# Convert.ToString(Eval("")) %><%# Convert.ToString(Eval("")) %></asp:Label><br />
                                <asp:Label ID="Label4" runat="server"><%# Convert.ToString(Eval("")) %></asp:Label>
                            </ItemTemplate>
</asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color:#D1E0F8;height:25px;">
                        <table width="534px">
                            <tr>
                                <td style="width:20%">
                                    <img alt="" style=" vertical-align:middle;" src="images/duanxin_r9_c1.gif"/>&nbsp;&nbsp;<span style="">发送内容</span>
                                </td>
                                <td align="right" style="width:80%;padding-right:15px;">
                                    <span onclick="openHalf()" onmouseover="javascript:this.style.cursor='hand';" style="height:25px"><img style=" vertical-align:middle;"  alt="" src="images/duanxin_r8_c5.gif"/>&nbsp;&nbsp;短信记录</span>
                                    <input type="hidden" id="hddStyle" value="0" runat="server" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtWriteText" Height="121px" BorderWidth="0px" Width="534px" TextMode="MultiLine" CssClass="textBox" Wrap="true" MaxLength="100" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="540px">
                <tr>
                    <td style="background:url(images/duanxin_r12_c1.gif) repeat-y;height:32px;width:8px; border-bottom-width:0px;">                    
                    </td>
                    <td style="background-color:#d1e0f8;height:32px;width:400px">
                       <label id="lblClienNumer">你还可以输入100个字符</label> 
                    </td>
                    <td style="background-color:#d1e0f8;">
                        <asp:Button ID="BtnSave" Text="发 送" style="background:url(images/duanxin_r3_c6.gif) repeat-x;width:64px;height:25px;" OnClick="BtnSave_Click" runat="server" />
                    </td>
                    <td style="background-color:#d1e0f8;">
                        <input id="btnCancel" type="button" value="关 闭" style="background:url(images/duanxin_r3_c6.gif) repeat-x;width:63px;height:25px;" onclick="javascript:window.close();"/>
                    </td>
                    <td style="background:url(images/duanxin_r12_c2.gif) repeat-y;height:32px;width:8px; border:0px;" id="needTd">
                    </td>
                </tr>
            </table>
         </div>
        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="BtnSave_Click" runat="server" /></Triggers></asp:UpdatePanel>         
         <div style="float:left;width:360px;display:none;" id="sec" runat="server">
             <div>
                <table width="360px" style="background:url(images/duanxin_r1_c01y.jpg) repeat-x;">
                    <tr>
                        <td style="padding-left:15px;vertical-align:bottom;text-align:left;width:250px;height:70px">
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
                        <td style="height:357px;border:2px solid #d1e0f8;width:355px">
                            <asp:DataList ID="DataList1" Width="535px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" Height="390px" RepeatColumns="1" CellPadding="5" CellSpacing="0" style="margin-right: 0px;margin-left:0px" runat="server">
<ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"><%# Convert.ToString(Eval("")) %></asp:Label><br />
                                    <asp:Label ID="Label2" runat="server"><%# Convert.ToString(Eval("")) %></asp:Label>
                                </ItemTemplate>
</asp:DataList>
                        </td>
                    </tr>
                </table>
                <table style="height:32px;">
                    <tr>
                        <td style="background-color:#d1e0f8;width:171px;">
                            <img alt="" src="images/duanxin_r13_c9.gif" style="text-align:center;"/>&nbsp;&nbsp;查找&nbsp;<JWControl:DateBox ID="txtDateDown" Height="15px" Width="60px" runat="server"></JWControl:DateBox>
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#"><img alt="" title="第一页" src="images/duanxin_r14_c11.gif" style="border:0px;"/></a>
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#"><img alt="" title="上一页" src="images/duanxin_r14_c13.gif" style="border:0px;"/></a>
                        </td>
                        <td style="background-color:#d1e0f8;">
                            第&nbsp;<asp:TextBox ID="CurPageIndex" Height="15px" Width="20px" runat="server"></asp:TextBox>&nbsp;页/<asp:Label ID="lblPageCounts" runat="server">120</asp:Label>页
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#"><img alt="" title="下一页" src="images/duanxin_r14_c17.gif" style=" border:0px;"/></a>
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#"><img alt="" title="最后一页" src="images/duanxin_r14_c19.gif" style="border:0px"/></a>
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
    </form>
</body>
</html>
