<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassPrivilege.aspx.cs" Inherits="oa_UserDefineFlow_ClassPrivilege" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>自定义权限设置</title>
    <base target="_self" />
   
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
    //选择人员
    function pickOperater() {
				var p = new Array();
				p[0] = "";
				p[1] = "";
				var bclass = getRequestParam("bclass"); 
				var url = "/EPC/WorkFlow/SelectAllUser.aspx?id="+bclass;
				window.showModalDialog(url,p,"dialogHeight:456px;dialogWidth:600px;center:1;help:0;status:0;");
//				if (p[0]!=""){
					document.getElementById('hiddenuser').value = p[0];
					document.getElementById('txtuserlist').value = p[1];
//				}
			}
	//重置
	function SetNull()
	{
	   var txtuser=document.getElementById("txtuserlist");
	   var hiduser=document.getElementById("hiddenuser");
	   txtuser.value="";
	   hiduser.value="";
	}		
	function CheckNull()
	{
       var txtuser=document.getElementById("txtuserlist");
       if(txtuser.value=="")
       {
         alert("权限用户列表不能为空");
         return false;
       }
	}
    </script>
</head>
<body>
    <form id="form1" onsubmit="return CheckNull();" runat="server">
    <div>
    			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>

        <table class="tableAudit" width="100%" border="1" class="tableAudit">
            <tr>
                <td colspan="2" class="divHeader">
                    自定义流程分类权限设置
                </td>
            </tr>
            <tr>
                <td class="td-label">
                    流程分类名称
                </td>
                <td>
                    <asp:TextBox ID="txtclassname" Width="160px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                    权限用户列表
                </td>
                <td>
                    <span class="spanSelect" style="width: 320px; float: left">
                        <asp:TextBox ID="txtuserlist" Style="width:300px; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px;" runat="server"></asp:TextBox>
                            <input type="hidden" id="hiddenuser" runat="server" />

                        <img alt="选择人员" id="imgBt" onclick="pickOperater();" src="/images1/iconSelect.gif" />
                    </span>
                  <input type="hidden" id="hdnFrontPerson" name="hdnFrontPerson" style="width: 10px" runat="server" />

                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" value="重置" onclick="SetNull();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div id="divStartWF" title="选择人员" style="display:none; overflow:hidden">
<iframe id="iframeWf" width="100%" height="100%" frameborder="0" src="" style="overflow:hidden"></iframe>
</div>
</body>
</html>
