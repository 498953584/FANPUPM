<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReclaimMail.aspx.cs" Inherits="oa_MailAdmin_ReclaimMail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title>

    <script language="javascript">
    function ClickRow(KeyField,sjid)
    {
        //alert(sjid);
        document.getElementById('hdnKeyField').value = KeyField;
        document.getElementById('HdnSJID').value = sjid;
        document.getElementById('btnDel').disabled = false;
    }
    </script>
   <script type="text/javascript" src="../../Script/jquery.js"></script>
   <script src="../../StockManage/Script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/Script/JustWinTable.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload = function()
        {
            var aa = new JustWinTable('gvReclaim');
        }
    </script>

</head>
<body class="body_frame" scroll="yes">
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="1"
            class="table-normal">
            <tr>
                <td class="td-title">
                    可收回邮件列表
                </td>
            </tr>
            <tr class="td-toolsbar">
                <td>
                    <asp:Button ID="btnDel" Text="收 回" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:HiddenField ID="hdnKeyField" runat="server" />
                    <asp:HiddenField ID="HdnSJID" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:GridView ID="gvReclaim" DataSourceID="sqlReclaim" CssClass="grid" AllowPaging="true" AutoGenerateColumns="false" PageSize="20" Width="100%" OnRowDataBound="gvReclaim_RowDataBound" DataKeyNames="I_YJ_ID" runat="server">
<EmptyDataTemplate>
                            <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse">
                                <tr class="grid_head">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col" style="width:100px">
                                        接收人
                                    </th>
                                    <th scope="col">
                                        主题
                                    </th>
                                   
                                    <th scope="col" style="width: 150px">
                                        时间
                                    </th>
                                    <th scope="col" style="width: 80px">
                                        带附件
                                    </th>
                                     <th scope="col" style="width: 80px">
                                        邮件状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="V_JSRXM" HeaderText="接收人" SortExpression="V_ZT" /><asp:BoundField DataField="V_ZT" HeaderText="主题" SortExpression="V_ZT" /><asp:BoundField DataField="DTM_SJSJ" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="时间" SortExpression="DTM_SJSJ" HtmlEncode="false" /><asp:BoundField DataField="c_fj" HeaderText="带附件" SortExpression="c_fj" /><asp:BoundField DataField="C_XBS" HeaderText="邮件状态" SortExpression="C_XBS" /></Columns><PagerStyle HorizontalAlign="Left"></PagerStyle></asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:SqlDataSource ID="sqlReclaim" SelectCommand="SELECT [I_SJID], [I_YJ_ID], [V_ZT], [V_FXRDM], [DTM_SJSJ],(case [c_fj] when 'n' then '无' when 'y' then '有' end) as c_fj,(case c_xbs when 'y' then '未读' when 'n' then '已读' end) as c_xbs ,[V_JSRXM] FROM [v_dzyj_Reclaim] WHERE ([V_FXRDM] = @V_FXRDM) order by dtm_sjsj desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="V_FXRDM" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
    </form>
</body>
</html>
