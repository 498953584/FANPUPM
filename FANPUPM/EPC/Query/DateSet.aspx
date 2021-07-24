<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DateSet.aspx.cs" Inherits="EPC_Query_DateSet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>选择日期</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
        function dbclose()
			{
				var strdate = parent.window.dialogArguments;
				strdate[0] = document.getElementById('Txtdate').value;
				window.close();
				//alert(strdate[0]); 
			}
	</script>		
</head>
<body>
    <form id="form1" runat="server">


        <table>
            <tr>
                <td colspan=2>
                <div>
                    <asp:Calendar ID="Calendar1" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Size="12px" ForeColor="Black" Height="200px" NextPrevFormat="FullMonth" Width="280px" OnSelectionChanged="Calendar1_SelectionChanged" OnUnload="Calendar1_Unload" Font-Names="Verdana" runat="server"></asp:Calendar>
                
                </div>                
                </td>

            </tr>
            <tr>

                <td class="td-submit"  colspan=2 align="right"> 
                <asp:TextBox ID="Txtdate" Width="0px" BorderStyle="None" runat="server"></asp:TextBox>
                   <input type="button" value="确  定" onclick="dbclose();">
                    <input type="button" value="关  闭" onclick="window.close();">
                </td>
            </tr>

        </table>
      
    </form>
</body>
</html>
