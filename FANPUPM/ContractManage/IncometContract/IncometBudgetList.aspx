<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometBudgetList.aspx.cs" Inherits="ContractManage_IncometContract_IncometBudgetList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择任务</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
		$(document).ready(function () {
			showTooltip('tooltip', 10);
			var spType = getRequestParam('spType');
			var winTable = new JustWinTable('gvBudget');
			replaceEmptyTable('emptyBudget', 'gvBudget');         
			$('#gvBudget tr:gt(0)').live('click', function () {
				var resCount = $(this).attr('resCount');
				$('#hfldResCount').val(resCount);
				$('#hfldCheckedIds').val(this.id);
                $('#hfldModifyType').val(this.ModifyType);
				if (spType == 'in') {
					$('#btnSave').removeAttr('disabled');
				} else {
					if (resCount == 0) {
						$('#btnSave').removeAttr('disabled');
					} else {
						$('#btnSave').attr('disabled', 'disabled');
					}
				}
			});

			$('#btnCancel').click(function () {
				var winNumber = getRequestParam('winNo');
				top.ui.closeWin({ winNo: winNumber });
			});

			// 绑定双击事件
			winTable.registDbClickListener(save);

			jw.formatTreegrid('gvBudget', 1);
		});

		function save() {
			var spType = getRequestParam('spType');
			// 如果是清单外变更，并且该节点已经配置了资源，则不能被选中
			if (spType == 'out' && $('#hfldResCount').val() > 0) {
			    top.ui.alert('该节点配置了资源，不能再进行清单外变更');
			    return false;
			}
			if ($('#hfldModifyType').val() == '0') {
			    top.ui.alert('清单外变更不能再进行变更');
			    return false;
            }
			var taskId = $('#hfldCheckedIds').val();
			var winNumber = getRequestParam('winNo');
			if (typeof top.ui.callback == 'function') {
				top.ui.callback(taskId);
			}

			top.ui.closeWin({ winNo: winNumber });
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%; height: 430px; overflow: auto; float: left;">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber,ModifyType" runat="server">
<EmptyDataTemplate>
                    <table id="emptyBudget" class="gvdata">
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
                            <td id="Th1" scope="col" visible="false" runat="server">
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
                            <span class="tooltip" title='<%# Eval("TaskName") %>'>
                                <%# StringUtility.GetStr(Eval("TaskName").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("TaskCode") %>'>
                                <%# StringUtility.GetStr(Eval("TaskCode").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                            <%# Eval("TypeName") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Eval("Unit") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px">
<ItemTemplate>
                            <%# Eval("Quantity") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("StartDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("EndDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" /><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("Note") %>'>
                                <%# StringUtility.GetStr(Eval("Note").ToString()) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
                            <%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
        <div id="divFooter2" class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <input type="button" id="btnSave" disabled="disabled" value="保存" onclick="save();" />
                        <input type="button" id="btnCancel" value="取消" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <!-- 保存选中节点中绑定资源的数量-->
    <asp:HiddenField ID="hfldResCount" runat="server" />
    <asp:HiddenField ID="hfldModifyType" runat="server" />
    </form>
</body>
</html>
