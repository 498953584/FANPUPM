namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ContactCorpAction
    {
        public void ActiveDeptList(int ID)
        {
            new ContactCorpCollection();
            publicDbOpClass.ExecuteSQL("update XPM_Basic_ContactCorp set IsValid=1,AuditState=5 where (CorpID = " + ID.ToString() + ") ");
        }

        public int AddContactCorp(com.jwsoft.pm.entpm.model.ContactCorp Cont)
        {
            object obj2 = ("" + "insert into  XPM_Basic_ContactCorp ( CorpTypeID,CorpName,CorpKind,WorkType,Speciality,Aptitude,") + "Capital,UnderlayAbility,Address,CorpBrief,Corporation,LinkMan,Telephone,HandPhone,Fax," + "ShopCard,TaxCard,AccountBank,BankAccounts,PostCode,WebSite,PeopleNumber,Client,IsVisible,IsDefault,IsFixed,IsValid,Owner,VersionTime,UserCode ) ";
            string str2 = string.Concat(new object[] { obj2, "values (", Cont.CorpTypeID, ",'", Cont.CorpName.ToString(), "','", Cont.CorpKind.ToString(), "','", Cont.WorkType.ToString(), "'," });
            string str3 = str2 + "'" + Cont.Speciality.ToString() + "','" + Cont.Aptitude.ToString() + "','" + Cont.Capital.ToString() + "','" + Cont.UnderlayAbility.ToString() + "',";
            string str4 = str3 + "'" + Cont.Address.ToString() + "','" + Cont.CorpBrief.ToString() + "','" + Cont.Corporation.ToString() + "','" + Cont.LinkMan.ToString() + "',";
            string str5 = str4 + "'" + Cont.Telephone.ToString() + "','" + Cont.HandPhone.ToString() + "','" + Cont.Fax.ToString() + "','" + Cont.ShopCard.ToString() + "',";
            string str6 = str5 + "'" + Cont.TaxCard.ToString() + "','" + Cont.AccountBank.ToString() + "','" + Cont.BankAccounts.ToString() + "',";
            object obj3 = str6 + "'" + Cont.PostCode.ToString() + "','" + Cont.WebSite.ToString() + "','" + Cont.PeopleNumber.ToString() + "','" + Cont.Client.ToString() + "',";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, Cont.IsVisible ? 1 : 0, ",", Cont.IsDefault ? 1 : 0, ",", Cont.IsFixed ? 1 : 0, ",", Cont.IsValid ? 1 : 0, ",'000000',getdate(),'", Cont.UserCode, "')" }));
        }

        public int ContactCorpcount(com.jwsoft.pm.entpm.model.ContactCorp Cont)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select * from XPM_Basic_ContactCorp where CorpName='" + Cont.CorpName.ToString() + "'").Rows.Count;
        }

        public com.jwsoft.pm.entpm.model.ContactCorp ContactCorpList(int CorpID)
        {
            com.jwsoft.pm.entpm.model.ContactCorp corp = new com.jwsoft.pm.entpm.model.ContactCorp();
            using (DataTable table = publicDbOpClass.DataTableQuary("SELECT * FROM XPM_Basic_ContactCorp where IsValid = 1 and CorpID = " + CorpID))
            {
                if (table.Rows.Count > 0)
                {
                    corp = this.DataRowToContactCorpInfo(table.Rows[0]);
                }
            }
            return corp;
        }

        private com.jwsoft.pm.entpm.model.ContactCorp DataRowToContactCorpInfo(DataRow dr)
        {
            return new com.jwsoft.pm.entpm.model.ContactCorp { 
                CorpID = Convert.ToInt32(dr["CorpID"]), CorpName = dr["CorpName"].ToString(), Corporation = dr["Corporation"].ToString(), CorpKind = (int) dr["CorpKind"], CorpTypeID = (int) dr["CorpTypeID"], Aptitude = dr["Aptitude"].ToString(), Speciality = dr["Speciality"].ToString(), LinkMan = dr["LinkMan"].ToString(), Telephone = dr["Telephone"].ToString(), HandPhone = dr["HandPhone"].ToString(), Fax = dr["Fax"].ToString(), ShopCard = dr["ShopCard"].ToString(), TaxCard = dr["TaxCard"].ToString(), BankAccounts = dr["BankAccounts"].ToString(), AccountBank = dr["AccountBank"].ToString(), Address = dr["Address"].ToString(), 
                Capital = dr["Capital"].ToString(), UnderlayAbility = dr["UnderlayAbility"].ToString(), PostCode = dr["PostCode"].ToString(), WebSite = dr["WebSite"].ToString(), PeopleNumber = dr["PeopleNumber"].ToString(), Client = dr["Client"].ToString(), WorkType = (int) dr["WorkType"], Zone = dr["Zone"].ToString(), CorpBrief = dr["CorpBrief"].ToString(), FlowGuid = new Guid(dr["FlowGuid"].ToString()), AuditState = int.Parse(dr["AuditState"].ToString()), UserCode = dr["UserCode"].ToString()
             };
        }

        public int DelContactCorp(int CorpID)
        {
            return publicDbOpClass.ExecSqlString("update  XPM_Basic_ContactCorp set IsValid = 0,VersionTime=getdate() where CorpID=" + CorpID);
        }

        public static DataTable getOwnerCorpList()
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "SELECT * FROM XPM_Basic_ContactCorp where IsValid = 1 and CorpTypeID in (4,5)");
        }

        public int pzDelCorp(int CorpID)
        {
            return publicDbOpClass.ExecSqlString("delete from XPM_Basic_ContactCorp where CorpID=" + CorpID);
        }

        public ContactCorpCollection QueryAllCorpList(ValidState vState)
        {
            ContactCorpCollection corps = new ContactCorpCollection();
            string sqlString = "select * from XPM_Basic_ContactCorp";
            switch (vState)
            {
                case ValidState.InValid:
                    sqlString = sqlString + " where IsValid = 0 ";
                    break;

                case ValidState.Valid:
                    sqlString = sqlString + " where IsValid = 1 ";
                    break;
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public ContactCorpCollection QueryCorpList(int typeID)
        {
            string str;
            ContactCorpCollection corps = new ContactCorpCollection();
            if (((typeID == 5) || (typeID == 4)) || ((typeID == 0x13) || (typeID == 20)))
            {
                str = "select * from XPM_Basic_ContactCorp where (CorpTypeID = " + typeID.ToString() + ")and(IsValid=1) and (AuditState = 1)";
            }
            else
            {
                str = "select * from XPM_Basic_ContactCorp where (CorpTypeID = " + typeID.ToString() + ")and(IsValid=1)";
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(str))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public ContactCorpCollection QueryCorpList(int typeID, string UserCode)
        {
            ContactCorpCollection corps = new ContactCorpCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_ContactCorp where (CorpTypeID = " + typeID.ToString() + ")and(IsValid=1) "))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public ContactCorpCollection QueryCorpList1(string strwhere)
        {
            ContactCorpCollection corps = new ContactCorpCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_ContactCorp where  " + strwhere + " "))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public ContactCorpCollection QueryCorpList3(int typeID)
        {
            ContactCorpCollection corps = new ContactCorpCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_ContactCorp where (CorpTypeID = " + typeID.ToString() + ")and(IsValid=1) and (AuditState<>1) "))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public ContactCorpCollection QuerylistForguidandSta(string guidcode)
        {
            ContactCorpCollection corps = new ContactCorpCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_ContactCorp where flowguid='" + guidcode + "' and auditstate=0"))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public ContactCorpCollection ShouDelQueryCorpList()
        {
            ContactCorpCollection corps = new ContactCorpCollection();
            string sqlString = "select * from XPM_Basic_ContactCorp where (IsValid=0)";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    corps.Add(this.DataRowToContactCorpInfo(table.Rows[i]));
                }
            }
            return corps;
        }

        public int UpdateContactCorp(com.jwsoft.pm.entpm.model.ContactCorp Cont)
        {
            string str = "";
            object obj2 = str;
            string str2 = string.Concat(new object[] { obj2, "update  XPM_Basic_ContactCorp set CorpTypeID =", Cont.CorpTypeID, " ,CorpName='", Cont.CorpName.ToString(), "'," });
            string str3 = str2 + "CorpKind='" + Cont.CorpKind.ToString() + "',WorkType='" + Cont.WorkType.ToString() + "',Speciality='" + Cont.Speciality.ToString() + "',";
            string str4 = str3 + "Aptitude='" + Cont.Aptitude.ToString() + "',Capital='" + Cont.Capital.ToString() + "',UnderlayAbility='" + Cont.UnderlayAbility.ToString() + "',";
            string str5 = str4 + "Address='" + Cont.Address.ToString() + "',CorpBrief='" + Cont.CorpBrief.ToString() + "',Corporation='" + Cont.Corporation.ToString() + "',";
            string str6 = str5 + "LinkMan='" + Cont.LinkMan.ToString() + "',Telephone='" + Cont.Telephone.ToString() + "',TaxCard='" + Cont.TaxCard.ToString() + "',";
            string str7 = str6 + "HandPhone='" + Cont.HandPhone.ToString() + "',Fax='" + Cont.Fax.ToString() + "',ShopCard='" + Cont.ShopCard.ToString() + "',";
            string str8 = str7 + "AccountBank='" + Cont.AccountBank.ToString() + "',BankAccounts='" + Cont.BankAccounts.ToString() + "',";
            object obj3 = str8 + "PostCode='" + Cont.PostCode.ToString() + "',WebSite='" + Cont.WebSite.ToString() + "',";
            object obj4 = string.Concat(new object[] { obj3, "PeopleNumber='", Cont.PeopleNumber.ToString(), "',Client='", Cont.Client.ToString(), "',IsVisible=", Cont.IsVisible ? 1 : 0, "," });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, "IsDefault=", Cont.IsVisible ? 1 : 0, ",IsFixed=", Cont.IsVisible ? 1 : 0, ",IsValid=", Cont.IsVisible ? 1 : 0, ",VersionTime=getdate() where CorpID =", Cont.CorpID }));
        }
    }
}

