<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostWeaveRoleSelectData.aspx.cs" Inherits="HR_Organization_PostWeaveRoleSelectData" %>

<html>
<head runat="server"><title></title><meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RoleTypeCode,RoleTypeName)
    {
        document.getElementById('BtnOk').disabled = false;
        document.getElementById('HdnRoleName').value = RoleTypeName;
        document.getElementById('HdnDutyCode').value = RoleTypeCode;
        document.getElementById('HdnRoleTypeCode').value = RoleTypeCode.substr(0,3);
    }
	
	function winclick()
	{
	    var role = parent.window.dialogArguments;
		role[0] = document.getElementById('HdnRoleName').value;
		role[1] = document.getElementById('HdnDutyCode').value;
		role[2] = document.getElementById('HdnRoleTypeCode').value;
		parent.window.returnvalue= role;
		window.close();
	}
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" method="post" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="100%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" DataSourceID="SqlDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col">
                                        岗位名称</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="RoleTypeName" HeaderText="岗位名称" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [PT_D_Role] WHERE ([ParentCode] = @ParentCode) ORDER BY [RoleTypeCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="ParentCode" QueryStringField="cc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="td-submit" height="20">
                    <input id="BtnOk" type="button" onclick="javascript:winclick();" disabled="disabled" value="确  定" />
                    &nbsp;
			        <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="关  闭">
			        <input id="HdnRoleName" value="" style="width: 20px" type="hidden" runat="server" />

			        <input id="HdnRoleTypeCode" value="" style="width: 20px" type="hidden" runat="server" />

			        <input id="HdnDutyCode" value="" style="width: 20px" type="hidden" runat="server" />

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
