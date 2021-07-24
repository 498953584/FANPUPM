<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemInfoList.aspx.cs" Inherits="oa_System_SystemInfoList" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>制度管理列表</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RecordID,AuditState,title)
    {
        document.getElementById('HdnRecordID').value = RecordID;   
        if(document.getElementById('WF1_FID') !=null )
          document.getElementById('WF1_FID').value = RecordID;
        if(document.getElementById('WF1_BusinessCode') !=null )
          document.getElementById('WF1_BusinessCode').value = "006";
        if(document.getElementById('WF1_BusinessClass') !=null )
          document.getElementById('WF1_BusinessClass').value = "001";
        if(document.getElementById('WF1_PrjGuid') !=null )
          document.getElementById('WF1_PrjGuid').value ="" ;      
	   document.getElementById("WF1_hidcontent").value="制度标题“"+title+"”审核";          //查看审核记录是显示审核内容在这里设置      
  
     if (AuditState == "-1")  //未提交状态
        {
           if(document.getElementById('btnStartWF') !=null )
	         document.getElementById('btnStartWF').disabled = false;     //可提交 
           if(document.getElementById('WF1_btnViewWF') !=null )
	         document.getElementById('WF1_btnViewWF').disabled = true;   //不可查看流程状态
	       if(document.getElementById('WF1_btnWFPrint') !=null )
	         document.getElementById('WF1_btnWFPrint').disabled = true;  //不可查看审核记录
	    }
	  if(AuditState=="-3")   //重报状态
	   {
	      if(document.getElementById('btnStartWF') !=null )
	         document.getElementById('btnStartWF').disabled = false;      //可提交
	      if(document.getElementById('WF1_btnViewWF') !=null )
	         document.getElementById('WF1_btnViewWF').disabled = false;   //可查看流程状态
	      if(document.getElementById('WF1_btnWFPrint') !=null )
	         document.getElementById('WF1_btnWFPrint').disabled = false;  //可查看审核记录   
	   } 
      if(AuditState=="1"||AuditState=="0"||AuditState=="-2")   //以通过或者审核中驳回时
	  {
	      if(document.getElementById('btnStartWF') !=null )
	         document.getElementById('btnStartWF').disabled = true;
	      if(document.getElementById('WF1_btnViewWF') !=null )
	         document.getElementById('WF1_btnViewWF').disabled = false;
	      if(document.getElementById('WF1_btnWFPrint') !=null )
	        document.getElementById('WF1_btnWFPrint').disabled = false;
	  }
	 if(AuditState=="0")//状态为0时才能撤销
	  {
	     document.getElementById("CancelBt").disabled=false;
      }
      else
      {
      document.getElementById("CancelBt").disabled=true;
      }
      
	 if( AuditState=="1"||AuditState=="0"||AuditState=="-3"||AuditState=="-2")   //彻底删除的可用性
	  {
	     if(document.getElementById('WF1_BtnWFDel') !=null )
          document.getElementById('WF1_BtnWFDel').disabled = false;
	  }
	 else
      {
	    if(document.getElementById('WF1_BtnWFDel') !=null )
          document.getElementById('WF1_BtnWFDel').disabled = true;
      }
     if (AuditState == "-1")   //未提交状态可以编辑和删除
      {
        document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
	   
      }
	 else                     //其他状态都不可以编辑和删除 
	  {
	 	document.getElementById('btnEdit').disabled = true;
	    document.getElementById('btnDel').disabled = true;
	  }
      document.getElementById('btnView').disabled = false; 

//        document.getElementById('btnAudit').disabled = true;
    }
    //添加\编辑
    function openEdit(t,cd,ctc)
    {  
        var rid =  document.getElementById('HdnRecordID').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SystemInfoAdd.aspx?t='+t+'&rid=00000000-0000-0000-0000-000000000000&cd='+cd+'&ctc='+ctc;
    	}
    	else
    	{
    	    var url = 'SystemInfoAdd.aspx?t='+t+'&rid='+rid+'&cd='+cd+'&ctc='+ctc;
    	} 	
    	
		return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:500px;center:1;help:0;status:0;");
		
    } 
    ///审核查看
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
    //查看---- 不调用此页面，用openEdit过程同一个页面  08-06-13   ZYG
    function OpenLock()
    {
      var rid =  document.getElementById('HdnRecordID').value ;
      var url = "SystemInfoLock.aspx?ic="+rid;  
      return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:600px;center:1;help:0;status:0;");		        
    }
    //08-07-25 zyg  调用审核
    function OpenAudit()
    {
      var rid =  document.getElementById('HdnRecordID').value ;
      var url = "/HR/Leave/ApplicationReq.aspx?ic="+rid+"&mid=SystemAutio";
      return window.showModalDialog(url,window,"dialogHeight:230px;dialogWidth:390px;center:1;help:0;status:0;");		        
    }
    -->    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
    <div>
    <table id="Tablex" cellspacing="0" cellpadding="0" width="100%"  height="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td height="20px" class="td-title">
                    <asp:Label ID="LBTitle" Text="Label" runat="server"></asp:Label></td>
            </tr>
            <TR>
				<td class="td-search" align="right" style="height: 20px">
                制度名称
                <asp:TextBox ID="txtName" ToolTip="请输入关键字" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" Text="" OnClick="btnSearch_Click" runat="server" />&nbsp;
                </td>
			</TR>
                <tr   >
                <td height="20px" class="td-toolsbar">             
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Enabled="false" Text="修 改" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Enabled="false" Text="删 除" OnClick="btnDel_Click" runat="server" />
                                                <asp:Button ID="BtnView" Enabled="false" Text="查 看" OnClick="BtnView_Click" runat="server" />  
                      <asp:Button ID="btnAudit" Enabled="false" Text="审 核" OnClick="btnAudit_Click" runat="server" />
                    &nbsp; &nbsp;
                <asp:HiddenField ID="hfRecord" runat="server" />
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" runat="server" />
                    </td>
            </tr>  
            <tr>
                <td height=100%>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVInOutMain" AutoGenerateColumns="false" CssClass="grid" Width="100%" AllowPaging="true" OnRowDataBound="GVInOutMain_RowDataBound" OnPageIndexChanging="GVInOutMain_PageIndexChanging" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                 <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                     border-collapse: collapse">
                                     <tr class="grid_head">
                                         <th scope="col" style="width: 40px">
                                             序 号</th>
                                         <th scope="col">
                                             制度名称</th>
                                         <th scope="col" style="width: 70px">
                                             制定日期</th>
                                         <th align="center" scope="col" style="width: 70px">
                                             制定人</th>
                                         <th align="center" scope="col" style="width: 80px">
                                             状态</th>
                                         <th align="center" scope="col" style="width: 70px">
                                             现行制度</th>    
                                         
                                     </tr>
                                 </table>
                             </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序 号"></asp:TemplateField><asp:HyperLinkField DataTextField="SystemName" HeaderText="制度名称" /><asp:BoundField DataField="SignDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="制定日期" HtmlEncode="false" SortExpression="SignDate" /><asp:BoundField DataField="SignMan" HeaderText="制定人" SortExpression="SignMan" /><asp:BoundField DataField="AuditState" HeaderText="状态" SortExpression="AuditState" /><asp:BoundField HeaderText="现行制度" /><asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" Visible="false" /></Columns><PagerStyle CssClass="GD-Pager"></PagerStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlSystemInfo" SelectCommand="SELECT [RecordID], [ClassID], [AuditState], [SystemType], [CorpCode], [SignDate], [SignMan], [SystemCode], [SystemName], [RecordDate], [UserCode],[IsCurrent], [Remark] FROM [OA_System_Info] WHERE ([AuditState]<> 1 and [SystemName] LIKE '%'+ @SystemName +'%') order by SignDate desc" EnableCaching="true" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="ClassID" QueryStringField="cid" Type="Int32"></asp:QueryStringParameter><asp:ControlParameter ControlID="txtName" Name="SystemName" PropertyName="Text" DefaultValue="%" Type="String" runat="server" /></SelectParameters></asp:SqlDataSource>
                        
                     </div>
                </td>
            </tr>
            </table>
    
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>

                    <asp:HiddenField ID="HdnRecordID" runat="server" />                    
    </form>
</body>
</html>
