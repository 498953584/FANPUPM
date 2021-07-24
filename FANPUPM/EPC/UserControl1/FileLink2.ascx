<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileLink2.ascx.cs" Inherits="EPC_UserControl1_FileLink2" %>


<script type="text/javascript">
    //////////////////////////////////////////////////////////////////////////
    //2011-10-19
    //mender lpw
    //增加 _type变量 此变量的作用 是用来区分 查看 修改 新增
    //将此变量传值到url地址页面中 根据此值判断是否存在新增附件按钮
    function openannexpage(fid, mid,_type) {
        if (fid != "") {
            //原代码写法
            //window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=" + mid + "&rc=" + escape(fid) + "&at=0&type=0", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
            window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=" + mid + "&rc=" + escape(fid) + "&at=0&type=" + _type + "", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
        }
        else {
            alert("请先保存！");
        }
    }
    function upLoadFile(fid, mid) {
        var url = "/CommonWindow/Annex/upLoadFile.aspx?mid=" + mid + "&fid=" + fid;
        return window.showModalDialog(url, window, "dialogHeight:180px;dialogWidth:400px;center:1;help:0;status:0;");
    }
		function delLoadFile(iid) {
		    alert(iid);
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


<div style="display:block; float:left; width:100%;">
    <div style="text-align:left">
    
    <input type="text" id="txtname" style=" width:255px;Height:15px" contenteditable="false" runat="server" />

<asp:Button ID="But_UpFile" Style="background: #fff url(/images1/btnBack.jpg); text-align: center; vertical-align: middle; width: 75px; height: 20px; margin-left: 7px; margin-right:0px;" Text="新增附件" BorderStyle="None" OnClick="Butsc_Click" runat="server" />
    
<asp:Button ID="Btn_Upload" Style="background: #fff url(/images1/btnBack.jpg); text-align: center; vertical-align: middle; width: 75px; height: 20px; margin-left: 7px; margin-right:0px;" Text="管理附件" BorderStyle="None" OnClick="Butsc_Click" runat="server" />
    </div>
</div>
