using System;
using System.Collections;
using System.Web.UI.WebControls;
namespace qiupeng.sql
{
	public class BindDrowDownList
	{
		private Db List = new Db();
		public void Bind_DropDownListLm(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "全部栏目";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListBuwei(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "不明确部位";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListNodeName(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "全部步骤";
			listItem.Value = "全部步骤";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownList(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "请选择";
			listItem.Value = "请选择";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListHeTong(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "全部合同";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListFlow(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "所有表单类型";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListmodle(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "全部类型";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListLiuCheng(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "所有流程";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListMetting(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "全部会议室";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListCar(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "全部车辆";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListFlowBh(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "开始";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListForm(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "表单字段";
			listItem.Value = string.Concat(new string[]
			{
				"",
				DateTime.Now.Year.ToString(),
				"",
				DateTime.Now.Month.ToString(),
				"",
				DateTime.Now.Day.ToString(),
				"",
				DateTime.Now.Hour.ToString(),
				"",
				DateTime.Now.Minute.ToString(),
				"",
				DateTime.Now.Second.ToString(),
				"",
				DateTime.Now.Millisecond.ToString(),
				""
			});
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListFormCh(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "已创建公式";
			listItem.Value = "";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownListFormZd(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "选择字段";
			listItem.Value = "";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownList_unit(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "根节点";
			listItem.Value = "0";
			MyDropDownList.Items.Insert(0, listItem);
		}
		public void Bind_DropDownList_kong(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
			listItem.Text = "";
			listItem.Value = "";
			MyDropDownList.Items.Insert(0, listItem);
		}
        //无空格方法
        public void Bind_DropDownList_ListBox(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
        {
            MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
            MyDropDownList.DataValueField = DataValueField;
            MyDropDownList.DataTextField = DataTextField;
            MyDropDownList.DataBind();
        }
		public void Bind_DropDownList_ListBox(ListBox MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_CheckBoxList(CheckBoxList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_nothing(DropDownList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
		}
		public void Bind_DropDownList_nothing1(RadioButtonList MyDropDownList, string SQL, string DataValueField, string DataTextField)
		{
			MyDropDownList.DataSource = this.List.GetGrid(SQL, "DataView");
			MyDropDownList.DataValueField = DataValueField;
			MyDropDownList.DataTextField = DataTextField;
			MyDropDownList.DataBind();
			ListItem listItem = new ListItem();
		}
		public void Bind_DropDownList_Year(DropDownList MyDropDownList, int _Begin, int _End)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add("");
			for (int i = _Begin; i <= _End; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_YearForSa(DropDownList MyDropDownList, int _Begin, int _End)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = _Begin; i <= _End; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_MonthForSa(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 1; i < 13; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_Month(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add("----");
			for (int i = 1; i < 13; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_Date(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 1; i < 32; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_Kq(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 1; i < 29; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_Hour(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < 24; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_Mini(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < 60; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
		public void Bind_DropDownList_Age(DropDownList MyDropDownList)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add("[请选择年龄]");
			for (int i = 20; i < 50; i++)
			{
				arrayList.Add(i.ToString());
			}
			MyDropDownList.DataSource = arrayList;
			MyDropDownList.DataBind();
		}
	}
}
