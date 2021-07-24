<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="DBBackup.aspx.cs" Inherits="DataBaseBackup" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>工程项目管理系统数据库维护</title><link href="StyleCss/PM1.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        function pickOneFile() {
            var file = new Array();
            file[0] = "";
            var url = "MyFileBrowser.aspx";
            window.showModalDialog(url, file, "dialogHeight:486px;dialogWidth:800px;center:1;help:0;status:0;");
            if (file[0] != "") {
                document.getElementById('FileUploadRestore').value = file[0];
            }
        }
        function CheckSQL(obj) {
            var flag = false;
            if (((obj.value.replace(/(^\s*)/g, "").toLowerCase()).indexOf("select")) == 0 || ((obj.value.replace(/(^\s*)/g, "").toLowerCase()).indexOf("exec")) == 0) {
                document.getElementById('btnChaXun').disabled = false;
                document.getElementById('btnUpdate').disabled = true;
            }
            else if (((obj.value.replace(/(^\s*)/g, "").toLowerCase()).indexOf("alter")) == 0) {
                document.getElementById('btnUpdate').disabled = false;
                document.getElementById('btnChaXun').disabled = true;
            }
            else {
                document.getElementById('btnUpdate').disabled = true;
                document.getElementById('btnChaXun').disabled = true;
            }
            document.getElementById('btnUpdate').disabled = false;
            document.getElementById('btnChaXun').disabled = false;

        }
    </script>

</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" class="body_default">
    <form id="Form1" runat="server">
    
    <table width="800px" height="50%" border-right: lightblue 1px solid; table-layout: fixed;
                border-top: lightblue 1px solid; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                border-collapse: collapse;" align="center" >
        <tr>
            <td class="td-head"  align="center" >
               <strong> 工程项目管理系统数据库维护          </strong> 
            </td>
        </tr>
        <tr>
            <td class="td-title">
                数据库备份
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <table>
                    <tr>
                        <td style="width: 100px">
                            <span style="font-size: 9pt">操 作 数 据 库</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBackupDBList" Font-Size="12px" Width="300px" runat="server"></asp:DropDownList>
                        </td>
                        <td style="width: 300px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <span style="font-size: 9pt">备份名称和位置</span>
                        </td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtBackupFilePath" ToolTip="如：D:\beifen" Font-Size="12px" Width="300px" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:Button ID="btnBackup" Font-Size="12px" Text="备份数据库" OnClick="btnBackup_Click" runat="server" />
                        </td>
                    </tr>
                   
                </table>
            </td>
        </tr>
        <tr style="display:none" >
            <td class="td-title">
                数据库还原
            </td>
        </tr>
        <tr  style="display:none"  >
            <td valign="top" align="center">
                <table>
                    <tr>
                        <td style="width: 100px; height: 21px">
                            <span style="font-size: 9pt">操 作 数 据 库</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRestoreDBList" Font-Size="12px" Width="300px" runat="server"></asp:DropDownList>
                        </td>
                        <td style="width: 300px; height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <span style="font-size: 9pt">选择要还原文件</span>
                        </td>
                        <td style="width: 300px;">
                            <input type="text" readonly="readonly" class="txtreadonly" style="background-color: #ffffc0;
                                width: 250px" id="FileUploadRestore" runat="server" />
<img id="imgpickOneFile" style="cursor: hand" onclick="pickOneFile();" src="/images/contact.gif" align="absmiddle" runat="server" />

                            
                        </td>
                        <td style="width: 300px">
                            <asp:Button ID="btnRestore" Font-Size="12px" Text="还原数据库" OnClick="btnRestore_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="td-title">
                数据库查询
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <table>
                    <tr>
                        <td style="width: 100px; height: 21px">
                            <span style="font-size: 9pt">操 作 数 据 库</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSearchDBList" AutoPostBack="true" Font-Size="12px" Width="300px" OnSelectedIndexChanged="ddlSearchDBList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <td style="width: 100px; height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 21px">
                            <span style="font-size: 9pt">数 据 库 中 表</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSearchTableList" Font-Size="12px" Width="300px" runat="server"></asp:DropDownList>
                        </td>
                        <td style="width: 100px; height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px" valign="middle">
                            <span style="font-size: 9pt">查 询 语 句  </span>
                        </td>
                        <td style="width: 450px" valign="top">
                            <asp:TextBox ID="txtSql" TextMode="MultiLine" Rows="5" ToolTip="表名用中括号括起来" Width="498px" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 100px" valign="top">
                            <asp:Button ID="btnChaXun" Enabled="false" Font-Size="12px" Text="查\u3000\u3000询" OnClick="btnSearch_Click" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnUpdate" Enabled="false" Font-Size="12px" Text="对象更新" OnClick="btnUpdate_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border-right: lightblue 1px solid; table-layout: fixed;
                border-top: lightblue 1px solid; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                border-collapse: collapse;" align="center" >
        <tr>
            <td style="width: 1024px" >
                <div class="gridBox" style="width: 100%; height: 200px;">
                    <asp:GridView class="grid" ID="GVTable" Width="100%" OnPageIndexChanging="GVTable_PageIndexChanging" runat="server"></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
