<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickMaterialsOfDepository.aspx.cs" Inherits="StockManage_Allocation_PickMaterialsOfDepository" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源选择</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('GVMaterialList');
            showTooltip('tooltip', 10);
            jwTable.registClickTrListener(trClickEvent);
            $('#GVMaterialList a').click(function () {
                $("#hdtp").val("1");
            });
            $('#GVMaterialList input').click(function () {
                $("#hdtp").val("0");
            });
        });

        // 保存事件
        function saveEvent() {
            var tsid = eval($("#hdTsid").val());
            var codelist = "";
            for (var i = 0; i < tsid.length; i++) {
                var val = tsid[i].split('|');
                codelist += "(scode='" + val[0] + "' and sprice='" + val[1] + "' and corp='" + val[2] + "') or ";
            }
            codelist += "(scode='-1' and sprice='-1' and corp='-1')";

            if (typeof top.ui.callback == 'function') {
                top.ui.callback({ code: codelist });
                top.ui.callback == null;
            }
            top.ui.closeWin();
        }

        function trClickEvent() {
            var chk;
            var inputs = this.getElementsByTagName('INPUT');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].getAttribute('type') == 'checkbox') {
                    chk = inputs[i];
                }
            }
            if (chk) {
                chk.click();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" cellspacing="0">
        <tr style="display: none;">
            <th class="headerrow">
                资源选择
            </th>
        </tr>
        <tr style="height: 30px;">
            <td>
                物资编号<asp:TextBox ID="txtResCode" Width="120px" runat="server"></asp:TextBox>
                物资名称<asp:TextBox ID="txtResName" Width="120px" runat="server"></asp:TextBox>
                供应商<asp:TextBox ID="txtSupply" Width="120px" runat="server"></asp:TextBox>
                <asp:Button ID="btnSertch" Text="查询" OnClick="btnSertch_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; height: 374px;">
                <div class="pagediv" style="height: 382px; width: 100%;">
                    <asp:GridView ID="GVMaterialList" Width="100%" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="GVMaterialList_RowDataBound" OnPageIndexChanging="GVMaterialList_PageIndexChanging" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="chkHdSelectIt" AutoPostBack="true" OnCheckedChanged="chkSelectIt_CheckedChanged" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="chkSelectIt" AutoPostBack="true" OnCheckedChanged="chkSelectIt_CheckedChanged" runat="server" />
                                    <asp:HiddenField ID="hdScode" Value='<%# Convert.ToString(Eval("ResourceCode")) %>' runat="server" />
                                    <asp:HiddenField ID="hdSprice" Value='<%# Convert.ToString(Eval("sprice")) %>' runat="server" />
                                    <asp:HiddenField ID="hdCorp" Value='<%# Convert.ToString(Eval("corpID")) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                    <asp:Label ID="Label1" Text='<%# Convert.ToString(Eval("ResourceCode")) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
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
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    <asp:Label ID="txtUnitPrice" Width="80px" Text='<%# Convert.ToString(Eval("sprice")) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存数量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                    <asp:Label ID="Label4" Text='<%# Convert.ToString(Eval("snumber")) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("CorpName").ToString(), 10) %>
                                    </span>
                                    <input type="hidden" id="HdnCorpID" value='<%# Convert.ToString(Eval("corpID")) %>' runat="server" />

                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td class="bottonrow2" style="height: 30px" align="right">
                <input id="btnSave" type="button" value="保 存" style="cursor: pointer;" onclick="saveEvent();" runat="server" />

                <input id="btnCancel" type="button" value="取 消" style="cursor: pointer;" onclick="top.ui.closeWin();" />
            </td>
        </tr>
    </table>
    <input id="HdnIsPage" type="hidden" value="" style="width: 1px;" runat="server" />

    <asp:HiddenField ID="hdTsid" runat="server" />
    <asp:HiddenField ID="hdtp" Value="0" runat="server" />
    <input id="Hdnacode" type="hidden" style="width: 1px" runat="server" />

    <input id="HdnOperator" type="hidden" style="width: 1px" runat="server" />

    </form>
</body>
</html>
