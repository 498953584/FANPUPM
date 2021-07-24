<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowAudit.ascx.cs" Inherits="StockManage_Common_ShowAudit" %>

<script type="text/javascript">
    addEvent(window,'load',function(){
        registerChkAuditEvent();
    });
    function registerChkAuditEvent() {
        if(!document.getElementById('ShowAudit1_chkAudit')) return false;
        if(!document.getElementById('trAudit')) return false;
        addEvent(document.getElementById('ShowAudit1_chkAudit'),'click',function(){
            if(this.checked){
                document.getElementById('trAudit').className = '';
            }
            else{
                document.getElementById('trAudit').className = 'noprint';
            }
        });
    }
</script>
<style type="text/css">
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
            font-weight: normal;
            /*text-align: center;*/
            border-color: Black;
            color: #727FAA;
            padding-left: 6px;
            padding-right: 6px;
        }
        table.gvdata1 td
        {
            /*overflow: hidden;*/
            vertical-align:top;
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
<table id="tableHeader" cellpadding="0" cellspacing="0" style="width: 100%; margin-top:10px;" runat="server"><tr style="height: 20px;" runat="server"><td style="font-size:13px; font-weight:bold; text-align:center; position:relative" runat="server">
            审核记录
                <div class="noprint" style="height: 20px; width: 100%; left: 0px;  bottom: 0px; text-align: right; position: absolute; font-weight:normal;">
                            <asp:CheckBox ID="chkAudit" style="float:right;" Checked="true" Text="打印" runat="server" />
                        </div>
            </td></tr></table>

<table cellpadding="0" cellspacing="0" border="1" class="gvdata1" id="table" style="width: 100%; border-collapse: collapse;" runat="server"><tr runat="server"><td style="white-space:nowrap; vertical-align:middle;" runat="server">发起人</td><td style="vertical-align:middle" runat="server"><asp:Label ID="LbUserName" runat="server"></asp:Label></td><td style="text-align:right;white-space:nowrap;vertical-align:middle;" runat="server">发起时间</td><td colspan="2" style="vertical-align:middle" runat="server"><asp:Label ID="LbStartTime" runat="server"></asp:Label></td></tr><tr class="header" runat="server"><td style="width:25px;white-space:nowrap;" runat="server">
            序号</td><td style="white-space:nowrap;" runat="server">
            节点名称</td><td style="white-space:nowrap;" runat="server">
            审核人</td><td style="white-space:nowrap;" runat="server">
            审核时间</td><td style="white-space:nowrap;" runat="server">
            审核结果</td></tr></table>
