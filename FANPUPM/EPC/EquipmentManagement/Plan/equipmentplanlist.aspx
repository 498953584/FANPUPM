<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentplanlist.aspx.cs" Inherits="EquipmentPlanManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>设备计划</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script src="../../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Script/jwJson.js"></script>

    <script type="text/javascript" src="../../../Script/wf.js"></script>

    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            var outReserveTable = new JustWinTable('grdList');
            setButton(outReserveTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
            setButton(outReserveTable, 'btnDel', 'btnEdit', 'btnSee', 'hfldPurchaseChecked');
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnSee'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
            setWfButtonState2(outReserveTable, 'WF1');
        });
        function rowAdd() {
            parent.parent.desktop.flowclass = window;
            var url = "/EPC/EquipmentManagement/Plan/equipmentplandetail.aspx?t=1&ci=" + document.getElementById("hfldPurchaseChecked").value; // "/StockManage/SmOutReserve/AddReserve.aspx";
                      toolbox_oncommand(url, "新增机械器具计划");
        }
        function rowEdit() {
            parent.parent.desktop.flowclass = window;
            var url = "/EPC/EquipmentManagement/Plan/equipmentplandetail.aspx?t=2&ci=" + document.getElementById("hfldPurchaseChecked").value;
            toolbox_oncommand(url, "编辑机械器具计划");
        }

        function rowQuery() {
//            parent.parent.desktop.flowclass = window;
            var url = "/EPC/EquipmentManagement/Plan/equipmentPlanView.aspx?ic=" + document.getElementById("hfldPurchaseChecked").value; // "/StockManage/SmOutReserve/AddReserve.aspx";
            viewopen(url);
        }

        //			function clickRow(dgd,obj,planCode,AuditState,wfguid)
        //			{
        //				document.getElementById('hdnPlanCode').value = planCode;			
        //                if(document.getElementById('WF1_FID') !=null )
        //                   document.getElementById('WF1_FID').value = wfguid;
        //                if(document.getElementById('WF1_BusinessCode') !=null )
        //                   document.getElementById('WF1_BusinessCode').value = "032";
        //                if(document.getElementById('WF1_BusinessClass') !=null )
        //                  document.getElementById('WF1_BusinessClass').value = "001";
        //                   if(document.getElementById('WF1_PrjGuid') !=null )
        //               document.getElementById('WF1_PrjGuid').value ="" ;
        //         	   document.getElementById("WF1_hidcontent").value="设备计划审核(计划编码"+planCode+")";          //查看审核记录是显示审核内容在这里设置

        //             
        //              if (AuditState == "-1")  //未提交状态
        //                {
        //                   if(document.getElementById('btnStartWF') !=null )
        //                     document.getElementById('btnStartWF').disabled = false;     //可提交 
        //                   if(document.getElementById('WF1_btnViewWF') !=null )
        //                     document.getElementById('WF1_btnViewWF').disabled = true;   //不可查看流程状态
        //                   if(document.getElementById('WF1_btnWFPrint') !=null )
        //                     document.getElementById('WF1_btnWFPrint').disabled = true;  //不可查看审核记录
        //                }
        //              if(AuditState=="-3")   //重报状态
        //               {
        //                  if(document.getElementById('btnStartWF') !=null )
        //                     document.getElementById('btnStartWF').disabled = false;      //可提交
        //                  if(document.getElementById('WF1_btnViewWF') !=null )
        //                     document.getElementById('WF1_btnViewWF').disabled = false;   //可查看流程状态
        //                  if(document.getElementById('WF1_btnWFPrint') !=null )
        //                     document.getElementById('WF1_btnWFPrint').disabled = false;  //可查看审核记录   
        //               } 
        //              if(AuditState=="1"||AuditState=="0"||AuditState=="-2")   //以通过或者审核中驳回时
        //              {
        //                  if(document.getElementById('btnStartWF') !=null )
        //                     document.getElementById('btnStartWF').disabled = true;
        //                  if(document.getElementById('WF1_btnViewWF') !=null )
        //                     document.getElementById('WF1_btnViewWF').disabled = false;
        //                  if(document.getElementById('WF1_btnWFPrint') !=null )
        //                    document.getElementById('WF1_btnWFPrint').disabled = false;
        //              }
        //             if(AuditState=="0")//状态为0时才能撤销
        //              {
        //                 document.getElementById("CancelBt").disabled=false;
        //              }
        //              else
        //              {
        //                 document.getElementById("CancelBt").disabled=true;
        //              }
        //             if( AuditState=="1"||AuditState=="0"||AuditState=="-3"||AuditState=="-2")   //彻底删除的可用性
        //              {
        //                 if(document.getElementById('WF1_BtnWFDel') !=null )
        //                  document.getElementById('WF1_BtnWFDel').disabled = false;
        //              }
        //             else
        //              {
        //                if(document.getElementById('WF1_BtnWFDel') !=null )
        //                  document.getElementById('WF1_BtnWFDel').disabled = true;
        //              }
        //			if(AuditState=="-1")
        //			 {
        //			    if(document.getElementById('btnDel') != null)			
        //				    document.getElementById('btnDel').disabled = false;
        //                if(document.getElementById('btnMod') != null)			
        //	                document.getElementById('btnMod').disabled = false;					
        //		      }
        //		     else
        //		      {
        //		         if(document.getElementById('btnDel') != null)			
        //				    document.getElementById('btnDel').disabled = true;
        //                 if(document.getElementById('btnMod') != null)			
        //	                document.getElementById('btnMod').disabled = true;		
        //		      }

        //			 if(document.getElementById('btn_see') != null)
        //		    	document.getElementById('btn_see').disabled = false;
        //		
        //				/*在这之前增加你的处理代码*/
        //				doClick(obj,dgd);//调用样式设置
        //			}
        //			function outRow(obj)
        //			{
        //				/*在这之前增加你的处理代码*/
        //				doMouseOut(obj);//调用样式设置
        //			}
        //			function overRow(obj)
        //			{
        //				/*在这之前添加你的处理代码*/
        //				doMouseOver(obj);//调用样式设置
        //			}
        //add edit
        function openEditPage(op) {
            var planCode = "";
            planCode = document.getElementById('hdnPlanCode').value;
            var url = "EquipmentPlanDetail.aspx?t=" + op + "&ci=" + planCode;
            //alert(op);
            //alert(planCode);
            //window.location.href=url;
            return window.showModalDialog(url, window, "dialogHeight:358px;dialogWidth:580px;center:1;help:0;status:0;");
        }

        function OpenAudit() {
            var rid = document.getElementById('hdnPlanCode').value;
            var url = "/HR/Leave/ApplicationReq.aspx?ic=" + rid + "&mid=PlanAuito";
            return window.showModalDialog(url, window, "dialogHeight:230px;dialogWidth:390px;center:1;help:0;status:0;");
        }
       

    </script>

    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
<body scroll="no" class="body_frame">
    <form id="Form1" method="post" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="td-title" align ="center" >
                <asp:Label ID="Lbbt" Font-Bold="true" runat="server"></asp:Label>设备计划管理
            </td>
        </tr>
        <tr id="editManue" runat="server"><td class="divFooter" style="text-align: left" runat="server">
                <input id="btnAdd" type="button" value="新增" />
                <input id="btnEdit" type="button" value="编辑" disabled="true" runat="server" />

                
                <asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClick="btnDel_Click" runat="server" />
                <input type="button" id="btnLook" disabled="disabled" value="查看" />
                 
                 
                
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="032" BusiClass="001" runat="server" />
            </td></tr>
        <tr id="see" runat="server"><td class="divFooter" style="text-align: left" runat="server"> <input type="button" id="btnSee" disabled="disabled" value="查看" /></td></tr>
        <tr>

            <td style="height: 100%; vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="grdList" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdList_PageIndexChanging" OnRowDataBound="grdList_RowDataBound" DataKeyNames="planCode,wfGuid,prjCode" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("planCode")) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编码"><ItemTemplate>
                                   
                                        <%# Eval("PlanCode") %>
                                   
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("PlanCreatTime").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划制作人"><ItemTemplate>
                                    <%# Eval("PlanMaker") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("IsAuditing").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                    <%# StringUtility.GetStr(Convert.ToString(Eval("Remark"))) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
   <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
