<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkLog.aspx.cs" Inherits="TypeList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>TypeList</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
	            function doClickRow(classId)
	            {
		            document.getElementById('btnEdit').disabled = false;
		            document.getElementById('btnDel').disabled = false;
		            document.getElementById('btnLook').disabled = false;
		            document.getElementById('hdnLogId').value=classId;
	            }
	        function openClassEdit(op)
	        {	
		        var LogId = 0;
		        var pageTitle = document.getElementById('Td_Title').innerText ;		
		        if (op==2)
		        {
			        LogId = document.getElementById('hdnLogId').value;
		        }
		        else if (op==3)
		        {
			        LogId = document.getElementById('hdnLogId').value;
		        }
		        else
		        {
		            LogId = 0;
		        }		
		        var url= "WorkLogAdd.aspx?t=" + op  + "&LogId="+ LogId + "&title=" + escape(pageTitle) + "&pmId= "+ document.getElementById('hdnPmId').value ;
        		
		        window.showModalDialog(url,window,"dialogHeight:600px;dialogWidth:660px;center:1;help:0;status:0;");
	            //var url= "ConstructionLogAdd.aspx?t=123&LogId="+ LogId + "&title=" + escape(pageTitle) + "&pmId= "+ document.getElementById('hdnPmId').value ;
		        //window.open(url);
	        }
		 
		</script>
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" border="1" cellpadding="0" cellspacing="0" id="Table1" width="100%"
				height="100%">
				<TR>
					   <td id="Td_Title" class="td-title" align="center" height="28" runat="server">
                           工作日志</td>
               
				</TR>
				<tr>
						   <td class="td-search" style="height:28px">
                    编码:
                    <asp:TextBox ID="txtCode" Width="80px" runat="server"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txtPart" Width="80px" Visible="false" runat="server"></asp:TextBox>
                    操作负责人：
                    <asp:TextBox ID="txtOperations" Width="80px" runat="server"></asp:TextBox>
                      施工日期：<JWControl:DateBox ID="workdate" Width="95px" runat="server"></JWControl:DateBox>
                    <asp:Button ID="btnSearch" Text=" " OnClick="btnSearch_Click" runat="server" />
                    &nbsp;
                </td>
				</tr>
				<TR>
					<TD class="td-toolsbar" align="right"><input id="hdnPmId" type="hidden" runat="server" />

					 <input type="hidden" id="hdnLogID" name="hdnClassID" runat="server" />

						<input id="hdnType" type="hidden" name="hdnType" runat="server" />
&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnAdd" CssClass="button-normal" Text="新  增" runat="server" />&nbsp;<asp:Button ID="BtnEdit" CssClass="button-normal" Text="编  辑" Enabled="false" runat="server" />&nbsp;<asp:Button ID="BtnDel" CssClass="button-normal" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" /><asp:Button ID="BtnLook" Text="查看" Enabled="false" runat="server" />
					</TD>
				</TR>
				<TR>
					<TD colSpan="2" height="100%" valign="top">
						<div class="gridBox">     <asp:DataGrid ID="dgClass" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15" OnItemDataBound="dgClass_ItemDataBound" OnPageIndexChanged="dgClass_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                  <asp:Label ID="Index" Text='<%# Convert.ToString(this.dgClass.CurrentPageIndex * this.dgClass.PageSize + this.dgClass.Items.Count + 1) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="编号">
<ItemTemplate>
                                        <asp:Label ID="TxtCode" Width="100%" Text='<%# Convert.ToString(Eval("code")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="天气">
<ItemTemplate>
                                        <asp:Label ID="TxtCode" Width="100%" Text='<%# Convert.ToString(Eval("amweather")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="生产情况">
<ItemTemplate>
                                        <asp:Label ID="TxtCode" Width="100%" Text='<%# Convert.ToString(Eval("daycontent")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="技术质量安全工作记录">
<ItemTemplate>
                                        <asp:Label ID="TxtCode" Width="100%" Text='<%# Convert.ToString(Eval("beton")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="记录人">
<ItemTemplate>
                                        <asp:Label ID="TxtOperations" Width="100%" Text='<%# Convert.ToString(Eval("operations")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="施工内容" Visible="false">
<ItemTemplate>
                                        <asp:Label ID="TxtDayContent" Width="100%" Text='<%# Convert.ToString(Eval("daycontent")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="thisDate" HeaderText="施工日期" DataFormatString="{0:yyyy年MM月dd日}"></asp:BoundColumn></Columns><PagerStyle HorizontalAlign="Center" Mode="NumericPages"></PagerStyle></asp:DataGrid></div><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
					</TD>
				</TR>
				
			</TABLE>
		</form>
	</body>
</HTML>
