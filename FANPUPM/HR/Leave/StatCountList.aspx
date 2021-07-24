<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatCountList.aspx.cs" Inherits="HR_Leave_StatCountList" EnableEventValidation="false" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>请休假统计</title>
    <script  type="text/javascript" src="/Script/jquery.js"></script>
     <script language="javascript" type="text/javascript">
    <!--
         $(document).ready(function () {
             calculateDay();
         });
    function ClickRow(bm,countdate,uc)
    {
        document.getElementById('Hdnbmdm').value = bm;  
        document.getElementById('HdnCountDate').value = countdate; 
        document.getElementById('HdnUserCode').value = uc;   
        document.getElementById('btnEdit').disabled = false;  
        document.getElementById('btnDel').disabled = false;
        
    }
   //编辑
    function openEdit(t)
    {   
        var bm = document.getElementById('Hdnbmdm').value; 
        var countdate = document.getElementById('HdnCountDate').value;
        var uc = document.getElementById('HdnUserCode').value;
        var url = 'StatCountEdit.aspx?bm='+bm+'&d='+countdate+'&uc='+uc;    
		return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:400px;center:1;help:0;status:0;");
    }
    function calculateDay() {
        var table = document.getElementById('GVStat');
        if (table == null){ 
            return;
        }
        for (var i = 1; i < table.rows.length; i++) {
            var row = table.rows[i];
            if (row && row.cells[4]) {
                var cells = row.cells;
                var notInday = parseFloat(cells[4].innerText) + parseFloat(cells[5].innerText) + parseFloat(cells[6].innerText) + parseFloat(cells[7].innerText) + parseFloat(cells[8].innerText) + parseFloat(cells[9].innerText) + parseFloat(cells[10].innerText) + parseFloat(cells[11].innerText);
                cells[13].innerText = (parseFloat(cells[12].innerText) - parseFloat(notInday)).toFixed(1);
                if ((parseFloat(cells[12].innerText) == parseFloat(cells[13].innerText)) && parseFloat(cells[12].innerText) > 0)
                    cells[14].innerText = "0%";
                else {
                    if (parseFloat(cells[12].innerText) == 0)
                        cells[14].innerText = "0%";
                    else {
                        cells[14].innerText = ((notInday / parseFloat(cells[12].innerText)) * 100).toString() + "%";
                    }
                }

            }
        }
    }
    -->    
    </script>
</head>
<body class="body_clear" scroll="no"> 
    <form id="form1" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
        <tr>               
            <td class="td-title">
                请休假统计</td>
        </tr>
        <tr class="td-search">
            <td>
                <asp:DropDownList ID="DDLYear" AppendDataBoundItems="true" runat="server"></asp:DropDownList>
                年
                <asp:DropDownList ID="DDLMonth" runat="server"><asp:ListItem Text="1" /><asp:ListItem Text="2" /><asp:ListItem Text="3" /><asp:ListItem Text="4" /><asp:ListItem Text="5" /><asp:ListItem Text="6" /><asp:ListItem Text="7" /><asp:ListItem Text="8" /><asp:ListItem Text="9" /><asp:ListItem Text="10" /><asp:ListItem Text="11" /><asp:ListItem Text="12" /></asp:DropDownList>
                月
                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" /></td>
        </tr>
        <tr>
            <td class="td-toolsbar">
                <asp:Button ID="btnStat" Text="重新统计" OnClick="btnStat_Click" runat="server" />
               <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
               <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />  
                <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />                
                <asp:HiddenField ID="Hdnbmdm" runat="server" />
                <asp:HiddenField ID="HdnCountDate" runat="server" />
                <asp:HiddenField ID="HdnUserCode" runat="server" />
                <input id="Hidstate" type="hidden" runat="server" />

                    
            </td>
        </tr>
        <tr>
        <td>    
         <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
             <asp:GridView ID="GVStat" AllowPaging="true" Width="100%" PageSize="15" AutoGenerateColumns="false" CssClass="grid" OnRowDataBound="GVStat_RowDataBound" OnPageIndexChanging="GVStat_PageIndexChanging" DataKeyNames="iYear,iMonth,UserCode" runat="server">
<EmptyDataTemplate>
                     <table id="Table2" border="0" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse">
                         <tr class="grid_head">
                             <th align="center" scope="col" style="width: 40px">
                                 序 号</th>
                             <th align="center" scope="col">
                                 姓名</th>
                             <th scope="col">
                                 迟到</th>
                             <th scope="col">
                                 早退</th>
                             <th scope="col">
                                 事假</th>
                            <th scope="col">
                                 病假</th>
                            <th scope="col">
                                 带薪婚假</th>
                            <th scope="col">
                                 产假</th>
                            <th scope="col">
                                 丧假</th>
                            <th scope="col">
                                 带薪年假</th>
                            <th scope="col">
                                 工伤</th>
                            <th scope="col">
                                 旷工</th>
                            <th scope="col">
                                 应到天数</th>
                            <th scope="col">
                                    实到天数</th>
                            <th scope="col">
                                 缺勤率</th>
                         </tr>
                     </table>
                 </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="40px"><ItemTemplate>  
                          <%# Container.DataItemIndex + 1 %>
                       </ItemTemplate></asp:TemplateField><asp:BoundField DataField="UserCode" HeaderText="姓名" ReadOnly="true" SortExpression="UserCode" /><asp:BoundField DataField="Later" HeaderText="迟到" SortExpression="Later" /><asp:BoundField DataField="LeaveEarly" HeaderText="早退" SortExpression="LeaveEarly" /><asp:BoundField DataField="Holiday1" HeaderText="事假" SortExpression="Holiday1" /><asp:BoundField DataField="Holiday5" HeaderText="病假" SortExpression="Holiday5" /><asp:BoundField DataField="Holiday2" HeaderText="带薪婚假" SortExpression="Holiday2" /><asp:BoundField DataField="Holiday6" HeaderText="产假" SortExpression="Holiday6" /><asp:BoundField DataField="Holiday7" HeaderText="丧假" SortExpression="Holiday7" /><asp:BoundField DataField="Holiday3" HeaderText="带薪年假" SortExpression="Holiday3" /><asp:BoundField DataField="Holiday4" HeaderText="工伤" SortExpression="Holiday4" /><asp:BoundField HeaderText="旷工" DataField="Holiday8" /><asp:BoundField HeaderText="应到天数" DataField="FactDay" /><asp:BoundField HeaderText="实到天数" /><asp:BoundField HeaderText="缺勤率" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
           
         </div>                                               
            </td>
        </tr>  
  </table>
    </div>
    </form>
</body>
</html>
