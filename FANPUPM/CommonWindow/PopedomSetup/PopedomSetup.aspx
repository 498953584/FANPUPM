<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopedomSetup.aspx.cs" Inherits="CommonWindow_PopedomSetup_PopedomSetup" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>管理员信息</title>
<script language="javascript" type="text/javascript">
    <!--
    function ClickRow(bc,uc)
    {
        document.getElementById('frmProject').src = "ProjectList.aspx?bc="+bc+"&uc="+uc;        
    }
    //选择管理员
    function openEdit(bc)
    {   
       var url = 'SelectYh.aspx?bc='+bc;
       return window.showModalDialog(url,window,"dialogHeight:450px;dialogWidth:600px;center:1;help:0;status:0;");		      
    } 
    
    -->    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
        
    <div>
      <table style="height:100%;width:100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">            
             <tr>               
                <td class="td-title">管理员信息
                    </td>
            </tr>
             <tr>               
                <td class="td-search" style="height: 24px">
                按用户名称
                    <asp:TextBox ID="TxtSearch" runat="server"></asp:TextBox>&nbsp;
                    <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" />
                    <asp:Button ID="BtnPopedomSetup" Text="选择管理员" OnClick="BtnPopedomSetup_Click" runat="server" /></td>
            </tr>
            <tr>
                <td valign="top" style="height:40%"> 
                    <asp:GridView ID="GVUserList" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlUserList" Width="100%" OnRowDataBound="GVUserList_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                border-collapse: collapse">
                                <tr class="grid_head">
                                    <th scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" scope="col" style="width: 100px">
                                        用户代码</th>
                                    <th scope="col">
                                        用户姓名</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号"></asp:TemplateField><asp:BoundField DataField="UserCode" HeaderText="用户代码" ReadOnly="true" SortExpression="UserCode" /><asp:BoundField DataField="v_xm" HeaderText="用户姓名" SortExpression="v_xm" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                    <asp:SqlDataSource ID="SqlUserList" SelectCommand="SELECT [BussinessCode], [UserCode], [v_xm], [c_sfyx] FROM [V_CPM_ManagerList] WHERE ([c_sfyx] = @c_sfyx)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="y" Name="c_sfyx" Type="String" /></SelectParameters></asp:SqlDataSource>
                
                </td>
            </tr>
            <tr>
                <td valign="top">
                <iframe id="frmProject" name="frmProject" src="about:blank" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
      </table>
    </div>
    </form>
</body>
</html>
