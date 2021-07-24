<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDrawList.aspx.cs" Inherits="oa_WorkManage_SubCompanyManage_CompanyDrawList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(RecordID,InStorageID,AuditState)
	{
	}
	function Check(obj)
	{
	    var o = document.getElementsByTagName('input');
	    for(var i=0;i<o.length;i++)
	    {
	        if(o[i].type == 'checkbox' && o[i].id != obj.id)
	        {
	            o[i].checked = obj.checked;
	        }
	    }
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">个人领用清单</td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="1px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" runat="server">
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
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField>
<HeaderTemplate>
                                    <input type="checkbox" id="chkMain" onclick="Check(this);" runat="server" />

                                </HeaderTemplate>

<ItemTemplate>
                                    <input type="checkbox" id="chkSub" runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResName" HeaderText="名称" HtmlEncode="false" /><asp:BoundField HeaderText="分类" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="单位" HtmlEncode="false" /><asp:BoundField DataField="PlanFee" HeaderText="单价(元)" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:BoundField DataField="ApplyNumber" HeaderText="数量" DataFormatString="{0:f0}" HtmlEncode="false" /><asp:BoundField DataField="MaterialID" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" height="20px" class="td-submit">
                <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			    <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
                </td>
            </tr>
        </table>
    </form>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
