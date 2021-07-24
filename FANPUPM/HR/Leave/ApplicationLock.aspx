<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationLock.aspx.cs" Inherits="HR_Leave_ApplicationLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>请假查看</title>
     <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
       -->
    </script>
</head>
<body  class="body_clear" scroll="no" >
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="2">请假申请</td>
      </tr>
       <tr>
	    <td class="td-label" style="width:25%">
            流程发起人:</td>
	    <td>
            <asp:Label ID="LbUserName" runat="server"></asp:Label></td>
	   </tr>
	    <tr>
	    <td class="td-label" style="width:25%">
            请(休)假类型:</td>
	    <td>
            <asp:Label ID="LbLeaveType" runat="server"></asp:Label></td>
	   </tr>
	    
	    <tr> 
	        <td class="td-label" >
		        请休假开始时间:</td>
	        <td  align="left">
                <asp:Label ID="LbBeginDate" runat="server"></asp:Label></td>
	    </tr>
	    <tr> 
	        <td class="td-label" >
		        销假时间:</td>
	        <td  align="left">
                <asp:Label ID="LbBackDate" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td class="td-label" >
		        请假天数:</td>
	        <td>
		        <asp:Label ID="LbDays" Width="60px" CssClass="text-normal" runat="server"></asp:Label>天</td>
	    </tr>
	     <tr>
	        <td class="td-label" >
		        实际离开天数:</td>
	        <td>
		        <asp:Label ID="LbLeaveDays" Width="60px" CssClass="text-normal" runat="server"></asp:Label>天</td>
	    </tr>
	    <tr>
	        <td class="td-label">
		        请假事由:</td>
	        <td >
		        <asp:Label ID="LbReason" Width="90%" runat="server"></asp:Label>
	        </td>
	    </tr>
	 
</table>
    </div>
    </form>
</body>
</html>
