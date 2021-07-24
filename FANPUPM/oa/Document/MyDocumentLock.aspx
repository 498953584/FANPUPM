<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyDocumentLock.aspx.cs" Inherits="oa_Document_MyDocumentLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>公文管理</title>
    <script language ="javascript" type="text/javascript">
	function pickPerson()
    {
        var p = new Array();
	    p[0] = "";
	    p[1] = "";
	    var url = "";
	    url = "../../CommonWindow/PickSinglePerson.aspx";
	    window.showModalDialog(url,p,"dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
	    if (p[0]!="")
	    {
	        document.getElementById('hdnUserCode').value = p[0];
		    document.getElementById('txtUserName').value = p[1];
	    }
    }
    function UpFile(RID)
	{			
		var RecordCode = RID;//编号
		var url = "../../commonWindow/Annex/AnnexList.aspx?mid=1&rc="+RecordCode+"&at=0&type=1";
		var ref = window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
		//if(ref)
		//{
		//    return true;
		//}
		//return false;
		return true;
	}
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="form1" runat="server">
    <div>
    <table class="table-form" id="table1"  cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="2" height="20">
               公文信息登记</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">公文标题</td>
			<td><asp:Label ID="LbTitle" runat="server"></asp:Label><font color="#ff0000">&nbsp;</font> 
			<input id="hdnTitle" style="WIDTH: 10px" type="hidden" name="hdnTitle" runat="server" />

			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
                流程发启人</td>
			<td><asp:Label ID="LbUserName" runat="server"></asp:Label>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="25%">起草时间</td>
			<td>
			<input id="hdnRecordDate" style="WIDTH: 10px" type="hidden" name="hdnRecordDate" runat="server" />

                <asp:Label ID="LbRecordDate" runat="server"></asp:Label></td>
		</tr>
		<tr runat="server"><td class="td-label" width="25%" runat="server">附件</td><td runat="server">
						<!--
                &nbsp;<asp:Button ID="btnAnnex" Text="查看附件" CssClass="button-normal" runat="server" />-->
                
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                
            </td></tr> 
		<tr>
			<td class="td-label" width="25%">情况说明</td>
			<td><asp:Label ID="LbRemark" runat="server"></asp:Label> </td>
		</tr> 		
	</table>
      </div> 
    </form>
</body>
</html>
