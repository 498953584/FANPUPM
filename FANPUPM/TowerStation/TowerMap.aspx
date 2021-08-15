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

        .left-list {
            position: absolute;
            z-index: 1999;
            top: 100px;
            left: 20px;
        }

        .car {
            box-shadow: 1px 1px 8px 0px #c8c8c8;
            border-radius: 5px;
            background-color: #FFF;
            padding: 15px;
        }

        #left_1 {
            margin-top: 15px;
            margin-left: 10px;
            width: 200px;
        }

        #left_2 {
            margin-top: 15px;
            margin-left: 10px;
            width: 200px;
        }

        #left_3 {
            margin-top: 15px;
            margin-left: 10px;
            width: 200px;
        }

        .right-list {
            position: absolute;
            z-index: 1999;
            top: 100px;
            right: 20px;
        }

        #right_1 {
            margin-top: 15px;
            margin-left: 10px;
            width: 200px;
        }

        #right_2 {
            margin-top: 15px;
            margin-left: 10px;
            width: 200px;
        }

        #right_3 {
            margin-top: 15px;
            margin-left: 10px;
            width: 200px;
        }


        .btnclass {
            background-color: #317ffe;
            border-radius: 5px;
            border-style: hidden;
            width: 40px;
            color: white;
        }

        .div {
            margin-top: 15px;
            margin-left: 10px;
        }

        .btn {
            border-radius: 3px;
            background-color: #317ffe;
            border: none;
            padding: 5px 20px;
            color: #FFFFFF;
        }

        .car-title {
            color: #317fff;
            font-size: 15px;
            margin: 0;
        }

        .flex {
            display: flex;
            justify-content: center;
        }

        .col {
            flex-basis: 50%;
            text-align: center;
        }

            .col .num {
                font-size: 25px;
                font-weight: bold;
                color: #666;
                margin: 10px;
                margin-top: 15px;
            }

            .col .typeName {
                color: #999;
                font-size: 13px;
                font-weight: 600;
                margin-top: 0;
            }

        .col30 {
            flex-basis: 33%;
            text-align: center;
        }

            .col30 .num {
                font-size: 25px;
                font-weight: bold;
                color: #666;
                margin: 10px;
                margin-top: 15px;
            }

            .col30 .typeName {
                color: #999;
                font-size: 13px;
                font-weight: 600;
                margin-top: 0;
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
				var bulidstate="";
				if(evt.geometry.contentItem.BuildState=="1")
					bulidstate="已建成";
				else if(evt.geometry.contentItem.BuildState=="2")
					bulidstate="建设中";
				else if(evt.geometry.contentItem.BuildState=="3")
					bulidstate="规划中";
                //设置infoWindow
                infoWindow.open(); //打开信息窗
                infoWindow.setPosition(evt.geometry.position);//设置信息窗位置
                var html = "<div><p>塔站名称：" + evt.geometry.contentItem.Name + "</p><p>联系人：" +
                    evt.geometry.contentItem.Liaison + "</p><p>联系电话：" + evt.geometry.contentItem.Phone +
                    "</p><p>建设状态：" + bulidstate +
                    "</p><p>是否国有：" + (evt.geometry.contentItem.IsStateOwned == 1 ? "是" : "否") + "</p><p>是否智能：" +
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
                    $("#num_new").text(strTemp.左上[0].new);
                    $("#num_old").text(strTemp.左上[0].old);

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
        //BuildState  为建设状态   1为已建成   2为建设中  3为规划中
        //IsIntelligence 为判断 是否智能   0为非智能，1为智能
        //IsStateOwned字段 为判断 是否国有   0为非国有，1为国有
        //title为打开的标签页名称
        function openTowerStation(towertype, BuildState, IsIntelligence, IsStateOwned, title) {
            top.ui._TowerStationManage = window;
            var filter = 'towertype=' + towertype;
            if (typeof (BuildState) != 'undefined')
                filter += "&BuildState=" + BuildState;
            if (typeof (IsIntelligence) != 'undefined')
                filter += "&IsIntelligence=" + IsIntelligence;
            if (typeof (IsStateOwned) != 'undefined')
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
            <input type="text" placeholder="名称" style="width: 240px; height: 28px; border: 0; outline: none; box-shadow: 1px 1px 3px 0px #c8c8c8; border-radius: 3px;" />
            <button type="button" class="btn">
                <img src="sousou.png" width="15px" />
            </button>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <div id="" style="background-color: #FFFFFF; display: inline-block; box-shadow: 1px 1px 3px 0px #c8c8c8; border-radius: 3px; padding: 0 10px;">
                <div id="" style="background-color: #FFFFFF; display: inline-block;">

                    <select id="drop_gys" runat="server" name="drop_gys"
                        style="border: none; background-color: #FFFFFF; padding-right: 15px; outline: none;">
                    </select>

                </div>
                <!-- <div id="" style="display: inline-block;">
            <div style="width: 1px;background-color: #333333;"></div>
        </div> -->
                <div id="" style="background-color: #FFFFFF; display: inline-block; padding: 5px;">
                    <select id="drop_networktype" name="drop_networktype" runat="server"
                        style="border: none; padding-right: 5px; border-left: solid 1px #bababa; outline: none;">
                        <option value="">请选择</option>
                        <option value="2G">2G</option>
                        <option value="3G">3G</option>
                        <option value="4G">4G</option>
                        <option value="5G">5G</option>
                        <option value="wifi">wifi</option>
                        <option value="应急通讯">应急通讯</option>
                        <option value="报警">报警</option>
                    </select>
                </div>
            </div>


            <button type="button" style="" onclick="SearchInfo()" class="btn">
                搜索
            </button>
            <button type="reset" class="btn">
                重置
            </button>
        </div>
        <div class="left-list">
            <div id="left_1" class="car">
                <div id="" style="display: flex; align-items: center;">
                    <img src="statistics.png" style="width: 17px; height: 17px;">
                    <h3 class="car-title">塔站统计</h3>
                </div>
                <div id="" class="flex">
                    <div id="" class="col">
                        <p id="num_new" class="num"></p>
                        <p class="typeName">新塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,undefined,undefined,undefined,'新塔站信息')" value="进入" />
                    </div>
                    <div id="" class="col">
                        <p id="num_old" class="num"></p>
                        <p class="typeName">旧塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(0,undefined,undefined,undefined,'旧塔站信息')" value="进入" />
                    </div>
                </div>
            </div>

            <div id="left_2" class="car">
                <div id="" style="display: flex; align-items: center;">
                    <img src="statistics.png" style="width: 17px; height: 17px;">
                    <h3 class="car-title">新塔站统计</h3>
                </div>
                <div id="" class="flex">
                    <div id="" class="col">
                        <p id="gy_new" class="num"></p>
                        <p class="typeName">国有塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,undefined,undefined,1,'新国有塔站信息')" value="进入" />
                    </div>
                    <div id="" class="col">
                        <p id="fgy_new" class="num"></p>
                        <p class="typeName">非国有塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,undefined,undefined,0,'新非国有塔站信息')" value="进入" />
                    </div>
                </div>
            </div>
            <div id="left_3" class="car">
                <div id="" style="display: flex; align-items: center;">
                    <img src="statistics.png" style="width: 17px; height: 17px;">
                    <h3 class="car-title">旧塔站统计</h3>
                </div>
                <div id="" class="flex">
                    <div id="" class="col">
                        <p id="gy_old" class="num"></p>
                        <p class="typeName">国有塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(0,undefined,undefined,1,'旧国有塔站信息')" value="进入" />
                    </div>
                    <div id="" class="col">
                        <p id="fgy_old" class="num"></p>
                        <p class="typeName">非国有塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(0,undefined,undefined,0,'旧非国有塔站信息')" value="进入" />
                    </div>
                </div>
            </div>
        </div>

        <div id="" class="right-list">
            <div id="right_1" class="car">
                <div id="" style="display: flex; align-items: center;">
                    <img src="statistics.png" style="width: 17px; height: 17px;">
                    <h3 class="car-title">新塔站统计</h3>
                </div>
                <div id="" class="flex">
                    <div id="" class="col">
                        <p id="zn_new" class="num"></p>
                        <p class="typeName">智能塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,undefined,1,undefined,'新智能塔站信息')" value="进入" />
                    </div>
                    <div id="" class="col">
                        <p id="fzn_new" class="num"></p>
                        <p class="typeName">非智能塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,undefined,0,undefined,'新非智能塔站信息')" value="进入" />
                    </div>
                </div>
            </div>
            <div id="right_2" class="car">
                <div id="" style="display: flex; align-items: center;">
                    <img src="statistics.png" style="width: 17px; height: 17px;">
                    <h3 class="car-title">旧塔站统计</h3>
                </div>
                <div id="" class="flex">
                    <div id="" class="col">
                        <p id="zn_old" class="num"></p>
                        <p class="typeName">智能塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(0,undefined,1,undefined,'旧智能塔站信息')" value="进入" />
                    </div>
                    <div id="" class="col">
                        <p id="fzn_old" class="num"></p>
                        <p class="typeName">非智能塔站</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,undefined,0,undefined,'新非智能塔站信息')" value="进入" />
                    </div>
                </div>
            </div>
            <div id="right_3" class="car">
                <div id="" style="display: flex; align-items: center;">
                    <img src="statistics.png" style="width: 17px; height: 17px;" />
                    <h3 class="car-title">新塔站状态统计</h3>
                </div>
                <div id="" class="flex">
                    <div id="" class="col30">
                        <p id="buildState_1" class="num"></p>
                        <p class="typeName">已建成</p>
                        <input class="btnclass" onclick="openTowerStation(1,1,undefined,undefined,'新塔-已建成信息')" type="button" value="进入" />
                    </div>
                    <div id="" class="col30">
                        <p id="buildState_2" class="num"></p>
                        <p class="typeName">建设中</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,2,undefined,undefined,'新塔-建设中信息')" value="进入" />
                    </div>
                    <div id="" class="col30">
                        <p id="buildState_3" class="num"></p>
                        <p class="typeName">规划中</p>
                        <input class="btnclass" type="button" onclick="openTowerStation(1,3,undefined,undefined,'新塔-规划中信息')" value="进入" />
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
