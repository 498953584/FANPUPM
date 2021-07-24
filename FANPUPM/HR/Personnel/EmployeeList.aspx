<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="EmployeeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>员工信息列表</title>
	<script language="javascript" type="text/javascript">
	<!--
	function ClickRow(UserCode)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('hdnUserCode').value = UserCode;
	}
	function openEmployeeEdit(op,CorpCode)
	{				
		var UserCode = 0;
		if (op==2)
		{
			UserCode = document.getElementById('hdnUserCode').value;
		}	
		var url= "EmployeeEdit.aspx?t=" + op + "&cc=" + CorpCode + "&uc="+ UserCode;
		return window.showModalDialog(url,window,"dialogHeight:220px;dialogWidth:400px;center:1;help:0;status:0;");
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">员工信息列表</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
						<input type="hidden" id="hdnUserCode" name="hdnUserCode" style="WIDTH: 10px" runat="server" />

						<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
						<asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    </td></tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                <asp:GridView ID="gvEmployeelist" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlTemplate" Width="100%" OnRowDataBound="gvEmployeelist_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="gvEmployeelist" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:10%">序号</th><th scope="col" style="width:25%">部门名称</th><th scope="col">姓名</th><th scope="col" style="width:15%">岗位</th><th scope="col" style="width:10%">入司日期</th><th scope="col" style="width:8%">年龄</th><th scope="col" style="width:10%">状态</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                  
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="bmmc" HeaderText="部门名称" SortExpression="bmmc" /><asp:BoundField DataField="v_xm" HeaderText="姓名" SortExpression="v_xm" /><asp:BoundField DataField="DutyName" HeaderText="岗位" SortExpression="DutyName" /><asp:BoundField DataField="EnterCorpDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入司日期" HtmlEncode="false" SortExpression="EnterCorpDate" /><asp:BoundField DataField="Age" HeaderText="年龄" SortExpression="Age" /><asp:BoundField DataField="StateName" HeaderText="状态" SortExpression="StateName" /></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlTemplate" SelectCommand="SELECT [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc, [I_DUTYID],(select b.RoleTypeName from pt_d_role b where b.RoleTypeCode = (select c.DutyCode from pt_duty c where c.i_dutyId = a.i_dutyId)) as DutyName, [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State],(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end) as StateName FROM [PT_yhmc] a WHERE ([RelationCorp] = @RelationCorp) order by v_xm" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="RelationCorp" QueryStringField="cc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>		
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
			</div>
		</TD>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
