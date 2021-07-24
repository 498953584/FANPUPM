<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutWPlan.aspx.cs" Inherits="oa_WorkPlanAndSummary_AboutWPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../../StockManage/skins/sm1.css" />

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="My97DatePicker/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <style type="text/css">
        .divFooter2
        {
            height: 24px;
            text-align: right;
            background: url(/images1/divBottomBack.jpg) repeat-x;
            vertical-align: middle;
            position: absolute;
            bottom: 0px;
            width: 100%;
        }
        .tableFooter2
        {
            width: 100%;
            text-align: right;
        }
        .txt
        {
            width: 40%;
            text-align: left;
        }
        .word
        {
            width: 10%;
            text-align: right;
        }
        .spanSelect1
        {
            height: 18px;
            border: 0px solid #B5CCDE;
            text-align: left;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            var counts = document.getElementById('hdnDataCount').value;
            var allVal = document.getElementById('hdnvalue').value;
            var infoArr = new Array();
            if (allVal != "") {
                infoArr = allVal.split('?');
                if (counts > 1) {
                    for (var i = 0; i < counts - 1; i++) {
                        var mT = document.createElement('table');
                        mT.id = "tb" + i;
                        document.getElementById('hdnNums').value += mT.id + "?";
                        mT.setAttribute('cellspadding', '5');
                        mT.setAttribute('cellspacing', '0');
                        mT.setAttribute('vertical-align', 'middle');
                        mT.width = '80%';
                        var mTr1 = mT.insertRow();
                        var leng = mT.length;

                        var dateInput = document.createElement('input');
                        dateInput.setAttribute('type', 'text');
                        dateInput.name = "beginDate" + i;
                        dateInput.setAttribute("onclick", "WdatePicker()");
                        dateInput.onfocus = getfocus;
                        dateInput.style.width = "55%";
                        dateInput.style.height = "15px";

                        var dateEndInput = document.createElement('input');
                        dateEndInput.setAttribute('type', 'text');
                        dateEndInput.name = "endedDate" + i;
                        dateEndInput.setAttribute("onclick", "WdatePicker()");
                        dateEndInput.onfocus = getfocus;
                        dateEndInput.style.width = "55%";
                        dateEndInput.style.height = "15px";

                        var txtRespon = document.createElement('input'); //负责人
                        //txtRespon.value = "<%# UserCode %>";
                        txtRespon.setAttribute("type", "text");
                        txtRespon.id = "txtCheify" + i;
                        txtRespon.name = "txtCheify" + i;
                        txtRespon.onfocus = getfocus;
                        txtRespon.style.width = "55%";
                        txtRespon.style.height = "15px";
                        //txtRespon.setAttribute("ondblclick", "openPersonPicker('" + txtRespon.id + "')");
                        var hdnCheify = document.createElement('input'); //创建隐藏文本框,保存人员编号
                        hdnCheify.id = "hdnCheify" + i;
                        hdnCheify.setAttribute("type", "hidden");

                        var txtDuty = document.createElement('input'); //责任人
                        txtDuty.setAttribute("type", "text");
                        txtDuty.id = "txtPerson" + i;
                        txtDuty.name = "txtPerson" + i;
                        txtDuty.onfocus = getfocus;
                        //txtDuty.setAttribute("ondblclick", "openPersonPicker('" + txtDuty.id + "')");
                        txtDuty.style.width = "55%";
                        txtDuty.style.height = "15px";
                        var hdnPerson = document.createElement('input'); //创建隐藏文本框,保存人员编号
                        hdnPerson.id = "hdnPerson" + i;
                        hdnPerson.setAttribute("type", "hidden");

                        var mTd11 = mTr1.insertCell();
                        mTd11.width = "25%";
                        mTd11.align = "right"
                        mTd11.style.paddingTop = "10px";
                        mTd11.style.paddingBottom = "6px";
                        var mTd12 = mTr1.insertCell();
                        mTd12.width = "25%";
                        mTd12.align = "right";
                        var mTd13 = mTr1.insertCell();
                        mTd13.width = "25%";
                        mTd13.align = "right";
                        var mTd14 = mTr1.insertCell();
                        mTd14.width = "25%";
                        mTd14.align = "right";
                        mTd11.innerHTML = "开始时间 ";
                        mTd11.appendChild(dateInput);
                        mTd12.innerHTML = "结束时间";
                        mTd12.appendChild(dateEndInput);
                        mTd13.innerHTML = "负 责 人 ";
                        mTd13.appendChild(txtRespon);
                        mTd13.appendChild(hdnCheify);
                        mTd14.innerHTML = "责 任 人";
                        mTd14.appendChild(txtDuty);
                        mTd14.appendChild(hdnPerson);




                        var mTr3 = mT.insertRow();
                        var leng = mT.length;
                        var txtContent = document.createElement('textarea');
                        txtContent.setAttribute('cols', '28');
                        txtContent.setAttribute('rows', '2');
                        txtContent.onfocus = getfocus;
                        txtContent.name = "txtConten" + i;
                        txtContent.style.width = "89%";
                        txtContent.style.height = "30px";
                        var txtNorm = document.createElement('input');
                        txtNorm.setAttribute('type', 'hidden');
                        txtNorm.id = "txtNormer" + i;
                        var mTd31 = mTr3.insertCell();
                        mTd31.align = "right";
                        mTd31.setAttribute("colspan", "4");
                        mTd31.style.verticalAlign = "middle";
                        mTd31.style.paddingTop = "6px";
                        mTd31.style.paddingBottom = "6px";
                        mTd31.innerHTML = "计划内容 ";
                        mTd31.appendChild(txtContent);
                        mTd31.appendChild(txtNorm);

                        document.getElementById('mTable').appendChild(mT);

                        var newarr = new Array();
                        var newarr = infoArr[i].split('|');
                        var tbs = document.getElementById('hdnNums').value;
                        var tbIds = new Array();
                        tbIds = tbs.split('?');
                        var idstr = tbIds[i].substring(2, tbIds.length - 1);
                        txtNorm.value = newarr[0];
                        txtContent.value = newarr[2];
                        txtDuty.value = newarr[6];
                        txtRespon.value = newarr[7];
                        dateInput.value = newarr[3].substring(0, newarr[3].indexOf(" "));
                        dateEndInput.value = newarr[4].substring(0, newarr[4].indexOf(" "));
                    }
                }
            }
        });
        function openPartPicker() {

            document.getElementById('frameShow').title = "请选择部门";
            document.getElementById('frameShow').style.display.bold;
            document.getElementById('sFrm').src = "Part.aspx?FromUrl=" + document.URL;
            $('#frameShow').dialog({ width: 600, height: 485, modal: true })
        }
        function openPersonPicker(InName) {
            document.getElementById("divFram").title = "选择人员";
            document.getElementById("divFram").style.display.bold;
            document.getElementById("ifFram").src = "../../Common/DivSelectUser.aspx?Method=returnUser&InputName=" + InName;
            $('#divFram').dialog({ width: 600, height: 485, modal: true })
        }
        function showTip(type) {
            if (type == 1) {
                $('#testDiv1').dialog({ width: 100, height: 18, model: false });
            }
            if (type == 2) {
                $('#testDiv2').dialog({ width: 100, height: 18, model: false });
            }
        }
        function returnUser(id, name, InName, DeptId, DeptName) {
            if (InName == "txtReportUser") {
                document.getElementById(InName).value = name;
                document.getElementById('hdnReportUser').value = id;
                document.getElementById('thisDeptId').value = DeptId;
                document.getElementById('txtPart').value = DeptName;

            }
        }
        function returnUserArr(id, name) {
            document.getElementById('hdnCheckUser').value = id;
            document.getElementById('txtCheckUser').value = name;
            //alert(id + "\n" + name);
        }
        function addTable() {
            var dateMs = new Date();
            var i = dateMs.getMilliseconds();
            var mT = document.createElement('table');
            mT.id = "tb" + i;
            document.getElementById('hdnNums').value += mT.id + "?";
            mT.setAttribute('cellspadding', '5');
            mT.setAttribute('cellspacing', '0');
            mT.setAttribute('vertical-align', 'middle');
            mT.width = '80%';
            var mTr1 = mT.insertRow();
            var leng = mT.length;

            var dateInput = document.createElement('input');
            dateInput.setAttribute('type', 'text');
            dateInput.name = "beginDate" + i;
            dateInput.setAttribute("onclick", "WdatePicker()");
            dateInput.onfocus = getfocus;
            dateInput.style.width = "55%";
            dateInput.style.height = "15px";

            var dateEndInput = document.createElement('input');
            dateEndInput.setAttribute('type', 'text');
            dateEndInput.name = "endedDate" + i;
            dateEndInput.setAttribute("onclick", "WdatePicker()");
            dateEndInput.onfocus = getfocus;
            dateEndInput.style.width = "55%";
            dateEndInput.style.height = "15px";

            var txtRespon = document.createElement('input');
            txtRespon.setAttribute("type", "text");
            txtRespon.id = "txtCheify" + i;
            txtRespon.name = "txtCheify" + i;
            txtRespon.onfocus = getfocus;
            txtRespon.style.width = "55%";
            txtRespon.style.height = "15px";
            txtRespon.value = "<%=GetCurrentName() %>";
            //txtRespon.setAttribute("ondblclick", "openPersonPicker('" + txtRespon.id + "')");
            var hdnCheify = document.createElement('input'); //创建隐藏文本框,保存人员编号
            hdnCheify.id = "hdnCheify" + i;
            hdnCheify.setAttribute("type", "hidden");

            var txtDuty = document.createElement('input');
            txtDuty.setAttribute("type", "text");
            txtDuty.id = "txtPerson" + i;
            txtDuty.name = "txtPerson" + i;
            txtDuty.onfocus = getfocus;
            //txtDuty.setAttribute("ondblclick,openPersonPicker('"+txtDuty.id+"')");
            txtDuty.style.width = "55%";
            txtDuty.style.height = "15px";
            var hdnPerson = document.createElement('input'); //创建隐藏文本框,保存人员编号
            hdnPerson.id = "hdnPerson" + i;
            hdnPerson.setAttribute("type", "hidden");

            var mTd11 = mTr1.insertCell();
            mTd11.width = "25%";
            mTd11.align = "right"
            mTd11.style.paddingTop = "10px";
            mTd11.style.paddingBottom = "6px";
            var mTd12 = mTr1.insertCell();
            mTd12.width = "25%";
            mTd12.align = "right";
            var mTd13 = mTr1.insertCell();
            mTd13.width = "25%";
            mTd13.align = "right";
            var mTd14 = mTr1.insertCell();
            mTd14.width = "25%";
            mTd14.align = "right";
            mTd11.innerHTML = "开始时间 ";
            mTd11.appendChild(dateInput);
            mTd12.innerHTML = "结束时间";
            mTd12.appendChild(dateEndInput);
            mTd13.innerHTML = "负 责 人 ";
            mTd13.appendChild(txtRespon);
            mTd13.appendChild(hdnCheify);
            mTd14.innerHTML = "责 任 人";
            mTd14.appendChild(txtDuty);
            mTd14.appendChild(hdnPerson);

            var mTr3 = mT.insertRow();
            var leng = mT.length;
            var txtContent = document.createElement('textarea');
            txtContent.setAttribute('cols', '28');
            txtContent.setAttribute('rows', '2');
            txtContent.onfocus = getfocus;
            txtContent.name = "txtConten" + i;
            txtContent.style.width = "89%";
            txtContent.style.height = "30px";
            var mTd31 = mTr3.insertCell();
            mTd31.align = "right";
            mTd31.setAttribute("colspan", "4");
            mTd31.style.verticalAlign = "middle";
            mTd31.style.paddingTop = "6px";
            mTd31.style.paddingBottom = "6px";
            mTd31.innerHTML = "计划内容 ";
            mTd31.appendChild(txtContent);

            document.getElementById('mTable').appendChild(mT);
        }
        function getfocus() {
            document.getElementById('hdnid').value = this.name;
        }
        function delTable() {
            var noid = document.getElementById('hdnid').value;
            if (noid != "") {
                var tid = noid.substring(9, noid.length + 1);
                var tbid = "tb" + tid;
                //alert(tbid)
                var dataCount = document.getElementById('hdnDataCount').value;
                var tagTb = document.getElementById('hdnNums').value;
                var Tbnums = new Array();
                Tbnums = tagTb.split('?');
                var hdnPlanid = "txtNormer" + tid;
                var tLen = Tbnums.length;
                for (var i = 0; i < tLen - 1; i++) {
                    if (Tbnums[i] == tbid && i < dataCount - 1) {
                        if (confirm("该数据可能已存在于数据库中,确定删除?")) {
                            document.getElementById('hdnDelPlanId').value += document.getElementById(hdnPlanid).value + "?";
                            document.getElementById('hdnDelTableId').value += tbid + "?,";
                        } else {
                            return;
                        }
                    }

                }
                var delTb = document.getElementById(tbid);
                delTb.parentNode.removeChild(delTb);
                document.getElementById('hdnid').value = ""; //清空已删除表的id
                document.getElementById('hdnNums').value = document.getElementById('hdnNums').value.replace(tbid + "?", "")//时刻保证保存动态table ID的隐藏控件的值与实际table数量一致.

            } else {
                alert('你并没有选中任何新增的计划!');
            }
        }
        function CheckEmpty() {
            var list = document.getElementsByTagName("input");
            for (var i = 0; i < list.length && list[i]; i++) {
                if (list[i].type == "text") {
                    if (list[i].value == "") {
                        top.ui.alert("请不要留空!")
                        return false;
                    }
                }
            }
        }
        function saveDate() {
            var val = document.getElementById('DateInTime').value;
            document.getElementById('hdnReportDate').value = val;
        }
        function winclose(tobj, url, rebool) {
            if (rebool) {
                parent.desktop[tobj].location = url + "?planType=" + getRequestParam("planType");
            };
            parent.desktop[tobj] = null;

            top.frameWorkArea.window.desktop.getActive().close();

        }
    </script>
    <form id="form1" runat="server">
    <div style="height: 95%; overflow: auto; width: 100%">
        <div style="margin-top: 5px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="divHeader">
                        工作计划
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="margin-top: 5px;">
            <table class="tableContent2" cellpadding="5px" cellspacing="0" width="80%">
                <tr>
                    <td class="word">
                        计划编号
                    </td>
                    <td class="txt" id="ttt">
                        <asp:TextBox ID="txtWPcode" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        开始日期
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="txtWPlanDate" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        填报人员
                    </td>
                    <td class="txt" id="Td2">
                        <span class="spanSelect" style="width: 100%">
                            <asp:TextBox ID="txtReportUser" Width="90%" ReadOnly="true" Style="height: 15px;
                                border: none; line-height: 16px; margin: 1px 2px" ToolTip="请单击图标选择" runat="server"></asp:TextBox>
                            <img src="/images1/iconSelect.gif" alt="选择填报人" id="img1" visible="false" onclick="openPersonPicker('txtReportUser')" runat="server" />

                        </span>
                    </td>
                    <td class="word">
                        部门名称
                    </td>
                    <td class="txt" id="Td3">
                        <span class="spanSelect" style="width: 100%">
                            <asp:TextBox ID="txtPart" Width="90%" ReadOnly="true" Style="height: 15px; border: none;
                                line-height: 16px; margin: 1px 2px" name="txtPart" runat="server"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        计划说明
                    </td>
                    <td class="txt" colspan="3">
                        <asp:TextBox ID="txtProduce" Height="30px" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <img src="images/IconTexto_WebDev_059.png" title="新增下一条工作计划" alt="新增下一条工作计划" id="imgadd"
                                        onclick="addTable()" style="width: 25px; cursor: hand;" />
                                </td>
                                <td align="left">
                                    <img src="images/IconTexto_WebDev_052.png" title="删除选中的工作计划" alt="删除选中的工作计划" id="imgdel"
                                        onclick="delTable()" style="width: 25px; cursor: hand;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" id="mTable" style="margin-top: 5px; vertical-align: top;">
            <div style="vertical-align: top;">
                <table class="tableContent2" cellpadding="5" cellspacing="0" style="vertical-align: middle;
                    width: 80%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%; text-align: right;">
                                        开始时间
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <asp:TextBox ID="BeginDate" Height="100%" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: right;">
                                        结束时间
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <asp:TextBox ID="EndDate" Height="100%" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: right;">
                                        负&nbsp;责&nbsp;人
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <span class="spanSelect" style="width: 100%">
                                            <asp:TextBox ID="txtDuty" Width="90%" ReadOnly="false" Style="height: 15px; border: none;
                                                line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                        </span>
                                    </td>
                                    <td style="width: 10%; text-align: right;">
                                        责&nbsp;任&nbsp;人
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <span class="spanSelect" style="width: 100%">
                                            <asp:TextBox ID="txtResponsibility" Width="90%" ReadOnly="false" Style="height: 15px;
                                                border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%; text-align: right">
                                        计划内容
                                    </td>
                                    <td style="width: 90%; text-align: left;">
                                        <asp:TextBox ID="txtContent" TextMode="MultiLine" Height="30px" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="frameShow" title="" style="display: none">
        <iframe id="sFrm" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <input type="hidden" id="hdnid" /><!--存储焦点控件的ID-->
    <input type="hidden" id="hdnNums" runat="server" />
<!--存储动态增加的所有表的ID-->
    <input type="hidden" id="hdnCheckUser" name="hdnCheckUserId" runat="server" />
<!--存储人员编码-->
    <input type="hidden" id="hdnDataCount" runat="server" />
<!--存储数据库中数据的条数-->
    <input type="hidden" id="hdnDelPlanId" runat="server" />
<!--主要用于编辑-->
    <input type="hidden" id="hdnReportUser" runat="server" />

    <input type="hidden" id="thisDeptId" runat="server" />

    <input type="hidden" id="thisDeptName" runat="server" />

    <input type="hidden" id="hdnDelTableId" runat="server" />

    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input ID="btnSave" Name="submit" type="submit" Value="保存" OnServerClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" />
                </td>
            </tr>
        </table>
    </div>
    <input id="hdnvalue" type="hidden" runat="server" />
<!--存储PlanDetails数据库中相应计划单号下所有数据-->
    </form>
</body>
</html>
