<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schema.aspx.cs" Inherits="ProgressManage_Schema" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>进度计划编制</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>

    <link href="../Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../Script/jquery.cookie.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../StockManage/script/Validation.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setWidthHeight();
            setIfFrameSrc();
            if ($('#setDate').css("display") != "none")
                Val.validate('form1', 'btnSave');
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
            var end;

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
                    $('#txtStartDate').val(start);
                    $('#txtEndDate').val(end);
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
            var url = "../ProgressManage/Gantt/Schema.htm?id=" + prjId;
            var src = "../Gantt/demo/Project.html?id=" + prjId;
            $('#ifPlusGantt').attr("src", url);
        }

        //设置时间
        function setPrjDate() {
            $('#dvSetPrjDate').dialog({
                open: function() {
                    $(this).parent().appendTo("form:first");
                },
                title: "设置时间",
                modal: true,
                resizable: false,
                width: 500,
                height: 300
            });
        }

        //管理时间
        function controlDate(para) {
            //alert(document.getElementsById('txtStartDate').value);
            var startDates = document.getElementById('txtStartDate').value;
            var startDateList = startDates.split('-');
            var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);
            var endDates = document.getElementById('txtEndDate').value;

            if (startDates != "" && endDates != "") {
                if (startDate == 'NaN') {
                    alert('系统提示：\n\n输入时间格式不正确！');
                    para.value = "";
                    return;
                }

                var endDatesList = endDates.split('-');
                var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);

                if (endDate == 'NaN') {
                    alert('系统提示：\n\n输入时间格式不正确！');
                    para.value = "";
                    return;
                }
                if (endDate - startDate <= 0) {
                    para.value = "";
                    alert('系统提示：\n\n结束时间必须大于开始时间！请重新选择时间！')
                }
            }
        }
        function ScrollToSelectNode(trvwId,divId) {
            try {
                var elem = document.getElementById('tvBudgett51');
                if (elem != null) {
                    var node = document.getElementById(elem.value);
                    if (node != null) {
                        //滚动被选择节点到TreeView顶部
                        node.scrollIntoView();

                        //使被选择节点距离TreeView顶部10，使被选择节点可见
                        document.getElementById(divId).scrollLeft = 0;
                        document.getElementById(divId).scrollTop = 70;
                    }
                }
            }
            catch (oException) {
            }
        }  
    </script>

</head>
<body >
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
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
                                    <asp:TreeView ID="tvBudget" Font-Size="12px" ShowLines="true" Style="position: absolute; top: 0px;" OnSelectedNodeChanged="tvBudget_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table class="tab">
                                    <tr>
                                        <td style="vertical-align: top;" colspan="3">
                                            <div id="divBudget" class="pagediv" style="overflow:hidden;">
                                                <span id="noProject" style="color: Red; display: none;">没有项目可以加载</span> <span id="setDate"
                                                    style="color: Red; display: none;">项目的结束时间必须大于开始时间，请 <a onclick="setPrjDate()" class="link">
                                                        设置时间</a> </span>
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
    <div id="dvSetPrjDate" style="display: none; margin: 0 auto; padding-left: 120px;">
        <table style="text-align: center; width: 85%; height: 80px;">
            <tr>
                <td>
                    开始时间
                </td>
                <td class="elemTd mustInput" align="left">
                    <JWControl:DateBox ID="txtStartDate" Width="180px" CssClass="{required:true, messages:{required:'开始时间必须输入'}}" onblur="controlDate(this)" runat="server"></JWControl:DateBox>
                </td>
                </td>
            </tr>
            <tr>
                <td>
                    结束时间
                </td>
                <td class="elemTd mustInput" align="left">
                    <JWControl:DateBox ID="txtEndDate" Width="180px" CssClass="{required:true, messages:{required:'结束时间必须输入'}}" onblur="controlDate(this)" runat="server"></JWControl:DateBox>
                </td>
            </tr>
        </table>
        <div style="text-align: right; vertical-align: bottom; margin-top: 150px; margin-right: 35px;">
            <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
            <input type="button" value="取消" />
        </div>
    </div>
    <asp:HiddenField ID="hfldTreeSelVale" runat="server" />
    </form>
</body>
</html>
