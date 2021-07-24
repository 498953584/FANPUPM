<%@ Page Language="C#" AutoEventWireup="true" CodeFile="infoedit.aspx.cs" Inherits="InfoEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>工程信息编辑</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self" />

    <script language="javascript">
		
		function pickCorp()
			{
				var corp = new Array();
				corp[0] = "";
				corp[1] = "";
				var url= "/CommonWindow/PickCorp.aspx";
				window.showModalDialog(url,corp,"dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
				if (corp[0]!="")
				{
					document.getElementById('HdnOwnerCode').value = corp[0];
					document.getElementById('txt_Owner').value = corp[1];
				}
			}
			
			function pickCorp1()
			{
				var corp = new Array();
				corp[0] = "";
				corp[1] = "";
				var url= "/CommonWindow/PickCorp.aspx";
				window.showModalDialog(url,corp,"dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
				if (corp[0]!="")
				{
					//document.getElementById('HdnOwnerCode').value = corp[0];
					document.getElementById('Txt_WorkUnit').value = corp[1];
				}
			}
			function pickOnePersonSS() {
			    var human = new Array();
			    human[0] = "";
			    human[1] = "";
			    var url = "/CommonWindow/PickSinglePerson.aspx";
			    window.showModalDialog(url, human, "dialogHeight:434px;dialogWidth:400px;center:1;help:0;status:0;");
			    if (human[0] != "") {
			        document.getElementById('Txt_LinkManSS').value = human[0];
			        document.getElementById('Txt_LinkMan').value = human[1];
			    }
			}
			function pickOnePersonYW() {
			    var human = new Array();
			    human[0] = "";
			    human[1] = "";
			    var url = "/CommonWindow/PickSinglePerson.aspx";
			    window.showModalDialog(url, human, "dialogHeight:434px;dialogWidth:400px;center:1;help:0;status:0;");
			    if (human[0] != "") {
			        
			        document.getElementById('ManagerCodeYW').value = human[0];
			        document.getElementById('Txt_businessman').value = human[1];
			        //alert(document.getElementById('ManagerCodeYW').value);
			    }
			}
		function WinGoBack()
		{
			var Type = window.document.getElementById("hdnSeeType").value;
			if(Type=="1")
			{
				window.location.href = "InfoList.aspx?SqlWhere=&amp;jw=2";
			}
			else
			{
				if('<%=Request.QueryString["OpType"] %>' == "SEE")
				{
					window.location.href = "InfoList.aspx?SqlWhere=&amp;jw=3";
				}
				else
				{
					window.location.href = "InfoList.aspx?SqlWhere=&amp;jw=2";
				}
			}
		}
		function CheckInputIsFloat(keyCode,e)
		{
				if((keyCode>95 && keyCode<106) || (keyCode>47 && keyCode<58) || keyCode == 8|| keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13)
				{
			    }
				else if(keyCode == 110 || keyCode==190)
				{
					 if(e.value == "" || e.value.indexOf(".") != -1)
					 {
						 return false;
					 }
				} 
				else
				{
					return false;
				}
		}
		
		function pickOnePerson()
		{
			var human = new Array();
			human[0] = "";
			human[1] = "";
			var url= "/CommonWindow/PickSinglePerson.aspx";
			window.showModalDialog(url,human,"dialogHeight:434px;dialogWidth:400px;center:1;help:0;status:0;");
			if (human[0]!="")
			{
				document.getElementById('HideManagerCode').value = human[0];
				document.getElementById('Txt_PrjManager').value = human[1];
			}
		}
		//判断开始时间和结束时间--zyg
	    function getBeginDate()
		{
			if (document.getElementById('txt_EndDate').value!="")
			{

				var beginTT=getTheDate(document.getElementById('txt_StartDate').value);
				var endTT = getTheDate(document.getElementById('txt_EndDate').value);
				if (beginTT==endTT)
				{
				    return;
				}
				if (beginTT!="")
				{
				    //alert('结束时间='+endTT);
				    //alert('开始时间='+beginTT);
					if (endTT<beginTT)
					{
						alert('结束时间不能早于开始时间！');
						document.getElementById('txt_StartDate').value = "";
						document.getElementById('txt_StartDate').blur();
					}

				}
			}

		}
		 function getEndDate()
		{
			if (document.getElementById('txt_StartDate').value!="")
			{

				var beginTT=getTheDate(document.getElementById('txt_StartDate').value);
				var endTT = getTheDate(document.getElementById('txt_EndDate').value);
				if (beginTT==endTT)
				{
				    return;
				}
				if (beginTT!="")
				{
				    //alert('结束时间='+endTT);
				   // alert('开始时间='+beginTT);
					if (endTT<beginTT)
					{
						alert('结束时间不能早于开始时间！');
						document.getElementById('txt_EndDate').value = "";
						document.getElementById('txt_EndDate').blur();
					}

				}
			}

		}
		//重新设置取到的时间格式
	    function getTheDate(dateStr)
		{
			var theYear;
			var theMonth;
			var theDay;
			var i = dateStr.indexOf("-");
			var j = dateStr.lastIndexOf("-");
			theYear = parseInt(dateStr.substr(0,i));
			theMonth = parseInt(dateStr.substr(i+1,j-i-1))-1;
			theDay = parseInt(dateStr.substr(j+1));
			return new Date(theYear,theMonth,theDay);
		}
		function checklen(e,maxlength)
	    {
			 if(e.value.length > maxlength)
			 {
				alert('输入长度不能大于'+maxlength);
				e.focus();
				 return false;
			 }

	    }			
    </script>

</head>
<body class="body_frame" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table class="table-form" cellspacing="0" cellpadding="0" width="100%" border="1">
                        <tr class="td-title">
                            <td width="9%" colspan="6">
                                基本内容</td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                项目类型</td>
                            <td width="25%">
                                <JWControl:DropDownTree ID="drop_PrjKindClass" runat="server"></JWControl:DropDownTree></td>
                            <td class="td-label" style="width: 70px" width="70">
                                项目编号</td>
                            <td width="22%">
                                <label>
                                    <asp:TextBox ID="txt_PrjCode" Width="100%" MaxLength="15" runat="server"></asp:TextBox></label></td>
                            <td class="td-label" width="9%">
                                项目名称</td>
                            <td width="26%">
                                <asp:TextBox ID="txt_PrjName" Width="100%" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                开始时间</td>
                            <td>
                                <JWControl:DateBox ID="txt_StartDate" Width="100%" runat="server"></JWControl:DateBox></td>
                            <td class="td-label" style="width: 70px">
                                结束时间</td>
                            <td>
                                <JWControl:DateBox ID="txt_EndDate" Width="100%" runat="server"></JWControl:DateBox></td>
                            <td class="td-label">
                                工程工期</td>
                            <td>
                                <asp:TextBox ID="txt_Duration" Width="100%" MaxLength="10" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                质量等级</td>
                            <td>
                                <JWControl:DropDownTree ID="drop_QualityClass" runat="server"></JWControl:DropDownTree></td>
                            <td class="td-label" style="width: 70px">
                                项目造价</td>
                            <td>
                                <asp:TextBox ID="txt_PrjCost" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" runat="server"></asp:TextBox>(万元)</td>
                            <td class="td-label">
                                <span>甲方名称</span></td>
                            <td>
                                <input type="text" class="txtreadonly" id="txt_Owner" style="background-color: #ffffc0" runat="server" />
<img id="IMGOPEN" style="cursor: hand" onclick="pickCorp();" src="/images/contact.gif" align="absmiddle" runat="server" />
</td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                项目地区
                            </td>
                            <td>
                                <JWControl:DropDownTree ID="ddt_Area" runat="server"></JWControl:DropDownTree>
                            </td>
                            <td class="td-label">
                                项目状态
                            </td>
                            <td>
                                <JWControl:DropDownTree ID="ddt_state" AutoPostBack="true" OnSelectedIndexChanged="ddt_state_SelectedIndexChanged" runat="server"></JWControl:DropDownTree>
                            </td>
                            <td class="td-label">
                                上级项目
                            </td>
                            <td>
                                <asp:DropDownList ID="fatherPrj" Enabled="false" Width="150px" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                设计公司</td>
                            <td>
                                <asp:TextBox ID="txt_Designer" Width="100%" MaxLength="20" runat="server"></asp:TextBox></td>
                            <td class="td-label" style="width: 70px">
                                监理公司</td>
                            <td>
                                <asp:TextBox ID="txt_Inspector" Width="100%" MaxLength="20" runat="server"></asp:TextBox></td>
                            <td class="td-label">
                                招标代理</td>
                            <td>
                                <asp:TextBox ID="txt_Counsellor" Width="100%" MaxLength="20" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td class="td-label">
                                项目地点</td>
                            <td colspan="5">
                                <asp:TextBox ID="txt_PrjPlace" Width="100%" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                项目简介</td>
                            <td colspan="5">
                                <asp:TextBox ID="txt_PrjInfo" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="200" onkeyup="checklen(this,2000);" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                备注</td>
                            <td colspan="5">
                                <font face="宋体">
                                    <asp:TextBox ID="txt_Remark" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="200" onkeyup="checklen(this,300);" runat="server"></asp:TextBox></font></td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                相关附件</td>
                            <td colspan="5">
                                <label id="lbFile" runat="server">
                                </label>
                                <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                                <input id="hdnSeeType" type="hidden" name="hdnSeeType" runat="server" />
<input id="HdnOwnerCode" type="hidden" name="HdnOwnerCode" runat="server" />
</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr_sw" runat="server"><td runat="server">
                    <table class="table-form" cellspacing="0" cellpadding="0" width="100%" border="1">
                        <tr class="td-title">
                            <td width="9%" colspan="6">
                                商务要求</td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                资质等级</td>
                            <td width="25%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_Rank" Width="20px" Visible="false" runat="server"></asp:TextBox>
                                    <JWControl:DropDownTree ID="drop_zzGrade" Width="136px" runat="server"></JWControl:DropDownTree></font></td>
                            <td class="td-label" style="width: 70px" width="70">
                                预算方式</td>
                            <td width="22%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_BudgetWay" Width="20px" Visible="false" runat="server"></asp:TextBox>
                                    <JWControl:DropDownTree ID="drop_ysType" Width="136px" runat="server"></JWControl:DropDownTree></font></td>
                            <td class="td-label" width="9%">
                                <font face="宋体">承包方式</font></td>
                            <td width="26%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_ContractWay" Width="20px" Visible="false" runat="server"></asp:TextBox>
                                    <JWControl:DropDownTree ID="drop_cbType" Width="136px" runat="server"></JWControl:DropDownTree></font></td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                付款条件</td>
                            <td width="25%">
                                <asp:TextBox ID="Txt_PayCondition" Width="10px" Visible="false" runat="server"></asp:TextBox>
                                <JWControl:DropDownTree ID="drop_Payment" Width="136px" runat="server"></JWControl:DropDownTree></td>
                            <td class="td-label" style="width: 70px" width="70">
                                招标形式</td>
                            <td width="22%">
                                <asp:TextBox ID="Txt_TenderWay" Width="10px" Visible="false" runat="server"></asp:TextBox>
                                <JWControl:DropDownTree ID="drop_zbType" Width="136px" runat="server"></JWControl:DropDownTree></td>
                            <td class="td-label" width="9%">
                                结算方式</td>
                            <td width="26%">
                                <asp:TextBox ID="Txt_PayWay" Width="10px" Visible="false" runat="server"></asp:TextBox>
                                <JWControl:DropDownTree ID="drop_jsType" Width="136px" runat="server"></JWControl:DropDownTree></td>
                        </tr>
                        
                        <tr>
                            <td class="td-label" width="9%">
                                重要程序评价
                            </td>
                            <td width="25%">
                                <asp:TextBox ID="Txt_KeyPart" Width="10px" Visible="false" runat="server"></asp:TextBox>
                                <JWControl:DropDownTree ID="drop_PrimaryGrade" Width="136px" runat="server"></JWControl:DropDownTree>
                            </td>
                            
                              <td class="td-label" width="9%">
                                实施单位
                            </td>
                            <td width="25%">
                                <input type="text" class="txtreadonly" id="Txt_WorkUnit" style="background-color: #ffffc0" runat="server" />
<img id="Img2" style="cursor: hand" onclick="pickCorp1();" src="/images/contact.gif" align="absmiddle" runat="server" />

                            </td>
                            
                            <td class="td-label" style="width: 70px;">
                                业务经理
                            </td>
                            <td width="22%">
                            <asp:HiddenField ID="ManagerCodeYW" runat="server" />
                                <asp:TextBox ID="Txt_businessman" Width="80%" BackColor="#FFFFC0" CssClass="txtreadonly" runat="server"></asp:TextBox>
                                <img id="Img3" style="cursor: hand" onclick="pickOnePersonYW();" src="/images/contact.gif" align="absmiddle" runat="server" />

                            </td>
                           
                        </tr>
                        <tr>
                          <td class="td-label" style="width: 70px">
                                 实施人
                            </td>
                            <td width="22%">
                                <asp:HiddenField ID="Txt_LinkManSS" runat="server" />
                                <asp:TextBox ID="Txt_LinkMan" Width="80%" BackColor="#FFFFC0" CssClass="txtreadonly" runat="server"></asp:TextBox>
                                <img id="Img4" style="cursor: hand" onclick="pickOnePersonSS();" src="/images/contact.gif" align="absmiddle" runat="server" />

                            </td>
                             <td class="td-label" width="9%">
                                联系电话
                            </td>
                            <td width="26%">
                              <asp:TextBox ID="Txt_telphone" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td class="td-label" width="9%">
                                项目经理
                            </td>
                            <td width="26%">
                                <input type="text" readonly="readonly" class="txtreadonly" style="background-color: #ffffc0" id="Txt_PrjManager" runat="server" />
<img id="Img1" style="cursor: hand" onclick="pickOnePerson();" src="/images/contact.gif" align="absmiddle" runat="server" />

                            </td>
                        </tr>
                        <tr class="td-title">
                            <td width="9%" colspan="6">
                                补充内容</td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                建筑类别</td>
                            <td width="25%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_BuildingType" Width="10px" Visible="false" runat="server"></asp:TextBox>
                                    <JWControl:DropDownTree ID="drop_jzType" Width="136px" runat="server"></JWControl:DropDownTree></font></td>
                            <td class="td-label" style="width: 70px" width="70">
                                楼栋数</td>
                            <td width="22%">
                                <asp:TextBox ID="Txt_TotalHouseNum" Width="100%" runat="server"></asp:TextBox></td>
                          <td class="td-label" width="9%">
                                等级
                            </td>
                            <td width="26%">
                                <asp:TextBox ID="Txt_grade" Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                建筑面积</td>
                            <td width="25%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_BuildingArea" Width="100%" runat="server"></asp:TextBox></font></td>
                            <td class="td-label" style="width: 70px" width="70">
                                占地面积</td>
                            <td width="22%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_UsegrounArea" Width="100%" runat="server"></asp:TextBox></font></td>
                            <td class="td-label" width="9%">
                                <font face="宋体">地下建筑面积</font></td>
                            <td width="26%">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_UndergroundArea" Width="100%" runat="server"></asp:TextBox></font></td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                项目资金情况</td>
                            <td colspan="5">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_PrjFundInfo" Width="100%" TextMode="MultiLine" Rows="2" MaxLength="100" onkeyup="checklen(this,300);" runat="server"></asp:TextBox></font></td>
                        </tr>
                        <tr>
                            <td class="td-label" width="9%">
                                其它说明</td>
                            <td colspan="5">
                                <font face="宋体">
                                    <asp:TextBox ID="Txt_OtherStatement" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="200" onkeyup="checklen(this,480);" runat="server"></asp:TextBox></font></td>
                        </tr>
                        <tr>
                            <td class="td-submit" colspan="6">
                                <input id="HdnPrjCode" type="hidden" name="HdnPrjCode" runat="server" />

                                <input id="HideManagerCode" type="hidden" name="HideManagerCode" runat="server" />

                                <asp:Button ID="btnSave" Text="保 存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />&nbsp;&nbsp;<input class="button-normal" onclick="window.returnValue=false;window.close();"
                                    type="button" value="取 消">&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_PrjCode" ErrorMessage="项目编号不能为空!" Display="None" runat="server"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_PrjName" ErrorMessage="项目名称不能为空!" Display="None" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txt_PrjCost" ErrorMessage="项目造价必须为数值！" Display="None" ValidationExpression="^\-?\d+(\.\d+)?$" runat="server"></asp:RegularExpressionValidator>&nbsp;
                    <asp:ValidationSummary ID="ValidationSummary1" Width="448px" Height="8px" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" runat="server"></asp:ValidationSummary>
                </td></tr>
        </table>
    </form>
</body>
</html>
