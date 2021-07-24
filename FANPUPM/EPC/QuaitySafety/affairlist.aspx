<%@ Page Language="C#" AutoEventWireup="true" CodeFile="affairlist.aspx.cs" Inherits="AffairList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>质量事务记录</title>
    <script src="../../Script/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var purchasePlanTable = new JustWinTable('GridView1');
            replaceEmptyTable('emptyTable', 'GridView1');
            //		        if (request("Type") == "Edit") {
            //		            setButton(purchasePlanTable, 'BtnDelete', 'BtnModify', 'BtnSee', 'Hidden1')
            //		        }
            // 行单击事件
            purchasePlanTable.registClickTrListener(function () {
                var mark = $(this).attr('mark');
                //  1:已归档  2：未设置为归档资料  3：设置为归档资料
                if (mark != "undefined" && mark != "" && mark != null) {
                    if (mark == '2') {
                        $('#BtnModify').removeAttr('disabled');
                        $('#BtnDelete').removeAttr('disabled');
                        $('#btnFiles').attr('disabled', 'disabled');
                    } else if (mark == '3') {
                        $('#BtnModify').removeAttr('disabled');
                        $('#BtnDelete').removeAttr('disabled');
                        $('#btnFiles').removeAttr('disabled');
                    } else if (mark == '1') {
                        $('#BtnModify').attr('disabled', 'disabled');
                        $('#BtnDelete').attr('disabled', 'disabled');
                        $('#btnFiles').attr('disabled', 'disabled');
                    } else {
                        $('#BtnModify').attr('disabled', 'disabled');
                        $('#BtnDelete').attr('disabled', 'disabled');
                        $('#btnFiles').attr('disabled', 'disabled');
                    }
                } else {
                    $('#BtnModify').attr('disabled', 'disabled');
                    $('#BtnDelete').attr('disabled', 'disabled');
                    $('#btnFiles').attr('disabled', 'disabled');
                    $('#BtnSee').attr('disabled', 'disabled');
                }
                $('#Hidden1').val($(this).attr('id'));
            });
            // 复选框的单击事件
            purchasePlanTable.registClickSingleCHKListener(function () {
                var checks = purchasePlanTable.getCheckedChk();
                if (checks.length != 1) {
                    $('#BtnModify').attr('disabled', 'disabled');
                    $('#BtnDelete').attr('disabled', 'disabled');
                    $('#btnFiles').attr('disabled', 'disabled');
                    $('#BtnSee').attr('disabled', 'disabled');
                    return;
                } else {
                    var trEle = getFirstParentElement(checks[0], 'tr');
                    var mark = trEle.mark;
                    $('#Hidden1').val(trEle.id);
                    //  1:已归档  2：未设置为归档资料  3：设置为归档资料
                    if (mark != "undefined" && mark != "" && mark != null) {
                        if (mark == '2') {
                            $('#BtnModify').removeAttr('disabled');
                            $('#BtnDelete').removeAttr('disabled');
                            $('#btnFiles').attr('disabled', 'disabled');
                            $('#BtnSee').removeAttr('disabled');
                        } else if (mark == '3') {
                            $('#BtnModify').removeAttr('disabled');
                            $('#BtnDelete').removeAttr('disabled');
                            $('#btnFiles').removeAttr('disabled');
                            $('#BtnSee').removeAttr('disabled');
                        } else if (mark == '1') {
                            $('#BtnModify').attr('disabled', 'disabled');
                            $('#BtnDelete').attr('disabled', 'disabled');
                            $('#btnFiles').attr('disabled', 'disabled');
                            $('#BtnSee').removeAttr('disabled');
                        } else {
                            $('#BtnModify').attr('disabled', 'disabled');
                            $('#BtnDelete').attr('disabled', 'disabled');
                            $('#btnFiles').attr('disabled', 'disabled');
                            $('#BtnSee').attr('disabled', 'disabled');
                        }
                    }
                }
            });
        });
        function OpType(obj, PrjCode) {
            var _type = document.getElementById("QS").value;
            var Id = document.getElementById('Hidden1').value;
            var Class = document.getElementById('hdnClass').value;
            var url;
            var refresh = false;
            var type = obj;
            var titleStr = "";
            if (_type == "S") {
                titleStr = "安全事务";
            } else {
                titleStr = "质量事务";
            }
            switch (type) {
                case "EDIT":
                    url = "/EPC/QuaitySafety/AffairEdit.aspx?Flag=" + _type + "&Type=EDIT&i_id=" + Id + "&DatumClass=" + Class + "&PrjCode=" + PrjCode;
                    break;
                case "ADD":
                    url = "/EPC/QuaitySafety/AffairEdit.aspx?Flag=" + _type + "&Type=Add&DatumClass=" + Class + "&PrjCode=" + PrjCode;
                    break;
                case "SEE":
                    url = "/EPC/QuaitySafety/affairview.aspx?Flag=" + _type + "&Type=SEE&i_id=" + Id + "&DatumClass=" + Class + "&PrjCode=" + PrjCode;
                    break;
            }
            viewopen_n(url, titleStr);
        }
        function viewopen_n(url, titleStr) {
            if (url != "") {
                top.ui._quaitysafetyaffairlist = window;
                toolbox_oncommand(url, titleStr);
            }
        }
        function replaceEmptyTable() {
            //当数据量为空时，修改样式
            if (!document.getElementById('GridView1')) return;
            if (!document.getElementById('emptyTable')) return;
            var gvwContractType = document.getElementById('GridView1');
            var emptyContractType = document.getElementById('emptyTable');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="divHeader" colspan="2" style="height: 28px;">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="" style="text-align: left; width: 100%; padding-left: 5px; padding-top: 5px;
                padding-bottom: 5px;" colspan="2">
                <asp:DropDownList ID="DDLLookup" Width="120px" AutoPostBack="true" runat="server"><asp:ListItem Value="1" Text="事务名称" /><asp:ListItem Value="2" Text="事务内容" /><asp:ListItem Value="3" Text="备注" /></asp:DropDownList>
                <asp:TextBox ID="TxtLookup" Width="120px" CssClass="" runat="server"></asp:TextBox>
                <asp:Button ID="btn_Search" Text="查询" OnClick="btn_Search_Click" runat="server" />
                &nbsp;&nbsp;
                <input id="Hidden1" type="hidden" size="1" name="Hidden1" runat="server" />

            </td>
        </tr>
        <tr id="TR_Btn" runat="server"><td id="Td_Btn" class="divFooter" style="text-align: left; width: 100%" runat="server">
                <font face="宋体"></font>
                <asp:Button ID="BtnAdd" CssClass="" Text="新  增" OnClick="BtnAdd_Click" runat="server" />&nbsp;
                <asp:Button ID="BtnModify" CssClass="" Text="编  辑" DESIGNTIMEDRAGDROP="149" Enabled="false" OnClick="BtnModify_Click" runat="server" />&nbsp;
                <asp:Button ID="BtnDelete" CssClass="" Text="删  除" Enabled="false" OnClick="BtnDelete_Click" runat="server" />&nbsp;
                <asp:Button ID="BtnSee" CssClass="" Text="查  看" Enabled="false" runat="server" />
                &nbsp;
                <asp:Button ID="btnFiles" Text="归档" OnClick="btnFiles_Click" runat="server" />
            </td></tr>
        <tr>
            <td valign="top" colspan="2" height="100%">
                <div class="gridBox">
                    <asp:GridView ID="GridView1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" DataKeyField="i_id" AllowPaging="true" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="i_id" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable" border="0" cellspacing="0" cellpadding="0" width="100%" class="gvdata">
                                <tr class="header" id="ba">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chkAll" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 20px;">
                                        
                                    </th>
                                    <th scope="col" style="width: 30px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        <%=AffairList.listTitleStr %>
                                    </th>
                                    <th scope="col">
                                        发生日期
                                    </th>
                                    <th scope="col">
                                        内容
                                    </th>
                                    <th scope="col">
                                        备注
                                    </th>
                                    <th scope="col" style="width: 17px;">
                                        附件
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                                    <input id="chkAll" type="checkbox" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:Label ID="Label3" Text='<%# Convert.ToString(Eval("i_id")) %>' runat="server"></asp:Label>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText=""><ItemTemplate>
                                    <asp:Label ID="Label1" class="link" Text="Label" runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:BoundField ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" DataField="AddDate" HeaderText="发生日期" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField DataField="Context" ItemStyle-Width="30%" HeaderText="内容" /><asp:TemplateField HeaderText="备注" ItemStyle-Width="20%"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("Remark").ToString(), 40) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                    <%# GetAnnx(Convert.ToString(Eval("i_id"))) %>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><HeaderStyle CssClass="header"></HeaderStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><FooterStyle CssClass="footer"></FooterStyle><PagerStyle CssClass="GD-Pager"></PagerStyle></asp:GridView>
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                </div>
                <input id="hdnClass" type="hidden" runat="server" />

            </td>
        </tr>
    </table>
    <asp:HiddenField ID="QS" runat="server" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        if (request("Type").toUpperCase() == "LIST") {
            $("#ba").find("th").eq(0).hide();
            $("#TR_Btn").hide();
        }
        $("#GridView1 tr").each(function () {
            var _markTem = $(this).attr("mark");
            if (_markTem != "undefined" && _markTem != "" && _markTem != null) {
                if (request("Type").toUpperCase() == "LIST") {
                    if (_markTem == "1") {
                        $(this).find('td').eq(0).find("img").attr("src", "/images/over.gif");
                    } else
                        if (_markTem == "2") {
                            $(this).find('td').eq(0).find("img").attr("src", "/images/Edit.gif");
                        } else
                            if (_markTem == "3") {
                                $(this).find('td').eq(0).find("img").attr("src", "/images/Process.gif");
                            }
                } else {
                    if (_markTem == "1") {
                        $(this).find('td').eq(1).find("img").attr("src", "/images/over.gif");
                    } else
                        if (_markTem == "2") {
                            $(this).find('td').eq(1).find("img").attr("src", "/images/Edit.gif");
                        } else
                            if (_markTem == "3") {
                                $(this).find('td').eq(1).find("img").attr("src", "/images/Process.gif");
                            }
                }
            }
        });
    });

    function clickRow(sc) {
    }

    function clickRow(obj, type) {
        document.getElementById('Hidden1').value = obj;
        if (document.getElementById('BtnSee') != null) {
            document.getElementById('BtnSee').disabled = false;
        }
        if (type != "List") {
            if (document.getElementById('BtnDelete') != null) {
                document.getElementById('BtnDelete').disabled = false;
            }
            if (document.getElementById('BtnModify') != null) {
                document.getElementById('BtnModify').disabled = false;
            }
        }
    }
    function request(paras) {
        var url = location.href;
        var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
        var paraObj = {}
        for (i = 0; j = paraString[i]; i++) {
            paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
        }
        var returnValue = paraObj[paras.toLowerCase()];
        if (typeof (returnValue) == "undefined") {
            return "";
        } else {
            return returnValue;
        }
    }
</script>
