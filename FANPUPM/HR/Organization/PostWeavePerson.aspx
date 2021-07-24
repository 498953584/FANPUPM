<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostWeavePerson.aspx.cs" Inherits="HR_Organization_PostWeavePerson" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head id="Head1" runat="server"><title></title>
    <script language="javascript" type="text/javascript">
	<!--
	function ClickRow(UserCode,deptID)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('hdnUserCode').value = UserCode;
		document.getElementById('hdnCorpCode').value = deptID;
	}
	function openEmployeeEdit(op)
	{				
		var UserCode = "";
		var url = "";
		var str = "";
		var deptID = document.getElementById('hdnCorpCode').value;
        if (deptID != "")
        {
            if (op==2)
	        {
		        UserCode = document.getElementById('hdnUserCode').value;
		        url= "EmployeeEdit.aspx?t=" + op + "&cc=" + deptID + "&uc="+ UserCode;
		        str = "dialogHeight:330px;dialogWidth:700px;center:1;help:0;status:0;" ;
	        }
	        else
	        {
	            url= "EmployeeAdd.aspx?t=" + op + "&cc=" + deptID + "&uc="+ UserCode;
	            str = "dialogHeight:280px;dialogWidth:700px;center:1;help:0;status:0;" ;
	        }
	        return window.showModalDialog(url,window,str);
	    }
	    else
	    {
	        alert('请选择部门!');
	    }
	}
	function aa(op)
	{
	    if (op == "2")
	    {
	        document.getElementById('btnAdd').disabled = false;
	    }
	    else
	    {
	        document.getElementById('btnAdd').disabled = true;
	    }
	}
	-->
	</script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
        <table width="100%"  height="100%" border="0" cellpadding="0" cellspacing="0" id="tablex">
            <tr>
                <TD class="td-title" align="center" height="20">员工信息列表</TD>
            </tr>
            <tr>
                <TD vAlign="top">
	                <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:GridView ID="gvEmployeelist" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlTemplate" Width="100%" OnRowDataBound="gvEmployeelist_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
                                    <table class="grid" cellspacing="0" rules="all" border="1" id="gvEmployeelist" style="border-collapse:collapse;">
                                        <tr class="grid_head">
	                                        <th scope="col" style="width:40px">序号</th><th scope="col" style="width:25%">部门名称</th><th scope="col">姓名</th><th scope="col" style="width:15%">岗位</th><th scope="col" style="width:10%">入司日期</th><th scope="col" style="width:8%">年龄</th><th scope="col" style="width:10%">状态</th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>  
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="bmmc" HeaderText="部门名称" SortExpression="bmmc" /><asp:BoundField DataField="v_xm" HeaderText="姓名" SortExpression="v_xm" /><asp:BoundField DataField="DutyName" HeaderText="岗位" SortExpression="DutyName" /><asp:BoundField DataField="EnterCorpDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入司日期" HtmlEncode="false" SortExpression="EnterCorpDate" /><asp:BoundField DataField="Age" HeaderText="年龄" SortExpression="Age" /><asp:BoundField DataField="StateName" HeaderText="状态" SortExpression="StateName" /></Columns></asp:GridView>
                        <asp:SqlDataSource ID="SqlTemplate" SelectCommand="SELECT [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc, [I_DUTYID],(select b.RoleTypeName from pt_d_role b where b.RoleTypeCode = (select c.DutyCode from pt_duty c where c.i_dutyId = a.i_dutyId)) as DutyName, [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State],(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end) as StateName FROM [PT_yhmc] a WHERE ([I_DUTYID] = @I_DUTYID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="I_DUTYID" QueryStringField="id" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
	                </div>
                </TD>
            </tr>
            </table>
</form>
</body>
</html>
