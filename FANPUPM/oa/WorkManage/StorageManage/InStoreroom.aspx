<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InStoreroom.aspx.cs" Inherits="oa_WorkManage_StorageManage_InStoreroom" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(DepotID,InStorageID,BillCode,AuditState)
	{
	    if(AuditState == "0")
	    {
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		    document.getElementById('btnStartWF').disabled = false;
		}
		else
		{
		    document.getElementById('btnEdit').disabled = true;
		    document.getElementById('btnDel').disabled = true;
		    document.getElementById('btnStartWF').disabled = true;
		}
		document.getElementById('HdnDepotID').value = DepotID;
		document.getElementById('HdnInStorageID').value = InStorageID;
		frmInStoreroom.location.href = "InStoreroomAssTab.aspx?ia="+AuditState+"&dd="+DepotID+"&bc="+BillCode+"&id="+InStorageID;
	}
	function OpenWin(op)
	{
	    var RecordCode = document.getElementById('HdnInStorageID').value;
		var url= "InStoreroomEdit.aspx?op=" + op + "&id=" + RecordCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:450px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">入库单信息<asp:ScriptManager ID="SManager" runat="server"></asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:UpdatePanel ID="UPanel" runat="server">
<ContentTemplate>
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnStartWF" Text="确  认" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                    <input id="HdnInStorageID" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnDepotID" type="hidden" style="width:20px" runat="server" />

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
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        入库单号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        入库日期</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        办理人</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="BillCode" HeaderText="入库单号" HtmlEncode="false" /><asp:BoundField DataField="InDate" HeaderText="入库日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Transactor" HeaderText="办理人" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                        </ContentTemplate>
</asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <iframe id="frmInStoreroom" frameborder="0" width="100%" height="100%" src="InStoreroomAssTab.aspx?ia=0&dd=-1&bc=&id=-1" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
