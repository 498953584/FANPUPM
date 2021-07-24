<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickSinglePerson.aspx.cs" Inherits="PickSinglePerson" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>选择人员...</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script language="JavaScript" type="text/JavaScript">
        function confirmHuman() {
            var human = window.dialogArguments;
            human[0] = document.getElementById('bmdm').value;   //部门代码
            human[1] = document.getElementById("dutyid").value; //岗位代码
            human[2] = document.getElementById('dutyname').value; //岗位名称
            window.returnvalue = human;
            window.close();
        }
    </script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td bgcolor="#ffffff">
                <iframe id="FraDept" name="FraDept" src="DeptList.aspx" width="100%" scrolling="auto" height="100%" runat="server"></iframe>
            </td>
            <td>
                <iframe id="FraHuman" name="FraHuman" src="DutyList.aspx" frameborder="0" width="300px"
                    height="100%"></iframe>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right" height="30">
                <input type="hidden" id="bmdm" runat="server" />

                <table width="60%" height="100%">
                    <tr align="center" valign="middle">
                        <td>
                            <input id="BtnOK" type="button" value="确  定" disabled onclick="confirmHuman();" class="button-normal">
                        </td>
                        <td>
                            <input id="BtnCancel" type="button" value="取  消" onclick="window.close();" class="button-normal">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" id="dutyid" runat="server" />

    <input type="hidden" id="dutyname" runat="server" />

    </form>
</body>
</html>
