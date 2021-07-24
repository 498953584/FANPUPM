<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tasklist.aspx.cs" Inherits="TaskList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>�����б�</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self"/>

        <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>

        <script language="javascript" type="text/javascript">
			function clickRow(obj)
			{
				/*����֮ǰ������Ĵ������*/
				doClick(obj,'TblSchedule');//������ʽ����
				
				
			}
            function clickonerow(obj,theCode,theName,projectCode,projectName,w)
			{
				if (w=3)
				{
					var task = parent.window.dialogArguments;
					task[0] = theCode;
					task[1]	= theName;
					task[2] = projectCode;
					task[3] = projectName;
					
				}
				
				
			}
			function dbClickRow(obj,theCode,theName,projectCode,projectName,w)
			{
				if (w=3)
				{
				    
					var task = parent.window.dialogArguments;
					task[0] = theCode;
					task[1]	= theName;
					task[2] = projectCode;
					task[3] = projectName;
					window.close();
				}
			}
		
			function switchDisplay(obj,t1,t2)
			{
				/*����֮ǰ������Ĵ������*/
				doSwitchDisplay(obj,'TblSchedule','HdnScheduleCodeList',t1,t2,'../../');//������ʽ����
			}
			function setnull()
			{
			    var task = parent.window.dialogArguments;
				task[0] = "";
				task[1]	= "";
				task[2] = "";
				task[3] = "";
			}
			
        </script>
</head>
<body class="body_clear" scroll="no">
    <form id="Form1" method="post" runat="server">
        <table style="border-collapse: collapse" height="100%" cellspacing="0" cellpadding="0"
            width="100%" border="1">
            <tr>
                <td bgcolor="#666666" height="24">
                    <asp:Label ID="LblProject" Font-Bold="true" ForeColor="White" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td valign="top" height=88%>
                    <div id="dvScheduleBox" class="gridBox">
                        <asp:Table ID="TblSchedule" CssClass="grid" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Width="100%" runat="server"></asp:Table>
                    </div>
                </td>
            </tr>
            <tr class="td-submit">
                <td align="right">
                    <input id="btnSave" type="button" value="ȷ ��" onclick="window.close();"  />
                    <input id="btnCancel" type="button" value="ȡ ��" onclick="setnull();window.close();" />&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <input id="HdnScheduleCodeList" style="width: 10px" type="hidden" name="HdnScheduleCodeList" runat="server" />

        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
