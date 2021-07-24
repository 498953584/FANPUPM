<%@ WebHandler Language="C#" Class="GetSalaryInfo" %>

using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class SaBooksItem
{
    public string Id
    {
        get;
        set;
    }
    public string ItemName
    {
        get;
        set;
    }
}
public class GetSalaryInfo : IHttpHandler
{
	private SASalaryBooksItemService saBooksItemService = new SASalaryBooksItemService();
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/json";
		string a = string.Empty;
		string id = string.Empty;
		string s = string.Empty;
		string a2 = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["page"]))
		{
			a2 = context.Request["page"].ToString();
			if (a2 == "salaryItem")
			{
				SASalaryItemService sASalaryItemService = new SASalaryItemService();
				if (!string.IsNullOrEmpty(context.Request["type"]))
				{
					a = context.Request["type"].ToString();
					if (a == "edit")
					{
						if (!string.IsNullOrEmpty(context.Request["id"]))
						{
							id = context.Request["id"].ToString();
							SASalaryItem byId = sASalaryItemService.GetById(id);
							if (byId != null)
							{
								s = JsonHelper.JsonSerializer<SASalaryItem>(byId);
							}
						}
					}
					else
					{
						if (a == "GetMaxNo")
						{
							s = sASalaryItemService.Max((SASalaryItem si) => si.No).ToString();
						}
					}
				}
			}
			else
			{
				if (a2 == "salaryBooks")
				{
					SASalaryBooksService sASalaryBooksService = new SASalaryBooksService();
					a = context.Request["type"].ToString();
					if (a == "edit" && !string.IsNullOrEmpty(context.Request["id"]))
					{
						id = context.Request["id"].ToString();
						SASalaryBooks byId2 = sASalaryBooksService.GetById(id);
						if (byId2 != null)
						{
							s = JsonHelper.JsonSerializer<SASalaryBooks>(byId2);
						}
					}
				}
				else
				{
					if (a2 == "salaryBooksItem" && !string.IsNullOrEmpty(context.Request["type"]))
					{
						a = context.Request["type"].ToString();
						if (a == "GetById")
						{
							if (!string.IsNullOrEmpty(context.Request["id"]))
							{
								id = context.Request["id"].ToString();
								SASalaryBooksItem byId3 = this.saBooksItemService.GetById(id);
								if (byId3 != null)
								{
									if (!string.IsNullOrEmpty(byId3.Formula))
									{
										string text = byId3.Formula;
										string[] array = text.Split(new char[]
										{
											'['
										});
										List<string> list = new List<string>();
										string[] array2 = array;
										for (int i = 0; i < array2.Length; i++)
										{
											string text2 = array2[i];
											if (text2.Contains(']'))
											{
												string item = text2.Substring(0, text2.IndexOf(']'));
												if (!list.Contains(item))
												{
													list.Add(item);
												}
											}
										}
										List<SaBooksItem> list2 = new List<SaBooksItem>();
										SASalaryItemService sASalaryItemService2 = new SASalaryItemService();
										foreach (string booksItemId in list)
										{
											SASalaryBooksItem sASalaryBooksItem = (
												from sbi in this.saBooksItemService
												where sbi.Id == booksItemId
												select sbi).FirstOrDefault<SASalaryBooksItem>();
											if (sASalaryBooksItem != null)
											{
												SASalaryItem byId4 = sASalaryItemService2.GetById(sASalaryBooksItem.ItemId);
												SaBooksItem saBooksItem = new SaBooksItem();
												if (byId4 != null)
												{
													saBooksItem.Id = "[" + booksItemId + "]";
													saBooksItem.ItemName = "[" + byId4.Name + "]";
													list2.Add(saBooksItem);
												}
											}
										}
										foreach (SaBooksItem current in list2)
										{
											text = text.Replace(current.Id, current.ItemName);
										}
										byId3.Formula = text;
									}
									if (byId3 != null)
									{
										s = JsonHelper.JsonSerializer<SASalaryBooksItem>(byId3);
									}
								}
							}
						}
						else
						{
							if (a == "GetMaxNo")
							{
								s = this.saBooksItemService.Max((SASalaryBooksItem sbi) => sbi.No).ToString();
							}
							else
							{
								if (a == "GetCountByItemId")
								{
									if (!string.IsNullOrEmpty(context.Request["saBooksId"]) && !string.IsNullOrEmpty(context.Request["saItemId"]))
									{
										string saBooksId = context.Request["saBooksId"];
										string saItemId = context.Request["saItemId"];
										s = (
											from sbi in this.saBooksItemService
											where sbi.BooksId == saBooksId && sbi.ItemId == saItemId
											select sbi).Count<SASalaryBooksItem>().ToString();
									}
								}
								else
								{
									if (a == "GetItemIdsByBooksId")
									{
										if (!string.IsNullOrEmpty(context.Request["saBooksId"]))
										{
											string saBooksId = context.Request["saBooksId"].ToString();
											List<SaBooksItem> list3 = new List<SaBooksItem>();
											List<SASalaryBooksItem> list4 = new List<SASalaryBooksItem>();
											if (!string.IsNullOrEmpty(context.Request["saItemId"]))
											{
												string currentItem = context.Request["saItemId"].ToString();
												//List<string> list;
												list4 = (
													from sbi in this.saBooksItemService
													where sbi.BooksId == saBooksId && sbi.ItemId != currentItem
													select sbi into list0
													orderby list0.No
													select list0).ToList<SASalaryBooksItem>();
											}
											else
											{
												//List<string> list;
												list4 = (
													from sbi in this.saBooksItemService
													where sbi.BooksId == saBooksId
													select sbi into list0
													orderby list0.No
													select list0).ToList<SASalaryBooksItem>();
											}
											SASalaryItemService sASalaryItemService3 = new SASalaryItemService();
											foreach (SASalaryBooksItem current2 in list4)
											{
												SASalaryItem byId5 = sASalaryItemService3.GetById(current2.ItemId);
												SaBooksItem saBooksItem2 = new SaBooksItem();
												if (byId5 != null)
												{
													saBooksItem2.Id = current2.Id;
													saBooksItem2.ItemName = byId5.Name;
													list3.Add(saBooksItem2);
												}
											}
											s = JsonHelper.JsonSerializer<SaBooksItem[]>(list3.ToArray());
										}
									}
									else
									{
										if (a == "verify")
										{
											string text3 = string.Empty;
											string saBookId = string.Empty;
											if (!string.IsNullOrEmpty(context.Request["formula"]))
											{
												if (!string.IsNullOrEmpty(context.Request["saBooksId"]))
												{
													saBookId = context.Request["saBooksId"].ToString();
												}
												text3 = context.Request["formula"].ToString().Replace("%2C", "+");
												SASalaryItemService source = new SASalaryItemService();
												string[] array3 = text3.Split(new char[]
												{
													'['
												});
												bool flag = true;
												List<string> list5 = new List<string>();
												string[] array2 = array3;
												for (int i = 0; i < array2.Length; i++)
												{
													string text4 = array2[i];
													if (text4.Contains(']'))
													{
														string itemsName = text4.Substring(0, text4.IndexOf(']'));
														if (!list5.Contains(itemsName))
														{
															SASalaryItem saItem = (
																from sis in source
																where sis.Name == itemsName
																select sis).FirstOrDefault<SASalaryItem>();
															SASalaryBooksItem sASalaryBooksItem2 = (
																from sbi in this.saBooksItemService
																where sbi.BooksId == saBookId && sbi.ItemId == saItem.Id
																select sbi).FirstOrDefault<SASalaryBooksItem>();
															if (saItem == null || sASalaryBooksItem2 == null)
															{
																flag = false;
																break;
															}
															string b = string.Empty;
															if (!string.IsNullOrEmpty(context.Request["saBooksItemId"]))
															{
																b = context.Request["saBooksItemId"].ToString();
																if (!(sASalaryBooksItem2.Id != b))
																{
																	flag = false;
																	break;
																}
																list5.Add(itemsName);
															}
														}
													}
												}
												if (flag)
												{
													string[] items = list5.ToArray();
													bool flag2 = CalculatorHelper.IsValid(text3, items);
													if (flag2)
													{
														s = "1";
													}
													else
													{
														s = "0";
													}
												}
												else
												{
													s = "0";
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			context.Response.Write(s);
		}
	}
}
