<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DutyList.aspx.cs" Inherits="HR_Personnel_DutyList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>无标题页</title>
    <script language="javascript">
        function ClickRow(DutyID, DeptID, DutyName) {
            parent.document.getElementById('dutyid').value = DutyID;
            parent.document.getElementById('bmdm').value = DeptID;
            parent.document.getElementById('dutyname').value = DutyName;
            parent.document.getElementById('BtnOK').disabled = false;
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table id="table1" border="0" class="table-normal" height="100%" width="100%">
            <tr>
                <td align="center" class="td-title" height="28" style="width: 518px">
                    岗位列表</td>
            </tr>
            <tr>
                <td style="width: 100%" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:GridView ID="GridView1" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlDuty" Width="100%" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="I_DUTYID,i_bmdm" runat="server">
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
</asp:TemplateField><asp:BoundField DataField="DutyName" HeaderText="岗位名称" SortExpression="DutyName" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                        <asp:SqlDataSource ID="SqlDuty" SelectCommand="SELECT I_DUTYID, i_bmdm, DutyName FROM PT_DUTY WHERE i_bmdm = @DeptID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="DeptID" QueryStringField="pid"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
