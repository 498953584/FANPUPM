<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPersonRecordEdit.aspx.cs" Inherits="oa_WorkManage_NewPersonRecordEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>新员工领用登记</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    <!--
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
	    function IsInteger(sourObj)
	    {
		    if (sourObj.value == "")
		    {
		    	sourObj.value = 0;
		    }
		    else
		    {
		       if (!(new RegExp(/^\d+$/).test(sourObj.value)))
		    	{
		    	    alert('数据类型不正确！');
		    	    sourObj.focus();
		    	    return;
			     }
		    }
	    }
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                    领用标准</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />
			        <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" PageSize="20" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        用品名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        单位</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        标准数量</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        实际数量</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="ResName" HeaderText="用品名称" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="单位" HtmlEncode="false" /><asp:BoundField DataField="Number" HeaderText="标准数量" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:TemplateField HeaderText="实际数量">
<ItemTemplate>
                                    <input type="text" id="factNum" onblur="checkDecimal(this);" style="width: 100%" runat="server" />

                                    <input type="hidden" id="HdnPlanFee" value='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "PlanFee")) %>' runat="server" />

                                    <input type="hidden" id="HdnMatterialID" value='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "DrawItemID")) %>' runat="server" />

                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
