namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class EquipmentAction
    {
        public static int AddEquipment(EquipmentInfo eptInfo)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + string.Format("insert into Ent_Ept_Equipments values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',{16},'{17}')", new object[] { 
                eptInfo.EquipmentUniqueCode, eptInfo.EquipmentType, eptInfo.EquipmentManCode, eptInfo.EquipmentName, eptInfo.Spec, eptInfo.ThePrecision, eptInfo.Manufacturer, eptInfo.FactoryCode, eptInfo.FactoryDate, eptInfo.PurchaseDate, eptInfo.CheckCycle, eptInfo.ServiceYear, eptInfo.OriginalPrice, eptInfo.State, eptInfo.LocateDeptId, eptInfo.Remark, 
                eptInfo.depreciation, eptInfo.ContactDept
             }));
        }

        public int AddEquipmentPlan(EquipmentPlanInfo epi)
        {
            string str = "";
            str = " begin ";
            object obj2 = str + " insert into Ent_Ept_Plan(PlanCode, PlanCreatTime, EnterDate, ExitDate, PlanMaker, PrjCode, Remark,  IsAuditing, IsFullUsed) ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { 
                obj2, " values('", epi.PlanCode, "','", epi.PlanCreatTime.ToShortDateString(), "','", epi.EnterDate.ToShortDateString(), "','", epi.ExitDate.ToShortDateString(), "','", epi.PlanMaker, "','", epi.PrjCode.ToString(), "','", epi.Remark, "', ", 
                epi.IsAuditing, ", ", epi.IsFullUsed, ") "
             }) + " end ");
        }

        public int CheckSubmit(string PlanCode)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.ExecSqlString(((str + " update Ent_Ept_Plan " + " set IsAuditing = 1 ") + " where PlanCode = '" + PlanCode + "' ") + " end ");
        }

        public static int DelEquipment(Guid equipmentUniqueCode)
        {
            return publicDbOpClass.ExecSqlString("delete Ent_Ept_Equipments where EquipmentUniqueCode = '" + equipmentUniqueCode + "'");
        }

        public int DelEquipmentPlan(string PlanCode)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.ExecSqlString(((str + " delete from Ent_Ept_PlanDetail where PlanCode = '" + PlanCode + "' ") + " delete from Ent_Ept_Plan where PlanCode= '" + PlanCode + "'") + " end ");
        }

        public string GetCategoryNameByCategoryCode(string strTypeId)
        {
            return Convert.ToString(publicDbOpClass.ExecuteScalar("select categoryname from EPM_Res_Category where categorycode ='" + strTypeId + "'"));
        }

        public static EquipmentCollection GetEquipmentCollection(string deptId, string equipmentCode, string equipmentName, string equipmentType)
        {
            EquipmentCollection equipments = new EquipmentCollection();
            string sqlWhere = "";
            sqlWhere = "(State=1)";
            if (deptId.ToString() != "")
            {
                sqlWhere = sqlWhere + " and (LocateDeptID = " + deptId + ") ";
            }
            if (equipmentCode.Trim() != "")
            {
                sqlWhere = sqlWhere + " and (EquipmentManualCode like '%" + equipmentCode.Trim() + "%') ";
            }
            if (equipmentName.Trim() != "")
            {
                sqlWhere = sqlWhere + " and (EquipmentName like '%" + equipmentName.Trim() + "%') ";
            }
            if (equipmentType.Trim() != "")
            {
                sqlWhere = sqlWhere + " and (EquipmentType='" + equipmentType + "') ";
            }
            using (DataTable table = publicDbOpClass.GetPageData(sqlWhere, "Ent_Ept_Equipments", "NoteSequenceID desc"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    equipments.Add(GetEquipmentInfoFromDataRow(table.Rows[i]));
                }
            }
            return equipments;
        }

        public static int GetEquipmentCount(string deptId, string equipmentCode, string equipmentName, string equipmentType)
        {
            string str = "";
            string str2 = "";
            str = " select count(1) from Ent_Ept_Equipments where";
            str2 = " (1=1) ";
            if (deptId.ToString() != "")
            {
                str2 = str2 + " and (LocateDeptID = " + deptId + ") ";
            }
            if (equipmentCode.Trim() != "")
            {
                str2 = str2 + " and (EquipmentManualCode like '%" + equipmentCode.Trim() + "%') ";
            }
            if (equipmentName.Trim() != "")
            {
                str2 = str2 + " and (EquipmentName like '%" + equipmentName.Trim() + "%') ";
            }
            if (equipmentType.Trim() != "")
            {
                str2 = str2 + " and (EquipmentType='" + equipmentType + "') ";
            }
            return (int) publicDbOpClass.ExecuteScalar(str + str2);
        }

        private static EquipmentInfo GetEquipmentInfoFromDataRow(DataRow dr)
        {
            EquipmentInfo info = new EquipmentInfo {
                NoteSeqId = (int) dr["NoteSequenceID"],
                EquipmentUniqueCode = new Guid(dr["EquipmentUniqueCode"].ToString()),
                EquipmentType = dr["EquipmentType"].ToString(),
                EquipmentManCode = dr["EquipmentManualCode"].ToString(),
                EquipmentName = dr["EquipmentName"].ToString(),
                Spec = dr["Spec"].ToString(),
                ThePrecision = dr["ThePrecision"].ToString(),
                Manufacturer = dr["Manufacturer"].ToString(),
                FactoryCode = dr["Factorycode"].ToString(),
                FactoryDate = (DateTime) dr["FactoryDate"],
                PurchaseDate = (DateTime) dr["PurchaseDate"],
                CheckCycle = (int) dr["CheckCycle"],
                ServiceYear = (int) dr["ServiceYear"],
                OriginalPrice = (decimal) dr["OriginalPrice"],
                State = (int) dr["State"],
                LocateDeptId = (int) dr["LocateDeptID"],
                Remark = dr["Remark"].ToString(),
                depreciation = decimal.Parse(dr["depreciation"].ToString())
            };
            try
            {
                info.ContactDept = dr["ContactDept"].ToString();
            }
            catch
            {
            }
            try
            {
                info.UserState = (bool) dr["UserState"];
            }
            catch
            {
            }
            return info;
        }

        private EquipmentPlanDetailInfo GetEquipmentPlanDetailFromDataRow(DataRow dr)
        {
            return new EquipmentPlanDetailInfo { ItemID = (int) dr["ItemID"], PlanCode = dr["PlanCode"].ToString(), EquipmentCode = dr["EquipmentCode"].ToString(), EquipmentName = dr["resourceName"].ToString(), EquipmentType = (int) dr["EquipmentType"], EquipmentCount = (decimal) dr["EquipmentCount"], Remark = dr["Remark"].ToString() };
        }

        private EquipmentPlanInfo GetEquipmentPlanFromDataRow(DataRow dr)
        {
            return new EquipmentPlanInfo { PlanID = (int) dr["PlanID"], PlanCode = dr["PlanCode"].ToString(), PlanCreatTime = (DateTime) dr["PlanCreatTime"], EnterDate = (DateTime) dr["EnterDate"], ExitDate = (DateTime) dr["ExitDate"], PlanMaker = dr["PlanMaker"].ToString(), PrjCode = (Guid) dr["PrjCode"], Remark = dr["Remark"].ToString(), IsAuditing = (int) dr["IsAuditing"], IsFullUsed = (int) dr["IsFullUsed"] };
        }

        public ArrayList GetEquipmentPlanList(string PlanCode)
        {
            ArrayList list = new ArrayList();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from V_Ept_Plan_Detail where PlanCode = '" + PlanCode + "'"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetEquipmentPlanDetailFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        public static DataTable getEquPlanTempRes(Guid operateSession, string userCode, string PlanCode)
        {
            string str = "";
            str = "select a.ResourceCode,a.ResourceName,isnull(b.EquipmentCount,0) as EquipmentCount,b.Remark from ";
            string str2 = str;
            return publicDbOpClass.DataTableQuary(((str2 + "(select * from EPM_Res_TempResource where SessionCode = '" + operateSession.ToString() + "' and UserCode = '" + userCode + "' and State>=1) a left join ") + "(select * from Ent_Ept_PlanDetail where PlanCode = '" + PlanCode.ToString() + "') b ") + " on a.ResourceCode = b.EquipmentCode");
        }

        public ArrayList GetRecordFromPage(string TableName, string Fieldsort, string Where)
        {
            ArrayList list = new ArrayList();
            using (DataTable table = publicDbOpClass.GetPageData(Where, TableName, Fieldsort))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetEquipmentPlanFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        public DataTable GetResouceById(string resouceCode)
        {
            return publicDbOpClass.DataTableQuary("select ResourceCode as EquipmentCode,ResourceName,0.00 as EquipmentCount ,'' as Remark  from Res_Resource where ResourceCode in (" + resouceCode + ")");
        }

        public string GetResouceCodeById(string resouceId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select ResourceCode from Res_Resource where resourceId in (" + resouceId + ")");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (str2 == "")
                    {
                        str2 = str2 + "'" + table.Rows[i]["ResourceCode"].ToString() + "'";
                    }
                    else
                    {
                        str2 = str2 + ",'" + table.Rows[i]["ResourceCode"].ToString() + "'";
                    }
                }
            }
            return str2;
        }

        public DataTable GetResourceByPlanCode(string planCode)
        {
            return publicDbOpClass.DataTableQuary("select EquipmentCode,r.ResourceName,EquipmentCount,Remark from Ent_Ept_PlanDetail as p inner join Res_Resource as r on p.EquipmentCode=r.ResourceCode where PlanCode = '" + planCode + "'");
        }

        public static EquipmentInfo GetSingleEquipmentInfo(Guid equipmentUniqueCode)
        {
            EquipmentInfo equipmentInfoFromDataRow = new EquipmentInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Equipments where EquipmentUniqueCode = '" + equipmentUniqueCode.ToString() + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    equipmentInfoFromDataRow = GetEquipmentInfoFromDataRow(table.Rows[0]);
                }
            }
            return equipmentInfoFromDataRow;
        }

        public EquipmentPlanInfo GetSingleEquipmentPlan(string PlanCode)
        {
            new EquipmentPlanInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Plan where PlanCode = '" + PlanCode + "'"))
            {
                return this.GetEquipmentPlanFromDataRow(table.Rows[0]);
            }
        }

        private string GetSynchronizeEquipmentPlanDetailString(string PlanCode, ArrayList al)
        {
            string str = "";
            EquipmentPlanDetailInfo info = new EquipmentPlanDetailInfo();
            for (int i = 0; i < al.Count; i++)
            {
                info = (EquipmentPlanDetailInfo) al[i];
                object obj2 = str + " insert into Ent_Ept_PlanDetail(PlanCode, EquipmentCode, EquipmentType, EquipmentCount,  Remark) ";
                str = string.Concat(new object[] { obj2, " values('", PlanCode, "','", info.EquipmentCode, "', 0, ", info.EquipmentCount, ",'", info.Remark, "') " });
            }
            return str;
        }

        public EquipmentPlanInfo GetwfPlanforGuid(string wfcode)
        {
            new EquipmentPlanInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Plan where wfguid = '" + wfcode + "'"))
            {
                return this.GetEquipmentPlanFromDataRow(table.Rows[0]);
            }
        }

        public bool moveResDetailToTemp(string PlanCode, string PageGuid, string UserCode)
        {
            string str = "insert into EPM_Res_TempResource (SessionCode,UserCode,ResourceCode,ResourceName,Specification,UnitID,UnitName,";
            string str2 = str + "PriceItemID,PriceItemName,Price,State,CategoryCode,CategoryName,ResourceStyle) ";
            return publicDbOpClass.NonQuerySqlString(((str2 + "select '" + PageGuid.ToString() + "','" + UserCode.ToString() + "',a.EquipmentCode,b.ResourceName,") + "b.Specification,b.UnitID,b.UnitName,b.PriceItemID,b.PriceItemName,b.Price,'1',b.CategoryCode,b.CategoryName,b.ResourceStyle") + " from Ent_Ept_PlanDetail a,EPM_V_Res_ResourceBasic b where a.EquipmentCode = b.ResourceCode and a.PlanCode = '" + PlanCode.ToString() + "'");
        }

        public static EquipmentCollection QueryEquipmentCollection(string deptId, string equipmentCode, string equipmentName, string equipmentType)
        {
            EquipmentCollection equipments = new EquipmentCollection();
            string sqlWhere = "";
            sqlWhere = "(1=1)";
            if (deptId.ToString() != "")
            {
                sqlWhere = sqlWhere + " and (LocateDeptID = " + deptId + ") ";
            }
            if (equipmentCode.Trim() != "")
            {
                sqlWhere = sqlWhere + " and (EquipmentManualCode like '%" + equipmentCode.Trim() + "%') ";
            }
            if (equipmentName.Trim() != "")
            {
                sqlWhere = sqlWhere + " and (EquipmentName like '%" + equipmentName.Trim() + "%') ";
            }
            if (equipmentType.Trim() != "")
            {
                sqlWhere = sqlWhere + " and (EquipmentType='" + equipmentType + "') ";
            }
            using (DataTable table = publicDbOpClass.GetPageData(sqlWhere, "Ent_Ept_Equipments", "NoteSequenceID desc"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    equipments.Add(GetEquipmentInfoFromDataRow(table.Rows[i]));
                }
            }
            return equipments;
        }

        public static int UpdEquipment(EquipmentInfo eptInfo)
        {
            object obj2 = ((((((" update Ent_Ept_Equipments set " + " EquipmentType='" + eptInfo.EquipmentType + "',") + " EquipmentManualCode='" + eptInfo.EquipmentManCode + "',") + " EquipmentName='" + eptInfo.EquipmentName + "',") + " Spec='" + eptInfo.Spec + "',") + " ThePrecision='" + eptInfo.ThePrecision + "',") + " Manufacturer='" + eptInfo.Manufacturer + "',") + " FactoryCode='" + eptInfo.FactoryCode + "',";
            object obj3 = string.Concat(new object[] { obj2, " FactoryDate='", eptInfo.FactoryDate, "'," });
            object obj4 = string.Concat(new object[] { obj3, " PurchaseDate='", eptInfo.PurchaseDate, "'," });
            object obj5 = string.Concat(new object[] { obj4, " CheckCycle=", eptInfo.CheckCycle, "," });
            object obj6 = string.Concat(new object[] { obj5, " ServiceYear=", eptInfo.ServiceYear, "," });
            object obj7 = string.Concat(new object[] { obj6, " OriginalPrice=", eptInfo.OriginalPrice, "," });
            object obj8 = string.Concat(new object[] { obj7, " State=", eptInfo.State, "," });
            object obj9 = string.Concat(new object[] { obj8, " LocateDeptID=", eptInfo.LocateDeptId, "," });
            object obj10 = string.Concat(new object[] { obj9, " depreciation =", eptInfo.depreciation, "," }) + " ContactDept ='" + eptInfo.ContactDept + "',";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj10, " Remark='", eptInfo.Remark, "' where EquipmentUniqueCode = '", eptInfo.EquipmentUniqueCode, "'" }));
        }

        public int UpdEquipmentPlan(EquipmentPlanInfo epi)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.ExecSqlString((((((((str + " update Ent_Ept_Plan ") + " set PlanCreatTime = '" + epi.PlanCreatTime.ToShortDateString() + "',") + " EnterDate = '" + epi.EnterDate.ToShortDateString() + "',") + " ExitDate = '" + epi.ExitDate.ToShortDateString() + "',") + " PlanMaker = '" + epi.PlanMaker + "',") + " Remark = '" + epi.Remark + "' ") + " where PlanCode = '" + epi.PlanCode + "' ") + " end ");
        }
    }
}

