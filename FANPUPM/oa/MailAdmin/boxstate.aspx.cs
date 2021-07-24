using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class BoxState : BasePage, IRequiresSessionState
	{
		private string _strSenderCode = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this._strSenderCode = this.Session["yhdm"].ToString();
			if (!base.IsPostBack)
			{
				int num = 0;
				float num2 = 0f;
				MailManage mailManage = new MailManage();
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.Text = "<A href=\"newmail.aspx\">收件箱</A>";
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				int num3 = mailManage.GetInMailNumber(this._strSenderCode);
				num += num3;
				tableCell.Text = num3.ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				float num4 = (float)mailManage.GetInMailSize(this._strSenderCode);
				num4 /= 1048576f;
				num2 += num4;
				tableCell.Text = num4.ToString("0.00");
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableRow.Height = Unit.Pixel(20);
				this.TabBox.Rows.Add(tableRow);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = "<A href=\"outbox.aspx\">发件箱</A>";
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				num3 = mailManage.GetOutMailNumber(this._strSenderCode);
				num += num3;
				tableCell.Text = num3.ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				num4 = (float)mailManage.GetOutMailSize(this._strSenderCode);
				num4 /= 1048576f;
				num2 += num4;
				tableCell.Text = num4.ToString("0.00");
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableRow.Height = Unit.Pixel(20);
				this.TabBox.Rows.Add(tableRow);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = "<A href=\"draftbox.aspx\">草稿箱</A>";
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				num3 = mailManage.GetDraftMailNumber(this._strSenderCode);
				num += num3;
				tableCell.Text = num3.ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				num4 = (float)mailManage.GetDraftMailSize(this._strSenderCode);
				num4 /= 1048576f;
				num2 += num4;
				tableCell.Text = num4.ToString("0.00");
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableRow.Height = Unit.Pixel(20);
				this.TabBox.Rows.Add(tableRow);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = "<A href=\"trashbox.aspx\">垃圾箱</A>";
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				num3 = mailManage.GetTrashMailNumber(this._strSenderCode);
				num += num3;
				tableCell.Text = num3.ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				num4 = (float)mailManage.GetTrashMailSize(this._strSenderCode);
				num4 /= 1048576f;
				num2 += num4;
				tableCell.Text = num4.ToString("0.00");
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableRow.Height = Unit.Pixel(20);
				this.TabBox.Rows.Add(tableRow);
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = "当前状态";
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = num.ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = num4.ToString("0.00");
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableRow.Height = Unit.Pixel(20);
				this.TabBox.Rows.Add(tableRow);
				DataTable userSet = mailManage.GetUserSet(this._strSenderCode);
				if (userSet.Rows.Count > 0)
				{
					num3 = int.Parse(userSet.Rows[0]["i_MailNumber"].ToString());
					num4 = (float)int.Parse(userSet.Rows[0]["i_DiskSpace"].ToString());
					num4 /= 1048576f;
					tableRow = new TableRow();
					tableCell = new TableCell();
					tableCell.Text = "系统设置";
					tableCell.HorizontalAlign = HorizontalAlign.Center;
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = num3.ToString();
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					tableRow.Cells.Add(tableCell);
					tableCell = new TableCell();
					tableCell.Text = num4.ToString("0.00");
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					tableRow.Cells.Add(tableCell);
					this.TabBox.Rows.Add(tableRow);
					tableRow = new TableRow();
					tableCell = new TableCell();
					tableCell.Text = this.ShowWarn();
					tableCell.ColumnSpan = 3;
					tableRow.Cells.Add(tableCell);
					tableRow.Height = Unit.Pixel(20);
					this.TabBox.Rows.Add(tableRow);
				}
			}
		}
		private string ShowWarn()
		{
			int num = 0;
			int num2 = 0;
			string text = "";
			MailManage mailManage = new MailManage();
			DataTable userSet = mailManage.GetUserSet(this._strSenderCode);
			if (userSet.Rows.Count > 0)
			{
				num = int.Parse(userSet.Rows[0]["i_MailNumber"].ToString());
				num2 = int.Parse(userSet.Rows[0]["i_DiskSpace"].ToString());
			}
			int mailNumber = mailManage.GetMailNumber(this._strSenderCode);
			int diskSpace = mailManage.GetDiskSpace(this._strSenderCode);
			if (num < mailNumber || num2 < diskSpace)
			{
				text = "<FONT color=\"#FF0000\"><B>警告：</B></FONT>";
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"你的邮箱中有",
					mailNumber.ToString(),
					"/",
					num.ToString(),
					"封邮件，"
				});
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"磁盘空间占用",
					(Convert.ToSingle(diskSpace) / 1048576f).ToString("0.00"),
					"/",
					(num2 / 1048576).ToString("0.00"),
					"MB,"
				});
				if (num < mailNumber && num2 < diskSpace)
				{
					text += "<FONT color=\"#FF0000\"><B>邮件数量和磁盘空间</B></FONT>都超出设置，请清理邮箱，不然暂存区的邮件将无法收取！";
				}
				else
				{
					if (num < mailNumber)
					{
						text += "<FONT color=\"#FF0000\"><B>邮件数量</B></FONT>超出设置，请清理邮箱！";
					}
					else
					{
						text += "<FONT color=\"#FF0000\"><B>磁盘空间</B></FONT>超出设置，请清理邮箱！";
					}
				}
			}
			return text;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


