<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestPayment.aspx.cs" Inherits="Fund_RequestPayment_RequestPayment" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目请款</title>

    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="/Script/wf.js"></script>

    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setWidthHeight();
            replaceEmptyTable('emptyPayment', 'gvBudget');
            var jwTable = new JustWinTable('gvBudget');
            setButton(jwTable, 'btnDelete', 'btnUpdate', 'btnLook', 'hfldPurchaseChecked');
            if (!jwTable.table) return;
            setWfButtonState2(jwTable, 'WF_1');
        });
        function planEdit(action) {
            parent.desktop.RequestPaymentEdit = window;
            var prjId = document.getElementById('hfldPrjId').value;
            var id = document.getElementById('hfldPurchaseChecked').value;
            var prjname=document.getElementById('hfPrjName').value;
            var url = '/fund/RequestPayment/RequestPaymentEdit.aspx?prjId=' + prjId+'&id='+id;
            if(action=="add")
            {
                url = url + '&action=add';
                titleText = '请款计划新增';
            }else
            {
                url = url + '&action=edit';
                titleText = '请款计划编辑';
            }
            url = url + '&prjname='+escape(prjname) ;
            toolbox_oncommand(url, titleText);
        }
        function setWidthHeight() {
            $('#divBudget').height($(this).height()-25);
            $('#div_project').height($(this).height()-20);
        }
      
        //单选
        function checkedSingle(id) {
             if(id==null || id=='')
            {return ;}
            if (document.getElementById(id).childNodes[7].firstChild) {
                var state = document.getElementById(id).childNodes[7].firstChild.state;
                document.getElementById('btnLook').removeAttribute('disabled');
                if (state == '0' || state == '1') {
                   
                }
                else {
                
                }
            }
        }       
      
        //查看附件
        function queryAdjunct(path) {
            alert(path);
            showFiles(path, 'divOpenAdjunct');
        }
        //查看
        function openQuery() {
            var id = document.getElementById('hfldPurchaseChecked').value;
            if (id != "") {
                var url = '/fund/RequestPayment/RequestPaymentView.aspx?ic=' + id ;
                var title = "";
                // title = $("#" + id).find("td").eq(2).html();
                //  title = $("#lblTitle").html();
                title = "请款计划";
                parent.desktop.RequestPaymentEdit = window;
                title += '明细';
                toolbox_oncommand(url, title);
            }
        }
    </script>

   <script language="javascript">
        function hideleft() {
            window.document.getElementById("td_Left").style.display = "none";
            window.document.getElementById("imgleft").style.display = "none";
            window.document.getElementById("imgright").style.display = "";
        }
        function showleft() {
            window.document.getElementById("td_Left").style.display = "";
            window.document.getElementById("imgleft").style.display = "";
            window.document.getElementById("imgright").style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td id="td_Left" style="width: 195px; vertical-align: top; height: 100%;">
                                <div style="">
                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div id="div_project" style="width: 195px; overflow: auto; height: 100%;">
                                    <asp:TreeView ID="tvBudget" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="width: 5; background-image: url(../../SysFrame/skin4/fenge_bg.gif)">
                                <img id="imgleft" alt="" src="../../SysFrame/skin4/label_back.gif" width="5" height="10"
                                    onclick="hideleft()" />
                                <img id="imgright" alt="" src="../../SysFrame/skin4/label_forward.gif" width="5" height="10"
                                    style="display: none;" onclick="showleft()" />
                            </td>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table class="tab" style="height: 100%;">
                                    <tr id="header">
                                        <td>
                                            <asp:Label ID="lblTitle" Text="项目请款" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="divFooter" style="text-align: left;">
                                            <input type="button" value="新增" id="btnAdd" onclick="planEdit('add')" runat="server" />

                                            <input type="button" id="btnUpdate" value="编辑" disabled="true" onclick="planEdit('edit')" runat="server" />

                                            <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                                            <input type="button" value="查看" id="btnLook" disabled="disabled" onclick="openQuery()" />
                                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="092" BusiClass="001" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; height: 100%;">
                                            <div id="divBudget" style="overflow: hidden; height: 100%;">
                                                <div id="divDiaries" style="overflow: auto;">
                                                    <asp:GridView ID="gvBudget" AllowPaging="true" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" OnPageIndexChanging="gvBudget_PageIndexChanging" DataKeyNames="RPayMainId" runat="server">
<EmptyDataTemplate>
                                                            <table id="emptyPayment" class="tab">
                                                                <tr class="header">
                                                                    <th scope="col" style="width: 20px;">
                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                                                    </th>
                                                                    <th scope="col" style="width: 25px;">
                                                                        序号 </td>
                                                                    <th scope="col">
                                                                        编号
                                                                    </th>
                                                                    <th scope="col">
                                                                        请款人
                                                                    </th>
                                                                    <th scope="col">
                                                                        请款时间
                                                                    </th>
                                                                    <th scope="col">
                                                                        请款金额
                                                                    </th>
                                                                    <th scope="col">
                                                                        流程状态
                                                                    </th>
                                                                    <th scope="col">
                                                                        备注
                                                                    </th>
                                                                    <th scope="col">
                                                                        附件</td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                                                    <asp:CheckBox ID="cbBoxAll" runat="server" />
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <asp:CheckBox ID="cbBox" runat="server" />
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="编号">
<ItemTemplate>
                                                                    <span class="al" onclick="viewopen('RequestPaymentView.aspx?ic=<%# Eval("RPayMainId") %>')">
                                                                        <%# Eval("RPayCode") %>
                                                                    </span>
                                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="请款人" DataField="People" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="请款时间" DataField="RPayDate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="请款金额" DataField="Money" DataFormatString="{0:F2}" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
                                                                    <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="备注" DataField="Remark" /><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                                    <%# GetAnnx(Convert.ToString(Eval("RPayMainId"))) %>
                                                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfplantype" runat="server" />
    <asp:HiddenField ID="hfPrjName" runat="server" />
    <asp:HiddenField ID="hfldMonthPlanID" runat="server" />
    </form>
</body>
</html>
