using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class TypeList : NBasePage, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BtnAdd.Attributes["onclick"] = "javascript:OpType('ADD')";
            this.BtnEdit.Attributes["onclick"] = "javascript:OpType('EDIT')";
            this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('该操作将删除所选分类下的全部数据，确定要删除当前选中分类吗？')){return false;}";
            if (!this.Page.IsPostBack)
            {
                this.ViewState["TYPEID"] = base.Request.QueryString["TypeId"];
                this.ViewState["VALId"] = "1";
                this.hdnParentId.Value = this.ViewState["TYPEID"].ToString();
                if (base.Request.QueryString["TypeId"] != null)
                {
                    if (base.Request.QueryString["TypeId"].ToString() == "1")
                    {
                        this.BtnEdit.Enabled = false;
                        this.BtnDel.Enabled = false;
                        this.BtnAdd.Enabled = true;
                    }
                    else
                    {
                        if (base.Request.QueryString["TypeId"].ToString() == "2")
                        {
                            this.BtnEdit.Enabled = false;
                            this.BtnDel.Enabled = false;
                            this.BtnAdd.Enabled = true;
                        }
                        else
                        {
                            if (base.Request.QueryString["TypeId"].ToString() == "3")
                            {
                                this.BtnEdit.Enabled = false;
                                this.BtnDel.Enabled = false;
                                this.BtnAdd.Enabled = true;
                            }
                            else
                            {
                                this.BtnEdit.Enabled = true;
                                this.BtnDel.Enabled = true;
                                this.BtnAdd.Enabled = true;
                            }
                        }
                    }
                }
                this.GridBind(this.ViewState["TYPEID"].ToString(), this.ViewState["VALId"].ToString());
            }
        }
        private void GridBind(string Type, string Valid)
        {
            string text = " (1=1) ";
            if (Type.Trim() != "")
            {
                text = text + " and ParentId = '" + Type + "' ";
            }
            else
            {
                text = (text ?? "");
            }
            if (Valid.Trim() != "")
            {
                text = text + " and isValid = '" + Valid + "' ";
            }
            else
            {
                text = (text ?? "");
            }
            DataTable pageData = publicDbOpClass.GetPageData(text, "EPM_Datum_Class");
            if (pageData.Rows.Count <= 0)
            {
                this.HiddenField1.Value = "EN";
            }
            this.DG_List.DataSource = pageData;
            this.DG_List.DataBind();
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.DG_List.ItemDataBound += new DataGridItemEventHandler(this.DG_List_ItemDataBound);
        }
        private void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                int num = e.Item.ItemIndex + 1;
                string text = this.DG_List.DataKeys[e.Item.ItemIndex].ToString();
                e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
                e.Item.Cells[0].Text = num.ToString();
                e.Item.Cells[3].Text = ((e.Item.Cells[3].Text == "True") ? "<img src='green.gif'>可见" : "<img src='../images/red.gif'>不可见");
                e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DG_List.ClientID.ToString(),
					"');clickRow('",
					text,
					"','",
					e.Item.Cells[4].Text,
					"');"
				});
                e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
                e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
            }
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            this.GridBind(this.ViewState["TYPEID"].ToString(), this.ViewState["VALId"].ToString());
            this.js.Text = "window.parent.lframe.location.href +='';";
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            this.GridBind(this.ViewState["TYPEID"].ToString(), this.ViewState["VALId"].ToString());
            this.js.Text = "window.parent.lframe.location.href +='';";
        }
        protected void BtnDel_Click(object sender, EventArgs e)
        {
            if (KnowledgeTypeAction.DelType(this.hdnType.Value) > 0)
            {
                this.GridBind(this.ViewState["TYPEID"].ToString(), this.ViewState["VALId"].ToString());
                this.js.Text = "window.parent.lframe.location.href +='';";
            }
        }
    }
