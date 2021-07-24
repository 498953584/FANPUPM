<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuperResult.aspx.cs" Inherits="EPC_SupplierGrade_SuperResult" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>供应商综合信誉</title>

    <script src="../../Script/jquery.js" type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>

    <script src="../../Script/jquery.ui/jquery.ui.core.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript">
        window.onload = function() {
        var purchasePlanTable = new JustWinTable('gvpf');
    }
        
        function check(obj) {
            if (document.getElementById('radls').select) {

                if (tp.style.display == "") {
                    tp.style.display = 'none';
                }
            }
        }
        function showyear() {
            if (document.getElementById('radls').select) {
                if (tp.style.display == 'none') {
                    tp.style.display = ""
                }
            }

        }
        function rowQuery() {
            var url = "ViewSendNote.aspx?t=1&ic=" + document.getElementById("hfldPurchaseChecked").value;
            viewopen(url);
        }

        function ClickRow(gys) {
            //alert(gys);
            document.getElementById('hidysid').value = gys;
       
          
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent" style="height: 100%">
        <table border="1" style="border-collapse: collapse; width: 100%">
            <tr>
                <td class="divHeader">
                    供应商资信评估查看
                </td>
            </tr>
            <tr>
                <td align="left">
                    <input id="radls" type="radio" value="0" onclick="check(radls)" checked="true" name="kptype" runat="server" />
历史评估
                    <input id="radyear" type="radio" value="1" onclick="showyear()" name="kptype" />年度评估
                    <div id="tp" style="display: none; float: left">
                        <asp:DropDownList ID="dply" runat="server"><asp:ListItem Text="2010" /><asp:ListItem Text="2011" /><asp:ListItem Text="2012" /><asp:ListItem Text="2013" /><asp:ListItem Text="2014" /><asp:ListItem Text="2015" /><asp:ListItem Text="2016" /><asp:ListItem Text="2017" /><asp:ListItem Text="2018" /><asp:ListItem Text="2019" /><asp:ListItem Text="2020" /></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnOK" Text="确  定" OnClick="btnOK_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="pagediv">
                        <asp:GridView ID="gvpf" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" AllowPaging="true" AllowSorting="true" PageSize="6" OnPageIndexChanging="gvpf_PageIndexChanging" OnRowDataBound="gvpf_RowDataBound" DataKeyNames="superid" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="供应商编号" DataField="superid" /><asp:BoundField HeaderText="供应商名称" DataField="CorpName" /><asp:BoundField HeaderText="评估次数" DataField="khnum" /><asp:BoundField HeaderText="综合得分" DataField="allpr" /><asp:BoundField HeaderText="最终得分" DataField="pj" /><asp:TemplateField><ItemTemplate>
                                        <a href='SuperResultDetail.aspx?spid=<%# Eval("superid") %>'>详情</a>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="divHeader">
                    资信评估图
                </td>
            </tr>
            <tr>
                <td align="left">
               
                  
                    <input id="hidysid" type="hidden" runat="server" />

                    <asp:Button ID="btnview" Text="生成图形" Width="80px" OnClick="btnview_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div>
                        <asp:Chart ID="Chart1" BackImageAlignment="Center" runat="server"><Titles /><Series><asp:Series Name="Series1"></asp:Series></Series><ChartAreas /></asp:Chart>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; height: 100%" class="divFooter">
                    <asp:Button ID="btnClose" Text="关  闭" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
