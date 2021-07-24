<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceList.aspx.cs" Inherits="BudgetManage_Resource_ResourceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源维护</title><link rel="stylesheet" type="text/css" href="../../Script/lightbox/jquery.lightbox-0.5.css" media="screen" />

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
            var aa = new JustWinTable('gvResource');
            setButton(aa, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
            addEvent($('#btnEdit')[0], 'click', rowEdit);
            addEvent($('#btnLook')[0], 'click', rowQuery);
            addEvent($('#btnAdd')[0], 'click', rowAdd);
            addEvent($('#btnExcelIn')[0], 'click', rowIn);
            $('.gallery').each(function() {
                $(this).find('a').lightBox({
                    overlayBgColor: '#555',
                    imageLoading: '../../Script/lightbox/images/lightbox-ico-loading.gif',
                    imageBtnPrev: '../../Script/lightbox/images/lightbox-btn-prev.png',
                    imageBtnNext: '../../Script/lightbox/images/lightbox-btn-next.png',
                    imageBtnClose: '../../Script/lightbox/images/lightbox-btn-close.jpg',
                    txtImage: '图片',
                    txtOf: '共',
                    maxWidth: 600,
                    maxHeight: 600
                });
            });
            //setWidthHeight();
        });
        function setWidthHeight() {
            $('#divBudget').width(($(this).width()
            ) - ($('#td_Left').width())-5);
            $('#divBudget').height($(this).height() - 105);
        }
        function rowEdit() {
            var url = "ResourceEdit.aspx?id=" + document.getElementById("hfldPurchaseChecked").value + "&parentId=" + $("#hdParentId").val() + "&nodeId=" + $("#hdPtNodeId").val();
            winopen(url)
        }
        function rowAdd() {
            var url = "ResourceEdit.aspx?parentId=" + $("#hdParentId").val() + "&nodeId=" + $("#hdPtNodeId").val();
            winopen(url)
        }
        function rowQuery() {
            var url = "ResourceEdit.aspx?t=1&id=" + $("#hfldPurchaseChecked").val();
            winopen(url)
        }
        //Excel导入
        function rowIn() {
            var url = "ResourceExcelIn.aspx?parentId=" + $("#hdParentId").val();
            winopen(url)
        }
           
    </script>

</head>
<body style="overflow-y:hidden;">
    <form id="form1" runat="server">
    <table style="width: 100%; height: 88%;">
        <tr id="header">
            <td colspan="2">
                <asp:Label ID="lblTitle" Font-Bold="true" Text="资源维护" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="td_Left" style="width: 240px; vertical-align: top; height: 100%;">
                <div class="pagediv" style="width: 240px; height: 108%;">
                    <asp:TreeView ID="tvResource" ExpandDepth="2" ShowLines="true" OnTreeNodePopulate="TreeView_Populate" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td style="width: 100%; vertical-align: top; height:108%; border-left: solid 2px #CADEED;">
            <iframe id="InfoList" name="InfoList" src="ResourceDetail.aspx?id=&nodeId=&parentId=" frameBorder="0" width="100%"  style=" overflow-x:auto; overflow-y:hidden; "  height="100%"></iframe>  
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
