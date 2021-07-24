<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheck.aspx.cs" Inherits="oa_WorkManage_StorageManage_StorageCheck" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(DepotID,CheckRecordID,AuditState)
	{
		frmInStoreroom.location.href = "StorageCheckDetail.aspx?ia="+AuditState+"&dd="+DepotID+"&id="+CheckRecordID;
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">盘点记录列表<asp:ScriptManager ID="SManager" runat="server"></asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:UpdatePanel ID="UPanel" runat="server">
<ContentTemplate>
                    <asp:Button ID="btnStartWF" Text="盘  点" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                    <input id="HdnInStorageID" type="hidden" style="width:20px" runat="server" />

                        </ContentTemplate>
</asp:UpdatePanel>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <asp:UpdatePanel ID="UPanelGV" runat="server">
<ContentTemplate>
                            <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        盘点日期</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        盘点人</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        状态</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="RecordDate" HeaderText="盘点日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="盘点人" HtmlEncode="false" /><asp:BoundField HeaderText="状态" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                        </ContentTemplate>
</asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <iframe id="frmInStoreroom" frameborder="0" width="100%" height="100%" src="StorageCheckDetail.aspx?ia=-1&dd=-1&id=-1" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
