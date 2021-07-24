<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrackDetail.aspx.cs" Inherits="ProgressManagement_Track_TrackDetail" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>月度报告 | 总体进度 | 里程碑 </title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />


    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setIfFrameSrc();
        });

        function setIfFrameSrc() {
            var id = getRequestParam('verId');
            //月度报告 type=1,总体进度 type=2 里程碑 type=3
            var type = getRequestParam('type');
            var url = '';
            if (type == '1') {
                url = '/ProgressManagement/Gantt/Month.htm?id=' + id;
            }
            else if (type == '2') {
                url = '/ProgressManagement/Gantt/Track.htm?id=' + id;
            }
            else {
                url = '/ProgressManagement/Gantt/Milestone.htm?id=' + id;
            }
            $('#ifPlusGantt').attr("src", url);
        }
    </script>

</head>
<body  style=" overflow:hidden;">
    <form id="form1" runat="server">
         <div id="divBudget" class="pagediv" style="overflow: hidden; width: 100%; height: 100%;">
        <iframe id="ifPlusGantt" style="width: 100%; height: 100%;" frameborder="0" scrolling="yes">
        </iframe>
    </div>
    </form>
</body>
</html>
