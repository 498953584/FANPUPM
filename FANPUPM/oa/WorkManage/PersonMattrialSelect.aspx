<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonMattrialSelect.aspx.cs" Inherits="oa_WorkManage_PersonMattrialSelect" %>

<html>
<head id="Head1" runat="server"><title>������Ϣ</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    function ClickRow(ResName,UseType,GetType,Unit,PlanFee,MattrialID)
	{
		document.getElementById('HdnResName').value = ResName;
		document.getElementById('HdnUseType').value = UseType;
		document.getElementById('HdnGetType').value = GetType;
		document.getElementById('HdnUnit').value = Unit;
		document.getElementById('HdnPlanFee').value = PlanFee;
		document.getElementById('HdnMattrialID').value = MattrialID;
		document.getElementById('btnAdd').disabled = false;
	}
	function MaterialSelect()
	{
	    var Matterial = window.dialogArguments;
	    Matterial[0] = document.getElementById('HdnResName').value;
	    Matterial[1] = document.getElementById('HdnUseType').value;
	    Matterial[2] = document.getElementById('HdnGetType').value;
	    Matterial[3] = document.getElementById('HdnUnit').value;
	    Matterial[4] = document.getElementById('HdnPlanFee').value;
	    Matterial[5] = document.getElementById('HdnMattrialID').value;
	    window.returnValue = Matterial;
	    window.close();
	}
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                        ������Ϣ
                        <input id="HdnResName" value="-1" style="width: 20px" type="hidden" runat="server" />

                        <input id="HdnUseType" value="-1" style="width: 20px" type="hidden" runat="server" />

                        <input id="HdnGetType" value="-1" style="width: 20px" type="hidden" runat="server" />

                        <input id="HdnUnit" value="-1" style="width: 20px" type="hidden" runat="server" />

                        <input id="HdnPlanFee" value="-1" style="width: 20px" type="hidden" runat="server" />

                        <input id="HdnMattrialID" value="-1" style="width: 20px" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td height="100%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        ���</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        ����</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        ����</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        �������</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        ��λ</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        �ƻ��ɱ�</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="���" HtmlEncode="false" /><asp:BoundField DataField="ResName" HeaderText="����" HtmlEncode="false" /><asp:BoundField HeaderText="����" HtmlEncode="false" /><asp:BoundField DataField="GetType" HeaderText="�������" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="��λ" HtmlEncode="false" /><asp:BoundField DataField="PlanFee" HeaderText="�ƻ��ɱ�" DataFormatString="{0:f2}" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="��ҳ" LastPageText="βҳ" NextPageText="��һҳ" PreviousPageText="��һҳ"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_OfficeRes_Resources] where GetType='0' ORDER BY [ResCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
			<td class="td-submit" height="20">
			    <asp:Button ID="btnAdd" Text="ȷ  ��" Enabled="false" runat="server" />&nbsp;
			    <INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="��  ��">
			    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
        </table>
    </form>
</body>
</html>
