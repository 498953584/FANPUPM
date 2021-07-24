<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectYh.aspx.cs" Inherits="CommonWindow_PopedomSetup_SelectYh" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>选择用户</title>
<script language="javascript" type="text/javascript">
     <!--
    window.name = "win";
       -->   
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
       <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <div>
      <table style="height:100%;width:100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal" >            
             <tr>               
                <td class="td-head" colspan="2">用户选择
                    </td>
            </tr>
            <tr>
                <td valign="top" style="width:35%">  
                   <div id="dvtree" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">               
                    <asp:TreeView ID="TVDept" ShowLines="true" OnSelectedNodeChanged="TVDept_SelectedNodeChanged" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView> 
                    </div>       
                 </td>
                <td valign="top">             
                 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">      <asp:SqlDataSource ID="SqlUserList" SelectCommand="SELECT [V_DLID], [V_DLMM], [V_BGFW], [v_yhdm], [v_xm], [i_xh], [c_sfyx], [i_bmdm], [V_BMMC] FROM [v_UserInfoList] WHERE ([c_sfyx] = @c_sfyx)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="y" Name="c_sfyx" Type="String" /></SelectParameters></asp:SqlDataSource>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                        
                             <asp:GridView ID="GVUserList" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlUserList" Width="100%" OnRowDataBound="GVUserList_RowDataBound" runat="server">
<EmptyDataTemplate>
                                     <table id="Table1" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                         border-collapse: collapse">
                                         <tr class="grid_head">
                                             <th align="center" scope="col" style="width: 40px">
                                                 &nbsp;</th>
                                             <th align="center" scope="col" style="width: 80px">
                                                 编号</th>
                                             <th scope="col">
                                                 名称</th>
                                         </tr>
                                     </table>
                                 </EmptyDataTemplate>
<Columns><asp:TemplateField>
<ItemTemplate>
                                             <asp:CheckBox ID="CBSelectYh" runat="server" />
                                         </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="v_yhdm" HeaderText="编号" SortExpression="v_yhdm" /><asp:BoundField DataField="v_xm" HeaderText="名称" SortExpression="v_xm" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="TVDept" EventName="SelectedNodeChanged" runat="server" /></Triggers></asp:UpdatePanel>
                 </div>
                
                 
      </td></tr>
      	<tr>
			<td  align="center" colspan="2" class="td-submit">
					<asp:Button ID="BtnSave" Text="确 定" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
               
                
            </td>
		</tr>
      </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
