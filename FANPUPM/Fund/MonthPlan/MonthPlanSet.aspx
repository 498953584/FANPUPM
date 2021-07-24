<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthPlanSet.aspx.cs" Inherits="Fund_MonthPlanSet" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_querydatectrl_ascx" Src="~/UserControl/QueryDateCtrl.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金计划编制</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script src="../../Web_Client/TreeNew.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _mpidq = "";
        var tf = true;
        $(function () {
            var contractTable = new JustWinTable('gvwWebLineList');
            replaceEmptyTable('emptyContractType', 'gvwWebLineList');
            JugdeFlowState();
            $("input[type='checkbox']").click(function () {
                document.getElementById('btnDel').disabled = false;
            });
            var _mpid = $("#mpid").val();
            _mpidq = _mpid;

            $('#btndatabind').hide();

            Load_MonthPlan();

            var hdf = $("#HiddenField1").val();
            if (hdf == "1") {
                $("#gvwWebLineList__ctl2_FileUpload2_hfldFolder").val($("#FileUpload1_hfldFolder").val());
            }
            Val.validate('form1', 'btnSave');
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - $('#trBudgetButtons').height() - 60);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);

        }
        function JugdeFlowState() {
            var _FlowState = $("#hdfFlowState").val();
            if (_FlowState != null && _FlowState.toString() != "") {
                var flowSate = parseInt(_FlowState);
                if (flowSate > -1 || flowSate == -2) {
                    $(".divHeader").hide();
                    $('#divBudget').height($(this).height() - 200);
                    $("form input").attr("disabled", "disabled");
                    $("#gvwWebLineList tr").removeAttr("onclick");
                    $("img").removeAttr("onclick");
                    $("#txtRemark").attr("disabled", "disabled");
                    $("#btnClose").removeAttr("disabled");
                }
            }
        }
        function addList() {
            var plantype = document.getElementById('hfplantype').value;
            var prjId = jw.getRequestParam('prjId');
            var _mpid = $("#mpid").val();
            if (_mpid != "") {
                var url = "/Fund/MonthPlan/MonthPlanEdit.aspx?mpid=" + _mpid + "&prjcode=" + prjId;
                url = url + '&plantype=' + plantype;
                var titleStr = "月支出计划新增";
                parent.desktop.flowclass = window;
                toolbox_oncommand(url, titleStr);
            }
        }


        function ClickRow(model) {
            document.getElementById('hdfNotModel').value = "'" + model + "'";
            document.getElementById('btnDel').disabled = false;
            if (tf) {
                document.getElementById('btnSave').disabled = false;
            }
            $("#btnEdit").removeAttr("disabled", "");
        }
        function btnEdit_onclick() {
            var uid = document.getElementById('hdfNotModel').value;
            var plantype = document.getElementById('hfplantype').value;
            var prjId = jw.getRequestParam('prjId');
            var _mpid = $("#mpid").val();
            if (_mpid != "") {
                var url = "/Fund/MonthPlan/MonthPlanEdit.aspx?UID=" + uid + "&mpid=" + _mpid + "&prjcode=" + prjId;
                url = url + '&plantype=' + plantype;
                var titleStr = "月支出计划编辑";
                parent.desktop.flowclass = window;
                toolbox_oncommand(url, titleStr);
            }
        }


        function f(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }


        // 脚本验证        
        function CheckInputIsFloat(keyCode, e) {
            if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13) {
            }
            else if (keyCode == 110 || keyCode == 190) {
                if (e.value == "" || e.value.indexOf(".") != -1) {
                    return false;
                }
            } else if (keyCode == 11 || keyCode == 67 || keyCode == 86) {
            }
            else {
                return false;
            }
        }

        function checkNumber(str) {
            var reg = /^\d+(\.\d+)?$/;
            if (!reg.exec(str)) return false
            return true
        }
        var TINPUT;

        // 选择合同
        function selectContr(tem_input) {
            var tr = jw.getParentUntil(tem_input, 'tr');

            var addoper = 'addoper';
            TINPUT = tem_input;
            var title = null;
            var url = null;

            var _mpid = $("#mpid").val();
            if (_mpid != "") {
                var plantype = jw.getRequestParam('plantype');
                var prjId = jw.getRequestParam('prjId');
                if (plantype == "payout") {
                    top.ui.callback = function (theCode, theName, bName) {
                        returnAccon(theCode, theName);
                        $(tr).find('.b-name').html(bName)
                    }
                    title = '选择支出合同';
                    url = '/Fund/MonthPlan/DivSelectContrtAccon.aspx?Method=returnAccon&mpid=' + _mpid + '&typevalue=' + addoper + '&prjcode=' + prjId;
                } else {
                    top.ui.callback = function (theCode, theName, bName) {
                        returnContr(theCode, theName);
                        $(tr).find('.b-name').html(bName)
                    }
                    title = '选择收入合同';
                    url = '/Fund/MonthPlan/DivSelectIncometCont.aspx?Method=returnContr&mpid=' + _mpid + '&typevalue=' + addoper + '&prjcode=' + prjId;
                }
            }

            top.ui._SelectContrt = window;
            top.ui.openWin({ title: title, url: url, width: 800, height: 420 });
        }
        function returnAccon(id, name) {
            var _mpid = $("#mpid").val();
            if (TINPUT != null) {
                $(TINPUT).prev().val(name);
                $(TINPUT).parent().prev().val(id);
            }
        }
        function returnContr(id, name) {
            if (TINPUT != null) {
                $(TINPUT).prev().val(name);
                $(TINPUT).parent().prev().val(id);
            }
        }
        function Load_MonthPlan() {
            $("#gvwWebLineList tr").each(function (i) {
                var c = i + 1;
                var _thisID = $(this).attr("id");
                if (_thisID != "undefined" && _thisID != "" && _thisID != null) {
                }
            });
        }
    </script>
</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="divHeader2" style="display: none">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Text="资金计划编制" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: center; position: relative;">
        <table class="" width="100%" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    计划月份
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtPlanMonth" Enabled="false" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目
                </td>
                <td class="txt">
                    <input id="txtProName" type="text" disabled="true" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    填 报 人
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtadduser" Height="15px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    填报日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtadddate" Height="15px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="40px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <div class="divHeader" style="text-align: left; padding-top: 2px; margin-top: 2px;
        width: 100%; height: 5%; position: relative;">
        <asp:Button ID="btnAdd" Text="新增" OnClick="btnAdd_Click" runat="server" />
        <asp:Button ID="btnEdit" Text="编辑" Visible="false" OnClick="btnEdit_Click" runat="server" />
        <asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
        <asp:HiddenField ID="hdfModel" runat="server" />
        <asp:HiddenField ID="hdfNotModel" runat="server" />
        <asp:HiddenField ID="hdfHD" runat="server" />
        <asp:HiddenField ID="hfplantype" runat="server" />
    </div>
    <div id="divBudget" style="width: 104%; height: 68%; overflow: auto;">
        <asp:GridView ID="gvwWebLineList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
                <table id="emptyContractType">
                    <tr class="header">
                        <th scope="col">
                        </th>
                        <th scope="col">
                            序号
                        </th>
                        <th scope="col">
                            排列序号
                        </th>
                        <th scope="col">
                            依据合同
                        </th>
                        <th scope="col">
                            上期计划金额
                        </th>
                        <th scope="col">
                            上期实际发生金额
                        </th>
                        <th scope="col">
                            上期计划执行完成情况
                        </th>
                        <th scope="col">
                            本期计划
                        </th>
                        <th scope="col">
                            备注
                        </th>
                        <th scope="col">
                            附件
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                        <asp:CheckBox ID="chkAll" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
                    </HeaderTemplate>

<ItemTemplate>
                        <asp:CheckBox ID="chk" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center" Visible="false"><ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="顺序号" Visible="false"><ItemTemplate>
                        <input type="text" id="txtOrderID" style="text-align: center" onkeydown="if(event.keyCode==13)event.keyCode=9;event.returnValue=CheckInputIsFloat(event.keyCode,this)" runat="server" />

                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同" ItemStyle-Width="135px" ItemStyle-CssClass="mustInput"><ItemTemplate>
                        <asp:HiddenField ID="hidenContractID" Value='<%# Convert.ToString(Eval("ContractID")) %>' runat="server" />
                        <span id="span2" class="spanSelect" style="width: 122px;">
                            <input type="text" style="width: 95px; height: 15px; border: none; line-height: 16px;
                                margin: 1px 0px 1px 2px;" id="txtContractName" class="{required:true, messages:{required:'依据合同必须输入'}}" readonly="readonly" value='<%# Convert.ToString(Eval("ContractName")) %>' runat="server" />

                            <img alt="选择合同" onclick="selectContr(this);" src="/images/icon.bmp" style="float: right;" />
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期结余" ItemStyle-Width="40px" Visible="false"><ItemTemplate>
                        <input type="text" id="txtOldBalance" style="text-align: right" onkeydown="if(event.keyCode==13)event.keyCode=9;event.returnValue=CheckInputIsFloat(event.keyCode,this)" class="edc" value='<%# Convert.ToString(Eval("OldBalance")) %>' runat="server" />

                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期计划金额" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                        
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期实际发生金额" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                        
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本期计划金额">
<ItemTemplate>
                        <input type="text" id="txtPlanMoney" style="text-align: right; width: 70;" class="eds decimal_input" runat="server" />

                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="乙方">
<ItemTemplate>
                        <div style="width: 100px;">
                            
                            <asp:Label ID="txtBName" Text="" CssClass="b-name" runat="server"></asp:Label>
                        </div>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                        <div style="width: 100px;">
                            <input type="text" id="txtConRemark" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />

                        </div>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload2" Name="附件" FileType="*.*" RecordCode='<%# Convert.ToString(Eval("UID")) %>' runat="server" />
                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <div class="divFooter2" style="position: relative; background-color: Blue;">
        <asp:Button ID="btnSave" Text="保存" Enabled="false" OnClick="btnSave_Click" runat="server" />
        <input id="btnClose" type="button" value="取消" onclick="winclose('FundMonthPlan', 'FundMonthPlan.aspx.aspx', false)" />
    </div>
    <div style="display: none">
        <table style="margin-left: auto; width: 100%; text-align: center;" cellpadding="5px"
            cellspacing="0">
            <tr id="Tr1" style="display: none">
                <td class="style1">
                    <asp:HiddenField ID="YMDBox" runat="server" />
                    <asp:HiddenField ID="hdnYear" runat="server" />
                    <asp:HiddenField ID="hdnMonth" runat="server" />
                    &nbsp;
                    <MyUserControl:usercontrol_querydatectrl_ascx ID="QueryDateCtrl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="style1">
                    <table class="" style="vertical-align: top;">
                        <tr id="trtoolbar" runat="server"><td style="text-align: left" runat="server">
                            </td></tr>
                        <tr id="trtoolbar2" style="display: none" runat="server"><td class="divFooter" style="text-align: left" runat="server">
                                &nbsp; &nbsp;
                                <asp:Label ID="lbEidtTS" ForeColor="#FF9900" runat="server"></asp:Label>
                            </td></tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divFram" title="添加" style="display: none">
            <iframe id="ifAdd" frameborder="0" width="100%" src="" height="100%"></iframe>
        </div>
        <div style="height: 0px; width: 0px;">
            <asp:Button ID="btndatabind" Text="" Width="0px" OnClick="btndatabind_Click" runat="server" />
        </div>
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
        </div>
        <div id="div1" style="display: none">
            <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
        <div id="showContent" style="display: none">
            <iframe id="ifShowContent" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
        <asp:HiddenField ID="mpid" runat="server" />
        <asp:HiddenField ID="hfldAdjunctPath" runat="server" />
        <asp:HiddenField ID="hdfcontrcn" runat="server" />
        <asp:HiddenField ID="hdfFlowState" runat="server" />
        <asp:HiddenField ID="hdfMonthPalnID" runat="server" />
        <asp:HiddenField ID="hdfTemponthPlanID" runat="server" />
    </div>
    </form>
</body>
</html>
