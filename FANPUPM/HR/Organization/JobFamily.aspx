<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobFamily.aspx.cs" Inherits="HR_Organization_JobFamily" %>

<HTML>
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RoleTypeCode,ChildNum)
	{
		document.getElementById('btnEdit').disabled = false;
		if(parseInt(ChildNum) > 0)
		{
		    document.getElementById('btnDel').disabled = true;
		}
		else
		{
		    document.getElementById('btnDel').disabled = false;
		}
		if(RoleTypeCode.length >= 6)
		{
		    document.getElementById('btnAdd').disabled = true;
		}
		else
		{
		    document.getElementById('btnAdd').disabled = false;
		}
		document.getElementById('HdnRoleTypeCode').value = RoleTypeCode;
		if(RoleTypeCode.length == 6)
		{
		    frmLibrary.location.href ="JobFamilyThird.aspx?f=false&cc="+RoleTypeCode;
		}
	}
	function OpenWin(op)
	{
	    var RoleTypeCode = document.getElementById('HdnRoleTypeCode').value;
		var url= "JobFamilyEdit.aspx?t=" + op + "&cc=" + RoleTypeCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:160px;dialogWidth:350px;center:1;help:0;status:0;");
		document.getElementById('HdnRoleTypeCode').value = "";
		if(ref)
		{
		    return true;
		}
		return false;
	}
	-->
    </script>
    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
</head>
<body class="body_clear" scroll="no"  onload = "selrow1('GridView1')">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal" style="table-layout:auto">
            <tr>
                <td height="20px" class="td-title">
                        岗位分类
                    <asp:ScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                        </ContentTemplate>
</asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td HEIGHT="50%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
<ContentTemplate>
                    <input id="HdnRoleTypeCode" type="hidden" style="width:20px" runat="server" />

                    <asp:GridView CssClass="grid" ID="GridView1" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="0px" CellPadding="0" Width="100%" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        编号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        分类名称</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="RoleTypeCode" HeaderText="编号" HtmlEncode="false" /><asp:BoundField HeaderText="分类名称" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerStyle CssClass="GD-Pager"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                            </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td HEIGHT="50%" valign="top">
                    <iframe id="frmLibrary" name="frmLibrary" src="JobFamilyThird.aspx?cc=&f=true" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
