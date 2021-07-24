using ASP;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
	public partial class Refresh : Page, IRequiresSessionState
	{
		protected string interval = ConfigurationSettings.AppSettings["RefreshTime"];

		protected void Page_Load(object sender, EventArgs e)
		{
			string text = "";
			DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
			if (dataTable != null)
			{
				base.Response.Write("count: " + dataTable.Rows.Count.ToString());
			}
			string text2;
			if (base.Application["UserCodeCollection"] != null)
			{
				text2 = ((DataTable)base.Application["UserCodeCollection"]).Rows.Count.ToString();
			}
			else
			{
				text2 = "0";
			}
			if (this.Session["userOperationLastTime"] != null)
			{
				int num = this.ExcessTime();
				if ((int)(DateTime.Now - (DateTime)this.Session["userOperationLastTime"]).TotalSeconds > num * 60)
				{
					this.Session.Remove("yhdm");
				}
			}
			if (this.Session["yhdm"] != null)
			{
				text = "联机";
			}
			else
			{
				if (this.Session["yhdm"] == null || text2 == "0")
				{
					text = "脱机";
					if (!this.Page.IsStartupScriptRegistered("loginOut"))
					{
						this.Page.RegisterStartupScript("loginOut", "<script language='javascript'>loginOut();</script>");
					}
				}
			}
			string text3 = "function rl(){\n";
			string text4 = text3;
			text3 = string.Concat(new string[]
			{
				text4,
				"document.location.reload();editUserInfo('",
				text2,
				"','",
				text,
				"');"
			});
			text3 = text3 + "setTimeout(rl," + this.interval + ");}\n";
			text3 = text3 + "setTimeout(rl," + this.interval + ");";
			this.js.Text = text3;
			if (this.Session["yhdm"] != null)
			{
				this.UpdateSingleUserMessage(this.Session["yhdm"].ToString());
				this.CheckUserCode();
			}
			this.DeleteOutTimeUserMessage();
		}
		private void UpdateSingleUserMessage(string userCode)
		{
			if (base.Application["UserCodeCollection"] != null)
			{
				DateTime now = DateTime.Now;
				DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (dataTable.Rows[i]["userCode"].ToString() == userCode)
					{
						base.Application.Lock();
						dataTable.Rows[i]["endTime"] = now;
						base.Application["UserCodeCollection"] = dataTable;
						base.Application.UnLock();
						return;
					}
				}
			}
		}
		private void DeleteOutTimeUserMessage()
		{
			if (base.Application["UserCodeCollection"] != null)
			{
				base.Application.Lock();
				DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
				DateTime now = DateTime.Now;
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					TimeSpan timeSpan = now - DateTime.Parse(dataTable.Rows[i]["endTime"].ToString());
					int num = int.Parse(this.interval);
					if ((int)timeSpan.TotalSeconds * 1000 > num * 5)
					{
						arrayList.Add(dataTable.Rows[i]["userCode"].ToString());
					}
				}
				for (int j = 0; j < arrayList.Count; j++)
				{
					for (int k = 0; k < dataTable.Rows.Count; k++)
					{
						if (dataTable.Rows[k]["userCode"].ToString().Trim() == arrayList[j].ToString().Trim())
						{
							dataTable.Rows.RemoveAt(k);
						}
					}
				}
				base.Application["UserCodeCollection"] = dataTable;
				base.Application.UnLock();
			}
		}
		private void CheckUserCode()
		{
			string userHostAddress = HttpContext.Current.Request.UserHostAddress;
			if (base.Application["TempUserCodeCollection"] != null)
			{
				base.Application.Lock();
				DataTable dataTable = (DataTable)base.Application["TempUserCodeCollection"];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (dataTable.Rows[i]["userCode"].ToString().Trim() == this.Session["yhdm"].ToString().Trim() && dataTable.Rows[i]["ip"].ToString().Trim() != userHostAddress.Trim())
					{
						DataTable dataTable2 = (DataTable)base.Application["UserCodeCollection"];
						foreach (DataRow dataRow in dataTable2.Rows)
						{
							if (dataRow["userCode"].ToString() == dataTable.Rows[i]["userCode"].ToString())
							{
								dataRow["ip"] = dataTable.Rows[i]["ip"];
								dataRow["loginTime"] = dataTable.Rows[i]["loginTime"];
								dataRow["endTime"] = dataTable.Rows[i]["endTime"];
								break;
							}
						}
						base.Application["UserCodeCollection"] = dataTable2;
						base.Application["TempUserCodeCollection"] = dataTable;
						if (!this.Page.IsStartupScriptRegistered("returnOut"))
						{
							this.Page.RegisterStartupScript("returnOut", "<script language='javascript'>returnOut('" + dataTable.Rows[i]["ip"].ToString() + "');</script>");
						}
						dataTable.Rows[i].Delete();
						return;
					}
				}
			}
		}
		private int ExcessTime()
		{
			string filename = base.Request.MapPath(base.Request.ApplicationPath) + "\\web.config";
			XmlDocument xmlDocument = new XmlDocument();
			int result;
			try
			{
				if (this.Session["ExcessTime"] != null)
				{
					result = (int)this.Session["ExcessTime"];
				}
				else
				{
					xmlDocument.Load(filename);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//sessionState[@timeout]");
					if (xmlNodeList.Count != 0)
					{
						this.Session["ExcessTime"] = int.Parse(xmlNodeList.Item(0).Attributes.GetNamedItem("timeout").Value);
					}
					else
					{
						this.Session["ExcessTime"] = 20;
					}
					result = (int)this.Session["ExcessTime"];
				}
			}
			catch
			{
				throw new Exception("读取Web.Config文件中Session过期配置节出错！请查看Web.Config文件中的sessionState配置节");
			}
			return result;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


