<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmMaterialBack.aspx.cs" Inherits="StockManage_basicset_SmMaterialBack" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>材料退库</title>
    <base  target="_self"/>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"> </script>
    <script type="text/javascript" >
	 function openPerson()
			{
			var url= "/CommonWindow/PickSinglePerson.aspx?sm";
		    winopen(url)
		    }

  function setBtnState()
  {
  if(window.location.href.indexOf('look')!=-1)
  {
      document.getElementById('btnAddMaterial').disabled = '!enable';
      document.getElementById('btnDelMaterial').disabled = '!enable';
      document.getElementById('btnSave').disabled = '!enable';
  
  }
 
  }

  function checkNull() {
      var strDate = document.getElementById('dtxtApplyDate').value;
      var people = document.getElementById('txtPeople').value;
      if (strDate == "") { alert('系统提示：\n\n日期不能为空'); return false; }
      if (people == "") { alert('系统提示：\n\n编制人不能为空'); return false; }
      return true;
  }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table  width="100%">
            <tr>
                <td style=""class="headerrow">
                    材料退库</td>
            </tr>
            <tr>
                <td  >
                     <table class="tab">
                        <tr>
                            <td style="width: 20%">
                                退库编号</td>
                            <td style="width: 30%">
                                <input id="txtCode" type="text" readonly="readonly" runat="server" />
</td>
                            <td style="width: 20%">
                                项目名称</td>
                            <td style="width: 30%">
                                <input id="txtProName" type="text" readonly="readonly" runat="server" />

                                <input id="hdnProCode" style="width: 10px" type="hidden" runat="server" />
</td>
                        </tr>
                         <tr>
                             <td style="width: 20%">
                               退库时间</td>
                             <td style="width: 30%">
                                 <JWControl:DateBox ID="dtxtApplyDate" runat="server"></JWControl:DateBox></td>
                             <td style="width: 20%">
                                 仓库编码</td>
                             <td style="width: 30%"><input id="txtTcode" type="text" readonly="readonly" runat="server" />

                                 <img alt="选择"
                                           height="16" onclick="" src="/Images/corp.gif" 
                                            style="cursor: hand; border:0px" width="16" />
                                 <input id="hndTcode" style="width: 10px" type="hidden" runat="server" />
</td>
                         </tr>
                        <tr>
                            <td style="width: 20%">
                                录入人</td>
                            <td style="width: 30%">
                                <input id="txtPeople" type="text" runat="server" />

                                <img alt="选择"
                                           height="16" onclick="openPerson();" src="/Images/corp.gif" 
                                            style="cursor: hand; border:0px" width="16" />
                                <input id="hdnPeople" style="width: 10px" type="hidden" runat="server" />
</td>
                            <td style="width:20%">
                                附件</td>
                            <td style="width: 30%">
                                &nbsp;<MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <td style="width:20%">
                                备注</td>
                            <td colspan="3">
                                <textarea id="txtRemark" cols="20" rows="2" runat="server"></textarea></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="bottonrow">
                    <input id="btnAddMaterial" type="button" value="从出库单选择" runat="server" />

                    <input id="btnDelMaterial" type="button" value="删除" runat="server" />
</td>
            </tr>
            <tr>
                <td style="">
                <div class="pagediv">
                    <asp:GridView ID="gvSmMaterialBack" AutoGenerateColumns="false" CssClass="gvdata" runat="server">
<EmptyDataTemplate>
                                                        <table class="tab" width="100%">
                                                            <tr class="header">
                                                                <td style="width: 30px">
                                                                    <asp:CheckBox ID="chkAll" runat="server" /></td>
                                                                <td style="width: 20px">
                                                                </td>
                                                                <td style="width: 100px">
                                                                    资源编号</td>
                                                                <td style="width: 150px">
                                                                   资源名称</td>
                                                                <td>
                                                                    规格</td>
                                                                     <td>
                                                                    单位</td>
                                                                     <td>
                                                                    单价</td>
                                                                    <td>
                                                                    供应商</td>
                                                                   <td>
                                                                    退库数量</td>
                                                            </tr>
                                                            <tr><td colspan="9" style="height:200px">暂无数据</td></tr>
                                                        </table>
                                                    </EmptyDataTemplate>
<Columns><asp:TemplateField><HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="chkBox" runat="server" />
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源编号"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="退库数量"><ItemTemplate>
                            <asp:TextBox ID="txtNumber" Text='<%# Convert.ToString(Eval("number")) %>' runat="server"></asp:TextBox>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="bottonrow">
                    <input id="btnSave" type="button" value="保存" OnServerClick="btnSave_ServerClick" runat="server" />

                    <input id="btnClose" type="button" value="关闭"  onclick="javascript:window.opener.location.href=window.opener.location.href;window.close();"/></td>
            </tr>
        </table>
    </form>
</body>
</html>
