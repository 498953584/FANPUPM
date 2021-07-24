<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MatterBill.aspx.cs" Inherits="oa_WorkManage_StorageManage_MatterBill" %>

<html>
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RecordID,AuditState)
	{
	    document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('HdnRecordID').value = RecordID;
	}
	function OpenWin(op)
	{
	    var RecordID = "0";
	    if(op != 'add')
	    {
	        RecordID = document.getElementById('HdnRecordID').value;
	    }
	    var TypeCode = document.getElementById('HdnTypeCode').value;
		var url= "MatterBillEdit.aspx?op=" + op + "&rid="+ RecordID +"&cc="+ TypeCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:230px;dialogWidth:360px;center:1;help:0;status:0;");
		if(ref)
	    {
		   return true;
		}
		return false;
	}
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
                    <asp:ScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                        物资清单
                </td>
            </tr>
            <tr>
                <td height="20px" valign="middle" class="td-toolsbar">
                    <asp:UpdatePanel ID="UpdatePanelBtn" runat="server">
<ContentTemplate>
                    <asp:Button ID="btnAdd" Text="新 增" Enabled="false" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <input id="HdnRecordID" value="-1" style="width: 20px" type="hidden" runat="server" />

                        </ContentTemplate>
</asp:UpdatePanel>
                    <input id="HdnTypeCode" value="" style="width: 20px" type="hidden" runat="server" />

                 </td>
            </tr>
            <tr>
                <td height="100%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
<ContentTemplate>
                        <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        分类</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        领用类别</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        单位</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        计划成本</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="ResName" HeaderText="名称" HtmlEncode="false" /><asp:BoundField HeaderText="分类" HtmlEncode="false" /><asp:BoundField DataField="GetType" HeaderText="领用类别" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="单位" HtmlEncode="false" /><asp:BoundField DataField="PlanFee" HeaderText="计划成本" DataFormatString="{0:f2}" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_OfficeRes_Resources] WHERE ([TypeCode] = @TypeCode) ORDER BY [ResCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter DefaultValue="" Name="TypeCode" QueryStringField="cc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
