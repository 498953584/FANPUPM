<%@ Page Language="C#" AutoEventWireup="true" CodeFile="measurelist.aspx.cs" Inherits="MeasureList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>安全目标维护 </title>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="/StockManage/script/Config2S.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script language="javascript" type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable();
            var contractTable = new JustWinTable('GridView_goall');
            if (request("type") == "Edit") {
                setButton(contractTable, 'BtnDelete', 'BtnModify', 'BtnSee', 'hiden_i_id')
            }
        });
        function registerDeleteEvent() {
            var btnDelete = document.getElementById('BtnDelete');
            btnDelete.onclick = function () {
                if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
                    return false;
                }
            }
        }
        function OpType(caseType, Id) {
            var tem_hiden_PrjCode = $("#hiden_PrjCode").val();            //项目编号
            var tem_hiden_i_id = $("#hiden_i_id").val();                            //实体编号ID
            var tem_hiden_page_Type = "";   //页面请求类型 list 和 edit
            var url = "";
            if (Id != "") {
                tem_hiden_i_id = Id;
            }
            var titleStr = "安全目标方案";
            tem_hiden_page_Type = caseType;
            switch (caseType) {
                case "Query":
                    if (tem_hiden_PrjCode != "" && tem_hiden_i_id != "") {
                        url = "/EPC/QuaitySafety/SafetyMeasure/MeasureView.aspx?Flage=1&Type=Query&Code=" + tem_hiden_i_id + "&PrjCode=" + tem_hiden_PrjCode;
                    }
                    break;
                case "Update":
                    if (tem_hiden_PrjCode != "" && tem_hiden_i_id != "") {
                        url = "/EPC/QuaitySafety/SafetyMeasure/measureedit.aspx?Flage=1&Type=Update&Code=" + tem_hiden_i_id + "&PrjCode=" + tem_hiden_PrjCode;
                    }
                    break;
                case "Add":
                    if (tem_hiden_PrjCode != "") {
                        url = "/EPC/QuaitySafety/SafetyMeasure/measureedit.aspx?Flage=1&Type=Add&PrjCode=" + tem_hiden_PrjCode;
                    }
                    break;
            }
            if (url != "") {
                top.ui._measurelist = window;
                toolbox_oncommand(url, titleStr);
            }
        }
        function clickRow(obj, type) {
            document.getElementById('hiden_i_id').value = obj;
            if (type != "List") {
                document.getElementById('BtnDelete').disabled = false;
                document.getElementById('BtnModify').disabled = false;
            }
            document.getElementById('BtnSee').disabled = false;
        }
        function replaceEmptyTable() {
            //当数据量为空时，修改样式
            if (!document.getElementById('GridView_goall')) return;
            if (!document.getElementById('emptyContractType')) return;
            var gvwContractType = document.getElementById('GridView_goall');
            var emptyContractType = document.getElementById('emptyContractType');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }
        $(function () {
            registerDeleteEvent();
            $("#BtnAdd").click(function () {
                OpType("Add", "");
            });
        });

        $(function () {
            if (request("Type").toUpperCase() == "LIST") {
                $("#ba").find("th").eq(0).hide();
                $(".divFooter").hide();
                //$("#header").hide();
            }
            $("#Button1").hide();
            $("#GridView_goall tr").each(function () {
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
        function clickRow(obj, type, sc) {
            document.getElementById('hiden_i_id').value = obj;
            if (type != "List") {
                document.getElementById('BtnDelete').disabled = false;
                document.getElementById('BtnModify').disabled = false;
            }
            document.getElementById('BtnSee').disabled = false;

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
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr id="header" class="divHeader">
            <td style="height: 28px;">
                <asp:Label ID="lbltitle" Text="Label" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            工程名称
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            安全目标
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left">
                <input id="BtnAdd" type="button" value="新增" runat="server" />

                <input id="BtnModify" type="button" value="编辑" disabled="true" runat="server" />

                <asp:Button ID="BtnDelete" CssClass="" Text="删除" Enabled="false" OnClick="BtnDelete_Click" runat="server" />
                <input id="BtnSee" type="button" value="查看" disabled="true" runat="server" />

                <asp:Button ID="btnFiles" Text="归档" OnClick="btnFiles_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2" height="100%">
                <div class="gridBox">
                    <asp:GridView ID="GridView_goall" PageSize="20" Width="100%" AllowPaging="true" CssClass="gvdata" AutoGenerateColumns="false" OnPageIndexChanging="GridView_goall_PageIndexChanging" OnRowDataBound="GridView_goall_RowDataBound" DataKeyNames="i_id" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContractType" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" disabled="disabled" />
                                    </th>
                                    <th scope="col" style="width: 20px;">
                                        <img alt="" src="/images/over.gif" width="12px" height="12px" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        工程名称
                                    </th>
                                    <th scope="col">
                                        安全目标
                                    </th>
                                    <th scope="col">
                                        备 注
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:Label ID="Label3" Text='<%# Convert.ToString(Eval("i_id")) %>' runat="server"></asp:Label>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:Image ID="Image1" ImageUrl="~/images/over.gif" Width="12px" Height="12px" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程名称"><ItemTemplate>
                                    <asp:Label CssClass="link" ID="lblTaskName" Text='<%# Convert.ToString(Eval("TaskName")) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="SaftyMeasure" HeaderText="安全目标" /><asp:BoundField DataField="Remark" HeaderText="备  注" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="hiden_PrjCode" runat="server" />
    
    <asp:HiddenField ID="hiden_i_id" runat="server" />
    
    <asp:HiddenField ID="hiden_page_Type" runat="server" />
    </form>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
