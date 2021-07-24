<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="BulletinView.aspx.cs" Inherits="BulletinView" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>公告查看</title>
	<script language="javascript">
	
	<!--
//	function selChange()
//	{
//	    var opts = document.all("RBLtBound") ;  

//	    if(opts[1].checked) 
//	    {
//	        document.getElementById('bm').style.display = "";
//	    }
//	    else if(opts[2].checked)
//	    {
//	        document.getElementById('bm').style.display = "none";
//	    }
//	}
//	function selDept(userCode)
//	{
//	    var p = new Array();
//		p[0] = "";
//		p[1] = "";
//		var dept = document.getElementById('hdnDept').value;
//	    var url = "../common/selDept.aspx?yhdm=" + userCode + "&dept="+dept;
//	    var str = window.showModalDialog(url,window,'dialogHeight:600px;dialogWidth:350px;center:1;help:0;status:0;');
//	    if (str == undefined)
//	    {
//	    }
//	    else
//	    {
//            if (str != "")
//            {
//                var index = str.indexOf("*");
//	            document.getElementById('hdnDept').value = str.substring(0,index);
//	            document.getElementById('tbDept').value = str.substr(index+1);
////              document.getElementById('hdnDept').value = p[0];
////		        document.getElementById('tbDept').value = p[0];
//                str = "";
//            }
//        }
//	}
	function UpFile(t)
	{
	    //t=2 为公告管理
	    var RecordCode = document.getElementById('hdnRecordId').value;//编号
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=2";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	  function download(url)
	{	
	alert(url);        
        window.location.href = url;
	}
	-->
	</script>
</head>
<body class="body_popup" scroll="no">
    <form id="form1" runat="server">
    <table id="TableMain" cellspacing="0" cellpadding="0" width="100%" border="1" align="center" class="table-normal">
        <tr>
            <td class="td-head" align="center">
                <asp:Label ID="lbTitle" Text="" runat="server"></asp:Label></td>
        </tr>
		<tr height = "80%">
			<td>
			    <table border = "0" width ="100%" height="100%" cellpadding="0" cellspacing="0">
			        <tr>
			            <td>内容：</td>
			        </tr>
			        <tr height="280">
			            <td valign="top">
			             <div  class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                            &nbsp; &nbsp;
                             <asp:Label ID="LblCon" runat="server"></asp:Label></div>
                            </td>
			        </tr>
			        <tr>
			            <td>附件：<asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
			            <td><asp:HyperLink ID="hlUrl" NavigateUrl="" runat="server"></asp:HyperLink></td>
			        </tr>
			        <tr>
			            <td align="right">部门：<asp:Label ID="lbDept" Text="部门" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			        </tr>
			        <tr>
			            <td align="right">发布日期：<asp:Label ID="lbDate" Text="" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
			        </tr>
			    </table>
                </td>
		</tr>
		<tr>
			<td class="td-submit">
			<input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width : 10px" runat="server" />

			<input type="hidden" id="hdnDept" name="hdnRecordId" style="width : 10px" runat="server" />

            <input id="btnClose" type="button" value="关 闭" onclick="javascript:window.close();" />
                </td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
