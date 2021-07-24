<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eFileLock.aspx.cs" Inherits="oa_eFile_eFileLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>电子档案查看</title>
    <script type="text/javascript" language="javascript">
	<!--
	 window.name = "win";
	 function download(url)
	{       
        window.location.href = url;
	}
	-->
	</script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
                <td class="td-head" colspan="4">档案管理</td>
            </tr>	   
             <tr>
	       <td class="td-label">
		        流程发起人
	        </td>
	        <td >
		        <asp:Label ID="LbUserName" Width="200px" runat="server"></asp:Label>&nbsp;
	        </td>
	        </tr>  
	    <tr>
	     <td class="td-label">
		        档案编号
	        </td>
	        <td >
               <asp:Label ID="LbFileCode" Width="200px" MaxLength="15" runat="server"></asp:Label>
	        </td>	     
	        <td class="td-label">
		        文档名称
	        </td>
	        <td >
		        <asp:Label ID="LbFileTitle" Width="200px" MaxLength="100" runat="server"></asp:Label>
	        </td>
	       
	      </tr>
	      <tr>
	       <td class="td-label">
		        提交人
	        </td>
	        <td >
		        <asp:Label ID="LbSubmitMan" Width="200px" runat="server"></asp:Label>&nbsp;
	        </td>
	         <td class="td-label">
                文件类型</td>
	        <td >
               <asp:Label ID="LbFileType" runat="server"></asp:Label></td>
	      </tr>
	      <tr>
	       
	        <td class="td-label">
                文件稿本</td>
	        <td >
                <asp:Label ID="LbFileCopy" runat="server"></asp:Label></td>
                 <td class="td-label">
		       密级
	        </td>
	        <td >
               <asp:Label ID="LbSecretLevel" runat="server"></asp:Label></td>
	       </tr>
	    <tr>	       
                <td class="td-label">
		        保存期限
	        </td>
	        <td>
		        <asp:Label ID="LbSaveLimit" Width="200px" runat="server"></asp:Label>
	        </td>
	     
	 
	        <td class="td-label">
                </td>
	        
            
	     </tr>
	      <tr>	       
                <td class="td-label">
		        借阅时间
	        </td>
	        <td>
		        <asp:Label ID="LbLendDate" Width="200px" runat="server"></asp:Label>
	        </td>
	     
	 
	        <td class="td-label">计划归还时间
                </td>
	        <td colspan="3" >
                <asp:Label ID="LbPlanReturnDate" runat="server"></asp:Label></td>
            
	     </tr>
	    <tr>
	       <td class="td-label">
		        借阅人
	        </td>
	        <td colspan="3">
		        <asp:Label ID="LbBorrowMan" Width="80%" runat="server"></asp:Label>
	        </td>
	    </tr>
			</table>
    </div>
    </form>
</body>
</html>
