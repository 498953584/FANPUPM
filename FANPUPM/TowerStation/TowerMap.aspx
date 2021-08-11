<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TowerMap.aspx.cs" Inherits="TowerStation_TowerMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>塔站分布图</title>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&key=VXNBZ-J6NCJ-64AFO-FUZ5V-3C7Z3-NWFMZ"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/json2.js"></script>
    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
    <style type="text/css">
        html,
        body, form {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }

        .div {
        }

        h3 {
            padding-left: 10px;
        }

        span {
            position: absolute;
            left: 120px;
        }

        #container {
            width: 100%;
            height: 100%;
        }

        .SearchTool {
            width: 800px;
            position: absolute;
            /*background-color:red;*/
            z-index: 1999;
            top: 50px;
            left: 20px;
        }


        #left_1 {
            margin-top: 15px;
            margin-left: 10px;
            width: 300px;
            height: 200px;
            position: absolute;
            background-color: #cccccc;
            z-index: 1999;
            top: 150px;
            left: 20px;
        }

        #left_2 {
            margin-top: 15px;
            margin-left: 10px;
            width: 300px;
            height: 130px;
            position: absolute;
            background-color: #cccccc;
            z-index: 1999;
            top: 360px;
            left: 20px;
        }

        #left_3 {
            margin-top: 15px;
            margin-left: 10px;
            width: 300px;
            height: 130px;
            position: absolute;
            background-color: #cccccc;
            z-index: 1999;
            top: 500px;
            left: 20px;
        }

        #right_1 {
            margin-top: 15px;
            margin-left: 10px;
            width: 300px;
            height: 130px;
            position: absolute;
            background-color: #cccccc;
            z-index: 1999;
            top: 150px;
            right: 20px;
        }

        #right_2 {
            margin-top: 15px;
            margin-left: 10px;
            width: 300px;
            height: 130px;
            position: absolute;
            background-color: #cccccc;
            z-index: 1999;
            top: 290px;
            right: 20px;
        }

        #right_3 {
            margin-top: 15px;
            margin-left: 10px;
            width: 300px;
            height: 170px;
            position: absolute;
            background-color: #cccccc;
            z-index: 1999;
            top: 430px;
            right: 20px;
        }


        .btnclass {
            position: absolute;
            right: 25px;
            background-color: #569cd5;
            border-radius: 5px;
            border-style: hidden;
            width: 40px;
            color: white;
            
        }
        .btnclass:hover {cursor:hand;}
        .div {
            margin-top: 15px;
            margin-left: 10px;
        }
    </style>
    <script>
        var map = null;
        var marker = null;
        //初始化
        var bounds = null;//显示 范围
        var infoWindow = null;//信息框
        $(document).ready(function () {
            initMap();
            //initgys();
            InitMapData();
            InitTowerPositon();
        });
        function initMap() {
            var center = new TMap.LatLng(39.984104, 116.307503);
            //初始化地图
            map = new TMap.Map("container", {
                //rotation: 20,//设置地图旋转角度
                //pitch: 30, //设置俯仰角度（0~45）
                zoom: 12,//设置地图缩放级别
                center: center//设置地图中心点坐标
            });
            marker = new TMap.MultiMarker({
                id: 'marker-layer',
                map: map
            });
            bounds = new TMap.LatLngBounds();
            // 获取缩放控件实例
            control = map.getControl(TMap.constants.DEFAULT_CONTROL_ID.ZOOM);
            control.setPosition(TMap.constants.CONTROL_POSITION.BOTTOM_RIGHT);
            infoWindow = new TMap.InfoWindow({
                map: map,
                position: new TMap.LatLng(39.984104, 116.307503),
                offset: { x: 0, y: -32 } //设置信息窗相对position偏移像素
            });
            infoWindow.close();//初始关闭信息窗关闭
            //监听标注点击事件
            marker.on("click", function (evt) {
                //设置infoWindow
                infoWindow.open(); //打开信息窗
                infoWindow.setPosition(evt.geometry.position);//设置信息窗位置
                var html = "<div><p>塔站名称：" + evt.geometry.contentItem.Name + "</p><p>联系人：" +
                    evt.geometry.contentItem.Liaison + "</p><p>联系电话：" + evt.geometry.contentItem.Phone +
                    "</p><p>建设状态：" + evt.geometry.contentItem.BuildState +
                    "</p><p>是否国有：" + (evt.geometry.contentItem.IsStateOwned==1?"是":"否") + "</p><p>是否智能：" +
                        (evt.geometry.contentItem.IsIntelligence == 1 ? "是" : "否") + "</p></div>";
                infoWindow.setContent(html);//设置信息窗内容
            })
        }
        function initgys() {
            $.ajax({
                type: 'POST',
                async: true,
                url: '/TowerStation/handler/GetMapData.ashx?Action=GetGYS', //document.getElementById('dropprovince').value,
                success: function (str) {
                    $("#drop_gys").append("<option value=''>请选择</option>");
                    var strTemp = JSON.parse(str);
                    $(strTemp).each(function (index, item) {
                        $("#drop_gys").append("<option value='" + item.NetworkManufacturer + "'>" + item.NetworkManufacturer + "</option>");
                    });



                }
            });
        }
        //更新铁塔统计数据。
        function InitMapData() {
            var gys = $("#drop_gys").val();
            var networktype = $("#drop_networktype").val();
            $.ajax({
                type: 'POST',
                async: true,
                url: '/TowerStation/handler/GetMapData.ashx?Action=GetTowerTypeInfos&gys=' + gys + '&networktype=' + networktype, //document.getElementById('dropprovince').value,
                success: function (str) {

                    var strTemp = JSON.parse(str);
                    //"{"左上":[{"new":0,"old":0}],"新塔-国有情况":[{"非国有":0,"国有":0}],"旧塔-国有情况":[{"非国有":0,"国有":0}],"新塔-是否智能":[{"非智能":0,"智能":0}],"旧塔-是否智能":[{"非智能":0,"智能":0}],"新塔-建设状态":[{"已建成":0,"建设中":0,"规划中":0}]}"
                    $("#gy_new").text(strTemp.新塔国有情况[0].国有);
                    $("#fgy_new").text(strTemp.新塔国有情况[0].非国有);

                    $("#gy_old").text(strTemp.旧塔国有情况[0].国有);
                    $("#fgy_old").text(strTemp.旧塔国有情况[0].非国有);

                    $("#zn_new").text(strTemp.新塔是否智能[0].智能);
                    $("#fzn_new").text(strTemp.新塔是否智能[0].非智能);

                    $("#zn_old").text(strTemp.旧塔是否智能[0].智能);
                    $("#fzn_old").text(strTemp.旧塔是否智能[0].非智能);

                    $("#buildState_1").text(strTemp.新塔建设状态[0].已建成);
                    $("#buildState_2").text(strTemp.新塔建设状态[0].建设中);
                    $("#buildState_3").text(strTemp.新塔建设状态[0].规划中);



                }
            });
        }
        //地图铺点方法
        function InitTowerPositon() {
            marker.setGeometries([]);
            var gys = $("#drop_gys").val();
            var networktype = $("#drop_networktype").val();
            $.ajax({
                type: 'POST',
                async: true,
                url: '/TowerStation/handler/GetMapData.ashx?Action=GetTowerMapInfos&gys=' + gys + '&networktype=' + networktype, //document.getElementById('dropprovince').value,
                success: function (str) {
                    if (str.length > 0) {
                        var strTemp = JSON.parse(str);
                        $(strTemp).each(function (i, item) {
                            var posi = new TMap.LatLng(item.GPSDimension, item.GPSLongitude);
                            marker.add({
                                position: posi,
                                contentItem: item
                            });
                            if (bounds.isEmpty() || !bounds.contains(posi)) {
                                bounds.extend(posi);
                            }
                        });
                        //设置地图可视范围
                        map.fitBounds(bounds, {
                            padding: 100 // 自适应边距
                        });
                    }
                }
            });
        }
        //下拉框后面的查询 按钮。
        function SearchInfo() {
            bounds = new TMap.LatLngBounds();//重置可视范围
            InitMapData();
            InitTowerPositon();
        }

        //打开塔站信息列表
        //towertype   1为新塔站  2为旧塔站
        //BuildState  为建设状态
        //IsIntelligence 为判断 是否智能   0为非智能，1为智能
        //IsStateOwned字段 为判断 是否国有   0为非国有，1为国有
        //title为打开的标签页名称
        function openTowerStation(towertype, BuildState, IsIntelligence, IsStateOwned,title) {
            top.ui._TowerStationManage = window;
            var filter = 'towertype=' + towertype;
            if (BuildState)
                filter += "&BuildState=" + BuildState;
            if (IsIntelligence)
                filter += "&IsIntelligence=" + IsIntelligence;
            if (IsStateOwned)
                filter += "&IsStateOwned=" + IsStateOwned;


            var url = '/TowerStation/TowerStationManage.aspx?' + filter;
            
            toolbox_oncommand(url, title);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
        </div>
        <div class="SearchTool">
            <input type="text" placeholder="名称" style="width: 240px" />
            <input type="button" value="搜索" />
            &nbsp;&nbsp;&nbsp;&nbsp;<select id="drop_gys" runat="server" name="drop_gys"></select>
            <select id="drop_networktype" name="drop_networktype">
                <option value="">请选择</option>
                <option value="2G">2G</option>
                <option value="3G">3G</option>
                <option value="4G">4G</option>
                <option value="5G">5G</option>
                <option value="wifi">wifi</option>
                <option value="应急通讯">应急通讯</option>
                <option value="报警">报警</option>
            </select>
            <input type="button" onclick="SearchInfo();" value="搜索" />
            <input type="reset" value="重置" />
        </div>
        <div id="left_1">
            <h3>塔站统计</h3>
            <input class="btnclass" type="button" style="top: 22px; left: 130px; width: 60px;" onclick="openTowerStation(1,undefined,undefined,undefined,'新塔站信息')" value="新塔站" />
            <input class="btnclass" type="button" style="top: 22px; left: 215px; width: 60px;" onclick="openTowerStation(0,undefined,undefined,undefined,'旧塔站信息')" value="旧塔站" />
        </div>
        <div id="left_2">
            <h3>新塔站统计</h3>
            <div class="div">国有塔站：<span id="gy_new">111</span><input class="btnclass" onclick="openTowerStation(1,undefined,undefined,1,'新国有塔站信息')"  type="button" value="进入" /></div>
            <div class="div">非国有塔站：<span id="fgy_new">222</span><input class="btnclass" onclick="openTowerStation(1,undefined,undefined,0,'新非国有塔站信息')"  type="button" value="进入" /></div>
        </div>
        <div id="left_3">
            <h3>旧塔站统计</h3>
            <div class="div">国有塔站：<span id="gy_old">333</span><input class="btnclass"  onclick="openTowerStation(0,undefined,undefined,1,'旧国有塔站信息')"  type="button" value="进入" /></div>
            <div class="div">非国有塔站：<span id="fgy_old">444</span><input class="btnclass"  onclick="openTowerStation(0,undefined,undefined,0,'旧非国有塔站信息')"  type="button" value="进入" /></div>
        </div>
        <div id="right_1">
            <h3>新塔站统计</h3>
            <div class="div">智能塔站：<span id="zn_new"></span><input class="btnclass" onclick="openTowerStation(1,undefined,1,undefined,'新智能塔站信息')"  type="button" value="进入" /></div>
            <div class="div">非智能塔站：<span id="fzn_new"></span><input class="btnclass" onclick="openTowerStation(1,undefined,0,undefined,'新非智能塔站信息')"   type="button" value="进入" /></div>
        </div>
        <div id="right_2">
            <h3>旧塔站统计</h3>
            <div class="div">智能塔站：<span id="zn_old"></span><input class="btnclass"  onclick="openTowerStation(0,undefined,1,undefined,'旧智能塔站信息')"  type="button" value="进入" /></div>
            <div class="div">非智能塔站：<span id="fzn_old"></span><input class="btnclass"  onclick="openTowerStation(0,undefined,0,undefined,'旧非智能塔站信息')" type="button" value="进入" /></div>
        </div>
        <div id="right_3">
            <h3>新塔站状态统计</h3>
            <div class="div">已建成：<span id="buildState_1"></span><input class="btnclass" type="button" value="进入" /></div>
            <div class="div">建设中：<span id="buildState_2"></span><input class="btnclass" type="button" value="进入" /></div>
            <div class="div">规划中：<span id="buildState_3"></span><input class="btnclass" type="button" value="进入" /></div>
        </div>
    </form>
</body>
</html>
