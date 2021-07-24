<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsOldInfo.aspx.cs" Inherits="SMS_Fram_SmsOldInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
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
            var smalldiv = document.getElementById('divMySearch');
            smalldiv.style.left = maxWidth / 3;
            smalldiv.style.top = maxHeight / 3;
            document.getElementById('divMySearch').style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                    <td style="height:390px;border:2px solid #d1e0f8;width:355px">
                        <asp:DataList ID="dlRecorde" Width="535px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" Height="390px" RepeatColumns="1" CellPadding="5" CellSpacing="0" style="margin-right: 0px;margin-left:0px" runat="server">
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
        <div>
            <table>
                <tr>
                    <td>
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
                        <asp:Button ID="BtnCancel" Text="取 消" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </div>
    </form>
</body>
</html>
