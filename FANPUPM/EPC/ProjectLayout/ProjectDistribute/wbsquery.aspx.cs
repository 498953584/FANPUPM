using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class ProjectGSQuery : PageBase, IRequiresSessionState
    {
        private string[] _ScheduleCodeList;
        private DataTable _DtSchedule = new DataTable();
        protected int templetId;
        public string jw = string.Empty;

        public string ProjectName
        {
            get
            {
                return (string)this.ViewState["PROJECTNAME"];
            }
            set
            {
                this.ViewState["PROJECTNAME"] = value;
            }
        }
        public Guid ProjectCode
        {
            get
            {
                return (Guid)this.ViewState["PROJECTCODE"];
            }
            set
            {
                this.ViewState["PROJECTCODE"] = value;
            }
        }
        protected ConstructWBSTaskAction bta
        {
            get
            {
                return new ConstructWBSTaskAction();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this._ScheduleCodeList = this.hdnScheduleCodeList.Value.Split(new char[]
			{
				'^'
			});
            if (base.Request["jw"] != null)
            {
                this.jw = base.Request["jw"].ToString().Trim();
                if (this.jw == "1")
                {
                    this.btnAdd.Attributes.CssStyle["display"] = "";
                    this.btnUpd.Attributes.CssStyle["display"] = "";
                    this.btnDel.Attributes.CssStyle["display"] = "";
                }
                if (this.jw == "4")
                {
                    this.Btn_wbs1.Attributes.CssStyle["display"] = "";
                    this.Btn_wbs.Attributes.CssStyle["display"] = "";
                    this.BtnRation.Attributes.CssStyle["display"] = "";
                    this.hlQD1.Attributes.CssStyle["display"] = "";
                    this.hlQD2.Attributes.CssStyle["display"] = "";
                }
                if (this.jw == "5")
                {
                    this.BtnConfirm.Attributes.CssStyle["display"] = "";
                }
                if (this.jw == "7")
                {
                    this.btnResource.Attributes.CssStyle["display"] = "";
                }
            }
            if (!this.Page.IsPostBack)
            {
                if (base.Request["pc"] == null || base.Request["pn"] == null || base.Request["PrjGuid"] == null)
                {
                    this.js.Text = "alert('参数有误！');";
                    return;
                }
                this.hdnPrjGuid.Value = base.Request.QueryString["PrjGuid"];
                this.ProjectCode = new Guid(base.Request["pc"]);
                this.DGsum.DataSource = this.bta.ResSum(this.ProjectCode);
                this.DGsum.DataBind();
                this.ProjectName = base.Request["pn"].ToString();
                this.lblProjectName.Text = this.ProjectName;
                this.hdnProjectCode.Value = this.ProjectCode.ToString();
                this.hdnProjectName.Value = base.Request["pn"].ToString();
                this.Init_theSchedule(this.ProjectCode);
                this.tblSchedule_Create(true);
                this.Btn_wbs.Attributes["onclick"] = "javascript:return ImportData('" + this.ProjectCode.ToString() + "','1');";
                this.BtnRation.Attributes["onclick"] = "javascript:return ImportData('" + this.ProjectCode.ToString() + "','2');";
                this.Btn_wbs1.Attributes["onclick"] = "javascript:return ImportData('" + this.ProjectCode.ToString() + "','3');";
                this.btnResource.Attributes["onclick"] = "javascript:return clickResource()";
                this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;} ";
                this.BtnConfirm.Attributes["onclick"] = "javascript:if(!confirm('该操作不能撤销，确认后将不能修改wbs结构！\\n确认后如过需要变更, \\n请通过合同变更和预算变更进行变更。')){return false;};";
            }
            this.js.Text = "window.parent.fraTask.location.href='TaskEdit.aspx?jw=" + this.jw + "';";
            if (this.bta.IsWBSConfirm(this.ProjectCode))
            {
                this.HdnIsConfirm.Value = "1";
                Label expr_450 = this.lblProjectName;
                expr_450.Text += "(预算已锁定)";
                this.Btn_wbs1.Enabled = false;
                this.Btn_wbs.Enabled = false;
                this.BtnRation.Enabled = false;
                this.BtnConfirm.Enabled = false;
                this.btnDel.Enabled = false;
                this.btnResource.Enabled = false;
            }
        }
        private void Init_theSchedule(Guid versionCode)
        {
            this._DtSchedule.Reset();
            this._DtSchedule.Columns.Add(new DataColumn("NoteID", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("TaskCode", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("TaskName", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("ParentTaskCode", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("QuantityUnit", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("WorkLayer", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("IsHaveServerChild", typeof(string)));
            this._DtSchedule.Columns.Add(new DataColumn("StartDate", typeof(DateTime)));
            this._DtSchedule.Columns.Add(new DataColumn("EndDate", typeof(DateTime)));
            this._DtSchedule.Columns.Add(new DataColumn("ContractPrice", typeof(decimal)));
            this._DtSchedule.Columns.Add(new DataColumn("GSTotalFee", typeof(decimal)));
            this._DtSchedule.Columns.Add(new DataColumn("IsAlter", typeof(bool)));
            this._DtSchedule.Columns.Add(new DataColumn("Remark", typeof(string)));
            WBSBidTaskCollection wBSBidTaskCollection = new WBSBidTaskCollection();
            wBSBidTaskCollection = this.bta.GetAllBidScheduleList(this.ProjectCode, 1);
            if (wBSBidTaskCollection.Count == 0)
            {
                this.BtnConfirm.Enabled = false;
            }
            for (int i = 0; i < wBSBidTaskCollection.Count; i++)
            {
                DataRow dataRow = this._DtSchedule.NewRow();
                WBSBidTask wBSBidTask = wBSBidTaskCollection[i];
                dataRow["NoteID"] = wBSBidTask.NoteID;
                dataRow["TaskCode"] = wBSBidTask.TaskCode;
                dataRow["TaskName"] = wBSBidTask.TaskName;
                dataRow["Quantity"] = ((wBSBidTask.Quantity == 0m) ? "0.00" : string.Format("{0:f}", wBSBidTask.Quantity));
                dataRow["QuantityUnit"] = wBSBidTask.QuantityUnit;
                dataRow["WorkLayer"] = Convert.ToInt32(wBSBidTask.WorkLayer);
                dataRow["ParentTaskCode"] = wBSBidTask.ParentTaskCode.ToString();
                dataRow["IsHaveChild"] = ((wBSBidTask.ChildNum > 0) ? "true" : "false");
                dataRow["IsHaveServerChild"] = "false";
                dataRow["StartDate"] = wBSBidTask.StartDate;
                dataRow["EndDate"] = wBSBidTask.EndDate;
                dataRow["ContractPrice"] = wBSBidTask.SynthPrice;
                dataRow["GSTotalFee"] = wBSBidTask.sumPrice;
                try
                {
                    dataRow["IsAlter"] = wBSBidTask.IsAlter;
                }
                catch
                {
                    dataRow["IsAlter"] = false;
                }
                dataRow["Remark"] = wBSBidTask.Remark;
                this._DtSchedule.Rows.Add(dataRow);
            }
        }
        private void tblSchedule_Create(bool firstExend)
        {
            DataTable dataTable = new DataTable();
            foreach (DataColumn dataColumn in this._DtSchedule.Columns)
            {
                dataTable.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
            }
            dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
            this.CreateChild(dataTable, "", 0, "", false, firstExend);
            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            tableCell.Text = this.ProjectName;
            tableCell.Height = Unit.Pixel(20);
            tableCell.Wrap = false;
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableCell.Text = "&nbsp;";
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableCell = new TableCell();
            tableRow.Cells.Add(tableCell);
            tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblSchedule.ClientID + "',false);clickRow(this,'','',0);";
            tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
            tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
            tableRow.CssClass = "grid_row";
            this.tblSchedule.Rows.Add(tableRow);
            this.tblSchedule_Draw(dataTable);
        }
        private void tblSchedule_Draw(DataTable dt)
        {
            ConstructWBSTaskAction constructWBSTaskAction = new ConstructWBSTaskAction();
            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dataRow = dt.Rows[i];
                tableRow = new TableRow();
                tableCell = new TableCell();
                tableCell.Text = dataRow["HeadImg"].ToString() + ((dataRow["TaskName"].ToString().Trim() == "") ? ".." : dataRow["TaskName"].ToString().Trim());
                tableCell.Wrap = false;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = dataRow["ParentTaskCode"].ToString() + "<font color=\"#ff0000\">" + dataRow["TaskCode"].ToString().Substring(dataRow["ParentTaskCode"].ToString().Length) + "</font>";
                tableCell.Width = Unit.Pixel(70);
                tableCell.Wrap = false;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = this.WorkLayerName(Convert.ToInt32(dataRow["WorkLayer"])).Trim();
                tableCell.Width = Unit.Pixel(50);
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableCell.Wrap = false;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = dataRow["QuantityUnit"].ToString();
                tableCell.Width = Unit.Pixel(30);
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableCell.Wrap = false;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.HorizontalAlign = HorizontalAlign.Right;
                tableCell.Text = dataRow["Quantity"].ToString();
                tableCell.Wrap = false;
                if (dataRow["IsAlter"] != null && dataRow["IsAlter"].ToString() != "" && Convert.ToBoolean(dataRow["IsAlter"].ToString()))
                {
                    tableCell.ForeColor = Color.Red;
                }
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = Convert.ToDateTime(dataRow["StartDate"]).ToString("yyyy-MM-dd");
                tableCell.Width = Unit.Pixel(70);
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableCell.Wrap = false;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = Convert.ToDateTime(dataRow["EndDate"]).ToString("yyyy-MM-dd");
                tableCell.Width = Unit.Pixel(70);
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableCell.Wrap = false;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                if (dataRow["WorkLayer"].ToString().Trim() == "1")
                {
                    tableCell.Text = constructWBSTaskAction.GetTotalMoneya(this.ProjectCode.ToString(), 1, dataRow["TaskCode"].ToString()).ToString();
                }
                else
                {
                    if (dataRow["WorkLayer"].ToString().Trim() == "2")
                    {
                        tableCell.Text = constructWBSTaskAction.GetTotalMoneya(this.ProjectCode.ToString(), 2, dataRow["TaskCode"].ToString()).ToString();
                    }
                    else
                    {
                        if (dataRow["WorkLayer"].ToString().Trim() == "3")
                        {
                            tableCell.Text = constructWBSTaskAction.GetTotalMoneya(this.ProjectCode.ToString(), 3, dataRow["TaskCode"].ToString()).ToString();
                            if (tableCell.Text.Trim() == "0")
                            {
                                tableCell.Text = dataRow["ContractPrice"].ToString();
                            }
                        }
                    }
                }
                tableCell.HorizontalAlign = HorizontalAlign.Right;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                if (dataRow["WorkLayer"].ToString().Trim() == "1")
                {
                    tableCell.Text = constructWBSTaskAction.GetTotalMoney(this.ProjectCode.ToString(), 1, dataRow["TaskCode"].ToString()).ToString();
                }
                else
                {
                    if (dataRow["WorkLayer"].ToString().Trim() == "2")
                    {
                        tableCell.Text = constructWBSTaskAction.GetTotalMoney(this.ProjectCode.ToString(), 2, dataRow["TaskCode"].ToString()).ToString();
                    }
                    else
                    {
                        if (dataRow["WorkLayer"].ToString().Trim() == "3")
                        {
                            tableCell.Text = constructWBSTaskAction.GetTotalMoney(this.ProjectCode.ToString(), 3, dataRow["TaskCode"].ToString()).ToString();
                        }
                    }
                }
                tableCell.HorizontalAlign = HorizontalAlign.Right;
                tableRow.Cells.Add(tableCell);
                tableCell = new TableCell();
                string text = dataRow["Remark"].ToString();
                if (text.Length > 10)
                {
                    text = text.Substring(0, 10) + "...";
                }
                tableCell.Text = text;
                tableCell.ToolTip = dataRow["Remark"].ToString();
                tableCell.HorizontalAlign = HorizontalAlign.Left;
                tableRow.Cells.Add(tableCell);
                if (dataRow["ParentTaskCode"].ToString() != "")
                {
                    tableRow.Attributes["id"] = dataRow["ParentTaskCode"].ToString();
                }
                if (dataRow["IsHaveChild"].ToString().ToLower() == "false")
                {
                    tableRow.Attributes["onclick"] = string.Concat(new string[]
					{
						"doClick(this,'",
						this.tblSchedule.ClientID,
						"',true);clickRow(this,'",
						dataRow["NoteID"].ToString(),
						"',",
						dataRow["IsHaveChild"].ToString().ToLower(),
						",",
						dataRow["WorkLayer"].ToString(),
						");"
					});
                }
                else
                {
                    tableRow.Attributes["onclick"] = string.Concat(new string[]
					{
						"doClick(this,'",
						this.tblSchedule.ClientID,
						"',false);clickRow(this,'",
						dataRow["NoteID"].ToString(),
						"',",
						dataRow["IsHaveChild"].ToString().ToLower(),
						",",
						dataRow["WorkLayer"].ToString(),
						");"
					});
                }
                tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
                tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
                tableRow.Style.Add("display", dataRow["display"].ToString());
                tableRow.CssClass = "grid_row";
                this.tblSchedule.Rows.Add(tableRow);
            }
        }
        private string WorkLayerName(int WorkLayerID)
        {
            string result = "";
            switch (WorkLayerID)
            {
                case 1:
                    result = "单位工程";
                    break;
                case 2:
                    result = "分部工程";
                    break;
                case 3:
                    result = "分项工程";
                    break;
                case 4:
                    result = "子项工程";
                    break;
            }
            return result;
        }
        public void CreateChild(DataTable dt1, string TaskCode, int level, string inherHead, bool isExpend, bool firstExpend)
        {
            DataRow[] array = this._DtSchedule.Select("ParentTaskCode ='" + TaskCode + "'");
            int num = array.Length;
            for (int i = 0; i < num; i++)
            {
                bool flag = false;
                DataRow dataRow = array[i];
                for (int j = 0; j < this._ScheduleCodeList.Length; j++)
                {
                    if (this._ScheduleCodeList[j] == dataRow["TaskCode"].ToString())
                    {
                        flag = true;
                        break;
                    }
                }
                DataRow dataRow2 = dt1.NewRow();
                dataRow2.ItemArray = dataRow.ItemArray;
                bool flag2 = dataRow["IsHaveChild"].ToString().ToLower() == "true";
                dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow, flag2, firstExpend);
                dataRow2["ParentTaskCode"] = TaskCode;
                dataRow2["IsHaveServerChild"] = (this._DtSchedule.Select("ParentTaskCode = '" + dataRow["TaskCode"].ToString() + "' ").Length > 0).ToString().ToLower();
                if (level == 0)
                {
                    dataRow2["display"] = "block";
                    isExpend = true;
                }
                else
                {
                    if (isExpend)
                    {
                        dataRow2["display"] = "block";
                    }
                    else
                    {
                        if (firstExpend)
                        {
                            dataRow2["display"] = "block";
                        }
                        else
                        {
                            dataRow2["display"] = "none";
                        }
                    }
                }
                dt1.Rows.Add(dataRow2);
                if (flag2)
                {
                    this.CreateChild(dt1, dataRow["TaskCode"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag, firstExpend);
                }
            }
        }
        public string getHeadImg(int currentIndex, int length, DataRow dr, bool isHaveChild, bool firstExpend)
        {
            string result = "";
            bool flag = false;
            if (currentIndex != length - 1)
            {
                if (isHaveChild)
                {
                    for (int i = 0; i < this._ScheduleCodeList.Length; i++)
                    {
                        if (this._ScheduleCodeList[i] == dr["TaskCode"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
                    }
                    else
                    {
                        if (firstExpend)
                        {
                            result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
                            this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
                        }
                        else
                        {
                            result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
                        }
                    }
                }
                else
                {
                    result = "<img src=\"../../../images/tree/t.gif\" align=\"absmiddle\">";
                }
            }
            else
            {
                if (currentIndex == length - 1)
                {
                    if (isHaveChild)
                    {
                        for (int j = 0; j < this._ScheduleCodeList.Length; j++)
                        {
                            if (this._ScheduleCodeList[j] == dr["TaskCode"].ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
                        }
                        else
                        {
                            if (firstExpend)
                            {
                                result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
                                this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
                            }
                            else
                            {
                                result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
                            }
                        }
                    }
                    else
                    {
                        result = "<img src=\"../../../images/tree/l.gif\" align=\"absmiddle\">";
                    }
                }
            }
            return result;
        }
        public string getInherImg(int currentIndex, int length, string inherHead)
        {
            string str;
            if (currentIndex == length - 1)
            {
                str = "<img src=\"../../../images/tree/white.gif\" align=\"absmiddle\">";
            }
            else
            {
                str = "<img src=\"../../../images/tree/i.gif\" align=\"absmiddle\">";
            }
            return inherHead + str;
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
        }
        protected void Btn_wbs_Click(object sender, EventArgs e)
        {
            this.Init_theSchedule(this.ProjectCode);
            this.tblSchedule_Create(true);
        }
        protected void Btn_wbs1_Click(object sender, EventArgs e)
        {
            this.Init_theSchedule(this.ProjectCode);
            this.tblSchedule_Create(true);
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (this.bta.BidTaskDel(Convert.ToInt32(this.hdnNoteID.Value)) == 1)
            {
                this.js.Text = "alert('删除成功!');";
                this.Init_theSchedule(this.ProjectCode);
                this.tblSchedule_Create(false);
                return;
            }
            this.js.Text = "alert('删除失败!');";
        }
        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (this.bta.WBSNodeIsCorrect(this.ProjectCode))
            {
                if (this.bta.WBSConfirm(this.ProjectCode) == 1)
                {
                    this.Btn_wbs1.Enabled = false;
                    this.Btn_wbs.Enabled = false;
                    this.BtnRation.Enabled = false;
                    this.BtnConfirm.Enabled = false;
                    this.btnDel.Enabled = false;
                    this.HdnIsConfirm.Value = "1";
                    this.js.Text = "alert('预算锁定成功!');";
                    this.Init_theSchedule(this.ProjectCode);
                    this.tblSchedule_Create(false);
                }
                else
                {
                    this.js.Text = "alert('预算锁定失败!');";
                }
            }
            else
            {
                this.js.Text = "alert('请确保工作结构正确，必须分解到分项工程！')";
            }
            this.Init_theSchedule(this.ProjectCode);
            this.tblSchedule_Create(false);
        }
        protected void btnResource_Click(object sender, EventArgs e)
        {
            this.Init_theSchedule(this.ProjectCode);
            this.tblSchedule_Create(false);
        }
        protected void BtnRation_Click(object sender, EventArgs e)
        {
            this.Init_theSchedule(this.ProjectCode);
            this.tblSchedule_Create(true);
        }
    }

