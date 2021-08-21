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
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/zDialog/zDialog.js"></script>
    <script type="text/javascript" src="../Script/zDialog/zDrag.js"></script>
        <script type="text/javascript" src="../Script/jquery.ui/jquery-ui-1.8.6.custom.js"></script>
    <script src="../Script/jquery.ui/jquery.ui.core.js"></script>
    <script src="../Script/jquery.ui/jquery.ui.widget.js"></script>
    <script src="../Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../Script/jquery.ui/jquery.ui.autocomplete.js"></script>
    <link rel="stylesheet" href="../Script/themes/jquery-ui.min.css">
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
            photoDivChange();
            var data = [
                
            ];

            var json = $("#hidAutocompleteValue").val();
            if (json) {
                data = $.parseJSON(json);
            }

            $('#TxtNetworkManufacturer').autocomplete({ source: data });
        });
        window.onload = function () {
            $("input[type=radio][name=PhotoType]").on('change', photoTypeChange);
            photoTypeChange();
        }

        function photoDivChange() {
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
        }

        //照片类型切换
        function photoTypeChange() {
            photoDivChange();
            var value = $('input:radio[name="PhotoType"]:checked').val();
            var img = document.getElementById(value.replace("PhotoType", "img"));
            if (img) {
                var rect = clacImgZoomParam(maxWidth, maxHeight, $(img).outerWidth(), $(img).outerHeight());
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

        var mylatLng = "";
        function btn_getlatLng() {
            mylatLng = ""
            var diag = new Dialog();
	        diag.Width = 800;
	        diag.Height = 450;
	        diag.Title = "请选择塔台坐标";
            diag.URL = "SelectMap.html";
            diag.ShowButtonRow=true;
            diag.CancelEvent = function () { diag.close(); };
            diag.OKEvent = function () {
                if (!mylatLng) {
                    alert("请选择塔台坐标！");
                    return;
                }
                var ary = mylatLng.split(",");
                form1.TxtGPSDimension.value = ary[0];
                form1.TxtGPSLongitude.value = ary[1];
                diag.close();
            }
	        diag.show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="hidAutocompleteValue"  runat="server" />
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
                                    <div id="preview360度全景拍摄" class="preview" style="display:none;">
                                        <img alt="" id="img360度全景拍摄" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview其他" class="preview" style="display:none;">
                                        <img alt="" id="img其他" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview地点入口" class="preview" style="display:none;">
                                        <img alt="" id="img地点入口" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview查看铁塔" class="preview" style="display:none;">
                                        <img alt="" id="img查看铁塔" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview电源" class="preview" style="display:none;">
                                        <img alt="" id="img电源" class="imgPhoto" src="" runat="server" />
                                    </div>
                                    <div id="preview展示天线负荷" class="preview" style="display:none;">
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
                    <td class="word">是否聚集区：
                    </td>
                    <td class="txt">
                        <asp:RadioButton ID="IsGatherArea1" GroupName="IsGatherArea" runat="server" Text="是" />
                        <asp:RadioButton ID="IsGatherArea0" GroupName="IsGatherArea" runat="server" Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="word">地点获取方式：
                    </td>
                    <td class="txt">
                        <asp:RadioButtonList ID="PlaceMode" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
                    </td>
                </tr>
                <tr>
                    <td class="word">建设状态：
                    </td>
                    <td class="txt">
                        <asp:RadioButtonList ID="BuildState" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                                <td> <button type="button" id="btn_Search" onclick="btn_getlatLng()">获取坐标</button></td>
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
                        <asp:RadioButtonList ID="BSTypeOfBS" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="BSBuildingUse" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
                    </td>
                </tr>
                <tr>
                    <td class="word">路面类型：
                    </td>
                    <td class="txt">
                        <asp:RadioButtonList ID="RoadType" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="PrivateRoadType" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="NeedImprove" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="BasicTerrain" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="BasicLandCategory" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="Obstacle" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="PlaceNeedImprove" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="ScPlanCableLayout" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
                    </td>
                    <td class="word">铁塔种类：
                    </td>
                    <td class="txt">
                        <asp:RadioButtonList ID="IronTowerClass" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="IronTowerType" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="LightingSubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
                    </td>
                </tr>
                <tr>
                    <td class="word">电力来源：
                    </td>
                    <td class="txt">
                        <asp:RadioButtonList ID="PowerSource" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                         <asp:RadioButtonList ID="NeedExtraDispose" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
                    </td>
                    <td class="word">监控子系统：
                    </td>
                    <td class="txt">
                        <asp:RadioButtonList ID="MonitorSubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                         <asp:RadioButtonList ID="SensingSubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="NetworkSubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
                       
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
                        <asp:RadioButtonList ID="OutdoorAdSubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="PowerSupplySubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
                        <asp:RadioButtonList ID="RSUSubsystem" runat="server" DataTextField="text" DataValueField="value" RepeatColumns="5"  RepeatDirection="Horizontal" />
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
