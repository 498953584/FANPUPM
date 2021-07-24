<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPhoto.aspx.cs" Inherits="CommonWindow_Annex_UploadPhoto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>上传附件</title><link href="/StyleCss/PM1.css" rel="stylesheet" type="text/css" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">

     <link href="/StyleCss/PM4.css" rel="stylesheet" type="text/css" />

    <script  type="text/javascript" src="../../../../Web_Client/TreeNew.js"></script>

    <script>
        window.name = "winz";
        function test() {
            var file = document.getElementById("fileAnnex");
            if (checkExd(file.value)) checkSize(file.value)
        }
        function checkSize(fileName) {
            var img = new Image();
            img.onerror = new Function("alert('文件不存在，或目标类型不匹配！');");
            img.onreadystatechange = function() {
                if (img.readyState == "complete") {
                    if (img.fileSize > 6* 1024 * 1024) alert("文件超过6M");
                }
            }
            img.src = fileName;
        }
        function checkExd(fileName) {
            if (fileName.lastIndexOf(".") + 1 >= fileName.length) {
                alert("格式不正确");
                return false;
            }
            var exd = fileName.substring(fileName.lastIndexOf(".") + 1).toUpperCase();
            if (exd == "GIF" || exd == "JPG" || exd == "BMP" || exd=="PNG" || exd=="JPEG")
                return true;
            else {
                alert("格式不正确");
                return false;
            }
        }

    </script>

</head>
<body class="body_popup" scroll="no" ms_positioning="FlowLayout">
    <form id="Formx" method="post" target="winz" runat="server">
    <table class="table-normal" id="Tablex" cellspacing="0" cellpadding="0" width="100%"
        border="1">
        <tr>
            <td colspan="2" height="20">
                <asp:Label ID="Label1" CssClass="td-title" Text="上传照片" runat="server"></asp:Label>
               
            </td>
        </tr>
        <tr>
            <td class="td-label" align="right" width="18%">
                照片编号
            </td>
            <td>
                <asp:TextBox ID="txtFileCode" Width="320px" CssClass="text-input" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" align="right" width="18%">
                照片文件
            </td>
            <td>
                <input id="fileAnnex" type="file" name="File1" style="width: 320px" class="text-input" onpropertychange="test()" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-label" valign="top" align="right">
                照片说明<br />
                (说明最多500个字符或者250个汉字)
            </td>
            <td>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="6" Columns="50" CssClass="textarea-input" Width="320px" runat="server"></asp:TextBox>
  
            </td>
        </tr>
        <tr>
            <td valign="top" align="right" colspan="2">
                <table id="Table2" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr align="center">
                        <td colspan="2" class="td-submit">
                            <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                            <asp:Button ID="btnAd" CssClass="button-normal" Text="确 定" runat="server" />
                            <input class="button-normal" id="btnClose" onclick="window.close();" type="button"
                                value="取 消" name="btnClose">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
       <span style="font-size:x-small; font-style: normal; color:Red">注：支持的格式有jpg、gif、jpeg、png、bmp
                </span>
    </form>
</body>
</html>
