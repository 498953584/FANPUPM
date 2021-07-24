<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDefineFlowAudit.aspx.cs" Inherits="UserDefineFlowAudit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>自定义流程审核</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="../../Script/jquery.cookie.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../../Script/jw.js"></script>

    <base target="_self" />
    <style type="text/css">
        table tr
        {
            border-color: Black;
        }
        .gvdata1
        {
            width: 100%; /*table-layout: fixed;*/
            border-collapse: separate;
            border-spacing: 0px 0px;
            border-width: 0px;
            border-bottom: solid 1px Black;
            border-top: solid 1px Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
        }
        table.gvdata1 th
        {
            overflow: hidden;
            font-weight: normal; /*text-align: center;*/
            border-color: Black;
            padding-left: 6px;
            padding-right: 6px;
        }
        table.gvdata1 td
        {
            /*overflow: hidden;*/
            vertical-align: top;
            padding-left: 6px;
            padding-right: 6px;
            font-weight: normal;
            border-color: Black;
        }
        table.gvdata1 tr
        {
            height: 22px;
        }
        .header
        {
            background-color: #EEF2F5;
        }
        .rowa
        {
            background-color: #fbfbfb;
        }
        .rowb
        {
            background-color: #ffffff;
        }
        .footer
        {
        }
    </style>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            registerChkAuditEvent();
        });
        function registerChkAuditEvent() {
            if (!document.getElementById('chkAudit')) return false;
            if (!document.getElementById('trAudit')) return false;
            addEvent(document.getElementById('chkAudit'), 'click', function() {
                if (this.checked) {
                    document.getElementById('trAudit').className = '';
                }
                else {
                    document.getElementById('trAudit').className = 'noprint';
                }
            });
        }
        function UpFile() {
            var RecordCode = document.getElementById('HdnRecordCode').value; //编号
            var url = "../../../commonWindow/Annex/AnnexList.aspx?mid=88&rc=" + RecordCode + "&at=0&type=1";
            var ref = window.showModalDialog(url, window, 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
            return true;
        }
        function download(filepath) {

            var url = "../../Common/DownLoad.aspx?path=" + filepath;
            document.getElementById('fileFrame').src = url;
        }
    </script>

</head>
<body id="print" style="overflow-x: hidden; overflow-y: auto;">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" class="tab" style="vertical-align: top; width: 100%;
        height: auto;">
        <tr>
            <td class="divHeader">
                自定义流程信息
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 100%">
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td style="text-align: right; white-space: nowrap; width:20%;">
                            发起人
                        </td>
                        <td style="width:80%" >
                            <asp:Label ID="LbMan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap;">
                            标题
                        </td>
                        <td>
                            <asp:Label ID="txtTitle" runat="server"></asp:Label>
                            <input id="hdnTitle" style="width: 10px" type="hidden" name="hdnTitle" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap;">
                            附件
                        </td>
                        <td>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            <input id="HdnRecordCode" type="hidden" runat="server" />

                            <input id="hidden" type="hidden" name="hdnFilePath" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; border-bottom: 1px solid Black; white-space: nowrap;
                            height: auto;">
                            内容
                        </td>
                        <td style="border-bottom: 1px solid Black; white-space: normal">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="txtContent" runat="server"></asp:Label></div>
                            <input id="hdnUserCode" style="width: 10px" type="hidden" name="hdnUserCode" runat="server" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit">
            <td style="vertical-align: top; width: 100%;">
                <table id="tableHeader" cellpadding="0" cellspacing="0" style="margin-top: 10px;
                    width: 100%" runat="server"><tr style="height: 20px;" runat="server"><td style="font-size: 13px; font-weight: bold; text-align: center; position: relative" runat="server">
                            审核记录
                            <div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
                                position: absolute; font-weight: normal;">
                                <asp:CheckBox ID="chkAudit" Style="float: right;" Checked="true" Text="打印" runat="server" />
                            </div>
                        </td></tr></table>
                <table cellpadding="0" cellspacing="0" border="1" class="gvdata1" id="table" style="border-collapse: collapse; width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 25px;" runat="server">
                            序号
                        </td><td style="white-space: nowrap;" runat="server">
                            节点名称
                        </td><td style="white-space: nowrap;" runat="server">
                            审核人
                        </td><td style="white-space: nowrap;" runat="server">
                            审核时间
                        </td><td style="white-space: nowrap;" runat="server">
                            审核结果
                        </td></tr></table>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    <iframe src="" id="fileFrame" height="0px" width="0px"></iframe>
    </form>
</body>
</html>
