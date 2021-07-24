namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class ContactCorp
    {
        public int DelSubContractType(SubContractInfo sc)
        {
            string str = "";
            str = " BEGIN ";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " DELETE FROM pt_reputation WHERE reputID = ", sc.CorpID, " " }) + " END ");
        }

        private ConstructionTeamInfo GetConstructionTeamFromDataRow(DataRow dr)
        {
            return new ConstructionTeamInfo { CorpID = (int) dr["CorpID"], CorpName = dr["CorpName"].ToString(), Address = dr["Address"].ToString(), Corporation = dr["Corporation"].ToString(), Telephone = dr["Telephone"].ToString(), Fax = dr["Fax"].ToString(), PostCode = dr["PostCode"].ToString(), SpecialityStrongSuit = dr["Speciality"].ToString(), PeopleNumber = dr["PeopleNumber"].ToString(), CorpBrief = dr["CorpBrief"].ToString() };
        }

        private ContactCorpInfo GetContactCorpFromDataRow(DataRow dr)
        {
            return new ContactCorpInfo { 
                CorpID = (int) dr["CorpID"], CorpName = dr["CorpName"].ToString(), Corporation = dr["Corporation"].ToString(), Address = dr["Address"].ToString(), CorpClassID = dr["CorpTypeID"].ToString(), Telephone = dr["Telephone"].ToString(), CorpKind = dr["CorpKind"].ToString(), LinkMan = dr["LinkMan"].ToString(), IFProvider = (dr["IFProvider"] != DBNull.Value) && ((bool) dr["IFProvider"]), CorpBrief = dr["CorpBrief"].ToString(), HandPhone = dr["HandPhone"].ToString(), Fax = dr["Fax"].ToString(), ShopCard = dr["ShopCard"].ToString(), TaxCard = dr["TaxCard"].ToString(), AccountBank = dr["AccountBank"].ToString(), BankAccounts = dr["BankAccounts"].ToString(), 
                CapitalAsserts = dr["Capital"].ToString(), UnderlayAbility = dr["UnderlayAbility"].ToString(), Zone = dr["Zone"].ToString(), PostCode = dr["PostCode"].ToString(), WebSite = dr["WebSite"].ToString(), SpecialityStrongSuit = dr["Speciality"].ToString(), PeopleNumber = dr["PeopleNumber"].ToString(), WorkType = dr["WorkType"].ToString(), Client = dr["Client"].ToString()
             };
        }

        private ContactProvideInfo GetContactProvideFromDataRow(DataRow dr)
        {
            return new ContactProvideInfo { 
                CorpID = (int) dr["CorpID"], CorpName = dr["CorpName"].ToString(), Corporation = dr["Corporation"].ToString(), LinkMan = dr["LinkMan"].ToString(), Telephone = dr["Telephone"].ToString(), HandPhone = dr["HandPhone"].ToString(), Fax = dr["Fax"].ToString(), ShopCard = dr["ShopCard"].ToString(), TaxCard = dr["TaxCard"].ToString(), Address = dr["Address"].ToString(), AccountBank = dr["AccountBank"].ToString(), BankAccounts = dr["BankAccounts"].ToString(), CorpKind = dr["CorpKind"].ToString(), CapitalAsserts = dr["Capital"].ToString(), UnderlayAbility = dr["UnderlayAbility"].ToString(), Zone = dr["Zone"].ToString(), 
                PostCode = dr["PostCode"].ToString(), WebSite = dr["WebSite"].ToString(), WorkType = dr["WorkType"].ToString(), CorpBrief = dr["CorpBrief"].ToString()
             };
        }

        public DataTable GetCorpClass(int typeID, ValidState vState)
        {
            string sqlString = "";
            sqlString = sqlString + " select * from XPM_Basic_CodeList where (TypeID=" + typeID.ToString() + ") and (IsVisible=1) ";
            switch (vState)
            {
                case ValidState.InValid:
                    sqlString = sqlString + "and(IsValid=0) ";
                    break;

                case ValidState.Valid:
                    sqlString = sqlString + "and(IsValid=1) ";
                    break;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDateTime()
        {
            string sqlString = " select DISTINCT appreDate from v_corp_reputation ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        private OwnerDesignInfo GetOwnerDesignFromDataRow(DataRow dr)
        {
            return new OwnerDesignInfo { CorpID = (int) dr["CorpID"], CorpName = dr["CorpName"].ToString(), Corporation = dr["Corporation"].ToString(), Address = dr["Address"].ToString(), Telephone = dr["Telephone"].ToString(), LinkMan = dr["LinkMan"].ToString(), CorpBrief = dr["CorpBrief"].ToString(), HandPhone = dr["HandPhone"].ToString(), Fax = dr["Fax"].ToString(), ShopCard = dr["ShopCard"].ToString(), PostCode = dr["PostCode"].ToString(), WebSite = dr["WebSite"].ToString(), Client = dr["Client"].ToString(), WorkType = dr["WorkType"].ToString(), CorpKind = dr["CorpKind"].ToString(), CorpClassID = dr["CorpClassID"].ToString() };
        }

        public int GetRecordCount(string tableName, string strWhere)
        {
            return publicDbOpClass.GetRecordCount(tableName, strWhere);
        }

        public ArrayList GetRecordFromPage(string tableName, string fieldName, int pageSize, int pageIndex, int orderType, string strWhere, ContractCorpType corpType)
        {
            ArrayList list = new ArrayList();
            using (DataTable table = publicDbOpClass.GetRecordFromPage(tableName, fieldName, pageSize, pageIndex, orderType, strWhere))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    switch (corpType)
                    {
                        case ContractCorpType.ContractCorp:
                            list.Add(this.GetContactCorpFromDataRow(table.Rows[i]));
                            break;

                        case ContractCorpType.SubContract:
                            list.Add(this.GetSubContractFromDataRow(table.Rows[i]));
                            break;

                        case ContractCorpType.ConstructionTeam:
                            list.Add(this.GetConstructionTeamFromDataRow(table.Rows[i]));
                            break;

                        case ContractCorpType.OwnerDesign:
                            list.Add(this.GetOwnerDesignFromDataRow(table.Rows[i]));
                            break;

                        case ContractCorpType.ContactProvide:
                            list.Add(this.GetContactProvideFromDataRow(table.Rows[i]));
                            break;
                    }
                }
            }
            return list;
        }

        private SubContractInfo GetSubContractFromDataRow(DataRow dr)
        {
            return new SubContractInfo { CorpID = (int) dr["CorpID"], CorpName = dr["CorpName"].ToString(), SpecialityStrongSuit = dr["Speciality"].ToString(), Corporation = dr["Corporation"].ToString(), Telephone = dr["Telephone"].ToString(), PeopleNumber = dr["PeopleNumber"].ToString(), AccountBank = dr["AccountBank"].ToString(), BankAccounts = dr["BankAccounts"].ToString(), CorpBrief = dr["CorpBrief"].ToString() };
        }
    }
}

