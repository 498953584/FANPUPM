<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryIPIFrame.aspx.cs" Inherits="HR_Salary_SalaryIPIFrame" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资核算</title>
    <script language="javascript" type="text/javascript">
    function OpenIssuePayInfo(bm)
    {
        var tempid = document.getElementById('hdnTempID').value;
        var year = document.getElementById('HdnYear').value;
        var url = " IssuePayInfoMonth.aspx?temid="+tempid+"&year="+year+"&bm="+bm;
        return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:700px;center:1;help:0;status:0;");
        
    }
    function OpenSalaryIPIEdit(bm)
    {
        var tempid = document.getElementById('hdnTempID').value;
        var year = document.getElementById('HdnYear').value;
        var month = document.getElementById('hdnMonth').value;
        var url = " SalaryIPIEdit.aspx?temid="+tempid+"&year="+year+"&bm="+bm+"&m="+month;
        return window.showModalDialog(url,window,"dialogHeight:450px;dialogWidth:700px;center:1;help:0;status:0;");
    }
    function ClickRow(recordid,as,bm,month)
    {        
    
          document.getElementById('HdnRecoreId').value = recordid; 
          document.getElementById('hdnMonth').value = month;          
         if(as == '-1' || as == '-2')
        {          
          document.getElementById('btnStartWF').disabled = false;  
          document.getElementById('btnAdjust').disabled = false;
          document.getElementById('btnIssuePay').disabled = false;
          document.getElementById('btnViewWF').disabled = true; 
          document.getElementById('btnWFPrint').disabled = true;
        }
        else
        {
          //document.getElementById('btnAdjust').disabled = true;
          document.getElementById('btnStartWF').disabled = true;
          if(as == '1')
          {  
          document.getElementById("btnAdjust").disabled=true;
           document.getElementById('btnIssuePay').disabled = false;
          }
          document.getElementById('btnViewWF').disabled = false; 
          document.getElementById('btnWFPrint').disabled = false;                           
        }
        var tempid = document.getElementById('hdnTempID').value;
        var year = document.getElementById('HdnYear').value;        
        document.getElementById('frmPage').src = "SalaryIPIViewList.aspx?temid="+tempid+"&year="+year+"&bm="+bm+"&m="+month;
    }
    
        ///查看审核
    function OpenViewWF(BusinessCode,BusinessClass)
    {
        var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "/EPC/Workflow/AuditViewWF.aspx?ic="+rid+'&bc='+BusinessCode+'&bcl='+BusinessClass;
      return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		    
    }
    //查看审核记录
    function OpenPrintWF(BusinessCode,BusinessClass)
    {
           var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "/EPC/Workflow/AuditViewPrint.aspx?ic="+rid+'&bc='+BusinessCode+'&bcl='+BusinessClass;
      // window.open(url,"");
     return window.showModalDialog(url,window,"dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");		        
    }
    
    </script>   
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
        <tr>
                <td class="td-toolsbar" style="height: 24px">模板选择 :&nbsp;&nbsp;<asp:DropDownList ID="DDLTempID" DataSourceID="SqlTempID" DataTextField="TempletName" DataValueField="TempletID" AutoPostBack="true" OnSelectedIndexChanged="DDLTempID_SelectedIndexChanged" runat="server"></asp:DropDownList>&nbsp; 年份：<asp:DropDownList ID="DDLYear" AutoPostBack="true" OnSelectedIndexChanged="DDLYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlTempID" SelectCommand="SELECT [TempletID], [TempletName] FROM [HR_Salary_Templet] WHERE ([BeginDate] <> @BeginDate) and UseState= 1" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="1900-01-01" Name="BeginDate" Type="DateTime" /></SelectParameters></asp:SqlDataSource>
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                    <asp:HiddenField ID="HdnYear" runat="server" />
                    <asp:HiddenField ID="hdnTempID" Value="0sss" runat="server" />
                    <asp:HiddenField ID="hdnMonth" runat="server" />                 
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" />&nbsp;
                </td></tr>
                <tr>
                     <td class="td-toolsbar" >
                         <asp:Button ID="btnIssuePayInfo" Text="生成工资表" Width="90px" OnClick="btnIssuePayInfo_Click" runat="server" />
                         <asp:Button ID="btnAdjust" Text="工资调整" Width="90px" Enabled="false" OnClick="btnAdjust_Click" runat="server" />
                         <asp:Button ID="btnStartWF" Text="提交审核" Width="90px" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                         <asp:Button ID="btnIssuePay" Text="工资发放" Width="90px" Enabled="false" OnClick="btnIssuePay_Click" runat="server" />
                         <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />                    
                         <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
                     </td>
                 </tr>
                <tr style="height:50%">
                  <td valign="top"><div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                      <asp:GridView ID="GVIssuePayInfo" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlIssuePayInfo" PageSize="12" Width="100%" OnRowDataBound="GVIssuePayInfo_RowDataBound" DataKeyNames="IssueYear,IssueMonth,CorpCode" runat="server">
<EmptyDataTemplate>
                              <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                  border-collapse: collapse">
                                  <tr class="grid_head">
                                      <th align="center" scope="col" style="width: 60px">
                                          序 号</th>
                                      <th align="center" scope="col">
                                          年份</th>
                                      <th align="center" scope="col">
                                          月份</th>
                                      <th align="center" scope="col">
                                          流程状态</th>
                                      <th align="center" scope="col">
                                          发放状态</th>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序 号" SortExpression="RecordID" /><asp:BoundField DataField="IssueYear" HeaderText="年份" ReadOnly="true" SortExpression="IssueYear" /><asp:BoundField DataField="IssueMonth" HeaderText="月份" ReadOnly="true" SortExpression="IssueMonth" /><asp:BoundField DataField="AuditState" HeaderText="流程状态" SortExpression="AuditState" /><asp:BoundField DataField="IsIssuePay" HeaderText="发放状态" SortExpression="IsIssuePay" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                      <asp:SqlDataSource ID="SqlIssuePayInfo" SelectCommand="SELECT [RecordID], [AuditState], [IssueYear], [IssueMonth], [CorpCode], [ClassCode], [Remark], [UserCode], [IsIssuePay], [TempletID], [RecordDate] FROM [HR_Salary_IssuePayInfo]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                  </div></td>
                </tr>
                <tr style="height:50%">
                <td> <iframe id="frmPage" name="frmPage" src="SalaryIPIViewList.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe> </td>
                </tr>
    </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
