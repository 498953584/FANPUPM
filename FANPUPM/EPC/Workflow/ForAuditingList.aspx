<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForAuditingList.aspx.cs" Inherits="ForAuditingList" %>

<html>
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(businessCode,InstanceCode,NoteID,IsAllPass,NodeID,DoWithUrl,BusinessClass)
    {
        document.getElementById('hdnInstanceCode').value = InstanceCode;
        document.getElementById('hdnNoteID').value = NoteID;
        document.getElementById('hdnIsAllPass').value = IsAllPass;
        document.getElementById('hdnNodeID').value = NodeID;
        document.getElementById('HdnBusinessCode').value = businessCode;
        document.getElementById('HdnBusinessClass').value = BusinessClass;
        
//        if (businessCode == "999")
//        {
            if(DoWithUrl != "")
            {
                document.getElementById('btnAudit').disabled = false;
            }
            else
            {
                alert('请填写处理界面Url！');
            }
//            
//        }
//        else
//        {
//             document.getElementById('btnAudit').disabled = true;
//             document.getElementById('BtnNew').disabled = true;
//        }
    }
//    function OpenWin()
//    {
//        var InstanceCode = document.getElementById('hdnInstanceCode').value ;
//        var NoteID = document.getElementById('hdnNoteID').value ;
//        var IsAllPass = document.getElementById('hdnIsAllPass').value ;
//        var NodeID = document.getElementById('hdnNodeID').value ;
//        url = "../../oa/UserDefineFlow/UserDefineFlowAudit.aspx?ic=" + InstanceCode + "&id=" + NoteID + "&pass=" + IsAllPass + "&nid=" + NodeID;
//        var ref = window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:600px;center:1;help:0;status:0;");
//		if(ref)
//		{
//		    return true;
//		}
//		return false;
//    }
    function OpenWin()
    {
        var InstanceCode = document.getElementById('hdnInstanceCode').value ;
        var NoteID = document.getElementById('hdnNoteID').value ;
        var IsAllPass = document.getElementById('hdnIsAllPass').value ;
        var NodeID = document.getElementById('hdnNodeID').value ;
        var bc = document.getElementById('HdnBusinessCode').value;
        var BusinessClass = document.getElementById('HdnBusinessClass').value;
        url = "WorkflowAuditFrame.aspx?ic=" + InstanceCode + "&id=" + NoteID + "&pass=" + IsAllPass + "&nid=" + NodeID+"&bc="+bc+"&bcl="+BusinessClass;        
        var ref = window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:600px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;    
    }
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal" style="table-layout:auto">
            <tr>
                <td height="20px" class="td-title">
                    <asp:Label ID="Label1" runat="server">待审核列表</asp:Label></td>
                    
            </tr> 
                    <tr>
                <td class="td-toolsbar" style="height: 24px">
                        <asp:DropDownList ID="DDLBusinessClass" DataSourceID="SqlBusinessClass" DataTextField="BusinessClassName" DataValueField="BusinessCode" runat="server"></asp:DropDownList>&nbsp; 发起人：<asp:TextBox ID="txtUserCode" Width="90px" runat="server"></asp:TextBox>
                    发起时间:<JWControl:DateBox ID="DBDate" CssClass="text-normal" Width="70px" runat="server"></JWControl:DateBox>
                    流程编号:
                    <asp:TextBox ID="txtTemplateCode" runat="server"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" />&nbsp;<asp:SqlDataSource ID="SqlBusinessClass" SelectCommand="select  (businesscode+businessclass) as BusinessCode,
(select BusinessClassName from WF_Business_Class where businesscode = a.businesscode and businessclass = a.businessclass) as BusinessClassName
 from V_WF_Instance a
where a.Operator = @Operator
group by a.businesscode,a.businessclass" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="Operator" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
            
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAudit" Text="审 核" Enabled="false" OnClick="btnAudit_Click" runat="server" />
                    <input id="hdnInstanceCode" type="hidden" style="width:10px" runat="server" />

                    <input id="hdnNoteID" type="hidden" style="width:10px" runat="server" />

                    <input id="hdnIsAllPass" type="hidden" style="width:10px" runat="server" />

                    <input id="hdnNodeID" type="hidden" style="width:10px" runat="server" />
&nbsp;
                    <asp:HiddenField ID="HdnBusinessCode" runat="server" />
                    <asp:HiddenField ID="HdnBusinessClass" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:GridView ID="gvAuditingList" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlAuditingList" Width="100%" PageSize="23" OnRowDataBound="gvAuditingList_RowDataBound" DataKeyNames="BusinessCode,InstanceCode,NoteID,IsAllPass,NodeID" runat="server">
<EmptyDataTemplate>
                                <table class="grid" cellspacing="0" rules="all" border="1" id="gvAuditingList" style="border-collapse:collapse;">
                                    <tr class="grid_head">
                                        <th scope="col" style="width:40px">序号</th><th scope="col">业务名称</th><th scope="col" style="width:15%">业务子类</th><th scope="col" style="width:15%">流程名称</th><th scope="col" style="width:10%">发起人</th><th scope="col" style="width:10%">发起时间</th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                          
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="BusinessClassName" HeaderText="业务子类" SortExpression="BusinessClassName" /><asp:BoundField DataField="TemplateName" HeaderText="流程名称" SortExpression="TemplateName" /><asp:BoundField DataField="organigerName" HeaderText="发起人" SortExpression="organigerName" /><asp:BoundField DataField="starttime" HeaderText="发起时间" HtmlEncode="false" SortExpression="starttime" /></Columns></asp:GridView>
                        <asp:SqlDataSource ID="SqlAuditingList" SelectCommand="SELECT a.BusinessCode,a.BusinessClass, (SELECT BusinessClassName FROM WF_Business_Class AS d WHERE (BusinessCode = a.BusinessCode) AND (BusinessClass = a.BusinessClass)) AS BusinessClassName, b.NoteID, b.IsAllPass, a.TemplateID, (SELECT TemplateName FROM WF_Template AS c WHERE (TemplateID = a.TemplateID)) AS TemplateName, b.NodeID, b.NodeName, (SELECT v_xm FROM PT_yhmc WHERE (v_yhdm = a.Organiger)) AS organigerName, a.StartTime, a.InstanceCode, dbo.GetBusinessName(a.BusinessCode) AS BusinessName FROM WF_Instance_Main AS a INNER JOIN WF_Instance AS b ON a.ID = b.ID WHERE ((b.Sing = 0) AND (b.Operator = @operator) OR (b.Sing = 0) AND (dbo.GetCommissionMan(a.TemplateID, b.Operator) = @operator) )" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="operator" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
