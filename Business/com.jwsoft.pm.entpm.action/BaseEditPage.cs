namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.common.baseclass;
    using System;

    public class BaseEditPage : PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request.QueryString["Type"] != null)
                {
                    this.Type = (ComType) Enum.Parse(typeof(ComType), base.Request.QueryString["Type"].ToString());
                }
                else
                {
                    this.Type = ComType.Add;
                }
                if ((base.Request.QueryString["NodeId"] != null) && (base.Request.QueryString["NodeId"].ToString() != ""))
                {
                    this.NodeId = base.Request.QueryString["NodeId"];
                }
                else
                {
                    this.NodeId = "1";
                }
                if (base.Request.QueryString["DatumId"] != null)
                {
                    this.DatumId = base.Request.QueryString["DatumId"].ToString();
                }
                else
                {
                    this.DatumId = "1";
                }
            }
            base.OnLoad(e);
        }

        public string OperatePrompt(int IsSuccess)
        {
            if (IsSuccess == 1)
            {
                string str = "alert('操作成功，请返回！');";
                return (str + "window.close();" + "window.location.reload();");
            }
            return "alert('操作失败，输入错误，或者网络连接故障！')";
        }

        public string BorrowId
        {
            get
            {
                return this.ViewState["BorrowId"].ToString();
            }
            set
            {
                this.ViewState["BorrowId"] = value;
            }
        }

        public Browse BorrowType
        {
            get
            {
                return (Browse) this.ViewState["BorrowType"];
            }
            set
            {
                this.ViewState["BorrowType"] = value;
            }
        }

        public Browse BrowseType
        {
            get
            {
                return (Browse) this.ViewState["BorrowType"];
            }
            set
            {
                this.ViewState["BorrowType"] = value;
            }
        }

        public string DatumId
        {
            get
            {
                return this.ViewState["DatumId"].ToString();
            }
            set
            {
                this.ViewState["DatumId"] = value;
            }
        }

        public string NodeId
        {
            get
            {
                return this.ViewState["NodeId"].ToString();
            }
            set
            {
                this.ViewState["NodeId"] = value;
            }
        }

        public ComType Type
        {
            get
            {
                return (ComType) this.ViewState["Type"];
            }
            set
            {
                this.ViewState["Type"] = value;
            }
        }

        public string UserId
        {
            get
            {
                if ((this.Session["yhdm"] != null) && (this.Session["yhdm"].ToString() != ""))
                {
                    return this.Session["yhdm"].ToString();
                }
                return "0";
            }
        }
    }
}

