<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consignee3.aspx.cs" Inherits="oa_MailAdmin_consignee3" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>人员选择</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />


    <script language="JavaScript" type="text/JavaScript">
        function confirmHuman() {
            var humanList = FraHuman.Form1.LBoxSelected;
            var human = window.dialogArguments;
            for (var i = 0; i < humanList.length; i++) {
                //human[0][i] = humanList.options[i].value;
                human[i] = humanList.options[i].text;
            }
            window.returnvalue = human;
            window.close();
        }
    </script>

</head>
<body leftmargin="0" topmargin="0" scroll="no" rightmargin="0" onunload="confirmHuman()">
    <form id="Form1" method="post" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%" style="background-color: #EBE9EE;">
        <tr valign="top">
            <td colspan="3">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr style="margin-top: 13px;">
                        <td>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td style="width: 35px;">
                                    </td>
                                    <td style="width: 200px;">
                                        <table height="378" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td height="27">
                                                    部门：<input id="HdnSysID" style="width: 1px; height: 1px" type="hidden" name="HdnSysID" runat="server" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                    <iframe id="FraDept" name="FraDept" src="deptinfo.aspx?ht=3" width="100%" scrolling="auto" height="370" runat="server"></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 10px;">
                                    </td>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="397" border="0">
                                            <tr>
                                                <td>
                                                    <iframe id="FraHuman" name="FraHuman" src="Human3.aspx" width="397" height="392"
                                                        frameborder="0"></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="height: 17px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
