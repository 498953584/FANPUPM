<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoView.aspx.cs" Inherits="TenderManage_InfoView" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; border-collapse: collapse;">
        <tr>
            <td class="divHeader">
                项目信息查看
                
                <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px;">
                    立项人基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            立项申请人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            部门/单位
                        </td>
                        <td class="elemTd
                        ">
                            <asp:Label ID="lblCorp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            岗位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDuty" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            联系方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    建设单位基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            建设单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOwner" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            联系人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOwnerLinkMan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            联系方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOwnerLinkManPhone" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            联系地址
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOwnerAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    基本内容
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td class="elemTd">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            计划开始日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            计划结束日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            所属区域
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblArea" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目地点
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjPlace" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            工程工期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDuration" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            工程量估算
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEngineeringEstimates" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            工程造价
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjCost" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            预测利润率
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblForecastProfitRate" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目类型
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjKindClass" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            专业造价
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjProfessionalCost" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            质量等级
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQualityClass" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设计单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDesigner" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目属性
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjState" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            项目审核情况
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblApprovalOf" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            招标代理单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCounsellor" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            监理单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInspector" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            项目跟踪人
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblDutyPerson" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            项目经理要求
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblPrjManagerRequire" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            技术负责人要求
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblTechnicalLeaderRequire" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目简介
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblPrjInfo" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            信息来源
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblInfoOrigin" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            其他特殊要求
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblElseRequest" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblRemark1" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="upload" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    商务要求
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            资质要求
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblRank" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            承包方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractWay" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            付款条件
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPayCondition" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            招标方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderWay" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            结算方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPayWay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预算方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudgetWay" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            重要程度
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblKeyPart" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            主要负责人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblWorkUnit" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            主要负责人联系电话
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTelphone" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目经理
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblPrjManager" runat="server"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    补充内容
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            工程类型
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblBuildingType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            规模
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTotalHouseNum" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            等级
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblgrade" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            资金来源
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjFundInfo" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            资金落实情况
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblFundWorkable" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            建筑面积
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBuildingArea" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            占地面积
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblUsegrounArea" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            地下面积
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblUndergroundArea" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            阅知人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjReadOne" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            绿化面积
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAfforestArea" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            园林面积
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblParkArea" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            其他说明
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblOtherStatement" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    管理参数
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            管理保证金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblManagementMargin" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            民工工资保证金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMigrantQualityMarginRate" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预扣税率
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblWithholdingTaxRate" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            履约保证金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPerformanceBond" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            其他（保证金）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblElseMargin" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                        </td>
                        <td class="elemTd">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr_qd" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目报名通过基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            报名日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProjApplyDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                           报名通过日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProjStartDate" runat="server"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblStartRemark" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            相关附件
                        </td>
                        <td colspan="3" id="file_qd" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_bt" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目报名不通过基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            报名通过日期
                        </td>
                        <td class="descTd">
                            <asp:Label ID="lblProjApplyDate1" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                           报名不通过日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProjStartDate1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblStartRemark1" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            相关附件
                        </td>
                        <td colspan="3" id="file_qd1" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_ys" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目资格预审基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            预审日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblApprovalDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预审保证金(元)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQualificationMargin" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            投标日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            登记期限(天)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRegistDeadline" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            经办人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAgent" runat="server"></asp:Label>
                        </td>
                        
                        <td class="descTd">
                            报名日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblApplyDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            资格预审要求
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblPrequalificationRequire" Text="" runat="server"></asp:Label>
                            </div>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_ys" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_pass" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目预审通过基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            通过日期
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblPassDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            说明
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblPassReason" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_pass" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_fail" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目预审失败基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            未通过日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblFailDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            理由
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblFailReason" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_fail" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_tb" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目投标基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            开标日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderBeginDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            最高价限
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderCeilingPrice" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderUnit" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            报价
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderQuote" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            评标方法
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderAppraiseMethod" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            参考价
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderAverage" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            投标答疑日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderAnswerDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            投标保证金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderEarnestMoney" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            保证金缴纳方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderPayWay" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            现场勘察日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderProspect" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            阅知人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTenderReadOne" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                        </td>
                        <td class="elemTd">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            现场费内容
                        </td>
                        <td colspan="3">
                            <textarea id="Textarea6" disabled="true" cols="20" rows="2" visible="false" runat="server"></textarea>
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblTenderCostContent" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            标书内容
                        </td>
                        <td colspan="3">
                            <textarea id="Textarea7" disabled="true" cols="20" rows="2" visible="false" runat="server"></textarea>
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblTenderContent" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <textarea id="Textarea10" disabled="true" cols="20" rows="2" visible="false" runat="server"></textarea>
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblTenderRemark" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_tb" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_zb" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目中标基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            中标日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSuccessBidDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            中标价格
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSuccessBidPrice" CssClass="decimal_input" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            中标备注
                        </td>
                        <td colspan="3">
                            <textarea id="Textarea8" disabled="true" cols="20" rows="2" visible="false" runat="server"></textarea>
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblSuccessBidRemark" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_zb" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_lb" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目落标基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            落标日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOutBidDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            落标保证金是否退还
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOutBidIsReturn" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            落标备注
                        </td>
                        <td colspan="3">
                            <textarea id="Textarea11" disabled="true" cols="20" rows="2" visible="false" runat="server"></textarea>
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblOutBidRemark" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_lb" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="tr_fq" visible="false" runat="server"><td style="vertical-align: top" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目放弃基本资料
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            放弃日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lbGiveUpTime" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            操作人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lbOperator" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            放弃原因
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lbGiveUpReason" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lbGiveUpNote" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" id="file_GiveUp" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="trAudit" style="vertical-align: top;" runat="server"><td runat="server">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="089" BusiClass="001" runat="server" />
            </td></tr>
        <tr id="trAudit_initate" style="vertical-align: top;" visible="false" runat="server"><td runat="server">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit2" BusiCode="135" BusiClass="001" runat="server" />
            </td></tr>
        <tr id="trAudit_pft" style="vertical-align: top;" visible="false" runat="server"><td runat="server">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit3" BusiCode="136" BusiClass="001" runat="server" />
            </td></tr>
        <tr id="trAudit_bit" style="vertical-align: top;" visible="false" runat="server"><td runat="server">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit4" BusiCode="137" BusiClass="001" runat="server" />
            </td></tr>
        <tr id="trAudit_giveUp" style="vertical-align: top;" visible="false" runat="server"><td runat="server">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit5" BusiCode="138" BusiClass="001" runat="server" />
            </td></tr>
    </table>
    </form>
</body>
</html>
