using ASP;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CodeEdit : PageBase, IRequiresSessionState
	{
		protected CodingAction CodingAct
		{
			get
			{
				return new CodingAction();
			}
		}
		protected int CodeID
		{
			get
			{
				object obj = this.ViewState["CODEID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["CODEID"] = value;
			}
		}
		protected int ParentCodeID
		{
			get
			{
				object obj = this.ViewState["PARENTCODEID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["PARENTCODEID"] = value;
			}
		}
		protected string ParentCodeList
		{
			get
			{
				object obj = this.ViewState["PARENTCODELIST"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PARENTCODELIST"] = value;
			}
		}
		protected int TypeID
		{
			get
			{
				object obj = this.ViewState["TYPEID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TYPEID"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["t"] == null || base.Request.Params["cid"] == null || base.Request.Params["pid"] == null || base.Request.Params["tid"] == null)
				{
					this.JS.Text = "alert('参数错误');window.returnValue=false;window.close();";
				}
				else
				{
					base.DealType = (OpType)Enum.Parse(typeof(OpType), base.Request.Params["t"]);
					this.CodeID = Convert.ToInt32((base.Request.Params["cid"] == "") ? "0" : base.Request.Params["cid"]);
					this.ParentCodeID = Convert.ToInt32((base.Request.Params["pid"] == "") ? "0" : base.Request.Params["pid"]);
					this.TypeID = Convert.ToInt32((base.Request.Params["tid"] == "") ? "0" : base.Request.Params["tid"]);
				}
				if (base.DealType == OpType.Add)
				{
					this.LblBasicCode.Text = "编码增加";
					this.ParentCodeID = this.CodeID;
					this.CodeID = 0;
					CodingInfo codingInfo = this.CodingAct.QuerySingleCodeInfo(this.ParentCodeID, this.TypeID);
					this.ParentCodeList = ((codingInfo.ParentCodeList == "") ? "," : codingInfo.ParentCodeList);
					return;
				}
				this.LblBasicCode.Text = "编码编辑";
				CodingInfo codeInfo = this.CodingAct.QuerySingleCodeInfo(this.CodeID, this.TypeID);
				this.RestoreCodeInfoToPage(codeInfo);
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
		private void RestoreCodeInfoToPage(CodingInfo codeInfo)
		{
			this.TxtCodeName.Text = codeInfo.CodeName.Trim();
			this.ChkIsDefault.Checked = codeInfo.IsDefault;
			this.ParentCodeList = codeInfo.ParentCodeList;
		}
		private CodingInfo GatherInputInfo()
		{
			return new CodingInfo
			{
				CodeID = this.CodeID,
				CodeName = this.TxtCodeName.Text.Trim(),
				ParentCodeID = this.ParentCodeID,
				ParentCodeList = this.ParentCodeList,
				IsDefault = this.ChkIsDefault.Checked,
				TypeID = this.TypeID,
				Owner = base.UserCode.Substring(0, 6)
			};
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			CodingInfo codeInfo = this.GatherInputInfo();
			CodingInfo codingInfo = new CodingInfo();
			int num = 0;
			if (base.DealType == OpType.Add)
			{
				codingInfo = this.CodingAct.QuerySingleCodeInfo(codeInfo.CodeName, codeInfo.TypeID);
				if (codingInfo.CodeName != "")
				{
					if (codingInfo.IsValid)
					{
						this.JS.Text = "alert('该编码已存在！')";
						return;
					}
					codeInfo.CodeID = codingInfo.CodeID;
					num = this.CodingAct.RecycleCode(codeInfo);
				}
				else
				{
					num = this.CodingAct.AddBasicCode(codeInfo, "ccd");
					XPMBasicCodeListService source = new XPMBasicCodeListService();
					XPMBasicCodeList parent = (
						from c in source
						where c.CodeID == this.ParentCodeID && c.TypeID == this.TypeID
						select c).FirstOrDefault<XPMBasicCodeList>();
					if (parent != null)
					{
						BasicPrivilegeService basicPrivilegeService = new BasicPrivilegeService();
						IList<string> user = basicPrivilegeService.GetUser("XPM_Basic_CodeList", parent.NoteID.ToString());
						XPMBasicCodeList xPMBasicCodeList = (
							from c in source
							where c.TypeID == this.TypeID && c.ParentCodeID == parent.CodeID && c.CodeName == codeInfo.CodeName
							select c).FirstOrDefault<XPMBasicCodeList>();
						foreach (string current in user)
						{
							basicPrivilegeService.Add(new BasicPrivilege
							{
								PrivilegeId = Guid.NewGuid().ToString(),
								RelationsTable = "XPM_Basic_CodeList",
								RelationsKey = xPMBasicCodeList.NoteID.ToString(),
								UserCode = current
							});
						}
					}
				}
			}
			if (base.DealType == OpType.Upd)
			{
				codingInfo = this.CodingAct.QuerySingleCodeInfo(codeInfo.CodeName, codeInfo.TypeID);
				if (codingInfo.CodeName != "")
				{
					if (codingInfo.CodeID != codeInfo.CodeID)
					{
						this.JS.Text = "alert('该编码已存在！如果没有出现在列表中,那么请重新添加！');";
					}
					else
					{
						num = this.CodingAct.UpdBasicCode(codeInfo);
					}
				}
				else
				{
					num = this.CodingAct.UpdBasicCode(codeInfo);
				}
			}
			if (num == 1)
			{
				string script = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\ttop.ui.tabSuccess({ parentName: '_codelist' });\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
				base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), script);
				return;
			}
			this.JS.Text = "alert('保存失败！');";
		}
		private static bool ProcessSqlStr(string Str, int type)
		{
			string text;
			if (type == 1)
			{
				text = "exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
			}
			else
			{
				text = "'|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare";
			}
			bool result = true;
			try
			{
				if (Str != "")
				{
					string[] array = text.Split(new char[]
					{
						'|'
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string value = array2[i];
						if (Str.IndexOf(value) >= 0)
						{
							result = false;
						}
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}


