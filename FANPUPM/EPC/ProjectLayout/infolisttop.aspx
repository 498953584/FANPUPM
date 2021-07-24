<%@ Page Language="C#" AutoEventWireup="true" CodeFile="infolisttop.aspx.cs" Inherits="InfoListTop" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>InfoListTop</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		 function getBeginDate()
		   {
			if (document.getElementById('txtStartDate').value!="")
			{

				var beginTT=getTheDate(document.getElementById('txtStartDate').value);
				var endTT = getTheDate(document.getElementById('txtEndDate').value);
				if (beginTT==endTT)
				{
				    return;
				}
				if (beginTT!="")
				{
					if (endTT<beginTT)
					{
						alert('结束时间不能早于开始时间！');
						document.getElementById('txtEndDate').value = "";
						document.getElementById('txtEndDate').blur();
					}

				}
			}

		}
		//重新设置取到的时间格式
	    function getTheDate(dateStr)
		{
			var theYear;
			var theMonth;
			var theDay;
			var i = dateStr.indexOf("-");
			var j = dateStr.lastIndexOf("-");
			theYear = parseInt(dateStr.substr(0,i));
			theMonth = parseInt(dateStr.substr(i+1,j-i-1))-1;
			theDay = parseInt(dateStr.substr(j+1));
			return new Date(theYear,theMonth,theDay);
		}
		</script>
	</head>
	
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%">
				<tr>
					<td>
						<TABLE  class="queryTable" id="Table2" cellSpacing="0" cellPadding="3px" border="0">
							<tr style="white-space:nowrap; text-align:left;">
								<td class="descTd" style="white-space:nowrap">
									项目时间
								 </td>
								 <td>
									<JWControl:DateBox ID="txtStartDate" Columns="10" ReadOnly="false" Width="60px" runat="server"></JWControl:DateBox>
									—<JWControl:DateBox ID="txtEndDate" Columns="10" ReadOnly="false" Width="60px" runat="server"></JWControl:DateBox>
								 </td>
								 <td class="descTd" style="white-space:nowrap">
									项目编号
								 </td>	
								<td>
									<asp:TextBox ID="txtPrjNum" Columns="10" Width="120px" runat="server"></asp:TextBox>
								</td>
								<td class="descTd" style="white-space:nowrap">
									项目名称
								</td>
								<td>
									<asp:TextBox ID="txtPrjName" Width="120px" runat="server"></asp:TextBox>
								</td>
								<td class="descTd" style="white-space:nowrap">
									项目单位
								</td>
								<td>
									<asp:TextBox ID="txtprjUnit" Width="120px" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr  style="white-space:nowrap;text-align:left;">
							    <td class="descTd">	
									项目类型
								</td>
								<td>
									<JWControl:DropDownTree ID="drop_PrjKindClass" Width="140px" runat="server"></JWControl:DropDownTree>
								</td>
								<td class="descTd">
								   工程造价
								</td>
								<td>
									<asp:DropDownList ID="DropDownList1" Width="120px" runat="server"><asp:ListItem Value="0" Text="不限" /><asp:ListItem Value="1" Text="100万以内" /><asp:ListItem Value="2" Text="100万—500万" /><asp:ListItem Value="3" Text="500万—1000万" /><asp:ListItem Value="4" Text="1000万—5000万" /><asp:ListItem Value="5" Text="5000万以上" /></asp:DropDownList>
								</td>	
								<td class="descTd">
									项目地区
								</td>
								<td>	
									<JWControl:DropDownTree ID="ddt_Area" Width="120px" runat="server"></JWControl:DropDownTree>
								</td>
								<td class="descTd">	
									项目状态
								</td>
								<td>
									<JWControl:DropDownTree ID="ddt_state" Width="120px" runat="server"></JWControl:DropDownTree>&nbsp;&nbsp;
								</td>
								<td>
									<asp:Button ID="btnSearch" CssClass="button-normal" Text="查询" OnClick="btnSearch_Click" runat="server" /><input type="hidden" id="hdnUrl" runat="server" />
	
								</td>	
								
										
								
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td height="100%" valign="top"><iframe width="100%" height="100%" frameborder="0" name="InfoList" id="InfoList" scrolling="no"
							src="<%=this.FramUrl %>?SqlWhere=<%=this.strURL %>"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
