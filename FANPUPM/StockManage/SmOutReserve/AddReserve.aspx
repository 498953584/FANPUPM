<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddReserve.aspx.cs" Inherits="StockManage_SmOutReserve_AddReserve" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>出库单</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
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
            $('#gvNeedNote tr').find('.selectTask').attr('readonly', 'readonly');
            $('#gvNeedNote tr').find('.selectTask').click(function () {
                selectTask(this.id);
            });
            $('#btnSave').click(function () {
                if (valForm() == false) {
                    return false;
                }
            });
        });

        // 表单验证
        function valForm() {
            if (document.getElementById("txtTrea").value == "") {
                top.ui.alert("请先选择仓库");
                return false;
            }
            if (document.getElementById("hdTsid").value == "") {
                top.ui.alert("请选择资源");
                return false;
            }
            if (document.getElementById("txtPickingPeople").value == "") {
                top.ui.alert("领料人不能为空");
                return false;
            }
        }

        // 打开仓库
        function openkc() {
            if ($('#hfldTrea').val() == "") {
                top.ui.alert("请先选择仓库");
            } else {
                var url = '/StockManage/SmOutReserve/StuffList.aspx?tcode=' + $('#hfldTrea').val();
                top.ui.callback = function (o) {
                    $('#hdTsid').val(o.id);
                    $('#btnShowGv').click();
                }
                top.ui.openWin({ title: '选择资源', url: url, width: 900, height: 500 });
            }
        }

        // 显示物资需求计划
        function displaySelectFromPurchase() {
            var url = '/StockManage/ProjectFrame.aspx?path=selectWantPlan&Type=check'
            top.ui.openWin({ title: '查看需求计划', url: url, width: 820, height: 500 });
        }

        // 比较出库数量
        function chkNum(index, num1, num2) {
            if (parseFloat(num1.value) < 0) {
                top.ui.alert('出库数量不能小于零!');
                num1.value = 0.000;
                return;
            }
            if (parseFloat(num1.value) > parseFloat(num2.value)) {
                top.ui.alert('出库数量超过了库存数量!');
                num1.value = 0.000;
                return;
            }
            var totalCount = 0.0;
            var number = 0.0;
            var sprice = 0.0;
            $('#gvNeedNote tr[id]').each(function () {
                number = $(this).find('input[id$=txtCNum]').val();
                sprice = $(this).find('input[id$=hdSprice]').val();
                var total = number * sprice;
                $(this).find('span[id$=lbTotal]').text(total.toFixed(3));
                totalCount += total;
            });
            $('#gvNeedNote').find('span[class=_total_]').text(totalCount.toFixed(3));
        }
        //选择设备
        function selectEqu() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            var equDivInfo = { url: url, title: '选择设备', width: 800, height: 500 };
            top.ui.callback = function (equInfo) {
                $('#hfldEquId').val(equInfo.id);
                $('#txtEquCode').val(equInfo.code);
            };
            top.ui.openWin(equDivInfo);
        }

        function selectTrea() {
            jw.selectTreasury({ codeinput: 'hfldTrea', nameinput: 'txtTrea' });
        }

        function selectTask(txtId) {
            var url = '/Equ/ShipOilWear/BudTaskList.aspx?type=taskName&prjId=' + $('#hfldPrjId').val();
            top.ui.callback = function (taskInfo) {
                var hfldId = txtId.replace('txt', 'hfld');
                $('#' + txtId).val(taskInfo.taskCode);
                $('#' + hfldId).val(taskInfo.taskId);
            }
            top.ui.openWin({ url: url, title: '选择分部分项', width: 1000, height: 500 });
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
    <div class="divContent2" style="text-align: center;">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
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
                    <asp:TextBox ID="txtPPCode" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    项目
                </td>
                <td class="txt readonly">
                    <input type="text" readonly="readonly" style="width: 100%;" id="txtProjectName" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    领料人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtPickingPeople" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    领料部门
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtPickingSector" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    选择仓库
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtTrea" CssClass="select_input" imgclick="selectTrea" Width="100%" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfldTrea" runat="server" />
                </td>
                <td class="word" style="white-space: nowrap;">
                    设备
                </td>
                <td>
                    <asp:TextBox ID="txtEquCode" CssClass="select_input" imgclick="selectEqu" Width="100%" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfldEquId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    说明
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtExplain" Style="width: 100%;" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    录入人
                </td>
                <td class="txt readonly">
                    <input type="text" readonly="readonly" width="100%" id="txtPeople" runat="server" />

                </td>
                <td class="word" style="white-space: nowrap;">
                    录入时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtInTime" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 10px">
                    <hr class="sp" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="headerDiv" style="text-align: left;">
                        <input type="button" id="btnSelectByd" value="从仓库选择资源" onclick="openkc()" style="width: 100px;" runat="server" />

                        <asp:Button ID="btnDel" Style="width: 100px;" Text="删除资源" OnClick="btnDel_Click" runat="server" />
                        <asp:Button ID="btnShowGv" Text="显示" Style="display: none;" OnClick="btnShowGv_Click" runat="server" />
                        <input type="button" id="Button1" value="查看需求计划" onclick="displaySelectFromPurchase();" style="width: 100px;" runat="server" />

                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td>
                    <div class="pagediv" style="margin: 0 auto; background-color: Aqua; text-align: center;">
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
                                            物资名称
                                        </th>
                                        <th scope="col">
                                            规格
                                        </th>
                                        <th scope="col">
                                            单位
                                        </th>
                                        <th scope="col">
                                            库存数量
                                        </th>
                                        <th scope="col">
                                            出库数量
                                        </th>
                                        <th scope="col">
                                            单价
                                        </th>
                                        <th scope="col">
                                            小计
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
                                        <asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("scode")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                        <%# Eval("scode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="材料名称"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Eval("Name") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存数量" HeaderStyle-Width="80px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" ID="txtSnumber" CssClass="snumber" Enabled="false" Text='<%# Convert.ToString((Eval("snumber").ToString() == "") ? "0.000" : Eval("snumber")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="出库数量" HeaderStyle-Width="80px"><ItemTemplate>
                                        <asp:TextBox ID="txtCNum" Style="text-align: right;" CssClass="decimal_input" Text='<%# Convert.ToString(Eval("number")) %>' ToolTip='<%# Convert.ToString(Eval("scode")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hdScode" Value='<%# Convert.ToString(Eval("scode")) %>' runat="server" />
                                        <asp:HiddenField ID="hdSprice" Value='<%# Convert.ToString(Eval("sprice")) %>' runat="server" />
                                        <asp:HiddenField ID="hdCorp" Value='<%# Convert.ToString(Eval("corpId")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="sprice" HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="出库小计" HeaderStyle-Width="80px">
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
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分部分项"><ItemTemplate>
                                        <asp:TextBox ID="txtTask" CssClass="selectTask" Text='<%# Convert.ToString(Eval("TaskName")) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldTask" Value='<%# Convert.ToString(Eval("TaskId")) %>' runat="server" />
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
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddReserve','SmOutReserveList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdTsid" runat="server" />
    <asp:HiddenField ID="hdflowstate" Value="-1" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    </form>
</body>
</html>
