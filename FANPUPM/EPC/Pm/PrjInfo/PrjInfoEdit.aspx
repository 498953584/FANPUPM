<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjInfoEdit.aspx.cs" Inherits="PrjInfoEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title id="PageTitle">��Ŀ��Ϣά��</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self"></base>

    <script language="javascript" type="text/javascript">
        window.name = "win";
        function pickCorp() {
            var corp = new Array();
            corp[0] = "";
            corp[1] = "";
            var url = "/CommonWindow/PickCorp.aspx";
            window.showModalDialog(url, corp, "dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
            if (corp[0] != "") {
                document.getElementById('HdnOwnerCode').value = corp[0];
                document.getElementById('txt_Owner').value = corp[1];
            }
        }
        function pickCorp1() {
            var corp = new Array();
            corp[0] = "";
            corp[1] = "";
            var url = "/CommonWindow/PickCorp.aspx";
            window.showModalDialog(url, corp, "dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
            if (corp[0] != "") {
                //document.getElementById('HdnOwnerCode').value = corp[0];
                document.getElementById('Txt_WorkUnit').value = corp[1];
            }
        }
        function CheckInputIsFloat(keyCode, e) {
            if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13) {
            }
            else if (keyCode == 110 || keyCode == 190) {
                if (e.value == "" || e.value.indexOf(".") != -1) {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function pickOnePerson() {
            var human = new Array();
            human[0] = "";
            human[1] = "";
            var url = "/CommonWindow/PickSinglePerson.aspx";
            window.showModalDialog(url, human, "dialogHeight:434px;dialogWidth:400px;center:1;help:0;status:0;");
            if (human[0] != "") {
                document.getElementById('ManagerCode').value = human[0];
                document.getElementById('Txt_PrjManager').value = human[1];
            }
        }
        function pickOnePersonSS() {
            var human = new Array();
            human[0] = "";
            human[1] = "";
            var url = "/CommonWindow/PickSinglePerson.aspx";
            window.showModalDialog(url, human, "dialogHeight:434px;dialogWidth:400px;center:1;help:0;status:0;");
            if (human[0] != "") {
                //document.getElementById('Txt_LinkManSS').value = human[0];
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
            }
        }

        //�жϿ�ʼʱ��ͽ���ʱ��--zyg
        function getBeginDate() {
            if (document.getElementById('txt_EndDate').value != "") {

                var beginTT = getTheDate(document.getElementById('txt_StartDate').value);
                var endTT = getTheDate(document.getElementById('txt_EndDate').value);
                if (beginTT == endTT) {
                    return;
                }
                if (beginTT != "") {
                    //alert('����ʱ��='+endTT);
                    //alert('��ʼʱ��='+beginTT);
                    if (endTT < beginTT) {
                        alert('����ʱ�䲻�����ڿ�ʼʱ�䣡');
                        document.getElementById('txt_StartDate').value = "";
                        document.getElementById('txt_StartDate').blur();
                    }

                }
            }

        }
        function getEndDate() {
            if (document.getElementById('txt_StartDate').value != "") {

                var beginTT = getTheDate(document.getElementById('txt_StartDate').value);
                var endTT = getTheDate(document.getElementById('txt_EndDate').value);
                if (beginTT == endTT) {
                    return;
                }
                if (beginTT != "") {
                    //alert('����ʱ��='+endTT);
                    // alert('��ʼʱ��='+beginTT);
                    if (endTT < beginTT) {
                        alert('����ʱ�䲻�����ڿ�ʼʱ�䣡');
                        document.getElementById('txt_EndDate').value = "";
                        document.getElementById('txt_EndDate').blur();
                    }

                }
            }

        }
        //��������ȡ����ʱ���ʽ
        function getTheDate(dateStr) {
            var theYear;
            var theMonth;
            var theDay;
            var i = dateStr.indexOf("-");
            var j = dateStr.lastIndexOf("-");
            theYear = parseInt(dateStr.substr(0, i));
            theMonth = parseInt(dateStr.substr(i + 1, j - i - 1)) - 1;
            theDay = parseInt(dateStr.substr(j + 1));
            return new Date(theYear, theMonth, theDay);
        }

        function check(obj)
             {

                 if (document.getElementById('chkxmgroup').checked) {

                     document.getElementById('ddlXmgroup').disabled = false;
                 }
                 else {
                     document.getElementById('ddlXmgroup').disabled = true;
                  }   
                
            }       

    </script>

</head>
<body scroll="yes" class="body_popup">
    <form id="Form1" method="post" target="win" runat="server">
    <table id="Table1" width="100%" border="0" class="table-form">
        <tr class="td-title">
            <td width="9%" colspan="6">
                ��������<asp:HiddenField ID="hdfGuid" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" class="td-label">
                ���룺
            </td>
            <td>
                <asp:TextBox ID="tbxPreCode" Enabled="false" Columns="12" BorderStyle="None" runat="server"></asp:TextBox>
                <asp:TextBox ID="TxtTypeCode" BorderStyle="None" Enabled="false" Columns="4" MaxLength="5" runat="server"></asp:TextBox>
            </td>
            <td colspan="4">
              
                <input id="chkxmgroup"   type="checkbox" name="chkxmgroup" onclick="check(chkxmgroup)" value="ʵʩ��Ŀ��">   
                ʵʩ��Ŀ��&nbsp;
                <asp:DropDownList ID="ddlXmgroup" Width="30%" Enabled="false" runat="server"></asp:DropDownList>
            </td>
            <asp:Label ID="LabcodeWR" ForeColor="Red" runat="server"></asp:Label>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="��Ŀ��Ų���Ϊ�գ�" ControlToValidate="txt_PrjCode" runat="server"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_PrjName" ErrorMessage="��Ŀ���ֲ���Ϊ�գ�" runat="server"></asp:RequiredFieldValidator>
            
        </tr>
        <tr>
            <td class="td-label" width="9%">
                ��Ŀ����
            </td>
            <td width="25%">
                <JWControl:DropDownTree ID="drop_PrjKindClass" runat="server"></JWControl:DropDownTree>
            </td>
            <td class="td-label" style="width: 70px" width="70">
                ��Ŀ���
            </td>
            <td width="22%">
                <label>
                    <asp:TextBox ID="txt_PrjCode" Width="100%" MaxLength="15" runat="server"></asp:TextBox></label>
            </td>
            <td class="td-label" width="9%">
                ��Ŀ����
            </td>
            <td width="26%">
                <asp:TextBox ID="txt_PrjName" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                ��ʼʱ��
            </td>
            <td>
                <JWControl:DateBox ID="txt_StartDate" Width="100%" runat="server"></JWControl:DateBox>
            </td>
            <td class="td-label" style="width: 70px">
                ����ʱ��
            </td>
            <td>
                <JWControl:DateBox ID="txt_EndDate" Width="100%" runat="server"></JWControl:DateBox>
            </td>
            <td class="td-label">
                ���̹���
            </td>
            <td>
                <asp:TextBox ID="txt_Duration" Width="100%" MaxLength="10" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                �����ȼ�
            </td>
            <td>
                <JWControl:DropDownTree ID="drop_QualityClass" runat="server"></JWControl:DropDownTree>
            </td>
            <td class="td-label" style="width: 70px">
                ��Ŀ���
            </td>
            <td>
                <asp:TextBox ID="txt_PrjCost" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" runat="server"></asp:TextBox>(��Ԫ)
            </td>
            <td class="td-label">
                <span>�׷�����</span>
            </td>
            <td>
                <input type="text" class="txtreadonly" id="txt_Owner" style="background-color: #ffffc0" runat="server" />
<img id="IMGOPEN" style="cursor: hand" onclick="pickCorp();" src="/images/contact.gif" align="absmiddle" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-label">
                ��Ŀ����
            </td>
            <td>
                <JWControl:DropDownTree ID="ddt_Area" runat="server"></JWControl:DropDownTree>
            </td>
            <td class="td-label">
                ��Ŀ״̬
            </td>
            <td>
                <JWControl:DropDownTree ID="ddt_state" runat="server"><Items><JWControl:DropDownTreeItem Text="�ڽ�" Value="4" runat="server"></JWControl:DropDownTreeItem><JWControl:DropDownTreeItem Text="ͣ��" Value="6" runat="server"></JWControl:DropDownTreeItem><JWControl:DropDownTreeItem Text="����" Value="7" runat="server"></JWControl:DropDownTreeItem><JWControl:DropDownTreeItem Text="����" Value="8" runat="server"></JWControl:DropDownTreeItem><JWControl:DropDownTreeItem Text="ά��" Value="9" runat="server"></JWControl:DropDownTreeItem></Items></JWControl:DropDownTree>
            </td>
            <td class="td-label" style="width: 70px">
                ��Ŀ�ص�
            </td>
            <td>
                <asp:TextBox ID="txt_PrjPlace" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                ��ƹ�˾
            </td>
            <td>
                <asp:TextBox ID="txt_Designer" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
            </td>
            <td class="td-label" style="width: 70px">
                �����˾
            </td>
            <td>
                <asp:TextBox ID="txt_Inspector" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
            </td>
            <td class="td-label">
                �б����
            </td>
            <td>
                <asp:TextBox ID="txt_Counsellor" Width="100%" MaxLength="20" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                ��Ŀ���
            </td>
            <td colspan="5">
                <asp:TextBox ID="txt_PrjInfo" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="200" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                ��ע
            </td>
            <td colspan="5">
                <font face="����">
                    <asp:TextBox ID="txt_Remark" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="200" runat="server"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                ��ظ���
            </td>
            <td colspan="5">
                <label id="lbFile" runat="server">
                </label>
                <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                <input id="hdnSeeType" type="hidden" name="hdnSeeType" runat="server" />
<input id="HdnOwnerCode" type="hidden" name="HdnOwnerCode" runat="server" />

            </td>
        </tr>
        <tr class="td-title">
            <td width="9%" colspan="6">
                ����Ҫ��
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                ���ʵȼ�
            </td>
            <td width="25%">
                <font face="����">
                    <asp:TextBox ID="Txt_Rank" Width="20px" Visible="false" runat="server"></asp:TextBox>
                    <JWControl:DropDownTree ID="drop_zzGrade" Width="136px" runat="server"></JWControl:DropDownTree>
                </font>
            </td>
            <td class="td-label" style="width: 70px" width="70">
                Ԥ�㷽ʽ
            </td>
            <td width="22%">
                <font face="����">
                    <asp:TextBox ID="Txt_BudgetWay" Width="20px" Visible="false" runat="server"></asp:TextBox>
                    <JWControl:DropDownTree ID="drop_ysType" Width="136px" runat="server"></JWControl:DropDownTree>
                </font>
            </td>
            <td class="td-label" width="9%">
                <font face="����">�а���ʽ</font>
            </td>
            <td width="26%">
                <font face="����">
                    <asp:TextBox ID="Txt_ContractWay" Width="20px" Visible="false" runat="server"></asp:TextBox>
                    <JWControl:DropDownTree ID="drop_cbType" Width="136px" runat="server"></JWControl:DropDownTree>
                </font>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                ��������
            </td>
            <td width="25%">
                <asp:TextBox ID="Txt_PayCondition" Width="10px" Visible="false" runat="server"></asp:TextBox>
                <JWControl:DropDownTree ID="drop_Payment" Width="136px" runat="server"></JWControl:DropDownTree>
            </td>
            <td class="td-label" style="width: 70px" width="70">
                �б���ʽ
            </td>
            <td width="22%">
                <asp:TextBox ID="Txt_TenderWay" Width="10px" Visible="false" runat="server"></asp:TextBox>
                <JWControl:DropDownTree ID="drop_zbType" Width="136px" runat="server"></JWControl:DropDownTree>
            </td>
            <td class="td-label" width="9%">
                ���㷽ʽ
            </td>
            <td width="26%">
                <asp:TextBox ID="Txt_PayWay" Width="10px" Visible="false" runat="server"></asp:TextBox>
                <JWControl:DropDownTree ID="drop_jsType" Width="136px" runat="server"></JWControl:DropDownTree>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                ��Ҫ��������
            </td>
            <td width="25%">
                <asp:TextBox ID="Txt_KeyPart" Width="10px" Visible="false" runat="server"></asp:TextBox>
                <JWControl:DropDownTree ID="drop_PrimaryGrade" Width="136px" runat="server"></JWControl:DropDownTree>
            </td>
            
              <td class="td-label" width="9%">
                ʵʩ��λ
            </td>
            <td width="25%">
                <input type="text" class="txtreadonly" id="Txt_WorkUnit" style="background-color: #ffffc0" runat="server" />
<img id="Img2" style="cursor: hand" onclick="pickCorp1();" src="/images/contact.gif" align="absmiddle" runat="server" />

            </td>
            
            <td class="td-label" style="width: 70px;">
                ҵ����
            </td>
            <td width="22%">
                <asp:TextBox ID="Txt_businessman" Width="80%" BackColor="#FFFFC0" CssClass="txtreadonly" runat="server"></asp:TextBox>
                <img id="Img3" style="cursor: hand" onclick="pickOnePersonYW();" src="/images/contact.gif" align="absmiddle" runat="server" />

                <asp:HiddenField ID="ManagerCodeYW" runat="server" />
            </td>
           
        </tr>
        <tr>
          <td class="td-label" style="width: 70px">
                 ʵʩ��
            </td>
            <td width="22%">
                <asp:HiddenField ID="Txt_LinkManSS" runat="server" />
                <asp:TextBox ID="Txt_LinkMan" Width="80%" BackColor="#FFFFC0" CssClass="txtreadonly" runat="server"></asp:TextBox><img id="Img4" style="cursor: hand" onclick="pickOnePersonSS();" src="/images/contact.gif" align="absmiddle" runat="server" />

            </td>
             <td class="td-label" width="9%">
                ��ϵ�绰
            </td>
            <td width="26%">
              <asp:TextBox ID="Txt_telphone" Width="100%" runat="server"></asp:TextBox>
            </td>
            <td class="td-label" width="9%">
                ��Ŀ����
            </td>
            <td width="26%">
                <input type="text" readonly="readonly" class="txtreadonly" style="background-color: #ffffc0" id="Txt_PrjManager" runat="server" />
<img id="Img1" style="cursor: hand" onclick="pickOnePerson();" src="/images/contact.gif" align="absmiddle" runat="server" />

            </td>
        </tr>
        <tr class="td-title">
            <td width="9%" colspan="6">
                ��������
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                �������
            </td>
            <td width="25%">
                <font face="����">
                    <asp:TextBox ID="Txt_BuildingType" Width="10px" Visible="false" runat="server"></asp:TextBox>
                    <JWControl:DropDownTree ID="drop_jzType" Width="136px" runat="server"></JWControl:DropDownTree>
                </font>
            </td>
            <td class="td-label" style="width: 70px" width="70">
                ����/����
            </td>
            <td width="22%">
                <asp:TextBox ID="Txt_TotalHouseNum" Width="100%" runat="server"></asp:TextBox>
            </td>
            <td class="td-label" width="9%">
                �ȼ�
            </td>
            <td width="26%">
                <asp:TextBox ID="Txt_grade" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                �������
            </td>
            <td width="25%">
                <font face="����">
                    <asp:TextBox ID="Txt_BuildingArea" Width="100%" runat="server"></asp:TextBox></font>
            </td>
            <td class="td-label" style="width: 70px" width="70">
                ռ�����
            </td>
            <td width="22%">
                <font face="����">
                    <asp:TextBox ID="Txt_UsegrounArea" Width="100%" runat="server"></asp:TextBox></font>
            </td>
            <td class="td-label" width="9%">
                <font face="����">�������</font>
            </td>
            <td width="26%">
                <font face="����">
                    <asp:TextBox ID="Txt_UndergroundArea" Width="100%" runat="server"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                �ʽ����
            </td>
            <td colspan="5">
                <font face="����">
                    <asp:TextBox ID="Txt_PrjFundInfo" Width="100%" TextMode="MultiLine" Rows="2" MaxLength="100" runat="server"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="9%">
                ����˵��
            </td>
            <td colspan="5">
                <font face="����">
                    <asp:TextBox ID="Txt_OtherStatement" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="200" runat="server"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="td-submit">
                <input id="ManagerCode" type="hidden" name="ManagerCode" runat="server" />

                <asp:Button ID="btnSave" Text="��  ��" OnClick="btnSave_Click" runat="server" />
                <input type="button" value="��  ��" onclick="window.close();">
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" EnableClientScript="true" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
    </form>
</body>
</html>
