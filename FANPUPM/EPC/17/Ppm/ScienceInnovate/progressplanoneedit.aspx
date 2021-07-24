<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressplanoneedit.aspx.cs" Inherits="ProgressPlanOneEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>编辑进步计划</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

    <base target="_self" />
    <meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script language="javascript" src="../../../../Web_Client/Tree.js" type="text/javascript"></script>
    <script language="javascript">
        function OpenAudit() {
            var strIsSave = document.getElementById("hidIsSave").value;
            var url = "ProgressPlanPermiss.aspx?planId=";
            if (strIsSave == "false") {
                alert("请先保存当前页数据！");
            }
            else {
                url += document.getElementById("hidPlanId").value;
                window.showModalDialog(url, window, "dialogHeight:400px;dialogWidth:500px;center:1;help:0;status:0;");
            }
        }
        function OpenAnnex() {
            var PlanId = document.getElementById("hidPlanId").value;
            if (PlanId == "") {
                alert("请先保存计划信息再上传附件!");
                return;
            }
            var url = "/CommonWindow/Annex/AnnexList.aspx?mid=1747&rc=" + PlanId + "&at=0&type=2";
            window.showModalDialog(url, window, 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');

        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height: 20px;">
                <td class="divHeader">
                    进步计划
                </td>
            </tr>
        </table>
    </div>
    <table id="Table1" class="tableContent2" cellpadding="5px" cellspacing="0">
        <tr>
            <td class="word">
                编号
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="txtPlanCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word">
                成果名称
            </td>
            <td colspan="3" class="txt">
                <asp:TextBox ID="txtResultName" MaxLength="50" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word" style="white-space: nowrap;">
                执行成果单位/人
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="txtResultUint" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word">
                应用工程名称
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="txtApplication" MaxLength="50" runat="server"></asp:TextBox>
                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
        <tr>
            <td class="word">
                附件
            </td>
            <td colspan="3" class="txt">
                <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="word">
                备注
            </td>
            <td colspan="3" class="txt">
                <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word">
                类别
            </td>
            <td class="txt">
                <asp:TextBox ID="txtPlanSort" runat="server"></asp:TextBox>
                <asp:Label ID="lbPlanSort" Visible="false" runat="server"></asp:Label>
                <input id="hidIsSave" type="hidden" value="true" name="Hidden1" style="width: 33px" runat="server" />

            </td>
            <td class="word">
                完成时间
            </td>
            <td class="txt">
                <JWControl:DateBox ID="txtCompletedDate" runat="server"></JWControl:DateBox>
            </td>
        </tr>
    </table>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr class="tableFooter2">
                <td>
                    <input id="hidPrjCode" type="hidden" name="Hidden2" runat="server" />

                    <input id="hidPlanId" type="hidden" name="Hidden1" runat="server" />

                    <asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />&nbsp;
                    <input type="button" onclick="window.returnValue=true;top.ui.closeTab();" value="取 消" id="BunClose" runat="server" />
&nbsp;
                    <input type="button" value="项目部审核情况" onclick="OpenAudit();" style="display: none;
                        width: 120px" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnProgressGuid" runat="server" />
    </form>
</body>
</html>
