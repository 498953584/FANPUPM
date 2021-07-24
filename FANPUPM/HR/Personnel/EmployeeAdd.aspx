<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeAdd.aspx.cs" Inherits="EmployeeAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>员工信息登记</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#BtnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
        });

        function pickDuty(deptID) {
            var p = new Array();
            p[0] = "";
            p[1] = "";
            p[2] = "";
            var url = "";
            url = "PickSinglePerson.aspx?cc=" + deptID;
            window.showModalDialog(url, p, "dialogHeight:500px;dialogWidth:550px;center:1;help:0;status:0;");
            if (p[0] != "") {
                document.getElementById('hdnDept').value = p[0];
                document.getElementById("hdnSuperordinateDuty").value = p[1];
                document.getElementById('txtSuperordinateDuty').value = p[2];
            }
        }

        function getbutpy() {
            document.getElementById("butGetPy").click();
        }

        function successed() {
            top.ui.show('添加成功！');
            var depCode = jw.getRequestParam('cc');
            var pUrl = top.ui._Employee.location.href;
            pUrl = jw.modifyParam({ url: pUrl, name: 'depCode', value: depCode })
            top.ui._Employee.location.href = pUrl;
            top.ui.closeTab();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="table2" border="1" cellpadding="0" cellspacing="0" class="table-form"
        width="100%">
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
                <asp:TextBox ID="txtDeptName" CssClass="text-input" Enabled="false" TabIndex="2" Width="90%" runat="server"></asp:TextBox>
                <input id="hdnDept" style="width: 10px" type="hidden" name="hdnDept" runat="server" />

            </td>
            <td colspan="2" rowspan="8" align="center" valign="middle" style="width: 25%;">
                <asp:Image ID="Image1" ImageAlign="Top" GenerateEmptyAlternateText="true" AlternateText="照片" ImageUrl="~/images/PhotoDefault.gif" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                <span style="color: Red;">* </span>姓&nbsp;&nbsp;&nbsp;&nbsp;名
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txtName" CssClass="text-input easyui-validatebox" TabIndex="3" Width="90%" data-options="required:true, validType:'validChars[20]'" MaxLength="25" runat="server"></asp:TextBox>
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
                <JWControl:Combox ID="CombNation" TabIndex="6" MaxLength="25" Width="90%" runat="server"></JWControl:Combox>
            </td>
            <td class="td-label" width="10%" style="height: 26px">
                学&nbsp;&nbsp;&nbsp;&nbsp;历
            </td>
            <td style="height: 26px; width: 15%;">
                <JWControl:Combox ID="CombEducationalBackground" TabIndex="7" MaxLength="25" Width="90%" runat="server"></JWControl:Combox>
            </td>
            <td class="td-label" width="10%" style="height: 26px">
                身&nbsp;&nbsp;&nbsp;&nbsp;高
            </td>
            <td style="height: 26px; width: 15%;">
                <asp:TextBox ID="txtStature" CssClass="text-input" TabIndex="8" Width="90%" MaxLength="25" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                岗&nbsp;&nbsp;&nbsp;&nbsp;位
            </td>
            <td style="width: 15%;">
                <asp:DropDownList ID="ddltDuty" DataSourceID="SqlDuty" DataTextField="DutyName" DataValueField="i_dutyid" Width="90%" runat="server"></asp:DropDownList>
                <input id="hdnDuty" style="width: 10px" type="hidden" name="hdnDuty" runat="server" />

                <asp:SqlDataSource ID="SqlDuty" SelectCommand="select distinct i_dutyid ,i_bmdm, DutyName from pt_duty where i_bmdm = @DeptID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter Name="DeptID" ControlID="hdnDept" PropertyName="Value" runat="server" /></SelectParameters></asp:SqlDataSource>
                &nbsp;
            </td>
            <td class="td-label" width="10%">
                职&nbsp;&nbsp;&nbsp;&nbsp;级
            </td>
            <td style="width: 15%;">
                <asp:DropDownList ID="ddltPositionLevel" TabIndex="10" AutoPostBack="true" Width="90%" Height="16px" OnSelectedIndexChanged="ddltPositionLevel_SelectedIndexChanged" runat="server"></asp:DropDownList>
            </td>
            <td class="td-label" width="10%">
                职&nbsp;&nbsp;&nbsp;&nbsp;称
            </td>
            <td style="width: 15%;">
                <asp:DropDownList ID="ddltPostAndRank" TabIndex="11" DataSourceID="SqlPostAndRank" DataTextField="PostAndRank" DataValueField="RecordID" Width="90%" runat="server"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlPostAndRank" SelectCommand="SELECT [RecordID], [PostAndRank] FROM [HR_Org_PostLevel] WHERE ([PositionLevel] = @PositionLevel)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="ddltPositionLevel" Name="PositionLevel" PropertyName="SelectedValue" Type="Int32" runat="server" /></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%" style="height: 36px">
                现居住地
            </td>
            <td colspan="2" style="height: 36px; width: 15%;">
                <asp:TextBox ID="txtAddress" CssClass="text-input" TabIndex="12" Width="90%" MaxLength="150" runat="server"></asp:TextBox>
            </td>
            <td class="td-label" style="width: 10%; height: 36px;">
                户口所在地
            </td>
            <td colspan="2" style="height: 36px; width: 15%;">
                <asp:TextBox ID="txtRegisteredPlace" CssClass="text-input" TabIndex="13" Width="90%" MaxLength="150" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                人员类型
            </td>
            <td style="height: 26px; width: 15%;">
                <asp:DropDownList ID="ddltClassID" DataSourceID="SqlPesonnelType" DataTextField="ClassName" DataValueField="ClassID" TabIndex="14" Width="90%" runat="server"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlPesonnelType" SelectCommand="SELECT [ClassID], [ClassName], [ClassTypeCode] FROM [PT_SingleClasses] WHERE ([ClassTypeCode] = (SELECT ClassTypeCode FROM PT_D_SingleClass WHERE [FilterField] = @ClassTypeCode))" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="HumanType" Name="ClassTypeCode" Type="String" /></SelectParameters></asp:SqlDataSource>
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
            <td style="width: 10%;" class="td-label">
                户籍性质
            </td>
            <td style="width: 15%;">
                <asp:DropDownList ID="ddlrdeNature" runat="server"><asp:ListItem Text="本地城镇" /><asp:ListItem Text="本地农村" /><asp:ListItem Text="外地城镇" /><asp:ListItem Text="外地农村" /></asp:DropDownList>
            </td>
            <td style="width: 10%;" class="td-label">
                转正日期
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txtFormalData" CssClass="Wdate" onfocus="WdatePicker({el:'txtFormalData',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
            </td>
            <td style="width: 10%;" class="td-label">
                最新合同终止日期
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txtconEndDate" CssClass="Wdate" onfocus="WdatePicker({el:'txtconEndDate',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%;" class="td-label">
                紧急联系人
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txturgentCellMan" CssClass="text-input" Width="90%" MaxLength="10" runat="server"></asp:TextBox>
            </td>
            <td style="width: 10%;" class="td-label">
                与本人关系
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txtucmConcern" CssClass="text-input" Width="90%" MaxLength="10" runat="server"></asp:TextBox>
            </td>
            <td style="width: 10%;" class="td-label">
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
                <div id="tr_edit" style="display: none" runat="server">
                    <a id="annexName" href="#" onclick="annexEdit();" style="cursor: hand" runat="server" />

                    
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
                <asp:TextBox ID="txtExpectationSalary" CssClass="text-input" TabIndex="21" Width="50%" MaxLength="5" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^(0|[1-9][0-9]*)$" ControlToValidate="txtExpectationSalary" ErrorMessage="输入错误" runat="server"></asp:RegularExpressionValidator>
            </td>
            <td class="td-label" style="width: 10%">
                邮&nbsp;&nbsp;&nbsp;&nbsp;编
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txtPostcode" CssClass="text-input" TabIndex="22" Width="90%" MaxLength="6" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46" ondragenter="return false" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                手机号
            </td>
            <td style="width: 15%;">
                <asp:TextBox ID="txtMobilePhoneCode" CssClass="text-input" MaxLength="13" TabIndex="20" Width="90%" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46" ondragenter="return false" runat="server"></asp:TextBox>
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
            <td class="td-label" style="width: 10%">
                部门负责人
            </td>
            <td style="width: 15%;">
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
                <asp:TextBox ID="dbBeginWorkDate" ReadOnly="true" CssClass="Wdate" onfocus="WdatePicker({el:'dbBeginWorkDate',dateFmt:'yyyy-MM-dd'})" Width="90%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="10%">
                职称资格
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
            <td colspan="3" style="height: 26px; width: 15%;">
                <asp:TextBox ID="txtCommunicateAddress" CssClass="text-input" TabIndex="23" Width="90%" MaxLength="100" runat="server"></asp:TextBox>
            </td>
            <td class="td-label" style="width: 10%;">
                <span style="color: Red;">* </span>系统账号
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtLogin" MaxLength="25" CssClass="text-input easyui-validatebox" onDblClick="getbutpy();" ToolTip="双击此处使用姓名拼音缩写！" Width="90%" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>&nbsp;
                <asp:Image ID="Image2" ImageUrl="~/images/check_right.gif" Visible="false" ImageAlign="AbsMiddle" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td-submit" colspan="8">
                <asp:Button ID="butGetPy" Text="得到拼音缩写" CausesValidation="false" Style="display: none" OnClick="butGetPy_Click" runat="server" />
                <asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                <input id="BtnClose" onclick="top.ui.closeTab();" type="button" value="取 消" name="BtnClose">
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
    </form>
</body>
</html>
