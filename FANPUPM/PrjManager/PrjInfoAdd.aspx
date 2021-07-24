<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjInfoAdd.aspx.cs" Inherits="PrjManager_PrjInfoAdd" EnableEventValidation="false" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/Project/EngineeringType.js"></script>
    <script type="text/javascript" src="../Script/json2.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //根据hfldIsEditProjectCode的值判断是否允许编辑项目编码
            if ($('#hfldIsEditProjectCode').val() == '0') {
                $('#txtPrjCode').attr('readonly', 'readonly');
            } else if ($('#hfldIsEditProjectCode').val() == '1') {
                $('#txtPrjCode').removeAttr('readonly');
            }
            Val.validate('form1', 'btnSave');
            document.getElementById('divScroll').style.overflowY = 'auto';
            document.getElementById('divScroll').style.height = $(this).height() - 28 + "px";

            showEngType();      //显示工程类型
            // 绑定项目类型
            bindKind();
            // 绑定资质要求
            bindRank();

            // 添加项目类型
            $('#btn_add_PrjKindClass').click(addPrjKindClass);

            //添加工程类型事件
            $('#btn_editBuildingType').click(editBuildingType)

            // 添加资质要求
            $('#btn_add_rank').click(addRank);
            // 保存项目生成的编码
            $('#hfldPrjCode').val($('#txtPrjCode').val());
            //保存按钮前台事件
            $('#btnSave').click(save);
            // 保存项目类型的Json数据
            $('#btnSave').click(savePrjKindClass);
            // 保存资质要求的Json数据
            $('#btnSave').click(saveRank);
            $('#txtStartDate').blur(function () {
                controlDate(this);
            })
            $('#txtEndDate').blur(function () {
                controlDate(this);
            })

            // 判断是否子合同
            var prjId = jw.getRequestParam('PrjId');
            var Action = jw.getRequestParam('Action');
            if (prjId) {
                if (Action == 'Add') {
                    top.ui.show('您正在创建子合同');
                }
                else {
                    return;
                }
            }
        });


        // 添加项目类型
        function addPrjKindClass() {
            $('.prjKindClass').last().after(createKind());              // 添加到页面
        }

        // 保存项目类型的Json数据
        function savePrjKindClass() {
            var kindArr = new Array();
            $('.prjKindClass').each(function (i) {
                var kind = {
                    "PrjKind": $(this).find('select option:selected').val(),
                    "ProfessionalCost": $(this).find('input:eq(0)').val()
                };
                if (kind.ProfessionalCost == '') {
                    kind.ProfessionalCost = '0';
                }
                var ProfessionalCost = kind.ProfessionalCost.split(',').join('');
                kind.ProfessionalCost = ProfessionalCost;
                kind.ProfessionalCost = kind.ProfessionalCost || '0';
                if (kind.PrjKind) kindArr.push(kind);
            })

            var json = kindArr.length == 0 ? '' : JSON.stringify(kindArr);
            $('#hfldPrjKindJson').val(json);
        }

        // 添加资质要求
        function addRank() {
            $('.rank').last().after(createRank());              // 添加到页面
        }

        // 保存资质要求的Json数据
        function saveRank() {
            var rankArr = new Array();
            $('.rank').each(function (i) {
                var rank = {
                    "Rank": $(this).find('select option:selected').val(),
                    "RankLevel": $(this).find('input:eq(0)').val()
                };

                if (rank.Rank && rank.RankLevel) rankArr.push(rank);
            });

            var json = rankArr.length == 0 ? '' : JSON.stringify(rankArr);
            $('#hfldRankJson').val(json);
        }


        // 创建新的项目类型
        function createKind() {
            var $kind = $('#tr_PrjKindClass').clone();
            var len = $('.prjKindClass').length;
            $kind.find('*[id]').each(function () { this.id += '_' + len; }); // 初始化ID
            $kind.find('td:even').empty();          // 清空文字说明
            $kind.find('select').val('');     // 清空项目类型
            $kind.find('input').val('');     // 初始化专业造价
            //$kind.find('input').mouseleave = $kind.find('input').val(0);
            //$kind.find('input').addClass="decimal_input";           
            // 删除按钮
            $kind.find(':button').val('删除').click(function () {
                $(this).parent().parent().remove();
            });
            return $kind;
        }

        // 创建新的资质要求
        function createRank() {
            var $rank = $('#tr_rank').clone();
            var len = $('.rank').length;
            $rank.find('*[id]').each(function () { this.id += '_' + len; }); // 初始化ID
            $rank.find('td:even').empty();          // 清空文字说明
            $rank.find('input,select').val('');     // 清空数据
            // 删除按钮
            $rank.find(':button').val('删除').click(function () {
                $(this).parent().parent().remove();
            });
            return $rank;
        }

        // 绑定项目类型
        function bindKind() {
            var kindJson = $('#hfldPrjKindJson').val();
            if (!kindJson) return false;

            var kindArr = eval(kindJson);
            for (var i = 0; i < kindArr.length; i++) {
                if (i == 0) {
                    $('#dropPrjKindClass').val(kindArr[i].PrjKind);
                    $('#txtProfessionalCost').val(kindArr[i].ProfessionalCost);
                } else {
                    var $kind = createKind();
                    $kind.find('select:eq(0)').val(kindArr[i].PrjKind);
                    $kind.find('input:eq(0)').val(kindArr[i].ProfessionalCost);
                    $('.prjKindClass').last().after($kind);
                }
            }
        }

        // 绑定资质要求
        function bindRank() {
            var rankJson = $('#hfldRankJson').val();
            if (!rankJson) return false;

            var rankArr = eval(rankJson);
            for (var i = 0; i < rankArr.length; i++) {
                if (i == 0) {
                    $('#dropRank').val(rankArr[i].Rank);
                    $('#txtRankLevel').val(rankArr[i].RankLevel);
                } else {
                    var $rank = createRank();
                    $rank.find('select:eq(0)').val(rankArr[i].Rank);
                    $rank.find('input:eq(0)').val(rankArr[i].RankLevel);
                    $('.rank').last().after($rank);
                }
            }
        }

        // 建设单位
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtOwner', idinput: 'hfldOwner', func: function (corp) {
                $.ajax({
                    type: "GET",
                    async: false,
                    url: '/TenderManage/Handler/GetOwnerInfo.ashx?ownerName=' + encodeURI(corp.name),
                    success: function (datas) {
                        var data = eval(datas)[0];
                        if (data != undefined) {
                            $('#txtOwnerLinkMan').val(data.LinkMan);
                            $('#txtOwnerLinkManPhone').val(data.Telephone);
                            $('#txtOwnerAddress').val(data.Address);
                        }

                    }
                });
            }
            });
        }

        //选择人员
        function selectUser(id, name) {
            var func = null;
            if (id == 'hfldPrjPeople') {
                func = function () {
                    $.ajax({
                        type: "GET",
                        async: false,
                        url: '/TenderManage/Handler/GetUserInfo.ashx?usercode=' + $('#hfldPrjPeople').val(),
                        success: function (datas) {
                            var data = eval(datas)[0];
                            if (data != undefined) {
                                $('#txtCorp').val(data.CorpName);
                                $('#txtDuty').val(data.Duty);
                                $('#txtPhone').val(data.Phone);
                            }

                        }
                    });
                }
            } else if (id == 'hfldWorkUnit') {
                func = function () {
                    $.ajax({
                        type: "GET",
                        async: false,
                        url: '/TenderManage/Handler/GetUserPhone.ashx?usercode=' + $('#hfldWorkUnit').val(),
                        success: function (data) {
                            if (data != undefined) {
                                $('#txtTelphone').val(data);

                            }

                        }
                    });
                }
            }
            jw.selectOneUser({ nameinput: name, codeinput: id, func: func });
        }
        //取消按钮事件
        function CancelClick() {
            winclose('PrjInfoAdd.aspx', 'PrjInfoList.aspx', false);
        }
        function City_onchange() {
            var city = document.getElementById('dropcity');
            document.getElementById('hfldCity').value = city[city.selectedIndex].text;
            if (document.getElementById('dropcity').value) {
                $.ajax({
                    type: 'POST',
                    async: false,
                    url: '/TenderManage/Handler/GetCodeByProvinceCity.ashx?province=' + encodeURI($('#dropprovince option:selected').text()) + '&city=' + encodeURI($('#dropcity option:selected').text()),
                    success: function (str) {
                        if (str != '') {
                            $('#txtPrjCode').val(str);
                        } else {
                            $('#txtPrjCode').val($('#hfldPrjCode').val());
                        }
                    }
                });
            }
        }
        function bindCity() {
            if (document.getElementById('dropprovince').value != '') {
                $.ajax({
                    type: 'POST',
                    async: false,
                    url: '/TenderManage/Handler/GetCity.ashx?province=' + document.getElementById('dropprovince').value,
                    success: function (str) {
                        var strTemp = str.split('|');
                        for (var i = 0; i < strTemp.length; i++) {
                            if (strTemp[i] == "请选择") {
                                document.getElementById('dropcity').options[i] = new Option("请选择", "");
                            }
                            else {
                                document.getElementById('dropcity').options[i] = new Option(strTemp[i], i);
                            }


                        }
                        document.getElementById('dropcity').length = strTemp.length;
                        var city = document.getElementById('dropcity');
                        document.getElementById('hfldCity').value = city[city.selectedIndex].text;
                    }
                });

            }
            else {
                document.getElementById('dropcity').options[0] = new Option("请选择", "");
                document.getElementById('dropcity').length = 1;
            }
        }
        function Province_onchange() {
            bindCity();
        }
        //管理日期
        function controlDate(para) {
            var startDates = document.getElementById('txtStartDate').value;
            var startDateList = startDates.split('-');
            var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);

            var endDates = document.getElementById('txtEndDate').value;
            var endDatesList = endDates.split('-');
            var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);
            if (startDates != '') {
                if (startDate == 'NaN') {
                    $.messager.alert('系统提示', '计划开始日期输入日期格式不正确！');
                    para.value = '';
                    return;
                }
            }
            if (endDates != '') {
                if (endDate == 'NaN') {
                    $.messager.alert('系统提示', '计划结束日期输入日期格式不正确！');
                    para.value = '';
                    return;
                }
            }
            if (startDates != '' && endDates != '') {
                if (endDate - startDate < 0) {
                    para.value = '';
                    $.messager.alert('系统提示', '计划结束日期不能小于计划开始日期，请重新选择日期！');
                }
            }
        }
        //限制输入字符长度
        function limitTextLenth(txtId) {
            var txtValue = txtId.value;
            if (txtValue.length > 100) {
                txtId.value = txtValue.substring(0, 100);
                $.messager.alert('系统提示', '输入的字数不能大于100个字');
            }
        }
    </script>
</head>
<body>
    <form id="form1" style="overflow: hidden" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr style="height: 100%;">
            <td style="height: 100%; vertical-align: top;">
                <div id="divScroll" runat="server">
                    <table width="100%" height="100%" cellpadding="0" cellspacing="0" id="tb">
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto; text-align: center">
                                    <legend>立项人基本资料</legend>
                                    <table id="table5" class="tableContent2" style="width: 95%" cellpadding="5px" cellspacing="0">
                                        <tr>
                                            <td class="word">
                                                立项申请人
                                            </td>
                                            <td class="txt" style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 99%;">
                                                    <asp:TextBox ID="txtName" Style="width: 89%; height: 15px; border: none;
                                                        line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldPrjPeople','txtName');" runat="server" />

                                                </span>
                                                <input id="hfldPrjPeople" type="hidden" style="width: 1px" runat="server" />

                                            </td>
                                            <td class="word">
                                                部门/单位
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtCorp" MaxLength="100" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                岗位
                                            </td>
                                            <td class="txt ">
                                                <asp:TextBox ID="txtDuty" MaxLength="100" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                联系方式
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto; text-align: center">
                                    <legend>建设单位基本资料</legend>
                                    <table id="table_owner" class="tableContent2" style="width: 95%" cellpadding="5px"
                                        cellspacing="0">
                                        <tr>
                                            <td class="word">
                                                建设单位
                                            </td>
                                            <td class="txt" style="padding-right: 3px">
                                                <span class="spanSelect" style="width: 99%;">
                                                    <asp:TextBox ID="txtOwner" Style="width: 89%; height: 15px; border: none; line-height: 16px;
                                                        margin: 1px  2px;" runat="server"></asp:TextBox>
                                                    <img alt="选择乙方" id="imgOwner" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" runat="server" />

                                                </span>
                                                <asp:HiddenField ID="hfldOwner" runat="server" />
                                            </td>
                                            <td class="word">
                                                联系人
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtOwnerLinkMan" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                联系方式
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtOwnerLinkManPhone" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                联系地址
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtOwnerAddress" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto; text-align: center">
                                    <legend>基本内容</legend>
                                    <table id="tableContent" class="tableContent2" style="width: 95%" cellpadding="5px"
                                        cellspacing="0">
                                        <tr>
                                            <td class="word">
                                                项目编号
                                            </td>
                                            <td class="txt mustInput">
                                                <input type="text" id="txtPrjCode" readonly="readonly" class="{required:true, messages:{required:'项目编号必须输入'}}" runat="server" />

                                            </td>
                                            <td class="word">
                                                项目名称
                                            </td>
                                            <td class="txt mustInput">
                                                <asp:TextBox ID="txtPrjName" CssClass="{required:true, messages:{required:'项目名称必须输入'}}" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                计划开始日期
                                            </td>
                                            <td class="txt  mustInput">
                                                <asp:TextBox ID="txtStartDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'计划开始日期必须输入'}}" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                计划结束日期
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtEndDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                所属区域
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropprovince" Width="40%" onchange="Province_onchange()" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="dropcity" Width="40%" onchange="City_onchange()" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                项目地点
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPrjPlace" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                工程工期
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtDuration" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                工程量估算
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtEngineeringEstimates" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                工程造价
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPrjCost" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                预测利润率
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtForecastProfitRate" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr_PrjKindClass" class="prjKindClass">
                                            <td class="word">
                                                项目类型
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                专业造价
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtProfessionalCost" class="decimal_input" Width="80%" runat="server"></asp:TextBox>
                                                <input id="btn_add_PrjKindClass" type="button" value="添加" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                质量等级
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropQualityClass" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                设计单位
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtDesigner" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目属性
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropProperty" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                项目审核情况
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtApprovalOf" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                监理单位
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtInspector" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                实施项目部
                                            </td>
                                            <td class="txt ">
                                                <asp:DropDownList ID="dropXmgroup" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                经办人
                                            </td>
                                            <td class="txt" style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 99%;">
                                                    <asp:TextBox ID="txtAgent" Style="width: 89%; height: 15px; border: none;
                                                        line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgAgent" onclick="selectUser('hfldAgent','txtAgent');" runat="server" />

                                                </span>
                                                <input id="hfldAgent" type="hidden" style="width: 1px" runat="server" />

                                            </td>
                                            <td class="word">
                                                项目跟踪人
                                            </td>
                                            <td class="txt" style="padding-right: 3px">
                                                <span class="spanSelect" style="width: 99%;">
                                                    <asp:TextBox ID="txtDutyPerson" Style="width: 89%; height: 15px; border: none;
                                                        line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img3" onclick="selectUser('hfldDutyPerson','txtDutyPerson');" runat="server" />

                                                </span>
                                                <input id="hfldDutyPerson" type="hidden" style="width: 1px" runat="server" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目经理要求
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtPrjManagerRequire" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                技术负责人要求
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtTechnicalLeaderRequire" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目简介
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtPrjInfo" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                信息来源
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtInfoOrigin" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                其他特殊要求
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtElseRequest" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                备注
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtRemark1" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                相关附件
                                            </td>
                                            <td colspan="3">
                                                <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>商务要求</legend>
                                    <table class="tableContent2" cellpadding="5px" style="width: 95%" cellspacing="0">
                                        <tr id="tr_rank" class="rank">
                                            <td class="word">
                                                资质要求
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropRank" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                资质等级
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtRankLevel" Width="80%" runat="server"></asp:TextBox>
                                                <input id="btn_add_rank" type="button" value="添加" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                承包方式
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropContractWay" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                付款条件
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropPayCondition" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                招标形式
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropTenderWay" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                结算方式
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropPayWay" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                预算方式
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropBudgetWay" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="word">
                                                重要程度
                                            </td>
                                            <td class="txt">
                                                <asp:DropDownList ID="dropKeyPart" Width="100%" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目经理
                                            </td>
                                            <td class="txt " style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 99%;">
                                                    <asp:TextBox ID="txtPrjManager" Style="width: 89%; height: 15px; border: none;
                                                        line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser('hfldPrjManager','txtPrjManager');" />
                                                </span>
                                                <input id="hfldPrjManager" type="hidden" style="width: 1px" runat="server" />

                                            </td>
                                            <td class="word">
                                                业务经理
                                            </td>
                                            <td class="txt " style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 99%;">
                                                    <asp:TextBox ID="txtBusinessman" Style="width: 89%; height: 15px;
                                                        border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img2" onclick="selectUser('hfldBusinessman','txtBusinessman');" />
                                                </span>
                                                <input id="hfldBusinessman" type="hidden" style="width: 1px" runat="server" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                主要负责人
                                            </td>
                                            <td class="txt " style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 99%">
                                                    <asp:TextBox ID="txtWorkUnit" Style="width: 89%; height: 15px; border: none; line-height: 16px;
                                                        margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                    <img alt="选择乙方" onclick="selectUser('hfldWorkUnit', 'txtWorkUnit');" src="../../images/icon.bmp" style="float: right;" id="imgWorkUnit" runat="server" />

                                                </span>
                                                <asp:HiddenField ID="hfldWorkUnit" runat="server" />
                                            </td>
                                            <td class="word">
                                                主要负责人联系方式
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtTelphone" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                            </td>
                                            <td class="txt " style="padding-right: 3px;">
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>补充内容</legend>
                                    <table class="tableContent2" cellpadding="5px" style="width: 95%" cellspacing="0">
                                        <tr id="tr_engType" class="tr_engType">
                                            <td class="word">
                                                工程类型
                                            </td>
                                            <td colspan="3" style="white-space: nowrap;">
                                                <asp:DropDownList ID="dropBuildingType_0" Width="40%" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="dropBuildingSubType_0" Width="40%" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtDetailGrade_0" Width="10%" runat="server"></asp:TextBox>级
                                                <input type="button" id="btn_editBuildingType" value="添加" count="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                规模
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtTotalHouseNum" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                等级
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtBuildingTypeNo" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onblur="this.value=this.value.replace(/[^\d]/g,'') " runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                资金来源
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPrjFundInfo" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                资金落实情况
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtFundWorkable" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                建筑面积
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtBuildingArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                占地面积
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtUsegrounArea" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                地下面积
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtUndergroundArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                阅知人
                                            </td>
                                            <td class="txt " style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 99%">
                                                    <asp:TextBox ID="txtPrjReadOne" ReadOnly="true" Style="width: 89%; height: 15px;
                                                        border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                    <img alt="选择阅知人" onclick="selectUser('hfldPrjReadOne', 'txtPrjReadOne');" src="../../images/icon.bmp" style="float: right;" id="img4" runat="server" />

                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                绿化面积
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtAfforestArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                园林面积
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtParkArea" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                其他说明
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtOtherStatement" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>管理参数</legend>
                                    <table class="tableContent2" style="width: 95%" cellpadding="5px" cellspacing="0">
                                        <tr>
                                            <td class="word">
                                                管理保证金
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtManagementMargin" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                民工工资保证金
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtMigrantQualityMarginRate" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                预扣税率
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtWithholdingTaxRate" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                履约保证金
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPerformanceBond" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                其他（保证金）
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtElseMargin" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                            </td>
                                            <td class="txt">
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="Div1" class="divFooter2" runat="server">
                    <table class="tableFooter2">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                                <input type="button" id="btnCancel" value="取消" onclick="CancelClick()" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="xx" style="display: none">
        <input type="text" id="txt1" /></div>
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="dropvalue" runat="server" />
    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
    <asp:HiddenField ID="hfldCity" runat="server" />
    <!-- 存放工程类型的Json-->
    <asp:HiddenField ID="hfldEngTypeJson" runat="server" />
    <!-- 存储项目阅知人-->
    <asp:HiddenField ID="hfldPrjReadOne" runat="server" />
    <!-- 存储项目类型的Json-->
    <asp:HiddenField ID="hfldPrjKindJson" runat="server" />
    <!-- 存储资质要求的Json-->
    <asp:HiddenField ID="hfldRankJson" runat="server" />
    <!-- 项目状态-->
    <asp:HiddenField ID="hfldPrjState" runat="server" />
    <!-- 存储系统生成的项目编码-->
    <asp:HiddenField ID="hfldPrjCode" runat="server" />
    <asp:HiddenField ID="hfldIsEditProjectCode" runat="server" />
    <!-- 放到此处解决与上传附件的用户控件样式冲突的问题-->
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
