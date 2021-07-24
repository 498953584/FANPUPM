<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="ProgressManage_View" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>总体进度视图</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setWidthHeight();
            setIfFrameSrc();
        });

        function setWidthHeight() {
            $('#divBudget').height($(this).height());
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
            $('#div_project').height($(this).height() - 20);
        }

        function setIfFrameSrc() {
            var prjId = $('#hfldTreeSelVale').val();
            var isLoadGantt = false;
            var start;
            var end

            if (prjId != '') {
                $.ajax({
                    type: "GET",
                    async: false,
                    url: "/ProgressManage/Handler/GetStarEndDate.ashx?" + new Date().getTime() + "&prjId=" + prjId,
                    dataType: "json",
                    success: function(data) {
                        if (data == "0") {
                            alert("系统提示：\n\n项目不存在，请刷新");
                            return;
                        }
                        else {
                            start = data[0].StartDate;
                            end = data[0].EndDate;
                            isLoadGantt = compareDate(start, end);
                        }
                    }
                });
            }

            if (prjId == "") {
                $('#noProject').css("display", "");
                $('#setDate').css("display", "none");
            }
            else {
                if (!isLoadGantt) {
                    $('#setDate').css("display", "");
                    $('#noProject').css("display", "none");
                }
            }

            if (isLoadGantt) {
                loadGantt(prjId);
            }
        }

        //比较项目的开始时间和结束时间,当开始时间<结束时间返回true
        function compareDate(start, end) {
            var isAllow = true;
            if (start == null || end == null || start == "" || end == "") {
                isAllow = false;
            }
            else {

                var startArray = start.split("-");
                var endArray = end.split("-");
                var startTime = new Date(startArray[0], startArray[1] - 1, startArray[2]);
                var endTime = new Date(endArray[0], endArray[1] - 1, endArray[2]);

                if (startTime >= endTime) {
                    isAllow = false;
                }
            }
            return isAllow;
        }
        //加载甘特图页面
        function loadGantt(prjId) {
            var url = "../ProgressManage/Gantt/View.htm?id=" + prjId;
            $('#ifPlusGantt').attr("src", url);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td id="td_Left" style="width: 195px; vertical-align: top; height: 100%;">
                                <div style="">
                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div id="div_project" class="pagediv" style="width: 195px; height: 100%; vertical-align: top;
                                    position: relative;">
                                    <asp:TreeView ID="tvBudget" Font-Size="12px" ShowLines="true" Style="top: 0px; position: absolute;" OnSelectedNodeChanged="tvBudget_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table class="tab">
                                    <tr>
                                        <td style="vertical-align: top;" colspan="3">
                                            <div id="divBudget" class="pagediv" style="overflow: hidden;">
                                                <span id="noProject" style="color: Red; display: none;">没有项目可以加载</span> <span id="setDate"
                                                    style="color: Red; display: none;">项目的结束时间必须大于开始时间，请联系管理员进行设置时间 </span>
                                                <iframe id="ifPlusGantt" style="width: 100%; height: 100%;" frameborder="0" scrolling="yes">
                                                </iframe>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldTreeSelVale" runat="server" />
    </form>
</body>
</html>
