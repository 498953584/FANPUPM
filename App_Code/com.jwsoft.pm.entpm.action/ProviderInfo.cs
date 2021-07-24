namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Text;
    using System.Web.Security;

    public class ProviderInfo
    {
        private string SysPwd = ConfigurationSettings.AppSettings["SysPwd"].ToString();

        public int Add(ProviderInfoModel model, string bs)
        {
            string str = "";
            if (bs == "0")
            {
                str = FormsAuthentication.HashPasswordForStoringInConfigFile(this.SysPwd, "md5");
            }
            if (bs == "1")
            {
                str = FormsAuthentication.HashPasswordForStoringInConfigFile(model.PassWord, "md5");
            }
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_provider_info", "ProviderId")) + 1;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_provider_info(");
            builder.Append("ProviderId,ClassId,ProviderName,ProviderICode,State,ServiceArea,Address,Contacter,Telphon,Mobile,WebStie,[E-mail],PriceRange,Comment,UserName,PassWord,UserCode,RecordDate,ProduceDimensions,TradeStatus,MarketDispositionf,BusinessStanding,CraftworkEquip,ProductQuality,ManufacturingCapacity,Equip,Manpower,OutstandingAchievement,ServiceEnsure,MarketingStrategy");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(num.ToString() + ",");
            builder.Append(model.ClassId.ToString() + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.ProviderName.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.ProviderICode.Trim()) + ",");
            builder.Append("'" + model.State + "',");
            builder.Append(SqlStringConstructor.GetQuotedString(model.ServiceArea.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.Address.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.Contacter.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.Telphon.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.Mobile.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.WebStie.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.Email.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.PriceRange.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.Comment.Trim()) + ",");
            builder.Append(SqlStringConstructor.GetQuotedString(model.UserName.Trim()) + ",");
            builder.Append("'" + str + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.ProduceDimensions + "',");
            builder.Append("'" + model.TradeStatus + "',");
            builder.Append("'" + model.MarketDispositionf + "',");
            builder.Append("'" + model.BusinessStanding + "',");
            builder.Append("'" + model.CraftworkEquip + "',");
            builder.Append("'" + model.ProductQuality + "',");
            builder.Append("'" + model.ManufacturingCapacity + "',");
            builder.Append("'" + model.Equip + "',");
            builder.Append("'" + model.Manpower + "',");
            builder.Append("'" + model.OutstandingAchievement + "',");
            builder.Append("'" + model.ServiceEnsure + "',");
            builder.Append("'" + model.MarketingStrategy + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static bool Cancel(string ProviderId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pm_provider_info set State='3', CancelTime='", DateTime.Now, "'  where ProviderId=", ProviderId }));
        }

        public string CheckProider(string ProviderId)
        {
            string str = "";
            int num = 0;
            int num2 = 0;
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from PM_Prj_Res_Provider where ProviderId=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、该供应商存在物品信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_prvd_accessory where ProviderId=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、该供应商存在资质证明信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_provider_AssessDetail where ProviderId=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、已发起考评；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from PM_WBS_Builder where BuilderID=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、已被工程项目应用；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from PM_Purc_BidProvider where ProviderId=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、已被招标采购应用；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from PM_Purc_InquiryProvPrice where ProviderId=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、已被采购询价应用；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from PM_Purc_AffirmPriceInfo where ProviderId=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、已被采购认价应用；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_ContInfo where SecondPartyID=" + ProviderId);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、已被合同应用；";
            }
            if (!(str != ""))
            {
                return "0";
            }
            return str;
        }

        private int CreateLevel(DataTable dtPrjClassTemp, string ClassCode)
        {
            DataRow[] rowArray = dtPrjClassTemp.Select("ClassCode='" + ClassCode + "'");
            if (rowArray[0]["ClassCode"].ToString().Length == 3)
            {
                return 0;
            }
            return (this.CreateLevel(dtPrjClassTemp, rowArray[0]["ClassCode"].ToString().Substring(0, rowArray[0]["ClassCode"].ToString().Length - 3)) + 1);
        }

        public bool Delete(string ProviderId)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_provider_info where ProviderId = " + ProviderId);
        }

        public bool EnterIn(string ProviderId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pm_provider_info set State = '2',CancelTime='", DateTime.Parse("1900-1-1"), "' where ProviderId=", ProviderId }));
        }

        public bool Exists(int ProviderId)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_provider_info where ProviderId=" + ProviderId);
            object objA = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(objA.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public DataTable GetAssessProList(string ProviderIds)
        {
            return publicDbOpClass.DataTableQuary("select *   from pm_provider_info  where ProviderId in (" + ProviderIds + ")");
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_provider_info ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by ProviderId ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ProviderInfoModel GetModel(int ProviderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_provider_info ");
            builder.Append(" where ProviderId=" + ProviderId);
            ProviderInfoModel model = new ProviderInfoModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            model.ProviderId = ProviderId;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ClassId"].ToString() != "")
            {
                model.ClassId = int.Parse(set.Tables[0].Rows[0]["ClassId"].ToString());
            }
            model.ProviderName = set.Tables[0].Rows[0]["ProviderName"].ToString();
            model.ProviderICode = set.Tables[0].Rows[0]["ProviderICode"].ToString();
            if (set.Tables[0].Rows[0]["State"].ToString() != "")
            {
                model.State = set.Tables[0].Rows[0]["State"].ToString().Trim();
            }
            model.ServiceArea = set.Tables[0].Rows[0]["ServiceArea"].ToString();
            model.Address = set.Tables[0].Rows[0]["Address"].ToString();
            model.Contacter = set.Tables[0].Rows[0]["Contacter"].ToString();
            model.Telphon = set.Tables[0].Rows[0]["Telphon"].ToString();
            model.Mobile = set.Tables[0].Rows[0]["Mobile"].ToString();
            model.WebStie = set.Tables[0].Rows[0]["WebStie"].ToString();
            model.Email = set.Tables[0].Rows[0]["E-mail"].ToString();
            model.PriceRange = set.Tables[0].Rows[0]["PriceRange"].ToString();
            if (set.Tables[0].Rows[0]["Score"].ToString() != "")
            {
                model.Score = decimal.Parse(set.Tables[0].Rows[0]["Score"].ToString());
            }
            model.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            model.PassWord = set.Tables[0].Rows[0]["PassWord"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
            {
                model.LastLoginTime = DateTime.Parse(set.Tables[0].Rows[0]["LastLoginTime"].ToString());
            }
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            model.Comment = set.Tables[0].Rows[0]["Comment"].ToString();
            return model;
        }

        public string GetNewLoginID(string LoginName)
        {
            return publicDbOpClass.ExecuteScalar("select top 1 ProviderId from pm_provider_info where UserName = '" + LoginName + "'").ToString();
        }

        public DataTable GetOneProvidrInfo(string ProviderId)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Provider_Info where ProviderId ='" + ProviderId + "'");
        }

        public DataTable GetProvClassStru()
        {
            DataTable dtPrjClassStru = new DataTable();
            dtPrjClassStru.Columns.Add("ClassId", typeof(string));
            dtPrjClassStru.Columns.Add("ClassName", typeof(string));
            DataTable providerClassList = this.GetProviderClassList();
            DataRow[] rowArray = providerClassList.Select("len(ClassCode) = 3");
            if (rowArray.Length > 0)
            {
                for (int i = 0; i < rowArray.Length; i++)
                {
                    DataRow row = dtPrjClassStru.NewRow();
                    row["ClassId"] = rowArray[i]["ClassId"].ToString();
                    row["ClassName"] = rowArray[i]["ClassName"].ToString();
                    dtPrjClassStru.Rows.Add(row);
                    if (this.GetSubClassCount(providerClassList, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_GenerateFirst(dtPrjClassStru, providerClassList, rowArray[i]["ClassCode"].ToString());
                    }
                }
                return dtPrjClassStru;
            }
            DataRow row2 = dtPrjClassStru.NewRow();
            row2["ClassId"] = "";
            row2["ClassName"] = "---没有供应商分类---";
            dtPrjClassStru.Rows.Add(row2);
            return dtPrjClassStru;
        }

        public DataTable GetProviderClassList()
        {
            string sqlString = "select * from pm_provider_class where state = '1' order by ClassCode ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetProviderList(string ProviderIds)
        {
            return publicDbOpClass.DataTableQuary("select *,StateName =  (case State  when '0' then '已删除' when '1' then '已准入' when '2' then '未准入' when '3' then '已注销' end )  from pm_Provider_Info where ProviderId in (" + ProviderIds + ")");
        }

        public DataTable GetProvidrInfo(string ClassId)
        {
            return publicDbOpClass.DataTableQuary("select *,StateName =  (case State  when '0' then '已删除' when '1' then '已准入' when '2' then '未准入' when '3' then '已注销' end )  ,dbo.f_Prj_SendBidTimes(ProviderId) as SendBidTimes ,dbo.f_Prj_CastBidTimes(ProviderId) as CastBidTimes ,dbo.f_Prj_ProvAssessScoring(0,ProviderId) as AssessScoring ,CancelTime from pm_Provider_Info where ClassId =" + ClassId);
        }

        public DataTable GetProvidrInfo(string ClassId, string State)
        {
            string sqlString = "";
            if (State == "")
            {
                sqlString = "select *,StateName =  (case State  when '0' then '已删除' when '1' then '已准入' when '2' then '未准入' when '3' then '已注销' end )  from pm_Provider_Info where ClassId =" + ClassId;
            }
            else
            {
                sqlString = "select *,StateName =  (case State  when '0' then '已删除' when '1' then '已准入' when '2' then '未准入' when '3' then '已注销' end )  from pm_Provider_Info where ClassId =" + ClassId + " and State ='" + State + "'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetProvidrInfo_sh(string pasid)
        {
            return publicDbOpClass.DataTableQuary("select *,StateName ='正在处理'  ,dbo.f_Prj_SendBidTimes(ProviderId) as SendBidTimes ,dbo.f_Prj_CastBidTimes(ProviderId) as CastBidTimes ,dbo.f_Prj_ProvAssessForAssessResult(ProviderId) as AssessResult ,dbo.f_Prj_ProvAssessForScore(ProviderId) as AssessScoring ,CancelTime from pm_provider_info where recordid ='" + pasid + "'");
        }

        public DataTable GetProvidrInfo1(string ClassId, int flag, string type)
        {
            string str = "";
            if (flag == 1)
            {
                str = " and State!=3";
            }
            if (flag == 2)
            {
                str = " and State=3";
            }
            string sqlString = "";
            if ((type == "Enter") || (type == "other"))
            {
                sqlString = "select *,StateName =  (case enterauditstate  when '0' then '审核中' when '-1' then '未准入' when '1' then '已准入' end )  ,dbo.f_Prj_SendBidTimes(ProviderId) as SendBidTimes ,dbo.f_Prj_CastBidTimes(ProviderId) as CastBidTimes ,dbo.f_Prj_ProvAssessForAssessResult(ProviderId) as AssessResult ,dbo.f_Prj_ProvAssessForScore(ProviderId) as AssessScoring ,CancelTime from pm_Provider_Info where ClassId =" + ClassId + str;
            }
            else if (type == "Void")
            {
                sqlString = "select *,StateName =  (case voidauditstate  when '0' then '审核中' when '-1' then '准入' when '1' then '已注销' end )  ,dbo.f_Prj_SendBidTimes(ProviderId) as SendBidTimes ,dbo.f_Prj_CastBidTimes(ProviderId) as CastBidTimes ,dbo.f_Prj_ProvAssessForAssessResult(ProviderId) as AssessResult ,dbo.f_Prj_ProvAssessForScore(ProviderId) as AssessScoring ,CancelTime from pm_Provider_Info where ClassId =" + ClassId + " and enterauditstate = 1 " + str;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        private int GetSubClassCount(DataTable dtPrjClassTemp, string ClassCode)
        {
            return dtPrjClassTemp.Select(string.Concat(new object[] { " len(ClassCode) - 3 =", ClassCode.Length, " and  ClassCode like '", ClassCode, "%'" })).Length;
        }

        public DataTable GetYH(string YHDM)
        {
            return publicDbOpClass.DataTableQuary("select a.V_DLMM,b.v_xm from pt_login a left join pt_yhmc b on a.v_yhdm=b.v_yhdm where a.V_YHDM='" + YHDM + "'");
        }

        public bool IsHasLoginName(string LoginName, string ProviderId, string opType)
        {
            int num = 0;
            bool flag = false;
            string sqlString = "";
            if (opType == "add")
            {
                sqlString = "select count(1) from pm_provider_info where UserName = '" + LoginName + "'";
            }
            if (opType == "edit")
            {
                sqlString = "select count(1) from pm_provider_info where ProviderId <> " + ProviderId + " and UserName = '" + LoginName + "'";
            }
            num = (int) publicDbOpClass.ExecuteScalar(sqlString);
            if (num > 0)
            {
                flag = true;
            }
            return flag;
        }

        private void PrjClassTree_Generate(DataTable dtPrjClassStru, DataTable dtPrjClassTemp, string ParentClassCode, bool isEnd, int iLevel, string strHead)
        {
            DataRow[] rowArray = dtPrjClassTemp.Select(string.Concat(new object[] { " len(ClassCode) - 3 =", ParentClassCode.Length, " and  ClassCode like '", ParentClassCode, "%'" }));
            if (isEnd)
            {
                for (int i = 0; i < rowArray.Length; i++)
                {
                    if (i != (rowArray.Length - 1))
                    {
                        DataRow row = dtPrjClassStru.NewRow();
                        row["ClassId"] = rowArray[i]["ClassId"].ToString();
                        row["ClassName"] = strHead + this.RepeatString("│", iLevel) + "├" + rowArray[i]["ClassName"].ToString();
                        dtPrjClassStru.Rows.Add(row);
                        if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                        {
                            this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), false, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                        }
                    }
                    else
                    {
                        DataRow row2 = dtPrjClassStru.NewRow();
                        row2["ClassId"] = rowArray[i]["ClassId"].ToString();
                        row2["ClassName"] = strHead + this.RepeatString("│", iLevel) + "└" + rowArray[i]["ClassName"].ToString();
                        dtPrjClassStru.Rows.Add(row2);
                        if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                        {
                            this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), true, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < rowArray.Length; j++)
                {
                    DataRow row3 = dtPrjClassStru.NewRow();
                    row3["ClassId"] = rowArray[j]["ClassId"].ToString();
                    if (j != (rowArray.Length - 1))
                    {
                        row3["ClassName"] = strHead + this.RepeatString("│", iLevel) + "├" + rowArray[j]["ClassName"].ToString();
                    }
                    else
                    {
                        row3["ClassName"] = strHead + this.RepeatString("│", iLevel) + "└" + rowArray[j]["ClassName"].ToString();
                    }
                    dtPrjClassStru.Rows.Add(row3);
                    if (this.GetSubClassCount(dtPrjClassTemp, rowArray[j]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[j]["ClassCode"].ToString(), true, this.CreateLevel(dtPrjClassTemp, rowArray[j]["ClassCode"].ToString()), strHead);
                    }
                }
            }
        }

        private void PrjClassTree_GenerateFirst(DataTable dtPrjClassStru, DataTable dtPrjClassTemp, string ClassCode)
        {
            DataRow[] rowArray = dtPrjClassTemp.Select(string.Concat(new object[] { " len(ClassCode) - 3 =", ClassCode.Length, " and  ClassCode like '", ClassCode, "%'" }));
            for (int i = 0; i < rowArray.Length; i++)
            {
                if (i != (rowArray.Length - 1))
                {
                    DataRow row = dtPrjClassStru.NewRow();
                    row["ClassId"] = rowArray[i]["ClassId"].ToString();
                    row["ClassName"] = "├" + rowArray[i]["ClassName"].ToString();
                    dtPrjClassStru.Rows.Add(row);
                    if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), false, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), "");
                    }
                }
                else
                {
                    DataRow row2 = dtPrjClassStru.NewRow();
                    row2["ClassId"] = rowArray[i]["ClassId"].ToString();
                    row2["ClassName"] = "└" + rowArray[i]["ClassName"].ToString();
                    dtPrjClassStru.Rows.Add(row2);
                    if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), true, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                    }
                }
            }
        }

        private string RepeatString(string strStr, int iNumber)
        {
            string str = "";
            for (int i = 0; i < iNumber; i++)
            {
                str = str + strStr;
            }
            return str;
        }

        public int Update(ProviderInfoModel model, string flag)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(this.SysPwd, "md5");
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_provider_info set  ");
            builder.Append("ProviderName=" + SqlStringConstructor.GetQuotedString(model.ProviderName.Trim()) + ",");
            builder.Append("ProviderICode=" + SqlStringConstructor.GetQuotedString(model.ProviderICode.Trim()) + ",");
            builder.Append("ClassId=" + model.ClassId.ToString() + ",");
            builder.Append("ServiceArea=" + SqlStringConstructor.GetQuotedString(model.ServiceArea.Trim()) + ",");
            builder.Append("Address=" + SqlStringConstructor.GetQuotedString(model.Address.Trim()) + ",");
            builder.Append("Contacter=" + SqlStringConstructor.GetQuotedString(model.Contacter.Trim()) + ",");
            builder.Append("Telphon=" + SqlStringConstructor.GetQuotedString(model.Telphon.Trim()) + ",");
            builder.Append("Mobile=" + SqlStringConstructor.GetQuotedString(model.Mobile.Trim()) + ",");
            builder.Append("WebStie=" + SqlStringConstructor.GetQuotedString(model.WebStie.Trim()) + ",");
            builder.Append("[E-mail]=" + SqlStringConstructor.GetQuotedString(model.Email.Trim()) + ",");
            builder.Append("PriceRange=" + SqlStringConstructor.GetQuotedString(model.PriceRange.Trim()) + ",");
            builder.Append("ProduceDimensions='" + model.ProduceDimensions + "',");
            builder.Append("TradeStatus='" + model.TradeStatus + "',");
            builder.Append("MarketDispositionf='" + model.MarketDispositionf + "',");
            builder.Append("BusinessStanding='" + model.BusinessStanding + "',");
            builder.Append("CraftworkEquip='" + model.CraftworkEquip + "',");
            builder.Append("ProductQuality='" + model.ProductQuality + "',");
            builder.Append("ManufacturingCapacity='" + model.ManufacturingCapacity + "',");
            builder.Append("Equip='" + model.Equip + "',");
            builder.Append("Manpower='" + model.Manpower + "',");
            builder.Append("OutstandingAchievement='" + model.OutstandingAchievement + "',");
            builder.Append("ServiceEnsure='" + model.ServiceEnsure + "',");
            builder.Append("MarketingStrategy='" + model.MarketingStrategy + "',");
            builder.Append("Comment=" + SqlStringConstructor.GetQuotedString(model.Comment.Trim()) + ",");
            if (flag == "y")
            {
                builder.Append("UserName=" + SqlStringConstructor.GetQuotedString(model.UserName.Trim()) + ",");
                builder.Append("PassWord='" + str + "' ");
            }
            else
            {
                builder.Append("UserName=" + SqlStringConstructor.GetQuotedString(model.UserName.Trim()));
            }
            builder.Append(" where ProviderId=" + model.ProviderId.ToString());
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public void UpdateLastLoginTime(string ProviderId)
        {
            publicDbOpClass.NonQuerySqlString("update pm_provider_info set LastLoginTime = getdate() where ProviderId = " + ProviderId);
        }

        public string UpdatePassword(string ProviderId, string OldPassword, string NewPassword)
        {
            string str = "0";
            OldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(OldPassword, "md5");
            NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(NewPassword, "md5");
            if (((int) publicDbOpClass.ExecuteScalar("select count(1) from pm_provider_info where PassWord ='" + OldPassword + "' and ProviderId =" + ProviderId)) == 0)
            {
                return "1";
            }
            if (!publicDbOpClass.NonQuerySqlString("update pm_provider_info set PassWord ='" + NewPassword + "' where PassWord ='" + OldPassword + "' and ProviderId =" + ProviderId))
            {
                str = "-1";
            }
            return str;
        }

        public DataTable ValidatorLoginAccess(string userName, string password)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            return publicDbOpClass.DataTableQuary("select ProviderId,state from pm_provider_info where State <> '0' and  UserName = '" + userName + "' and PassWord = '" + str + "'");
        }
    }
}

