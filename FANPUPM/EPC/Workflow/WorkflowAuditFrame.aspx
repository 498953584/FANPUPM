<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WorkflowAuditFrame.aspx.cs" Inherits="ModuleSet_Workflow_WorkflowAuditFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程审核</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link href="../../StockManage/Skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
     <script type="text/javascript" src="../../StockManage/script/MultiAffix.js"></script>
     <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script language="javascript" type="text/javascript">
    <!--
        window.name = "win";
        function picksingPerson(op)
		{
			 //var url = "Consignee.aspx";// 
			 var url="/EPC/WorkFlow/SelectAllUser.aspx";
			var human = new Array();	
			human[0]="";
			human[1]="";			
			window.showModalDialog(url,human,"dialogHeight:456px;dialogWidth:600px;center:1;help:0;status:0;");			
			if(op==1)
			{
//			    for (var i=0;i<human.length;i++)
//			    {
				    document.getElementById("txtFrontPerson").value = human[1] ;
				    document.getElementById('hdnFrontPerson').value = human[0] ;
//			    }			   
//			    if(i>400)
//			    {
//			        alert('不能超过四百个人。');
//			        document.getElementById("txtFrontPerson").value ='';
//			        document.getElementById('hdnFrontPerson').value ='';			    
//			    }
			}
			else if(op ==2)
			{
//			    for (var i=0;i<human.length;i++)
//			    {
			        document.getElementById("hdnAnnouncer").value = human[0];
				    document.getElementById('txtAnnouncer').value = human[1] ;
				   // document.getElementById("txtAnnouncer").value += human[i] + ",";
				   // document.getElementById('hdnAnnouncer').value += human[i] + ",";
//			    }
//			    if(i>400)
//			    {
//			        alert('不能超过四百个人。');
//			        document.getElementById("txtAnnouncer").value ='';
//			        document.getElementById('hdnAnnouncer').value ='';			    
//			    }
			}
			else
			{
//			     for (var i=0;i<human.length;i++)
//			    {
				    document.getElementById("txtAfterPerson").value = human[1] ;
				    document.getElementById('hdnAfterPerson').value = human[0] ;
//			    }
//			    if(i>400)
//			    {
//			        alert('不能超过四百个人。');
//			        document.getElementById("txtAfterPerson").value ='';
//			        document.getElementById('hdnAfterPerson').value ='';			    
//			    }
			}					
		}

        function pickPerson(op)
        {
            var p = new Array();
		    p[0] = "";
		    p[1] = "";
		    var url = "";
		    //url = "../../CommonWindow/PickSinglePerson.aspx";
		    url = "/EPC/WorkFlow/SelectUser.aspx";
		    window.showModalDialog(url,p,"dialogHeight:456px;dialogWidth:600px;center:1;help:0;status:0;");
		    if (p[0]!="")
		    {
		        if (op == 1)
		        {
			        document.getElementById('hdnFrontPerson').value = p[0];
			        document.getElementById('txtFrontPerson').value = p[1];
			    }
			    else if(op == 2)
			    {
			        document.getElementById('hdnAnnouncer').value = p[0];
			        document.getElementById('txtAnnouncer').value = p[1];
			    }
			    else if(op==3)
			    {
			       document.getElementById('hdnNextPerson').value = p[0];
			       document.getElementById('txtnextperson').value = p[1];
			    }
			    else
			    {
			        document.getElementById('hdnAfterPerson').value = p[0];
			        document.getElementById('txtAfterPerson').value = p[1];
			    }
			    document.getElementById('btnSubmit').disabled = false;
		    }
        }
        function ckeck(op)
        {
            var nodeId = "";
            nodeId = document.getElementById("hdnNodeID").value;
            if (nodeId != "") //当前节点非前插或后插节点
            {
                if (op == 0) 
                {
                    document.getElementById('btnAfter').style.display = 'none';
                    document.getElementById('trFront').style.display = 'block';                      
                    document.getElementById("btnRet").style.display="";             
	                document.getElementById('trResult').style.display = '';
	                document.getElementById('trAuditInfo').style.display = 'none';
	                document.getElementById('trAnnouncer').style.display = 'none';
	                document.getElementById('trContent').style.display = 'none';
	                document.getElementById('trAfter').style.display = 'none';
	                document.getElementById('trSend').style.display = '';
	                document.getElementById('hdnType').value = '前插';
	                document.getElementById('Lbresult').style.display='none';  //前插审核人时不需要显示
	                
	                document.getElementById('RBLRoleType').style.display = 'none';  //前插审核人时不需要
	                //document.getElementById('btnSubmit').disabled = true;
	                document.getElementById('trUpFiles').style.display = '';
	                document.getElementById('trAuditMain').style.display = 'none';   //前插或者后插审核时不需要显示当前节点审核要点，节点审核要点
	                document.getElementById('trAuditRemark').style.display ='';
	             
	       
	                
                }
                else
                {
                    document.getElementById('btnFront').style.display = 'none';
                    document.getElementById('trFront').style.display = 'none';
                     document.getElementById("btnRet").style.display=""; 
	                document.getElementById('trResult').style.display = '';
	                document.getElementById('trAuditInfo').style.display = 'none';
	                document.getElementById('trAnnouncer').style.display = 'none';
	                document.getElementById('trContent').style.display = 'none';
	                document.getElementById('trAfter').style.display = '';
	                document.getElementById('trSend').style.display = '';
	                document.getElementById('hdnType').value = '后插';
	                 document.getElementById('Lbresult').style.display='';	                
	                document.getElementById('RBLRoleType').style.display = '';  
	                document.getElementById('RBLRoleType').disabled = true;
	               // document.getElementById('btnSubmit').disabled = true;
	                document.getElementById('trUpFiles').style.display = '';
	                document.getElementById('trAuditMain').style.display = 'none';  
	                document.getElementById('trAuditRemark').style.display ='';
	        
                }
            }
        }
        function openAudit(op,BusinessCode,BusinessClass)
        {
            var url = "";
            var instanceCode = document.getElementById('hdnInstanceCode').value
            if (op == 0)
            {
                //url = "FlowStatus.aspx?ic="+instanceCode;
                url = "AuditViewWF.aspx?ic="+instanceCode+'&bc='+BusinessCode+'&bcl='+BusinessClass;
            }
            else
            {
               // url = "AuditStatus.aspx?ic="+instanceCode;
               url = "AuditViewPrint.aspx?ic="+instanceCode+'&bc='+BusinessCode+'&bcl='+BusinessClass;
            }
            window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:800px;center:1;help:0;status:0;");
        }
        //还原
        function ReturnPage()
        {
            document.getElementById("btnFront").style.display="";
            document.getElementById("btnAfter").style.display="";            
            document.getElementById('trFront').style.display = 'none';
            document.getElementById("btnRet").style.display="none"; 
            document.getElementById('trResult').style.display = '';
            document.getElementById('trAuditInfo').style.display = '';
            document.getElementById('trAnnouncer').style.display = '';
            document.getElementById('trContent').style.display = '';
            document.getElementById('trAfter').style.display = 'none';
            document.getElementById('trSend').style.display = '';
            document.getElementById('hdnType').value = '';
            document.getElementById('RBLRoleType').disabled = false;
            document.getElementById('btnSubmit').disabled = false;
            document.getElementById('trUpFiles').style.display = '';
            document.getElementById('trAuditRemark').style.display ='none';
            document.getElementById('trAuditMain').style.display ='';
        }
        //附件上传
    	function UpFile(RecordCode)
        {			
	       // var RecordCode = document.getElementById('hdnNodeID').value;//编号
	        var url = "../../../commonWindow/Annex/AnnexList.aspx?mid=30&rc="+RecordCode+"&at=0";
	        var ref = window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	        return true;
        }
	     //附件下载   
	    function download(filepath,OriginalName)
	    {
	        var url = "/epc/uploadfile/down.asp?fileName=" + escape(OriginalName) + "&filePath=" + escape(filepath) ;	                   
            document.getElementById('fileFrame').src = url;
	    }
	    function setHeight()
        {
             var height=document.getElementById("td-page").clientHeight;
           document.getElementById("frmPage").style.height=height;
           var height=document.getElementById("td-audit").clientHeight;
           document.getElementById("divaudit").style.height=height;
        
        }
        -->
    </script>

</head>
<body onload="setHeight();" scroll="no">
    <form id="form1" runat="server">  
    <table style="height: 100%; width: 100%" class="tableAudit">
        <tr>
            <td valign="top" style="width: 100%; height: 45%; border-bottom:solid 2px #cccccc" id="td-page">
                <iframe id="frmPage" name="frmPage" src="about:blank" frameborder="1" width="100%" height="100px" scrolling="yes" runat="server"></iframe>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right" id="td-audit" style="height:52%">
                <div style="text-align: right; overflow: auto;height:200px" id="divaudit">
                    <table id="tableVersion" cellspacing="0" cellpadding="0" width="100%" border="1" style="height:100%">
                        <tr>
                            <td colspan="4" style="text-align:left">
                                <input type="hidden" id="hdnNodeID" name="hdnNodeID" style="width: 10px" runat="server" />

                                <input type="hidden" id="hdnType" name="hdnType" style="width: 10px" runat="server" />

                                <input type="hidden" id="hdnInstanceCode" name="hdnInstanceCode" style="width: 10px" runat="server" />

                                <input id="btnRet" type="button" value="还原" onclick="ReturnPage();" style="width: 60px;display:none"  />
                                <input id="btnFront" type="button" value="前插审核人" onclick="ckeck(0);" style="width: 80px" runat="server" />

                                <input id="btnAfter" type="button" value="后插审核人" onclick="ckeck(1);" style="width: 80px" runat="server" />

                                <input id="btnFlowStatus" type="button" value="查看流程状态" style="width: 100px" runat="server" />

                                <input id="btnAuditrecord" type="button" value="查看审核记录" style="width: 100px" runat="server" />

                            </td>
                        </tr>
                        <tr id="trResult">
                            <td class="td-label" colspan="4">
                                <asp:Label ID="Lbresult" Text="审核结果" runat="server"></asp:Label>
                                <asp:RadioButtonList ID="RBLRoleType" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Selected="true" Value="1" Text="通过" /><asp:ListItem Value="-2" Text="驳回" /><asp:ListItem Value="-3" Text="重报" /></asp:RadioButtonList>
                                &nbsp; &nbsp; &nbsp;
                                <asp:ImageButton ID="btnSubmit" Style="vertical-align:middle" Text="确 定" ImageUrl="~/images/auditimg.jpg" OnClick="btnSubmit_Click" runat="server" />
                               
                                &nbsp; &nbsp; &nbsp; 审核时限&nbsp; &nbsp; &nbsp;<asp:Label ID="LbDuring" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;小时 &nbsp; &nbsp; &nbsp;
                                <asp:Label ID="LbDuringInfo" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trFront" style="display: none">
                            <td class="td-label" id="tdFront">
                                选择前插人员
                            </td>
                            <td class="td-content">
                                <span class="spanSelect" style="width: 180px; float:left">
                                    <asp:TextBox ID="txtFrontPerson" Style="width: 160px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 2px;" runat="server"></asp:TextBox>                                        
                                    <img alt="选择人员" onclick="pickPerson(1);" src="/images1/iconSelect.gif" />
                                    <input type="hidden" id="hdnFrontPerson" name="hdnFrontPerson" style="width: 10px" runat="server" />

                                </span>&nbsp;&nbsp;
                                <img id="img1" src="../../images/help.jpg" alt="" title="前插审核人时，当前无需审核" style="vertical-align:middle" />                           
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFrontPerson" Enabled="false" Display="None" ErrorMessage="请选择前插人员！" runat="server"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trAfter" style="display: none">
                            <td class="td-label">
                                选择后插人员
                            </td>
                            <td colspan="3" class="td-content">
                                <span class="spanSelect" style="width:180px; float:left">
                                    <asp:TextBox ID="txtAfterPerson" Style="width: 160px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 2px;" runat="server"></asp:TextBox>
                                    <img alt="选择人员" onclick="pickPerson(4);" src="/images1/iconSelect.gif" />
                                    <input type="hidden" id="hdnAfterPerson" name="hdnAnnouncer" style="width: 10px" runat="server" />

                                </span>&nbsp;&nbsp;
                                   <img id="img2" src="../../images/help.jpg" alt="" title='1、“后插审核人”，会改变本次流程正常的审核流向；<br />2、选择“后插审核人”，系统默认流程状态是通过状态。' style="vertical-align:middle" />  

                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAfterPerson" Enabled="false" Display="None" ErrorMessage="请选择后插人员！" runat="server"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="tr_selector" style="display: none" runat="server"><td class="td-label" runat="server">
                                请选择流程下一个审核人
                            </td><td class="td-content" runat="server">
                                <span class="spanSelect" style="width: 180px">
                                    <asp:TextBox ID="txtnextperson" Style="width: 160px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 2px;" runat="server"></asp:TextBox>
                                    <img alt="选择人员" onclick="pickPerson(3);" src="/images1/iconSelect.gif" />
                                    <input type="hidden" id="hdnNextPerson" name="hdnNextPerson" style="width: 10px" runat="server" />

                                </span>
                            </td></tr>
                        <tr id="trPass" visible="false" runat="server"><td class="td-label" runat="server">
                                <asp:Label ID="LbAduPass" Text="二次验证密码" runat="server"></asp:Label>
                            </td><td colspan="3" class="td-content" runat="server">
                                <asp:TextBox ID="txtAuditPwd" TextMode="Password" Width="180px" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAuditPwd" Display="None" ErrorMessage="请填写二次验证密码！" runat="server"></asp:RequiredFieldValidator>
                            </td></tr>
                        <tr id="trAuditMain" style="display:block">
                            <td class="td-label">
                                审核要点
                            </td>
                            <td colspan="3" class="td-content">
                            <div id="divAuditMain" runat="server"></div>
                           
                            </td>
                        </tr>
                        <tr id="trAuditRemark" style="display: none">
                            <td class="td-label">
                                审核要点                     
                                 <img id="img3" src="../../images/help.jpg" alt="" title='1、	由于是临时插入审核人，改变了流程正常的审核流向；一般情况下，需要对临时插入的审核人说明该流程的“审核要点”；<br />2、	“审核要点”仅对该插入的审核人有效；<br />3、	“审核要点”不是必填项。' style="vertical-align:middle" />                           

                            </td>                           
                            <td colspan="3" class="td-content" style="padding-top:2px; height:110px">
                              <textarea id="txtAuditRemark" class="wysiwyg" cols="70" rows="3" runat="server"></textarea>
                            </td>                           
                        </tr>
                        <tr id="trAuditInfo">
                            <td class="td-label">
                                审核意见
                            </td>
                            <td colspan="3" class="td-content" style="padding-top:2px; height:120px">
                              <textarea id="txtAuditInfo" class="wysiwyg" cols="80" runat="server"></textarea>
                            </td>
                        </tr>
                        <tr id="trUpFiles">
                            <td class="td-label">
                                相关附件
                            </td>
                     
                            <td colspan="3" class="td-content" style="padding-top:3px">
                          
                            </td>
                        </tr>
                        <tr id="trAnnouncer">
                            <td class="td-label">
                                告知人
                            </td>
                            <td colspan="3" class="td-content">
                                <span class="spanSelect" style="width: 320px">
                                    <asp:TextBox ID="txtAnnouncer" Style="width: 300px; height: 15px; border: none; line-height: 16px;
                                        margin: 1px 2px;" runat="server"></asp:TextBox>
                                    <img alt="选择人员" onclick="picksingPerson(2);" src="/images1/iconSelect.gif" />
                                    <input type="hidden" id="hdnAnnouncer" name="hdnAnnouncer" style="width: 10px" runat="server" />

                                </span>
                            </td>
                        </tr>
                        <tr id="trContent">
                            <td class="td-label">
                                告知内容
                            </td>
                            <td colspan="3" class="td-content" style="padding-top:3px; height:120px">
                                <textarea id="txtContent" class="wysiwyg" cols="80" runat="server"></textarea>                                
                            </td>
                        </tr>
                        <tr id="trSend">
                            <td >
                            </td>
                            <td colspan="3" class="td-content">
                                <asp:CheckBox ID="ckbIsSendMsg" Text="短信通知下一个审核人" runat="server" />&nbsp;
                                <asp:CheckBox ID="ckbIsSendInfo" Text="发送消息给通知下一个审核人" Checked="true" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="../../StockManage/script/jquery.wysiwyg.js"></script>
    <script type="text/javascript">
(function($)
{
  $('.wysiwyg').wysiwyg({
    controls: {
      strikeThrough : { visible : true },
      underline     : { visible : true },
      
      separator00 : { visible : true },
      
      justifyLeft   : { visible : true },
      justifyCenter : { visible : true },
      justifyRight  : { visible : true },
      justifyFull   : { visible : true },
      
      separator01 : { visible : true },
      
      indent  : { visible : true },
      outdent : { visible : true },
      
      separator02 : { visible : true },
      
      subscript   : { visible : true },
      superscript : { visible : true },
      
      separator03 : { visible : true },
      
      undo : { visible : false },
      redo : { visible : false },
      
      separator04 : { visible : false },
      
      insertOrderedList    : { visible : true },
      insertUnorderedList  : { visible : true },
      insertHorizontalRule : { visible : true },
      
      h4mozilla : { visible : false && $.browser.mozilla, className : 'h4', command : 'heading', arguments : ['h4'], tags : ['h4'], tooltip : "Header 4" },
      h5mozilla : { visible : false && $.browser.mozilla, className : 'h5', command : 'heading', arguments : ['h5'], tags : ['h5'], tooltip : "Header 5" },
      h6mozilla : { visible : false && $.browser.mozilla, className : 'h6', command : 'heading', arguments : ['h6'], tags : ['h6'], tooltip : "Header 6" },
      
      h4 : { visible : false  && !( $.browser.mozilla ), className : 'h4', command : 'formatBlock', arguments : ['<H4>'], tags : ['h4'], tooltip : "Header 4" },
      h5 : { visible : false && !( $.browser.mozilla ), className : 'h5', command : 'formatBlock', arguments : ['<H5>'], tags : ['h5'], tooltip : "Header 5" },
      h6 : { visible : false && !( $.browser.mozilla ), className : 'h6', command : 'formatBlock', arguments : ['<H6>'], tags : ['h6'], tooltip : "Header 6" },
      
      separator05 : { visible : false },
      separator06 : { visible : false },      
      separator07 : { visible : false },
      
      cut   : { visible : false },
      copy  : { visible : false },
      insertImage:{visible:false},
      paste : { visible : false }
    }
  });
})(jQuery);
    </script>
      <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
 
    <script type="text/javascript">  
    $(document).ready(function(){
        $("#img1").tooltip({showURL:false});
        $("#img2").tooltip({showURL:false});
        $("#img3").tooltip({showURL:false});
    });
    </script>
    <iframe src="" id="fileFrame" height="0px" width="0px"></iframe>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    
    </form>
    </body>
</html>
