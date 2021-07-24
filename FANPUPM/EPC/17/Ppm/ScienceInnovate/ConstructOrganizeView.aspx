<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConstructOrganizeView.aspx.cs" Inherits="ConstructOrganizeView" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="HEAD1" runat="server"><title>施工组织</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
    </base>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>

    <script type="text/javascript">
       window.onload = function() {
         var mark=document.getElementById("hdnmark").value;
         if(mark==1)
           {
             GetWaterMarked(window,'/images/yinzh.jpg','this');
           }
      }
    </script>

</head>
<body scroll="no" style="height: 100%;">
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader" style="width: 100%">
                    施工组织
                </td>
            </tr>
        </table>
        <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    填报单位
                </td>
                <td colspan="3" class="elemTd">
                    <asp:Label ID="lblUnit" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    施组名称
                </td>
                <td colspan="3" class="elemTd">
                    <asp:Label ID="lblName" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr id="TrGDType" style="display:none;" runat="server"><td class="word" runat="server">
                    文档类别
                </td><td class="txt" colspan="3" runat="server">
                <asp:Label ID="lblmarkType" Text="" runat="server"></asp:Label>
                <JWControl:DropDownTree ID="DDTClass" Visible="false" runat="server"></JWControl:DropDownTree>
                <input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />

                <input id="HdnDocCode" style="WIDTH: 10px" type="hidden" name="HdnDocCode" runat="server" />

                </td></tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3" class="elemTd">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    主要描述
                </td>
                <td colspan="3">
                    <asp:Label ID="lblshuoming" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    备注
                </td>
                <td colspan="3">
                    <asp:Label ID="lblRemark" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    编制人
                </td>
                <td class="elemTd" style="padding-right: 3px">
                    <asp:Label ID="lblpeople" Text="" runat="server"></asp:Label>
                </td>
                <td class="descTd" style="white-space: nowrap;">
                    编制日期
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblDate" Text="Label" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hdnmark" runat="server" />

    </form>
</body>
</html>
