<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPersonRecord.aspx.cs" Inherits="oa_WorkManage_NewPersonRecord" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(UserCode,PositionLevel)
	{
		document.getElementById('HdnUserCode').value = UserCode;
		document.getElementById('HdnPositionLevel').value = PositionLevel;
		document.getElementById('btnTransact').disabled = false;
	}
	function OpenWin(op)
	{
	    var UserCode = document.getElementById('HdnUserCode').value;
	    var PositionLevel = document.getElementById('HdnPositionLevel').value;
		var url= "NewPersonRecordEdit.aspx?pl="+PositionLevel+"&uc="+ UserCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:500px;dialogWidth:450px;center:1;help:0;status:0;");
		return false;
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">新员工领用登记</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnTransact" Text="领用办理" Enabled="false" runat="server" />
                    <input id="HdnUserCode" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnPositionLevel" type="hidden" style="width:20px" runat="server" />

            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" DataSourceID="SqlDataSource1" PageSize="20" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        部门</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        姓名</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        级别</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        职衔</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        入司时间</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        年龄</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField HeaderText="部门" HtmlEncode="false" /><asp:BoundField DataField="v_xm" HeaderText="姓名" HtmlEncode="false" /><asp:BoundField HeaderText="级别" HtmlEncode="false" /><asp:BoundField HeaderText="职衔" HtmlEncode="false" /><asp:BoundField DataField="EnterCorpDate" HeaderText="入司时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Age" HeaderText="年龄" HtmlEncode="false" /><asp:BoundField HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" SelectCommand="select * from PT_yhmc WHERE ([RelationCorp] = @RelationCorp) and State='0' and v_yhdm not in (select distinct UseMan from OA_OfficeRes_PersonalApplication where IsComplete='1') ORDER BY [v_yhdm]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="RelationCorp" QueryStringField="cc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
