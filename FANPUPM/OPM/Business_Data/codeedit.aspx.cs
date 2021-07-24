using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Text;
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["t"] == null || base.Request.Params["cid"] == null || base.Request.Params["pid"] == null || base.Request.Params["tid"] == null)
				{
					this.JS.Text = "alert('参数错误');window.returnValue=false;window.close();";
				}
				else
				{
					base.DealType = (OpType)System.Enum.Parse(typeof(OpType), base.Request.Params["t"]);
					this.CodeID = System.Convert.ToInt32((base.Request.Params["cid"] == "") ? "0" : base.Request.Params["cid"]);
					this.ParentCodeID = System.Convert.ToInt32((base.Request.Params["pid"] == "") ? "0" : base.Request.Params["pid"]);
					this.TypeID = System.Convert.ToInt32((base.Request.Params["tid"] == "") ? "0" : base.Request.Params["tid"]);
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
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void RestoreCodeInfoToPage(CodingInfo codeInfo)
		{
			this.TxtCodeName.Text = codeInfo.CodeName;
			this.ChkIsDefault.Checked = codeInfo.IsDefault;
			this.ParentCodeList = codeInfo.ParentCodeList;
			this.TxtSimpleCode.Text = codeInfo.Owner;
			this.txtXh.Text = codeInfo.I_xh.ToString();
		}
		private CodingInfo GatherInputInfo()
		{
			CodingInfo codingInfo = new CodingInfo();
			codingInfo.CodeID = this.CodeID;
			codingInfo.CodeName = this.TxtCodeName.Text;
			codingInfo.ParentCodeID = this.ParentCodeID;
			codingInfo.ParentCodeList = this.ParentCodeList;
			codingInfo.IsDefault = this.ChkIsDefault.Checked;
			codingInfo.TypeID = this.TypeID;
			codingInfo.SignCode2 = this.GetSingCode(this.TypeID);
			codingInfo.Owner = this.TxtSimpleCode.Text.Trim();
			if (this.txtXh.Text.Trim() != "")
			{
				codingInfo.I_xh = System.Convert.ToInt32(this.txtXh.Text.Trim());
			}
			else
			{
				codingInfo.I_xh = 0;
			}
			return codingInfo;
		}
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			int num;
			if (!string.IsNullOrEmpty(this.txtXh.Text.Trim()) && !int.TryParse(this.txtXh.Text.Trim(), out num))
			{
				this.JS.Text = "top.ui.alert('请输入整数类型的序号！');";
				return;
			}
			CodingInfo codingInfo = this.GatherInputInfo();
			CodingInfo codingInfo2 = new CodingInfo();
			int num2 = 0;
			if (base.DealType == OpType.Add)
			{
				codingInfo2 = this.CodingAct.QuerySingleCodeInfo(codingInfo.CodeName, codingInfo.TypeID);
				if (codingInfo2.CodeName != "")
				{
					if (codingInfo2.IsValid)
					{
						this.JS.Text = "top.ui.alert('该编码已存在！')";
						return;
					}
					codingInfo.CodeID = codingInfo2.CodeID;
					num2 = this.CodingAct.RecycleCode(codingInfo);
				}
				else
				{
					num2 = this.CodingAct.AddBasicCode(codingInfo, "ccd");
				}
			}
			if (base.DealType == OpType.Upd)
			{
				codingInfo2 = this.CodingAct.QuerySingleCodeInfo(codingInfo.CodeName, codingInfo.TypeID);
				if (codingInfo2.CodeName != "")
				{
					if (codingInfo2.CodeID != codingInfo.CodeID)
					{
						this.JS.Text = "top.ui.alert('该编码已存在！如果没有出现在列表中,那么请重新添加！');";
						return;
					}
					num2 = this.CodingAct.UpdBasicCode(codingInfo);
				}
				else
				{
					num2 = this.CodingAct.UpdBasicCode(codingInfo);
				}
			}
			if (num2 == 1)
			{
				publicDbOpClass.ExecSqlString(string.Concat(new object[]
				{
					" update XPM_Basic_CodeList set Owner='",
					this.TxtSimpleCode.Text,
					"'  where  CodeID=",
					codingInfo.CodeID,
					"  "
				}));
				this.RegisterScript("top.ui.tabSuccess({ parentName: '_codeedit' });");
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败！');";
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
		protected void RegisterScript(string message)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("<script type='text/javascript'>").Append(System.Environment.NewLine).Append(message).Append("</script>");
			base.ClientScript.RegisterStartupScript(base.GetType(), System.Guid.NewGuid().ToString(), stringBuilder.ToString());
		}
		private string GetSingCode(int typeId)
		{
			CodingType codingType = this.CodingAct.QuerySingleCodingType(typeId);
			return codingType.SignCode;
		}
	}


