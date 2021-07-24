//此为记载某一个记录行的全局变量
var RecordObj = null;
var OverRecordObj = null;

function OnRecord(obj)
{
	obj.className = 'trm_focus';
	if( RecordObj != null && RecordObj != obj)
	{
		RecordObj.className = 'trm_out';
	}
	RecordObj = obj;				
}

function OnMouseOverRecord(obj)
{			
	if(obj != RecordObj)
	{	
		obj.className = 'trm_over';
	}
	
	if( OverRecordObj != null && OverRecordObj != RecordObj)
	{
		OverRecordObj.className = 'trm_out';
	}
	
	OverRecordObj = obj;
}

function ChkObj()
{
	if(RecordObj==null)
	{
		alert('请选择记录!');
		return false;
	}
	else
	{
		return true;
	}
}
/*
*hxp
*2008-5-8
*实现增、删、改、查询、查看、快捷捷
*/

