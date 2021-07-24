<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometBalanceList.aspx.cs" Inherits="ContractManage_IncometBalance_IncometBalanceList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收款合同变更列表</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
     window.onload = function(){
         var aa=new JustWinTable('gvConract');
           setButton(aa,'btnDel','btnEdit','btnLook','hfldPurchaseChecked');
           addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
           addEvent(document.getElementById('btnLook'), 'click', rowQuery);
           addEvent(document.getElementById('btnBAdd'), 'click', rowBAdd);     
          
     function rowEdit()
     {
        var url="AddIncometModify.aspx?id=" + document.getElementById("hfldPurchaseChecked").value;
        winopen(url)
     }
      function rowBAdd()
     {
        var url="AddIncometModify.aspx?t=1&id=" + document.getElementById("hfldPurchaseChecked").value;
        winopen(url)
     }
     function rowQuery()
     {
        var url="ViewSmPurchaseplan.aspx?t=1&ic=" + this.guid+"&BusiCode=072&BusiClass=001";
        viewopen(url)
     }
        
     }   
     function openProjPicker()
			{
				var prj = new Array();
				prj[0] = "";
				prj[1] = "";
				var url= "/CommonWindow/ProjectPop.aspx";
				window.showModalDialog(url,prj,"dialogHeight:300px;dialogWidth:600px;center:1;help:0;status:0;");
				if (prj[0]!="")
				{
					document.getElementById('hdnProjectCode').value = prj[0];
					document.getElementById('txtProject').value = prj[1];
				}
			}  
  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="tab">
            <tr style="height: 20px;">
                <td class="headerrow">
                    收款合同变更</td>
            </tr>
            <tr>
                <td style="height: 35%; width: 100%;">
                    <table border="0" class="tab">
                        <tr style="height: 30px;">
                            <td style="height: 20px; vertical-align: top;">
                                <div>
                                    合同编号
                                    <asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
                                    合同名称
                                    <asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
                                    合同类型
                                    <asp:TextBox ID="txtTypeID" Width="120px" runat="server"></asp:TextBox>
                                    <span style="word-spacing: 20px;">项 目</span>
                                    <input type="text" id="txtProject" readonly="readonly" style="background-color: #ffffc0;
                                        width: 120px;" title="请双击此处选择" ondblclick="openProjPicker();" runat="server" />

                                    <asp:HiddenField ID="hdnProjectCode" runat="server" />
                                </div>
                                <div style="margin-top: 3px;">
                                    起始金额
                                    <asp:TextBox ID="txtStartContractPrice" Width="120px" runat="server"></asp:TextBox>
                                    结束金额
                                    <asp:TextBox ID="txtEndContractPrice" Width="120px" runat="server"></asp:TextBox>
                                    起始时间
                                    <JWControl:DateBox ID="txtStartSignedTime" Width="120px" runat="server"></JWControl:DateBox>
                                    结束时间
                                    <JWControl:DateBox ID="txtEndSignedTime" Width="120px" runat="server"></JWControl:DateBox>
                                    <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;" class="bottonrow">
                                <input type="button" id="btnAdd" value="新增" onclick="viewopen('AddIncometContract.aspx','','')" />
                                <input type="button" id="btnBAdd" style="width: 100px;" value="新增补充协议" />
                                <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                                <asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                                <input type="button" id="btnLook" disabled="disabled" value="查看" />
                                <asp:Button ID="btnAleave" Text="权限" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div class="pagediv">
                                    <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ID")) %>' runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更时间"><ItemTemplate>
                                                    <span class="al" onclick="viewopen('ViewSmPurchaseplan.aspx?ic=<%# Eval("ContractId") %>')">
                                                        <%# Eval("ChangeTime") %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更金额"><ItemTemplate>
                                                    <%# Eval("ChangePrice") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更原因"><ItemTemplate>
                                                    <%# Eval("ChangeReason") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订时间"><ItemTemplate>
                                                    <%# Eval("SignedTime") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="办理人"><ItemTemplate>
                                                    <%# Eval("Transactor") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                    <%# GetAnnx(Convert.ToString(Eval("ID"))) %>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
       
        </table>
        <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
