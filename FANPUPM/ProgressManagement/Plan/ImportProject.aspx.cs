using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using PluSoft.Data;
using PluSoft.Utils;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class demo_Import : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDrop();
		}
	}
	protected void btnImport_Click(object sender, System.EventArgs e)
	{
		string physicalApplicationPath = base.Request.PhysicalApplicationPath;
		string text = "UploadFiles\\Gantt\\";
		string str = physicalApplicationPath + text;
		HttpPostedFile httpPostedFile = base.Request.Files["fupData"];
		string text2 = System.DateTime.Now.ToString("yyyyMMddhhmmss");
		string str2 = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf("."));
		text2 += str2;
		if (httpPostedFile.ContentLength > 0)
		{
			httpPostedFile.SaveAs(string.Format("{0}{1}{2}", physicalApplicationPath, text, text2));
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			try
			{
				System.Collections.Hashtable hashtable = PlusProject.Read(str + text2);
				Project project = new Project();
				project.Load(hashtable);
				project.OrderProjectByStart();
				System.Collections.ArrayList arrayList = (System.Collections.ArrayList)hashtable["Tasks"];
				arrayList = TreeUtil.ToTree(arrayList, "children", "UID", "ParentTaskUID");
				hashtable["Tasks"] = arrayList;
				string progressVerId = base.Request["id"];
				this.hfldProjectUID.Value = DBProject.ImportXML(progressVerId, hashtable);
				stringBuilder.Append("alert('系统提示：\\n\\nXML导入成功！');closeTab(true);");
			}
			catch (System.Exception ex)
			{
				stringBuilder.Append("alert('系统提示：\\n\\nXML导入失败," + ex.Message + "！');");
			}
			base.RegisterScript(stringBuilder.ToString());
		}
	}
	protected void BindDrop()
	{
		string progressVerId = base.Request["id"];
		DataTable allVersion = cn.justwin.BLL.ProgressManagement.Version.GetAllVersion(progressVerId);
		this.dropVersion.DataSource = allVersion;
		this.dropVersion.DataTextField = "VersionCode";
		this.dropVersion.DataValueField = "ProgressVersionId";
		this.dropVersion.DataBind();
	}
	protected void btnVersion_Click(object sender, System.EventArgs args)
	{
		string progressVerId = base.Request["id"];
		this.hfldProjectUID.Value = DBProject.ImportVersion(progressVerId, this.dropVersion.SelectedValue);
		base.RegisterScript("$('#raVersion').click();alert('系统提示：\\n\\n版本导入成功！');closeTab(true);");
	}
}


