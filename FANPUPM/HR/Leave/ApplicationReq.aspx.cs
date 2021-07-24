using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_ApplicationReq : BasePage, IRequiresSessionState
{
	public string billCode;
	public string reqType;

	protected void Page_Load(object sender, EventArgs e)
	{
		this.billCode = base.Request["ic"].ToString();
		this.reqType = base.Request["mid"].ToString();
		this.labDate.Text = DateTime.Now.ToShortDateString();
		userManageDb userManageDb = new userManageDb();
		this.labSHperson.Text = userManageDb.GetUserName(this.Session["yhdm"].ToString());
		if (this.reqType == "InBill" || this.reqType == "OutBill" || this.reqType == "BackBill" || this.reqType == "Redeploy")
		{
			this.BtnSave.Attributes["onclick"] = "if(!confirm('此次审核将影响库存,是否继续?')){return false;}";
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		string text = "";
		string text2 = this.txtldyj.Text.ToString().Trim();
		if (this.reqType.Equals("qj"))
		{
			text = string.Concat(new string[]
			{
				" UPDATE HR_Leave_Application set remark  = '",
				text2,
				"',auditPerson='",
				this.labSHperson.Text,
				"',AuditState  =",
				this.RadioButtonList1.SelectedValue,
				"  WHERE RecordID='",
				this.billCode,
				"' "
			});
		}
		else
		{
			if (this.reqType.Equals("bg"))
			{
				text = string.Concat(new string[]
				{
					"UPDATE PT_BULLETIN_MAIN set AuditState=",
					this.RadioButtonList1.SelectedValue,
					" where I_BULLETINID='",
					this.billCode,
					"'"
				});
			}
			else
			{
				if (this.reqType.Equals("Contract"))
				{
					text = string.Concat(new string[]
					{
						"update EPM_Con_ContractMain set AuditState=",
						this.RadioButtonList1.SelectedValue,
						" where FlowGuid='",
						this.billCode,
						"'"
					});
					this.Page.RegisterStartupScript("", "<script>alert('" + this.RadioButtonList1.SelectedValue + "');</script>");
				}
				else
				{
					if (this.reqType.Equals("Cost"))
					{
						text = string.Concat(new string[]
						{
							"UPDATE EPM_CostImport set AuditResult  =",
							this.RadioButtonList1.SelectedValue,
							" WHERE RecordID='",
							this.billCode,
							"'"
						});
					}
					else
					{
						if (this.reqType.Equals("AppBill"))
						{
							text = string.Concat(new string[]
							{
								"UPDATE EPM_Stuff_ApplyBill set AuditResult  =",
								this.RadioButtonList1.SelectedValue,
								" WHERE ApplyUniqueCode='",
								this.billCode,
								"'"
							});
						}
						else
						{
							if (this.reqType.Equals("SuppBill"))
							{
								text = string.Concat(new string[]
								{
									"UPDATE EPM_Stuff_SupplyPlan_Main set AuditResult  =",
									this.RadioButtonList1.SelectedValue,
									" WHERE ItemID='",
									this.billCode,
									"'"
								});
							}
							else
							{
								if (this.reqType.Equals("InBill"))
								{
									this.Page.RegisterStartupScript("", "<script>alert('" + this.billCode + "');</script>");
									if (this.billCode != "")
									{
										text = " begin ";
										string text3 = text;
										text = string.Concat(new string[]
										{
											text3,
											" UPDATE EPM_Stuff_MaterialIn set AuditResult  = ",
											this.RadioButtonList1.SelectedValue,
											" WHERE StockInCode='",
											this.billCode,
											"' "
										});
										DataTable stockList = GoodsInAction.GetStockList(this.billCode, "ru", "EPM_Stuff_MaterialIn");
										if (stockList != null)
										{
											for (int i = 0; i < stockList.Rows.Count; i++)
											{
												string text4 = stockList.Rows[i]["prjcode"].ToString();
												string text5 = stockList.Rows[i]["StockInCode"].ToString();
												string text6 = stockList.Rows[i]["ResourceCode"].ToString();
												string text7 = GoodsInAction.getcatecode(text6);
												decimal num = Convert.ToDecimal(stockList.Rows[i]["UnitPrice"].ToString());
												string text8 = stockList.Rows[i]["Factory"].ToString();
												decimal num2 = Convert.ToDecimal(stockList.Rows[i]["Quantity"].ToString());
												string text9 = stockList.Rows[i]["FactoryDate"].ToString();
												if (GoodsInAction.ifAddorEdit(text6, text4, num.ToString()) == 0)
												{
													object obj = text;
													text = string.Concat(new object[]
													{
														obj,
														"insert into EPM_Stuff_Stock(StockCode,ResourceCode,CategoryCode,Quantity,Price,Factory,FactoryDate,ModifyDate,ProjectCode) values('",
														text5,
														"','",
														text6,
														"','",
														text7,
														"','",
														num2,
														"','",
														num,
														"','",
														text8,
														"','",
														text9,
														"','",
														DateTime.Now,
														"','",
														text4,
														"')"
													});
												}
												else
												{
													object obj2 = text;
													text = string.Concat(new object[]
													{
														obj2,
														"update EPM_Stuff_Stock set Quantity=Quantity+",
														num2,
														", ModifyDate='",
														DateTime.Now,
														"'  where ProjectCode='",
														text4,
														"' AND ResourceCode='",
														text6,
														"'"
													});
												}
											}
										}
										text += " end ";
									}
								}
								else
								{
									if (this.reqType.Equals("OutBill") && this.billCode != "")
									{
										text = " begin ";
										string text10 = text;
										text = string.Concat(new string[]
										{
											text10,
											" UPDATE EPM_Stuff_MaterialOut set AuditResult  = ",
											this.RadioButtonList1.SelectedValue,
											" WHERE   OutCode='",
											this.billCode,
											"' "
										});
										DataTable stockList2 = GoodsInAction.GetStockList(this.billCode, "chu", "EPM_Stuff_MaterialOut");
										if (stockList2 != null)
										{
											for (int j = 0; j < stockList2.Rows.Count; j++)
											{
												string text11 = stockList2.Rows[j]["prjcode"].ToString();
												string text12 = stockList2.Rows[j]["OutCode"].ToString();
												string text13 = stockList2.Rows[j]["ResourceCode"].ToString();
												string text14 = GoodsInAction.getcatecode(text13);
												decimal num3 = Convert.ToDecimal(stockList2.Rows[j]["Price"].ToString());
												string text15 = stockList2.Rows[j]["Factory"].ToString();
												decimal num4 = Convert.ToDecimal(stockList2.Rows[j]["Fact"].ToString());
												string text16 = stockList2.Rows[j]["FactoryDate"].ToString();
												if (GoodsInAction.ifAddorEdit(text13, text11, num3.ToString()) == 0)
												{
													object obj3 = text;
													text = string.Concat(new object[]
													{
														obj3,
														"insert into EPM_Stuff_Stock(StockCode,ResourceCode,CategoryCode,Quantity,Price,Factory,FactoryDate,ModifyDate,ProjectCode) values('",
														text12,
														"','",
														text13,
														"','",
														text14,
														"',",
														-num4,
														",'",
														num3,
														"','",
														text15,
														"','",
														text16,
														"','",
														DateTime.Now,
														"','",
														text11,
														"')"
													});
												}
												else
												{
													object obj4 = text;
													text = string.Concat(new object[]
													{
														obj4,
														"update EPM_Stuff_Stock set Quantity=Quantity -",
														num4,
														", ModifyDate='",
														DateTime.Now,
														"' where ProjectCode='",
														text11,
														"' AND ResourceCode='",
														text13,
														"' "
													});
												}
											}
										}
										text += " end ";
									}
								}
							}
						}
					}
				}
			}
		}
		if (this.reqType.Equals("BackBill") && this.billCode != "")
		{
			text = " begin ";
			string text17 = text;
			text = string.Concat(new string[]
			{
				text17,
				" UPDATE EPM_Stuff_BackBill set AuditResult  = ",
				this.RadioButtonList1.SelectedValue,
				" WHERE BackCode='",
				this.billCode,
				"' "
			});
			DataTable stockList3 = GoodsInAction.GetStockList(this.billCode, "tui", "EPM_Stuff_MaterialIn");
			if (stockList3 != null)
			{
				for (int k = 0; k < stockList3.Rows.Count; k++)
				{
					string text18 = stockList3.Rows[k]["prjcode"].ToString();
					stockList3.Rows[k]["BackCode"].ToString();
					string text19 = stockList3.Rows[k]["ResourceCode"].ToString();
					GoodsInAction.getcatecode(text19);
					decimal num5 = Convert.ToDecimal(stockList3.Rows[k]["Price"].ToString());
					stockList3.Rows[k]["Factory"].ToString();
					decimal num6 = Convert.ToDecimal(stockList3.Rows[k]["quantity"].ToString());
					stockList3.Rows[k]["FactoryDate"].ToString();
					if (GoodsInAction.ifAddorEdit(text19, text18, num5.ToString()) == 0)
					{
						object obj5 = text;
						text = string.Concat(new object[]
						{
							obj5,
							"insert into EPM_Stuff_Stock(ResourceCode,Quantity,ModifyDate,ProjectCode) values('",
							text19,
							"','",
							num6,
							"',",
							DateTime.Now,
							"','",
							text18,
							"')"
						});
					}
					else
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"UPDATE EPM_Stuff_Stock set Quantity=Quantity+",
							num6,
							",ModifyDate='",
							DateTime.Now,
							"'  where ProjectCode='",
							text18,
							"' and ResourceCode='",
							text19,
							"'"
						});
					}
				}
			}
			text += " end ";
		}
		if (this.reqType.Equals("Redeploy"))
		{
			if (this.billCode != "")
			{
				text = " begin ";
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					" UPDATE EPM_Stuff_Attemper set AuditResult  =  ",
					this.RadioButtonList1.SelectedValue,
					" WHERE   OutCode='",
					this.billCode,
					"' "
				});
				DataTable stockList4 = GoodsInAction.GetStockList(this.billCode, "bo", "EPM_Stuff_Attemper");
				if (stockList4 != null)
				{
					for (int l = 0; l < stockList4.Rows.Count; l++)
					{
						string text20 = stockList4.Rows[l]["ProjectCode"].ToString();
						string text21 = stockList4.Rows[l]["LLDept"].ToString();
						stockList4.Rows[l]["OutCode"].ToString();
						string text22 = stockList4.Rows[l]["ResourceCode"].ToString();
						GoodsInAction.getcatecode(text22);
						decimal num7 = Convert.ToDecimal(stockList4.Rows[l]["Price"].ToString());
						decimal num8 = Convert.ToDecimal(stockList4.Rows[l]["Fact"].ToString());
						if (GoodsInAction.ifAddorEdit(text22, text21, num7.ToString()) == 0)
						{
							this.Page.RegisterStartupScript("", "<script>alert('对不起，拨出物资数量不合法,请求被驳回！');</script>");
						}
						else
						{
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								"update EPM_Stuff_Stock set Quantity=Quantity +",
								-num8,
								",ModifyDate='",
								DateTime.Now,
								"' where ProjectCode='",
								text21,
								"' and ResourceCode='",
								text22,
								"'"
							});
							obj = text;
							text = string.Concat(new object[]
							{
								obj,
								"update EPM_Stuff_Stock set Quantity=Quantity+",
								num8,
								",ModifyDate='",
								DateTime.Now,
								"' where ProjectCode='",
								text20,
								"' and ResourceCode='",
								text22,
								"'"
							});
						}
					}
					text += " end ";
				}
				else
				{
					this.Page.RegisterStartupScript("", "<script>alert('对不起，调拨物资数量为空,请求被驳回!');</script>");
				}
			}
		}
		else
		{
			if (this.reqType.Equals("SystemAutio"))
			{
				text = string.Concat(new string[]
				{
					" update OA_System_Info set AuditState=",
					this.RadioButtonList1.SelectedValue,
					" where RecordID='",
					this.billCode,
					"'"
				});
			}
			else
			{
				if (this.reqType.Equals("PlanAuito"))
				{
					text = string.Concat(new string[]
					{
						"update Ent_Ept_Plan set IsAuditing = ",
						this.RadioButtonList1.SelectedValue,
						"  where PlanCode = '",
						this.billCode,
						"'"
					});
				}
				else
				{
					if (this.reqType.Equals("DeptAtu"))
					{
						text = string.Concat(new string[]
						{
							"update XPM_Basic_ContactCorp set AuditState = ",
							this.RadioButtonList1.SelectedValue,
							"  where CorpID = '",
							this.billCode,
							"'"
						});
					}
				}
			}
		}
		string text23 = AduitAction.SetOK(text);
		AduitAction.setAduitMsg(this.reqType, this.billCode, text2, this.labSHperson.Text, this.RadioButtonList1.SelectedValue);
		if (text23.Trim().Equals("审核失败！"))
		{
			this.Page.RegisterStartupScript("", "<script>alert('" + text23 + "');</script>");
			this.JS.Text = "alert('" + text23 + "')";
			return;
		}
		base.Response.Write("<script>window.opener=null;window.close();</script>");
	}
}


