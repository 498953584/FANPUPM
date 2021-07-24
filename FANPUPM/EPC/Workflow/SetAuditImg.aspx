<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAuditImg.aspx.cs" Inherits="EPC_Workflow_SetAuditImg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>审核签名设置</title>
     <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
    $(document).ready(function(){
       // $("#img").tooltip({showURL:false});
    });
    </script>
    <script type="text/javascript">
      function CheckImg()
      { 
        var AllImgExt=".jpg|.jpeg|.gif|.bmp|.png|"//全部图片格式类型
        var upfile=document.getElementById("FUFilePath").value;
        if(upfile==""||upfile==null)
        {
           alert("请选择上传的签名图片");
           return false;
        }
        else
        {
          var fileExtention=upfile.substr(upfile.lastIndexOf(".")).toLowerCase();
          if(AllImgExt.indexOf(fileExtention+"|")==-1)
          {
            alert("请选择图片格式jpg,png,gif,bmp的签名图片");
            return false;
          }
        }
        
     
      }
    </script>
</head>
<body>
    <form id="form1" onsubmit="return CheckImg();" runat="server">
    <div align="center">
        <table class="tableAudit" style="width:600px" border="1">
        <tr>
          <td colspan="2" class="divHeader">审核签名设置</td>
        </tr>
            	<tr>
					<td class="td-label">姓 名</td>
					<td class="td-content"><asp:TextBox ID="tbLoginName" Width="180px" ReadOnly="true" runat="server"></asp:TextBox></td>
				</tr>
				   <tr>
                <td class="td-label">原签名图片
                </td>
                <td class="td-content">
                    <asp:Image ID="ImgName" Width="80px" Height="80px" ImageUrl="~/images/defaultaudit.gif" runat="server" /><br />
                             <span style="width:100%; text-align:left;line-height:20px; background-color:#fbfbfb">支持jpg、png、gif、bmp格式的签名图片，最佳尺寸 80px（宽） x 80px（高）,签名以最新上传的签名为依据</span>

                </td>
            </tr>
            <tr>            
                <td class="td-label">
                    上传个人签名
                </td>
                <td class="td-content">
                    <asp:FileUpload ID="FUFilePath" Width="80%" runat="server" />
                </td>
            </tr>
         
            <tr>
            <td colspan="2">
                <asp:Button ID="SaveBt" Text="上传" OnClick="SaveBt_Click" runat="server" />
               
                  <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
