<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotosList.aspx.cs" Inherits="EPC_ProjectCost_PhotosCheckIn_PhotosList" %>

<html>
<head runat="server"><title></title><link href="/StyleCss/PM4.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../../Web_Client/TreeNew.js"></script>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../Scripts/ui/effects.core.js"></script>

    <script type="text/javascript" src="../../Scripts/ui/effects.blind.js"></script>

    <script type="text/javascript" src="../../Scripts/ui/effects.fold.js"></script>

    <script type="text/javascript" src="../../Scripts/ui/effects.slide.js"></script>

    <script type="text/javascript" src="../../Scripts/ui/effects.transfer.js"></script>

    <script type="text/javascript">

        var json;
        var index;

        var IntendanceGuid = "<%=IntendanceGuid %>";
        var type = "<%=photoType %>";
        var jsCount = "<%=index %>";

        $(document).ready(function() {
            PageMethods.GetImages(IntendanceGuid,type, onSucceed, function(result) { alert("生成Json数据时错误!") });

        });
        function onSucceed(result) {
          
            json = eval(result);
            if (json != null) {
                for (var one in json) {
                    var imgName = json[one].PhotoName;
                    var imgPath = json[one].PhotoPath;
                    var PhotoExplain = json[one].PhotoExplain;
                    var PhotoNumber = json[one].PhotoNumber;

                }
           
                ImgClick(jsCount);
            } else {
                document.getElementById("btnNext").disabled = true;
                document.getElementById("btnPrevious").disabled = true;
            }

        }

        function ImgClick(index) {
            this.index = index;
            ChangeImg();
        }
        function Previous() {
            if (index == 0)//已到第一张
            {
                alert('已到第一张！');
            }
            else {
                if ($.browser.version == 6 && $.browser.msie) {
                    $("#imgBox").attr("style", "");
                }
                index = Number(index) - 1;
                ChangeImg();
            }
        }
        function Next() {
            index = Number(index) + 1
            if (index == Number(json.length))//已到最后一张
            {
                index = Number(index) - 1;
                alert('已到最后一张！');
            }
            else {
                if ($.browser.version == 6 && $.browser.msie) {
                    $("#imgBox").attr("style", "");
                }
                ChangeImg();
            }
        }


        function ChangeImg() {
         
            $("#imgBox").attr("style", "");
            $("#divContainer").attr("style", "text-align:center");
            imgSrc = json[index].PhotoPath + json[index].PhotoName;
            $("#divQuestion").html(json[index].ThumbnaName);
            $("#GPinfo").text(json[index].PhotoExplain);
            $("#lblNumber").text("照片编号：" + json[index].PhotoNumber);
            $("#imgBox").attr("src", imgSrc);
            $("#imgList").hide("fold", {}, 500, callback);
        }
        function callback() {
            $("#imgShow").show("blind", {}, 500, IsIE6);
        }
        function IsIE6() {
            var h = document.body.clientHeight - 230;
            var toolBoxWidth = document.body.clientWidth - 30;
            if ($("#imgBox").height() > h && $("#imgBox").width() > toolBoxWidth) {
                $("#imgBox").height(h);
                $("#divContainer").height(h);
                $("#divContainer").attr("style", " overflow-y: auto;border:1px solid blue;width:100%;text-align:center");

            } else if ($("#imgBox").width() > toolBoxWidth) {
                $("#imgBox").width(toolBoxWidth);
                $("#divContainer").width(toolBoxWidth);
                $("#divContainer").attr("style", "overflow-y: auto;border:1px solid blue;width:100%;text-align:center");
            } else if ($("#imgBox").height() > h) {
                $("#imgBox").height(h);
                $("#divContainer").height(h);
                $("#divContainer").attr("style", "overflow-y: auto;border:1px solid blue;width:100%;text-align:center");
            }

        }
        //自动播放
        function changImg2() {
            if (Number(index + 1) == Number(json.length))//已到最后一张
            {
                document.getElementById("btnPrevious").disabled = false;
                document.getElementById("btnNext").disabled = false;
                document.getElementById("btnPlay").disabled = false;
                document.getElementById("btnStop").disabled = true;
                clearInterval(id);
     
            }
            else {
                if ($.browser.version == 6 && $.browser.msie) {
                    $("#imgBox").attr("style", "");
                }
                index = Number(index) + 1;
                ChangeImg();

            }
        }
        var id;
        function play() {
            document.getElementById("btnPrevious").disabled = true;
            document.getElementById("btnNext").disabled = true;
            document.getElementById("btnPlay").disabled = true;
            document.getElementById("btnStop").disabled = false;
            if (Number(index + 1) == Number(json.length))//已到最后一张
            {
                index = -1;
            }
            id = setInterval(changImg2, 6000);
        }
        function playStop() {
            document.getElementById("btnPrevious").disabled = false;
            document.getElementById("btnNext").disabled = false;
            document.getElementById("btnPlay").disabled = false;
            document.getElementById("btnStop").disabled = true;
            clearInterval(id);
        }
    </script>

</head>
<body scroll="no">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    存在问题描述
    <div id="divQuestion" style="font-weight: bold; padding-top: 5px; border: 1px solid blue;
        width: 100%; height: 100px; text-align: left; overflow: auto">
        
    </div>
    <div id="Div1" style="margin: 2 2 2 2; padding-top: 5px; width: 99%; height: 6%;
        text-align: right;">
        <input id="btnPlay" onclick="play()" type="button" class="button-normal" value="自动播放" />
        <input id="btnStop" onclick="playStop()" type="button" class="button-normal" value="停止" />
        <input id="btnPrevious" onclick="Previous()" type="button" class="button-normal"
            value="前一张" />
        <input id="btnNext" onclick="Next()" type="button" class="button-normal" value="后一张" />
    </div>
    <div id="imgList" style="display: none; margin: 2 2 2 2; padding: 2; border: 2px solid blue;
        width: 99%; height: 99%; text-align: left;">
    </div>
    <div id="imgShow" style="margin: 2 2 2 2; padding: 2; width: 100%; text-align: left;
        overflow: outo">
        <div id="divContainer" style="border: 1px solid blue; width: 100%; text-align: center;">
            <img id="imgBox" class="imgBox" src="" /><br />
            <asp:Label ID="lblNumber" Text="" runat="server"></asp:Label>
        </div>
    </div>
    <div id="GPinfo" style="font-weight: bold; padding-top: 5px; border: 1px solid blue;
        width: 100%; height: 10%; text-align: left;">
        进度描述
    </div>
    <table style="width: 100%;">
        <tr align="right">
            <td align="right" class="td-submit">
                <input type="button" class="button-normal" value="关 闭" onclick="javascript:window.close();" />
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
