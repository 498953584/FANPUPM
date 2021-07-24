<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditFrame.aspx.cs" Inherits="EPC_Workflow_AuditFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程审核</title></head>

<frameset rows="200,*" frameborder="1" bordercolor="#cccccc" framespacing="4px;">
		<frame name="frmPage" src="<%=this.infoURL %>" frameborder="0" style="overflow:auto"></frame>
		<frame name="FraCorpList" src="<%=this.auditURL %>" frameborder="0"></frame>		
</frameset>
</html>
