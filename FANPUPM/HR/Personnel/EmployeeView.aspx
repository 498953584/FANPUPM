<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeView.aspx.cs" Inherits="HR_Personnel_EmployeeView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>员工信息登记</title>
    <base target="_self"></base>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" language="javascript">
        addEvent(window, 'load', function () {
            if (document.getElementById("gvExperience") == null) {
                document.getElementById("trWorkTitle").style.display = "none";
                document.getElementById("trWorkContent").style.display = "none";
            }
            if (document.getElementById("gvRewardsAndPunishment") == null) {
                document.getElementById("trRewardsAndPunishmentTitle").style.display = "none";
                document.getElementById("trRewardsAndPunishmentContent").style.display = "none";
            }
            if (document.getElementById("gvEducation") == null) {
                document.getElementById("trEducationTitle").style.display = "none";
                document.getElementById("trEducationContent").style.display = "none";
            }
            if (document.getElementById("gvTrain") == null) {
                document.getElementById("trTrainTitle").style.display = "none";
                document.getElementById("trTrainContent").style.display = "none";
            }
            if (document.getElementById("gvFamilyMembers") == null) {
                document.getElementById("trFamilyTitle").style.display = "none";
                document.getElementById("trFamilyContent").style.display = "none";
            }
            if (document.getElementById("GVDuty") == null) {
                document.getElementById("trDutyTitle").style.display = "none";
                document.getElementById("trDutyContent").style.display = "none";
            }
        });
        function printPage() {
            var hkey_root, hkey_path, hkey_key;
            hkey_root = "HKEY_CURRENT_USER";
            hkey_path = "SoftwareMicrosoftInternet ExplorerPageSetup";

            try {
                var RegWsh = new ActiveXObject("WScript.Shell");
                hkey_key1 = "header";
                hkey_key2 = "footer";
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key1, "");
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key2, "");
            } catch (e) { }
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            window.document.body.innerHTML = bdhtml;
            window.location.reload();
            return false;

        }
    </script>
</head>

<body class="body_frame">
    <form id="form1" runat="server">
    <div>
        <input id="btnprintList" type="button" value="打印" onclick="printPage()" style="width: 80px;" runat="server" />
</div>
    <!--startprint-->
    <table class="table-normal" id="Table1" cellspacing="0" cellpadding="0" width="100%"
        border="1">
        <tr>
            <td class="td-head" colspan="8" height="20" align="center">
                员工详细信息
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                员工编号
            </td>
            <td style="height: 26px; width: 15%;">
                <asp:Label ID="labUserCode" Text="" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                入司日期
            </td>
            <td>
                <asp:Label ID="LabEnterCorpDate" runat="server"></asp:Label>
            </td>
            
            <td class="td-label" width="10%">
                所属部门
            </td>
            <td>
                <asp:Label ID="Labbm" runat="server"></asp:Label>
            </td>
            <td colspan="2" rowspan="8" align="center" valign="middle" height="120" width="90">
                &nbsp;<asp:Panel ID="Panel1" Height="50px" Width="125px" runat="server">
                    <asp:Image ID="Image1" Width="90px" Height="120px" ImageAlign="Top" GenerateEmptyAlternateText="true" AlternateText="照片" ImageUrl="~/images/PhotoDefault.gif" runat="server" /></asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                姓&nbsp;&nbsp;&nbsp;&nbsp;名
            </td>
            <td>
                <asp:Label ID="labname" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                年&nbsp;&nbsp;&nbsp;&nbsp;龄
            </td>
            <td style="width: 128px">
                <asp:Label ID="labAge" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                性&nbsp;&nbsp;&nbsp;&nbsp;别
            </td>
            <td width="15%">
                <asp:Label ID="labSex" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%" style="height: 26px">
                民&nbsp;&nbsp;&nbsp;&nbsp;族
            </td>
            <td style="height: 26px">
                <asp:Label ID="LabNation" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%" style="height: 26px">
                学&nbsp;&nbsp;&nbsp;&nbsp;历
            </td>
            <td style="height: 26px; width: 128px;">
                <asp:Label ID="LabEducational" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%" style="height: 26px">
                身&nbsp;&nbsp;&nbsp;&nbsp;高
            </td>
            <td style="height: 26px">
                <asp:Label ID="Labhigt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                岗&nbsp;&nbsp;&nbsp;&nbsp;位
            </td>
            <td>
                <asp:Label ID="LabDuty" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                职&nbsp;&nbsp;&nbsp;&nbsp;级
            </td>
            <td style="width: 128px">
                <asp:Label ID="LabPositionLevel" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                职&nbsp;&nbsp;&nbsp;&nbsp;称
            </td>
            <td>
                <asp:Label ID="LabPostAndRank" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                现居住地
            </td>
            <td colspan="2">
                <asp:Label ID="LabAddress" runat="server"></asp:Label>
            </td>
            <td class="td-label" style="width: 128px">
                户口所在地
            </td>
            <td colspan="2">
                &nbsp;<asp:Label ID="LabRegisteredPlace" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                人员类型
            </td>
            <td style="height: 26px">
                <asp:Label ID="LabClassID" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                电脑水平
            </td>
            <td style="height: 26px; width: 128px;">
                <asp:Label ID="LabComputeLevel" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                外语水平
            </td>
            <td style="height: 26px">
                <asp:Label ID="LabEnglishLevel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right;" class="td-label">
                户籍性质
            </td>
            <td style="width: 15%;">
                <asp:Label ID="labrdeNature" Text="" runat="server"></asp:Label>
            </td>
            <td style="width: 10%; text-align: right;" class="td-label">
                转正日期
            </td>
            <td style="width: 15%;">
                <asp:Label ID="labFormalData" Text="" runat="server"></asp:Label>
            </td>
            <td style="width: 10%; text-align: right;" class="td-label">
                最新合同终止日期
            </td>
            <td style="width: 15%;">
                <asp:Label ID="labconEndDate" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right;" class="td-label">
                紧急联系人
            </td>
            <td style="width: 15%;">
                <asp:Label ID="laburgentCellMan" Text="" runat="server"></asp:Label>
            </td>
            <td style="width: 10%; text-align: right;" class="td-label">
                与本人关系
            </td>
            <td style="width: 15%;">
                <asp:Label ID="labucmConcern" Text="" runat="server"></asp:Label>
            </td>
            <td style="width: 10%; text-align: right;" class="td-label">
                联系人电话
            </td>
            <td style="width: 15%;">
                <asp:Label ID="labucmTel" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                婚姻状况
            </td>
            <td>
                <asp:Label ID="LabMarriage" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                员工状态
            </td>
            <td colspan="5">
                <div id="tr_add" runat="server">
                    <asp:Label ID="Labnowstata" Width="134px" runat="server"></asp:Label>&nbsp;</div>
                <div id="tr_edit" style="display: none" runat="server">
                    <a id="annexName" href="#" onclick="annexEdit();" style="cursor: hand" runat="server" />

                    &nbsp;&nbsp;
                </div>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                驾驶水平
            </td>
            <td width="10%">
                <asp:Label ID="LabDriveLevel" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                联系电话
            </td>
            <td style="width: 128px">
                <asp:Label ID="LabTel" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                期望薪金
            </td>
            <td>
                <asp:Label ID="LabtExpectationSalary" runat="server"></asp:Label>&nbsp;
            </td>
            <td class="td-label" style="width: 10%">
                邮&nbsp;&nbsp;&nbsp;&nbsp;编
            </td>
            <td width="10%">
                <asp:Label ID="LabPostcode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                手机号
            </td>
            <td width="10%">
                <asp:Label ID="LabMobilePhoneCode" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                出生日期
            </td>
            <td style="width: 128px">
                <asp:Label ID="LabBirthday" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                身份证号
            </td>
            <td>
                <asp:Label ID="LabIDCard" runat="server"></asp:Label>
            </td>
            <td class="td-label" style="width: 10%">
                部门负责人
            </td>
            <td width="10%">
                <asp:Label ID="labChargeMan" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                政治面貌
            </td>
            <td width="10%">
                <asp:Label ID="LabPoliticsFace" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                入党时间
            </td>
            <td style="width: 128px">
                <asp:Label ID="LabJoinPartyDate" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                入司渠道
            </td>
            <td>
                <asp:Label ID="LabJoinCorpMode" runat="server"></asp:Label>
            </td>
            <td class="td-label" style="width: 10%">
                介绍人
            </td>
            <td width="10%">
                <asp:Label ID="LabIntroducer" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                毕业院校
            </td>
            <td width="10%">
                <asp:Label ID="LabGraduateSchool" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                专业
            </td>
            <td style="width: 128px">
                <asp:Label ID="LabSpecialty" runat="server"></asp:Label>
            </td>
            <td class="td-label" width="10%">
                学历层级
            </td>
            <td>
                <asp:Label ID="LabLevel" runat="server"></asp:Label>
            </td>
            <td class="td-label" style="width: 10%">
                参加工作时间
            </td>
            <td width="10%">
                <asp:Label ID="LabBeginWorkDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                职称资格
            </td>
            <td width="10%">
                <asp:Label ID="LabtPostAndCompetency" runat="server"></asp:Label>
            </td>
            <td colspan="6">
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
            </td>
            <td colspan="6" colspan="6">
                <asp:CheckBoxList ID="cblInsurance" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Text="养老保险" /><asp:ListItem Value="2" Text="医疗保险" /><asp:ListItem Value="3" Text="失业保险" /><asp:ListItem Value="4" Text="工伤保险" /><asp:ListItem Value="5" Text="住房公积金" /><asp:ListItem Value="6" Text="人身意外险" /></asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                通讯地址
            </td>
            <td style="height: 26px" colspan="7">
                <asp:Label ID="LabCommunicateAddress" runat="server"></asp:Label>
            </td>
        </tr>
        
        <tr id="trWorkTitle">
            <td class="td-head" colspan="8" height="20" align="center">
                工作经历
            </td>
        </tr>
        <tr id="trWorkContent">
            <td colspan="8">
                <asp:GridView ID="gvExperience" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlExperience" Width="100%" DataKeyNames="RecordID" runat="server"><HeaderStyle BackColor="#EFEBDE" Font-Size="12px"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                
                            </ItemTemplate>

<FooterTemplate>
                            </FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="起始日期" SortExpression="StartDate"><ItemTemplate>
                                <asp:Label ID="lbStartDate" Text='<%# System.Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate>
<FooterTemplate>
                                <JWControl:DateBox ID="dbStartDate" Width="100%" runat="server"></JWControl:DateBox>
                            </FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="截止日期" SortExpression="EndDate"><ItemTemplate>
                                <asp:Label ID="lbEndDate" Text='<%# System.Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="公司名称" SortExpression="EnterpriseName"><ItemTemplate>
                                <asp:Label ID="lbEnterpriseName" Text='<%# System.Convert.ToString(Eval("EnterpriseName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="职位" SortExpression="Post"><ItemTemplate>
                                <asp:Label ID="lbPost" Text='<%# System.Convert.ToString(Eval("Post"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlExperience" SelectCommand="SELECT [RecordID], [UserCode], [StartDate], [EndDate], [EnterpriseName], [Post] FROM [HR_Personnel_Experience] WHERE ([UserCode] = @UserCode)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [HR_Personnel_Experience] SET [UserCode] = @UserCode, [StartDate] = @StartDate, [EndDate] = @EndDate, [EnterpriseName] = @EnterpriseName, [Post] = @Post WHERE [RecordID] = @original_RecordID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr id="trRewardsAndPunishmentTitle">
            <td class="td-head" colspan="8" height="20" align="center">
                奖罚情况
            </td>
        </tr>
        <tr id="trRewardsAndPunishmentContent">
            <td colspan="8">
                <asp:GridView ID="gvRewardsAndPunishment" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlRewardsAndPunishment" Width="100%" DataKeyNames="RecordID" runat="server"><HeaderStyle BackColor="#EFEBDE" Font-Size="12px"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="荣誉称号" SortExpression="Credit"><ItemTemplate>
                                <asp:Label ID="lbCredit" Text='<%# System.Convert.ToString(Eval("Credit"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="奖惩原因" SortExpression="Reason"><ItemTemplate>
                                <asp:Label ID="lbReason" Text='<%# System.Convert.ToString(Eval("Reason"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="授予日期" SortExpression="AwardDate"><ItemTemplate>
                                <asp:Label ID="lbAwardDate" Text='<%# System.Convert.ToString(Eval("AwardDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="授予单位" SortExpression="AwardOrg"><ItemTemplate>
                                <asp:Label ID="lbAwardOrg" Text='<%# System.Convert.ToString(Eval("AwardOrg"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlRewardsAndPunishment" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [Credit], [Reason], [AwardDate], [AwardOrg] FROM [HR_Personnel_RewardsAndPunishment] WHERE ([UserCode] = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr id="trEducationTitle">
            <td class="td-head" colspan="8" height="20" align="center">
                教育经历
            </td>
        </tr>
        <tr id="trEducationContent">
            <td colspan="8">
                <asp:GridView ID="gvEducation" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlEducation" Width="100%" DataKeyNames="RecordID" runat="server"><HeaderStyle BackColor="#EFEBDE" Font-Size="12px"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="起始日期" SortExpression="StartDate"><ItemTemplate>
                                <asp:Label ID="lbStartDate" Text='<%# System.Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="截止日期" SortExpression="EndDate"><ItemTemplate>
                                <asp:Label ID="lbEndDate" Text='<%# System.Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="学校名称" SortExpression="SchoolName"><ItemTemplate>
                                <asp:Label ID="lbSchoolName" Text='<%# System.Convert.ToString(Eval("SchoolName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="专业" SortExpression="Specialty"><ItemTemplate>
                                <asp:Label ID="lbSpecialty" Text='<%# System.Convert.ToString(Eval("Specialty"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlEducation" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [StartDate], [EndDate], [SchoolName], [Specialty] FROM [HR_Personnel_Education] WHERE ([UserCode] = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr id="trTrainTitle">
            <td class="td-head" colspan="8" height="20" align="center">
                培训记录
            </td>
        </tr>
        <tr id="trTrainContent">
            <td colspan="8">
                <asp:GridView ID="gvTrain" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlTrain" Width="100%" DataKeyNames="RecordID" runat="server"><HeaderStyle BackColor="#EFEBDE" Font-Size="12px"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="课程名称" SortExpression="Courses"><ItemTemplate>
                                <asp:Label ID="lbCourses" Text='<%# System.Convert.ToString(Eval("Courses"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="课时" SortExpression="Hour"><ItemTemplate>
                                <asp:Label ID="lbHour" Text='<%# System.Convert.ToString(Eval("Hour"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="起始日期" SortExpression="StartDate"><ItemTemplate>
                                <asp:Label ID="lbStartDate" Text='<%# System.Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="截止日期" SortExpression="EndDate"><ItemTemplate>
                                <asp:Label ID="lbEndDate" Text='<%# System.Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="培训机构" SortExpression="TrainOrgan"><ItemTemplate>
                                <asp:Label ID="lbTrainOrgan" Text='<%# System.Convert.ToString(Eval("TrainOrgan"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" SortExpression="Remark"><ItemTemplate>
                                <asp:Label ID="lbRemark" Text='<%# System.Convert.ToString(Eval("Remark"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlTrain" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [Courses], [Hour], [StartDate], [EndDate], [TrainOrgan], [Remark] FROM [HR_Personnel_Train] WHERE ([UserCode] = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr id="trFamilyTitle">
            <td class="td-head" colspan="8" height="20" align="center">
                家庭关系
            </td>
        </tr>
        <tr id="trFamilyContent">
            <td colspan="8">
                <asp:GridView ID="gvFamilyMembers" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlFamilyMembers" Width="100%" DataKeyNames="RecordID" runat="server"><HeaderStyle BackColor="#EFEBDE" Font-Size="12px"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="姓名" SortExpression="Name"><ItemTemplate>
                                <asp:Label ID="lbName" Text='<%# System.Convert.ToString(Eval("Name"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="与本人关系" SortExpression="Relation"><ItemTemplate>
                                <asp:Label ID="lbRelation" Text='<%# System.Convert.ToString(Eval("Relation"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="联系电话" SortExpression="Tel"><ItemTemplate>
                                <asp:Label ID="lbTel" Text='<%# System.Convert.ToString(Eval("Tel"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工作单位" SortExpression="WorkUnit"><ItemTemplate>
                                <asp:Label ID="lbWorkUnit" Text='<%# System.Convert.ToString(Eval("WorkUnit"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                <asp:SqlDataSource ID="SqlFamilyMembers" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [Name], [Relation], [Tel], [WorkUnit] FROM [HR_Personnel_FamilyMembers] WHERE ([UserCode] = @UserCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr id="trDutyTitle">
            <td class="td-head" colspan="8" height="20" align="center">
                兼职岗位
            </td>
        </tr>
        <tr id="trDutyContent">
            <td colspan="8">
                <asp:GridView CssClass="grid" ID="GVDuty" AutoGenerateColumns="false" Width="100%" CellPadding="0" runat="server"><HeaderStyle HorizontalAlign="Center" BackColor="#EFEBDE" Font-Size="12px"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="V_BMMC" HeaderText="部门" HtmlEncode="false" /><asp:BoundField DataField="RoleTypeName" HeaderText="职务" HtmlEncode="false" /></Columns></asp:GridView>
            </td>
        </tr>
    </table>
    <!--endprint-->
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
