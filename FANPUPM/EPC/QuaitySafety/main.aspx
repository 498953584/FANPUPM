<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="Main" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>规范类型管理</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
</head>
<frameset class="body_frame" frameborder="1" bordercolor="#CADEED" framespacing="2"
    cols="22%,78%">
		<FRAME name=lframe src="<%=this.strTree %>" width="100%" scrolling=no frameborder=1 height="100%">
		<FRAME name="InfoList" src="<%=this.strRur %>" frameborder=0>
	</frameset>
</html>
