<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentDrawApplyAssTab.aspx.cs" Inherits="oa_WorkManage_StorageManage_DepartmentDrawApplyAssTab" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
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
	function OpenWin(op)
	{
	    var RecordID = document.getElementById('HdnRecordID').value;
	    var InStorageID = document.getElementById('HdnInStorageID').value;
	    var DepartmentID = document.getElementById('HdnDepartmentID').value;
		var url= "DepartmentDrawApplyAssTabEdit.aspx?op=" + op + "&rid=" + RecordID + "&id="+ InStorageID+"&dm="+DepartmentID;
		var ref = window.showModalDialog(url,window,"dialogHeight:180px;dialogWidth:480px;center:1;help:0;status:0;");
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
                <td height="20px" colspan="2" class="td-title">部门领用申请</td>
            </tr>
            <tr>
                <td width="60%" class="td-toolsbar" style="text-align:left">                
                部门年定额总数:<asp:Label ID="LblRation" runat="server"></asp:Label>元&nbsp;
                部门月定额:<asp:Label ID="LblMonth" runat="server">0.00</asp:Label>元&nbsp;
                已领金额:<asp:Label ID="LbAlready" runat="server">0.00</asp:Label>元\r
                </td>
                
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新 增" Enabled="false" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <input id="HdnRecordID" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnInStorageID" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnDepartmentID" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                 </td>
            </tr>
            <tr>
                <td colspan=2 valign="top" height="100%">
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
                                        单位</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        单价(元)</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        数量</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="ResName" HeaderText="名称" HtmlEncode="false" /><asp:BoundField HeaderText="分类" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="单位" HtmlEncode="false" /><asp:BoundField DataField="PlanFee" HeaderText="单价(元)" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:BoundField DataField="ApplyNumber" HeaderText="数量" DataFormatString="{0:f2}" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
