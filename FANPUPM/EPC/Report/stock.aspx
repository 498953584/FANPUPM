<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stock.aspx.cs" Inherits="reportView" EnableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server"><title>OrderQuery</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript">
			function openQuery(reportid)
			{
				var url= "../Query/ReportQuerySet.aspx?reportid="+reportid;				
				return window.showModalDialog(url,window,"dialogHeight:266px;dialogWidth:430px;center:1;help:0;status:0;");
			}
       
		</script>
	</head>
	<body style="margin:0px;overflow:hidden;">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border=0 >
				<tr class="td-toolsbar">
					<td height="24">&nbsp;&nbsp;<asp:Button ID="btnSet" CssClass="button-normal" Text="高级查询" OnClick="btnSearch_Click" runat="server" />
						<asp:Button ID="btnexecl" CssClass="button-normal" Text="导出->Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
						<asp:Button ID="btnWord" CssClass="button-normal" Text="导出->Word" Width="80px" OnClick="btnWord_Click" runat="server" />
				    </td>
				</tr>
				<tr>
					<td>
						<table class="table-normal" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr id="TRTitle" class="report_grid_head" height="24" runat="server"><td align="center" colspan="3" class="td-title" bgcolor="Gainsboro" runat="server"><asp:Label ID="Lb_Title" runat="server"></asp:Label></td></tr><tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro" height="24" runat="server"><td align="left" width="33%" style="height: 24px" runat="server">库名：<asp:DropDownList ID="DropDownList1" Width="121px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server"></asp:DropDownList>&nbsp; &nbsp;<asp:Label ID="Lb_H1" runat="server"></asp:Label></td><td align="center" width="33%" style="height: 24px" runat="server"><asp:Label ID="Lb_H2" runat="server"></asp:Label></td><td align="right" width="33%" style="height: 24px" runat="server"><asp:Label ID="Lb_H3" runat="server"></asp:Label></td></tr><tr runat="server"><td valign="top" colspan="3" style="width:100%;height:100%;" runat="server">
							        <DIV style="WIDTH: 100%; HEIGHT: 100%;overflow:auto;">
							            <asp:GridView ID="DG1" style="border:solid 0px blue; width:100%;" OnRowDataBound="DG1_RowDataBound" OnSorting="DG1_Sorting" runat="server">
<EmptyDataTemplate>
                                                <table cellspacing="0" rules="all" border="1" id="DG1" style="width:100%;border-collapse: collapse;border:solid 0px red;">
                                                    <tr class="grid_head" nowrap="nowrap" align="center" valign="middle" style="background-color: Gainsboro;">
                                                        <th scope="col"> 序号</th>
                                                        <th scope="col">项目编号</th>
                                                        <th scope="col">项目名称</th>
                                                        <th scope="col"> ...</th>
                                                    </tr>
                                                    <tr class="grid_row" nowrap="nowrap" align="center" valign="middle" onclick="OnRecord(this);"  onmouseover="OnMouseOverRecord(this)">
                                                        <td>1</td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号"></asp:TemplateField></Columns><RowStyle CssClass="grid_row" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle><HeaderStyle CssClass="grid_head" BackColor="Gainsboro" Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle></asp:GridView> 
                                    </DIV>
							    </td></tr><tr id="TrFooter" bgcolor="activeborder" height="24" class="report_head" runat="server"><td align="left" runat="server"><asp:Label ID="Lb_F1" runat="server"></asp:Label></td><td align="center" runat="server"><asp:Label ID="Lb_F2" runat="server"></asp:Label></td><td align="right" runat="server"><asp:Label ID="Lb_F3" runat="server"></asp:Label></td></tr></table>
				        <input id="Hdn1" type="hidden" name="Hdn1" runat="server" />

					</td>
				</tr>
            </TABLE>
		</form>
	</body>
</html>
