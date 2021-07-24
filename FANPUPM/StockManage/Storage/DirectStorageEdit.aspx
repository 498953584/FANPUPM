<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectStorageEdit.aspx.cs" Inherits="StockManage_Storage_DirectStorageEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>新增物资（直接的不通过采购单）</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.external/jquery.bgiframe-2.1.2.js"></script>
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
            addEvent(document.getElementById('btnCancel'), 'click', btnCancelEvent);
            document.getElementById('btnDelete').onclick = deleteStorageStock;
            document.getElementById('btnSave').onclick = btnSaveEvent;
            replaceEmptyTable('emptyStock', 'gvwStorageStock');
            var stockTable = new JustWinTable('gvwStorageStock');
            showTooltip('tooltip', 10);
            setHeight();

            $('.txtreadonly').attr('readOnly', true); // 供应商不可手动修改
        });

        function setHeight() {
            $('#divStorageStock').height($(this).height() - $('#divButtons').height() - 65);
        }
        function btnCancelEvent() {
            winclose('StorageEdit', 'Storage.aspx', false)
        }

        function deleteStorageStock() {
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }

        function btnSaveEvent() {
            if (!validateInput()) {
                return false;
            }
        }

        function validateInput() {
            var inputs = document.getElementById('gvwStorageStock').getElementsByTagName('INPUT');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf('txtNumber') >= 0) {
                    var validateInput = new ValidateInput(inputs[i].id, '数量');
                    if (!validateInput.validateMustInput()) return false;
                    if (!validateInput.validateIntFormat()) return false;
                    if (!validateInput.validateNoneZero()) { $('#' + inputs[i].id).focus(); return false; }
                }
            }
            return true;
        }

        function theMonenyChange(index, isOnblur) {
            tb = document.getElementById('gvwStorageStock');
            var cells = tb.rows[parseFloat(index) + 1].cells;
            var quantity = parseFloat(cells[9].children[0].value);
            var price = parseFloat(cells[10].children[0].value);
            if (isOnblur == '1') {
                cells[9].children[0].value = getFormat(quantity, 3);
            }
            if (isOnblur == '1') {
                cells[10].children[0].value = getFormat(price, 3);
            }
            var total = getFormat(quantity) * getFormat(price);
            cells[11].innerText = getFormat(total, 3);

            var contractMoney = 0;
            for (var i = 1; i < tb.rows.length - 1; i++) {
                contractMoney += parseFloat(tb.rows[i].cells[11].innerText);
            }
            tb.rows[tb.rows.length - 1].cells[1].innerText = getFormat(contractMoney, 3);
        }

        //格式化三位小数
        function getFormat(num, places) {
            var numFormat;
            if (typeof places == 'undefined' || places == undefined)
                places = 3;
            if (num.toFixed) {
                numFormat = num.toFixed(places);
            } else {
                var f3 = Math.pow(10, places);
                numFormat = Math.round(num * f3) / f3;
            }
            return numFormat;
        }
        //选择供应商
        function pickCorp(txtCorp) {
            var txtID = txtCorp.id.replace('txt', 'hfld')
            jw.selectOneCorp({ idinput: txtID, nameinput: txtCorp.id });
        }

        function selectResource() {
            var typeCode = '0002,0003';
            var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
            top.ui.callback = function (obj) {
                $('#hfldResourceId').val(obj.id);
                $('#btnBindResource').click();
            }
            top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="入库单" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
            <tr>
                <td>
                    <div id="divButtons" class="headerDiv" style="text-align: left;">
                        <input type="button" id="btnSelectResource" value="从材料库中选择" onclick="selectResource();"
                            style="width: 150px;" />
                        <asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td>
                    <div id="divStorageStock" class="pagediv">
                        <asp:GridView ID="gvwStorageStock" CssClass="gvdata" AutoGenerateColumns="false" OnPageIndexChanging="gvwStorageStock_PageIndexChanging" OnRowDataBound="gvwStorageStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
<EmptyDataTemplate>
                                <table id="emptyStock" class="gvdata">
                                    <tr class="header">
                                        <td style="width: 20px" scope="col">
                                            <input type="checkbox" id="ch1" />
                                        </td>
                                        <td style="width: 30px" align="center" scope="col">
                                            序号
                                        </td>
                                        <td scope="col" align="center">
                                            物资编号
                                        </td>
                                        <td scope="col" align="center">
                                            物资名称
                                        </td>
                                        <td scope="col" align="center">
                                            规格
                                        </td>
                                        <td scope="col" align="center">
                                            型号
                                        </td>
                                        <td scope="col" align="center">
                                            品牌
                                        </td>
                                        <td scope="col" align="center">
                                            技术参数
                                        </td>
                                        <td scope="col" align="center">
                                            单位
                                        </td>
                                        <td scope="col" align="center">
                                            数量
                                        </td>
                                        <td scope="col" align="center">
                                            价格
                                        </td>
                                        <td scope="col" align="center">
                                            小计
                                        </td>
                                        <td align="center">
                                            供应商
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceCode" HeaderText="物资编号" /><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="200px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
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
                                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Name" HeaderText="单位" /><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                        <asp:TextBox ID="txtNumber" Style="text-align: right; width: 80px;" MaxLength="8" onkeypress="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else this.t_value=this.value;if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onkeyup="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else {this.t_value=this.value;if(this.t_value.length>0)theMonenyChange(this.No,'0');}if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onblur="if(!this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/)){this.value=this.o_value;}else{if(this.value.length==0){this.value='0';theMonenyChange(this.No,'1');}if(this.value.match(/^\.\d+$/))this.value=0+this.value;if(this.value.match(/^\.$/)){this.value=0;}this.o_value=this.value;theMonenyChange(this.No,'1');}" Text='<%# Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("number")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格"><ItemTemplate>
                                        <asp:TextBox ID="txtPrice" Style="text-align: right; width: 80px;" MaxLength="7" onkeypress="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else this.t_value=this.value;if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onkeyup="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else {this.t_value=this.value;if(this.t_value.length>0)theMonenyChange(this.No,'0');}if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onblur="if(!this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/)){this.value=this.o_value;}else{if(this.value.length==0){this.value='0';theMonenyChange(this.No,'1');}if(this.value.match(/^\.\d+$/))this.value=0+this.value;if(this.value.match(/^\.$/)){this.value=0;}this.o_value=this.value;theMonenyChange(this.No,'1');}" Text='<%# Convert.ToString((Eval("price").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("price")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="80px">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
                                        <asp:TextBox ID="txtCorp" Width="90px" CssClass="txtreadonly" ondblclick="pickCorp(this)" ToolTip="请双击此处选择" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldCorp" Value='<%# Convert.ToString(Eval("corpId")) %>' runat="server" />
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
                    <asp:Button ID="btnSave" Text="保存" Enabled="false" OnClick="btnSave_Click" runat="server" />
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
    <asp:HiddenField ID="hfldPid" runat="server" />
    <asp:HiddenField ID="hfldProject" runat="server" />
    <asp:HiddenField ID="hfldPurchaseCode" runat="server" />
    <asp:HiddenField ID="hfldResourceCode" runat="server" />
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    
    <asp:Button ID="btnBindResource" Text="" OnClick="btnBindResource_Click" runat="server" />
    </form>
</body>
</html>
