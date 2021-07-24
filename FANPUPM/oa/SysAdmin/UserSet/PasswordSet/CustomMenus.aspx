<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomMenus.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_CustomMenus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>无标题页</title>
    <script language="JavaScript" type="text/javascript">
			function clicktr(obj,taskID,taskCode,updName)
			{
				//document.getElementById('hdn_TaskID').value = taskID;
				//document.getElementById('hdn_TaskCode').value = taskCode;
				//document.getElementById('hdn_UpdName').value = updName			
				var table = document.getElementById('grd_ModuleList');
				for(var i=0;i<table.rows.length;i++)
				{
					if (table.rows[i].className == 'click')
					{
						table.rows[i].className = 'out';
					}
				}
				obj.className = 'click';
				//document.getElementById('UpdButton').disabled = false;
				//document.getElementById('RelButton').disabled = false;
			}
			function outtr(obj)
			{
				if (obj.className.toLowerCase()=='click')
					return;
				obj.className = 'out';
			}
			function overtr(obj)
			{
				if (obj.className.toLowerCase()=='click')
					return;
				obj.className = 'over';
			}
			function switchDisplay(obj,t1,t2)
			{
				var show = false;
				var table = document.getElementById('tbl');
				var coll = table.all(t1);
				if (coll != null){
					if (coll.length != null){
						for (i=0; i<coll.length; i++){
							if (coll(i).style.display=='none'){
								coll(i).style.display='block';
							}
							else{
								coll(i).style.display='none';
							}
						}
					}
					else
					{
						if (coll.style.display=='none'){
							coll.style.display='block';
						}
						else{
							coll.style.display='none';
						}
					}
				}
				if (obj.src.toLowerCase().indexOf("minus")>0)
				{
					obj.src = "images/" + t2+ "plus.gif";
					doTaskList(t1,'d');					
					hideChild(t1);
				}
				else
				{
					obj.src = "images/" + t2 + "minus.gif";
					doTaskList(t1,'a');
					showChild(t1);
				}
			}
			
			function showChild(value)
			{
				var table = document.getElementById('tbl');
				var task = document.getElementById('hdn_ModuleCodeList');
				var tasklist = task.value.split(',');
				var childlist
				for(var i=0;i<tasklist.length;i++){
					if ((tasklist[i].indexOf(value)==0)&&(tasklist[i]!=value)){
						childlist = table.all(tasklist[i]);
						if (childlist!=null){
							if (childlist.length){
								for (var j=0; j<childlist.length; j++){
									childlist[j].style.display='block';
								}
							}
							else
							{
								childlist.style.display='block';
							}
						}
					}
				}
			}
			
			function hideChild(value)
			{	
				var table = document.getElementById('tbl');
				var task = document.getElementById('hdn_ModuleCodeList');
				var tasklist = task.value.split(',');
				
				var childlist
				for(var i=0;i<tasklist.length;i++){
					if ((tasklist[i].indexOf(value)==0)&&(tasklist[i]!=value)){
						childlist = table.all(tasklist[i]);
						if (childlist!=null){
							if (childlist.length){
								for (var j=0; j<childlist.length; j++){
									childlist[j].style.display='none';
								}
							}
							else
							{
								childlist.style.display='none';
							}
						}
					}
				}
			}
			
			function doTaskList(value,op)
			{
				var exists = false;
				var task = document.getElementById('hdn_ModuleCodeList');
				var tasklist = task.value.split(',');
				if (op=='a')
				{
					for (var i=0;i<tasklist.length;i++)
					{
						if (tasklist[i]==value)
						{
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						tasklist.push(value);
					}
				}
				else
				{
					for (var i=0;i<tasklist.length;i++)
					{
						if (tasklist[i]==value)
						{
							tasklist.splice(i,1);
						}
					}
				}
				document.getElementById('hdn_ModuleCodeList').value = tasklist.join(',');
			}
			
			function cbClick(obj)
			{
				doCheckChild(obj,'cbn'+obj.value);
				doCheckParent(obj,'cb' + obj.value.substr(0,obj.value.length-2));
			}
			
			function doCheckParent(oriObj,nodeName)
			{
				var parList = document.getElementById(nodeName);
				if (parList)
				{
					if (oriObj.checked)
					{
						if (parList.checked)
						{
							return;
						}
						else
						{
							parList.checked = true;
							doCheckParent(oriObj,'cb' + parList.value.substr(0,parList.value.length-2));
						}
					}
					else
					{
						var doNothing = false;
						var broNodeList = document.getElementsByName('cbn' + nodeName.substr(2,nodeName.length-2));
						if (broNodeList.length>1)
						{
							for(var i =0; i<broNodeList.length;i++)
							{
								if (broNodeList[i].checked)
								{
									doNothing = true;
								}
							}
						}
						if (doNothing)
						{
							return;
						}
						else
						{
							parList.checked = false;
							doCheckParent(oriObj,'cb' + parList.value.substr(0,parList.value.length-2));
						}
					}
				}
			}
			
			function doCheckChild(oriObj,nodeName)
			{
				var childNodeName;
				var subList = document.getElementsByName(nodeName);
				if (subList.length>0)
				{
					for (var i=0;i<subList.length;i++)
					{
						subList[i].checked = oriObj.checked;
						doCheckChild(oriObj,'cbn' + subList[i].value);
					}
				}
			}
			function collectModules()
			{
				var moduleList = ,;
				var cbList = document.getElementsByTagName("INPUT");
				//alert(cbList);
				for (var i=0;i<cbList.length;i++)
				{
					if (cbList[i].type == "checkbox")
					{
						if (cbList[i].checked)
						{
						    cbName = cbList[i].name;
						    //如果是菜单项
						    if(cbName.substr(0,3) == "cbn")
						    {
						        moduleList += cbList[i].value + ",";
						    }							
						}
					}
				}
				document.getElementById('hdn_Modules').value = moduleList;
			}
		</script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" height="100%" cellspacing="1" cellpadding="1" width="100%" border="1">
				<tr>
                <td class="td-title">常用菜单设置
                </td>
            </tr>
           <tr>
					<td>
						<div id="ModuleListBox" class="gridBox">
                    <asp:Table ID="tbl" Width="100%" BorderWidth="1px" BorderStyle="Dotted" runat="server"></asp:Table>
                    </div>
                </td>
            </tr>
            <tr>
					<td height="20" class="td-submit">
                    <asp:Button ID="btnConfirm" Text="修 改" OnClick="btnConfirm_Click" runat="server" />
                    <input id="Reset1" type="reset" value="还 原" /></td>
            </tr>
        </table><input id="hdn_ModuleCodeList" style="WIDTH: 10px" type="hidden" name="hdn_ModuleCodeList" runat="server" />
 <input id="hdn_Modules" style="WIDTH: 10px" type="hidden" name="hdn_Modules" runat="server" />

    </form>
</body>
</html>
