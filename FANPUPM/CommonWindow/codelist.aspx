<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codelist.aspx.cs" Inherits="CodeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>选择列表</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
    </base>

    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

    <script language="javascript">
			function dbClickResRow(rn,rc)
			{
				var res = parent.window.dialogArguments;
				res[0] = rn;
				res[1] = rc;
				window.parent.close();
			}
			function ClickRow(rn,rc)
			{
			    var res = parent.window.dialogArguments;
				res[0] = rn;
				res[1] = rc;
			}
			function setnull()
			{
			    var res = parent.window.dialogArguments;
				res[0] = "";
				res[1] = "";
			}
    </script>

</head>
<body class="body_frame" scroll="yes">
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="1" height="100%">
                <tr>
                    <td>
                        <asp:Label ID="Lb" runat="server"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>
                        <div style="OVERFLOW">
                            <asp:DataGrid ID="DG" Width="100%" CssClass="grid" AutoGenerateColumns="false" DataKeyField="CodeID" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn Visible="false" DataField="CodeID"></asp:BoundColumn><asp:BoundColumn DataField="CodeName" HeaderText="请选择"></asp:BoundColumn></Columns></asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr height="20" valign="bottom" align="right" class="td-submit">
                    <td>
                        <input id="btnSave" type="button" value="确 定" onclick="window.close();" />
                        <input id="btnCancel" type="button" value="取 消" onclick="setnull();window.close();" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>
