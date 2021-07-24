<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjLoan_Repayment.aspx.cs" Inherits="PrjLoan_Repayment" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金管理-项目借款</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>    
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>          
    <script type="text/javascript">
        addEvent(window, 'load', function() {
            setWidthHeight();
            var jwTable = new JustWinTable('gvBudget');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hdnLoanID');
            document.getElementById('btnRepayment').disabled=true;
            if (!jwTable.table) return;
            setWfButtonState2(jwTable, 'WF1');
            deleteClick();
            replaceEmptyTable('emptyContractType', 'gvBudget');
        });
        //删除前提示
        function deleteClick() {
            var btnDelete = document.getElementById('btnDel');
            btnDelete.onclick = function() {
                if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
                    return false;
                }
            }
        }       
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 25);
            $('#div_project').height($(this).height() - 20);
        }
        //点击行赋值
        function clickRows(temAID,ReturnState, temtr) {
          document.getElementById('btnRepayment').disabled=false;
            if (temAID != "") {
                $("#hdnLoanID").val(temAID); 
                if(ReturnState=="1")
                {   document.getElementById('btnRepayment').disabled=true; }                     
            }
        }
        //编辑
        function btnEdit_onclick() {
            parent.parent.desktop.flowclass = window;
            var titleText = '查看借款信息';
            var accid = $("#hdnLoanID").val();
            var _PrjId = $("#hfldPrjId").val();
            var prjname = $("#hfPrjName").val();
            if (accid != "") {
                url = "/Fund/PrjLoan/PrjLoanEdit.aspx?Action=edit&ic=" + accid + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
                toolbox_oncommand(url, titleText);
            }
        }
        //查看
        function btnQuery_onclick() {
            parent.parent.desktop.flowclass = window;
            var titleText = '查看借款信息';
            var accid = $("#hdnLoanID").val();
            var _PrjId = $("#hfldPrjId").val();
            var prjname = $("#hfPrjName").val();
            if (accid != "") {
                url = "/Fund/PrjLoan/PrjLoanView.aspx?Action=query&ic=" + accid + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
                toolbox_oncommand(url, titleText);
            }
        }

        function shoView(ID) {
            parent.parent.desktop.flowclass = window;
            var titleText = '查看借款信息';
            var accid = $("#hdnLoanID").val();
            var _PrjId = $("#hfldPrjId").val();
            var prjname = $("#hfPrjName").val();
            if (accid != "") {
                url = "/Fund/PrjLoan/PrjLoanView.aspx?Action=query&ic=" + ID + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
                toolbox_oncommand(url, titleText);
            }
        }
        //新增
        function btnAdd_onclick() {
            parent.parent.desktop.flowclass = window;
            var titleText = '新增项目借款';
            var _PrjId = $("#hfldPrjId").val();
            var prjname = $("#hfPrjName").val();
            if (_PrjId != "") {
                url = "/Fund/PrjLoan/PrjLoanEdit.aspx?Action=add&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
                toolbox_oncommand(url, titleText);
            }
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">    
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="width: 100%; height: 100%; ">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>                            
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">                     
                                <table class="tab" style="vertical-align: top;">
                                    <tr id="trloan" style="display:none" runat="server"><td class="divFooter" style="text-align: left;" runat="server">
                                            <input type="button" value="新增" id="btnAdd" onclick="return btnAdd_onclick()" runat="server" />

                                            <input id="btnEdit" type="button" disabled="true" value="编辑" onclick="return btnEdit_onclick()" runat="server" />

                                            <asp:Button ID="btnDel" Text="删除" Enabled="false" runat="server" />

                                             <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="095" runat="server" />
                                        </td></tr>
                                      
                                    <tr style="vertical-align: top;">
                                        <td class="divFooter" style="text-align: left;">
                                          <input type="button" id="btnQuery" disabled="disable`d" value="查看" onclick="return btnQuery_onclick()" /><asp:Button ID="btnRepayment" Text="归还" disabled="disabled" OnClick="btnRepayment_Click" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; height: 100%;">
                                            <div id="divBudget" style="overflow: hidden;">
                                                <div id="divDiaries" style="overflow: auto;">
                                                    <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="LoanID" runat="server">
<EmptyDataTemplate>
                                                            <table id="emptyContractType" class="gvdata" width="100%" border="0">
                                                                <tr class="header">
                                                                <th scope="col">
                                                                    <input id="Checkbox1" type="checkbox" />
                                                                </th>
                                                                    <th scope="col">
                                                                        序号
                                                                    </th>
                                                                   <th scope="col">
                                                                        项目
                                                                    </th>
                                                                    <th scope="col">
                                                                        借款日期
                                                                    </th>
                                                                    <th scope="col">
                                                                        借款人
                                                                    </th>
                                                                    <th scope="col">
                                                                        借款用途
                                                                    </th>
                                                                    <th scope="col">
                                                                        借款金额
                                                                    </th>
                                                                    <th scope="col">
                                                                        借款利率
                                                                    </th>
                                                                    <th scope="col">
                                                                        约定归还日期
                                                                    </th>
                                                                    <th scope="col">
                                                                        流程状态
                                                                    </th>
                                                                    <th scope="col">
                                                                        备注
                                                                    </th>
                                                                    <th scope="col">
                                                                        附件
                                                                    </th>                                                                    
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" />
                                                            </HeaderTemplate><ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                                 <%# Eval("PrjName") %>                                                                    
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款单号"><ItemTemplate>
                                                                <asp:Label ID="labLoanCode" CssClass="link" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LoanCode")) %>' runat="server"></asp:Label>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款用途"><ItemTemplate>
                                                                    <%# Eval("LoanCause") %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                                 <%# DataBinder.Eval(Container.DataItem, "LoanDate", "{0:yyyy-MM-dd}") %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款人"><ItemTemplate>
                                                                   <%# Eval("LoanManName") %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>                                                               
                                                                    <%# DataBinder.Eval(Container.DataItem, "LoanFund", "{0:F2}") %>
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款利率" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "LoanRate", "{0:P1}") %>                                                                   
                                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="约定归还日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "PlanReturnDate", "{0:yyyy-MM-dd}") %>
                                                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="距离到期天数" DataField="syday" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="归还状态" DataField="ReturnFlowState" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="归还经手人" DataField="ReturntellerName" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="归还日期" DataFormatString="{0:yyyy-MM-dd}" DataField="ReturnDate" ItemStyle-HorizontalAlign="Center" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>        
   <!-- 当前项目的GUID -->
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <!-- 选择当前对象的ID -->
    <asp:HiddenField ID="hdnLoanID" runat="server" />
    <!-- 当前项目 -->
    <asp:HiddenField ID="hfPrjName" runat="server" />
    
    </form>
</body>
</html>
