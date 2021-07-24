<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjInfoView.aspx.cs" Inherits="PrjManager_PrjInfoView" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目信息查看</title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script src="../Script/jw.js" type="text/javascript"></script>
    <script src="../Script/DecimalInput.js" type="text/javascript"></script>
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
                            立项人
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
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px;">
                    基本要求
                </div>
                <div style="width: 100%">
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
                            <td class="descTd" style="white-space: nowrap">
                                计划开始日期
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                            </td>
                            <td class="descTd" style="white-space: nowrap">
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
                            <td class="descTd">
                                工程量估算
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblEngineeringEstimates" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                项目造价
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPrjCost" CssClass="decimal_input" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                预测利润率
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblForecastProfitRate" runat="server"></asp:Label>
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
                                <asp:Label ID="lblPrjProfessionalCost" runat="server"></asp:Label>
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
                                <asp:Label ID="lblProperty" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                项目审核情况
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblApprovalOf" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                           
                            <td class="descTd">
                                监理单位
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblInspector" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                项目状态
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPrjState" runat="server"></asp:Label>
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
                                项目责任人
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblDutyPerson" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="descTd">
                                实施项目部
                            </td>
                            <td class="elemTd" colspan="3">
                                <asp:Label ID="lblXmgroup" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                项目经理要求
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblPrjManagerRequire" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                技术负责人要求
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblTechnicalLeaderRequire" runat="server"></asp:Label>
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
                </div>
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
                        <td class="elemTd" colspan="3">
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
                            项目经理
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjManager" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            业务经理
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBusinessman" runat="server"></asp:Label>
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
                        <td class="elemTd" colspan="3">
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
          <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="122" BusiClass="001" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
