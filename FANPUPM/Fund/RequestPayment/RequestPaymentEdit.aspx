<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestPaymentEdit.aspx.cs" Inherits="Fund_RequestPayment_RequestPaymentEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>请款编辑</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>
    
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>

    <script type="text/ecmascript">
    
    addEvent(window, 'load', function() {
            addEvent(document.getElementById('btnSelectplan'), 'click', pickPurchase);
            replaceEmptyTable('emptyPlan', 'gvwPlan');
            var jwTable = new JustWinTable('gvwPlan');
            //setButton(jwTable, 'btnDelete', 'hfldPurchaseChecked');
            document.getElementById('btnSave').onclick = btnSaveClick;
            document.getElementById('btnAdd').onclick = btnSaveClick;
        });
        function pickPurchase() {
            $('#divSelectFromPlan').dialog({ width: 600, height: 485, modal: true });
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('hdnPerson').value = id;
            document.getElementById('txtPerson').value = name;
        }
        //选择人员
        function selectUser() {
            document.getElementById("divFram").title = "选择人员";
            document.getElementById("ifFram").src = "/Common/DivSelectUser.aspx?Method=returnUser";
            selectnEvent('divFram');
        }
        var _thistr=null;
        function selectContr(thistr) {
        _thistr=thistr;
            var prjguid=document.getElementById("hdnPrjguid").value;
            var addoper = 'addoper';
            document.getElementById("divFram").title = "选择支出合同";
            document.getElementById("ifFram").src = "/Fund/MonthPlan/DivSelectContrtAccon.aspx?Method=returnAccon&typevalue=" + addoper + "&prjcode=" +prjguid;
            selectnEvent('divFram');
            $('#divFram').dialog({ width: 600, height: 420, modal: true });
        }
        function returnAccon(id, valname) {
        if(_thistr!=null){
            //document.getElementById('hdnCont').value = id;
           // document.getElementById('txtCont').value = name;
           $(_thistr).next().val(id);
             $(_thistr).val(valname);
            }
        }
        
         function selectPlansubject(thistr) {
                _thistr=thistr;
            document.getElementById("divFram").title = "选择科目";
            var addoper = 'addoper';
            document.getElementById("ifFram").src = "/Fund/MonthPlan/selectPlansubject.aspx?Method=returnPlansubject&plantype=payout";
            selectnEvent('divFram');
            $('#divFram').dialog({ width: 220, height: 400, modal: true });
        }
        
        
        
          function btnSaveClick() {
            if (!validate()) {
                return false;
            }
        }

        function validate() {
            var inputs = document.getElementById('gvwPlan').getElementsByTagName('INPUT');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf('hdnCont') > 0) {
                    if (!inputs[i].value) {
                        alert('系统提示：\n\n合同不能为空');
                        return false;
                    }
                }
                if (inputs[i].id.indexOf('txtsubject') > 0) {
                    if (!inputs[i].value) {
                        alert('系统提示：\n\n所属科目不能为空');
                        return false;
                    }
                }
            }
            return true;
        }
        
        function returnPlansubject(valname) {
        if(_thistr!=null){
            $(_thistr).val(valname);
            //$("#txtsubject").val(valname);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td class="divHeader">
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="请款编辑" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" style="" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    请款单编号
                </td>
                <td class="txt mustInput" id="ttt">
                    <asp:TextBox ID="txtRPayCode" Height="15px" Style="width: 100%;" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtprjName" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    请款人
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtPerson" CssClass="txtreadonly" Style="width: 90%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择请款人" onclick="selectUser();" src="/images/icon.bmp" style="float: right;" />
                    </span>
                    <input type="hidden" id="hdnPerson" runat="server" />

                </td>
                <td class="word">
                    请款日期
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="DateInTime" Height="15px" Style="width: 100%;" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="flAnnx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="vertical-align: top; padding-top: 7px;">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtExplain" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <hr class="sp" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="headerDiv" style="text-align: left; height: 30px; width: 821px;">
                        <input type="button" id="btnSelectplan" style="width: 150px" value="从计划中选择" />
                        <asp:Button ID="btnAdd" Text="新增" OnClick="btnAdd_Click" runat="server" />
                        <asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="text-align: right">
                    <div id="divBudget" class="pagediv">
                        <asp:GridView ID="gvwPlan" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPlan_RowDataBound" DataKeyNames="RPayuid" runat="server">
<EmptyDataTemplate>
                                <table id="emptyPlan" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号 </td>
                                        <th scope="col">
                                            所属计划
                                        </th>
                                        <th scope="col">
                                            依据合同
                                        </th>
                                        <th scope="col">
                                            所属科目
                                        </th>
                                        <th scope="col">
                                            计划额度
                                        </th>
                                        <th scope="col">
                                            已用额度
                                        </th>
                                        <th scope="col">
                                            可用额度
                                        </th>
                                        <th scope="col">
                                            请款金额
                                        </th>
                                        <th scope="col">
                                            经手人
                                        </th>
                                        <th scope="col">
                                            备注
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="所属计划"><ItemTemplate>
                                        <asp:Label ID="lblPlanName" Text='<%# Convert.ToString(Eval("PlanName")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="依据合同">
<ItemTemplate>
                                        <asp:TextBox ID="txtCont" ContentEditable="false" CssClass="txtreadonly" Style="background-color: #ffffc0;" Width="100%" Text='<%# Convert.ToString(Eval("ContractOutName")) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hdnCont" Value='<%# Convert.ToString(Eval("ContractID")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="所属科目">
<ItemTemplate>
                                        <asp:TextBox ID="txtsubject" CssClass="txtreadonly" ondblclick="selectPlansubject(this)" Style="background-color: #ffffc0;" Width="100%" Text='<%# Convert.ToString(Eval("RPaysubject")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划额度"><ItemTemplate>
                                        <asp:Label ID="lblBalance" Text='<%# Convert.ToString(Eval("ThisBalance")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已用额度"><ItemTemplate>
                                        <asp:Label ID="lblAllowMoney" Text='<%# Convert.ToString(Eval("AllowMoney")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="可用额度"><ItemTemplate>
                                        <asp:Label ID="lblSurplus" Text='<%# Convert.ToString(Eval("Surplus")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="请款金额">
<ItemTemplate>
                                        <asp:TextBox ID="txtRPMoney" CssClass="decimal_input" Width="100%" Text='<%# Convert.ToString(Eval("RPayMoney")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="经手人">
<ItemTemplate>
                                        <asp:TextBox ID="txtPerson" Width="100%" Text='<%# Convert.ToString(Eval("RPayUser")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                        <asp:TextBox ID="txtremark" Width="100%" Text='<%# Convert.ToString(Eval("ReMark")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="RPayUID" Visible="false"><ItemTemplate>
                                        <asp:Label ID="lbluid" Text='<%# Convert.ToString(Eval("RPayUID")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();"
                        value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSelectFromPlan" title="从计划中选择" style="display: none">
        <iframe id="ifSelectFromPlan" frameborder="0" width="100%" height="100%" runat="server">
        </iframe>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <input type="hidden" id="hdnCode" runat="server" />

    <input type="hidden" id="hdnPrjguid" runat="server" />

    <input type="hidden" id="hdnUID" runat="server" />

    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldSingleId" runat="server" />
    <asp:Button ID="btnPlan" Width="0px" Text="Button" OnClick="btnPlan_Click" runat="server" />
    </form>
</body>
</html>
