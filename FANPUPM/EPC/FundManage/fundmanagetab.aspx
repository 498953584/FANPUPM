<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fundmanagetab.aspx.cs" Inherits="FundManageTab" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>��������</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">

        <script language="jscript">
		//<iewc:Tab Text="�������"></iewc:Tab>
		//<iewc:TabSeparator></iewc:TabSeparator>
		//<iewc:PageView>
			//<iframe id="fund" name="fund" width="100%" height="100%" frameBorder="0" src="FundApplicationList.aspx?pt=<%=Request["pt"] %>&Type=<%=Request["Type"] %>&pc=<%=Request["pc"] %>&pn=<%=Request["pn"] %>"></iframe>
		//</iewc:PageView>
        </script>
</head>
<body class="body_frame" scroll="no" >
    <form id="Form1" method="post" runat="server">
        <table class="table-normal" id="Tablet" cellspacing="0" cellpadding="0" width="100%"
            border="1" height="100%">
            <tr  valign="top"  >
                <td  style="display:none;" >
                    <iewc:TabStrip ID="TabStrip" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabSelectedStyle="background-image:url(../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" TabHoverStyle="background-color:#777777;" TabDefaultStyle="background-image:url(../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TargetID="MPage" runat="server"><Items><iewc:Tab Text="��������" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;height:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
                </td>
            </tr>
            <tr>
                <td>
                    <iewc:MultiPage ID="MPage" runat="server"><iewc:PageView runat="server">
                            <iframe id="Money" name="Money" width="100%" height="100%" frameborder="0" src="/EPC/Cont/ContractPayExamine.aspx?pt=<%=Request["pt"] %>&pc=<%=Request["pc"] %>&pn=<%=Request["pn"] %>">
                            </iframe>
                        </iewc:PageView></iewc:MultiPage>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
