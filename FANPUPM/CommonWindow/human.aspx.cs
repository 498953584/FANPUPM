using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class Human : BasePage, IRequiresSessionState
    {
        private int _iSysID;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            SysManageDb sysManageDb = new SysManageDb();
            if (base.Request.QueryString.Count > 0)
            {
                try
                {
                    this._iSysID = int.Parse(base.Request.QueryString["sysid"].ToString());
                    goto IL_4F;
                }
                catch
                {
                    goto IL_4F;
                }
            }
            this._iSysID = sysManageDb.GetDefault();
        IL_4F:
            this.HdnSysID.Value = this._iSysID.ToString();
            if (!base.IsPostBack)
            {
                int num = 0;
                try
                {
                    num = int.Parse(base.Request.QueryString["deptid"].ToString());
                }
                catch
                {
                }
                if (num != 0 && num != 1)
                {
                    userManageDb userManageDb = new userManageDb();
                    DataSet dataSet = userManageDb.DepUserQuaryDs(num.ToString(), "y");
                    DataTable dataTable = dataSet.Tables[0];
                    this.LBoxHuman.Items.Clear();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        ListItem item = new ListItem(dataTable.Rows[i]["v_xm"].ToString(), dataTable.Rows[i]["v_yhdm"].ToString());
                        this.LBoxHuman.Items.Add(item);
                    }
                }
                this.SelectedList_Restore();
            }
        }
        private void SelectedList_Restore()
        {
            string text = "";
            string text2 = "";
            try
            {
                text = this.Session["HumanCode"].ToString();
                text2 = this.Session["HumanName"].ToString();
            }
            catch
            {
            }
            string[] array = text.Split(new char[]
			{
				'!'
			});
            string[] array2 = text2.Split(new char[]
			{
				','
			});
            for (int i = 0; i < array.Length - 1; i++)
            {
                this.LBoxSelected.Items.Add(new ListItem(array2[i], array[i]));
            }
        }
        protected void BtnAdd_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.LBoxHuman.Items.Count; i++)
            {
                if (this.LBoxHuman.Items[i].Selected)
                {
                    string text = this.HdnSysID.Value.ToString() + ":" + this.LBoxHuman.Items[i].Value.ToString();
                    string text2 = this.LBoxHuman.Items[i].Text.ToString();
                    bool flag = false;
                    for (int j = 0; j < this.LBoxSelected.Items.Count; j++)
                    {
                        if (text == this.LBoxSelected.Items[j].Value.ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                    int num = 0;
                    while (num < this.LBoxSelected.Items.Count && !(this.HdnSysID.Value.ToString() == this.LBoxSelected.Items[num].Value.ToString().Split(new char[]
					{
						':'
					})[0].ToString()))
                    {
                        num++;
                    }
                    if (!flag)
                    {
                        HttpSessionState session;
                        (session = this.Session)["HumanCode"] = session["HumanCode"] + text + "!";
                        HttpSessionState session2;
                        (session2 = this.Session)["HumanName"] = session2["HumanName"] + text2 + ",";
                        this.LBoxSelected.Items.Add(new ListItem(text2, text));
                    }
                }
            }
        }
        protected void BtnAddAll_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.LBoxHuman.Items.Count; i++)
            {
                string text = this.HdnSysID.Value.ToString().Trim() + ":" + this.LBoxHuman.Items[i].Value.ToString();
                string text2 = this.LBoxHuman.Items[i].Text.ToString();
                bool flag = false;
                for (int j = 0; j < this.LBoxSelected.Items.Count; j++)
                {
                    if (text == this.LBoxSelected.Items[j].Value.ToString())
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    HttpSessionState session;
                    (session = this.Session)["HumanCode"] = session["HumanCode"] + text + "!";
                    HttpSessionState session2;
                    (session2 = this.Session)["HumanName"] = session2["HumanName"] + text2 + ",";
                    this.LBoxSelected.Items.Add(new ListItem(text2, text));
                }
            }
        }
        protected void BtnRemoveAll_Click(object sender, System.EventArgs e)
        {
            this.Session["HumanCode"] = "";
            this.Session["HumanName"] = "";
            this.LBoxSelected.Items.Clear();
        }
        protected void BtnRemove_Click(object sender, System.EventArgs e)
        {
            for (int i = this.LBoxSelected.Items.Count - 1; i >= 0; i--)
            {
                if (this.LBoxSelected.Items[i].Selected)
                {
                    string a = this.LBoxSelected.Items[i].Value.ToString().Split(new char[]
					{
						':'
					})[0].ToString();
                    this.LBoxSelected.Items.RemoveAt(i);
                    int num = this.LBoxSelected.Items.Count - 1;
                    while (num >= 0 && !(a == this.LBoxSelected.Items[num].Value.ToString().Split(new char[]
					{
						':'
					})[0].ToString()))
                    {
                        num--;
                    }
                }
            }
            this.HumanList_Reset();
        }
        private void HumanList_Reset()
        {
            this.Session["HumanCode"] = "";
            this.Session["HumanName"] = "";
            for (int i = 0; i < this.LBoxSelected.Items.Count; i++)
            {
                HttpSessionState session;
                (session = this.Session)["HumanCode"] = session["HumanCode"] + this.LBoxSelected.Items[i].Value.ToString() + "!";
                HttpSessionState session2;
                (session2 = this.Session)["HumanName"] = session2["HumanName"] + this.LBoxSelected.Items[i].Text.ToString() + ",";
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
    }