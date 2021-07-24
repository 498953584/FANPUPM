<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRLayoutLock.aspx.cs" Inherits="HR_Organization_HRLayoutLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>人力资源规划</title>
    <script language ="javascript">
	<!--
	window.name = "win";
	function UpFile()
	{			
		var RecordCode = document.getElementById('HdnRecordCode').value;//编号
		var url = "../../../commonWindow/Annex/AnnexList.aspx?mid=28&rc="+RecordCode+"&at=0";
		var ref = window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
		//if(ref)
		//{
		//    return true;
		//}
		//return false;
		return true;
	}
	-->
	</script>
</head>
<body class="body_popup">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" id="tablex" cellspacing="0" cellpadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colspan="4" height="20">
                人力资源年度规划
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                规划名称</td>
			<td>
			    <asp:Label ID="LbLayoutName" runat="server"></asp:Label>
			</td>
		
			<td class="td-label" width="20%">
                添加日期</td>
            <td>
                <asp:Label ID="LbLayoutDate" runat="server"></asp:Label>    
            </td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                添加人</td>
			<td>
                <asp:Label ID="LblAddPerson" runat="server"></asp:Label>
            </td>
		
			<td class="td-label" width="20%">
                附件
            </td>
            <td >
                <asp:Label ID="lblAnnex" runat="server"></asp:Label>&nbsp;
            </td>
		</tr>   
         <tr>
             <td colspan="4">
                 <asp:GridView ID="GVBook" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="1px" CellPadding="0" CssClass="grid" DataSourceID="SQLDataSource" Width="100%" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                         <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                             style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                             width: 100%; border-collapse: collapse; border-right-width: 0px">
                             <tr align="center" class="grid_head">
                                 <td rowspan="2" style="background-color: #ece9d8;">
                                     <font style="font-weight: bold">人员类别</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" rowspan="2" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">平均工资</font></td>
                                 <td align="center" bgcolor='#ece9d8' colspan="13" height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">平均人数分布</font></td>
                             </tr>
                             <tr>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">1月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">2月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">3月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">4月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">5月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">6月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">7月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">8月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">9月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">10月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">11月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">12月</font></td>
                                 <td align="center" bgcolor='#ece9d8' height="20" style='border-left: #ffffff 1px solid;
                                     border-top: #ffffff 1px solid; border-right: #aca899 1px solid; border-bottom: #aca899 1px solid;
                                     text-align: center;'>
                                     <font style="font-weight: bold">备注</font></td>
                             </tr>
                         </table>
                     </EmptyDataTemplate>
<Columns><asp:BoundField DataField="ClassName" HeaderText="人员类别" HtmlEncode="false" /><asp:TemplateField HeaderText="平均工资">
<ItemTemplate>
                                 <asp:TextBox ID="txtAveragePay" CssClass="text-normal" Text="0" Width="70px" runat="server"></asp:TextBox>
                                 <input id="HdnClassID" type="hidden" value='<%# Convert.ToString(Eval("DataItem.ClassID")) %>' runat="server" />

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
             </td>
         </tr>
		
	</table>
    </div>
    </form>
</body>
</html>
