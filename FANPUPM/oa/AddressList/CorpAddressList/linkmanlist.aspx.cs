using ASP;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LinkmanList : BasePage, IRequiresSessionState
	{
		private PtYhmcBll yhmc = new PtYhmcBll();
		protected AddressListDb ald;
		protected string _DeptCode;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				this.Session["HumanCode"] = "";
				this.Session["HumanName"] = "";
				if (this.Page.Request.Params["code"] == null)
				{
					DataSet subDepartment = this.ald.getSubDepartment(1);
					DataTable dataTable = subDepartment.Tables[0];
					if (dataTable.Rows.Count == 0)
					{
						this._DeptCode = "1";
					}
					else
					{
						this._DeptCode = dataTable.Rows[0]["i_bmdm"].ToString();
					}
				}
				else
				{
					if (this.Page.Request.Params["code"] != null)
					{
						this._DeptCode = this.Page.Request.Params["code"].ToString();
					}
				}
				this.hdnDept.Value = this._DeptCode;
				this.BindDepInfo();
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('真的要删除选定记录吗？')){return false;}";
			}
		}
		private void BindDepInfo()
		{
			userManageDb userManageDb = new userManageDb();
			string str = userManageDb.depName(this.hdnDept.Value);
			DataTable dataSource = this.cGetDeptLinkman(Convert.ToInt32(this.hdnDept.Value));
			this.dgLinkman.DataSource = dataSource;
			this.dgLinkman.DataBind();
			this.lblTitle.Text = str + "通讯录";
			string sqlString = "SELECT * FROM PT_d_bm  WHERE i_bmdm='" + this.hdnDept.Value + "'";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count >= 1)
			{
				if (dataTable.Rows[0]["adss"].ToString().Trim() == "")
				{
					this.labaddress.Text = "--------------------";
				}
				else
				{
					this.labaddress.Text = dataTable.Rows[0]["adss"].ToString();
				}
				if (dataTable.Rows[0]["yb"].ToString().Trim() == "")
				{
					this.labyb.Text = "--------------------";
				}
				else
				{
					this.labyb.Text = dataTable.Rows[0]["yb"].ToString();
				}
				if (dataTable.Rows[0]["fx"].ToString().Trim() == "")
				{
					this.labfx.Text = "--------------------";
					return;
				}
				this.labfx.Text = dataTable.Rows[0]["fx"].ToString();
			}
		}
		public DataTable cGetDeptLinkman(int bmdm)
		{
			string sqlString;
			if (bmdm == 1)
			{
				sqlString = "select a.*,b.v_bmmc from pt_txl_nbtxl a,pt_d_bm b where a.i_bmdm = b.i_bmdm order by b.i_bmdm, a.i_xh";
			}
			else
			{
				sqlString = "select a.*,b.v_bmmc from pt_txl_nbtxl a,pt_d_bm b where a.i_bmdm = b.i_bmdm and b.i_bmdm='" + bmdm + "' order by b.i_bmdm, a.i_xh";
			}
			return publicDbOpClass.DataTableQuary(sqlString);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgLinkman.ItemDataBound += new DataGridItemEventHandler(this.dgLinkman_ItemDataBound);
		}
		private void dgLinkman_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex == -1)
			{
				CheckBox checkBox = (CheckBox)e.Item.FindControl("cbSelAll");
				if (checkBox != null)
				{
					checkBox.Attributes.Add("onclick", "selectAll(this)");
					return;
				}
			}
			else
			{
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				string a = ((DataRowView)e.Item.DataItem)["c_sjbs"].ToString();
				if (a == "n")
				{
					e.Item.Cells[6].Text = ((DataRowView)e.Item.DataItem)["v_sj"].ToString();
				}
				else
				{
					if (a == "y" && ((DataRowView)e.Item.DataItem)["v_sj"].ToString() != "")
					{
						e.Item.Cells[6].Text = "-";
					}
				}
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"javascript:window.showModalDialog('Broker.aspx?Op=Browse&iDept=",
					this._DeptCode,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"',window,'dialogHeight:500px;dialogWidth:450px;center:1;help:0;status:0;');return false;"
				});
				e.Item.ToolTip = "双击查看详细信息";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[2].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = string.Concat(new string[]
				{
					"javascript:void(window.open('Broker.aspx?Op=Browse&iDept=",
					this._DeptCode,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"','','left=100,top=100,width=450,height=500,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));"
				});
			}
		}
		protected void dgLinkman_PreRender(object sender, EventArgs e)
		{
			string text = "";
			text += "function selectAll(obj)\n";
			text += "{\n";
			text += "\t\tvar num = document.all.tags(\"input\").length;\n";
			text += "\t\tvar str_temp;\n";
			text += "\t\t//设置子模块复选框\n";
			text += "\t\tfor(var i=0; i<num; i++)\n";
			text += "\t\t{\n";
			text += "\t\t\tstr_temp = document.all.tags(\"input\")[i].id;\n";
			text += "\t\t\tif(str_temp.substr(str_temp.length-6,6) == 'cbItem')\n";
			text += "\t\t\t{\n";
			text += "\t\t\t\tdocument.all.tags(\"input\")[i].checked = obj.checked;\n";
			text += "\t\t\t}\n";
			text += "\t\t}\n";
			text += "\t}";
			this.js.Text = text;
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			foreach (DataGridItem dataGridItem in this.dgLinkman.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbItem");
				if (checkBox.Checked)
				{
					string id = this.dgLinkman.DataKeys[dataGridItem.ItemIndex].ToString();
					this.ald.cDelLinkman(id);
				}
			}
			if (this.hdnDept.Value != "")
			{
				DataTable dataSource = this.cGetDeptLinkman(Convert.ToInt32(this.hdnDept.Value));
				this.dgLinkman.DataSource = dataSource;
				this.dgLinkman.DataBind();
			}
		}
		protected void btnMessage_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (DataGridItem dataGridItem in this.dgLinkman.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbItem");
				if (checkBox.Checked && dataGridItem.Cells[6].Text != "" && dataGridItem.Cells[6].Text != "-")
				{
					text = text + dataGridItem.Cells[6].Text + ",";
				}
			}
			text = text.Trim(new char[]
			{
				','
			});
			string text2 = "";
			text2 += "<script language=javascript>";
			text2 += "var strUrl = parent.location.href;";
			text2 = text2 + "parent.location.href='../SendMsg.aspx?strHandset=" + text + "&srcUrl='+ strUrl;";
			text2 += "</script>";
			this.Page.RegisterStartupScript("redirect", text2);
		}
		protected void btnExp_Click1(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "内部通讯录.xls");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.dgLinkman.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		protected void btnModfAdd_Click(object sender, EventArgs e)
		{
			try
			{
				string text = this.Session["HumanCode"].ToString();
				string[] array = text.Split(new char[]
				{
					'!',
					':'
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					if (text2.Length == 8)
					{
						PtYhmc modelById = this.yhmc.GetModelById(text2);
						string yhdm = this.Page.Session["yhdm"].ToString();
						string v_xm = modelById.v_xm;
						int i_bmdm = modelById.i_bmdm;
						string zw = "";
						string address = modelById.Address;
						string postcode = modelById.Postcode;
						string bgdh = "";
						string tel = modelById.Tel;
						string zdbs = "n";
						string mobilePhoneCode = modelById.MobilePhoneCode;
						string sjbs = "n";
						string cz = "";
						string xb = "m";
						if (modelById.Sex == "1")
						{
							xb = "m";
						}
						else
						{
							if (modelById.Sex == "2")
							{
								xb = "f";
							}
						}
						string dzyx = "";
						string wlch = "";
						string bz = "";
						string xh = "0";
						this.ald.cAddLinkman(yhdm, v_xm, i_bmdm, zw, address, postcode, bgdh, tel, zdbs, mobilePhoneCode, sjbs, cz, xb, dzyx, wlch, bz, xh);
					}
				}
				this.BindData();
			}
			catch
			{
				this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('系统提示：\\n\\n添加失败！');</script>");
			}
		}
		public void BindData()
		{
			DataTable dataSource = this.cGetDeptLinkman(Convert.ToInt32(this.hdnDept.Value));
			this.dgLinkman.DataSource = dataSource;
			this.dgLinkman.DataBind();
		}
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			this.BindData();
			this.BindDepInfo();
		}
	}


