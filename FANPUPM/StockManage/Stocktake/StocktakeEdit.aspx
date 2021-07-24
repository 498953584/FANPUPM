<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeEdit.aspx.cs" Inherits="StockManage_Stocktake_StocktakeEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>盘点结存</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../script/Validation.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            var action = getRequestParam('Action');
            if (action == 'Query') {
                setAllInputDisabled();
            }
            showTooltip('tooltip', 10);
            addEvent(document.getElementById('btnCancel'), 'click', btnCancelEvent);
            var stockTable = new JustWinTable('gvwStocktake');
            if (document.getElementById('hfldState').value == '0') {
                document.getElementById('btnHangUp').removeAttribute('disabled');
            } else if (document.getElementById('hfldState').value == '1') {
                document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
            }
            if (document.getElementById('gvwStocktake') == null) {
                document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
            }

            // 红色显示盈亏数据
            $('.profit_and_loss').each(function () {
                var profitAndLoss = _strToDecimal($(this).html());
                if (profitAndLoss < 0) {
                    var $tr = $(getFirstParentElement(this, 'TR'));
                    $tr.css('color', '#ff0000')
                }
            });
        });


        function btnCancelEvent() {
            winclose('StocktakeEdit', 'StocktakeList.aspx', false)
        }

        function btnSaveEvent() {
            if (!validateInput()) {
                return false;
            }
        }

        // 计算盘点盈亏
        function calcStocktakeNum(e) {
            var $tr = $(getFirstParentElement(e, 'TR'));
            var bookNum = _strToDecimal($tr.find('.book_num').html());              // 账面数量
            var stocktakeNum = _strToDecimal($tr.find('.stocktake_num').val());     // 盘点数量
            var profitAndLoss = stocktakeNum - bookNum;                             // 盈亏
            $tr.find('.profit_and_loss').html(_formatDecimal(profitAndLoss));
            if (profitAndLoss < 0) {
                // 红色显示
                $tr.css('color', '#ff0000')
            }
        }

        function setSingleMonth() {
            var year = document.getElementById('ddlYear').value;
            var month = document.getElementById('ddlMonth').value;
            var tcode = document.getElementById('hfldTCode').value;
            var isExist = 0;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/StockManage/Handler/Stocktake.ashx?' + new Date().getTime() + '&type=equal&treasuryCode=' + tcode + '&stocktakeDate=' + year + month,
                success: function (data) {
                    isExist = data;
                }
            });
            if (isExist >= 1) {
                top.ui.alert('所选年月的盘点已经结束，不能重复盘点.');
                document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
            } else {
                var strDateS = new Date(year + '/' + month + '/1');
                var strDateE = new Date(document.getElementById('hfldToday').value + '/1');
                intDay = (strDateE - strDateS) / (1000 * 3600 * 24);
                if (intDay < 0) {
                    top.ui.alert('不能对所选的年月进行盘点.');
                    document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
                } else {
                    var isExist2 = 0;
                    $.ajax({
                        type: 'GET',
                        async: false,
                        url: '/StockManage/Handler/Stocktake.ashx?' + new Date().getTime() + '&type=more&treasuryCode=' + tcode + '&stocktakeDate=' + year + month,
                        success: function (data) {
                            isExist2 = data;
                        }
                    });
                    if (isExist2 == 1) {
                        top.ui.alert('不能对所选的年月进行盘点.');
                        document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
                    } else {
                        document.getElementById('txtName').value = year + '年' + month + '月的盘点单';
                        if (document.getElementById('hfldState').value == '0') {
                            document.getElementById('btnHangUp').removeAttribute('disabled');
                        } else if (document.getElementById('hfldState').value == '1') {
                            document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
                        }
                        if (document.getElementById('gvwStocktake') == null) {
                            document.getElementById('btnHangUp').setAttribute('disabled', 'disabled');
                        }
                    }
                }
            }
        }

        //格式化三位小数
        function getFormat(num) {
            var numFormat;
            if (num.toFixed) {
                numFormat = num.toFixed(3);
            } else {
                var f3 = Math.pow(10, 3);
                numFormat = Math.round(num * f3) / f3;
            }
            return numFormat;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="盘点结存" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    盘点编码
                </td>
                <td class="txt" id="ttt">
                    <asp:TextBox ID="txtCode" Enabled="false" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    盘点名称
                </td>
                <td class="txt mustInput" id="Td1">
                    <asp:TextBox ID="txtName" Height="15px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    盘点所属年月
                </td>
                <td class="txt" style="padding-right: 3px">
                    <asp:DropDownList ID="ddlYear" DataValueField="Year" onchange="setSingleMonth()" runat="server"></asp:DropDownList>
                    年&nbsp;
                    <asp:DropDownList ID="ddlMonth" DataValueField="Month" onchange="setSingleMonth()" runat="server"></asp:DropDownList>
                    月
                </td>
                <td class="word" style="white-space: nowrap;">
                    盘点日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="DateInTime" Enabled="false" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    盘点开始时间
                </td>
                <td class="txt" style="padding-right: 3px">
                    <asp:TextBox ID="txtBeginDate" Enabled="false" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    盘点截止日期
                </td>
                <td class="txt" style="padding-right: 3px">
                    <asp:TextBox ID="txtEndDate" Enabled="false" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    仓库名称
                </td>
                <td class="txt" style="padding-right: 3px">
                    <asp:TextBox ID="txtTName" Enabled="false" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    盘点人
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtPerson" Enabled="false" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="vertical-align: top; padding-top: 7px; white-space: nowrap;">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtExplain" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
            <tr>
                <td style="height: 10px">
                    <hr class="sp" />
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td>
                    <div class="pagediv">
                        <asp:GridView ID="gvwStocktake" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwStocktake_RowDataBound" DataKeyNames="ResourceCode" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceCode" HeaderText="物资编号" /><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="200px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                        <asp:HiddenField ID="hlfdResourceName" Value='<%# Convert.ToString(Eval("ResourceName").ToString()) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="150px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                        </span>
                                        <asp:HiddenField ID="hlfdSpecification" Value='<%# Convert.ToString(Eval("Specification").ToString()) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                        <asp:Label ID="lblUnit" Text='<%# Convert.ToString(Eval("Unit")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Price" HeaderText="价格" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="供应商" HeaderStyle-Width="200px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Supplier").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Supplier").ToString(), 10) %>
                                        </span>
                                        <asp:HiddenField ID="hlfdSupplier" Value='<%# Convert.ToString(Eval("Supplier").ToString()) %>' runat="server" />
                                        <asp:HiddenField ID="hlfdSupplierId" Value='<%# Convert.ToString(Eval("SupplierId").ToString()) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期结余">
<ItemTemplate>
                                        <asp:Label ID="lblLastMonthNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("LastMonthNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入库数量">
<ItemTemplate>
                                        <asp:Label ID="lblStorageNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("StorageNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="甲供数量">
<ItemTemplate>
                                        <asp:Label ID="lblFirstStorageNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("FirstStorageNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="出库数量">
<ItemTemplate>
                                        <asp:Label ID="lblOutReserveNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("OutReserveNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调拨入库数量">
<ItemTemplate>
                                        <asp:Label ID="lblTransferringInNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("TransferringInNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调拨出库数量">
<ItemTemplate>
                                        <asp:Label ID="lblTransferringOutNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("TransferringOutNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="报损出库数量">
<ItemTemplate>
                                        <asp:Label ID="lblWastageNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("WastageNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="退库数量">
<ItemTemplate>
                                        <asp:Label ID="lblRefundingNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("RefundingNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="账面数量">
<ItemTemplate>
                                        <asp:Label ID="lblBookNum" CssClass="book_num" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("BookNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="盘点数量">
<ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" CssClass="decimal_input stocktake_num" Width="80px" ID="txtNum" onblur="calcStocktakeNum(this);" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("StocktakeNum")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="盘盈/盘亏" HeaderStyle-Width="200px">
<ItemTemplate>
                                        <span class="profit_and_loss">
                                            <%# (Convert.ToDecimal(Eval("StocktakeNum")) - Convert.ToDecimal(Eval("BookNum"))).ToString("0.000") %></span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="200px"><ItemTemplate>
                                        <asp:TextBox Width="80px" ID="txtNote" ToolTip='<%# Convert.ToString(Eval("Note")) %>' Text='<%# Convert.ToString(Eval("Note")) %>' runat="server"></asp:TextBox>
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
                    <asp:Button ID="btnHangUp" Text="挂起" OnClick="btnHangUp_Click" runat="server" />
                    <asp:Button ID="btnSave" Text="保存并提交" Width="80px" Visible="false" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSelectFromPurchase" title="从采购单中选择" style="display: none">
        <iframe id="ifResoueceFromPurchase" frameborder="0" width="100%" height="100%" src="PurchaseList.aspx">
        </iframe>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <!--项目编码-->
    <asp:HiddenField ID="hfldStocktakeId" runat="server" />
    <asp:HiddenField ID="hfldTCode" runat="server" />
    
    <asp:HiddenField ID="hfldPurchaseCode" runat="server" />
    <asp:HiddenField ID="hfldResourceCode" runat="server" />
    <asp:HiddenField ID="hfldIsAdd" runat="server" />
    <asp:HiddenField ID="hfldState" Value="0" runat="server" />
    <asp:HiddenField ID="hfldToday" runat="server" />
    
    </form>
</body>
</html>
