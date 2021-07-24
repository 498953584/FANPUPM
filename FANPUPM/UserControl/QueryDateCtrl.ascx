<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QueryDateCtrl.ascx.cs" Inherits="EPC_UserControl1_QueryDateCtrl" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<script src="../Web_Client/TreeNew.js" type="text/javascript"></script>
<script type="text/javascript">
    function getCtrlDate() {
        if (document.getElementById("QueryDateCtrl1_hdnDate").value != document.getElementById("QueryDateCtrl1_txtDate").value) {
            document.getElementById("QueryDateCtrl1_hdnDate").value = document.getElementById("QueryDateCtrl1_txtDate").value;
            document.getElementById("YMDBox").value = document.getElementById("QueryDateCtrl1_txtDate").value;
            document.getElementById("QueryDateCtrl1_btnSearch").click();
        }
    }

    //显示月份选择层
    function showDiv(obj) {
        i = 1;
        if (document.getElementById("ifMonth").src == "") {
            var url = "/Web_Client/SelMonth.html?id=" + obj.id;
            document.getElementById("ifMonth").src = url;
        }
        document.getElementById("ifMonth").style.display = "";
        setCtrl(obj);
    }
    //处理页面单击事件
    function document.onclick() {
        with (window.event) {
            if (srcElement != document.getElementById("QueryDateCtrl1_txtMonth"))
                document.getElementById("ifMonth").style.display = "none";
        }
    }
    //设置月份选择层显示位置
    function setCtrl(senderObj) {
        var dads = document.getElementById("ifMonth").style;
        var th = senderObj;
        var ttop = senderObj.offsetTop;       //senderObj控件的定位点高
        var thei = senderObj.clientHeight;   //senderObj控件本身的高
        var tleft = senderObj.offsetLeft;    //senderObj控件的定位点宽
        var ttyp = senderObj.type;          //senderObj控件的类型
        while (senderObj = senderObj.offsetParent) {
            ttop += senderObj.offsetTop;
            tleft += senderObj.offsetLeft;
        }
        ///到此，获取了控件的绝对位置

        if ((tleft + 200) > document.body.clientWidth) {
            tleft = document.body.clientWidth - 200;
        }
        if ((ttop + 150) > document.body.clientHeight) {
            ttop = document.body.clientHeight - 150;
        }
        dads.top = (ttyp == "image") ? ttop + thei : ttop + thei + 4;
        dads.left = tleft;
        outObject = th;
    }
</script>

<link href="../../StyleCss/PM1.css" rel="stylesheet" type="text/css" />
<asp:Label ID="lblName" Text="" runat="server"></asp:Label>
<asp:TextBox onkeydown="javascript:return false;" ID="txtMonth" Width="60px" CssClass="text-input" AutoPostBack="true" OnTextChanged="txtMonth_TextChanged" runat="server"></asp:TextBox>
<asp:DropDownList ID="ddlYear" AutoPostBack="true" Width="60px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
<JWControl:DateBox ID="txtDate" class="text-input" Width="70px" onblur="getCtrlDate();" OnTextChanged="txtDate_TextChanged" runat="server"></JWControl:DateBox>
<asp:Button ID="btnPre" Width="55px" CssClass="button-normal" OnClick="btnPre_Click" runat="server" />
<asp:Button ID="btnNow" Width="55px" CssClass="button-normal" OnClick="btnNow_Click" runat="server" />
<asp:Button ID="btnNext" Width="55px" CssClass="button-normal" OnClick="btnNext_Click" runat="server" /><asp:Button ID="btnSearch" Style="display: none;" OnClick="btnSearch_Click" runat="server" />
<asp:HiddenField ID="hdnMonth" runat="server" />
<asp:HiddenField ID="hdnDate" runat="server" />
<iframe bgcolor="#000000" id="ifMonth" frameborder="0" style="position: absolute;
    width: 200; height: 150; z-index: 9998; display: none"></iframe>
