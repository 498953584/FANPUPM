using ASP;
using cn.justwin.BLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LinkmanSearch : BasePage, IRequiresSessionState
	{
		public static bool isbtnout;
		protected string _DeptCode;
		private AddressListDb myAddress;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.myAddress = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				base.Response.Cache.SetNoStore();
				LinkmanSearch.isbtnout = false;
				if (this.Page.Request.Params["code"] == null)
				{
					DataSet subDepartment = this.myAddress.getSubDepartment(1);
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
				string str = this.myAddress.depName(this._DeptCode);
				DataTable dataSource = this.cGetDeptLinkman(Convert.ToInt32(this._DeptCode));
				this.dgLinkman.DataSource = dataSource;
				this.dgLinkman.DataBind();
				this.lblTitle.Text = str + "通讯录";
				string sqlString = "SELECT * FROM PT_d_bm  WHERE i_bmdm='" + this._DeptCode + "'";
				DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable2.Rows.Count >= 1)
				{
					this.labaddress.Text = dataTable2.Rows[0]["adss"].ToString();
					this.labyb.Text = dataTable2.Rows[0]["yb"].ToString();
					this.labfx.Text = dataTable2.Rows[0]["fx"].ToString();
				}
			}
		}
		public DataTable cGetDeptLinkman(int bmdm)
		{
			string sqlString = "select a.*,b.v_bmmc from pt_txl_nbtxl a,pt_d_bm b where a.i_bmdm = b.i_bmdm and b.i_bmdm like '" + bmdm + "%' order by b.i_bmdm, a.i_xh";
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
				if (((DataRowView)e.Item.DataItem)["v_yhdm"].ToString() == this.Page.Session["yhdm"].ToString())
				{
					((LinkButton)e.Item.Cells[7].Controls[1]).Text = "编辑";
					((LinkButton)e.Item.Cells[7].Controls[1]).Attributes["onclick"] = string.Concat(new string[]
					{
						"javascript:window.showModalDialog('Broker.aspx?Op=Mod&iDept=",
						this._DeptCode,
						"&id=",
						this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
						"',window,'dialogHeight:500px;dialogWidth:450px;center:1;help:0;status:0;');return false;"
					});
				}
				else
				{
					((LinkButton)e.Item.Cells[7].Controls[1]).Text = "";
					((LinkButton)e.Item.Cells[7].Controls[1]).Enabled = false;
				}
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"javascript:window.showModalDialog('Broker.aspx?Op=Browse&iDept=",
					this._DeptCode,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"',window,'dialogHeight:500px;dialogWidth:450px;center:1;help:0;status:0;');return false;"
				});
				e.Item.ToolTip = "双击查看详细信息";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[1].Controls[0];
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
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			string strKey = this.tbKey.Text.Trim();
			string selectedValue = this.ddlClass.SelectedValue;
			DataTable dataSource = this.cSearchLinkman(strKey, selectedValue);
			this.dgLinkman.DataSource = dataSource;
			this.dgLinkman.DataBind();
			this.lblTitle.Text = "查询结果";
		}
		public DataTable cSearchLinkman(string strKey, string strClass)
		{
			string sqlString = string.Concat(new string[]
			{
				"select a.*,b.v_bmmc from pt_txl_nbtxl a,pt_d_bm b where a.i_bmdm = b.i_bmdm and ",
				strClass,
				" like '%",
				strKey,
				"%' order by b.i_bmdm, a.i_xh "
			});
			return publicDbOpClass.DataTableQuary(sqlString);
		}
		protected void btnMessage_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (DataGridItem dataGridItem in this.dgLinkman.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbItem");
				if (checkBox.Checked && dataGridItem.Cells[5].Text != "" && dataGridItem.Cells[5].Text != "-")
				{
					text = text + dataGridItem.Cells[5].Text + ",";
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
			List<string> list = new List<string>();
			foreach (DataGridColumn dataGridColumn in this.dgLinkman.Columns)
			{
				string headerText = dataGridColumn.HeaderText;
				if (headerText != "" && headerText != "职位")
				{
					list.Add(headerText);
				}
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("V_XM");
			dataTable.Columns.Add("V_YZBM");
			dataTable.Columns.Add("V_BGDH");
			dataTable.Columns.Add("V_SJ");
			dataTable.Columns.Add("V_DZYX");
			for (int i = 0; i < this.dgLinkman.Items.Count; i++)
			{
				HyperLink hyperLink = (HyperLink)this.dgLinkman.Items[i].Cells[1].Controls[0];
				char[] trimChars = new char[]
				{
					'&',
					'n',
					'b',
					's',
					'p',
					';'
				};
				string text = hyperLink.Text;
				string text2 = this.dgLinkman.Items[i].Cells[2].Text.Trim(trimChars);
				string text3 = this.dgLinkman.Items[i].Cells[3].Text.Trim(trimChars);
				string text4 = this.dgLinkman.Items[i].Cells[4].Text.Trim(trimChars);
				string text5 = this.dgLinkman.Items[i].Cells[5].Text.Trim(trimChars);
				string[] values = new string[]
				{
					text,
					text2,
					text3,
					text4,
					text5
				};
				dataTable.Rows.Add(values);
			}
			string[] fieldName = new string[]
			{
				"V_XM",
				"V_YZBM",
				"V_BGDH",
				"V_SJ",
				"V_DZYX"
			};
			ExcelHelper.ExportExcel(dataTable, list.ToArray(), fieldName, new string[0], "内部通讯录.xls", base.Request.Browser.Browser);
		}
		private void ExportToExcel(DataGrid dg, string fileName, string typeName)
		{
			HttpResponse response = this.Page.Response;
			string browser = base.Request.Browser.Browser;
			if (browser.ToUpper() != "SAFARI")
			{
				fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
			}
			response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
			response.ContentEncoding = Encoding.GetEncoding("GB2312");
			response.ContentType = typeName;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.dgLinkman.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		private bool DownFile(HttpResponse Response, string fileName, string fullPath)
		{
			bool result;
			try
			{
				Response.ContentType = "application/octet-stream";
				Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ";charset=GB2312");
				FileStream fileStream = File.OpenRead(fullPath);
				long length = fileStream.Length;
				int num = 102400;
				byte[] buffer = new byte[num];
				if ((long)num > length)
				{
					num = Convert.ToInt32(length);
				}
				long num2 = 0L;
				bool flag = false;
				while (!flag)
				{
					if (num2 + (long)num > length)
					{
						num = Convert.ToInt32(length - num2);
						buffer = new byte[num];
						flag = true;
					}
					fileStream.Read(buffer, 0, num);
					Response.BinaryWrite(buffer);
					num2 += (long)num;
				}
				fileStream.Close();
				File.Delete(fullPath);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}


