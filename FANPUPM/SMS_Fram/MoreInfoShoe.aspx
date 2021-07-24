<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoreInfoShoe.aspx.cs" Inherits="SMS_Fram_MoreInfoShoe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

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
            $('#divFramPrj').dialog({ width: 500, height: 400, modal: true })
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('hdnPeople').value = id;
            document.getElementById('txtPeoples').value = name;
        }
//        function getDivPoint() {
//            var maxWidth = document.body.offsetWidth;
//            var maxHeight = document.body.offsetHeight;
//            var smalldiv = document.getElementById('divMySearch');
//            smalldiv.style.left = maxWidth / 3;
//            smalldiv.style.top = maxHeight / 3;
//            document.getElementById('divMySearch').style.display = "block";
//        }
    </script>
    <style type="text/css">
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="532px" style="height:360px;">
            <tr>
                <td style="text-align:right;background-color:#d1e0f8;height:20px;width:80px">
                    发送时间&nbsp;
                </td>
                <td style="text-align:left;background-color:#d1e0f8;">
                    <JWControl:DateBox ID="txtDateSend" Height="15px" Width="75px" runat="server"></JWControl:DateBox>
                </td>
                <td style="text-align:right;background-color:#d1e0f8;width:80px">
                    接收号码&nbsp;
                </td>
                <td style="text-align:left;background-color:#d1e0f8;">
                    <span id="span1" class="spanSelect" style="width: 75px;background-color:White;">
                            <input type="text" style="width: 60px; height: 15px; border: none; line-height: 16px;
                                margin: 1px 2px" id="txtPeoples" runat="server" />

                            
                    </span>
                </td>
                <td style="text-align:right;background-color:#d1e0f8;width:80px">
                    内容关键字&nbsp;
                </td>
                <td style="text-align:left;background-color:#d1e0f8;">
                    <asp:TextBox ID="txtKeyWord" Height="15px" Width="80px" ToolTip="所有文本将被视为一个关键字" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:center;background-color:#d1e0f8;border-right:1px solid #d1e0f8;">
                    <input id="BtnSearch" type="button" value="开始查找" style="width:80px;" OnServerClick="BtnSearch_Click" runat="server" />

                </td>
            </tr>
            <tr>
                <td style="height:320px;border:1px solid #d1e0f8;" colspan="7">
                    <div style="overflow-y:auto;overflow-x:hidden;height:320px;width:532px;" class="scrolly">
                        <asp:DataList ID="dlRecorde" Width="100%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="1" CellSpacing="0" style="margin-right: 0px;margin-left:0px; line-height:20px;" runat="server">
<ItemTemplate>
                                    <asp:Label ID="Label1" ForeColor="Blue" runat="server"><%# Convert.ToString(Eval("msg")) %></asp:Label><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server"><%# Convert.ToString(Eval("msg")) %></asp:Label>
                                </ItemTemplate>
</asp:DataList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="7" style="border:1px solid #d1e0f8;height:15px;">
                  <table>
                    <tr>
                        <td style="background-color:#d1e0f8;width:340px;">
                        
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#" id="FirPage" OnServerClick="FirPage_Click" runat="server" />
<img alt="" title="第一页" src="images/duanxin_r14_c11.gif" style="border:0px;"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#" id="PrePage" OnServerClick="PrePage_Click" runat="server" />
<img alt="" title="上一页" src="images/duanxin_r14_c13.gif" style="border:0px;"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:112px;text-align:center;">
                            第&nbsp;<asp:Label ID="CurPageIndex" Height="15px" Width="20px" runat="server"></asp:Label>&nbsp;页/<asp:Label ID="lblPageCounts" runat="server">55555</asp:Label>页
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#" id="NextPage" OnServerClick="NextPage_Click" runat="server" />
<img alt="" title="下一页" src="images/duanxin_r14_c17.gif" style=" border:0px;"/>
                        </td>
                        <td style="background-color:#d1e0f8;width:20px">
                            <a href="#" id="LastPage" OnServerClick="LastPage_Click" runat="server" />
<img alt="" title="最后一页" src="images/duanxin_r14_c19.gif" style="border:0px"/>
                        </td>
                    </tr>
                  </table>  
                </td>
            </tr>
        </table>
    </div>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdnPeople" runat="server" />
    </form>
</body>
</html>
