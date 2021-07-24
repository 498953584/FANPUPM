<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologystandardquery.aspx.cs" Inherits="TechnologyStandardQuery" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>TechnologyStandardQuery</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2S.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script language="javascript">
        window.onload = function () {
            var purchasePlanTable = new JustWinTable('DGrdStandard');
            setButton(purchasePlanTable, 'BtnDel', 'BtnUpd', 'BtnView', 'HdnId');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
        }
        function rowQuery(id) {
            var projectCode = document.getElementById("HdnPrjCode").value;
            var prjName = document.getElementById("HdnPrjName").value;
            var url = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardEdit.aspx?Type=View&Id=" + id + "&pc=" + projectCode + "&pn=" + prjName;
            parent.parent.desktop.AddIncometBalance = window;
            toolbox_oncommand(url, "查看技术标准台账");
        }
        function CanEdit(bool) {
            if (document.getElementById("BtnUpd") != null)
                document.getElementById("BtnUpd").disabled = bool;
        }
        function openEdit(opType) {
            var projectCode = document.getElementById("HdnPrjCode").value;
            var prjName = document.getElementById("HdnPrjName").value;
            var Id = document.getElementById("HdnId").value;
            var url;
            var title;
            switch (opType) {
                case 'Add':
                    url = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardEdit.aspx?Type=Add&pc=" + projectCode + "&Id=" + Id + "&pn=" + prjName;
                    title = "新增技术标准台账";
                    break;
                case 'Upd':
                    url = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardEdit.aspx?Type=Upd&Id=" + Id + "&pc=" + projectCode + "&pn=" + prjName;
                    title = "编辑技术标准台账";
                    break;
                case 'View':
                    url = "/EPC/17/Ppm/ScienceInnovate/TechnologyStandardEdit.aspx?Type=View&Id=" + Id + "&pc=" + projectCode + "&pn=" + prjName;
                    title = "查看技术标准台账";
                    break;
            }
            top.ui._technologystandardquery = window; //不可少
            toolbox_oncommand(url, title); //引用js
            //return window.showModalDialog(url,window,'dialogHeight:250px;dialogWidth:500px;center:1;help:0;status:0;');
        }
        function StandardSelect() {
            var projectCode = document.getElementById("HdnPrjCode").value;
            var url;
            url = "../../Entpm/ScienceInnovate/EntStandardFrame.aspx?type=2&PrjCode=" + projectCode;
            return window.showModalDialog(url, window, 'dialogHeight:375px;dialogWidth:600px;center:1;help:0;status:0;');
        }
        function ShowAnnex(RecordCode) {
            window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1721&rc=1721" + RecordCode + "&at=0&type=2", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
        }
        //			$(function() {
        //			    $(".gvdata tr").each(function(i) {
        //			        var _tr = $(this);
        //			        $(_tr).find("td").eq(0).children().click(function() {
        //			            //alert("找到第 " + i + " 个TD的input");
        //			            var tem = $(_tr).find("td").eq(0).children().attr("checked");
        //			            $(_tr).find("td").last().children().attr("checked", tem);
        //			        });
        //			    });
        //			});
    </script>
    <style type="text/css">
        .dgheader
        {
            background-color: #EEF2F5;
            white-space: nowrap;
            overflow: hidden;
            font-weight: normal;
            text-align: center;
            border-color: #CADEED;
            color: #727FAA;
            padding-left: 6px;
            padding-right: 6px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr style="width: 100%">
            <td class="divFooter" style="text-align: left; width: 100%">
                <asp:Button ID="BtnAdd" Text="新 增" runat="server" />
                <asp:Button ID="BtnUpd" Text="编 辑" Enabled="false" runat="server" />
                <asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
                <asp:Button ID="BtnView" Text="查 看" Enabled="false" runat="server" />
                <asp:Button ID="BtnSelect" Text="从公司技术标准体系选择" Width="168px" OnClick="BtnSelect_Click" runat="server" />&nbsp;
                <asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />
                <input id="HdnId" style="width: 48px; height: 22px" type="hidden" size="2" name="HdnId" runat="server" />
<input id="HdnPrjCode" style="width: 64px; height: 22px" type="hidden" size="5" name="Hidden1" runat="server" />
<input id="HdnPrjName" style="width: 56px;
                            height: 22px" type="hidden" size="4" name="Hidden2" runat="server" />

                <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                <asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:DataGrid ID="DGrdStandard" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" DataKeyField="MainID" AllowPaging="true" PageSize="25" OnPageIndexChanged="DGrdStandard_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>

<ItemTemplate>
                                <asp:Label ID="lblid" Visible="false" Text='<%# Convert.ToString(Eval("MainID")) %>' runat="server"></asp:Label>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn>
<HeaderTemplate>
                                <span>
                                    
                                </span>
                            </HeaderTemplate>

<ItemTemplate>
                                <img alt="" src="" id="imgPNG" style="width: 12px; height: 12px;" runat="server" />

                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>

<EditItemTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="标准代号"><ItemTemplate>
                                <span class="al" onclick="rowQuery('<%# Eval("MainID") %>',0);">
                                    <%# Eval("TechnologyCriterionID") %>
                                </span>
                            </ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="TechnologyPromulgateTime" HeaderText="年号"></asp:BoundColumn><asp:BoundColumn DataField="TechnologyName" HeaderText="标准名称"></asp:BoundColumn><asp:BoundColumn DataField="TechnologyClassify" HeaderText="标准分类"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                <%# GetAnnx(1725, Convert.ToString(Eval("TechGuid"))) %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="mark" HeaderText="归档状态" Visible="false"></asp:BoundColumn><asp:TemplateColumn Visible="false" HeaderText="审核">
<ItemTemplate>
                                <asp:CheckBox ID="isAudit" runat="server" />
                            </ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
