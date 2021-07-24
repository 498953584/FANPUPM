<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerDeptSet.aspx.cs" Inherits="oa_SysAdmin_UserManage_ManagerDeptSet" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>管理员-部门设置</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
		function postBackObject()
        {   
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") //点击treeview的checkbox是触发
            {
               var d=o.id;//获得当前checkbox的id;
               var e= d.replace("CheckBox,Nodes");//通过查看脚本信息,获得包含所有子节点div的id
               var div= window.document.getElementById(e);//获得div对象
               if(div!=null)  //如果不为空则表示,存在自节点
               {
                   var check=div.getElementsByTagName("INPUT");//获得div中所有的已input开始的标记
                   
                   if(o.checked == true)  //如果是选中，则其下所有子节点置为无效，且未选中
                   {
                       for(i=0;i<check.length;i++)    
                       {
                            if(check[i].type=="checkbox") //如果是checkbox
                            {
                                check[i].checked=false;//子节点的状态置为未选中
                                check[i].disabled=true;
                            }

                       }
                   }
                   else                   //如果是撤销选中，则撤销其所有子节点的无效状态
                   {
                       for(i=0;i<check.length;i++)    
                       {
                            if(check[i].type=="checkbox") //如果是checkbox
                            {
                                check[i].disabled=false;
                            }

                       }
                   }
               }
            } 
        }
        
        function initCheckBoxs()
        {
            var objs = window.document.getElementsByTagName("INPUT");
            
            for(i=0;i<objs.length;i++)
            {
                if(objs[i].checked)
                {
                   selChilden(objs[i]);
                }
            }
        }
        
        function selChilden(obj)
        {
               var d=obj.id;//获得当前checkbox的id;
               var e= d.replace("CheckBox,Nodes");//通过查看脚本信息,获得包含所有子节点div的id

               var div= window.document.getElementById(e);//获得div对象
               if(div!=null)  //如果不为空则表示,存在子节点
               {
                   var check=div.getElementsByTagName("INPUT");//获得div中所有的已input开始的标记
                   for(j=0;j<check.length;j++)    
                   {
                       check[j].disabled=true;
                   }
               }
        }
		</script>
</head>
<body class="body_popup" scroll="no" onload="javascript:initCheckBoxs();">
    <form id="form1" runat="server">
    <div>
       <TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1"
				style="BORDER-COLLAPSE: collapse">
				<TR>
					<TD>
						<DIV id="dvDeptBox" class="gridBox">
                            <asp:TreeView ID="tvDepartment" ShowCheckBoxes="All" runat="server"></asp:TreeView>
                        </DIV>
					</TD>
				</TR>
				<tr>
					<td height="30" align="right">
                        <table width="60%" height="100%">
							<tr align="center" valign="middle">
								<td>
                                    <asp:Button ID="btnSave" CssClass="button-normal" Text="保 存" OnClick="btnSave_Click" runat="server" /></td>
								<td><input id="BtnCancel" type="button" value="关 闭" onclick="window.parent.close();" class="button-normal"/>
                                    </td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
    
    </div>
    </form>
</body>
</html>
