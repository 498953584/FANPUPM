<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutTarget.aspx.cs" Inherits="ContractManage_PayoutContract_PayoutTarger" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>添加控制指标</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <style type="text/css">
        
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var tab = new JustWinTable('gvBudget');
            tab.registClickTrListener(function () {
                $('#hfldCheckedIds').val($(this).attr('id'));
                $('#hfldModifyType').val($(this).attr('modifyType'));
                $('#hfldOrderNumber').val($(this).attr('orderNumber'));
                $('#hfldSubCount').val($(this).attr('subCount'));
                $('#btnOk').attr('disabled', false);
            });
            tab.registDbClickListener(function () {
                btnok_click();
            });
            replaceEmptyTable('emptygvBudget', 'gvBudget');
            jw.formatTreegrid('gvBudget', 1);

            // 清单外红色显示
            $('#gvBudget tr[modifyType=0]').each(function () {
                $(this).css('color', 'red');
            });
        });

        function btnok_click() {
            if (typeof top.ui.callback == 'function') {
                var taskid = $('#hfldCheckedIds').val();
                var modifyType = $('#hfldModifyType').val();
                var subCount = $('#hfldSubCount').val();
                var mType = $('#hfldMtype').val();
                if (subCount != '0' && mType == '1') {
                    top.ui.alert('已经有子项的父项不能进行清单内变更。');
                    return false;
                }
                if (modifyType == '0' && mType!='') {
                    top.ui.alert('清单外的节点不允许再次变更。');
                    return false;
                }
                var ret = top.ui.callback({ taskId: taskid });
                if (ret == false) {
                    // 表示父页面对回传的值没有验证通过
                    return false;
                }
            }
            btncancel_click();
        }

        function btncancel_click() {
            var winNo = jw.getRequestParam('winNo');
            if (winNo) {
                top.ui.closeWin({ winNo: winNo });
            } else {
                top.ui.closeTab();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <div id="divBudget">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TaskId,OrderNumber,ModifyType,SubCount" runat="server">
<EmptyDataTemplate>
                    <table id="emptygvBudget" class="gvdata">
                        <tr class="header">
                            <th scope="col" style="width: 25px;">
                                序号
                            </th>
                            <th scope="col">
                                名称
                            </th>
                            <th scope="col">
                                编码
                            </th>
                            <th scope="col">
                                类型
                            </th>
                            <th scope="col">
                                单位
                            </th>
                            <th scope="col">
                                工程量
                            </th>
                            <th scope="col">
                                开始时间
                            </th>
                            <th scope="col">
                                结束时间
                            </th>
                            <th scope="col">
                                综合单价
                            </th>
                            <th scope="col">
                                小计
                            </th>
                            <th scope="col">
                                备注
                            </th>
                            <td scope="col" visible="false" runat="server">
                                排序
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# Eval("TaskName") %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
                            <%# Eval("TaskCode") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Eval("Unit") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# Eval("Quantity") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("StartDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("EndDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# Common2.FormatDecimal(Eval("UnitPrice")) %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# Common2.FormatDecimal(Eval("Total2")) %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                            <asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
                            <%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
    </div>
    <div id="divFooter2" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input type="button" id="btnOk" value="保存" onclick="btnok_click();" disabled="disabled" />
                    <input type="button" id="btnCancel" value="取消" onclick="btncancel_click();" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldModifyType" runat="server" />
    
    <asp:HiddenField ID="hfldSendCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    
    <asp:HiddenField ID="hfldOrderNumber" runat="server" />
    
    <asp:HiddenField ID="hfldSubCount" runat="server" />
    
    <asp:HiddenField ID="hfldMtype" runat="server" />
    </form>
</body>
</html>
