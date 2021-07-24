<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummPlan.aspx.cs" Inherits="oa_WorkPlanAndSummary_SummPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="My97DatePicker/My97DatePicker/WdatePicker.js"></script>
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
    </style>
    <script type="text/javascript">
        window.onload = function () {
            //alert(document.getElementById('hdnDataCount').value);
            var counts = document.getElementById('hdnDataCount').value;
            var allVal = document.getElementById('hdnvalue').value;
            var infoArr = new Array();
            if (allVal != "") {
                infoArr = allVal.split('?');
                //alert(infoArr[1].split(',')[2]);
                if (counts > 0) {
                    for (var i = 0; i < counts; i++) {
                        //addTable();
                        //var dateMs = new Date();
                        //var l = dateMs.getMilliseconds();
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
                        dateInput.setAttribute("readonly", "readonly");
                        dateInput.name = "beginDate" + i;
                        //dateInput.setAttribute("onclick", "WdatePicker()");
                        dateInput.onfocus = getfocus;
                        dateInput.style.width = "55%";
                        dateInput.style.height = "15px";

                        var dateEndInput = document.createElement('input');
                        dateEndInput.setAttribute('type', 'text');
                        dateEndInput.setAttribute("readonly", "readonly");
                        dateEndInput.name = "endedDate" + i;
                        //dateEndInput.setAttribute("onclick", "WdatePicker()");
                        dateEndInput.onfocus = getfocus;
                        dateEndInput.style.width = "55%";
                        dateEndInput.style.height = "15px";

                        var txtRespon = document.createElement('input');
                        //txtRespon.value = "<%# UserCode %>";
                        txtRespon.setAttribute("type", "text");
                        txtRespon.setAttribute("readonly", "readonly");
                        txtRespon.id = "txtCheify" + i;
                        txtRespon.name = "txtCheify" + i;
                        txtRespon.onfocus = getfocus;
                        txtRespon.style.width = "55%";
                        txtRespon.style.height = "15px";
                        //txtRespon.setAttribute("ondblclick", "openPersonPicker('" + txtRespon.id + "')");
                        var hdnCheify = document.createElement('input'); //创建隐藏文本框,保存人员编号
                        hdnCheify.id = "hdnCheify" + i;
                        hdnCheify.setAttribute("type", "hidden");

                        var txtDuty = document.createElement('input');
                        txtDuty.setAttribute("type", "text");
                        txtDuty.setAttribute("readonly", "readonly");
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
                        txtContent.setAttribute("readonly", "readonly");
                        //txtContent.setAttribute('type', 'text');
                        txtContent.onfocus = getfocus;
                        txtContent.name = "txtConten" + i;
                        txtContent.style.width = "89%";
                        txtContent.style.height = "30px";
                        var txtNorm = document.createElement('input');
                        //txtnorm.setattribute("cols", "28");
                        //txtnorm.setattribute('rows', '3');
                        txtNorm.setAttribute('type', 'hidden');
                        //txtNorm.onfocus = getfocus;
                        txtNorm.id = "txtNormer" + i;
                        txtNorm.name = "txtNormer" + i;
                        //txtNorm.style.width = "78%";
                        //txtNorm.style.height="45px";
                        var mTd31 = mTr3.insertCell();
                        mTd31.align = "right";
                        mTd31.setAttribute("colspan", "4");
                        mTd31.style.verticalAlign = "middle";
                        mTd31.style.paddingTop = "6px";
                        mTd31.style.paddingBottom = "6px";
                        //var mTd32 = mTr3.insertCell();
                        //mTd32.align = "right";
                        mTd31.innerHTML = "计划内容 ";
                        mTd31.appendChild(txtContent);
                        //mTd32.innerHTML = "完成标准 ";
                        mTd31.appendChild(txtNorm);

                        var mTr5 = mT.insertRow();
                        var txtPecent = document.createElement('input');
                        txtPecent.onfocus = getfocus;
                        txtPecent.type = "text";
                        //if (getRequestParam("Action") == "edit") {
                        //txtPecent.value=
                        //}
                        txtPecent.name = "txtpecent" + i;
                        txtPecent.style.width = "89%";
                        txtPecent.style.height = "15px";
                        txtPecent.onblur = loseFocus;
                        //txtPecent.style.border = "1px solid #808000";
                        var mTd51 = mTr5.insertCell();
                        mTd51.align = "right";
                        mTd51.setAttribute("colspan", "4");
                        mTd51.style.verticalAlign = "middle";
                        mTd51.style.paddingTop = "6px";
                        mTd51.style.paddingBottom = "6px";
                        mTd51.innerHTML = "完成百分比";
                        mTd51.appendChild(txtPecent);

                        var mTr4 = mT.insertRow();
                        var txtSummary = document.createElement('textarea');
                        txtSummary.setAttribute('cols', '28');
                        txtSummary.setAttribute('rows', '2');
                        //txtContent.setAttribute('type', 'text');
                        txtSummary.onfocus = getfocus;
                        txtSummary.name = "txtsummer" + i;
                        txtSummary.style.width = "89%";
                        txtSummary.style.height = "30px";
                        //txtSummary.style.border = "1px solid #808000";
                        var hdnDate = document.createElement('input');
                        hdnDate.name = "hdnDateSm" + i;
                        hdnDate.type = "hidden";
                        var dt = new Date();
                        hdnDate.value = dt.getFullYear().toString() + "-" + dt.getMonth().toString() + "-" + dt.getDate().toString();
                        var mTd41 = mTr4.insertCell();
                        mTd41.align = "right";
                        mTd41.setAttribute("colspan", "4");
                        mTd41.style.verticalAlign = "middle";
                        mTd41.style.paddingTop = "6px";
                        mTd41.style.paddingBottom = "6px";
                        mTd41.innerHTML = "计划总结";
                        mTd41.appendChild(txtSummary);

                        document.getElementById('mTable').appendChild(mT);

                        var newarr = new Array();
                        //alert(infoArr[i].split(','));
                        var newarr = infoArr[i].split('|');
                        //var tbs = document.getElementById('hdnNums').value;
                        //var tbIds = new Array();
                        //tbIds = tbs.split('?');
                        //var idstr = tbIds[i].substring(2, tbIds.length - 1);
                        txtNorm.value = newarr[0];
                        //alert(txtNorm.value);
                        txtContent.value = newarr[2];
                        txtDuty.value = newarr[6];
                        txtRespon.value = newarr[7];
                        dateInput.value = newarr[3].substring(0, newarr[3].indexOf(" "));
                        dateEndInput.value = newarr[4].substring(0, newarr[4].indexOf(" "));
                        if (getRequestParam("Action") == "edit") {
                            var hdntext = document.getElementById('hdnTextSum').value;
                            var summArr = hdntext.split('?');
                            for (var x = 0; x < summArr.length - 1; x++) {
                                var sumdetailArr = summArr[x].split('|');
                                if (txtNorm.value == sumdetailArr[2]) {
                                    txtPecent.value = sumdetailArr[1];
                                    txtSummary.value = sumdetailArr[0];
                                }
                            }
                        }

                        //alert(txtNorm.value);
                    }
                    //alert(txtNorm.value);
                }
            }

        }
        function getfocus() {
            document.getElementById('hdnid').value = this.name;
            //alert(document.getElementById('hdnNums').value)//document.activeElement.id;
        }
        function loseFocus() {
            if (this.value.indexOf('%') < 0) {
                if (this.value > 0 && this.value < 100) {
                    var txt = this.value + '%';
                    this.value = txt;
                }
                else if (this.value != "") {
                    alert("数据非法!请输入百分比例");
                    this.value = "";
                }
            }
            else {
                var num = this.value.substring(0, this.value.indexOf('%'));
                if (num < 0 || num > 100) {
                    alert("数据不合法")
                    this.value = "";
                }
            }
        }
        function winclose(tobj, url, rebool) {
            if (rebool) {
                //alert(getRequestParam("planType"));
                parent.desktop[tobj].location = url + "?planType=" + getRequestParam("planType");
            };
            parent.desktop[tobj] = null;

            top.frameWorkArea.window.desktop.getActive().close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 95%; overflow: auto; width: 100%">
        <div style="margin-top: 5px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="divHeader">
                        工作计划总结
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
                        <asp:TextBox ID="txtWPcode" Height="15px" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        填报日期
                    </td>
                    <td class="txt" id="Td1">
                        
                        <asp:TextBox ID="DateInTime" Height="15px" ToolTip="点击选择时间" ReadOnly="true" runat="server"></asp:TextBox>
                        <input id="hdnReportDate" type="hidden" runat="server" />

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
                        </span>
                    </td>
                    <td class="word">
                        部门名称
                    </td>
                    <td class="txt" id="Td3">
                        <span class="spanSelect" style="width: 100%">
                            <asp:TextBox ID="txtPart" Width="90%" ReadOnly="true" Style="height: 15px; border: none;
                                line-height: 16px; margin: 1px 2px" name="txtPart" runat="server"></asp:TextBox><input id="hdnDeptID" type="hidden" runat="server" />

                        </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="word">
                        计划说明
                    </td>
                    <td class="txt" colspan="3">
                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Height="30px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 5px; vertical-align: top;" align="center">
            <div id="mTable">
            </div>
            <table class="tableContent2" cellpadding="5px" cellspacing="0" width="80%">
                <tr>
                    <td style="width: 10%; text-align: right">
                        自我打分
                    </td>
                    <td style="width: 90%; text-align: left">
                        <asp:TextBox ID="txtScore" Height="15px" Width="100%" CssClass="easyui-numberbox" data-options="min:0,max:100,precision:2,groupSeparator:','" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        总结说明
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="txtSummPro" Height="30px" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
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
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="frameShow" title="" style="display: none">
        <iframe id="sFrm" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <input id="hdnDataCount" type="hidden" runat="server" />

    <input id="hdnvalue" type="hidden" runat="server" />

    <input id="hdnid" type="hidden" runat="server" />

    <input id="hdnNums" type="hidden" runat="server" />

    <input id="hdnTextSum" type="hidden" runat="server" />

    </form>
</body>
</html>
