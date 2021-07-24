<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileLink.ascx.cs" Inherits="EPC_UserControl1_FileLink" %>


<script language=javascript>
    function openannexpage(fid, mid, _type)
		{	
			if(fid!="" )
			{
			//	window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid="+mid+"&rc="+escape(fid)+"&at=0&type=0,",'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			    window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=" + mid + "&rc=" + escape(fid) + "&at=0&type=" + _type + "", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			}
			else
			{
			 alert("请先保存！");
			}	
		}
		function upLoadFile(fid,mid)
		{
			var url= "/CommonWindow/Annex/upLoadFile.aspx?mid=" + mid + "&fid=" + fid;
			return window.showModalDialog(url,window,"dialogHeight:180px;dialogWidth:400px;center:1;help:0;status:0;");
		}
		function delLoadFile(iid)
		{
        alert(iid);
        }
    function showAnnex()
		{
		    document.Form1.TBoxAnnex.value = "";
		    var url = "broker.aspx?go=1";
		    var annex = new Array();
		    window.showModalDialog(url,annex,"dialogHeight:225px;dialogWidth:425px;center:1;help:0;status:0;");
		    for (var i=0;i<annex.length;i++)
		    {
			    document.Form1.TBoxAnnex.value += annex[i] + ",";
		    }
		}
</script>
<asp:Literal ID="LL" runat="server"></asp:Literal>
<asp:Button ID="But_UpFile" CssClass="button-normal" Text="@添加附件" BorderStyle="None" ForeColor="DarkGreen" OnClick="Butsc_Click" runat="server" />
 <asp:Button ID="Btn_Upload" CssClass="button-normal" Text="@附件管理" BorderStyle="None" ForeColor="DarkGreen" OnClick="Butsc_Click" runat="server" />
