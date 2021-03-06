<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActualDetail.aspx.cs" Inherits="ProgressManagement_Actual_ActualDetail" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>实际进度上报明</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setIfFrameSrc();
        });

        function setIfFrameSrc() {
            //计划版本Id
            var id = getRequestParam('verId');
            loadGantt(id);
        }

        function loadGantt(progressVerId) {
            var url = "/ProgressManagement/Gantt/Actual.htm?id=" + progressVerId;
            $('#ifPlusGantt').attr("src", url);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divBudget" class="pagediv" style="overflow: hidden; width: 100%; height: 100%;">
        <iframe id="ifPlusGantt" style="width: 100%; height: 100%;" frameborder="0" scrolling="auto">
        </iframe>
    </div>
    </form>
</body>
</html>
