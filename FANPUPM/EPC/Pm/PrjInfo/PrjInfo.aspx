<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjInfo.aspx.cs" Inherits="PrjInfo" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>项目信息</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<link href="../../../Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    
    <script language="JavaScript" type="text/javascript">

        addEvent(window, 'load', function () {
            var jwTable = new JustWinTable('gvPrjInfo');
            replaceEmptyTable('emptyTable', 'gvPrjInfo');
            formateTable('gvPrjInfo', 0);
            showTooltip('tooltip', 25);
            setButton2(jwTable);
        });
        function setButton2(jwTable) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            //单击行
            jwTable.registClickTrListener(function () {
                document.getElementById('hdnModuleCode').value = this.typeCode;
            });
        }

        function clickRow(obj, moduleCode, isLeaf) {
            document.getElementById('btnAdd').disabled = false;
            //if (document.getElementById('hdnModuleCode').value != moduleCode)//控制连续在当前分类上单击时，不用处理下面的代码。
            //{
            document.getElementById('hdnModuleCode').value = moduleCode;

            if (moduleCode.length == 10) {
                document.getElementById('btnAdd').disabled = true;
            } else {
                document.getElementById('btnAdd').disabled = false;
            }
            if (moduleCode == "") {
                document.getElementById('btnDel').disabled = (moduleCode.length == 0);
                document.getElementById('btnUser').disabled = true;
            }
            else {
                document.getElementById('btnDel').disabled = !isLeaf;
                document.getElementById('btnUser').disabled = !isLeaf;
            }
            document.getElementById('btnEdit').disabled = (moduleCode.length == 0);

            //待办按钮
            document.getElementById("btnWaitingJob").disabled = !isLeaf;
            /*在这之前添加你的处理代码*/
            //doClick(obj, 'grdModules'); //调用样式设置

            //显示相应的技术参数和付款周期信息
            //				    var url = "ResTypeOther.aspx?TypeCode="+moduleCode+"&pt=1" ;
            //				    document.getElementById("FraFlow").src = url;
            //}
        }
        function outRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOut(obj); //调用样式设置
        }
        function overRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOver(obj); //调用样式设置
        }

        function switchDisplay(obj, t1, t2) {
            /*在这之前添加你的处理代码*/
            doSwitchDisplay(obj, 'grdModules', 'hdnModuleCodeList', t1, t2, '../../../'); //调用样式设置
        }
        function ClickBtn(op) {
            var moduleCode = document.getElementById('hdnModuleCode').value;
            var url = ""
            var re = true;
            switch (op.toLowerCase()) {
                case "add":
                    url = "PrjInfoEdit.aspx?TypeCode=" + moduleCode + "&op=add";
                    re = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;');
                    break;
                case "upd":
                    url = "PrjInfoEdit.aspx?TypeCode=" + moduleCode + "&op=upd";
                    re = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;');
                    break;
                case "view":
                    url = "PrjInfoEdit.aspx?TypeCode=" + moduleCode + "&op=upd";
                    re = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;');
                    break;
                case "del":
                    re = confirm("确定要删除该项目吗？");
                    break;
                case "user":

                    url = "Popedomdis.aspx?PrjCode=001&PrjGuid=&subpc=" + moduleCode;
                    //re = window.showModalDialog(url,window,'dialogHeight:520px;dialogWidth:600px;center:1;help:0;status:0;');
                    window.open(url, '', 'left=150,top=50,width=600,height=500,toolbar=no,status=yes,scrollbars=yes,resizable=no');
                    break;
            }
            return re;
        }


        //?
        function OpenUpAdd() {
            var MKDM = document.getElementById('hdnModuleCode').value;
            if (MKDM == "") {
                alert("请选择模块！");
            }
            else {
                var url = "/UploadFiles/uploadfile/AnnexList.aspx?mid=59&rc=" + MKDM + "&at=0&type=2";
                window.showModalDialog(url, window, 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
            }
        }
        //弹出待办工作页面   ??
        function WaitingJob() {
            var MKDM = document.getElementById('hdnModuleCode').value;
            if (MKDM == "") {
                alert("请选择模块！");
            }
            else {
                window.open("WaitingJob.aspx?mkdm=" + MKDM, "", "width=500,height=500,titlebar=no;");
            }
        }
        function adminDel() {
            url = "../../../EPC/UserControl1/TwoPassSet.aspx?tt=1";
            return window.showModalDialog(url, window, "dialogHeight:135px;dialogWidth:260px;center:1;status:no;scroll:no;help:no;");
        }
        //查看
        function OpenView(typeCode) {
            var url = 'PrjInfoView.aspx?typecode=' + typeCode;
            window.open(url, '', 'left=150,top=150,width=840,height=530,toolbar=no,status=yes,scrollbars=yes,resiz able=no');
        }
    </script>
</head>

<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table2" class="table-normal" height="100%" cellspacing="0" cellpadding="0"
        width="100%">
        <tr class="td-toolsbar">
            <td width="20">
                <input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

            </td>
            <td style="text-align: right; white-space: nowrap;">
                项目编号
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:TextBox ID="txtPrjCode" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right; white-space: nowrap;">
                项目名称
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:TextBox ID="txtprjName" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right; white-space: nowrap;">
                建设单位
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:TextBox ID="txtOwner" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right; white-space: nowrap;">
                项目地点
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:TextBox ID="txtPrjPlace" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td style="white-space: nowrap;">
                <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
            </td>
            <td valign="middle" align="right" height="24" style="white-space: nowrap;">
                <asp:Button ID="btnAdd" CssClass="button-normal" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                <asp:Button ID="btnEdit" CssClass="button-normal" Text="修 改" OnClick="btnUpd_Click" runat="server" />
                <asp:Button ID="btnUser" Text="项目人员" CssClass="button-normal" Width="60px" OnClick="btnUser_Click" runat="server" />
                <asp:Button ID="btnDel" CssClass="button-normal" Text="删 除" OnClick="btnDel_Click" runat="server" />
                <asp:Button ID="btndelAdmin" Text="超级删除" OnClick="btndelAdmin_Click" runat="server" />
                <button id="btnWaitingJob" disabled onclick="WaitingJob();" type="button" class="button-normal"
                    style="display: none">
                    待办工作</button>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" colspan="11">
                
                <div id="dvModulesBox">
                    <asp:GridView ID="gvPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,i_ChildNum,PrjState" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable">
                                <tr class="header">
                                    <th scope="col">
                                        项目编号
                                    </th>
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        项目名称
                                    </th>
                                    <th scope="col">
                                        建设单位
                                    </th>
                                    <th scope="col">
                                        工程造价（万元）
                                    </th>
                                    <th scope="col">
                                        项目地点
                                    </th>
                                    <th scope="col">
                                        开始时间
                                    </th>
                                    <th scope="col">
                                        结束时间
                                    </th>
                                    <th scope="col">
                                        状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:BoundField DataField="PrjCode" HeaderText="项目编号" ItemStyle-HorizontalAlign="Left" /><asp:BoundField DataField="TypeCode" HeaderText="序号" ItemStyle-HorizontalAlign="Left" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                    
                                    <span class="link tooltip" title="" onclick="OpenView('');">
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价（万元）">
<ItemTemplate>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目地点" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <span class="tooltip" title="">
                                        
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始时间" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PrjState" HeaderText="项目状态" ItemStyle-HorizontalAlign="Left" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
        <tr height="0" style="display: none">
            <td valign="top" colspan="11">
                <iframe id="FraFlow" name="FraFlow" src="" frameborder="no" width="100%" height="100%">
                </iframe>
            </td>
        </tr>
    </table>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <JWControl:PersistScrollPosition ID="PersistScrollPosition2" ControlToPersist="dvModulesBox" runat="server">
    </JWControl:PersistScrollPosition>
    </form>
</body>
</html>
