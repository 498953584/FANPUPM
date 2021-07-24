<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRLayout_PersonPay.aspx.cs" Inherits="HR_Organization_HRLayout_PersonPay" %>

<html>
<head runat="server"><title></title><meta http-equiv="content-type" content="text/html; charset=gb2312" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
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
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" style="table-layout:fixed;">
            <tr>
                <td height="20px" class="td-submit">
                    <asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="1px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr class="grid_head" align="center">
                                    <td rowspan="2" style="background-color:#ece9d8;"><font style="FONT-WEIGHT: bold">人员类别</font></td>
                                    <td rowspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">平均工资</font></td>
                                    <td colspan=13 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">平均人数分布</font></td>
                                </tr>
                                <tr>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">1月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">2月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">3月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">4月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">5月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">6月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">7月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">8月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">9月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">10月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">11月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">12月</font></td>
                                    <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style="FONT-WEIGHT: bold">备注</font></td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:BoundField DataField="ClassName" HeaderText="人员类别" HtmlEncode="false" /><asp:TemplateField HeaderText="平均工资">
<ItemTemplate>
                                    <asp:TextBox ID="txtAveragePay" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                    <input type="hidden" id="HdnClassID" value='<%# Convert.ToString(Eval("DataItem.ClassID")) %>' runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="平均人数分布/1月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney1" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="2月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney2" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="3月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney3" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="4月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney4" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="5月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney5" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="6月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney6" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="7月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney7" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="8月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney8" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="9月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney9" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="10月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney10" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="11月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney11" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="12月">
<ItemTemplate>
                                    <asp:TextBox ID="txtMoney12" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                    <asp:TextBox ID="txtRemark" CssClass="text-normal" Text="" Width="100%" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [PT_SingleClasses] WHERE ([ClassTypeCode] = @ClassTypeCode and [IsValid]=@IsValid) ORDER BY [ClassID]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="003" Name="ClassTypeCode" Type="String" /><asp:Parameter DefaultValue="1" Name="IsValid" Type="String" /></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
