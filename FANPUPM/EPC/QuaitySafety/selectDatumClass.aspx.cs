using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_QuaitySafety_selectDatumClass : NBasePage, IRequiresSessionState
{
	private string type = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			int rootTypeId;
			if (this.type == "12")
			{
				rootTypeId = 2;
			}
			else
			{
				rootTypeId = 3;
			}
			DatumClass datumClass = new DatumClass();
			IList<DatumClass> datumClass2 = datumClass.GetDatumClass(rootTypeId);
			string value = JsonHelper.JsonSerializer<DatumClass[]>(datumClass2.ToArray<DatumClass>());
			this.hfldDatumClassJson.Value = value;
		}
	}
}


