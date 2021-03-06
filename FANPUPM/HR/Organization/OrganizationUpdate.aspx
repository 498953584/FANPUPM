<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationUpdate.aspx.cs" Inherits="HR_Organization_OrganizationUpdate" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html>
<head id="Head1" runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RecordID,AuditState)
	{
	    var intAuditState = parseInt(AuditState);
	    if(intAuditState > -1)
	    {
	        document.getElementById('btnStartWF').disabled = true;
	        document.getElementById('btnEdit').disabled = true;
		    document.getElementById('btnDel').disabled = true;
	    }
	    else
	    {
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnStartWF').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		}
		 if(AuditState == '-1')
      {
         document.getElementById('btnViewWF').disabled = true; 
         document.getElementById('btnWFPrint').disabled = true;
      }
      else
      {
        document.getElementById('btnViewWF').disabled = false;
        document.getElementById('btnWFPrint').disabled = false;       
      }
		document.getElementById('HdnRecordID').value = RecordID;
		document.getElementById('btnView').disabled = false;
	}
	function OpenWin(op)
	{
	    var RecordID = "";
	    if(op != 'add')
	    {
	        RecordID = document.getElementById('HdnRecordID').value;
	    }
	    var dc = document.getElementById('HdnDeptCode').value;
		var url= "OrganizationUpdateEdit.aspx?t=" + op + "&rid=" + RecordID +"&dc="+ dc;
		var ref = window.showModalDialog(url,window,"dialogHeight:270px;dialogWidth:560px;center:1;help:0;status:0;");
		if(ref)
	    {
		   return true;
		}
		return false;
	}
	     ///查看审核
    function OpenViewWF()
    {
        var rid =  document.getElementById('HdnRecordID').value ;
      var url = "/ModuleSet/Workflow/AuditViewWF.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		    
    }
      //查看审核记录
    function OpenPrintWF()
    {
           var rid =  document.getElementById('HdnRecordID').value ;
      var url = "/ModuleSet/Workflow/AuditViewPrint.aspx?ic="+rid;
      // window.open(url,"");
     return window.showModalDialog(url,window,"dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");		        
    }
        //查看
    function OpenLock()
    {
      var rid =  document.getElementById('HdnRecordID').value ;
      var url = "OrganizationUpdateLock.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:600px;center:1;help:0;status:0;");		        
    }
    
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
                    <asp:ScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td rowspan="3" valign="top" width="20%">
                    <asp:TreeView ID="TVDept" ShowLines="true" OnSelectedNodeChanged="TVDept_SelectedNodeChanged" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>
                </td>
                <td height="20px" class="td-title">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                            <asp:Label ID="LblMsg" runat="server"></asp:Label>&nbsp;组织机构变动申请记录
                        </ContentTemplate>
</asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td height="20px" valign="middle" class="td-toolsbar">
                    <asp:UpdatePanel ID="UpdatePanelBtn" runat="server">
<ContentTemplate>
                            <asp:Button ID="btnAdd" Text="新 增" Enabled="false" OnClick="btnAdd_Click" runat="server" />
                            <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                            <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                            <asp:Button ID="btnStartWF" Text="提交审核" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                            <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
                            <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
                            <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />
                            <input id="HdnRecordID" style="width: 20px" type="hidden" runat="server" />

                            <input id="HdnDeptCode" style="width: 20px" type="hidden" runat="server" />

                            
                            <input id="HdnDeptName" style="width: 20px" type="hidden" runat="server" />

                        </ContentTemplate>
</asp:UpdatePanel>
                 </td>
            </tr>
            <tr>
                <td height="900" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
<ContentTemplate>
                        <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        调整时间</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        申请单位</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        调整内容</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        调整原因</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        状态</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="RecordDate" HeaderText="调整时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="申请单位" HtmlEncode="false" /><asp:BoundField DataField="AdjustContent" HeaderText="调整内容" HtmlEncode="false" /><asp:BoundField DataField="AdjustReason" HeaderText="调整原因" HtmlEncode="false" /><asp:BoundField HeaderText="状态" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [HR_Org_Adjust] WHERE ([CorpCode] = @CorpCode) ORDER BY [RecordDate]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="TVDept" Name="CorpCode" PropertyName="SelectedValue" Type="String" runat="server" /></SelectParameters></asp:SqlDataSource>
                        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnStartWF" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="TVDept" EventName="SelectedNodeChanged" runat="server" /></Triggers></asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
