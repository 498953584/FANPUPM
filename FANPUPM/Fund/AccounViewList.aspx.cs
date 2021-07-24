using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccounDetailList : NBasePage, IRequiresSessionState
{
	private string _IC = string.Empty;

	public string IC
	{
		get
		{
			return this._IC;
		}
		set
		{
			this._IC = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["ic"] != null)
		{
			this._IC = base.Request.QueryString["ic"].ToString();
			this.HiddenField1.Value = this._IC;
			this.bindItem(this._IC);
		}
	}
	private void bindItem(string accountid)
	{
		DataTable dataTable = new DataTable();
		Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
		string arg_11_0 = string.Empty;
		dataTable = fund_Prj_Accoun.initialFundByAccountID(accountid);
		this.GridView1.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView1.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value = new string[]
			{
				dataTable.Compute("SUM(INMoney)", string.Empty).ToString()
			};
			int[] index = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView1, value, index);
		}
		dataTable = fund_Prj_Accoun.OtherIncomeByAccountID(accountid);
		this.GridView2.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView2.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value2 = new string[]
			{
				dataTable.Compute("SUM(INMoney)", string.Empty).ToString()
			};
			int[] index2 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView2, value2, index2);
		}
		dataTable = fund_Prj_Accoun.ContIncomeRZByAccountID(accountid);
		this.GridView3.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView3.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value3 = new string[]
			{
				dataTable.Compute("SUM(INMoney)", string.Empty).ToString()
			};
			int[] index3 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView3, value3, index3);
		}
		dataTable = fund_Prj_Accoun.LoanFundByAccountID(accountid);
		this.GridView4.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView4.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value4 = new string[]
			{
				dataTable.Compute("SUM(LoanFund)", string.Empty).ToString()
			};
			int[] index4 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView4, value4, index4);
		}
		dataTable = fund_Prj_Accoun.ReturnLoanByAccountID(accountid);
		this.GridView5.DataSource = this.Sort(dataTable, "FR_Time");
		this.GridView5.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value5 = new string[]
			{
				dataTable.Compute("SUM(FR_Money)", string.Empty).ToString(),
				dataTable.Compute("SUM(FR_interest)+SUM(FR_deduct)", string.Empty).ToString()
			};
			int[] index5 = new int[]
			{
				4,
				5
			};
			GridViewUtility.AddTotalRow(this.GridView5, value5, index5);
		}
		dataTable = fund_Prj_Accoun.MustPaidContByAccountID(accountid);
		this.GridView6.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView6.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value6 = new string[]
			{
				dataTable.Compute("SUM(PaymentMoney)", string.Empty).ToString()
			};
			int[] index6 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView6, value6, index6);
		}
		dataTable = fund_Prj_Accoun.MustPaidOtherCostByAccountID(accountid);
		this.GridView7.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView7.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value7 = new string[]
			{
				dataTable.Compute("SUM(Amount)", string.Empty).ToString()
			};
			int[] index7 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView7, value7, index7);
		}
		dataTable = fund_Prj_Accoun.PaidContByAccountID(accountid);
		this.GridView8.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView8.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value8 = new string[]
			{
				dataTable.Compute("SUM(PayOutMoney)", string.Empty).ToString()
			};
			int[] index8 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView8, value8, index8);
		}
		dataTable = fund_Prj_Accoun.PaidOtherCostByAccountID(accountid);
		this.GridView9.DataSource = this.Sort(dataTable, "OccurredDate");
		this.GridView9.DataBind();
	}
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (this.hdnProjectCode.Value != "")
		{
			stringBuilder.Append("PrjGuid='").Append(this.hdnProjectCode.Value).Append("'");
		}
		else
		{
			stringBuilder.Append("1=1");
		}
		if (!string.IsNullOrEmpty(this.txtBeginDate.Text))
		{
			stringBuilder.Append(" AND OccurredDate >='").Append(this.txtBeginDate.Text + " 00:00:00.000").Append("' ");
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text))
		{
			stringBuilder.Append(" AND OccurredDate <'").Append(Convert.ToDateTime(this.txtEndDate.Text).AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00.000").Append("' ");
		}
		string value = this.HiddenField1.Value;
		DataTable dataTable = new DataTable();
		DataTable dataTable2 = new DataTable();
		Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
		dataTable = fund_Prj_Accoun.initialFundByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row = array2[i];
				dataTable2.ImportRow(row);
			}
			this.GridView1.DataSource = dataTable2;
			this.GridView1.DataBind();
			string[] value2 = new string[]
			{
				dataTable2.Compute("SUM(INMoney)", string.Empty).ToString()
			};
			int[] index = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView1, value2, index);
		}
		dataTable = fund_Prj_Accoun.OtherIncomeByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array3 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				DataRow row2 = array4[j];
				dataTable2.ImportRow(row2);
			}
			this.GridView2.DataSource = dataTable2;
			this.GridView2.DataBind();
			string[] value3 = new string[]
			{
				dataTable2.Compute("SUM(INMoney)", string.Empty).ToString()
			};
			int[] index2 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView2, value3, index2);
		}
		dataTable = fund_Prj_Accoun.ContIncomeRZByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array5 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array6 = array5;
			for (int k = 0; k < array6.Length; k++)
			{
				DataRow row3 = array6[k];
				dataTable2.ImportRow(row3);
			}
			this.GridView3.DataSource = dataTable2;
			this.GridView3.DataBind();
			string[] value4 = new string[]
			{
				dataTable2.Compute("SUM(INMoney)", string.Empty).ToString()
			};
			int[] index3 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView3, value4, index3);
		}
		dataTable = fund_Prj_Accoun.LoanFundByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array7 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array8 = array7;
			for (int l = 0; l < array8.Length; l++)
			{
				DataRow row4 = array8[l];
				dataTable2.ImportRow(row4);
			}
			this.GridView4.DataSource = dataTable2;
			this.GridView4.DataBind();
			string[] value5 = new string[]
			{
				dataTable.Compute("SUM(LoanFund)", string.Empty).ToString()
			};
			int[] index4 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView4, value5, index4);
		}
		dataTable = fund_Prj_Accoun.ReturnLoanByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array9 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array10 = array9;
			for (int m = 0; m < array10.Length; m++)
			{
				DataRow row5 = array10[m];
				dataTable2.ImportRow(row5);
			}
			this.GridView5.DataSource = dataTable2;
			this.GridView5.DataBind();
			string[] value6 = new string[]
			{
				dataTable.Compute("SUM(FR_Money)", string.Empty).ToString(),
				dataTable.Compute("SUM(FR_interest)", string.Empty).ToString()
			};
			int[] index5 = new int[]
			{
				4,
				5
			};
			GridViewUtility.AddTotalRow(this.GridView5, value6, index5);
		}
		dataTable = fund_Prj_Accoun.MustPaidContByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array11 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array2 = array11;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row6 = array2[i];
				dataTable2.ImportRow(row6);
			}
			this.GridView6.DataSource = dataTable2;
			this.GridView6.DataBind();
			string[] value7 = new string[]
			{
				dataTable.Compute("SUM(PaymentMoney)", string.Empty).ToString()
			};
			int[] index6 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView6, value7, index6);
		}
		dataTable = fund_Prj_Accoun.MustPaidOtherCostByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array12 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array2 = array12;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row7 = array2[i];
				dataTable2.ImportRow(row7);
			}
			this.GridView7.DataSource = dataTable2;
			this.GridView7.DataBind();
			string[] value8 = new string[]
			{
				dataTable.Compute("SUM(Amount)", string.Empty).ToString()
			};
			int[] index7 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView7, value8, index7);
		}
		dataTable = fund_Prj_Accoun.PaidContByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array13 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array2 = array13;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row8 = array2[i];
				dataTable2.ImportRow(row8);
			}
			this.GridView8.DataSource = dataTable2;
			this.GridView8.DataBind();
			string[] value9 = new string[]
			{
				dataTable.Compute("SUM(PayOutMoney)", string.Empty).ToString()
			};
			int[] index8 = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.GridView8, value9, index8);
		}
		dataTable = fund_Prj_Accoun.PaidOtherCostByAccountID(value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array14 = dataTable.Select(stringBuilder.ToString());
			dataTable2 = dataTable.Clone();
			DataRow[] array2 = array14;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row9 = array2[i];
				dataTable2.ImportRow(row9);
			}
			this.GridView9.DataSource = dataTable2;
			this.GridView9.DataBind();
		}
	}
	public DataTable Sort(DataTable dt, string sortstr)
	{
		DataView defaultView = dt.DefaultView;
		defaultView.Sort = sortstr;
		return defaultView.ToTable();
	}
}


