<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ConstructTask.aspx.cs" Inherits="BudgetManage_Construct_ConstructMain" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectrestask_ascx" Src="~/StockManage/UserControl/SelectResTask.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量</title><link href="../../StockManage/Skins/jquery.wysiwyg.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/Budget/ConstructTask.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var gvTask = new JustWinTable('gvTask');
            $('#btnBindResTask').hide();
            setButton2(gvTask, 'btnDel', 'btnEdit', 'btnTemImport', 'hfldPurchaseChecked');
            $('#txtDate').attr('readonly', 'readonly');
        });
    
        //添加行进行显示资源信息
		var prevId;
		function showInfo(taskId,id) {
		    var tab_Resource = null;
		    $.ajax({
		        type: 'GET',
		        async: false,
		        url: '/BudgetManage/Handler/ReturnResource.ashx?' + new Date().getTime() + '&taskId=' + taskId + '&type=check',
		        success: function (data) {
		            tab_Resource = data;
		        }
		    });
		    var isDisplay = $('#' + id + '1').get(0);
		    if (isDisplay == undefined) {
		        $('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="13" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
		        if (prevId != undefined && prevId != id) {
		            $('#' + prevId + '1').remove();
		        }
		        prevId = id;
		    }
		    else {
		        $('#' + id + '1').remove();
		        isDisplay = undefined;
		    }
		}

        function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '')
                    document.getElementById(hdChkId).value = this.id;
                if (this.guid) {
                    document.getElementById(btnQuery).guid = this.guid;
                }
                var checkedChk = jwTable.getCheckedChk();
                document.getElementById(btnDel).removeAttribute('disabled');
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    var checkedChks = jwTable.getCheckedChk();
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }

                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();

                }
            });
        };

        function addTask() {
            document.getElementById('SelectResource1_btnSelectResource').onclick('MultiTask', document.getElementById('hfldPrjId').value);
        }

        function theMonenyChange(index) {
            tb = document.getElementById('gvTask');
            var cells = tb.rows[parseFloat(index) + 1].cells;
            var Quantity = parseFloat(cells[7].children[0].value);
            if (Quantity < 0) {
                alert("系统提示：\n\n今日完成量不能小于0!")
                cells[7].children[0].value = getFormat(0);
            } else {
                cells[7].children[0].value = getFormat(Quantity);
            }
        }

        function selectResource(consTaskId) {
            parent.desktop.ResourceList = window;
            var url = "/BudgetManage/Construct/ResourceList.aspx?consTaskId=" + consTaskId + "&type=" + document.getElementById("hfldAddOrEdit").value + "&prjId=" + document.getElementById("hfldPrjId").value + "&reportId=" + document.getElementById("hfldReportId").value;
            toolbox_oncommand(url, "资源清单");
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
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
        <table style="border: 0px; width: 100%; height: 100%;">
            <tr style="width: 100%; height: 5%;">
                <td style="text-align: left;" class="divFooter">
                    上报日期
                    <asp:TextBox ID="txtDate" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="divFooter" style="text-align: left;">
                    记录人<asp:TextBox ID="txtInputUser" Width="80px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td style="text-align: right;" class="divFooter">
                    <input id="Button2" type="button" value="选择任务" style="width: 80px;" onclick="addTask();" runat="server" />

                    <asp:Button Width="80px" ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDelete_Click" runat="server" />
                    <asp:Button ID="btnUpdate" Width="80px" Text="保存" OnClick="btnUpdate_Click" runat="server" />
                </td>
                <MyUserControl:stockmanage_usercontrol_selectrestask_ascx ID="SelectResource1" Text="增加资源" Width="75.0" ButtonId="btnBindResTask" TypeCode="0" runat="server" />
            </tr>
            <tr>
                <td colspan="3" style="width: 100%; vertical-align: top; height: 61%;">
                    <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
                        <asp:GridView ID="gvTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvTask_RowDataBound" DataKeyNames="TaskId,ConsTaskId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("ConsTaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="TaskCode" HeaderText="任务编码" ItemStyle-HorizontalAlign="Left" /><asp:TemplateField>
<HeaderTemplate>
                                        分项工作
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("TaskName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Quantity" HeaderText="总工作量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="SumAccountingQuantity" HeaderText="累计审核量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="SurplusQuantity" HeaderText="剩余工作量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        今日完成量
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:TextBox ID="txtCompleteQuantity" CssClass="decimal_input" Style="text-align: right;" Width="80px" onkeypress="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else this.t_value=this.value;if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onkeyup="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else {this.t_value=this.value;}if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onblur="if(!this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/)){this.value=this.o_value;}else{if(this.value.length==0){this.value='0';theMonenyChange(this.No);}if(this.value.match(/^\.\d+$/))this.value=0+this.value;if(this.value.match(/^\.$/)){this.value=0;}this.o_value=this.value;theMonenyChange(this.No);}" Text='<%# System.Convert.ToString(Eval("CompleteQuantity"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        形象进度
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:TextBox ID="txtWorkContent" Text='<%# System.Convert.ToString(Eval("WorkContent"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        选择
                                    </HeaderTemplate>

<ItemTemplate>
                                        <input id="Button1" type="button" value="选择资源" style="width: 80px;" onclick="selectResource('<%# Eval("ConsTaskId") %>');" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        图片
                                    </HeaderTemplate>

<ItemTemplate>
                                        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="fuPicture" Type="2" Name="图片" FileType="*.jpg;*.gif;*.png" Class="ConstructReport" RecordCode='<%# System.Convert.ToString(Eval("ConsTaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr style="height: 25%;">
                <td colspan="3" align="left">
                    <table cellspacing="0" cellpadding="0" border="1px" style="width: 100%; border-bottom: 0px;
                        border-right: 0px;">
                        <tr>
                            <td class="td-label">
                                附件:
                            </td>
                            <td class="td-content">
                                <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="ConstructReportAccessory" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                技术质量<br />
                                安全工作纪录:
                            </td>
                            <td class="td-content">
                                <textarea id="txtWorkCard" class="wysiwyg" cols="80" style="width: 100%;" runat="server"></textarea>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfldIsPostBack" runat="server" />
        <script type="text/javascript" src="../../StockManage/script/jquery.wysiwyg.js"></script>
        <script type="text/javascript">
            if ($('#hfldIsPostBack').val() != 'true') {
                (function ($) {
                    $('.wysiwyg').wysiwyg({
                        controls: {
                            strikeThrough: { visible: true },
                            underline: { visible: true },

                            separator00: { visible: true },

                            justifyLeft: { visible: true },
                            justifyCenter: { visible: true },
                            justifyRight: { visible: true },
                            justifyFull: { visible: true },

                            separator01: { visible: true },

                            indent: { visible: true },
                            outdent: { visible: true },

                            separator02: { visible: true },

                            subscript: { visible: true },
                            superscript: { visible: true },

                            separator03: { visible: true },

                            undo: { visible: false },
                            redo: { visible: false },

                            separator04: { visible: false },

                            insertOrderedList: { visible: true },
                            insertUnorderedList: { visible: true },
                            insertHorizontalRule: { visible: true },

                            h4mozilla: { visible: false && $.browser.mozilla, className: 'h4', command: 'heading', arguments: ['h4'], tags: ['h4'], tooltip: "Header 4" },
                            h5mozilla: { visible: false && $.browser.mozilla, className: 'h5', command: 'heading', arguments: ['h5'], tags: ['h5'], tooltip: "Header 5" },
                            h6mozilla: { visible: false && $.browser.mozilla, className: 'h6', command: 'heading', arguments: ['h6'], tags: ['h6'], tooltip: "Header 6" },

                            h4: { visible: false && !($.browser.mozilla), className: 'h4', command: 'formatBlock', arguments: ['<H4>'], tags: ['h4'], tooltip: "Header 4" },
                            h5: { visible: false && !($.browser.mozilla), className: 'h5', command: 'formatBlock', arguments: ['<H5>'], tags: ['h5'], tooltip: "Header 5" },
                            h6: { visible: false && !($.browser.mozilla), className: 'h6', command: 'formatBlock', arguments: ['<H6>'], tags: ['h6'], tooltip: "Header 6" },

                            separator05: { visible: false },
                            separator06: { visible: false },
                            separator07: { visible: false },

                            cut: { visible: false },
                            copy: { visible: false },
                            insertImage: { visible: false },
                            paste: { visible: false }
                        }
                    });
                })(jQuery);
            }
        </script>
    </div>  
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldReportId" runat="server" />
    
    <asp:Button ID="btnBindResTask" OnClick="btnBindResTask_Click" runat="server" />
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldAddOrEdit" runat="server" />
    <!-- 保存选择的资源-->
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    <div id="divSelectResource" title="选择资源" style="display: none">
        <iframe id="ifResouece" frameborder="0" width="100%" height="100%"></iframe>
    </div>
    <input type="button" id="btnBindResource" value="btnBindResource" style="display: none" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
