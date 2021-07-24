<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoView.aspx.cs" Inherits="InfoView" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>无标题页</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
</head>
<body scroll="yes" class="body_popup">
    <form id="Form1" method="post" target="win" runat="server">
        <table id="Table1" width="100%" border="1" class="table-form" style="border-collapse: collapse">
            <tr >
                <td width="9%" colspan="6" align=center  class="td-title">
                    --基本内容--</td>
            </tr>
            <tr style="display:none ">
                <td class="td-label" style="width: 119px" >
                    编码:</td>
                <td style="width: 207px">
                    <asp:Label ID="labTypeCode" Text="Label" Width="95px" runat="server"></asp:Label></td>
                <td colspan="4">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    项目类型:</td>
                <td style="width: 207px">
                    <asp:Label ID="labPrjkind" Width="149px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px" width="70">
                    项目编号:</td>
                <td width="22%">
                    <label>
                        <asp:Label ID="labPrjcode" Width="158px" runat="server"></asp:Label></label></td>
                <td class="td-label" style="width: 84px">
                    项目名称:</td>
                <td style="width: 229px">
                    <asp:Label ID="labPrjname" Width="190px" ForeColor="Red" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    开始时间:</td>
                <td style="width: 207px">
                    <asp:Label ID="labstarttime" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px">
                    结束时间:</td>
                <td>
                    <asp:Label ID="labendtime" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px">
                    工程工期:</td>
                <td style="width: 229px">
                    <asp:Label ID="labgq" Width="103px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    质量等级:</td>
                <td >
                    <asp:Label ID="labquclass" Width="100px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px">
                    项目造价:</td>
                <td>
                    <asp:Label ID="labPrjCost" Width="87px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px">
                    <span>甲方名称:</span></td>
                <td>
                    <asp:Label ID="labOwner" Width="94px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    项目地区:</td>
                <td style="width: 207px">
                    <asp:Label ID="labarea" Width="100px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px">
                    项目状态:</td>
                <td>
                    <asp:Label ID="labstata" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px">
                    项目地点:</td>
                <td style="width: 229px">
                    <asp:Label ID="LabPrjPlace" Width="94px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    设计公司:</td>
                <td style="width: 207px">
                    <asp:Label ID="LabDesigner" Width="100px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px">
                    监理公司:</td>
                <td>
                    <asp:Label ID="LabInspector" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px">
                    招标代理:</td>
                <td style="width: 229px">
                    <asp:Label ID="LabCounsellor" Width="94px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    项目简介:</td>
                <td colspan="5">
                    <asp:Label ID="Labprjinfo" Width="100px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    备注:</td>
                <td colspan="5">
                    <font face="宋体">
                        <asp:Label ID="LabRemark" Width="106px" runat="server"></asp:Label></font></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    相关附件</td>
                <td colspan="5">
                <asp:Literal ID="lbllink" runat="server"></asp:Literal>
                    
                </td>
            </tr>
            <tr class="td-title">
                <td width="9%" colspan="6" align=center >
                    --商务要求--</td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    资质等级:</td>
                <td style="width: 207px">
                    <font face="宋体">
                        <asp:Label ID="Labrank" Width="100px" runat="server"></asp:Label></font></td>
                <td class="td-label" style="width: 70px" width="70">
                    预算方式:</td>
                <td width="22%">
                    <font face="宋体">
                        <asp:Label ID="LabBudgetWay" Width="100px" runat="server"></asp:Label></font></td>
                <td class="td-label" style="width: 84px">
                    承包方式:</td>
                <td style="width: 229px">
                    <font face="宋体">
                        <asp:Label ID="LabContractWay" Width="100px" runat="server"></asp:Label></font></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    付款条件:</td>
                <td style="width: 207px">
                    <asp:Label ID="LabPayCondition" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px" width="70">
                    招标形式:</td>
                <td width="22%">
                    <asp:Label ID="LabTenderWay" Width="100px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px">
                    结算方式:</td>
                <td style="width: 229px">
                    <asp:Label ID="LabPayWay" Width="94px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    重要程序评价:</td>
                <td colspan="5">
                    <asp:Label ID="LabKeyPart" Width="94px" runat="server"></asp:Label></td>
                
            </tr>
            
            
            <tr>
                <td class="td-label" style="width: 119px; height: 21px;">
                    实施单位:</td>
                <td style="height: 21px; width: 207px;">
                    <asp:Label ID="LabWorkUnit" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px; height: 21px;" width="70">
                    实施人:</td>
                <td width="22%" style="height: 21px">
                    <asp:Label ID="Lablinkman" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px; height: 21px;">
                    项目经理:</td>
                <td style="width: 229px; height: 21px;">
                    <asp:Label ID="Labmanager" Width="94px" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px; height: 21px;">
                    联系电话:</td>
                <td style="height: 21px; width: 207px;">
                    <asp:Label ID="Label1" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 70px; height: 21px;" width="70">
                    业务经理:</td>
                <td width="22%" style="height: 21px">
                    <asp:Label ID="Label3" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px; height: 21px;">
                    &nbsp;</td>
                <td style="width: 229px; height: 21px;">
                    &nbsp;</td>
            </tr>
            <tr class="td-title">
                <td width="9%" colspan="6" align=center>
                    --补充内容--</td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    建筑类别:</td>
                <td style="width: 207px">
                    <font face="宋体">
                        <asp:Label ID="LabBuildingType" Width="100px" runat="server"></asp:Label></font></td>
                <td class="td-label" style="width: 70px" width="70">
                    楼 栋 数:</td>
                <td width="22%">
                    <asp:Label ID="LabTotalHouseNum" Width="94px" runat="server"></asp:Label></td>
                <td class="td-label" style="width: 84px">
                等级:
                </td>
                <td style="width: 229px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    建筑面积:</td>
                <td style="width: 207px">
                    <font face="宋体">
                        <asp:Label ID="LabBuildingArea" Width="100px" runat="server"></asp:Label></font></td>
                <td class="td-label" style="width: 70px" width="70">
                    占地面积:</td>
                <td width="22%">
                    <font face="宋体">
                        <asp:Label ID="LabUsegrounArea" Width="100px" runat="server"></asp:Label></font></td>
                <td class="td-label" style="width: 84px">
                    地下面积:</td>
                <td style="width: 229px">
                    <font face="宋体">
                        <asp:Label ID="LabUndergroundArea" Width="100px" runat="server"></asp:Label></font></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    资金情况:</td>
                <td colspan="5">
                    <font face="宋体">
                        <asp:Label ID="LabPrjFundInfo" Width="100px" runat="server"></asp:Label></font></td>
            </tr>
            <tr>
                <td class="td-label" style="width: 119px">
                    其它说明:</td>
                <td colspan="5">
                    <font face="宋体">
                        <asp:Label ID="LabOtherStatement" Width="100px" runat="server"></asp:Label></font></td>
            </tr>
            <tr>
                <td colspan="6" class="td-submit" style="height: 14px" align=center>
                    <input type="button" value="关  闭" onclick="window.close();">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
