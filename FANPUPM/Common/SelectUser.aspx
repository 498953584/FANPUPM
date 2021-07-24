<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectUser.aspx.cs" Inherits="Common_SelectUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>选择人员</title>
    <style type="text/css">
       

    </style>
    <script type="text/javascript" src="../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
         
        });       
        
        //给父页面设置值
        function setVal()
        {
         var method=getRequestParam('Method');//方法
         var id="";
         var name="";
         var a=document.getElementById('lbSelect');
         var options = a.getElementsByTagName('OPTION');                  
          for(var i = 0; i < options.length; i++){ 
             if(options[i].selected){     
                id=options[i].value;
                name=options[i].text;  
             }            
          }

          window.opener[method](id,name);
          window.close();
        }
        //添加options
      function lbselect(lb1,lb2,type)
      {
         var a=document.getElementById(lb1); 
         var b = document.getElementById(lb2);
         var options = a.getElementsByTagName('OPTION');                  
          for(var i = 0; i < options.length; i++){
                if(options[i].selected){
                 b.appendChild(options[i]);
                 lbselect(lb1,lb2,type);
                }
          }
         
      } 
    </script>

</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <span id="lbmsg"></span>
        <div class="divContent" style="height: 450px; margin-left: 20px; overflow: hidden;">
            <table class="tableContent" cellpadding="5px" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; border: solid 0px red; width: 200px; text-align: left;"
                        rowspan="3">
                        <div style="font-weight: bold; margin-left: 10px; margin-top: 10px; margin-bottom: 10px;">
                            请选择</div>
                        <div id="departmentDiv" style="height: 400px; width: 300px; overflow: auto; border: solid 1px #cdd4df;">
                            <asp:TreeView ID="TvBranch" Width="100%" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="trvwDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                        </div>
                    </td>
                    <td style="border: solid 0px red; height: 30px; text-align: left; vertical-align: bottom;">
                        <div style="margin-left: 10px;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="border: solid 0px red; height: 320px; vertical-align: top;">
                        <table style="height: 320px;" cellpadding="0px" cellspacing="0">
                            <tr>
                                <td style="border: solid 0px red; width: 200px; vertical-align: top; text-align: right;">
                                    <div style="height: 30px; margin-right: 10px;">
                                        <asp:TextBox ID="txtQuery" BorderColor="#CDD4DF" Height="15px" Width="95px" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                                    </div>
                                    <div style="margin-right: 10px;">
                                        <asp:ListBox ID="lbSelect" Style="border: solid 1px #cdd4df;" DataTextField="v_xm" DataValueField="v_yhdm" Width="155px" Height="330px" runat="server"></asp:ListBox></div>
                                </td>
                                <td style="border: solid 0px red; width: 20px; text-align: center; display:none;">
                                    <div style="vertical-align: top; border: solid 0px red; height: 360px;">
                                        
                                    </div>                            
                                    <div style="height: 30px;">
                                        <img src="../images1/4_03.jpg" alt="添加" style="cursor: pointer;" onclick="lbselect('lbSelect','lbUser',1)"
                                            id="btnAdd" />
                                    </div>
                                    <div style="height: 30px;">
                                        <img src="../images1/4_06.jpg" alt="全部" style="cursor: pointer; display: none;" onclick="lbselect('lbSelect','lbUser',2)"
                                            id="btnAddAll" />
                                    </div>
                                    <div style="height: 60px;">
                                    </div>
                                    <div style="height: 30px;">
                                        <img src="../images1/4_08.jpg" alt="删除" style="cursor: pointer;" onclick="lbselect('lbUser','lbSelect',1)"
                                            id="btnDel" />
                                    </div>
                                    <div style="height: 80px; border: solid 0px red;">
                                        <img src="../images1/4_10.jpg" alt="全部" style="cursor: pointer; display: none;" onclick="lbselect('lbUser','lbSelect',2)"
                                            id="btnDelAll" />
                                    </div>                                 
                                </td>
                                <td style="border: solid 0px red; width: 140px; vertical-align: top; text-align: left;">
                                    <div style="height: 30px;">
                                    </div>
                                    <div style="margin-left: 10px;">
                                        <asp:ListBox ID="lbUser" Style="border: solid 1px #cdd4df; display:none;" Width="100px" Height="330px" runat="server"></asp:ListBox></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border: solid 0px red; text-align: right;">
                        <input type="button" id="btnSave" onclick="setVal()" value="确定" runat="server" />

                        <input type="button" id="btnCancel" value="取消" onclick="window.close()" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
