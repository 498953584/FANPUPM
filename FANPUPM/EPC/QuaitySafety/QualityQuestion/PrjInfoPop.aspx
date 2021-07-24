<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjInfoPop.aspx.cs" Inherits="OPM_PM_PrjInfo_PrjInfoPop" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>项目快捷展示</title>
    <script src="../../../Script/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#box").animate({ height: 210 }, "slow");
            //   $("#divFooter").animate({ width: 260 }, "slow");
            $('.boxgrid.captionfull').hover(function () {
                $(".cover", this).stop().animate({ top: '124px' }, { queue: false, duration: 80 });
            }, function () {
                $(".cover", this).stop().animate({ top: '150px' }, { queue: false, duration: 80 });
            });
        });
        //关闭
        function btn_close_onclick() {
            window.parent.document.getElementById("ifrmjwDiv").style.display = "none";
        }
        //查看
        function ParentView() {
            //        try
            //        {
            //         window.parent.document.getElementById("btnQuery").onclick();
            //        }
            //        catch{
            //window.parent.document.getElementById("btnQuery").click();
            //        }

        }

        function ChangeImg(imgsrc, title, obj) {
            $("#imgshow").attr({ "src": imgsrc }, { "title": title });
        }
    </script>
    <style type="text/css">
        body
        {
            font-size: 12px;
            padding: 0px;
            margin: 0px;
        }
        #index div
        {
            padding: 0px 2px 0px 2px;
            margin-right: 10px;
            float: right;
            color: white;
            cursor: pointer;
            font-weight: bolder;
        }
        .boxgrid
        {
            width: 150px;
            height: 150px;
            margin: 1px 0 2px 2px;
            float: left;
            border: solid 2px #8399AF;
            overflow: hidden; /*position: relative; */
        }
        .boxcaption
        {
            float: left;
            position: absolute;
            background: #000;
            height: 30px;
            width: 100%;
            color: White;
            opacity: .5; /* For IE 5-7 */
            filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=50); /* For IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)";
        }
        .captionfull .boxcaption
        {
            top: 150;
            left: 0;
        }
        .caption .boxcaption
        {
            top: 100;
            left: 0;
        }
        h3 a
        {
            vertical-align: middle;
            color: White;
            font-size: 12px;
        }
    </style>
</head>
<body scroll="no">
    <div id="box" style="padding: 2px; background-color: #cadeed; width: 324px; height: 5;">
        <table width="320" border="0" style="background-color: white;">
            <tr>
                <td colspan="2" style="background-color: #cadeed">
                    <div style="cursor: pointer; float: right;">
                        <img id="Img1" src="Scripts/PrjInfomoveBmp/images/close.gif" onclick="btn_close_onclick()" /></div>
                    </td> </tr>
    <tr>
        <td width="150" align="center" style="">
            <div class="boxgrid captionfull">
                <img alt="" width="150" height="150" id="imgshow" <%=this.GetFirstImgstr %> />
                <div class="cover boxcaption" v>
                    <h3>
                        <%=this.PrjLink %></h3>
                </div>
            </div>
        </td>
        <td rowspan="2" width="150" style="vertical-align: top;">
            <textarea contenteditable="false" style="border: 1px solid #cadeed; width: 155; height: 175px;
                background-color: transparent; overflow-y: auto; overflow-x: hidden;"><%=this.Explain %></textarea>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="index" style="width: 150; background-color: #cadeed;">
                <%=this.GetImgstr %></div>
        </td>
        <td>
        </td>
    </tr>
    </table> </div>
</body>
</html>
