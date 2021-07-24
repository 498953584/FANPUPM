<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoAdd.aspx.cs" Inherits="BudgetManage_TenderManage_InfoAdd" EnableEventValidation="false" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/Project/EngineeringType.js"></script>
    <script type="text/javascript" src="../Script/json2.js"></script>
    <script type="text/javascript" src="JS/InfoAdd.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
</head>
<body>
    <form id="form1" style="overflow: auto;" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td id="prjState" visible="false" runat="server">
                <table align="center">
                    <tr>
                        <td rowspan="2" style="vertical-align: middle">
                            <img id="lx" src="PrjImg/lx-1.gif" runat="server" />

                        </td>
                        <td rowspan="2" style="vertical-align: middle">
                            <img id="lx1" src="PrjImg/jt-1.gif" runat="server" />

                        </td>
                        <td rowspan="2" style="vertical-align: middle">
                            <img id="qd" src="PrjImg/qd-1.gif" runat="server" />

                        </td>
                        <td rowspan="2" style="vertical-align: middle">
                            <img id="qd1" src="PrjImg/jt-1.gif" runat="server" />

                        </td>
                        <td rowspan="2" style="vertical-align: middle">
                            <img id="ys" src="PrjImg/ys-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom;">
                            <img id="ys1" src="PrjImg/jt1-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom">
                            <img id="yspass" src="PrjImg/yspass-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom;">
                            <img id="yspass1" src="PrjImg/jt2-1.gif" runat="server" />

                        </td>
                        <td rowspan="2" style="vertical-align: middle">
                            <img id="tb" src="PrjImg/tb-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom">
                            <img id="tb1" src="PrjImg/jt1-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom">
                            <img id="zb" style="margin-top: 12px; margin-bottom: -5px" src="PrjImg/zb-1.gif" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <img id="ys2" src="PrjImg/jt2-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom">
                            <img id="ysfail" style="margin-top: 12px" src="PrjImg/ysfail-1.gif" runat="server" />

                        </td>
                        <td>
                        </td>
                        <td style="vertical-align: top">
                            <img id="tb2" src="PrjImg/jt2-1.gif" runat="server" />

                        </td>
                        <td style="vertical-align: bottom">
                            <img id="lb" style="margin-top: 12px" src="PrjImg/lb-1.gif" runat="server" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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
                                            <td class="" style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 100%;">
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
                                                <asp:TextBox ID="txtCorp" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                岗位
                                            </td>
                                            <td class="txt ">
                                                <asp:TextBox ID="txtDuty" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                联系方式
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPhone" Width="100%" runat="server"></asp:TextBox>
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
                                            <td class="" style="padding-right: 3px">
                                                <span class="spanSelect" style="width: 100%;">
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
                                                <asp:TextBox ID="txtOwnerLinkMan" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                联系方式
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtOwnerLinkManPhone" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                联系地址
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtOwnerAddress" Width="100%" runat="server"></asp:TextBox>
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
                                                <input type="text" id="txtPrjCode" class="{required:true, messages:{required:'项目编号必须输入'}}" style="width: 100%;" runat="server" />

                                            </td>
                                            <td class="word">
                                                项目名称
                                            </td>
                                            <td class="txt mustInput">
                                                <asp:TextBox ID="txtPrjName" Width="100%" CssClass="{required:true, messages:{required:'项目名称必须输入'}}" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                计划开始日期
                                            </td>
                                            <td class="txt mustInput ">
                                                <asp:TextBox ID="txtStartDate" onclick="WdatePicker()" onblur="controlDate(this);" CssClass="{required:true, messages:{required:'计划开始日期必须输入'}}" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                计划结束日期
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtEndDate" onclick="WdatePicker()" onblur="controlDate(this);" runat="server"></asp:TextBox>
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
                                                <asp:TextBox ID="txtPrjPlace" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                工程工期
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtDuration" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                工程量估算
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtEngineeringEstimates" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                工程造价
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtPrjCost" class="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                预测利润率
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtForecastProfitRate" class="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr_PrjKindClass" class="prjKindClass">
                                            <td class="word">
                                                项目类型
                                            </td>
                                            <td>
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
                                                <asp:TextBox ID="txtDesigner" Width="100%" runat="server"></asp:TextBox>
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
                                            <td style="">
                                                <asp:TextBox ID="txtApprovalOf" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word" style="white-space: nowrap;">
                                                招标代理单位
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtCounsellor" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="word">
                                                监理单位
                                            </td>
                                            <td class="txt">
                                                <asp:TextBox ID="txtInspector" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目跟踪人
                                            </td>
                                            <td class="">
                                                <span class="spanSelect" style="width: 100%;">
                                                    <asp:TextBox ID="txtDutyPerson" Style="width: 80%; height: 15px; border: none;
                                                        line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img2" onclick="selectUser('hfldDutyPerson','txtDutyPerson');" runat="server" />

                                                </span>
                                                <input id="hfldDutyPerson" type="hidden" style="width: 1px" runat="server" />

                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目经理要求
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtPrjManagerRequire" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                技术负责人要求
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtTechnicalLeaderRequire" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                项目简介
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtPrjInfo" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                信息来源
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtInfoOrigin" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                其他特殊要求
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtElseRequest" TextMode="MultiLine" Height="50px" Width="100%" Rows="3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                备注
                                            </td>
                                            <td colspan="3" style="">
                                                <asp:TextBox ID="txtRemark1" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="word">
                                                相关附件
                                            </td>
                                            <td colspan="3" id="file_lx" runat="server">
                                                <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 85%; margin: auto; text-align: center">
                                    <legend>商务要求</legend>
                                    <table class="tableContent2" style="width: 95%" cellpadding="5px" cellspacing="0">
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
                                                主要负责人
                                            </td>
                                            <td class="" style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 100%">
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
                                                项目经理
                                            </td>
                                            <td class="" style="padding-right: 3px;">
                                                <span class="spanSelect" style="width: 100%;">
                                                    <asp:TextBox ID="txtPrjManager" Style="width: 89%; height: 15px; border: none;
                                                        line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser('hfldPrjManager','txtPrjManager');" />
                                                </span>
                                                <input id="hfldPrjManager" type="hidden" style="width: 1px" runat="server" />

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
                                    <table class="tableContent2" style="width: 95%" cellpadding="5px" cellspacing="0">
                                        <tr class="tr_engType" id="tr_engType">
                                            <td class="word">
                                                工程类型
                                            </td>
                                            <td colspan="3" style="white-space: nowrap;">
                                                <asp:DropDownList ID="dropBuildingType_0" Width="40%" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="dropBuildingSubType_0" Width="40%" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtDetailGrade_0" Width="10%" runat="server"></asp:TextBox>级
                                                <input type="button" id="btn_editBuildingType" value="添加" />
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
                                            <td class="">
                                                <span class="spanSelect" style="width: 99%">
                                                    <asp:TextBox ID="txtPrjReadOne" ReadOnly="true" Style="width: 80%; height: 15px;
                                                        border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                    <img alt="选择阅知人" onclick="selectUser('hfldPrjReadOne', 'txtPrjReadOne');" src="../../images/icon.bmp" style="float: right;" id="img3" runat="server" />

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
                                            <td colspan="3" style="padding-right: 1px">
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
                                            <td colspan="3" style="padding-right: 1px">
                                                <asp:TextBox ID="txtElseMargin" class="decimal_input" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr id="tr_qd" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目报名通过基本资料</legend>
                                    <div id="div3" title="项目报名通过基本资料" style="display: none" runat="server">
                                        <table id="table1" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    报名日期
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtProjApplyDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    报名通过日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtProjStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    备注
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtStartRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    相关附件
                                                </td>
                                                <td colspan="3" id="file_qd" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileStart" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_qdn" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目报名不通过基本资料</legend>
                                    <div id="div2" title="项目报名不通过基本资料" style="display: none" runat="server">
                                        <table id="table9" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    报名日期
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtProjApplyDate1" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    报名不通过日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtProjStartDate1" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    备注
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtStartRemark1" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    相关附件
                                                </td>
                                                <td colspan="3" id="file_qd1" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload2" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_ys" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目资格预审基本资料</legend>
                                    <div id="div14" title="项目资格预审基本资料" style="display: none" runat="server">
                                        <table id="table6" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    预审日期
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtApprovalDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    预审保证金(元)
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtQualificationMargin" CssClass="decimal_input" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    投标日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    登记期限(天)
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtRegistDeadline" onkeyup="this.value=this.value.replace(/[^\d]/g,''); var txtValue = this.value; if (txtValue.length > 10) {this.value = txtValue.substring(0, 10);$.messager.alert('系统提示', '输入的字数不能大于10个字');} " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    经办人
                                                </td>
                                                <td class="" style="padding-right: 3px;">
                                                    <span class="spanSelect" style="width: 99%;">
                                                        <asp:TextBox ID="txtAgent" Style="width: 80%; height: 15px; border: none;
                                                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgAgent" onclick="selectUser('hfldAgent','txtAgent');" runat="server" />

                                                    </span>
                                                    <input id="hfldAgent" type="hidden" style="width: 1px" runat="server" />

                                                </td>
                                                <td class="word">
                                                    报名日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtApplyDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    阅知人
                                                </td>
                                                <td class="">
                                                    <span class="spanSelect" style="width: 99%">
                                                        <asp:TextBox ID="txtQualificationReadOne" ReadOnly="true" Style="width: 80%; height: 15px;
                                                            border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                        <img alt="选择阅知人" onclick="selectUser('hfldQualificationReadOne', 'txtQualificationReadOne');" src="../../images/icon.bmp" style="float: right;" id="img4" runat="server" />

                                                    </span>
                                                    <asp:HiddenField ID="hfldQualificationReadOne" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    资格预审要求
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtysdetail" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    附件
                                                </td>
                                                <td colspan="3" id="file_ys" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="Fileys" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_yspass" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目预审通过基本资料</legend>
                                    <div id="div15" title="项目预审通过基本资料" style="display: none" runat="server">
                                        <table id="table7" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    通过日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtPassDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    说明
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtPassReason" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    附件
                                                </td>
                                                <td colspan="3" id="Td1" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="Fileyspass" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_ysfail" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目预审失败基本资料</legend>
                                    <div id="div16" title="项目预审失败基本资料" style="display: none" runat="server">
                                        <table id="table8" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    未通过日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtFailDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    理由
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtFailReason" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    附件
                                                </td>
                                                <td colspan="3" id="Td2" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="Fileysfail" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_tb" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目投标基本资料</legend>
                                    <div id="div4" title="项目投标基本资料" style="display: none" runat="server">
                                        <table id="table2" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    开标日期
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtTenderBeginDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    最高限价
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderCeilingPrice" class="decimal_input" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    单位
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderUnit" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    报价
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderQuote" class="decimal_input" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    评标方法
                                                </td>
                                                <td class="txt">
                                                    <asp:DropDownList ID="dropTenderAppraiseMethod" Width="100%" runat="server"></asp:DropDownList>
                                                </td>
                                                <td class="word">
                                                    参考价
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderAverage" class="decimal_input" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    投标答疑日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderAnswerDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    投标保证金
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtTenderEarnestMoney" class="decimal_input" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    缴费方式
                                                </td>
                                                <td class="txt">
                                                    <asp:DropDownList ID="dropTenderPayWay" Width="100%" runat="server"></asp:DropDownList>
                                                </td>
                                                <td class="word">
                                                    现场勘察日期
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTenderProspect" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    阅知人
                                                </td>
                                                <td class="">
                                                    <span class="spanSelect" style="width: 99%">
                                                        <asp:TextBox ID="txtTenderReadOne" ReadOnly="true" Style="width: 80%; height: 15px;
                                                            border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                                        <img alt="选择阅知人" onclick="selectUser('hfldTenderReadOne', 'txtTenderReadOne');" src="../../images/icon.bmp" style="float: right;" id="img5" runat="server" />

                                                    </span>
                                                    <asp:HiddenField ID="hfldTenderReadOne" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    现场费内容
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtTenderCostContent" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    标书内容
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtTenderContent" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    备注
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtTenderRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    相关附件
                                                </td>
                                                <td colspan="3" id="file_tb" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileTender" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_zb" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目中标基本资料</legend>
                                    <div id="div5" title="项目中标基本资料" style="display: none" runat="server">
                                        <table id="table3" class="tableContent2" style="width: 96%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    中标日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtSuccessBidDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    中标价格
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtSuccessBidPrice" class="decimal_input" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    中标备注
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtSuccessBidRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    相关附件
                                                </td>
                                                <td colspan="3" id="file_zb" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileSuccessBid" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
                        <tr id="tr_lb" style="display: none" runat="server"><td runat="server">
                                <fieldset style="width: 85%; margin: auto;">
                                    <legend>项目落标基本资料</legend>
                                    <div id="div6" title="项目落标基本资料" style="display: none" runat="server">
                                        <table id="table4" class="tableContent2" style="width: 94%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    落标日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtOutBidDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    落标保证金是否退还
                                                </td>
                                                <td class="txt">
                                                    <asp:RadioButtonList ID="RblOutBidIsReturn" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="0" Text="是" /><asp:ListItem Value="1" Text="否" /></asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    落标备注
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOutBidRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    相关附件
                                                </td>
                                                <td colspan="3" id="file_lb" style="white-space: nowrap;" runat="server">
                                                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileOutBid" Name="附件" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </td></tr>
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
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hfldstate" runat="server" />
    <asp:HiddenField ID="hfldMaxState" runat="server" />
    <asp:HiddenField ID="dropvalue" runat="server" />
    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
    <asp:HiddenField ID="divHeight" runat="server" />
    <asp:HiddenField ID="hfldCity" runat="server" />
    <!-- 存放工程类型的Json-->
    <asp:HiddenField ID="hfldEngTypeJson" runat="server" />
    <!-- 存储项目阅知人-->
    <asp:HiddenField ID="hfldPrjReadOne" runat="server" />
    <!-- 存储项目类型的Json-->
    <asp:HiddenField ID="hfldPrjKindJson" runat="server" />
    <!-- 存储资质要求的Json-->
    <asp:HiddenField ID="hfldRankJson" runat="server" />
    <!-- 存储是否允许编辑项目编码-->
    <asp:HiddenField ID="hfldIsEditProjectCode" runat="server" />
    <!-- 存储系统生成的项目编码-->
    <asp:HiddenField ID="hfldPrjCode" runat="server" />
    <asp:Button ID="btnSaveData" Text="保存" Style="display: none;" OnClick="btnSaveData_Click" runat="server" />
    </form>
</body>
</html>
