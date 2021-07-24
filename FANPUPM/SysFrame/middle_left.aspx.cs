using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class middle_left : PageBase, IRequiresSessionState
	{
		private const int TotalWidth = 187;
		private const int ImageWidth = 32;
		private const int Height = 25;
		private const int BaseWidth = 23;
		private const int LineWidth = 2;
		private const int EmptyWidth = 16;
		private const int RightWidth = 5;
		private const string OneTdID = "image1";
		private const string TwoTdID = "image2";
		private const string tdStyle = "background:url(images/FontBG.gif) no-repeat";
		private ModuleCollection module = new ModuleCollection();
		private bool IsFirstSubNode;
		public string strSkinPath = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["SkinID"] != null)
			{
				this.Session["SkinID"] = "4";
			}
			this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
			this.hdnPath.Value = this.strSkinPath;
			if (!this.Page.IsPostBack && this.Session["yhdm"] != null)
			{
				this.module = ModuleAction.RenderTreeModuleListGather(this.Session["yhdm"].ToString(), this.Session["pttest"].ToString());
				this.RenderModuleList(this.module);
				return;
			}
			base.Response.Write("登陆时间过期，请退出重新登陆！");
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void RenderModuleList(ModuleCollection module)
		{
			bool addEmpty = false;
			foreach (Module module2 in module)
			{
				if (module2.Level == 1)
				{
					this.RenderOneLevelRow(module2, addEmpty);
					addEmpty = true;
					this.IsFirstSubNode = true;
				}
				else
				{
					bool bottomNode = module.IndexOf(module2.ModuleCode.Substring(0, module2.ModuleCode.Length - 2)).BottomNode;
					if (module2.ChildNum.Trim() != "0")
					{
						if (module2.BottomNode)
						{
							this.RenderEndChildNode(module2, bottomNode);
						}
						else
						{
							this.RenderNoEndChildNode(module2, bottomNode);
						}
					}
					else
					{
						if (module2.BottomNode)
						{
							this.RenderEndLeafNode(module2, bottomNode);
						}
						else
						{
							this.RenderNoEndLeafNode(module2, bottomNode);
						}
					}
				}
			}
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableRow = new HtmlTableRow();
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.Attributes["class"] = "menuTr1";
			htmlTableCell.Height = "1";
			htmlTableCell.ColSpan = 187;
			htmlTableRow.Cells.Add(htmlTableCell);
			this.ModuleList.Controls.Add(htmlTableRow);
		}
		private void RenderOneLevelRow(Module info, bool AddEmpty)
		{
			HtmlTableRow htmlTableRow;
			HtmlTableCell htmlTableCell;
			if (AddEmpty)
			{
				htmlTableRow = new HtmlTableRow();
				htmlTableCell = new HtmlTableCell();
				htmlTableCell.Attributes["class"] = "menuTr1";
				htmlTableCell.Height = "1";
				htmlTableCell.ColSpan = 187;
				htmlTableRow.Cells.Add(htmlTableCell);
				this.ModuleList.Controls.Add(htmlTableRow);
			}
			htmlTableRow = new HtmlTableRow();
			htmlTableRow.Height = 25.ToString();
			htmlTableRow.ID = info.ModuleCode;
			string text = this.strSkinPath + "/" + info.ImagePath;
			string str = "";
			if (text.LastIndexOf(".") != -1)
			{
				str = text.Substring(0, text.LastIndexOf("."));
			}
			htmlTableRow.Attributes["onclick"] = "ClickOneLevelNode(this,'" + str + "');";
			htmlTableRow.Attributes["onmouseover"] = "OnMouseOverOneLevel(this,'" + str + "');";
			htmlTableRow.Attributes["onmouseout"] = "OnMouseOutOneLevel(this,'" + str + "');";
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.VAlign = "middle";
			htmlTableCell.Width = "5px";
			htmlTableCell.ID = info.ModuleCode + "image1";
			htmlTableCell.Attributes["style"] = "background:url(" + str + "_1.gif) no-repeat";
			htmlTableCell.ColSpan = 187;
			htmlTableCell.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + info.ModuleName;
			htmlTableRow.Controls.Add(htmlTableCell);
			this.ModuleList.Controls.Add(htmlTableRow);
		}
		private void RenderNoEndChildNode(Module info, bool IsBottom)
		{
			this.AddIntervalLine(info.ModuleCode);
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.Height = 25.ToString();
			htmlTableRow.ID = info.ModuleCode;
			htmlTableRow.Attributes["style"] = "display:none";
			htmlTableRow.Attributes["onclick"] = "ClickChildNode(this,'');";
			htmlTableRow.Attributes["onmouseover"] = " OnMouseOverChild(this);";
			htmlTableRow.Attributes["onmouseout"] = "OnMouseOutChild(this);";
			int num = 23;
			this.AddLeftLine(htmlTableRow, ref num);
			this.AddSpaceCell(htmlTableRow, ref num);
			HtmlTableCell htmlTableCell;
			for (int i = 0; i < info.ModuleCode.Length / 2 - 2; i++)
			{
				htmlTableCell = new HtmlTableCell();
				htmlTableCell.ColSpan = 23;
				htmlTableCell.Attributes["style"] = "background:url(" + this.strSkinPath + "/Erect.gif) no-repeat";
				num += htmlTableCell.ColSpan;
				htmlTableRow.Controls.Add(htmlTableCell);
			}
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image1";
			htmlTableCell.ColSpan = 32;
			htmlTableCell.Attributes["style"] = "background:url(" + this.strSkinPath + "/NormalClose.gif) no-repeat";
			htmlTableRow.Controls.Add(htmlTableCell);
			num += htmlTableCell.ColSpan;
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image2";
			htmlTableCell.Attributes["style"] = "background:url(images/FontBG.gif) no-repeat";
			htmlTableCell.ColSpan = 187 - (num + 5);
			htmlTableCell.InnerText = info.ModuleName;
			htmlTableCell.VAlign = "middle";
			htmlTableRow.Controls.Add(htmlTableCell);
			this.AddLeftLine(htmlTableRow);
			this.ModuleList.Controls.Add(htmlTableRow);
		}
		private void RenderEndChildNode(Module info, bool IsBottom)
		{
			this.AddIntervalLine(info.ModuleCode);
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.Height = 25.ToString();
			htmlTableRow.ID = info.ModuleCode;
			htmlTableRow.Attributes["style"] = "display:none";
			htmlTableRow.Attributes["onclick"] = "ClickChildNode(this,'');";
			htmlTableRow.Attributes["onmouseover"] = " OnMouseOverChild(this);";
			htmlTableRow.Attributes["onmouseout"] = "OnMouseOutChild(this);";
			int num = 0;
			this.AddLeftLine(htmlTableRow, ref num);
			this.AddSpaceCell(htmlTableRow, ref num);
			num += this.ControlErectOrEmpty(htmlTableRow, info.ModuleCode);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.Width = "25px";
			htmlTableCell.ID = info.ModuleCode + "image1";
			htmlTableCell.ColSpan = 32;
			htmlTableCell.Attributes["style"] = "background:url(" + this.strSkinPath + "/EndClose.gif) no-repeat";
			htmlTableRow.Controls.Add(htmlTableCell);
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image2";
			htmlTableCell.Attributes["style"] = "background:url(images/FontBG.gif) no-repeat";
			htmlTableCell.InnerText = info.ModuleName;
			htmlTableCell.VAlign = "middle";
			htmlTableCell.ColSpan = 187 - (num + 32 + 5);
			htmlTableRow.Controls.Add(htmlTableCell);
			this.AddLeftLine(htmlTableRow);
			this.ModuleList.Controls.Add(htmlTableRow);
		}
		private void RenderNoEndLeafNode(Module info, bool IsBottom)
		{
			this.AddIntervalLine(info.ModuleCode);
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.Height = 25.ToString();
			htmlTableRow.ID = info.ModuleCode;
			htmlTableRow.Attributes["style"] = "display:none";
			this.SetPath(htmlTableRow, info.FilePath, info.ModuleName);
			htmlTableRow.Attributes["onmouseover"] = " OnMouseOverChild(this);";
			htmlTableRow.Attributes["onmouseout"] = "OnMouseOutChild(this);";
			int num = 0;
			this.AddLeftLine(htmlTableRow, ref num);
			this.AddSpaceCell(htmlTableRow, ref num);
			num += this.ControlErectOrEmpty(htmlTableRow, info.ModuleCode);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image1";
			htmlTableCell.ColSpan = 32;
			htmlTableCell.Attributes["style"] = "background:url(" + this.strSkinPath + "/Leaf.gif) no-repeat";
			htmlTableRow.Controls.Add(htmlTableCell);
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image2";
			htmlTableCell.Attributes["style"] = "background:url(images/FontBG.gif) no-repeat";
			htmlTableCell.ColSpan = 187 - (num + 32 + 5);
			htmlTableCell.InnerText = info.ModuleName;
			htmlTableCell.VAlign = "middle";
			htmlTableRow.Controls.Add(htmlTableCell);
			this.AddLeftLine(htmlTableRow);
			this.ModuleList.Controls.Add(htmlTableRow);
		}
		private void RenderEndLeafNode(Module info, bool IsBottom)
		{
			this.AddIntervalLine(info.ModuleCode);
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.Height = 25.ToString();
			htmlTableRow.ID = info.ModuleCode;
			htmlTableRow.Attributes["style"] = "display:none";
			this.SetPath(htmlTableRow, info.FilePath, info.ModuleName);
			htmlTableRow.Attributes["onmouseover"] = " OnMouseOverChild(this);";
			htmlTableRow.Attributes["onmouseout"] = "OnMouseOutChild(this);";
			int num = 0;
			this.AddLeftLine(htmlTableRow, ref num);
			this.AddSpaceCell(htmlTableRow, ref num);
			num += this.ControlErectOrEmpty(htmlTableRow, info.ModuleCode);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image1";
			htmlTableCell.ColSpan = 32;
			htmlTableCell.Attributes["style"] = "background:url(" + this.strSkinPath + "/EndLeaf.gif) no-repeat";
			htmlTableRow.Controls.Add(htmlTableCell);
			htmlTableCell = new HtmlTableCell();
			htmlTableCell.ID = info.ModuleCode + "image2";
			htmlTableCell.Attributes["style"] = "background:url(images/FontBG.gif) no-repeat";
			htmlTableCell.ColSpan = 187 - (num + 32 + 5);
			htmlTableCell.InnerText = info.ModuleName;
			htmlTableCell.VAlign = "middle";
			htmlTableRow.Controls.Add(htmlTableCell);
			this.AddLeftLine(htmlTableRow);
			this.ModuleList.Controls.Add(htmlTableRow);
		}
		private void SetPath(HtmlTableRow tr, string path, string title)
		{
			if (path.IndexOf("http") > -1)
			{
				tr.Attributes["onclick"] = string.Concat(new string[]
				{
					"goWhere('",
					tr.ID,
					"','",
					path,
					"','",
					title,
					"');"
				});
				return;
			}
			if (path.Length > 0 && path[0] != '/')
			{
				tr.Attributes["onclick"] = string.Concat(new string[]
				{
					"goWhere('",
					tr.ID,
					"','../",
					path,
					"','",
					title,
					"');"
				});
				return;
			}
			if (path.Length > 0 && path[0] == '/')
			{
				tr.Attributes["onclick"] = string.Concat(new string[]
				{
					"goWhere('",
					tr.ID,
					"','..",
					path,
					"','",
					title,
					"');"
				});
			}
		}
		private void AddLeftLine(HtmlTableRow tr, ref int Cols)
		{
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ColSpan = 2;
			Cols += htmlTableCell.ColSpan;
			tr.Cells.Add(htmlTableCell);
		}
		private void AddLeftLine(HtmlTableRow tr)
		{
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ColSpan = 2;
			tr.Cells.Add(htmlTableCell);
		}
		private void AddSpaceCell(HtmlTableRow tr, ref int Cols)
		{
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ColSpan = 16;
			htmlTableCell.Attributes["style"] = "background:url(images/empty.gif) no-repeat";
			Cols += htmlTableCell.ColSpan;
			tr.Controls.Add(htmlTableCell);
		}
		private int ControlErectOrEmpty(HtmlTableRow tr, string moduleCode)
		{
			int num = 0;
			for (int i = 0; i < moduleCode.Length / 2 - 2; i++)
			{
				if (this.module.IndexOf(moduleCode.Substring(0, 4 + i * 2)).BottomNode)
				{
					HtmlTableCell htmlTableCell = new HtmlTableCell();
					htmlTableCell.ColSpan = 23;
					htmlTableCell.Attributes["style"] = "background:url(images/empty.gif) no-repeat";
					num += htmlTableCell.ColSpan;
					tr.Controls.Add(htmlTableCell);
				}
				else
				{
					HtmlTableCell htmlTableCell = new HtmlTableCell();
					htmlTableCell.ColSpan = 23;
					htmlTableCell.Attributes["style"] = "background:url(" + this.strSkinPath + "/erect.gif) no-repeat";
					num += htmlTableCell.ColSpan;
					tr.Controls.Add(htmlTableCell);
				}
			}
			return num;
		}
		private void AddIntervalLine(string ModuleCode)
		{
			if (this.IsFirstSubNode)
			{
				new HtmlTableRow();
				new HtmlTableCell();
				this.IsFirstSubNode = false;
			}
		}
	}


