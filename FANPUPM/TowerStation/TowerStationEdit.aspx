<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TowerStationEdit.aspx.cs" Inherits="TowerStation_TowerStationEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <style type="text/css">
        h1 {
            font-size: 18px; /* 18px / 12px = 1.5 */
            font-weight: 900;
        }

        h2 {
            font-size: 16px;
            font-weight: 900;
        }

        h3 {
            font-size: 14px;
            font-weight: 900;
        }

        .word {
            width: 120px !important;
            text-align: left !important;
            white-space: nowrap;
        }

        .word1 {
            width: 50px;
            text-align: left;
            white-space: nowrap;
        }

        .word3 {
            width: 200px;
            text-align: left;
            white-space: nowrap;
        }

        .preview {
            width: 200px;
            height: 230px;
            border: 1px solid #B5CCDE;
            overflow: hidden;
        }

        .imgPhoto {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
        }
        /*上传头像*/
        #a-upon input {
            filter: alpha(opacity=0);
            opacity: 0;
            position: absolute;
            left: 0;
            top: 0;
        }

        #a-upon {
            position: relative;
            width: 200px;
            height: 100%;
            color: #888;
            cursor: pointer;
            overflow: hidden;
            border-radius: 0.2em;
            background: #fafafa;
            display: block;
            border: 1px solid #B5CCDE;
            text-decoration: none;
        }

            #a-upon:active {
                color: #444;
                background: #eee;
                border-color: #ccc;
                text-decoration: none
            }
    </style>
    <script type="text/javascript">
        var showTabIndex = 1;
        var maxWidth = 200;
        var maxHeight = 230;
        $(document).ready(function () {
            showTab(showTabIndex);
            var action = getRequestParam('mode');
            if (action === '3') {
                setAllInputDisabled();
                enabledInput("BtnBack");
                enabledInput("BtnNext");
                enabledInput("BtnNext");
                $("input[type=radio][name=PhotoType]").removeAttr("disabled");
            }

            $("input[type=radio][name=OutdoorAdSubsystem]").on('change', outdoorAdSubsystemChange);
            outdoorAdSubsystemChange();
            $("input[type=radio][name=PhotoType]").on('change', photoTypeChange);
            photoTypeChange();
        });

        //照片类型切换
        function photoTypeChange() {
            var value = $('input:radio[name="PhotoType"]:checked').val();
            $("#Fup360度全景拍摄").hide();
            $("#Fup其他").hide();
            $("#Fup地点入口").hide();
            $("#Fup查看铁塔").hide();
            $("#Fup电源").hide();
            $("#Fup展示天线负荷").hide();
            $(value.replace("PhotoType", "#Fup")).show();

            $("#preview360度全景拍摄").hide();
            $("#preview其他").hide();
            $("#preview地点入口").hide();
            $("#preview查看铁塔").hide();
            $("#preview电源").hide();
            $("#preview展示天线负荷").hide();
            $(value.replace("PhotoType", "#preview")).show();

            var img = document.getElementById(value.replace("PhotoType", "img"));
            if (img) {
                var rect = clacImgZoomParam(maxWidth, maxHeight, img.offsetWidth, img.offsetHeight);
                img.width = rect.width;
                img.height = rect.height;
                img.style.marginLeft = rect.left + 'px';
                img.style.marginTop = rect.top + 'px';
            }
        }

        function outdoorAdSubsystemChange() {
            var value = $('input:radio[name="OutdoorAdSubsystem"]:checked').val();
            if (value === "OutdoorAdSubsystem居住区") {
                $('#tbJuZhuQu').show();
                $('#tbZiYouMeiTi').hide();
            }
            else if (value === "OutdoorAdSubsystem自有媒体") {
                $('#tbZiYouMeiTi').show();
                $('#tbJuZhuQu').hide();
            } else {
                $('#tbZiYouMeiTi').hide();
                $('#tbJuZhuQu').hide();
            }
        }

        function showTab(index) {
            showTabIndex = index;
            $(".tableContent2").hide();
            $("#tb" + index).show();
            if (showTabIndex === 1) {
                $("#BtnBack").hide();
            } else {
                $("#BtnBack").show();
            }
            if (showTabIndex === 4) {
                $("#BtnNext").hide();
            } else {
                $("#BtnNext").show();
            }
        }
        function closeTab() {
            var action = getRequestParam('mode');
            if (action === "3") {
                top.ui.closeTab();
            }
            else if (confirm('信息还未填写完成，退出该页面，将不会保存已填信息，是否要继续？')) {
                top.ui.closeTab();
            }
        }

        function previewImage(file) {
            var div = document.getElementById(file.id.replace("Fup", "preview"));
            //var inputImage = document.getElementById("FupImage");
            var ext = file.value.substring(file.value.lastIndexOf(".") + 1).toLowerCase();
            var img;
            if (ext != 'png' && ext != 'jpg' && ext != 'jpeg') {
                alert("文件必须为图片！");
                file.value = "";
                return;
            }
            var name = file.id.replace("Fup", "img");
            if (file.files && file.files[0]) {
                div.innerHTML = '<img id=' + name + '>';
                img = document.getElementById(name);
                img.onload = function () {
                    var rect = clacImgZoomParam(maxWidth, maxHeight, img.offsetWidth, img.offsetHeight);
                    img.width = rect.width;
                    img.height = rect.height;
                    img.style.marginLeft = rect.left + 'px';
                    img.style.marginTop = rect.top + 'px';
                }
                var reader = new FileReader();
                reader.onload = function (evt) { img.src = evt.target.result; }
                reader.readAsDataURL(file.files[0]);
            }
            else {
                var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
                file.select();
                var src = document.selection.createRange().text;
                div.innerHTML = '<img id=' + name + ' class="imgPhoto">';
                img = document.getElementById(name);
                img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
                var rect = clacImgZoomParam(maxWidth, maxHeight, img.offsetWidth, img.offsetHeight);
                //status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
                div.innerHTML = "<div id=divPreview style='width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;margin-left:" + rect.left + "px;" + sFilter + src + "\"'></div>";
            }
        }
        function clacImgZoomParam(maxWidth, maxHeight, width, height) {
            var param = { top: 0, left: 0, width: width, height: height };
            if (width > maxWidth || height > maxHeight) {
                var rateWidth = width / maxWidth;
                var rateHeight = height / maxHeight;

                if (rateWidth > rateHeight) {
                    param.width = maxWidth;
                    param.height = Math.round(height / rateWidth);
                } else {
                    param.width = Math.round(width / rateHeight);
                    param.height = maxHeight;
                }
            }

            param.left = Math.round((maxWidth - param.width) / 2);
            param.top = Math.round((maxHeight - param.height) / 2);
            return param;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <div class="divContent2">
            <table class="tableContent2" id="tb1" cellspacing="0" cellpadding="5px" width="100%" border="0">
                <tr>
                    <td colspan="2" class="txt">
                        <h2>基本信息</h2>
                    </td>
                    <td colspan="2" class="txt">
                        <h2>照片记录</h2>
                    </td>
                </tr>
                <tr>
                    <td class="word">名称：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtName" Width="250px" Columns="21" MaxLength="200" runat="server"></asp:TextBox>
                    </td>
                    <td class="txt" colspan="2" rowspan="9">
                        <table>
                            <tr>
                                <td rowspan="6" style="width: 200px; padding-right: 8px;">
                                    <div id="preview360度全景拍摄" class="preview">
                                        <img alt="" id="img360度全景拍摄" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview其他" class="preview">
                                        <img alt="" id="img其他" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview地点入口" class="preview">
                                        <img alt="" id="img地点入口" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview查看铁塔" class="preview">
                                        <img alt="" id="img查看铁塔" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview电源" class="preview">
                                        <img alt="" id="img电源" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview展示天线负荷" class="preview">
                                        <img alt="" id="img展示天线负荷" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <a id="a-upon" title="双击这里上传">
                                        <asp:FileUpload ID="Fup360度全景拍摄" runat="server" onchange="previewImage(this)" accept="image/gif, image/jpeg, image/png, image/jpg" />
                                        <asp:FileUpload ID="Fup其他" runat="server" onchange="previewImage(this)" accept="image/gif, image/jpeg, image/png, image/jpg" Style="display: none;" />
                                        <asp:FileUpload ID="Fup地点入口" runat="server" onchange="previewImage(this)" accept="image/gif, image/jpeg, image/png, image/jpg" Style="display: none;" />
                                        <asp:FileUpload ID="Fup查看铁塔" runat="server" onchange="previewImage(this)" accept="image/gif, image/jpeg, image/png, image/jpg" Style="display: none;" />
                                        <asp:FileUpload ID="Fup电源" runat="server" onchange="previewImage(this)" accept="image/gif, image/jpeg, image/png, image/jpg" Style="display: none;" />
                                        <asp:FileUpload ID="Fup展示天线负荷" runat="server" onchange="previewImage(this)" accept="image/gif, image/jpeg, image/png, image/jpg" Style="display: none;" />双击这里上传
                                    </a>
                                </td>
                                <td>
                                    <asp:RadioButton ID="PhotoType360度全景拍摄" runat="server" Checked="True" GroupName="PhotoType" Text="360度全景拍摄" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="PhotoType其他" runat="server" GroupName="PhotoType" Text="其他" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="PhotoType地点入口" runat="server" GroupName="PhotoType" Text="地点入口" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="PhotoType查看铁塔" runat="server" GroupName="PhotoType" Text="查看铁塔" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="PhotoType电源" runat="server" GroupName="PhotoType" Text="电源" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="PhotoType展示天线负荷" runat="server" GroupName="PhotoType" Text="展示天线负荷" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="word">省：
                    </td>
                    <td class="txt">
                        <asp:DropDownList ID="DdlProvince" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="DdlProvince_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="word">地区（市）：
                    </td>
                    <td class="txt">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="DdlCity" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="DdlCity_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlProvince" EventName="SelectedIndexChanged" runat="server" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="word">区域：
                    </td>
                    <td class="txt">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="DdlArea" runat="server" Width="250px"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlProvince" EventName="SelectedIndexChanged" runat="server" />
                                <asp:AsyncPostBackTrigger ControlID="DdlCity" EventName="SelectedIndexChanged" runat="server" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="word">地址：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAddress" Width="250px" Columns="21" MaxLength="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">联系人：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtLiaison" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">电话号码：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtPhone" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">电邮地址：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtEmail" Width="250px" Columns="21" MaxLength="60" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">地点获取方式：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="PlaceMode购买" GroupName="PlaceMode" runat="server" Text="购买" />
                        <asp:RadioButton ID="PlaceMode租赁" GroupName="PlaceMode" runat="server" Text="租赁" />
                        <asp:RadioButton ID="PlaceMode共享" GroupName="PlaceMode" runat="server" Text="共享" />
                    </td>
                </tr>
                <tr>
                    <td class="word">建设状态：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="BuildState已建成" GroupName="BuildState" runat="server" Text="已建成" />
                        <asp:RadioButton ID="BuildState建设中" GroupName="BuildState" runat="server" Text="建设中" />
                        <asp:RadioButton ID="BuildState规划中" GroupName="BuildState" runat="server" Text="规划中" />
                    </td>
                    <td class="word">大致水平位置：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtHorizontalPosition" Width="250px" Columns="21" MaxLength="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">建设时间：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtBuildTime" Width="250px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">拍摄位置：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtShootingPosition" Width="250px" Columns="21" MaxLength="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">是否国有：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsStateOwned1" GroupName="IsStateOwned" runat="server" Text="是" />
                        <asp:RadioButton ID="IsStateOwned0" GroupName="IsStateOwned" runat="server" Text="否" />
                    </td>
                    <td class="word">离拔高度：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAltitude" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">是否智能：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsIntelligence1" GroupName="IsIntelligence" runat="server" Text="是" />
                        <asp:RadioButton ID="IsIntelligence0" GroupName="IsIntelligence" runat="server" Text="否" />
                    </td>
                    <td class="word">在地图上标记位置：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtMapPosition" Width="250px" Columns="21" MaxLength="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="txt">
                        <h2>地理信息</h2>
                    </td>
                    <td colspan="2" class="txt" rowspan="5"></td>
                </tr>
                <tr>
                    <td class="word">标称坐标：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>经度</td>
                                <td>
                                    <asp:TextBox ID="TxtNominalLongitude" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>维度</td>
                                <td>
                                    <asp:TextBox ID="TxtNominalDimension" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="word">GPS坐标：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>经度</td>
                                <td>
                                    <asp:TextBox ID="TxtGPSLongitude" Width="100px" runat="server"></asp:TextBox></td>
                                <td>维度</td>
                                <td>
                                    <asp:TextBox ID="TxtGPSDimension" Width="100px" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="word">地图坐标：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>经度</td>
                                <td>
                                    <asp:TextBox ID="TxtMapLongitude" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>维度</td>
                                <td>
                                    <asp:TextBox ID="TxtMapDimension" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="word">地图基准：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtMapDatum" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="tableContent2" id="tb2" cellspacing="0" cellpadding="5px" width="100%"
                border="0">
                <tr>
                    <td colspan="4" class="txt">
                        <h2>民用基础设施数据</h2>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <h3>1.地点入口道路</h3>
                    </td>
                    <td class="word">建筑结构类型：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="BSTypeOfBS钢筋混凝土" GroupName="BSTypeOfBS" runat="server" Text="钢筋混凝土" />
                        <asp:RadioButton ID="BSTypeOfBS钢" GroupName="BSTypeOfBS" runat="server" Text="钢" />
                        <asp:RadioButton ID="BSTypeOfBS其他" GroupName="BSTypeOfBS" runat="server" Text="其他" />
                    </td>
                </tr>
                <tr>
                    <td class="word">距离公路距离：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtRoadDistance" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米
                    </td>
                    <td class="word">建筑用途：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="BSBuildingUse住宅" GroupName="BSBuildingUse" runat="server" Text="住宅" />
                        <asp:RadioButton ID="BSBuildingUse办公" GroupName="BSBuildingUse" runat="server" Text="办公" />
                        <asp:RadioButton ID="BSBuildingUse商场商厂" GroupName="BSBuildingUse" runat="server" Text="商场/商厂" />
                        <asp:RadioButton ID="BSBuildingUse学校医院" GroupName="BSBuildingUse" runat="server" Text="学校/医院" />
                    </td>
                </tr>
                <tr>
                    <td class="word">路面类型：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="RoadType沥青" GroupName="RoadType" runat="server" Text="沥青" />
                        <asp:RadioButton ID="RoadType混凝土" GroupName="RoadType" runat="server" Text="混凝土" />
                        <asp:RadioButton ID="RoadType集料" GroupName="RoadType" runat="server" Text="集料" />
                        <asp:RadioButton ID="RoadType土路" GroupName="RoadType" runat="server" Text="土路" />
                    </td>
                    <td class="txt" colspan="2">
                        <h3>4.屋顶通道</h3>
                </tr>
                <tr>
                    <td class="word">私人道路长度：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtPrivateRoadLength" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米
                    </td>
                    <td class="word">需使用吊车：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsUseCrane1" GroupName="IsUseCrane" runat="server" Text="是" />
                        <asp:RadioButton ID="IsUseCrane0" GroupName="IsUseCrane" runat="server" Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="word">路面类型：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="PrivateRoadType沥青" GroupName="PrivateRoadType" runat="server" Text="沥青" />
                        <asp:RadioButton ID="PrivateRoadType混凝土" GroupName="PrivateRoadType" runat="server" Text="混凝土" />
                        <asp:RadioButton ID="PrivateRoadType集料" GroupName="PrivateRoadType" runat="server" Text="集料" />
                        <asp:RadioButton ID="PrivateRoadType土路" GroupName="PrivateRoadType" runat="server" Text="土路" />
                    </td>
                    <td class="word">楼梯可用：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsStairsAvailable1" GroupName="IsStairsAvailable" runat="server" Text="是" />
                        <asp:RadioButton ID="IsStairsAvailable0" GroupName="IsStairsAvailable" runat="server" Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="word">地面需改进的地：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="NeedImprove桥" GroupName="NeedImprove" runat="server" Text="桥" />
                        <asp:RadioButton ID="NeedImprove路面" GroupName="NeedImprove" runat="server" Text="路面" />
                        <asp:RadioButton ID="NeedImprove其他" GroupName="NeedImprove" runat="server" Text="其他" />
                    </td>
                    <td class="word">规格尺寸：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>宽度</td>
                                <td>
                                    <asp:TextBox ID="TxtSpecificationWidth" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                                <td>高度</td>
                                <td>
                                    <asp:TextBox ID="TxtSpecificationHeight" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <h3>2.绿地</h3>
                        <td class="word">客用电梯：
                        </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsPassengerElevator1" GroupName="IsPassengerElevator" runat="server" Text="是" />
                        <asp:RadioButton ID="IsPassengerElevator0" GroupName="IsPassengerElevator" runat="server" Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="word">基本地形：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="BasicTerrain平地" GroupName="BasicTerrain" runat="server" Text="平地" />
                        <asp:RadioButton ID="BasicTerrain湿地" GroupName="BasicTerrain" runat="server" Text="湿地" />
                        <asp:RadioButton ID="BasicTerrain斜坡" GroupName="BasicTerrain" runat="server" Text="斜坡" />
                    </td>
                    <td class="word">装载量：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtPeLoadingCapacity" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>人
                    </td>
                </tr>
                <tr>
                    <td class="word">土地基本类别：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="BasicLandCategory正常" GroupName="BasicLandCategory" runat="server" Text="正常" />
                        <asp:RadioButton ID="BasicLandCategory岩石" GroupName="BasicLandCategory" runat="server" Text="岩石" />
                        <asp:RadioButton ID="BasicLandCategory砂土" GroupName="BasicLandCategory" runat="server" Text="砂土" />
                        <asp:RadioButton ID="BasicLandCategory黏土" GroupName="BasicLandCategory" runat="server" Text="黏土" />
                    </td>
                    <td class="word">货物电梯：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsCargoElevator1" GroupName="IsCargoElevator" runat="server" Text="是" />
                        <asp:RadioButton ID="IsCargoElevator0" GroupName="IsCargoElevator" runat="server" Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="word">障碍：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="Obstacle无" GroupName="Obstacle" runat="server" Text="无" />
                        <asp:RadioButton ID="Obstacle树" GroupName="Obstacle" runat="server" Text="树" />
                        <asp:RadioButton ID="Obstacle建筑物" GroupName="Obstacle" runat="server" Text="建筑物" />
                    </td>
                    <td class="word">装载量：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtCeLoadingCapacity" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>Kg
                    </td>
                </tr>
                <tr>
                    <td class="word">地点需改进：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="PlaceNeedImprove清空" GroupName="PlaceNeedImprove" runat="server" Text="清空" />
                        <asp:RadioButton ID="PlaceNeedImprove拆除" GroupName="PlaceNeedImprove" runat="server" Text="拆除" />
                        <asp:RadioButton ID="PlaceNeedImprove切割填充" GroupName="PlaceNeedImprove" runat="server" Text="切割/填充" />
                        <asp:RadioButton ID="PlaceNeedImprove拆除现有" GroupName="PlaceNeedImprove" runat="server" Text="拆除现有" />
                    </td>
                    <td class="word">规格尺寸：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>宽度</td>
                                <td>
                                    <asp:TextBox ID="TxtCeSpecificationWidth" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                                <td>高度</td>
                                <td>
                                    <asp:TextBox ID="TxtCeSpecificationHeight" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <h3>3.屋顶建筑结构</h3>
                    </td>
                    <td class="txt" colspan="2">
                        <h3>5.需要额外支撑结构</h3>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <table>
                            <tr>
                                <td>高度：
                                </td>
                                <td class="txt">
                                    <asp:TextBox ID="TxtBSHeight" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米
                                </td>
                                <td>层数：
                                </td>
                                <td class="txt">
                                    <asp:TextBox ID="TxtBSLayersNum" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>层
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td class="word">衡量：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>高度</td>
                                <td>
                                    <asp:TextBox ID="TxtMeasureHeight" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <table>
                            <tr>
                                <td>类型：</td>
                                <td class="txt">
                                    <asp:TextBox ID="TxtBSType" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>年限：</td>
                                <td class="txt">
                                    <asp:TextBox ID="TxtBSYears" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>年</td>
                            </tr>
                        </table>
                    </td>
                    <td class="word">通用整梯：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>宽度</td>
                                <td>
                                    <asp:TextBox ID="TxtUniversalLadderWidth" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                                <td>高度</td>
                                <td>
                                    <asp:TextBox ID="TxtUniversalLadderHeight" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="tableContent2" id="tb3" cellspacing="0" cellpadding="5px" width="100%"
                border="0">
                <tr>
                    <td class="txt" colspan="2">
                        <h3>6.接地与建筑物防雷系统</h3>
                    </td>
                    <td class="txt" colspan="2">
                        <h3>9.高温通风/空调</h3>
                    </td>
                </tr>
                <tr>
                    <td class="word">是否接地：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsGrounded1" GroupName="IsGrounded" runat="server" Text="是" />
                        <asp:RadioButton ID="IsGrounded0" GroupName="IsGrounded" runat="server" Text="否" />
                    </td>
                    <td class="word">运营商名称：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAirconditionOperatorName" Width="250px" Columns="21" MaxLength="100" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">是否有使用的可能：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsPossibleToUse1" GroupName="IsPossibleToUse" runat="server" Text="是" />
                        <asp:RadioButton ID="IsPossibleToUse0" GroupName="IsPossibleToUse" runat="server" Text="否" />
                    </td>
                    <td class="word">空调型号：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAirconditionModel" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">主接地尺寸：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsMainGroundingSize1" GroupName="IsMainGroundingSize" runat="server" Text="是" />
                        <asp:RadioButton ID="IsMainGroundingSize0" GroupName="IsMainGroundingSize" runat="server" Text="否" />
                    </td>
                    <td class="word">空调品牌：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAirconditionBrand" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">是否防雷：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsLightningProtection1" GroupName="IsLightningProtection" runat="server" Text="是" />
                        <asp:RadioButton ID="IsLightningProtection0" GroupName="IsLightningProtection" runat="server" Text="否" />
                    </td>
                    <td class="word">空调序列号：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAirconditionSn" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">是否有使用的可能：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="LpIsPossibleToUse1" GroupName="LpIsPossibleToUse" runat="server" Text="是" />
                        <asp:RadioButton ID="LpIsPossibleToUse0" GroupName="LpIsPossibleToUse" runat="server" Text="否" />
                    </td>
                    <td class="word">空调能力：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtAirconditionCapacity" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2"></td>
                    <td class="word">空调状态：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="AirconditionState1" GroupName="AirconditionState" runat="server" Text="OK" />
                        <asp:RadioButton ID="AirconditionState0" GroupName="AirconditionState" runat="server" Text="NO" />
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <h3>7.轴与线缆布局</h3>
                    </td>
                    <td class="txt" colspan="2">
                        <h3>10.铁塔数据</h3>
                    </td>
                </tr>
                <tr>
                    <td class="word">轴、线缆是否接触：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="ScIsContact1" GroupName="ScIsContact" runat="server" Text="是" />
                        <asp:RadioButton ID="ScIsContact0" GroupName="ScIsContact" runat="server" Text="否" />
                    </td>
                    <td class="word">塔高：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtIronTowerHeight" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米
                    </td>
                </tr>
                <tr>
                    <td class="word">计划线缆布局：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="ScPlanCableLayout托盘" GroupName="ScPlanCableLayout" runat="server" Text="托盘" />
                        <asp:RadioButton ID="ScPlanCableLayout固定夹" GroupName="ScPlanCableLayout" runat="server" Text="固定夹" />
                        <asp:RadioButton ID="ScPlanCableLayout其他" GroupName="ScPlanCableLayout" runat="server" Text="其他" />
                    </td>
                    <td class="word">铁塔种类：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IronTowerClass绿地" GroupName="IronTowerClass" runat="server" Text="绿地" />
                        <asp:RadioButton ID="IronTowerClass屋顶" GroupName="IronTowerClass" runat="server" Text="屋顶" />
                        <asp:RadioButton ID="IronTowerClass现存塔" GroupName="IronTowerClass" runat="server" Text="现存塔" />
                    </td>
                </tr>
                <tr>
                    <td class="word">可用托盘数量：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>这次检查</td>
                                <td>
                                    <asp:TextBox ID="TxtCurrentAvailablePalletNum" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>个</td>
                                <td>上次检查</td>
                                <td>
                                    <asp:TextBox ID="TxtLastAvailablePalletNum" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>个</td>
                            </tr>
                        </table>
                    </td>
                    <td class="word">铁塔类型：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IronTowerType自支撑铁塔" GroupName="IronTowerType" runat="server" Text="自支撑铁塔" />
                        <asp:RadioButton ID="IronTowerType3脚" GroupName="IronTowerType" runat="server" Text="3脚" />
                        <asp:RadioButton ID="IronTowerType4脚" GroupName="IronTowerType" runat="server" Text="4脚" />
                        <asp:RadioButton ID="IronTowerType单极天线单管" GroupName="IronTowerType" runat="server" Text="单极天线/单管" />
                        <asp:RadioButton ID="IronTowerType拉线铁塔" GroupName="IronTowerType" runat="server" Text="拉线铁塔" />
                        <asp:RadioButton ID="IronTowerType其他" GroupName="IronTowerType" runat="server" Text="其他" />
                    </td>
                </tr>
                <tr>
                    <td class="word">线缆支线数量：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>这次检查</td>
                                <td>
                                    <asp:TextBox ID="TxtCurrentCableFeederNum" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>个</td>
                                <td>上次检查</td>
                                <td>
                                    <asp:TextBox ID="TxtLastCableFeederNum" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>个</td>
                            </tr>
                        </table>
                    </td>
                    <td class="word">铁塔位置结构：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td>海拔</td>
                                <td>
                                    <asp:TextBox ID="TxtIronTowerLocation" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>米</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="word">支线类型：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtFeederType" Width="250px" Columns="21" MaxLength="100" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">铁塔制造商：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtIronTowerManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="txt" colspan="2">
                        <h3>8.电力</h3>
                    </td>
                    <td class="word">照明子系统：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="LightingSubsystem高压钠灯" GroupName="LightingSubsystem" runat="server" Text="高压钠灯" />
                        <asp:RadioButton ID="LightingSubsystem荧光灯" GroupName="LightingSubsystem" runat="server" Text="荧光灯" />
                        <asp:RadioButton ID="LightingSubsystem发光二极管" GroupName="LightingSubsystem" runat="server" Text="发光二极管" />
                        <asp:RadioButton ID="LightingSubsystem陶瓷金属卤化物" GroupName="LightingSubsystem" runat="server" Text="陶瓷金属卤化物" /><br />
                        <asp:RadioButton ID="LightingSubsystem其他" GroupName="LightingSubsystem" runat="server" Text="其他" />
                        <asp:RadioButton ID="LightingSubsystem汇集网关" GroupName="LightingSubsystem" runat="server" Text="汇集网关" />
                        <asp:RadioButton ID="LightingSubsystem控制功能" GroupName="LightingSubsystem" runat="server" Text="控制功能" />
                        <asp:RadioButton ID="LightingSubsystem监控功能" GroupName="LightingSubsystem" runat="server" Text="监控功能" />
                    </td>
                </tr>
                <tr>
                    <td class="word">电力来源：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="PowerSource建筑物提供" GroupName="PowerSource" runat="server" Text="建筑物提供" />
                        <asp:RadioButton ID="PowerSource电力公司" GroupName="PowerSource" runat="server" Text="电力公司" />
                        <asp:RadioButton ID="PowerSource不可用" GroupName="PowerSource" runat="server" Text="不可用" />
                    </td>
                    <td class="word">照明系统制造商：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtLightingManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">需要额外配置：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="NeedExtraDispose电线杆" GroupName="NeedExtraDispose" runat="server" Text="电线杆" />
                        <asp:RadioButton ID="NeedExtraDispose线缆" GroupName="NeedExtraDispose" runat="server" Text="线缆" />
                        <asp:RadioButton ID="NeedExtraDispose变压器" GroupName="NeedExtraDispose" runat="server" Text="变压器" />
                        <asp:RadioButton ID="NeedExtraDispose其他" GroupName="NeedExtraDispose" runat="server" Text="其他" />
                    </td>
                    <td class="word">监控子系统：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="MonitorSubsystem视频摄像头" GroupName="MonitorSubsystem" runat="server" Text="视频摄像头" />
                        <asp:RadioButton ID="MonitorSubsystem数据传输网" GroupName="MonitorSubsystem" runat="server" Text="数据传输网" />
                        <asp:RadioButton ID="MonitorSubsystem软件平台" GroupName="MonitorSubsystem" runat="server" Text="软件平台" />
                        <asp:RadioButton ID="MonitorSubsystem公共安全机构" GroupName="MonitorSubsystem" runat="server" Text="公共安全机构" />
                        <asp:RadioButton ID="MonitorSubsystem产权方" GroupName="MonitorSubsystem" runat="server" Text="产权方" />
                        <asp:RadioButton ID="MonitorSubsystem其他" GroupName="MonitorSubsystem" runat="server" Text="其他" />
                    </td>
                </tr>
                <tr>
                    <td class="word" rowspan="3">其他信息：
                    </td>
                    <td class="txt">
                        <table>
                            <tr>
                                <td class="word1">型号</td>
                                <td>
                                    <asp:TextBox ID="TxtPowerModel" Width="150px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td class="word">监控系统制造商：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtMonitorManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="txt">
                        <table>
                            <tr>
                                <td class="word1">序列号</td>
                                <td>
                                    <asp:TextBox ID="TxtPowerSn" Width="150px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td class="word">传感子系统：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="NetworkSubsystem温度" GroupName="NetworkSubsystem" runat="server" Text="温度" />
                        <asp:RadioButton ID="NetworkSubsystem湿度" GroupName="NetworkSubsystem" runat="server" Text="湿度" />
                        <asp:RadioButton ID="NetworkSubsystem噪声" GroupName="NetworkSubsystem" runat="server" Text="噪声" />
                        <asp:RadioButton ID="NetworkSubsystem光照强度" GroupName="NetworkSubsystem" runat="server" Text="光照强度" />
                        <asp:RadioButton ID="NetworkSubsystem产权方" GroupName="NetworkSubsystem" runat="server" Text="产权方" />
                    </td>
                </tr>
                <tr>
                    <td class="txt">
                        <table>
                            <tr>
                                <td class="word1">状态</td>
                                <td>
                                    <asp:RadioButton ID="PowerState1" GroupName="PowerState" runat="server" Text="OK" />
                                    <asp:RadioButton ID="PowerState0" GroupName="PowerState" runat="server" Text="NO" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="word">传感系统制造商：
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="TxtSensingManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="tableContent2" id="tb4" cellspacing="0" cellpadding="5px" width="100%" style="margin-top: 20px;"
                border="0">
                <tr>
                    <td class="word3">网络服务子系统：
                    </td>
                    <td class="txt3">
                        <asp:RadioButton ID="NetworkSubsystemWIFI" GroupName="NetworkSubsystem" runat="server" Text="WIFI" />
                        <asp:RadioButton ID="NetworkSubsystem3G" GroupName="NetworkSubsystem" runat="server" Text="3G" />
                        <asp:RadioButton ID="NetworkSubsystem4G" GroupName="NetworkSubsystem" runat="server" Text="4G" />
                        <asp:RadioButton ID="NetworkSubsystem5G" GroupName="NetworkSubsystem" runat="server" Text="5G" />
                        <asp:RadioButton ID="NetworkSubsystem应急通讯" GroupName="NetworkSubsystem" runat="server" Text="应急通讯" />
                        <asp:RadioButton ID="NetworkSubsystem报警" GroupName="NetworkSubsystem" runat="server" Text="报警" />
                    </td>
                </tr>
                <tr>
                    <td class="word3">网络服务系统的制造商：
                    </td>
                    <td class="txt3">
                        <asp:TextBox ID="TxtNetworkManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word3">户外广告服务子系统：
                    </td>
                    <td class="txt3">
                        <asp:RadioButton ID="OutdoorAdSubsystem有灯" GroupName="OutdoorAdSubsystem" runat="server" Text="有灯" />
                        <asp:RadioButton ID="OutdoorAdSubsystem无灯" GroupName="OutdoorAdSubsystem" runat="server" Text="无灯" />
                        <asp:RadioButton ID="OutdoorAdSubsystem自有媒体" GroupName="OutdoorAdSubsystem" runat="server" Text="自有媒体" />
                        <asp:RadioButton ID="OutdoorAdSubsystem居住区" GroupName="OutdoorAdSubsystem" runat="server" Text="居住区" />
                        <table id="tbZiYouMeiTi">
                            <tr>
                                <td>可视距离</td>
                                <td>
                                    <asp:TextBox ID="TxtVisualDistance" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>人流量</td>
                                <td>
                                    <asp:TextBox ID="TxtVisitorsFlowrate" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>媒体规格</td>
                                <td>
                                    <asp:TextBox ID="TxtMediaSpecification" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>发布行业</td>
                                <td>
                                    <asp:TextBox ID="TxtPublishingIndustry" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table id="tbJuZhuQu">
                            <tr>
                                <td>离地高度</td>
                                <td>
                                    <asp:TextBox ID="TxtGroundClearance" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>车流量</td>
                                <td>
                                    <asp:TextBox ID="TxtVehicleFlowrate" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>投放周期</td>
                                <td>
                                    <asp:TextBox ID="TxtLaunchCycle" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td>发布品牌</td>
                                <td>
                                    <asp:TextBox ID="TxtReleaseBrand" Width="100px" Columns="21" MaxLength="50" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="word3">户外广告服务系统的制造商：
                    </td>
                    <td class="txt3">
                        <asp:TextBox ID="TxtOutdoorAdManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word3">充电供电子系统：
                    </td>
                    <td class="txt3">
                        <asp:RadioButton ID="PowerSupplySubsystem充电桩" GroupName="PowerSupplySubsystem" runat="server" Text="充电桩" />
                        <asp:RadioButton ID="PowerSupplySubsystem电动车换电柜" GroupName="PowerSupplySubsystem" runat="server" Text="电动车换电柜" />
                        <asp:RadioButton ID="PowerSupplySubsystem共享充电宝" GroupName="PowerSupplySubsystem" runat="server" Text="共享充电宝" />
                        <asp:RadioButton ID="PowerSupplySubsystem光伏子系统" GroupName="PowerSupplySubsystem" runat="server" Text="光伏子系统" />
                        <asp:RadioButton ID="PowerSupplySubsystem直流" GroupName="PowerSupplySubsystem" runat="server" Text="直流" />
                        <asp:RadioButton ID="PowerSupplySubsystem交流" GroupName="PowerSupplySubsystem" runat="server" Text="交流" />
                    </td>
                </tr>
                <tr>
                    <td class="word3">充电供电系统的制造商：
                    </td>
                    <td class="txt3">
                        <asp:TextBox ID="TxtPowerSupplyManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word3">RSU子系统：
                    </td>
                    <td class="txt3">
                        <asp:RadioButton ID="RSUSubsystem有限网关" GroupName="RSUSubsystem" runat="server" Text="有限网关" />
                        <asp:RadioButton ID="RSUSubsystem无线网络" GroupName="RSUSubsystem" runat="server" Text="无线网络" />
                        <asp:RadioButton ID="RSUSubsystem边缘计算" GroupName="RSUSubsystem" runat="server" Text="边缘计算" />
                        <asp:RadioButton ID="RSUSubsystem车联网系统" GroupName="RSUSubsystem" runat="server" Text="车联网系统" />
                    </td>
                </tr>
                <tr>
                    <td class="word3">RSU系统的制造商：
                    </td>
                    <td class="txt3">
                        <asp:TextBox ID="TxtRSUManufacturer" Width="250px" Columns="21" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="divFooter2">
                <table class="tableFooter2">
                    <tr>
                        <td class="td-submit" colspan="4">
                            <input class="button-normal" id="BtnBack" type="button" value="上一步" onclick="showTab(showTabIndex-1)" />&nbsp;
                            <input class="button-normal" id="BtnNext" type="button" value="下一步" onclick="showTab(showTabIndex+1)" />&nbsp;
                            <asp:Button ID="BtnSave" Text="保存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                            <input class="button-normal" id="btnCancel" type="button" value="取消" onclick="closeTab()" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
