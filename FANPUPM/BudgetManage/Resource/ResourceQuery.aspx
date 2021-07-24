<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceQuery.aspx.cs" Inherits="BudgetManage_Resource_ResourceQuery" MaintainScrollPositionOnPostBack="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源查询</title><link rel="stylesheet" type="text/css" href="../../Script/lightbox/jquery.lightbox-0.5.css" media="screen" />

    <style type="text/css">
        #gallery img
        {
            border: 1px solid #3e3e3e;
            border-width: 5px 5px 20px;
        }
    </style>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../Script/lightbox/jquery.lightbox-0.5.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        $(window).load(function() {
            replaceEmptyTable('emptyTable', 'gvResource');
            var aa = new JustWinTable('gvResource');
            $('.gallery').each(function() {
                $(this).find('a').lightBox({
                    overlayBgColor: '#555',
                    imageLoading: '../../Script/lightbox/images/lightbox-ico-loading.gif',
                    imageBtnPrev: '../../Script/lightbox/images/lightbox-btn-prev.png',
                    imageBtnNext: '../../Script/lightbox/images/lightbox-btn-next.png',
                    imageBtnClose: '../../Script/lightbox/images/lightbox-btn-close.jpg',
                    txtImage: '图片',
                    txtOf: '共'
                });
            });
        });

        //对文本框输入数字的限制
        function keyPress(ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/)) ob.value = ob.t_value; else ob.t_value = ob.value; if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/)) ob.o_value = ob.value;
        }
        function keyUp(ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/)) ob.value = ob.t_value; else ob.t_value = ob.value; if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/)) ob.o_value = ob.value;
        }
        function onBlur(ob) {
            if (!ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/)) ob.value = ob.o_value; else { if (ob.value.match(/^\.\d+$/)) ob.value = 0 + ob.value; if (ob.value.match(/^\.$/)) ob.value = 0; ob.o_value = ob.value };
        }
        //查看供应商
        function openContractEdit(Id) {
            var url = "/EPC/Basic/ContactCorpEdit.aspx?t=2&ci=" + Id + "&Action=Query";
            return window.showModalDialog(url, window, "dialogHeight:410px;dialogWidth:700px;center:1;help:0;status:0;");
        }
    </script>

</head>
<body style="overflow-y:hidden;">
    <form id="form1" runat="server">
    <table style="width: 100%; height: 88%;">
        <tr id="header">
            <td colspan="2">
                <asp:Label ID="lblTitle" Font-Bold="true" Text="资源查询" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 240px; vertical-align: top; height: 100%;">
                <div class="pagediv" style="width: 240px; height: 108%;" id="div1">
                    <asp:TreeView ID="tvResource" ExpandDepth="2" ShowLines="true" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td style="width: 100%; vertical-align: top; height: 108%; border-left: solid 2px #CADEED;">
              <iframe id="InfoList" name="InfoList" src="ResourceQueryDetail.aspx?id=''" frameBorder="0" width="100%"  style=" overflow-x:auto; overflow-y:hidden; "  height="100%"></iframe>  
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
