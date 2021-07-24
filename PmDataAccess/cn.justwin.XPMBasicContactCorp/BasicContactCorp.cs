namespace cn.justwin.XPMBasicContactCorp
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BasicContactCorp
    {
        public int ActiveDeptList(int CorpID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update  XPM_Basic_ContactCorp set ");
            builder.Append(" IsValid=1 where CorpID=@CorpID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpID", SqlDbType.Int, 4) };
            commandParameters[0].Value = CorpID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Add(BasicContactCorpModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into XPM_Basic_ContactCorp(");
            builder.Append("CorpTypeID,CorpName,CorpKind,WorkType,Speciality,Aptitude,Capital,UnderlayAbility,Address,CorpBrief,Corporation,LinkMan,Telephone,IFProvider,HandPhone,Fax,ShopCard,TaxCard,AccountBank,BankAccounts,Zone,PostCode,WebSite,PeopleNumber,Client,IsVisible,IsDefault,IsFixed,IsValid,Owner,VersionTime,FlowGuid,AuditState,UserCode,Brand,Email,Contry)");
            builder.Append(" values (");
            builder.Append("@CorpTypeID,@CorpName,@CorpKind,@WorkType,@Zone,@Speciality,@Aptitude,@Capital,@UnderlayAbility,@Address,@CorpBrief,@Corporation,@LinkMan,@Telephone,@IFProvider,@HandPhone,@Fax,@ShopCard,@TaxCard,@AccountBank,@BankAccounts,@PostCode,@WebSite,@PeopleNumber,@Client,@IsVisible,@IsDefault,@IsFixed,@IsValid,@Owner,@VersionTime,@FlowGuid,@AuditState,@UserCode,@Brand,@Email,@Contry)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@CorpTypeID", SqlDbType.Int, 4), new SqlParameter("@CorpName", SqlDbType.NVarChar, 80), new SqlParameter("@CorpKind", SqlDbType.Int, 4), new SqlParameter("@WorkType", SqlDbType.Int, 4), new SqlParameter("@Zone", SqlDbType.VarChar, 200), new SqlParameter("@Speciality", SqlDbType.NVarChar), new SqlParameter("@Aptitude", SqlDbType.NVarChar, 240), new SqlParameter("@Capital", SqlDbType.VarChar, 100), new SqlParameter("@UnderlayAbility", SqlDbType.VarChar, 100), new SqlParameter("@Address", SqlDbType.NVarChar, 100), new SqlParameter("@CorpBrief", SqlDbType.NVarChar, 0xbb8), new SqlParameter("@Corporation", SqlDbType.NVarChar, 0x12), new SqlParameter("@LinkMan", SqlDbType.NVarChar, 20), new SqlParameter("@Telephone", SqlDbType.VarChar, 250), new SqlParameter("@IFProvider", SqlDbType.Bit, 1), new SqlParameter("@HandPhone", SqlDbType.VarChar, 250), 
                new SqlParameter("@Fax", SqlDbType.VarChar), new SqlParameter("@ShopCard", SqlDbType.VarChar, 100), new SqlParameter("@TaxCard", SqlDbType.VarChar, 50), new SqlParameter("@AccountBank", SqlDbType.VarChar, 100), new SqlParameter("@BankAccounts", SqlDbType.VarChar, 50), new SqlParameter("@PostCode", SqlDbType.VarChar, 100), new SqlParameter("@WebSite", SqlDbType.VarChar), new SqlParameter("@PeopleNumber", SqlDbType.VarChar, 100), new SqlParameter("@Client", SqlDbType.VarChar, 50), new SqlParameter("@IsVisible", SqlDbType.Bit, 1), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@IsFixed", SqlDbType.Bit, 1), new SqlParameter("@IsValid", SqlDbType.Bit, 1), new SqlParameter("@Owner", SqlDbType.VarChar, 6), new SqlParameter("@VersionTime", SqlDbType.DateTime), new SqlParameter("@FlowGuid", SqlDbType.UniqueIdentifier, 0x10), 
                new SqlParameter("@AuditState", SqlDbType.Int, 4), new SqlParameter("@UserCode", SqlDbType.VarChar, 50), new SqlParameter("@Brand", SqlDbType.NVarChar, 250), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@Contry", SqlDbType.NVarChar, 300)
             };
            commandParameters[0].Value = model.CorpTypeID;
            commandParameters[1].Value = model.CorpName;
            commandParameters[2].Value = model.CorpKind;
            commandParameters[3].Value = model.WorkType;
            commandParameters[20].Value = model.Zone;
            commandParameters[4].Value = model.Speciality;
            commandParameters[5].Value = model.Aptitude;
            commandParameters[6].Value = model.Capital;
            commandParameters[7].Value = model.UnderlayAbility;
            commandParameters[8].Value = model.Address;
            commandParameters[9].Value = model.CorpBrief;
            commandParameters[10].Value = model.Corporation;
            commandParameters[11].Value = model.LinkMan;
            commandParameters[12].Value = model.Telephone;
            commandParameters[13].Value = model.IFProvider;
            commandParameters[14].Value = model.HandPhone;
            commandParameters[15].Value = model.Fax;
            commandParameters[0x10].Value = model.ShopCard;
            commandParameters[0x11].Value = model.TaxCard;
            commandParameters[0x12].Value = model.AccountBank;
            commandParameters[0x13].Value = model.BankAccounts;
            commandParameters[0x15].Value = model.PostCode;
            commandParameters[0x16].Value = model.WebSite;
            commandParameters[0x17].Value = model.PeopleNumber;
            commandParameters[0x18].Value = model.Client;
            commandParameters[0x19].Value = model.IsVisible;
            commandParameters[0x1a].Value = model.IsDefault;
            commandParameters[0x1b].Value = model.IsFixed;
            commandParameters[0x1c].Value = model.IsValid;
            commandParameters[0x1d].Value = model.Owner;
            commandParameters[30].Value = model.VersionTime;
            commandParameters[0x1f].Value = model.FlowGuid;
            commandParameters[0x20].Value = model.AuditState;
            commandParameters[0x21].Value = model.UserCode;
            commandParameters[0x22].Value = model.Brand;
            commandParameters[0x23].Value = model.Email;
            commandParameters[0x24].Value = model.Contry;
            commandParameters[0x25].Value = model.IsHot;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<BasicContactCorpModel> BasicCorpList()
        {
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            string cmdText = " SELECT * FROM XPM_Basic_ContactCorp ORDER BY VersionTime DESC ";
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public int ContactCorpcount(BasicContactCorpModel Cont)
        {
            string cmdText = " select * from XPM_Basic_ContactCorp where CorpName='" + Cont.CorpName.ToString() + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null).Rows.Count;
        }

        public int DelContactCorp(int CorpID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update  XPM_Basic_ContactCorp set ");
            builder.Append(" IsValid='" + 0 + "', VersionTime=getdate() where CorpID=@CorpID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpID", SqlDbType.Int, 4) };
            commandParameters[0].Value = CorpID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(int CorpID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from XPM_Basic_ContactCorp ");
            builder.Append(" where CorpID=@CorpID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpID", SqlDbType.Int, 4) };
            commandParameters[0].Value = CorpID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(int CorpID, string CorpName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from XPM_Basic_ContactCorp ");
            builder.Append(" where CorpID=@CorpID and CorpName=@CorpName ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpID", SqlDbType.Int, 4), new SqlParameter("@CorpName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = CorpID;
            commandParameters[1].Value = CorpName;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public string GetCodeName(string codid)
        {
            string str = " select * from XPM_Basic_CodeList where CodeID=" + int.Parse(codid);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, str.ToString(), null))
            {
                if (reader.Read())
                {
                    return reader[5].ToString();
                }
                return "";
            }
        }

        public string GetCodeName(string codid, string typid)
        {
            string str = string.Concat(new object[] { " select * from XPM_Basic_CodeList where CodeID=", int.Parse(codid), " and TypeID=", int.Parse(typid) });
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, str.ToString(), null))
            {
                if (reader.Read())
                {
                    return reader[5].ToString();
                }
                return "";
            }
        }

        public List<BasicContactCorpModel> GetList(int COPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where  CorpID=" + COPID);
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where 1=1 ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public BasicContactCorpModel GetModel(int CorpID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  * from XPM_Basic_ContactCorp ");
            builder.Append(" where CorpID=@CorpID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpID", SqlDbType.Int, 4) };
            commandParameters[0].Value = CorpID;
            BasicContactCorpModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public BasicContactCorpModel GetModel(int CorpID, string CorpName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *from XPM_Basic_ContactCorp ");
            builder.Append(" where CorpID=@CorpID and CorpName=@CorpName ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpID", SqlDbType.Int, 4), new SqlParameter("@CorpName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = CorpID;
            commandParameters[1].Value = CorpName;
            BasicContactCorpModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public string GetName(string codid)
        {
            string str = " select * from XPM_Basic_CodeList where NoteID=" + int.Parse(codid);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, str.ToString(), null))
            {
                if (reader.Read())
                {
                    return reader[5].ToString();
                }
                return "";
            }
        }

        public List<BasicContactCorpModel> QueryAllCorpList(int state)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            switch (state)
            {
                case 0:
                    builder.Append(" where IsValid=0");
                    break;

                case 1:
                    builder.Append("  where IsValid = 1");
                    break;
            }
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> QueryCorpList(int typid)
        {
            StringBuilder builder = new StringBuilder();
            if (((typid == 5) || (typid == 4)) || ((typid == 0x13) || (typid == 20)))
            {
                builder.Append("select * from XPM_Basic_ContactCorp where (CorpTypeID = " + typid.ToString() + ")and(IsValid=1) and (AuditState = 1)");
            }
            else
            {
                builder.Append("select * from XPM_Basic_ContactCorp where (CorpTypeID = " + typid.ToString() + ")and(IsValid=1)");
            }
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> QueryCorpList(int typid, string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where CorpTypeID=" + typid);
            builder.Append(" and IsValid=1 order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> QueryCorpList1(string sqlwhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where  " + sqlwhere);
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> QueryCorpList3(int typeid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where  CorpTypeID=" + typeid);
            builder.Append(" and(IsValid=1) and (AuditState<>1)");
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> QueryCorpListBySearch(int typId, string search)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.AppendFormat(" where IsValid=1 AND CorpTypeID='{0}'", typId);
            builder.AppendLine();
            builder.AppendFormat("and CorpName LIKE '%{0}%'", search);
            builder.AppendLine();
            if (((typId == 4) || (typId == 5)) || (((typId == 10) || (typId == 0x13)) || (typId == 20)))
            {
                builder.AppendFormat("and AuditState=1 ", new object[0]).AppendLine();
            }
            builder.Append("order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<BasicContactCorpModel> QuerylistForguidandSta(string guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where FlowGuid='" + guid + "'");
            builder.Append(" and auditstate=0");
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public BasicContactCorpModel ReaderBind(IDataReader dataReader)
        {
            BasicContactCorpModel model = new BasicContactCorpModel();
            object obj2 = dataReader["CorpID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CorpID = (int) obj2;
            }
            obj2 = dataReader["CorpTypeID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CorpTypeID = (int) obj2;
            }
            model.CorpName = dataReader["CorpName"].ToString();
            obj2 = dataReader["CorpKind"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CorpKind = new int?((int) obj2);
            }
            obj2 = dataReader["WorkType"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.WorkType = new int?((int) obj2);
            }
            model.Speciality = dataReader["Speciality"].ToString();
            model.Aptitude = dataReader["Aptitude"].ToString();
            model.Capital = dataReader["Capital"].ToString();
            model.UnderlayAbility = dataReader["UnderlayAbility"].ToString();
            model.Address = dataReader["Address"].ToString();
            model.CorpBrief = dataReader["CorpBrief"].ToString();
            model.Corporation = dataReader["Corporation"].ToString();
            model.LinkMan = dataReader["LinkMan"].ToString();
            model.Telephone = dataReader["Telephone"].ToString();
            obj2 = dataReader["IFProvider"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IFProvider = (bool) obj2;
            }
            model.HandPhone = dataReader["HandPhone"].ToString();
            model.Fax = dataReader["Fax"].ToString();
            model.ShopCard = dataReader["ShopCard"].ToString();
            model.TaxCard = dataReader["TaxCard"].ToString();
            model.AccountBank = dataReader["AccountBank"].ToString();
            model.BankAccounts = dataReader["BankAccounts"].ToString();
            model.Zone = dataReader["Zone"].ToString();
            model.PostCode = dataReader["PostCode"].ToString();
            model.WebSite = dataReader["WebSite"].ToString();
            model.PeopleNumber = dataReader["PeopleNumber"].ToString();
            model.Client = dataReader["Client"].ToString();
            model.Brand = dataReader["Brand"].ToString();
            model.Email = dataReader["Email"].ToString();
            model.Contry = dataReader["Contry"].ToString();
            obj2 = dataReader["IsVisible"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsVisible = (bool) obj2;
            }
            obj2 = dataReader["IsDefault"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsDefault = (bool) obj2;
            }
            obj2 = dataReader["IsFixed"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsFixed = (bool) obj2;
            }
            obj2 = dataReader["IsValid"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsValid = (bool) obj2;
            }
            model.Owner = dataReader["Owner"].ToString();
            obj2 = dataReader["VersionTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.VersionTime = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["FlowGuid"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FlowGuid = new Guid(obj2.ToString());
            }
            obj2 = dataReader["AuditState"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.AuditState = new int?((int) obj2);
            }
            model.UserCode = dataReader["UserCode"].ToString();
            model.IsHot = dataReader["IsHot"].ToString();
            return model;
        }

        public List<BasicContactCorpModel> ShouDelQueryCorpList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM XPM_Basic_ContactCorp ");
            builder.Append(" where IsValid=0");
            builder.Append("  order by VersionTime desc ");
            List<BasicContactCorpModel> list = new List<BasicContactCorpModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public int Update(BasicContactCorpModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update XPM_Basic_ContactCorp set ");
            builder.Append("CorpTypeID=@CorpTypeID,");
            builder.Append("CorpKind=@CorpKind,");
            builder.Append("WorkType=@WorkType,");
            builder.Append("Speciality=@Speciality,");
            builder.Append("Aptitude=@Aptitude,");
            builder.Append("Capital=@Capital,");
            builder.Append("UnderlayAbility=@UnderlayAbility,");
            builder.Append("Address=@Address,");
            builder.Append("CorpBrief=@CorpBrief,");
            builder.Append("Corporation=@Corporation,");
            builder.Append("LinkMan=@LinkMan,");
            builder.Append("Telephone=@Telephone,");
            builder.Append("IFProvider=@IFProvider,");
            builder.Append("HandPhone=@HandPhone,");
            builder.Append("Fax=@Fax,");
            builder.Append("ShopCard=@ShopCard,");
            builder.Append("TaxCard=@TaxCard,");
            builder.Append("AccountBank=@AccountBank,");
            builder.Append("BankAccounts=@BankAccounts,");
            builder.Append("Zone=@Zone,");
            builder.Append("PostCode=@PostCode,");
            builder.Append("WebSite=@WebSite,");
            builder.Append("PeopleNumber=@PeopleNumber,");
            builder.Append("Client=@Client,");
            builder.Append("IsVisible=@IsVisible,");
            builder.Append("IsDefault=@IsDefault,");
            builder.Append("IsFixed=@IsFixed,");
            builder.Append("IsValid=@IsValid,");
            builder.Append("Owner=@Owner,");
            builder.Append("VersionTime=@VersionTime,");
            builder.Append("FlowGuid=@FlowGuid,");
            builder.Append("AuditState=@AuditState,");
            builder.Append("UserCode=@UserCode,");
            builder.Append("Brand=@Brand,");
            builder.Append("Email=@Email,");
            builder.Append("Contry=@Contry,");
            builder.Append("IsHot=@IsHot");
            builder.Append(" where CorpID=@CorpID");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@CorpTypeID", SqlDbType.Int, 4), new SqlParameter("@CorpKind", SqlDbType.Int, 4), new SqlParameter("@WorkType", SqlDbType.Int, 4), new SqlParameter("@Speciality", SqlDbType.NVarChar), new SqlParameter("@Aptitude", SqlDbType.NVarChar, 240), new SqlParameter("@Capital", SqlDbType.VarChar, 100), new SqlParameter("@UnderlayAbility", SqlDbType.VarChar, 100), new SqlParameter("@Address", SqlDbType.NVarChar, 100), new SqlParameter("@CorpBrief", SqlDbType.NVarChar, 0xbb8), new SqlParameter("@Corporation", SqlDbType.NVarChar, 0x12), new SqlParameter("@LinkMan", SqlDbType.NVarChar, 20), new SqlParameter("@Telephone", SqlDbType.VarChar, 250), new SqlParameter("@IFProvider", SqlDbType.Bit, 1), new SqlParameter("@HandPhone", SqlDbType.VarChar, 250), new SqlParameter("@Fax", SqlDbType.VarChar), new SqlParameter("@ShopCard", SqlDbType.VarChar, 100), 
                new SqlParameter("@TaxCard", SqlDbType.VarChar, 50), new SqlParameter("@AccountBank", SqlDbType.VarChar, 100), new SqlParameter("@BankAccounts", SqlDbType.VarChar, 50), new SqlParameter("@Zone", SqlDbType.VarChar, 200), new SqlParameter("@PostCode", SqlDbType.VarChar, 100), new SqlParameter("@WebSite", SqlDbType.VarChar), new SqlParameter("@PeopleNumber", SqlDbType.VarChar, 100), new SqlParameter("@Client", SqlDbType.VarChar, 50), new SqlParameter("@IsVisible", SqlDbType.Bit, 1), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@IsFixed", SqlDbType.Bit, 1), new SqlParameter("@IsValid", SqlDbType.Bit, 1), new SqlParameter("@Owner", SqlDbType.VarChar, 6), new SqlParameter("@VersionTime", SqlDbType.DateTime), new SqlParameter("@FlowGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@AuditState", SqlDbType.Int, 4), 
                new SqlParameter("@UserCode", SqlDbType.VarChar, 50), new SqlParameter("@Brand", SqlDbType.NVarChar, 250), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@Contry", SqlDbType.NVarChar, 300), new SqlParameter("@IsHot", SqlDbType.NVarChar, 10), new SqlParameter("@CorpID", SqlDbType.Int, 4), new SqlParameter("@CorpName", SqlDbType.NVarChar, 80)
             };
            commandParameters[0].Value = model.CorpTypeID;
            commandParameters[1].Value = model.CorpKind;
            commandParameters[2].Value = model.WorkType;
            commandParameters[3].Value = model.Speciality;
            commandParameters[4].Value = model.Aptitude;
            commandParameters[5].Value = model.Capital;
            commandParameters[6].Value = model.UnderlayAbility;
            commandParameters[7].Value = model.Address;
            commandParameters[8].Value = model.CorpBrief;
            commandParameters[9].Value = model.Corporation;
            commandParameters[10].Value = model.LinkMan;
            commandParameters[11].Value = model.Telephone;
            commandParameters[12].Value = model.IFProvider;
            commandParameters[13].Value = model.HandPhone;
            commandParameters[14].Value = model.Fax;
            commandParameters[15].Value = model.ShopCard;
            commandParameters[0x10].Value = model.TaxCard;
            commandParameters[0x11].Value = model.AccountBank;
            commandParameters[0x12].Value = model.BankAccounts;
            commandParameters[0x13].Value = model.Zone;
            commandParameters[20].Value = model.PostCode;
            commandParameters[0x15].Value = model.WebSite;
            commandParameters[0x16].Value = model.PeopleNumber;
            commandParameters[0x17].Value = model.Client;
            commandParameters[0x18].Value = model.IsVisible;
            commandParameters[0x19].Value = model.IsDefault;
            commandParameters[0x1a].Value = model.IsFixed;
            commandParameters[0x1b].Value = model.IsValid;
            commandParameters[0x1c].Value = model.Owner;
            commandParameters[0x1d].Value = model.VersionTime;
            commandParameters[30].Value = model.FlowGuid;
            commandParameters[0x1f].Value = model.AuditState;
            commandParameters[0x20].Value = model.UserCode;
            commandParameters[0x21].Value = model.Brand;
            commandParameters[0x22].Value = model.Email;
            commandParameters[0x23].Value = model.Contry;
            commandParameters[0x24].Value = model.IsHot;
            commandParameters[0x25].Value = model.CorpID;
            commandParameters[0x26].Value = model.CorpName;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

