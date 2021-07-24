<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DutyList2.aspx.cs" Inherits="DutyList2" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>权限设置</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table id="table1" border="0" class="table-normal" height="100%" width="100%">
            <tr>
                <td align="center" class="td-title" height="28" style="width: 518px">
                    已选岗位列表</td>
            </tr>
            <tr>
                <td style="width: 100%" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:GridView ID="GridView1" AutoGenerateColumns="false" CssClass="grid" Width="100%" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="i_dutyid,i_bmdm" runat="server">
<EmptyDataTemplate>
                                <table id="SqlDuty" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse;">
                                    <tr class="grid_head">
                                        <th scope="col" style="width: 40px">
                                            序号</th>
                                       
                                        <th scope="col">
                                            岗位名称</th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <asp:CheckBox ID="CBoxObj" runat="server" />
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="RoletypeName" HeaderText="岗位名称" SortExpression="RoletypeName" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                        
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <input id="HidDutyCodeS" type="hidden" runat="server" />

        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
        
    </form>
</body>
</html>
