<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjTree.aspx.cs" Inherits="PrjTree" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_builddiary_ucprojectlist_ascx" Src="~/EPC/BuildDiary/UCProjectList.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ProjectList</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self" />

    <script src="../../Web_Client/TreeNew.js" type="text/javascript"></script>

    <style>
        .body_clear /*�ıߵ�ҳ�߾�Ϊ0*/
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            font-size: 9pt;
            font-family: ����;
        }
    </style>
</head>
<body class="body_clear" scroll="no" onload="selrow3('UProjectList1_tvProjectt1')">
    <form id="Form1" method="post" runat="server">
    <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" style="table-layout: fixed;">
        <tr>
            <td>
                <div style="width: 100%; height: 100%; overflow: auto;">
                    <MyUserControl:epc_builddiary_ucprojectlist_ascx ID="UProjectList1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    </form>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
