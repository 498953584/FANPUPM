<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologyjdquery.aspx.cs" Inherits="TechnologyJDQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>TechnologyJDQuery</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2S.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var purchasePlanTable = new JustWinTable('DGrdTechnology');
            if (request("Levels").toUpperCase() != "1") {
                setButton(purchasePlanTable, 'BtnDel', 'BtnUpd', 'BtnView', 'HdnId')
            }
            setWfButtonState2(purchasePlanTable, 'WF1');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
        });

        function rowQuery(path, _title) {
            parent.parent.desktop.AddIncometBalance = window;
            toolbox_oncommand(path, _title);
        }
        function FileUp() {
            var hnClassID = "28" + window.document.getElementById('HdnId').value;
            var ClassID = "FD1104BD-A7F4-45EA-9FEB-0265AF013D00";
            var pc = document.getElementById('HdnPrjCode').value;
            var hnProjectCode = "1";
            url = "../FileManage/ProjectFileUpAdd.aspx?FilesID=" + hnClassID + "&ProjectCode=" + hnProjectCode + "&ClassID=" + ClassID + "&PC=" + pc + "&Science=1&ModuleId=28";
            url += "&SpecialID=0";
            return window.showModalDialog(url, window, 'dialogHeight:220px;dialogWidth:380px;center:1;help:0;status:0;');
        }

        function openEdit(opType) {
            var projectCode = document.getElementById("HdnPrjCode").value;
            var prjname = escape(document.getElementById("HdnPrjName").value);
            var Id = document.getElementById("HdnId").value;
            var url;
            var title;
            switch (opType) {
                case 'Add':
                    url = "/EPC/17/Ppm/ScienceInnovate/TechnologyJDEdit.aspx?Type=Add&pc=" + projectCode + "&Id=" + Id + "&pn=" + prjname;
                    title = "新增技术交底";
                    break;
                case 'Upd':
                    url = "/EPC/17/Ppm/ScienceInnovate/TechnologyJDEdit.aspx?Type=Upd&Id=" + Id + "&pc=" + projectCode + "&pn=" + prjname;
                    title = "编辑技术交底";

                    break;
                case 'View':
                    url = "/EPC/17/Ppm/ScienceInnovate/TechnologyJDEdit.aspx?Type=View&Id=" + Id + "&pc=" + projectCode + "&pn=" + prjname;
                    title = "查看技术交底";
                    break;
            }
            //			parent.parent.desktop.flowclass = window; //不可少
            top.ui._flowclass = window;
            toolbox_oncommand(url, title); //引用js
        }
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
    <font face="宋体">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr id="header">
                <td style="height: 28px">
                    技术交底
                </td>
            </tr>
            <tr>
                <td valign="top" style="vertical-align: top; height: 30px;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd" style="">
                                施工单位
                            </td>
                            <td>
                                <asp:TextBox ID="txtConstructionUnit" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                被交底单位
                            </td>
                            <td>
                                <asp:TextBox ID="txtByTellUnit" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                交底开始日期
                            </td>
                            <td>
                                <asp:TextBox ID="TellDateBegin" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                交底结束日期
                            </td>
                            <td>
                                <asp:TextBox ID="TellDateEnd" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" class="divFooter" style="text-align: left; width: 100%; vertical-align: top;">
                    <asp:Button ID="BtnAdd" Text="新 增" runat="server" />
                    <asp:Button ID="BtnUpd" Text="编 辑" Enabled="false" runat="server" />
                    <asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
                    <asp:Button ID="BtnView" Text="查 看" Enabled="false" runat="server" />
                    <input id="HdnId" style="width: 48px; height: 22px" type="hidden" size="2" name="HdnId" runat="server" />
<input id="HdnPrjCode" style="width: 64px; height: 22px" type="hidden" size="5" name="Hidden1" runat="server" />
<input id="HdnPrjName" style="width: 56px;
                                height: 22px" type="hidden" size="4" name="Hidden2" runat="server" />

                    <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                    <font face="宋体">
                        <asp:Button ID="btnFiles" Text="归 档" Enabled="false" OnClick="btnFiles_Click" runat="server" />
                    </font>
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="114" BusiClass="001" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                        <asp:DataGrid ID="DGrdTechnology" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" DataKeyField="MainID" AllowPaging="true" PageSize="25" OnPageIndexChanged="DGrdTechnology_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                        <input id="chkAll" type="checkbox" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:HiddenField ID="HiddenField1" Value='<%# Convert.ToString(Eval("MainID")) %>' runat="server" />
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn>
<ItemTemplate>
                                        <img src="" alt="" width="12px" height="12px">
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="hfldPrjCode" Value='<%# Convert.ToString(Eval("PrjCode")) %>' runat="server" />
                                        <asp:HiddenField ID="hfldGuid" Value='<%# Convert.ToString(Eval("TechGuid")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FillDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="FillName" HeaderText="填报人"></asp:BoundColumn><asp:TemplateColumn HeaderText="工程名称">
<ItemTemplate>
                                        <asp:Label ID="Label1" CssClass="link" Text="Label" runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ConstructionUnit" HeaderText="施工单位"></asp:BoundColumn><asp:BoundColumn DataField="ByTellUnit" HeaderText="被交底单位"></asp:BoundColumn><asp:BoundColumn DataField="TellName" HeaderText="交底人"></asp:BoundColumn><asp:BoundColumn DataField="ByTellPeople" HeaderText="被交底人"></asp:BoundColumn><asp:BoundColumn DataField="TellDate" HeaderText="交底日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="TellLocus" HeaderText="交底地点"></asp:BoundColumn><asp:BoundColumn DataField="TellContentAbstract" HeaderText="交底内容提要"></asp:BoundColumn><asp:TemplateColumn HeaderText="流程状态"><ItemTemplate>
                                        <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                    </ItemTemplate></asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                    </table>
                </td>
            </tr>
        </table>
    </font>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        if (request("Levels").toUpperCase() == "1") {
            $("#ba").find("th").eq(0).hide();
            $("#td_Btn").hide();
            $(".divFooter").hide();
        }
        if ($("#DGrdStandard tr").size() == 2) {
            if ($(".GD-Pager") != null) {
                $(".GD-Pager").hide();
                $("#DGrdStandard tr").eq(0).find("td").eq(0).find("input").attr("disabled", "disabled");
            }
        }
        $("#Button1").hide();
        $(".queryTable td").attr("style", "white-space:nowrap;");
        $("#DGrdTechnology tr").each(function () {
            //            $(this).click(function () {
            //                var _IDTem = $(this).attr("id");
            //                $("#HdnId").val(_IDTem);
            //                var _leaves = $(this).attr("Levels");
            //                if (_leaves != "1") {
            //                    document.getElementById("BtnUpd").disabled = false;
            //                    document.getElementById("BtnDel").disabled = false;
            //                }
            //            });
            var _markTem = $(this).attr("mark");
            if (_markTem != "undefined" && _markTem != "" && _markTem != null) {
                if (request("Levels").toUpperCase() == "1") {
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
