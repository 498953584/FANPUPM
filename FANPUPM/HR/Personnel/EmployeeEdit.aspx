<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeEdit.aspx.cs" Inherits="EmployeeEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Microsoft.Web.UI.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>员工信息登记</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
	        // 添加验证
	        $('#BtnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
	    });
	    //        // 选择上级岗位
	    //        function pickDuty(deptID) {
	    //            //            var deptId = getRequestParam('cc');
	    //            //            var url = '/HR/Personnel/PickSinglePerson.aspx?cc=' + deptId;

	    //            //            return false;

	    //            var p = new Array();
	    //            p[0] = "";
	    //            p[1] = "";
	    //            var url = "";
	    //            url = "PickSinglePerson.aspx?cc=" + deptID;
	    //            window.showModalDialog(url, p, "dialogHeight:500px;dialogWidth:550px;center:1;help:0;status:0;");
	    //            //window.open(url,"wi");
	    //            if (p[0] != "") {
	    //                document.getElementById("hdnSuperordinateDuty").value = p[1];
	    //            }
	    //        }
	    function annexEdit() {
	    }
	    function ClickRow(op, recordId) {
	        document.getElementById('hdnRecordID' + op).value = recordId;
	    }

	    // 选择部门
	    function selDept() {
	        jw.selectOneDep({ idinput: 'hdnDept', nameinput: 'txtDeptName' });
	    }

	    function DutyClickRow(DUTYID, DeptID) {
	        document.getElementById('btnDelDuty').disabled = false;
	        document.getElementById('HdnDUTYID').value = DUTYID;
	        document.getElementById('HdnDeptID').value = DeptID;
	    }
	    function OpenDutyWin(UserCode) {
	        var url = "AddDuty.aspx?ec=" + UserCode;
	        var ref = window.showModalDialog(url, window, "dialogHeight:160px;dialogWidth:300px;center:1;help:0;status:0;");
	        if (ref) {
	            return true;
	        }
	        return false;
	    }

	    function successed(action) {
	        top.ui.show(action + '成功！');
	        var depCode = jw.getRequestParam('cc');
	        var pUrl = top.ui._Employee.location.href;
	        pUrl = jw.modifyParam({ url: pUrl, name: 'depCode', value: depCode })
	        top.ui._Employee.location.href = pUrl;
	        top.ui.closeTab();
	    }
	</script>
</head>
<body class="body_frame">
	<form id="form1" runat="server">
	<table id="table2" border="1" cellpadding="0" cellspacing="0" class="table-form"
		width="100%">
		<tr>
			<td style="height: 41px">
				<iewc:TabStrip ID="TabStrip1" TargetID="MultiPage1" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabIndex="-1" runat="server"><Items><iewc:TabSeparator ID="TabSeparator1" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab1" Text="基本信息" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator2" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab2" Text="工作经历" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator3" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab3" Text="奖惩情况" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator4" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab4" Text="教育经历" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator5" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab5" Text="培训记录" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator6" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab6" Text="家庭关系" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator7" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab7" Text="兼职岗位" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator8" DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<iewc:MultiPage ID="MultiPage1" Height="100%" runat="server"><iewc:PageView ID="PageView1" runat="server">
						<table class="table-form" id="Table1" cellspacing="0" cellpadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-head" colspan="8" height="20">
									员工信息登记
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									员工编号
								</td>
								<td style="height: 26px; width: 15%;">
									<asp:TextBox ID="txtUserCode" CssClass="text-input" TabIndex="23" Width="90%" MaxLength="100" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" style="width: 10%">
									入司日期
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="dbEnterCorpDate" CssClass="Wdate" onfocus="WdatePicker({el:'dbEnterCorpDate',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
								</td>
								
								<td class="td-label" width="10%">
									所属部门
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtDeptName" CssClass="text-input" Enabled="false" TabIndex="2" Width="75%" runat="server"></asp:TextBox>
									<img alt="选择部门" onclick="selDept();" src="/images/contact.gif" style="border-style: none;
										vertical-align: bottom;" />
									<input id="hdnDept" style="width: 10px" type="hidden" name="hdnDept" runat="server" />

								</td>
								<td colspan="2" rowspan="8" align="center" valign="middle" style="width: 25%;">
									<asp:Image ID="Image1" Width="112px" Height="140px" ImageAlign="Top" GenerateEmptyAlternateText="true" AlternateText="照片" ImageUrl="~/images/PhotoDefault.gif" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									<span style="color: Red;">* </span>姓&nbsp;&nbsp;&nbsp;&nbsp;名
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtName" CssClass="text-input easyui-validatebox " TabIndex="3" Width="90%" data-options="required:true, validType:'validChars[20]'" MaxLength="25" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									年&nbsp;&nbsp;&nbsp;&nbsp;龄
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtAge" CssClass="text-input" TabIndex="4" Width="50%" MaxLength="4" runat="server"></asp:TextBox>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^(0|[1-9][0-9]*)$" ControlToValidate="txtAge" ErrorMessage="输入错误" runat="server"></asp:RegularExpressionValidator>
								</td>
								<td class="td-label" width="10%">
									性&nbsp;&nbsp;&nbsp;&nbsp;别
								</td>
								<td width="15%">
									<asp:DropDownList ID="ddltSex" TabIndex="5" Width="90%" runat="server"><asp:ListItem Value="1" Text="男" /><asp:ListItem Value="2" Text="女" /></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%" style="height: 26px">
									民&nbsp;&nbsp;&nbsp;&nbsp;族
								</td>
								<td style="height: 26px; width: 15%;">
									<JWControl:Combox ID="CombNation" onfocus="this.select()" TabIndex="6" MaxLength="25" runat="server"></JWControl:Combox>
								</td>
								<td class="td-label" width="10%" style="height: 26px">
									学&nbsp;&nbsp;&nbsp;&nbsp;历
								</td>
								<td style="height: 26px; width: 15%;">
									<JWControl:Combox ID="CombEducationalBackground" onfocus="this.select()" TabIndex="7" MaxLength="25" runat="server"></JWControl:Combox>
								</td>
								<td class="td-label" width="10%" style="height: 26px">
									身&nbsp;&nbsp;&nbsp;&nbsp;高
								</td>
								<td style="height: 26px; width: 15%;">
									<asp:TextBox ID="txtStature" CssClass="text-input" TabIndex="8" Width="80%" MaxLength="25" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									岗&nbsp;&nbsp;&nbsp;&nbsp;位
								</td>
								<td style="width: 15%;">
									<asp:DropDownList ID="ddltDuty" DataSourceID="SqlDuty" DataTextField="DutyName" DataValueField="i_dutyid" Width="90%" runat="server"></asp:DropDownList>
									<input id="hdnDuty" style="width: 10px" type="hidden" name="hdnDuty" runat="server" />

									<asp:SqlDataSource ID="SqlDuty" SelectCommand="select distinct i_dutyid, DutyName, i_bmdm from pt_duty b where i_bmdm = @DeptID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter Name="DeptID" ControlID="hdnDept" PropertyName="Value" runat="server" /></SelectParameters></asp:SqlDataSource>
									&nbsp;
								</td>
								<td class="td-label" width="10%">
									职&nbsp;&nbsp;&nbsp;&nbsp;级
								</td>
								<td style="width: 15%;">
									<asp:DropDownList ID="ddltPositionLevel" TabIndex="10" AutoPostBack="true" Width="90%" OnSelectedIndexChanged="ddltPositionLevel_SelectedIndexChanged" runat="server"></asp:DropDownList>
								</td>
								<td class="td-label" width="10%">
									职&nbsp;&nbsp;&nbsp;&nbsp;称
								</td>
								<td style="width: 15%;">
									<asp:DropDownList ID="ddltPostAndRank" TabIndex="11" Width="90%" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									现居住地
								</td>
								<td colspan="2">
									<asp:TextBox ID="txtAddress" CssClass="text-input" TabIndex="12" Width="90%" MaxLength="150" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" style="width: 10%">
									户口所在地
								</td>
								<td colspan="2">
									<asp:TextBox ID="txtRegisteredPlace" CssClass="text-input" TabIndex="13" Width="90%" MaxLength="150" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									人员类型
								</td>
								<td style="height: 26px; width: 15%;">
									<asp:DropDownList ID="ddltClassID" DataSourceID="SqlPesonnelType" DataTextField="ClassName" DataValueField="ClassID" TabIndex="14" Width="90%" runat="server"></asp:DropDownList>
									<asp:SqlDataSource ID="SqlPesonnelType" SelectCommand="SELECT [ClassID], [ClassName], [ClassTypeCode] FROM [PT_SingleClasses] 
                                        WHERE [IsValid]='1' AND ([ClassTypeCode] = (SELECT ClassTypeCode FROM PT_D_SingleClass 
                                        WHERE [FilterField] = @ClassTypeCode))" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="HumanType" Name="ClassTypeCode" Type="String" /></SelectParameters></asp:SqlDataSource>
								</td>
								<td class="td-label" width="10%">
									电脑水平
								</td>
								<td style="height: 26px; width: 15%;">
									<JWControl:Combox ID="CombComputeLevel" onfocus="this.select()" TabIndex="15" MaxLength="25" Width="90%" runat="server"></JWControl:Combox>
								</td>
								<td class="td-label" width="10%">
									外语水平
								</td>
								<td style="height: 26px; width: 15%;">
									<JWControl:Combox ID="CombEnglishLevel" onfocus="this.select()" TabIndex="16" MaxLength="25" Width="90%" runat="server"></JWControl:Combox>
								</td>
							</tr>
							<tr>
								<td style="width: 10%; text-align: right;" class="td-label">
									户籍性质
								</td>
								<td style="width: 15%;">
									<asp:DropDownList ID="ddlrdeNature" runat="server"><asp:ListItem Text="本地城镇" /><asp:ListItem Text="本地农村" /><asp:ListItem Text="外地城镇" /><asp:ListItem Text="外地农村" /></asp:DropDownList>
								</td>
								<td class="td-label" style="width: 10%">
									转正日期
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="dbFormalDate" CssClass="Wdate" onfocus="WdatePicker({el:'dbFormalDate',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td style="width: 10%; text-align: right;" class="td-label">
									最新合同终止日期
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtconEndDate" CssClass="Wdate" onfocus="WdatePicker({el:'txtconEndDate',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td style="width: 10%; text-align: right;" class="td-label">
									紧急联系人
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txturgentCellMan" Width="90%" MaxLength="10" CssClass="text-input" runat="server"></asp:TextBox>
								</td>
								<td style="width: 10%; text-align: right;" class="td-label">
									与本人关系
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtucmConcern" Width="90%" MaxLength="10" CssClass="text-input" runat="server"></asp:TextBox>
								</td>
								<td style="width: 10%; text-align: right;" class="td-label">
									联系人电话
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtucmTel" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" Width="90%" MaxLength="20" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									婚姻状况
								</td>
								<td style="width: 15%;">
									<asp:DropDownList ID="ddltMarriage" TabIndex="17" Width="90%" runat="server"><asp:ListItem Value="未婚" Text="未婚" /><asp:ListItem Text="已婚" /></asp:DropDownList>
								</td>
								<td class="td-label" width="10%">
									个人照片
								</td>
								<td colspan="5">
									<div id="tr_add" runat="server">
										<asp:FileUpload ID="FileUpload1" Width="90%" TabIndex="18" runat="server" />
									</div>
									<div id="tr_edit" runat="server">
										<a id="annexName" href="#" onclick="annexEdit();" style="cursor: hand" runat="server">
										</a>
										<asp:TextBox ID="txtOriginalName" CssClass="text-input" TabIndex="4" Enabled="false" Visible="false" runat="server"></asp:TextBox>
										<asp:ImageButton ID="ImageBtn" ImageUrl="~/images/del.gif" OnClick="ImageBtn_Click" runat="server" />
										<input id="hdnFilePath" style="width: 10px" type="hidden" name="hdnFilePath" runat="server" />

									</div>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									驾驶水平
								</td>
								<td style="width: 15%;">
									<JWControl:Combox ID="CombDriveLevel" onfocus="this.select()" TabIndex="19" MaxLength="25" Width="90%" runat="server"></JWControl:Combox>
								</td>
								<td class="td-label" width="10%">
									联系电话
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtTel" CssClass="easyui-numberbox" data-options="min:0,max:99999999999" TabIndex="20" Width="90%" MaxLength="20" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									期望薪金
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtExpectationSalary" CssClass="text-input" TabIndex="21" Width="90%" MaxLength="5" runat="server"></asp:TextBox>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^(0|[1-9][0-9]*)$" Display="None" ControlToValidate="txtExpectationSalary" ErrorMessage="期望薪金输入错误" runat="server"></asp:RegularExpressionValidator>
								</td>
								<td class="td-label" width="10%">
									邮&nbsp;&nbsp;&nbsp;&nbsp;编
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtPostcode" CssClass="text-input" TabIndex="22" Width="90%" MaxLength="6" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									手机号
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtMobilePhoneCode" CssClass="text-input" MaxLength="13" TabIndex="20" Width="90%" runat="server"></asp:TextBox>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtMobilePhoneCode" Display="None" ErrorMessage="手机号输入错误" ValidationExpression="^1[3-9][0-9]{1}[0-9]{8}$|^15[0-9]{1}[0-9]{8}$" runat="server"></asp:RegularExpressionValidator>
								</td>
								<td class="td-label" width="10%">
									出生日期
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="dbBirthday" CssClass="Wdate" onfocus="WdatePicker({el:'dbBirthday',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									身份证号
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtIDCard" CssClass="text-input" TabIndex="21" Width="90%" MaxLength="18" runat="server"></asp:TextBox>&nbsp;
								</td>
								<td class="td-label" width="10%">
									部门负责人
								</td>
								<td width="15%">
									<asp:CheckBox ID="chbIsChargeMan" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									政治面貌
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtPoliticsFace" CssClass="text-input" MaxLength="25" TabIndex="20" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									入党时间
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="dbJoinPartyDate" CssClass="Wdate" onfocus="WdatePicker({el:'dbJoinPartyDate',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									入司渠道
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtJoinCorpMode" CssClass="text-input" TabIndex="21" Width="90%" MaxLength="25" runat="server"></asp:TextBox>&nbsp;
								</td>
								<td class="td-label" style="width: 10%">
									介绍人
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtIntroducer" CssClass="text-input" TabIndex="22" Width="90%" MaxLength="10" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									毕业院校
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtGraduateSchool" CssClass="text-input" MaxLength="50" TabIndex="20" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									专业
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtSpecialty" CssClass="text-input" MaxLength="25" TabIndex="20" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td class="td-label" width="10%">
									学历层级
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtLevel" CssClass="text-input" TabIndex="21" Width="90%" MaxLength="5" runat="server"></asp:TextBox>&nbsp;
								</td>
								<td class="td-label" style="width: 10%">
									参加工作时间
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="dbBeginWorkDate" Width="90%" CssClass="Wdate" onfocus="WdatePicker({el:'dbBeginWorkDate',dateFmt:'yyyy-MM-dd'})" runat="server"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									职称与资格
								</td>
								<td style="width: 15%;">
									<asp:TextBox ID="txtPostAndCompetency" CssClass="text-input" MaxLength="20" TabIndex="20" Width="90%" runat="server"></asp:TextBox>
								</td>
								<td colspan="6">
									<asp:CheckBoxList ID="cblInsurance" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Text="养老保险" /><asp:ListItem Value="2" Text="医疗保险" /><asp:ListItem Value="3" Text="失业保险" /><asp:ListItem Value="4" Text="工伤保险" /><asp:ListItem Value="5" Text="住房公积金" /><asp:ListItem Value="6" Text="人身意外险" /></asp:CheckBoxList>
								</td>
							</tr>
							<tr>
								<td class="td-label" width="10%">
									通讯地址
								</td>
								<td colspan="3" style="height: 26px">
									<asp:TextBox ID="txtCommunicateAddress" CssClass="text-input" TabIndex="23" Width="90%" MaxLength="100" runat="server"></asp:TextBox>
								</td>
								<td>
								</td>
								<td>
								</td>
							</tr>
							<tr>
								<td class="td-submit" colspan="8" height="20">
									<asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
									<input id="BtnClose" onclick="top.ui.closeTab();" type="button" value="取 消" name="BtnClose">
								</td>
							</tr>
						</table>
					</iewc:PageView><iewc:PageView ID="PageView2" runat="server">
						<table class="table-form" id="Table3" cellspacing="0" cellpadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-head" height="20">
									工作经历
								</td>
							</tr>
							<tr>
								<td class="td-toolsbar">
									<input type="hidden" id="hdnRecordID1" name="hdnRecordID1" style="width: 10px" runat="server" />

									<asp:Button ID="btnAdd1" Text="新 增" OnClick="btnAdd1_Click" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="gvExperience" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlExperience" Width="100%" ShowFooter="true" OnRowDataBound="gvExperience_RowDataBound" OnRowEditing="gvExperience_RowEditing" OnRowCommand="gvExperience_RowCommand" OnRowDeleting="gvExperience_RowDeleting" OnRowCancelingEdit="gvExperience_RowCancelingEdit" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
											<table class="grid" cellspacing="0" rules="all" border="1" id="gvExperience" style="border-collapse: collapse;">
												<tr class="grid_head">
													<th scope="col" style="width: 40px;">
														序号
													</th>
													<th scope="col" style="width: 10%;">
														起始日期
													</th>
													<th scope="col" style="width: 10%;">
														截止日期
													</th>
													<th scope="col">
														公司名称
													</th>
													<th scope="col" style="width: 20%;">
														职位
													</th>
													<th scope="col" style="width: 12%;">
													</th>
												</tr>
												<tr id="footer" runat="server"><td id="Td1" align="center" runat="server">
													</td><td id="Td2" runat="server">
														<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td3" runat="server">
														<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td4" runat="server">
														<asp:TextBox ID="txtEnterpriseName" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td5" runat="server">
														<asp:TextBox ID="txtPost" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td6" runat="server">
														<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
														<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
													</td></tr>
											</table>
										</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>

<FooterTemplate>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="起始日期" SortExpression="StartDate"><EditItemTemplate>
													
													<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" Text='<%# Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbStartDate" Text='<%# Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													
													<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="截止日期" SortExpression="EndDate"><EditItemTemplate>
													<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" Text='<%# Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbEndDate" Text='<%# Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="公司名称" SortExpression="EnterpriseName"><EditItemTemplate>
													<asp:TextBox ID="txtEnterpriseName" MaxLength="20" Width="100%" Text='<%# Convert.ToString(Eval("EnterpriseName")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbEnterpriseName" Text='<%# Convert.ToString(Eval("EnterpriseName")) %>' runat="server"></asp:Label>
												</ItemTemplate><FooterTemplate>
													<asp:TextBox ID="txtEnterpriseName" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
												</FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="职位" SortExpression="Post"><EditItemTemplate>
													<asp:TextBox ID="txtPost" Width="100%" MaxLength="20" Text='<%# Convert.ToString(Eval("Post")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbPost" Text='<%# Convert.ToString(Eval("Post")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtPost" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="">
<ItemTemplate>
													<asp:LinkButton ID="btnEdit" Text="编 辑" CommandName="Edit" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnDelete" Text="删 除" CommandName="Delete" runat="server"></asp:LinkButton>
												</ItemTemplate>

<EditItemTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Update" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</EditItemTemplate>

<FooterTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</FooterTemplate>
</asp:TemplateField></Columns></asp:GridView>
									<asp:SqlDataSource ID="SqlExperience" SelectCommand="SELECT [RecordID], [UserCode], [StartDate], [EndDate], [EnterpriseName], [Post] FROM [HR_Personnel_Experience] WHERE ([UserCode] = @UserCode)" DeleteCommand="DELETE FROM [HR_Personnel_Experience] WHERE [RecordID] = @original_RecordID" InsertCommand="INSERT INTO [HR_Personnel_Experience] ([UserCode], [StartDate], [EndDate], [EnterpriseName], [Post]) VALUES (@UserCode, @StartDate, @EndDate, @EnterpriseName, @Post)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [HR_Personnel_Experience] SET [UserCode] = @UserCode, [StartDate] = @StartDate, [EndDate] = @EndDate, [EnterpriseName] = @EnterpriseName, [Post] = @Post WHERE [RecordID] = @original_RecordID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters><DeleteParameters><asp:Parameter Name="original_RecordID" Type="Int32" /></DeleteParameters><UpdateParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="StartDate" Type="DateTime" /><asp:Parameter Name="EndDate" Type="DateTime" /><asp:Parameter Name="EnterpriseName" Type="String" /><asp:Parameter Name="Post" Type="String" /><asp:Parameter Name="original_RecordID" Type="Int32" /></UpdateParameters><InsertParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="StartDate" Type="DateTime" /><asp:Parameter Name="EndDate" Type="DateTime" /><asp:Parameter Name="EnterpriseName" Type="String" /><asp:Parameter Name="Post" Type="String" /></InsertParameters></asp:SqlDataSource>
								</td>
							</tr>
						</table>
					</iewc:PageView><iewc:PageView ID="PageView3" runat="server">
						<table class="table-form" id="Table4" cellspacing="0" cellpadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-head" height="20">
									奖惩情况
								</td>
							</tr>
							<tr>
								<td class="td-toolsbar">
									<input type="hidden" id="hdnRecordID2" name="hdnRecordID2" style="width: 10px" runat="server" />

									<asp:Button ID="btnAdd2" Text="新 增" OnClick="btnAdd2_Click" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="gvRewardsAndPunishment" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlRewardsAndPunishment" Width="100%" ShowFooter="true" OnRowDataBound="gvRewardsAndPunishment_RowDataBound" OnRowEditing="gvRewardsAndPunishment_RowEditing" OnRowCommand="gvRewardsAndPunishment_RowCommand" OnRowDeleting="gvRewardsAndPunishment_RowDeleting" OnRowCancelingEdit="gvRewardsAndPunishment_RowCancelingEdit" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
											<table class="grid" cellspacing="0" rules="all" border="1" id="gvRewardsAndPunishment"
												style="border-collapse: collapse;">
												<tr class="grid_head">
													<th scope="col" style="width: 40px;">
														序号
													</th>
													<th scope="col">
														荣誉称号
													</th>
													<th scope="col" style="width: 25%;">
														奖惩原因
													</th>
													<th scope="col">
														授予日期
													</th>
													<th scope="col" style="width: 20%;">
														授予单位
													</th>
													<th scope="col" style="width: 12%;">
													</th>
												</tr>
												<tr id="footer" runat="server"><td id="Td7" align="center" runat="server">
													</td><td id="Td8" runat="server">
														<asp:TextBox ID="txtCredit" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td9" runat="server">
														<asp:TextBox ID="txtReason" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td10" runat="server">
														<asp:TextBox ID="dbAwardDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td11" runat="server">
														<asp:TextBox ID="txtAwardOrg" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td12" runat="server">
														<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
														<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
													</td></tr>
											</table>
										</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>

<FooterTemplate>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="荣誉称号" SortExpression="Credit"><EditItemTemplate>
													<asp:TextBox ID="txtCredit" Width="100%" Text='<%# Convert.ToString(Eval("Credit")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbCredit" Text='<%# Convert.ToString(Eval("Credit")) %>' runat="server"></asp:Label>
												</ItemTemplate><FooterTemplate>
													<asp:TextBox ID="txtCredit" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="奖惩原因" SortExpression="Reason"><EditItemTemplate>
													<asp:TextBox ID="txtReason" Width="100%" Text='<%# Convert.ToString(Eval("Reason")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbReason" Text='<%# Convert.ToString(Eval("Reason")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtReason" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="授予日期" SortExpression="AwardDate"><EditItemTemplate>
													<asp:TextBox ID="dbAwardDate" Width="120px" onclick="WdatePicker()" Text='<%# Convert.ToString(Eval("AwardDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbAwardDate" Text='<%# Convert.ToString(Eval("AwardDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="dbAwardDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="授予单位" SortExpression="AwardOrg"><EditItemTemplate>
													<asp:TextBox ID="txtAwardOrg" Width="100%" Text='<%# Convert.ToString(Eval("AwardOrg")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbAwardOrg" Text='<%# Convert.ToString(Eval("AwardOrg")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtAwardOrg" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="">
<ItemTemplate>
													<asp:LinkButton ID="btnEdit" Text="编 辑" CommandName="Edit" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnDelete" Text="删 除" CommandName="Delete" runat="server"></asp:LinkButton>
												</ItemTemplate>

<EditItemTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Update" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</EditItemTemplate>

<FooterTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</FooterTemplate>
</asp:TemplateField></Columns></asp:GridView>
									<asp:SqlDataSource ID="SqlRewardsAndPunishment" DeleteCommand="DELETE FROM [HR_Personnel_RewardsAndPunishment] WHERE [RecordID] = @original_RecordID" InsertCommand="INSERT INTO [HR_Personnel_RewardsAndPunishment] ([UserCode], [Credit], [Reason], [AwardDate], [AwardOrg]) VALUES (@UserCode, @Credit, @Reason, @AwardDate, @AwardOrg)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [Credit], [Reason], [AwardDate], [AwardOrg] FROM [HR_Personnel_RewardsAndPunishment] WHERE ([UserCode] = @UserCode)" UpdateCommand="UPDATE [HR_Personnel_RewardsAndPunishment] SET [UserCode] = @UserCode, [Credit] = @Credit, [Reason] = @Reason, [AwardDate] = @AwardDate, [AwardOrg] = @AwardOrg WHERE [RecordID] = @original_RecordID " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><DeleteParameters><asp:Parameter Name="original_RecordID" Type="Int32" /></DeleteParameters><UpdateParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="Credit" Type="String" /><asp:Parameter Name="Reason" Type="String" /><asp:Parameter Name="AwardDate" Type="DateTime" /><asp:Parameter Name="AwardOrg" Type="String" /><asp:Parameter Name="original_RecordID" Type="Int32" /></UpdateParameters><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters><InsertParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="Credit" Type="String" /><asp:Parameter Name="Reason" Type="String" /><asp:Parameter Name="AwardDate" Type="DateTime" /><asp:Parameter Name="AwardOrg" Type="String" /></InsertParameters></asp:SqlDataSource>
								</td>
							</tr>
						</table>
					</iewc:PageView><iewc:PageView ID="PageView4" runat="server">
						<table class="table-form" id="Table5" cellspacing="0" cellpadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-head" height="20">
									教育经历
								</td>
							</tr>
							<tr>
								<td class="td-toolsbar">
									<input type="hidden" id="hdnRecordID3" name="hdnRecordID3" style="width: 10px" runat="server" />

									<asp:Button ID="btnAdd3" Text="新 增" OnClick="btnAdd3_Click" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="gvEducation" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlEducation" Width="100%" ShowFooter="true" OnRowDataBound="gvEducation_RowDataBound" OnRowEditing="gvEducation_RowEditing" OnRowCommand="gvEducation_RowCommand" OnRowDeleting="gvEducation_RowDeleting" OnRowCancelingEdit="gvEducation_RowCancelingEdit" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
											<table class="grid" cellspacing="0" rules="all" border="1" id="gvRewardsAndPunishment"
												style="border-collapse: collapse;">
												<tr class="grid_head">
													<th scope="col" style="width: 40px;">
														序号
													</th>
													<th scope="col" style="width: 10%;">
														起始日期
													</th>
													<th scope="col" style="width: 10%;">
														截止日期
													</th>
													<th scope="col">
														学校名称
													</th>
													<th scope="col" style="width: 20%;">
														专业
													</th>
													<th scope="col" style="width: 12%;">
													</th>
												</tr>
												<tr id="footer" runat="server"><td id="Td13" align="center" runat="server">
													</td><td id="Td14" runat="server">
														<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td15" runat="server">
														<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td16" runat="server">
														<asp:TextBox ID="txtSchoolName" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td17" runat="server">
														<asp:TextBox ID="txtSpecialty" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td18" runat="server">
														<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
														<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
													</td></tr>
											</table>
										</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>

<FooterTemplate>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="起始日期" SortExpression="StartDate"><EditItemTemplate>
													<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" Text='<%# Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbStartDate" Text='<%# Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="截止日期" SortExpression="EndDate"><EditItemTemplate>
													<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" Text='<%# Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbEndDate" Text='<%# Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="学校名称" SortExpression="SchoolName"><EditItemTemplate>
													<asp:TextBox ID="txtSchoolName" Width="100%" Text='<%# Convert.ToString(Eval("SchoolName")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbSchoolName" Text='<%# Convert.ToString(Eval("SchoolName")) %>' runat="server"></asp:Label>
												</ItemTemplate><FooterTemplate>
													<asp:TextBox ID="txtSchoolName" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="专业" SortExpression="Specialty"><EditItemTemplate>
													<asp:TextBox ID="txtSpecialty" Width="100%" Text='<%# Convert.ToString(Eval("Specialty")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbSpecialty" Text='<%# Convert.ToString(Eval("Specialty")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtSpecialty" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="">
<ItemTemplate>
													<asp:LinkButton ID="btnEdit" Text="编 辑" CommandName="Edit" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnDelete" Text="删 除" CommandName="Delete" runat="server"></asp:LinkButton>
												</ItemTemplate>

<EditItemTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Update" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</EditItemTemplate>

<FooterTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</FooterTemplate>
</asp:TemplateField></Columns></asp:GridView>
									<asp:SqlDataSource ID="SqlEducation" DeleteCommand="DELETE FROM [HR_Personnel_Education] WHERE [RecordID] = @original_RecordID " InsertCommand="INSERT INTO [HR_Personnel_Education] ([UserCode], [StartDate], [EndDate], [SchoolName], [Specialty]) VALUES (@UserCode, @StartDate, @EndDate, @SchoolName, @Specialty)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [StartDate], [EndDate], [SchoolName], [Specialty] FROM [HR_Personnel_Education] WHERE ([UserCode] = @UserCode)" UpdateCommand="UPDATE [HR_Personnel_Education] SET [UserCode] = @UserCode, [StartDate] = @StartDate, [EndDate] = @EndDate, [SchoolName] = @SchoolName, [Specialty] = @Specialty WHERE [RecordID] = @original_RecordID " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><DeleteParameters><asp:Parameter Name="original_RecordID" Type="Int32" /></DeleteParameters><UpdateParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="StartDate" Type="DateTime" /><asp:Parameter Name="EndDate" Type="DateTime" /><asp:Parameter Name="SchoolName" Type="String" /><asp:Parameter Name="Specialty" Type="String" /><asp:Parameter Name="original_RecordID" Type="Int32" /></UpdateParameters><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters><InsertParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="StartDate" Type="DateTime" /><asp:Parameter Name="EndDate" Type="DateTime" /><asp:Parameter Name="SchoolName" Type="String" /><asp:Parameter Name="Specialty" Type="String" /></InsertParameters></asp:SqlDataSource>
								</td>
							</tr>
						</table>
					</iewc:PageView><iewc:PageView ID="PageView5" runat="server">
						<table class="table-form" id="Table6" cellspacing="0" cellpadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-head" height="20">
									培训记录
								</td>
							</tr>
							<tr>
								<td class="td-toolsbar">
									<input type="hidden" id="hdnRecordID4" name="hdnRecordID4" style="width: 10px" runat="server" />

									<asp:Button ID="btnAdd4" Text="新 增" OnClick="btnAdd4_Click" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="gvTrain" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlTrain" Width="100%" ShowFooter="true" OnRowDataBound="gvTrain_RowDataBound" OnRowEditing="gvTrain_RowEditing" OnRowCommand="gvTrain_RowCommand" OnRowDeleting="gvTrain_RowDeleting" OnRowCancelingEdit="gvTrain_RowCancelingEdit" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
											<table class="grid" cellspacing="0" rules="all" border="1" id="gvTrain" style="border-collapse: collapse;">
												<tr class="grid_head">
													<th scope="col" style="width: 40px;">
														序号
													</th>
													<th scope="col" style="width: 15%;">
														课程名称
													</th>
													<th scope="col" style="width: 10%;">
														课时
													</th>
													<th scope="col" style="width: 10%;">
														起始日期
													</th>
													<th scope="col" style="width: 10%;">
														截止日期
													</th>
													<th scope="col">
														培训机构
													</th>
													<th scope="col" style="width: 20%;">
														备注
													</th>
													<th scope="col" style="width: 12%;">
													</th>
												</tr>
												<tr id="footer" runat="server"><td id="Td19" align="center" runat="server">
													</td><td id="Td20" runat="server">
														<asp:TextBox ID="txtCourses" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td21" runat="server">
														<asp:TextBox ID="txtHour" Width="100%" MaxLength="3" runat="server"></asp:TextBox>
													</td><td id="Td22" runat="server">
														<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td23" runat="server">
														<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
													</td><td id="Td24" runat="server">
														<asp:TextBox ID="txtTrainOrgan" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td25" runat="server">
														<asp:TextBox ID="txtRemark" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td26" runat="server">
														<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
														<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
													</td></tr>
											</table>
										</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>

<FooterTemplate>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="课程名称" SortExpression="Courses"><EditItemTemplate>
													<asp:TextBox ID="txtCourses" Width="100%" Text='<%# Convert.ToString(Eval("Courses")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbCourses" Text='<%# Convert.ToString(Eval("Courses")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtCourses" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="课时" SortExpression="Hour"><EditItemTemplate>
													<asp:TextBox ID="txtHour" Width="100%" Text='<%# Convert.ToString(Eval("Hour")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbHour" Text='<%# Convert.ToString(Eval("Hour")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtHour" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="起始日期" SortExpression="StartDate"><EditItemTemplate>
													<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" Text='<%# Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbStartDate" Text='<%# Convert.ToString(Eval("StartDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="dbStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="截止日期" SortExpression="EndDate"><ItemTemplate>
													<asp:Label ID="lbEndDate" Text='<%# Convert.ToString(Eval("EndDate", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<EditItemTemplate>
													<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</EditItemTemplate>

<FooterTemplate>
													<asp:TextBox ID="dbEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="培训机构" SortExpression="TrainOrgan"><EditItemTemplate>
													<asp:TextBox ID="txtTrainOrgan" Width="100%" Text='<%# Convert.ToString(Eval("TrainOrgan")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbTrainOrgan" Text='<%# Convert.ToString(Eval("TrainOrgan")) %>' runat="server"></asp:Label>
												</ItemTemplate><FooterTemplate>
													<asp:TextBox ID="txtTrainOrgan" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" SortExpression="Remark"><EditItemTemplate>
													<asp:TextBox ID="txtRemark" Width="100%" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtRemark" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
													<asp:LinkButton ID="btnEdit" Text="编 辑" CommandName="Edit" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnDelete" Text="删 除" CommandName="Delete" runat="server"></asp:LinkButton>
												</ItemTemplate>

<EditItemTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Update" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</EditItemTemplate>

<FooterTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</FooterTemplate>
</asp:TemplateField></Columns></asp:GridView>
									<asp:SqlDataSource ID="SqlTrain" DeleteCommand="DELETE FROM [HR_Personnel_Train] WHERE [RecordID] = @original_RecordID " InsertCommand="INSERT INTO [HR_Personnel_Train] ([UserCode], [Courses], [Hour], [StartDate], [EndDate], [TrainOrgan], [Remark]) VALUES (@UserCode, @Courses, @Hour, @StartDate, @EndDate, @TrainOrgan, @Remark)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [Courses], [Hour], [StartDate], [EndDate], [TrainOrgan], [Remark] FROM [HR_Personnel_Train] WHERE ([UserCode] = @UserCode)" UpdateCommand="UPDATE [HR_Personnel_Train] SET [UserCode] = @UserCode, [Courses] = @Courses, [Hour] = @Hour, [StartDate] = @StartDate, [EndDate] = @EndDate, [TrainOrgan] = @TrainOrgan, [Remark] = @Remark WHERE [RecordID] = @original_RecordID " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><DeleteParameters><asp:Parameter Name="original_RecordID" Type="Int32" /></DeleteParameters><UpdateParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="Courses" Type="String" /><asp:Parameter Name="Hour" Type="String" /><asp:Parameter Name="StartDate" Type="DateTime" /><asp:Parameter Name="EndDate" Type="DateTime" /><asp:Parameter Name="TrainOrgan" Type="String" /><asp:Parameter Name="Remark" Type="String" /><asp:Parameter Name="original_RecordID" Type="Int32" /></UpdateParameters><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters><InsertParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="Courses" Type="String" /><asp:Parameter Name="Hour" Type="String" /><asp:Parameter Name="StartDate" Type="DateTime" /><asp:Parameter Name="EndDate" Type="DateTime" /><asp:Parameter Name="TrainOrgan" Type="String" /><asp:Parameter Name="Remark" Type="String" /></InsertParameters></asp:SqlDataSource>
								</td>
							</tr>
						</table>
					</iewc:PageView><iewc:PageView ID="PageView6" runat="server">
						<table class="table-form" id="Table7" cellspacing="0" cellpadding="0" width="100%"
							border="1">
							<tr>
								<td class="td-head" height="20">
									家庭关系
								</td>
							</tr>
							<tr>
								<td class="td-toolsbar">
									<input type="hidden" id="hdnRecordID5" name="hdnRecordID5" style="width: 10px" runat="server" />

									<asp:Button ID="btnAdd5" Text="新 增" OnClick="btnAdd5_Click" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="gvFamilyMembers" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlFamilyMembers" Width="100%" ShowFooter="true" OnRowDataBound="gvFamilyMembers_RowDataBound" OnRowEditing="gvFamilyMembers_RowEditing" OnRowCommand="gvFamilyMembers_RowCommand" OnRowDeleting="gvFamilyMembers_RowDeleting" OnRowCancelingEdit="gvFamilyMembers_RowCancelingEdit" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
											<table class="grid" cellspacing="0" rules="all" border="1" id="gvTrain" style="border-collapse: collapse;">
												<tr class="grid_head">
													<th scope="col" style="width: 40px;">
														序号
													</th>
													<th scope="col" style="width: 15%;">
														姓名
													</th>
													<th scope="col" style="width: 25%;">
														与本人关系
													</th>
													<th scope="col" style="width: 15%;">
														联系电话
													</th>
													<th scope="col">
														工作单位
													</th>
													<th scope="col" style="width: 12%;">
													</th>
												</tr>
												<tr id="footer" runat="server"><td id="Td27" align="center" runat="server">
													</td><td id="Td28" runat="server">
														<asp:TextBox ID="txtName" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td id="Td29" runat="server">
														<asp:TextBox ID="txtRelation" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td runat="server">
														<asp:TextBox ID="txtTel" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td runat="server">
														<asp:TextBox ID="txtWorkUnit" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
													</td><td runat="server">
														<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
														<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
													</td></tr>
											</table>
										</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>

<FooterTemplate>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="姓名" SortExpression="Name"><EditItemTemplate>
													<asp:TextBox ID="txtName" Width="100%" Text='<%# Convert.ToString(Eval("Name")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbName" Text='<%# Convert.ToString(Eval("Name")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtName" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="与本人关系" SortExpression="Relation"><EditItemTemplate>
													<asp:TextBox ID="txtRelation" Width="100%" Text='<%# Convert.ToString(Eval("Relation")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbRelation" Text='<%# Convert.ToString(Eval("Relation")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtRelation" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="联系电话" SortExpression="Tel"><EditItemTemplate>
													<asp:TextBox ID="txtTel" Width="100%" Text='<%# Convert.ToString(Eval("Tel")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbTel" Text='<%# Convert.ToString(Eval("Tel")) %>' runat="server"></asp:Label>
												</ItemTemplate>
<FooterTemplate>
													<asp:TextBox ID="txtTel" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工作单位" SortExpression="WorkUnit"><EditItemTemplate>
													<asp:TextBox ID="txtWorkUnit" Width="100%" Text='<%# Convert.ToString(Eval("WorkUnit")) %>' runat="server"></asp:TextBox>
												</EditItemTemplate><ItemTemplate>
													<asp:Label ID="lbWorkUnit" Text='<%# Convert.ToString(Eval("WorkUnit")) %>' runat="server"></asp:Label>
												</ItemTemplate><FooterTemplate>
													<asp:TextBox ID="txtWorkUnit" Width="100%" runat="server"></asp:TextBox>
												</FooterTemplate></asp:TemplateField><asp:TemplateField>
<ItemTemplate>
													<asp:LinkButton ID="btnEdit" Text="编 辑" CommandName="Edit" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnDelete" Text="删 除" CommandName="Delete" runat="server"></asp:LinkButton>
												</ItemTemplate>

<EditItemTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Update" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</EditItemTemplate>

<FooterTemplate>
													<asp:LinkButton ID="btnSave" Text="保 存" CommandName="Insert" runat="server"></asp:LinkButton>
													<asp:LinkButton ID="btnCancel" Text="取 消" CommandName="Cancel" runat="server"></asp:LinkButton>
												</FooterTemplate>
</asp:TemplateField></Columns></asp:GridView>
									<asp:SqlDataSource ID="SqlFamilyMembers" DeleteCommand="DELETE FROM [HR_Personnel_FamilyMembers] WHERE [RecordID] = @original_RecordID " InsertCommand="INSERT INTO [HR_Personnel_FamilyMembers] ([UserCode], [Name], [Relation], [Tel], [WorkUnit]) VALUES (@UserCode, @Name, @Relation, @Tel, @WorkUnit)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [RecordID], [UserCode], [Name], [Relation], [Tel], [WorkUnit] FROM [HR_Personnel_FamilyMembers] WHERE ([UserCode] = @UserCode)" UpdateCommand="UPDATE [HR_Personnel_FamilyMembers] SET [UserCode] = @UserCode, [Name] = @Name, [Relation] = @Relation, [Tel] = @Tel, [WorkUnit] = @WorkUnit WHERE [RecordID] = @original_RecordID " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><DeleteParameters><asp:Parameter Name="original_RecordID" Type="Int32" /></DeleteParameters><UpdateParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="Name" Type="String" /><asp:Parameter Name="Relation" Type="String" /><asp:Parameter Name="Tel" Type="String" /><asp:Parameter Name="WorkUnit" Type="String" /><asp:Parameter Name="original_RecordID" Type="Int32" /></UpdateParameters><SelectParameters><asp:QueryStringParameter Name="UserCode" QueryStringField="uc" Type="String"></asp:QueryStringParameter></SelectParameters><InsertParameters><asp:Parameter Name="UserCode" Type="String" /><asp:Parameter Name="Name" Type="String" /><asp:Parameter Name="Relation" Type="String" /><asp:Parameter Name="Tel" Type="String" /><asp:Parameter Name="WorkUnit" Type="String" /></InsertParameters></asp:SqlDataSource>
								</td>
							</tr>
						</table>
					</iewc:PageView><iewc:PageView runat="server">
						<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" id="table8">
							<tr>
								<td height="20px" class="td-toolsbar">
									<asp:Button ID="btnAddDuty" Text="新 增" OnClick="btnAddDuty_Click" runat="server" />
									<asp:Button ID="btnDelDuty" Text="删  除" Enabled="false" OnClick="btnDelDuty_Click" runat="server" />
									<input id="HdnDUTYID" type="hidden" style="width: 20px" value="0" runat="server" />

									<input id="HdnDeptID" type="hidden" style="width: 20px" value="0" runat="server" />

								</td>
							</tr>
							<tr>
								<td valign="top" height="100%">
									<asp:GridView CssClass="grid" ID="GVDuty" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVDuty_RowDataBound" runat="server">
<EmptyDataTemplate>
											<table id="Tableb" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
												style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
												width: 100%; border-collapse: collapse; border-right-width: 0px">
												<tr align="center" class="grid_head">
													<th align="center" nowrap="nowrap" scope="col" style="width: 40px">
														序号
													</th>
													<th align="center" nowrap="nowrap" scope="col">
														部门
													</th>
													<th align="center" nowrap="nowrap" scope="col">
														岗位
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="V_BMMC" HeaderText="部门" HtmlEncode="false" /><asp:BoundField DataField="DutyName" HeaderText="岗位" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle></asp:GridView>
								</td>
							</tr>
						</table>
					</iewc:PageView></iewc:MultiPage>
				<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
