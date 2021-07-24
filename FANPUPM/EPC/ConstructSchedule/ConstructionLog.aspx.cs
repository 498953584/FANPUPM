using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
 public partial class TypeList : NBasePage, IRequiresSessionState
    {
         protected string QProjectID
        {
            get
            {
                if (base.Request.QueryString["PrjGuid"] != null)
                {
                    return base.Request.QueryString["PrjGuid"];
                }
                return "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.QProjectID == "-1")
            {
                this.BtnAdd.Enabled = false;
            }
            this.hdnPmId.Value = this.QProjectID;
            this.BtnAdd.Attributes["onclick"] = "openClassEdit(1)";
            this.BtnEdit.Attributes["onclick"] = "openClassEdit(2)";
            this.BtnLook.Attributes["onclick"] = "openClassEdit(3)";
            this.BtnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
            this.myDataBind();
        }
        private void myDataBind()
        {
            this.dgClass.DataSource = publicDbOpClass.GetList(string.Format(" ProjectId='{0}'", this.QProjectID));
            this.dgClass.DataBind();
        }
        private void myDataQuery()
        {
            this.dgClass.DataSource = publicDbOpClass.GetQuery(this.txtCode.Text, this.txtPart.Text, this.txtOperations.Text, this.QProjectID, this.workdate.Text);
            this.dgClass.DataBind();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void dgClass_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                e.Item.Attributes["id"] = this.dgClass.DataKeys[e.Item.ItemIndex].ToString();
                if (e.Item.Cells[3].Text.Length > 30)
                {
                    e.Item.Cells[3].ToolTip = e.Item.Cells[3].Text;
                    e.Item.Cells[3].Text = e.Item.Cells[3].Text.Substring(0, 29) + "...";
                }
                if (e.Item.Cells[4].Text.Length > 30)
                {
                    e.Item.Cells[4].ToolTip = e.Item.Cells[4].Text;
                    e.Item.Cells[4].Text = e.Item.Cells[4].Text.Substring(0, 29) + "...";
                }
            }
        }
        protected void dgClass_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.dgClass.CurrentPageIndex = e.NewPageIndex;
            this.myDataQuery();
        }
        protected void BtnDel_Click(object sender, EventArgs e)
        {
            string logID = Convert.ToString(this.hfldConstruct.Value);
            if (publicDbOpClass.Delete(logID) == 1)
            {
                this.js.Text = "alert('删除成功！');";
                this.myDataBind();
                return;
            }
            this.js.Text = "alert('删除错误！');";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.myDataQuery();
        }
    }
