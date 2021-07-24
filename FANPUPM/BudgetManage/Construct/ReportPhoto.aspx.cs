using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ReportPhoto : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private static string constructReport = ConfigHelper.Get("ConstructReport");
	public string imaStr = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	protected void trvwBudget_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hfldTaskId.Value = this.trvwBudget.SelectedValue;
		this.DisplayPhoto();
	}
	private void DisplayPhoto()
	{
		string selectedValue = this.trvwBudget.SelectedValue;
		if (!string.IsNullOrEmpty(selectedValue))
		{
			BudTask byId = BudTask.GetById(selectedValue);
			IList<string> reportImgPathList = byId.GetReportImgPathList();
			using (IEnumerator<string> enumerator = reportImgPathList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					string path = HttpContext.Current.Server.MapPath(current);
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					if (directoryInfo.Exists)
					{
						FileInfo[] files = directoryInfo.GetFiles("*.*");
						FileInfo[] array = files;
						for (int i = 0; i < array.Length; i++)
						{
							FileInfo fileInfo = array[i];
							string text = current + "/" + fileInfo.ToString();
							string text2 = fileInfo.ToString().Substring(0, fileInfo.ToString().IndexOf('.'));
							string text3 = this.imaStr;
							this.imaStr = string.Concat(new string[]
							{
								text3,
								"<SPAN class=\"photo\"><a href=\"",
								text,
								"\" title=\"",
								text2,
								"\"><IMG src=\"",
								text,
								"\" width=\"100\" height=\"100\" alt=\"\" ></A></a></SPAN>"
							});
						}
					}
				}
				return;
			}
		}
		string value = this.hdnProjectCode.Value;
		IList<ConstructReport> auditedByPrj = ConstructReport.GetAuditedByPrj(value);
		Dictionary<string[], DateTime> dictionary = new Dictionary<string[], DateTime>();
		foreach (ConstructReport current2 in auditedByPrj)
		{
			foreach (ConstructTask current3 in current2.ConstructTaskList)
			{
				string text4 = BudgetManage_Construct_ReportPhoto.constructReport + "/" + current3.Id;
				string path2 = HttpContext.Current.Server.MapPath(text4);
				DirectoryInfo directoryInfo2 = new DirectoryInfo(path2);
				if (directoryInfo2.Exists)
				{
					FileInfo[] files2 = directoryInfo2.GetFiles("*.*");
					FileInfo[] array2 = files2;
					for (int j = 0; j < array2.Length; j++)
					{
						FileInfo fileInfo2 = array2[j];
						string[] array3 = new string[2];
						DateTime creationTime = fileInfo2.CreationTime;
						string text5 = text4 + "/" + fileInfo2.ToString();
						string text6 = fileInfo2.ToString().Substring(0, fileInfo2.ToString().IndexOf('.'));
						array3[0] = text5;
						array3[1] = text6;
						dictionary.Add(array3, creationTime);
					}
				}
			}
		}
		IOrderedEnumerable<KeyValuePair<string[], DateTime>> orderedEnumerable =
			from s in dictionary
			orderby s.Value descending
			select s;
		int num = 0;
		foreach (KeyValuePair<string[], DateTime> current4 in orderedEnumerable)
		{
			if (num >= 9)
			{
				break;
			}
			string[] key = current4.Key;
			DateTime arg_2DD_0 = current4.Value;
			string text7 = this.imaStr;
			this.imaStr = string.Concat(new string[]
			{
				text7,
				"<SPAN class=\"photo\"><a href=\"",
				key[0],
				"\" title=\"",
				key[1],
				"\"><IMG src=\"",
				key[0],
				"\" width=\"100\" height=\"100\" alt=\"\" ></A></a></SPAN>"
			});
			num++;
		}
	}
	private void BindTrvwBudget()
	{
		this.trvwBudget.Nodes.Clear();
		string value = this.hdnProjectCode.Value;
		if (!string.IsNullOrEmpty(value))
		{
			IList<BudTask> firstLayerList = BudTask.GetFirstLayerList(value);
			foreach (BudTask current in firstLayerList)
			{
				TreeNode treeNode = new TreeNode(current.Name, current.Id);
				IList<BudTask> listByParentId = BudTask.GetListByParentId(treeNode.Value);
				if (listByParentId.Count > 0)
				{
					this.AddChildToTrvwBudget(treeNode, listByParentId);
				}
				this.trvwBudget.Nodes.Add(treeNode);
			}
			this.trvwBudget.ExpandAll();
		}
	}
	private void AddChildToTrvwBudget(TreeNode parentNode, IList<BudTask> budTaskList)
	{
		foreach (BudTask current in budTaskList)
		{
			TreeNode treeNode = new TreeNode(current.Name, current.Id);
			IList<BudTask> listByParentId = BudTask.GetListByParentId(current.Id);
			if (listByParentId.Count > 0)
			{
				this.AddChildToTrvwBudget(treeNode, listByParentId);
			}
			else
			{
				treeNode.ToolTip = treeNode.Text;
			}
			parentNode.ChildNodes.Add(treeNode);
		}
	}
	protected void btnDisplayBud_Click(object sender, EventArgs e)
	{
		this.BindTrvwBudget();
		this.DisplayPhoto();
	}
}


