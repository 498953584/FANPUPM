<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costinputtop.aspx.cs" Inherits="CostInputTop" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>CostInputTop</title>
  
  <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

    <script type="text/javascript">
			function OnClickGridItem(CostID,RecordID,AuditState,PrjName)
			{
			   //alert(isAudit);
				document.getElementById('CostID').value=CostID;
				document.getElementById('RecordID').value = RecordID;
				
		       if(document.getElementById('WF1_FID') !=null )
                   document.getElementById('WF1_FID').value = RecordID;
                if(document.getElementById('WF1_BusinessCode') !=null )
                   document.getElementById('WF1_BusinessCode').value = "018";
                if(document.getElementById('WF1_BusinessClass') !=null )
                  document.getElementById('WF1_BusinessClass').value = "001";
                   if(document.getElementById('WF1_PrjGuid') !=null )
                document.getElementById('WF1_PrjGuid').value ="" ;
                document.getElementById("WF1_hidcontent").value="项目"+PrjName+"的其他成本审核";          //查看审核记录是显示审核内容在这里设置
              
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
			        
			        
			        
		    if (AuditState=="-1")
		    {
			    if(document.getElementById('btn_Fix') != null)
			    document.getElementById('btn_Fix').disabled = false;
			    if(document.getElementById('btn_Del') != null)
			    document.getElementById('btn_Del').disabled = false;
			    if(document.getElementById('BtnAudit') != null)
			    document.getElementById("BtnAudit").disabled = false;
			    if(document.getElementById('butOK') != null)
			    document.getElementById("butOK").disabled=false;
		    }
		    else
		    {
			    if(document.getElementById('btn_Fix') != null)
			    document.getElementById('btn_Fix').disabled = true;
			    if(document.getElementById('btn_Del') != null)
			    document.getElementById('btn_Del').disabled = true;
			    if(document.getElementById('BtnAudit') != null)
			    document.getElementById("BtnAudit").disabled = true;
			    if(document.getElementById('butOK') != null)
			    document.getElementById("butOK").disabled=true;
		    }
		    <%if (base.Request["ic"] != null)
{%>		
			    var url = 'CostInputBottom.aspx?isAudit='+AuditState+'&RecordID='+RecordID+'&Type=<%=Request["Type"].ToString() %>'+"&PrjCode=";
			    document.getElementById("FraFlow").src = url;
		    <%}
else
{%>
		        var url = 'CostInputBottom.aspx?isAudit='+AuditState+'&RecordID='+RecordID+'&Type=<%=Request["Type"].ToString() %>'+"&PrjCode="+document.getElementById('PrjCode').value;
		        parent.bottomFrame.location.href=url;
		    <%}%>
				
				
			}
			
			function OpType(opType)	//按钮事件
			{
				var PrjCode	=document.getElementById('PrjCode').value; 	
				var CostID = document.getElementById('CostID').value;
				var RecordID = document.getElementById('RecordID').value;

				var url;
				switch(opType)
				{
				  case 'add':
					url = "CostInputEdit.aspx?opType="+opType+"&PrjCode="+PrjCode+"&RecordID=";
					return window.showModalDialog(url,window,'dialogHeight:600px;dialogWidth:700px;center:1;help:0;status:0;');		
					window.location.href=window.location.href;
				  break;
				   
				  case 'edit':
						url = "CostInputEdit.aspx?opType="+opType+"&PrjCode="+PrjCode+"&RecordID="+RecordID;
						return window.showModalDialog(url,window,'dialogHeight:600px;dialogWidth:700px;center:1;help:0;status:0;');
						window.location.href=window.location.href;
				  break;
				  
				  case 'Judge':
						if(CostID =="")
						{window.alert("请选择项目!");}
						else
						{
						url ="CostJudge.aspx?opType="+opType+"&RecordID="+RecordID;
						return window.showModalDialog(url,window,'dialogHeight:250px;dialogWidth:460px;center:1;help:0;status:0;');
						//window.location.href = window.location.href;
						}
				  break;					
				}
			}
			
			function hiddenTr()		//隐藏审核
			{
				TrJudge.style.display="none";
				TrBtn.style.display="block";
			}
			function displayTr()
			{
				TrJudge.style.display="block";
				TrBtn.style.display="none";
			}
			 //费用审核
            function OpenAudit()
            {
              var rid =  document.getElementById('RecordID').value ;
              var url = "/HR/Leave/ApplicationReq.aspx?ic="+rid+"&mid=Cost";
              return window.showModalDialog(url,window,"dialogHeight:230px;dialogWidth:390px;center:1;help:0;status:0;");		        
            }
    </script>

</head>
<body class="body_frame" scroll="no" onload="selrow1('dgCostInputPri')">
    <form id="Form1" method="post" runat="server">
        <table class="table-normal" id="Table1" height="100%" width="100%" border="1">
            <tr>
                <td class="td-Title" align="center" colspan="3">
                    <input id="CostID" type="hidden" size="1" name="CostID" runat="server" />

                    <input id="RecordID" type="hidden" size="1" name="RecordID" runat="server" />
<input id="PrjCode" type="hidden" size="1" name="PrjCode" runat="server" />
其它成本信息</td>
            </tr>
            <tr id="TrBtn">
                <td class="td-toolsbar">
                    <asp:Button ID="btn_Add" Enabled="false" Text="新 增" OnClick="btn_Add_Click" runat="server" />&nbsp;<asp:Button ID="btn_Fix" Enabled="false" Text="编 辑" OnClick="btn_Fix_Click" runat="server" />&nbsp;<asp:Button ID="btn_Del" Enabled="false" Text="删 除" OnClick="btn_Del_Click" runat="server" />&nbsp;<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" runat="server" />
                </td>
            </tr>
            <tr id="TrJudge" style="display: none">
                <td class="td-search">
                    <asp:Button ID="btn_Judge" Text="审 核" Width="0px" OnClick="btn_Judge_Click" runat="server" />&nbsp;&nbsp;&nbsp; 发生单位:<asp:TextBox ID="txt_HappenUnit" runat="server"></asp:TextBox>
                    &nbsp;填报人:<asp:TextBox ID="txt_FillPeople" CssClass="text-input" runat="server"></asp:TextBox>
                    &nbsp;<asp:Button ID="btn_Search" CssClass="button-normal" OnClick="btn_Search_Click" runat="server" />
                    <asp:Button ID="butOK" Text="审 核" Enabled="false" CssClass="button-normal" OnClick="butOK_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 100%">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="dgCostInputPri" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="12" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="ID" HeaderText="成本录入序号"></asp:BoundColumn><asp:TemplateColumn HeaderText="费用项目">
<ItemTemplate>
                                        <%# Eval("CostItemName") %>
                                        <input id="hidState" type="hidden" value='<%# Convert.ToString(Eval( "AuditResult")) %>' runat="server" />

                                    </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="HappenUnit" HeaderText="发生单位"></asp:BoundColumn><asp:BoundColumn DataField="HappenDate" HeaderText="发生时间" DataFormatString="{0:d}"></asp:BoundColumn><asp:BoundColumn DataField="FillPeople" HeaderText="填报人"></asp:BoundColumn><asp:BoundColumn DataField="TouchManName" HeaderText="经手人"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="auditdate" HeaderText="审核时间" DataFormatString="{0:d}"></asp:BoundColumn><asp:BoundColumn HeaderText="流程状态"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="AuditResult" HeaderText="流程状态bit"></asp:BoundColumn><asp:BoundColumn DataField="hj" HeaderText="合计" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="RecordID" HeaderText="RecordID"></asp:BoundColumn><asp:BoundColumn DataField="PrjName" HeaderText="所属项目"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
                </td>
            </tr>

            <tr >
                <td valign="top" colspan="2">
                    <iframe id="FraFlow" name="FraFlow" src="/ModuleSet/Workflow/FlowList.aspx?ic=00000000-0000-0000-0000-000000000000&pt=1" frameborder="no" width="100%" height="0%" runat="server"></iframe>
                </td>
            </tr>
            <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
        </table>
    </form>
</body>
</html>
