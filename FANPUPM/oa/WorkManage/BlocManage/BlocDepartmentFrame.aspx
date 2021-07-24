<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlocDepartmentFrame.aspx.cs" Inherits="oa_WorkManage_BlocManage_BlocDepartmentFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
        function ClickRow(PARecordID,IsComplete)
        {
            if(IsComplete == "0" || IsComplete == "")
            {
                document.getElementById('btnTransact').disabled = false;
            }
            else
            {
                document.getElementById('btnTransact').disabled = true;
            }
            document.getElementById('HdnRecordID').value = PARecordID;
            frmMatter.location.href = "BlocDepartmentDraw.aspx?id="+PARecordID;
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                        领用记录</td>
             </tr>				
             <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnTransact" Text="办理确认" Enabled="false" OnClick="btnTransact_Click" runat="server" />
                    <input id="HdnRecordID" style="width: 20px" type="hidden" runat="server" />
</td>
             </tr>
             <tr>
                <td height="50%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVManager" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="1px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVManager_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        申请日期</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        集团部门名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        状态</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="ApplyDate" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="集团部门名称" HtmlEncode="false" /><asp:BoundField HeaderText="状态" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_OfficeRes_DepartmentApplication] as a inner join OA_OfficeRes_ApplicationCollect b 
on a.ACRecordID = b.ACRecordID where b.AuditState = 1 and (a.AuditState = 1 or a.IsSubmit = 1) and a.CorpCode='00' 
" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                    </div>
                </td>
             </tr>
             <tr>
                <td height="50%" valign="top">
                    <iframe id="frmMatter" name="frmMatter" src="BlocDepartmentDraw.aspx?id=" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
