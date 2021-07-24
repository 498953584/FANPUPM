<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelLinkMan.aspx.cs" Inherits="SMS_Fram_SelLinkMan" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        function selClick() 
        {
            if (document.getElementById('divShowLink').style.display != 'none') 
            {
                if (document.getElementById('divShowBase').style.display == 'none') 
                {
                    document.getElementById('divShowBase').style.display = 'block';
                    document.getElementById('divShowLink').style.display="none";
                }
            } 
            else 
            {
                if (document.getElementById('divShowBase') != 'none') 
                {
                    document.getElementById('divShowLink').style.display = "block";
                    document.getElementById('divShowBase').style.display = "none";
                }    
            }
        }
        function rowDbclick(cellname, cellnumber) {
            //alert(cellnumber);
            if (cellnumber!='&nbsp;') {
                var method = getRequestParam('Action'); //方法
                parent[method](cellname, cellnumber);
                divClose(parent);               
            } else {
            alert("手机号码为空,无法发送!!");
            return false;
            }
        }
        function returnUser(id, name) 
        {
            document.getElementById('hdids').value = id;
            document.getElementById('hdnames').value = name;
        }
        function doAction() {
            if (document.getElementById('hdids').value != "" && document.getElementById('hdnames').value != "") 
            {
                var method = getRequestParam('Method');
                parent[method](document.getElementById('hdids'), document.getElementById('hdnames').value);
            }
            divClose(parent);
        }       
    </script>
    <style type="text/css">
    .span_btn
    {
    	display:block;
        border:1px solid #EAF4FD;
        width:130px; 
        background-color:#cadeff;
        text-decoration:none; 
        font-weight:bold;
        font-size:12px;
        color:teal;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="280px" cellpadding="3" cellspacing="0">
            <tr>
                <td>
                    <a href="javascript:void(0)" ><span class="span_btn" onclick="selClick()">从公司部门人员中选择</span></a>
                </td>
                <td>
                    <a href="javascript:void(0)" ><span class="span_btn" onclick="selClick()">从个人通讯录中选择</span></a>
                </td>
            </tr>
        </table>
        <div id="divShowBase" style="height:455px;width:560px;">
            <iframe frameborder="0" id="SelFromBase" src="/Common/DivSelectAllUser.aspx?Method=returnUser&parurl=SMSOne" width="100%" height="100%"></iframe>
        </div>
        <div id="divShowLink" style="height:455px;width:560px;display:none;" title="双击选择!">
            <div style="overflow-y:auto;height:456px;overflow-x:hidden;">
                <asp:DataGrid ID="dgLinks" Width="100%" Height="100%" AllowPaging="false" PageSize="50" AutoGenerateColumns="false" GridLines="Horizontal" DataSourceID="sourcedata1" OnItemDataBound="dgLinks_ItemDataBound" runat="server"><AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle><HeaderStyle Font-Bold="true" ForeColor="White" BackColor="Teal" HorizontalAlign="Center"></HeaderStyle><ItemStyle HorizontalAlign="Center"></ItemStyle><Columns><asp:TemplateColumn HeaderText="序号" HeaderStyle-Width="50px"><ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server"><%# Convert.ToString(Container.ItemIndex + 1) %></asp:Label>
                            </ItemTemplate></asp:TemplateColumn><asp:BoundColumn HeaderText="姓名" DataField="v_xm" ItemStyle-Width="100px"></asp:BoundColumn><asp:BoundColumn HeaderText="手机号码" DataField="v_sj" ItemStyle-Width="100px"></asp:BoundColumn><asp:BoundColumn HeaderText="单位" DataField="v_dw"></asp:BoundColumn></Columns></asp:DataGrid>
                <asp:SqlDataSource ID="sourcedata1" SelectCommand="select * from pt_txl_grtxl" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
            </div>
        </div>
    </div>
    <table width="100%">
        <tr>
            <td style="border: solid 0px red; text-align: center;">
                <asp:Button ID="btnSaveSure" Text="确 定" runat="server" />
                <input  type="button" id="btnCancelSure" value="取 消" onclick="divClose(parent)"/>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdids" runat="server" />
    <asp:HiddenField ID="hdnames" runat="server" />
    </form>
</body>
</html>
