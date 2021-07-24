namespace com.jwsoft.pm.entpm.action.ContractManage
{
    using com.jwsoft.pm.data;
    using Microsoft.Web.UI.WebControls;
    using System;
    using System.Data;
    using System.Text;

    public class ContractClass
    {
        public string ClassCodeS = "'-1'";

        public bool addAudiEssentialInfo(string ContClassID, string EssentialName, string AdvertProceeding, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_AudiEssential (ContClassID,EssentialName,AdvertProceeding,IsValid,Remark,UserCode,RecordDate) values (" + ContClassID + "," + EssentialName + "," + AdvertProceeding + ",'1'," + Remark + ",'" + UserCode + "','" + RecordDate + "')");
        }

        public bool addClauseItemInfo(string ContClassID, string ClauseItemName, string DataType, string SerialNumber, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_ClauseItem (ContClassID,ClauseItemName,DataType,SerialNumber,Remark,UserCode,RecordDate,IsValid) values (" + ContClassID + "," + ClauseItemName + ",'" + DataType + "'," + SerialNumber + "," + Remark + ",'" + UserCode + "','" + RecordDate + "','1')");
        }

        public bool addContClassInfo(string ContClassCode, string ContClassName, string ConClassAttr, string Remark, string UserCode, string RecordDate)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_Cont_Class", "ContClassID")) + 1;
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_Class (ContClassID,ContClassCode,ContClassName,ConClassAttr,Remark,UserCode,RecordDate) values ( " + num.ToString() + ",'" + ContClassCode + "'," + ContClassName + ",'" + ConClassAttr + "'," + Remark + ",'" + UserCode + "','" + RecordDate + "')");
        }

        public bool addTempletInfo(string ContClassID, string TempletName, string Remark, string UserCode, string RecordDate)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_Cont_Templet", "TempletID")) + 1;
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_Templet (TempletID,ContClassID,TempletName,Remark,UserCode,RecordDate) values (" + num.ToString() + "," + ContClassID + "," + TempletName + "," + Remark + ",'" + UserCode + "','" + RecordDate + "')");
        }

        public DataTable ContClassList(string sqlWhere)
        {
            return publicDbOpClass.DataTableQuary("select * ,ConClassAttrName = ( case ConClassAttr  when '0' then '其它合同' when '1' then '项目合同' when '2' then '采购合同' when '3' then '设计合同' when '4' then '施工合同' end ) from pm_Cont_Class " + sqlWhere + " order by ContClassCode");
        }

        public void CreateContClassTree(DataTable dt, TreeNodeCollection Nds, string ContClassCode, string strTarget, string strNavigationURL)
        {
            TreeNode node;
            if (ContClassCode == "")
            {
                DataRow[] rowArray = dt.Select("len(ContClassCode) = 3");
                for (int i = 0; i < rowArray.Length; i++)
                {
                    node = new TreeNode {
                        Text = rowArray[i]["ContClassName"].ToString(),
                        ID = rowArray[i]["ContClassID"].ToString(),
                        NodeData = rowArray[i]["ContClassID"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = strTarget;
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?ContClassID=" + rowArray[i]["ContClassID"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = true;
                    this.ClassCodeS = this.ClassCodeS + ",'" + rowArray[i]["ContClassCode"].ToString() + "'";
                    this.CreateContClassTree(dt, node.Nodes, rowArray[i]["ContClassCode"].ToString(), strTarget, strNavigationURL);
                }
            }
            else
            {
                DataRow[] rowArray2 = dt.Select("ContClassCode like '" + ContClassCode + "%' and (len(ContClassCode) - 3) = " + ContClassCode.Length.ToString());
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    node = new TreeNode {
                        Text = rowArray2[j]["ContClassName"].ToString(),
                        ID = rowArray2[j]["ContClassID"].ToString(),
                        NodeData = rowArray2[j]["ContClassID"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = strTarget;
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?ContClassID=" + rowArray2[j]["ContClassID"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = false;
                    this.ClassCodeS = this.ClassCodeS + ",'" + rowArray2[j]["ContClassCode"].ToString() + "'";
                    this.CreateContClassTree(dt, node.Nodes, rowArray2[j]["ContClassCode"].ToString(), strTarget, strNavigationURL);
                }
            }
        }

        public void CreateContClassTree1(DataTable dt, TreeNodeCollection Nds, string ContClassCode, string strTarget, string strNavigationURL)
        {
            this.CreateContClassTree(dt, Nds, "", "rFrame", strNavigationURL);
            int num = 3;
            do
            {
                num += 3;
                foreach (DataRow row in dt.Select(" ContClassCode not in (" + this.ClassCodeS + ") and len(ContClassCode) = " + num.ToString()))
                {
                    this.CreateContClassTree(dt, Nds, row["ContClassCode"].ToString().Substring(0, row["ContClassCode"].ToString().Length - 3), "rFrame", strNavigationURL);
                }
            }
            while (dt.Select(" ContClassCode not in (" + this.ClassCodeS + ")").GetLength(0) > 0);
        }

        public bool delAudiEssentialInfo(string EssentialID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_AudiEssential where  EssentialID =" + EssentialID);
        }

        public bool delClauseItemInfo(string ClauseItemID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_ClauseItem where ClauseItemID=" + ClauseItemID);
        }

        public bool delContClassInfo(string ContClassID)
        {
            string str = " begin ";
            return publicDbOpClass.NonQuerySqlString(((((((str + " delete pm_Cont_AudiEssential  where ContClassID =" + ContClassID) + " delete pm_Cont_Templet  where ContClassID =" + ContClassID) + " delete pm_Cont_ClauseItem  where ContClassID =" + ContClassID) + " delete pm_Cont_AddOnsItem  where ContClassID =" + ContClassID) + " delete pm_Cont_FastItem  where ContClassID =" + ContClassID) + " delete pm_Cont_Class where ContClassID =" + ContClassID) + " end ");
        }

        public bool delTempletInfo(string TempletID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_Templet where TempletID=" + TempletID);
        }

        public DataTable GetAudiEssential(string ContClassID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_AudiEssential where ContClassID =" + ContClassID);
        }

        public DataTable GetAudiEssentialInfo(string EssentialID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_AudiEssential where EssentialID =" + EssentialID);
        }

        public DataTable GetClassAddOnsItem(string ContClassID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_AddOnsItem where ContClassID =" + ContClassID);
        }

        public DataTable GetClassFastItem(string ContClassID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_FastItem where ContClassID =" + ContClassID);
        }

        public DataTable GetClauseItem(string ContClassID)
        {
            return publicDbOpClass.DataTableQuary("select * ,DataTypeName = ( case DataType  when 'n' then '数字型'  when 'c' then '字符型'  when 'd' then '日期型'  end ) from pm_Cont_ClauseItem where ContClassID =" + ContClassID + " order by SerialNumber");
        }

        public DataTable GetClauseItemInfo(string ClauseItemID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ClauseItem where ClauseItemID =" + ClauseItemID);
        }

        public int GetContClassCount(string ContClassCode)
        {
            object obj2 = DBNull.Value;
            obj2 = publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_Class  where ContClassCode ='" + ContClassCode + "'");
            if (obj2 != DBNull.Value)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        public string GetDutyName(string I_DUTYID)
        {
            string str = string.Empty;
            try
            {
                foreach (DataRow row in publicDbOpClass.DataTableQuary("SELECT dbo.PT_DUTY.i_bmdm,dbo.PT_DUTY.DutyCode, dbo.PT_D_Role.RoleTypeCode, dbo.PT_D_Role.RoleTypeName, dbo.PT_DUTY.I_DUTYID FROM dbo.PT_D_Role INNER JOIN dbo.PT_DUTY ON dbo.PT_D_Role.RoleTypeCode=dbo.PT_DUTY.DutyCode WHERE    dbo.PT_DUTY.I_DUTYID in (" + I_DUTYID + ")").Rows)
                {
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        str = str + row["RoleTypeName"].ToString();
                    }
                    else
                    {
                        str = str + "，" + row["RoleTypeName"].ToString();
                    }
                }
            }
            catch
            {
            }
            return str;
        }

        public string getPurviews(string ContClassID)
        {
            return publicDbOpClass.ExecuteScalar("select isnull(Purview,'')  from pm_Cont_Class where ContClassID=" + ContClassID).ToString();
        }

        public DataTable GetTemplet(string ContClassID)
        {
            return publicDbOpClass.DataTableQuary("select *  from pm_Cont_Templet where ContClassID =" + ContClassID);
        }

        public DataTable GetTempletAnnex(string TempletID, string ModuleID)
        {
            return publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where RecordCode =" + TempletID + " and ModuleID = " + ModuleID);
        }

        public DataTable GetTempletInfo(string TempletID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_Templet where TempletID =" + TempletID);
        }

        public DataTable GetUserContClass(string YHDM)
        {
            string sqlString = "";
            if (YHDM == "")
            {
                sqlString = "select * ,ConClassAttrName = ( case ConClassAttr  when '0' then '其它合同' when '1' then '项目合同' when '2' then '采购合同' when '3' then '设计合同' when '4' then '施工合同' end ) from pm_Cont_Class  order by ContClassCode";
                return publicDbOpClass.DataTableQuary(sqlString);
            }
            DataTable table2 = publicDbOpClass.DataTableQuary(((" select a.* ,ConClassAttrName = ( case a.ConClassAttr  when '0' then '其它合同' when '1' then '项目合同' when '2' then '采购合同' when '3' then '设计合同' when '4' then '施工合同' end ) from pm_Cont_Class a" + " where  Patindex(") + " '%,'+(select cast(i_dutyid as varchar)  from pt_yhmc where v_yhdm = '" + YHDM + "')  +',%'") + " ,','+isnull(a.Purview,'0')+',') > 0  order by a.ContClassCode");
            string str2 = "";
            if (table2.Rows.Count > 0)
            {
                foreach (DataRow row in table2.Rows)
                {
                    if (str2 == "")
                    {
                        str2 = "select * ,ConClassAttrName = ( case ConClassAttr  when '0' then '其它合同' when '1' then '项目合同' when '2' then '采购合同' when '3' then '设计合同' when '4' then '施工合同' end ) from pm_Cont_Class where ContClassCode like '" + row["ContClassCode"].ToString().Trim() + "%'";
                    }
                    else
                    {
                        str2 = str2 + " or ContClassCode like '" + row["ContClassCode"].ToString().Trim() + "%'";
                    }
                }
            }
            else
            {
                str2 = "select * ,ConClassAttrName = ( case ConClassAttr  when '0' then '其它合同' when '1' then '项目合同' when '2' then '采购合同' when '3' then '设计合同' when '4' then '施工合同' end ) from pm_Cont_Class where ContClassCode = '-1'";
            }
            return publicDbOpClass.DataTableQuary(str2);
        }

        public bool IsHaveContInfo(string ContClassID)
        {
            int num = 0;
            bool flag = false;
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_ContInfo where ContClassID = " + ContClassID);
            if (num > 0)
            {
                flag = true;
            }
            return flag;
        }

        public int LowerLevelsCount(string ContClassCode)
        {
            string[] strArray = new string[] { "select count(1) from pm_Cont_Class  where (ContClassCode like '", ContClassCode, "%') and (len(ContClassCode)= ", (ContClassCode.Length + 3).ToString(), ")" };
            return (int) publicDbOpClass.ExecuteScalar(string.Concat(strArray));
        }

        public string MakeClassCode(string Code)
        {
            string str = "";
            int num = Code.Length + 3;
            StringBuilder builder = new StringBuilder();
            if (Code == "")
            {
                builder.Append(" select max(ContClassCode) from pm_Cont_Class where len(ContClassCode)=3");
            }
            else
            {
                builder.Append(" select max(ContClassCode) from pm_Cont_Class where ContClassCode like '" + Code + "%' and len(ContClassCode)=" + num.ToString());
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = (string) obj2;
                str = Convert.ToString((int) (int.Parse(str.Substring(str.Length - 3, 3)) + 1)).PadLeft(3, '0');
                return (Code.Trim() + str);
            }
            return (Code.Trim() + "001");
        }

        public bool updAudiEssentialInfo(string EssentialID, string EssentialName, string AdvertProceeding, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_AudiEssential set EssentialName =" + EssentialName + ",AdvertProceeding =" + AdvertProceeding + ",Remark =" + Remark + ",UserCode ='" + UserCode + "',RecordDate ='" + RecordDate + "' where EssentialID =" + EssentialID);
        }

        public bool updClassAddOnsItem(string ContClassID, string bs, string UserCode, string RecordDate, string Flag1, string Flag2, string Flag3, string Flag4, string Flag5, string Flag6, string Flag7, string Flag8, string Flag9, string Flag10)
        {
            string sqlString = "";
            if (bs == "add")
            {
                sqlString = "insert into pm_Cont_AddOnsItem (ContClassID,UserCode,RecordDate,Flag1,Flag2,Flag3,Flag4,Flag5,Flag6,Flag7,Flag8,Flag9,Flag10) values (" + ContClassID + ",'" + UserCode + "','" + RecordDate + "','" + Flag1 + "','" + Flag2 + "','" + Flag3 + "','" + Flag4 + "','" + Flag5 + "','" + Flag6 + "','" + Flag7 + "','" + Flag8 + "','" + Flag9 + "','" + Flag10 + "')";
            }
            if (bs == "edit")
            {
                sqlString = "update pm_Cont_AddOnsItem set UserCode ='" + UserCode + "',RecordDate ='" + RecordDate + "',Flag1 ='" + Flag1 + "',Flag2 ='" + Flag2 + "',Flag3 ='" + Flag3 + "',Flag4 ='" + Flag4 + "',Flag5 ='" + Flag5 + "',Flag6 ='" + Flag6 + "',Flag7 ='" + Flag7 + "',Flag8 ='" + Flag8 + "',Flag9 ='" + Flag9 + "',Flag10 ='" + Flag10 + "' where ContClassID =" + ContClassID;
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool updClassFastItem(string ContClassID, string bs, string UserCode, string RecordDate, string Flag1, string Flag2, string Flag3, string Flag4, string Flag5, string Flag6, string Flag7, string Flag8, string Flag9, string Flag10, string Flag11, string Flag12, string Flag13, string Flag14, string Flag15, string Flag16, string Flag17)
        {
            string sqlString = "";
            if (bs == "add")
            {
                sqlString = "insert into pm_Cont_FastItem (ContClassID,UserCode,RecordDate,Flag1,Flag2,Flag3,Flag4,Flag5,Flag6,Flag7,Flag8,Flag9,Flag10,Flag11,Flag12,Flag13,Flag14,Flag15,Flag16,Flag17) values (" + ContClassID + ",'" + UserCode + "','" + RecordDate + "','" + Flag1 + "','" + Flag2 + "','" + Flag3 + "','" + Flag4 + "','" + Flag5 + "','" + Flag6 + "','" + Flag7 + "','" + Flag8 + "','" + Flag9 + "','" + Flag10 + "','" + Flag11 + "','" + Flag12 + "','" + Flag13 + "','" + Flag14 + "','" + Flag15 + "','" + Flag16 + "','" + Flag17 + "')";
            }
            if (bs == "edit")
            {
                sqlString = "update pm_Cont_FastItem set UserCode ='" + UserCode + "',RecordDate ='" + RecordDate + "',Flag1 ='" + Flag1 + "',Flag2 ='" + Flag2 + "',Flag3 ='" + Flag3 + "',Flag4 ='" + Flag4 + "',Flag5 ='" + Flag5 + "',Flag6 ='" + Flag6 + "',Flag7 ='" + Flag7 + "',Flag8 ='" + Flag8 + "',Flag9 ='" + Flag9 + "',Flag10 ='" + Flag10 + "',Flag11 ='" + Flag11 + "',Flag12 ='" + Flag12 + "',Flag13 ='" + Flag13 + "',Flag14 ='" + Flag14 + "',Flag15 ='" + Flag15 + "',Flag16 ='" + Flag16 + "',Flag17 ='" + Flag17 + "' where ContClassID =" + ContClassID;
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool updClauseItemInfo(string ClauseItemID, string ClauseItemName, string DataType, string SerialNumber, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_ClauseItem set ClauseItemName =" + ClauseItemName + ",DataType ='" + DataType + "',SerialNumber =" + SerialNumber + ",Remark =" + Remark + ",UserCode ='" + UserCode + "',RecordDate ='" + RecordDate + "' where ClauseItemID =" + ClauseItemID);
        }

        public bool updContClassInfo(string ContClassCode, string ContClassName, string ConClassAttr, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_Class set ContClassName =" + ContClassName + ",ConClassAttr ='" + ConClassAttr + "',Remark =" + Remark + ",UserCode ='" + UserCode + "',RecordDate ='" + RecordDate + "' where ContClassCode ='" + ContClassCode + "'");
        }

        public bool updContClassPurv(string ContClassID, string DutyIDs)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_Class set Purview = '" + DutyIDs + "' where ContClassID=" + ContClassID);
        }

        public bool updTempletInfo(string TempletID, string TempletName, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_Templet set TempletName =" + TempletName + ",Remark =" + Remark + ",UserCode ='" + UserCode + "',RecordDate ='" + RecordDate + "' where TempletID =" + TempletID);
        }
    }
}

