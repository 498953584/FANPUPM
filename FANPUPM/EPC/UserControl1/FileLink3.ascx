<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileLink3.ascx.cs" Inherits="EPC_UserControl1_FileLink3" %>

<link rel="stylesheet" type="text/css" href="../../StyleCss/PM4.css" />
<script language="javascript">
    function openannexpage(fid, mid, _type) {
        if (fid != "") {
            //	window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid="+mid+"&rc="+escape(fid)+"&at=0&type=0,",'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
            window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=" + mid + "&rc=" + escape(fid) + "&at=0&type=" + _type + "", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
        }
        else {
            alert("请先保存！");
        }
    }
    function upLoadFile(fid, mid, prjguid) {
        var url = "/CommonWindow/Annex/upLoadFile.aspx?mid=" + mid + "&fid=" + fid + "&prjguid=" + prjguid;
        /*解决上传空间与JQuery-va验证框架的submit冲突，弃用验证框架，此代码亦可弃用*/
        if (typeof (validate_IsSumbit_self) != "undefined")
            validate_IsSumbit_self = true;
        /*--------------------------------------------------------------*/
        return window.showModalDialog(url, window, "dialogHeight:180px;dialogWidth:400px;center:1;help:0;status:0;");

    }
    function delupFile(iid) {
        /*解决上传空间与JQuery-va验证框架的submit冲突，弃用验证框架，此代码亦可弃用*/
        if (typeof (validate_IsSumbit_self) != "undefined")
            validate_IsSumbit_self = true;
        /*--------------------------------------------------------------*/
        document.getElementById("FileLink1_hdAnnxCode").value = iid;
        document.getElementById("FileLink1_btnDel").click();
    }
    function showAnnex() {
        document.Form1.TBoxAnnex.value = "";
        var url = "broker.aspx?go=1";
        var annex = new Array();
        window.showModalDialog(url, annex, "dialogHeight:225px;dialogWidth:425px;center:1;help:0;status:0;");
        for (var i = 0; i < annex.length; i++) {
            document.Form1.TBoxAnnex.value += annex[i] + ",";
        }
    }
</script>
<asp:Literal ID="LL" ClientIDMode="AutoID" runat="server"></asp:Literal>
<asp:Button ID="But_UpFile" style="background:url(/images/Add-attachment.gif) no-repeat left;" Width="85px" Text="添加附件" BorderStyle="None" ForeColor="DarkGreen" OnClick="Butsc_Click" runat="server" />
<asp:Button ID="btnDel" Text="删除" OnClick="btnDel_Click" runat="server" />
<input id="hdAnnxCode" type="hidden" name="hdAnnxCode" runat="server" />
