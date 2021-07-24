using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WebForm9 : BasePage, IRequiresSessionState
	{
		private const int TotalCols = 4;
		private const string ImaHeight = "62";
		private const string FontHeight = "25";
		private const string SpaceHeight = "20";
		private const string bgStyle = "font-size:14;cursor:hand";
		private const string imageID = "_image";
		private const string fontID = "_font";
		public string str_title = "";
		private ModuleCollection allModuleList = new ModuleCollection();
		private ModuleCollection PartList = new ModuleCollection();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack && this.Session["yhdm"] != null)
			{
				base.Response.Redirect("ShowInfomation.aspx");
				this.allModuleList = ModuleAction.GetUserModule(this.Session["yhdm"].ToString(), this.Session["pttest"].ToString());
				if (base.Request["id"] != null)
				{
					this.PartList = ModuleAction.GetOneLevelByID(base.Request["id"]);
				}
				else
				{
					this.PartList = ModuleAction.GetOneLevel();
				}
				HtmlTableRow htmlTableRow = new HtmlTableRow();
				HtmlTableRow htmlTableRow2 = new HtmlTableRow();
				int num;
				if (base.Request["id"] != null)
				{
					num = 1;
					if (this.PartList.Count != 0)
					{
						this.str_title = this.PartList[0].ModuleName;
					}
				}
				else
				{
					num = 0;
				}
				int i;
				for (i = num; i < this.PartList.Count; i++)
				{
					if (i != num && (i - num) % 4 == 0)
					{
						this.table_Navigate.Rows.Add(htmlTableRow);
						this.table_Navigate.Rows.Add(htmlTableRow2);
						this.RenderSpaceRow(this.table_Navigate);
						htmlTableRow = new HtmlTableRow();
						htmlTableRow2 = new HtmlTableRow();
					}
					this.RenderImageRow(htmlTableRow, this.PartList[i]);
					this.RenderFontRow(htmlTableRow2, this.PartList[i]);
				}
				this.table_Navigate.Rows.Add(htmlTableRow);
				this.table_Navigate.Rows.Add(htmlTableRow2);
				this.RenderSpaceRow(this.table_Navigate);
				if ((i - num) % 4 != 0)
				{
					for (i = (i - num) % 4; i < 4; i++)
					{
						this.RenderEmptyCell(htmlTableRow, "image");
						this.RenderEmptyCell(htmlTableRow2, "font");
					}
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void RenderTitleRow(Module info)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			this.table_Navigate.Rows.Add(htmlTableRow);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.VAlign = "center";
			htmlTableCell.ColSpan = 2;
			htmlTableCell.Height = "40";
			htmlTableCell.Attributes["style"] = "font-size:18;color:#002eb6;BACKGROUND:url(images/HeadIco.JPG) no-repeat;";
			string str = "";
			for (int i = 0; i < 8; i++)
			{
				str += "&nbsp;";
			}
			htmlTableCell.InnerHtml = str + info.ModuleName;
		}
		private void RenderImageRow(HtmlTableRow tr, Module info)
		{
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ColSpan = 1;
			htmlTableCell.Align = "center";
			htmlTableCell.Height = "62";
			htmlTableCell.Attributes["style"] = "font-size:14;cursor:hand";
			HtmlImage htmlImage = new HtmlImage();
			htmlImage.ID = info.ModuleCode + "_image";
			if (info.ImagePath.Trim() != "")
			{
				if (info.ImagePath[0] != '/')
				{
					htmlImage.Src = info.ImagePath;
				}
				else
				{
					htmlImage.Src = info.ImagePath;
				}
			}
			else
			{
				htmlImage.Src = "../../images/ico.gif";
			}
			htmlTableCell.Controls.Add(htmlImage);
			htmlImage.Attributes["onmouseover"] = "imageOver(this,'" + info.ModuleCode + "');";
			htmlImage.Attributes["onmouseout"] = "imageOut(this,'" + info.ModuleCode + "');";
			if (info.ChildNum.Trim() != "0")
			{
				htmlImage.Attributes["onclick"] = "goWhere('" + info.ModuleCode + "','');";
			}
			else
			{
				if (this.allModuleList.IndexOf(info.ModuleCode) != null)
				{
					if (info.FilePath.Length > 0 && info.FilePath[0] != '/')
					{
						htmlImage.Attributes["onclick"] = string.Concat(new string[]
						{
							"goWhere('",
							info.ModuleCode,
							"','../",
							info.FilePath,
							"');"
						});
					}
					else
					{
						if (info.FilePath.Length > 0 && info.FilePath[0] == '/')
						{
							htmlImage.Attributes["onclick"] = string.Concat(new string[]
							{
								"goWhere('",
								info.ModuleCode,
								"','..",
								info.FilePath,
								"');"
							});
						}
					}
				}
				else
				{
					htmlImage.Attributes["onclick"] = "alert('您没有访问的权限！');";
				}
			}
			tr.Cells.Add(htmlTableCell);
		}
		private void RenderFontRow(HtmlTableRow tr, Module info)
		{
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ColSpan = 1;
			htmlTableCell.ID = info.ModuleCode + "_font";
			htmlTableCell.Attributes["style"] = "font-size:14;cursor:hand";
			htmlTableCell.Attributes["onmouseover"] = "fontOver(this,'" + info.ModuleCode + "');";
			htmlTableCell.Attributes["onmouseout"] = "fontOut(this,'" + info.ModuleCode + "');";
			if (info.ChildNum.Trim() != "0")
			{
				htmlTableCell.Attributes["onclick"] = "goWhere('" + info.ModuleCode + "','');";
			}
			else
			{
				if (this.allModuleList.IndexOf(info.ModuleCode) != null)
				{
					if (info.FilePath.Length > 0 && info.FilePath[0] != '/')
					{
						htmlTableCell.Attributes["onclick"] = string.Concat(new string[]
						{
							"goWhere('",
							info.ModuleCode,
							"','../",
							info.FilePath,
							"');"
						});
					}
					else
					{
						if (info.FilePath.Length > 0 && info.FilePath[0] == '/')
						{
							htmlTableCell.Attributes["onclick"] = string.Concat(new string[]
							{
								"goWhere('",
								info.ModuleCode,
								"','..",
								info.FilePath,
								"');"
							});
						}
					}
				}
				else
				{
					htmlTableCell.Attributes["onclick"] = "alert('您没有访问的权限！');";
				}
			}
			htmlTableCell.Align = "center";
			htmlTableCell.Height = "25";
			htmlTableCell.InnerText = info.ModuleName;
			tr.Cells.Add(htmlTableCell);
		}
		private void RenderSpaceRow(HtmlTable table)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			table.Rows.Add(htmlTableRow);
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.ColSpan = 4;
			htmlTableCell.Height = "20";
			htmlTableCell.InnerHtml = "&nbsp;";
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			table.Rows.Add(htmlTableRow2);
			htmlTableRow2.Cells.Add(htmlTableCell2);
			htmlTableCell2.ColSpan = 4;
			htmlTableCell2.Height = "1";
			htmlTableCell2.Attributes["background"] = "images/dotline.jpg";
		}
		private void RenderEmptyCell(HtmlTableRow tr, string type)
		{
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.Attributes["style"] = "font-size:14;cursor:hand";
			htmlTableCell.ColSpan = 1;
			htmlTableCell.Align = "center";
			if (type == "image")
			{
				htmlTableCell.Height = "62";
			}
			else
			{
				htmlTableCell.Height = "25";
			}
			tr.Cells.Add(htmlTableCell);
		}
	}


