<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectSupervise.aspx.cs" Inherits="ProjectSupervise" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//Dtd html 4.0 transitional//EN" >
<html>
<head id="Head1" runat="server"><title>
        <%=this.strTitle %></title>
    <script src="../../Script/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            replaceEmptyTable();
            var GridView1Table = new JustWinTable('GridView1');
            // 行的单击事件
            GridView1Table.registClickTrListener(function () {
                //  1:已归档  2：未设置为归档资料  3：设置为归档资料
                var mark = $(this).attr('mark');
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
                $('#Hidden1').val($(this).attr('id'));
            });
            // 复选框的点击事件
            GridView1Table.registClickSingleCHKListener(function () {
                var checks = GridView1Table.getCheckedChk();
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
            //		        var ve = document.getElementById("EDIT").value;
            //		        if (ve == "Edit") {
            //		            setButton(GridView1Table, 'BtnDelete', 'BtnModify', 'BtnSee', 'Hidden1')
            //		        }
        }
        function OpType(obj, PrjCode, Id) {
            var _type = document.getElementById("QS").value;
            if (Id == "") {
                Id = document.getElementById('Hidden1').value;
            }
            var Class = document.getElementById('hdnClass').value;
            var CA = document.getElementById('hiden_CA').value;
            var url = "";
            var refresh = false;
            var type = obj;
            var titleStr = "";
            if (_type == "S") {
                if (CA == 5) {
                    titleStr = "安全事故报告";
                }
                if (CA == 6) {
                    titleStr = "安全措施";
                }
                if (CA == 4) {
                    titleStr = "安全检查记录";
                }
            } else {
                if (CA == 2) {
                    titleStr = "质量事故报告";
                }
                if (CA == 1) {
                    titleStr = "质量检查记录";
                }
                if (CA == 3) {
                    titleStr = "工程质量竣工验收";
                }
            }
            switch (type) {
                case "EDIT":
                    url = "/EPC/QuaitySafety/ProjectSuperviseEdit.aspx?Flag=" + _type + "&CA=" + CA + "&Type=EDIT&i_id=" + Id + "&DatumClass=" + Class + "&PrjCode=" + PrjCode;
                    break;
                case "ADD":
                    url = "/EPC/QuaitySafety/ProjectSuperviseEdit.aspx?Flag=" + _type + "&CA=" + CA + "&Type=Add&DatumClass=" + Class + "&PrjCode=" + PrjCode;
                    break;
                case "SEE":
                    url = "/EPC/QuaitySafety/ProjectSuperviseView.aspx?Flag=" + _type + "&CA=" + CA + "&Type=SEE&i_id=" + Id + "&DatumClass=" + Class + "&PrjCode=" + PrjCode;
                    break;
            }
            if (url != "") {
                viewopen_n(url, titleStr);
            }
        }
        function viewopen_n(url, titleStr) {
            if (url != "") {
                top.ui._ProjectSupervise = window;
                toolbox_oncommand(url, titleStr);
            }
        }

        function replaceEmptyTable() {
            //当数据量为空时，修改样式
            if (!document.getElementById('GridView1')) return;
            if (!document.getElementById('emptyContractType')) return;
            var gvwContractType = document.getElementById('GridView1');
            var emptyContractType = document.getElementById('emptyContractType');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="EDIT" runat="server" />
    <asp:HiddenField ID="hiden_CA" runat="server" />
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="divHeader" colspan="2" style="height: 28px;">
                <%=this.strTitle %>
            </td>
        </tr>
        <tr>
            <td class="" style="text-align: left; width: 100%; padding-left: 5px; padding-top: 5px;
                padding-bottom: 5px;" colspan="2">
                <asp:DropDownList ID="DDLLookup" Width="120px" AutoPostBack="true" runat="server"><asp:ListItem Value="1" Text="名称" /><asp:ListItem Value="2" Text="内容" /><asp:ListItem Value="3" Text="备注" /></asp:DropDownList>
                <asp:TextBox ID="TxtLookup" Width="120px" CssClass="" runat="server"></asp:TextBox>
                <asp:Button ID="btn_Search" Text="查询" OnClick="btn_Search_Click" runat="server" />
                &nbsp;&nbsp;
                <input id="Hidden1" type="hidden" size="1" name="Hidden1" runat="server" />

            </td>
        </tr>
        <tr>
            <td id="td_Btn" class="divFooter" style="text-align: left; width: 100%" runat="server">
                <asp:Button ID="BtnAdd" CssClass="" Text="新增" OnClick="BtnAdd_Click" runat="server" />&nbsp;
                <asp:Button ID="BtnModify" CssClass="" Text="编辑" DESIGNTIMEDRAGDROP="149" Enabled="false" OnClick="BtnModify_Click" runat="server" />
                <input id="Button1" type="button" value="编辑" />
                &nbsp;
                <asp:Button ID="BtnDelete" CssClass="" Text="删除" Enabled="false" OnClick="BtnDelete_Click" runat="server" />&nbsp;
                <asp:Button ID="BtnSee" CssClass="" Text="查看" disabled="disabled" runat="server" />
                &nbsp;
                <asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2" height="100%">
                <div class="gridBox">
                    <asp:GridView ID="GridView1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" DataKeyField="i_id" AllowPaging="true" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="i_id" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContractType" class="gvdata">
                                <tr class="header" id="ba">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" disabled="disabled" />
                                    </th>
                                    <th scope="col" style="width: 20px;">
                                    </th>
                                    <th scope="col" style="width: 30px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        <%=this.listTitleStr %>
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
</asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px"><HeaderTemplate>
                                    
                                </HeaderTemplate><ItemTemplate>
                                    <asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="" ItemStyle-Width="30%">
<ItemTemplate>
                                    <asp:Label ID="Label1" class="link" Text="Label" runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" DataField="AddDate" HeaderText="发生日期" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField DataField="Context" ItemStyle-Width="30%" HeaderText="内容" /><asp:TemplateField ItemStyle-Width="20%" HeaderText="备注"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("Remark").ToString(), 40) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                    <%# GetAnnx(Convert.ToString(Eval("i_id"))) %>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><HeaderStyle CssClass="header"></HeaderStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    var img_src = "EPC/QuaitySafety/";
    $(function () {
        if (request("Type").toUpperCase() == "LIST") {
            $("#ba").find("th").eq(0).hide();
            $("#td_Btn").hide();
        }
        $("#Button1").hide();
        $("#GridView1 tr").each(function () {
            var _markTem = $(this).attr("mark");
            if (_markTem != "undefined" && _markTem != "" && _markTem != null) {
                if (request("Type").toUpperCase() == "LIST") {
                    if (_markTem == "1") {
                        $(this).find('td').eq(0).find("img").attr("src", "/images/over.gif");
                    } else if (_markTem == "2") {
                        $(this).find('td').eq(0).find("img").attr("src", "/images/Edit.gif");
                    } else if (_markTem == "3") {
                        $(this).find('td').eq(0).find("img").attr("src", "/images/Process.gif");
                    }
                } else {
                    if (_markTem == "1") {
                        $(this).find('td').eq(1).find("img").attr("src", "/images/over.gif");
                    } else if (_markTem == "2") {
                        $(this).find('td').eq(1).find("img").attr("src", "/images/Edit.gif");
                    } else if (_markTem == "3") {
                        $(this).find('td').eq(1).find("img").attr("src", "/images/Process.gif");
                    }
                }
            }
        });
    });


    function clickRow(sc) {
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
