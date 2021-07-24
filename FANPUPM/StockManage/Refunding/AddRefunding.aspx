<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRefunding.aspx.cs" Inherits="StockManage_Refunding_AddRefunding" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>材料退库</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyContractType', 'gvNeedNote');
            var jwTable = new JustWinTable('gvNeedNote');
            showTooltip('tooltip', 10);
        });


        // 表单验证
        function valForm() {
            if (document.getElementById("hdnProjectCode").value == "") {
                top.ui.alert("请选择项目");
                return false;
            }
            if (document.getElementById("hdwzId").value == "") {
                top.ui.alert("请选择资源");
                return false;
            }
            return true;
        }

        // 从出库选择验证
        function openkc() {
            if (document.getElementById("hfldTrea").value == "" || document.getElementById("hdnProjectCode").value == "") {
                top.ui.alert("请先选择项目和仓库");

            } else {
                var src = '/StockManage/Refunding/BackStockList.aspx?tcode=' + $("#hfldTrea").val() + '&prcode=' + $("#hdnProjectCode").val();
                top.ui.callback = function (code) {
                    $('#hdwzId').val(code);
                    $('#btnShowGv').click();
                }
                top.ui.openWin({ title: '从出库单中选择', url: src, width: 900, height: 485 });
            }
        }

        // 验证退库数量不为0
        function chkTNum() {
            var nums = document.getElementById("hdTnumId").value.split(',');
            var boolNum = true;
            for (i = 0; i < nums.length - 1; i++) {
                if (parseFloat(document.getElementById(nums[i]).value) <= 0) {
                    boolNum = false;
                }
            }
            return boolNum;
        }

        // 比较出库数量
        function chkNum(index, num1, num2) {
            if (parseFloat(num1.value) > parseFloat(num2.value)) {
                top.ui.alert('退库数量超过了可退数量');
                num1.value = parseFloat(0.000);
                return false;
            }
            if (parseFloat(num1.value) < 0) {
                top.ui.alert("退库数量必须大于0");
                num1.value = parseFloat(0.000);
                return false;
            }
            var sprice = 0.00;
            var number = 0.00;
            var rowTotal = 0.00;
            var allTotal = 0.00;
            $('#gvNeedNote tr[id]').each(function () {
                sprice = $(this).find('input[id$=txtTNum]').val();
                number = $(this).find('span[id$=lbSprice]').text();
                rowTotal = sprice * number;
                $(this).find('span[id$=lbTotal]').text(rowTotal.toFixed(3));
                allTotal += rowTotal;
            });
            $('#gvNeedNote').find('span=[class=_total_]').text(allTotal.toFixed(3));
        }
        function selectTrea() {
            jw.selectTreasury({ codeinput: 'hfldTrea', nameinput: 'txtTrea' });
        }
    </script>
    <style type="text/css">
        #FileLink1_But_UpFile
        {
            width: auto;
        }
        #FileLink1_Btn_Upload
        {
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    单据号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPPCode" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap">
                    选择仓库
                </td>
                <td class="txt mustInput" style="padding-right: 1px">
                    <asp:TextBox ID="txtTrea" CssClass="select_input" imgclick="selectTrea" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfldTrea" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    项目
                </td>
                <td class="txt readonly" colspan="3" style="padding-right: 3px">
                    <input type="text" readonly="readonly" style="width: 100%;" id="txtProjectName" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    说明
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtExplain" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    录入人
                </td>
                <td class="txt readonly">
                    <input type="text" readonly="readonly" class="txtreadonly" width="100%" id="txtPeople" runat="server" />

                </td>
                <td class="word" style="white-space: nowrap">
                    录入时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtInTime" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 10px">
                    <hr class="sp" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left;">
                    <input type="button" id="btnSelectByd" onclick="openkc()" value="从出库单中选择" style="width: 100px;" runat="server" />

                    <asp:Button ID="btnDel" Style="width: 100px;" Text="删除材料" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnShowGv" Text="显示" Style="display: none;" OnClick="btnShowGv_Click" runat="server" />
                </td>
            </tr>
        </table>
        
        <div>
            <table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
                <tr>
                    <td style="margin: 0px; padding: 0px;">
                        <div class="pagediv">
                            <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvNeedNote_RowDataBound" DataKeyNames="scode" runat="server">
<EmptyDataTemplate>
                                    <table id="emptyContractType" class="gvdata">
                                        <tr class="header">
                                            <th scope="col" style="width: 20px;">
                                                <input id="chk1" type="checkbox" />
                                            </th>
                                            <th scope="col" style="width: 25px;">
                                                序号
                                            </th>
                                            <th scope="col">
                                                物资编号
                                            </th>
                                            <th scope="col">
                                                退库单号
                                            </th>
                                            <th scope="col">
                                                物资名称
                                            </th>
                                            <th scope="col">
                                                规格
                                            </th>
                                            <th scope="col">
                                                单位
                                            </th>
                                            <th scope="col">
                                                剩余量
                                            </th>
                                            <th scope="col">
                                                退库数量
                                            </th>
                                            <th scope="col">
                                                单价
                                            </th>
                                            <th scope="col">
                                                退库小计
                                            </th>
                                            <th scope="col">
                                                供应商
                                            </th>
                                            <th scope="col">
                                                型号
                                            </th>
                                            <th scope="col">
                                                品牌
                                            </th>
                                            <th scope="col">
                                                技术参数
                                            </th>
                                            <th scope="col">
                                                分部分项
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </HeaderTemplate><ItemTemplate>
                                            <asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("orsid")) %>' runat="server" />
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                            <%# Eval("scode") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="出库单号"><ItemTemplate>
                                            <%# Eval("orcode") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称" HeaderStyle-Width="150px"><ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="40px"><ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="25px"><ItemTemplate>
                                            <%# Eval("Name") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="剩余量" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <asp:TextBox Enabled="false" Style="text-align: right;" ID="txtNum" ToolTip='<%# Convert.ToString(Eval("scode")) %>' Text='<%# Convert.ToString(base.GetNumber(Eval("number").ToString(), Eval("orcode").ToString(), Eval("sprice").ToString(), Eval("scode").ToString(), Eval("CorpId").ToString())) %>' runat="server"></asp:TextBox>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="退库数量"><ItemTemplate>
                                            <asp:TextBox Style="text-align: right;" CssClass="decimal_input" ID="txtTNum" ToolTip='<%# Convert.ToString(Eval("orsid")) %>' Text='<%# Convert.ToString(Eval("tnumber")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价">
<ItemTemplate>
                                            <asp:Label ID="lbSprice" CssClass="decimal_input" Text='<%# Convert.ToString(Eval("sprice")) %>' runat="server"></asp:Label>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="退库小计" HeaderStyle-Width="80px">
<ItemTemplate>
                                            <asp:Label ID="lbTotal" CssClass="decimal_input" Text='<%# Convert.ToString((Eval("Total").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("Total")).ToString("0.000")) %>' runat="server"></asp:Label>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("Corp").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("Corp").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分部分项" HeaderStyle-Width="60px"><ItemTemplate>
                                            <asp:Label ID="lblTaskName" Text='<%# Convert.ToString(Eval("TaskName")) %>' runat="server"></asp:Label>
                                            <asp:HiddenField ID="hfldTaskId" Value='<%# Convert.ToString(Eval("TaskId")) %>' runat="server" />
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" Style="cursor: pointer;" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddRefunding','RefundingList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdflowstate" Value="-1" runat="server" />
    <asp:HiddenField ID="hdTnumId" runat="server" />
    <asp:HiddenField ID="hdnProjectCode" runat="server" />
    </form>
</body>
</html>
