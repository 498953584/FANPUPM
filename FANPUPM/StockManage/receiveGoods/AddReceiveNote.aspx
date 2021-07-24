<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddReceiveNote.aspx.cs" Inherits="StockManage_receiveGoods_AddReceiveNote" %>
<%@ Import Namespace="cn.justwin.BLL"%>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收货验收单</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvNeedNote');
            showTooltip('tooltip', 10);
        });

        //表单验证
        function valForm() {
            if (document.getElementById("txtExplain").value == "") {
                alert("系统提示:\n\n现场验收情况不能为空");
                return false;
            }
            var inputs = document.getElementById('gvNeedNote').getElementsByTagName('INPUT');
            return true;
        }
        function pickBindResource() {
            var url = "/EPC/Basic/Resource/ResourceSelect/ResourceSelectFrame.aspx?pt=0&rs=2";
            winopen(url);
        }

        function openCorp(suppyCode, goodsid) {
            document.getElementById("divFram").title = "供应商打分";
            var SnId = document.getElementById("hdGuid").value;

            var url = "../../EPC/SupplierGrade/SupplierGrade.aspx?SUPID=" + suppyCode + "&BILLSID=" + SnId + "&goodsid=" + goodsid;
            window.showModalDialog(url, "", "dialogHeight:200px;dialogWidth:900px;center:1;help:0;status:0;");

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdfSnId" runat="server" />
                    <asp:HiddenField ID="hdfScode" runat="server" />
                    <asp:HiddenField ID="hdfPrjCode" runat="server" />
                    <asp:HiddenField ID="hdfrnCode" runat="server" />
                    <asp:HiddenField ID="hdfstId" runat="server" />
                    <asp:HiddenField ID="hdfsoID" runat="server" />
                    <asp:HiddenField ID="hdfAllocationId" runat="server" />
                    <asp:HiddenField ID="hdfSendUser" runat="server" />
                    <asp:HiddenField ID="hdfTaCode" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4">
                    <div style="border: solid 0px red; height: 250px;">
                        <iframe id="ifView" frameborder="0" name="InfoList" width="99%" scrolling="yes" height="100%" runat="server"></iframe>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="headerDiv" style="text-align: left;">
                        <asp:Button ID="btnDel" Text="删除物资" Width="80px" OnClick="btnDel_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="pagediv" style="border: solid 0px red;">
                        <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvNeedNote_RowDataBound" DataKeyNames="scode,suppyCode" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("scode")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Eval("Name") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                        <asp:TextBox Width="80px" ID="txtNum" CssClass="decimal_input" ToolTip='<%# Convert.ToString(Eval("scode")) %>' Text='<%# Convert.ToString(Eval("number")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格"><ItemTemplate>
                                        <asp:Label ID="labPrice" Width="80px" Text='<%# Convert.ToString(Eval("price")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                        <asp:Label ID="labCrop" ToolTip="" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商评估" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                        <asp:Button ID="btnCorp" Text="评估" Enabled="false" OnClick="btnCorp_Click" runat="server" />
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td style="width: 50px; text-align: right;">
                    现场验收情况
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtExplain" Rows="2" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50px; text-align: right;">
                    备注
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRemark" Rows="2" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddSendNote','SendNoteList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSelectFromPurchase" title="从需求计划单中选择" style="display: none">
        <iframe id="ifResoueceFromPurchase" frameborder="0" width="100%" height="100%" src="SelectByNote.aspx">
        </iframe>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdnCodeList" runat="server" />
    <asp:HiddenField ID="hdfDepotType" runat="server" />
    </form>
</body>
</html>
