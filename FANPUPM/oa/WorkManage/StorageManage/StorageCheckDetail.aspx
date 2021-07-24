<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckDetail.aspx.cs" Inherits="oa_WorkManage_StorageManage_StorageCheckDetail" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(RecordID,InStorageID,AuditState)
	{
	    if(parseInt(AuditState) <= -1)
	    {
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		}
		else
		{
		    document.getElementById('btnEdit').disabled = true;
		    document.getElementById('btnDel').disabled = true;
		}
		document.getElementById('HdnRecordID').value = RecordID;
	}
	function checkDecimal(sourObj)
	{
		if (sourObj.value=="")
		{
			sourObj.value = 0;
		}
		if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
		{
			alert('数据类型不正确！');
			sourObj.focus();
			return;
		}
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">盘点记录明细</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnSave" Text="保  存" Enabled="false" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnConfirm" Text="确  认" Enabled="false" OnClick="btnConfirm_Click" runat="server" />
                    <input id="HdnRecordID" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        分类</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        领用类别</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        单位</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        单价(元)</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        账面数量</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        实际数量</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        盘点人</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="ResName" HeaderText="名称" HtmlEncode="false" /><asp:BoundField HeaderText="分类" HtmlEncode="false" /><asp:BoundField HeaderText="领用类别" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="单位" HtmlEncode="false" /><asp:BoundField DataField="AvgPrice" HeaderText="单价(元)" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:BoundField DataField="Number" HeaderText="账面数量" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:TemplateField HeaderText="实际数量">
<ItemTemplate>
                                    <asp:TextBox ID="txtFactAmount" CssClass="text-normal" style="width:100%" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="盘点人" HtmlEncode="false" /><asp:BoundField HeaderText="备注" HtmlEncode="false" /><asp:BoundField DataField="MaterialID" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
