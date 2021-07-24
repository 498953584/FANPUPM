<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectEquipment.aspx.cs" Inherits="Equ_Equipment_SelectEquipment" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设备类别设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/Equipment/SelectEquipment.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 89%;">
            <tr>
                <td style="width: 150px; vertical-align: top; height: 420px;">
                    <div class="pagediv" style="width: 150px; height: 100%;" id="div1" runat="server">
                        <asp:TreeView ID="trvwEquipmentType" ShowLines="true" ExpandDepth="2" OnSelectedNodeChanged="trvwEquipmentType_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
                </td>
                <td style="width: 100%; vertical-align: top; border-left: solid 2px #CADEED;">
                    <table border="0" class="tab">
                        <tr style="height: 1px;">
                            <td style="vertical-align: top;">
                                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                    <tr>
                                        <td class="descTd">
                                            设备状态
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlState" Width="120px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td class="descTd">
                                            编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="descTd">
                                            名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div class="pagediv">
                                    <asp:GridView ID="gvEquipment" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvEquipment_RowDataBound" DataKeyNames="Id,Code,Name,Specification,PurchasePrice,PurchaseDate" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyEquipment" style="width: 100%; margin-left: 0px; margin-right: 0px;
                                                padding: 0;">
                                                <tr class="header" style="width: 100%;">
                                                    <th scope="col" style="width: 25px;">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        设备编号
                                                    </th>
                                                    <th scope="col">
                                                        设备名称
                                                    </th>
                                                    <th scope="col">
                                                        规格
                                                    </th>
                                                    <th scope="col">
                                                        设备状态
                                                    </th>
                                                    <th scope="col">
                                                        原值
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Code" HeaderText="编号" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="原值" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="制造厂家"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 98%; height: 25px; float: left; text-align: right; vertical-align: middle">
        <input type="button" id="btnSave" disabled="disabled" value="保存" />
        <input type="button" id="btnCancel" value="取消" />
    </div>
    
    <asp:HiddenField ID="hfldId" runat="server" />
    
    <asp:HiddenField ID="hfldCode" runat="server" />
    
    <asp:HiddenField ID="hfldName" runat="server" />
    
    <asp:HiddenField ID="hfldSpecification" runat="server" />
    
    <asp:HiddenField ID="hfldPurchasePrice" runat="server" />
    
    <asp:HiddenField ID="hfldPurchaseDate" runat="server" />
    </form>
</body>
</html>
