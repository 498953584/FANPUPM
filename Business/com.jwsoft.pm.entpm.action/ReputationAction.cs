namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ReputationAction
    {
        public int addReputation(Reputation rep)
        {
            object obj2 = (" " + "insert into  pt_reputation (") + " requtBh,requtBgbh, dwId, productMc, makeNl,serviceSh,reputSh,productJc,procedtSy,materialFs, " + " qualityFS , projectFS , tryFS , prjDeptFS , appraise ,appraJg ,manager, appreDate ,tablePeo ) ";
            object obj3 = string.Concat(new object[] { obj2, " values ('", rep.RequtBh, "' , '", rep.RequtBgbh, "',", rep.DwId, " , '", rep.ProductMc, "' , '", rep.MakeNl, "','", rep.ServiceSh, "' ,'" });
            object obj4 = string.Concat(new object[] { obj3, rep.ReputSh, "' ,'", rep.ProductJc, "' ,'", rep.ProcedtSy, "' , ", rep.MaterialFs, " , ", rep.QualityFS, " , " });
            string str2 = string.Concat(new object[] { obj4, rep.ProjectFS, " , ", rep.TryFS, " , ", rep.PrjDeptFS, " , '", rep.Appraise, "' , ", rep.AppraJg, " , '" });
            return publicDbOpClass.ExecSqlString(str2 + rep.Manager + "' , '" + rep.AppreDate + "' , '" + rep.TablePeo + "')");
        }

        private Reputation GetReputationFromDataRow(DataRow dr)
        {
            return new Reputation { 
                RequtBh = dr["requtBh"].ToString(), RequtBgbh = dr["requtBgbh"].ToString(), CorpName = dr["CorpName"].ToString(), DwId = Convert.ToInt32(dr["CorpID"].ToString()), Corporation = dr["Corporation"].ToString(), MakeNl = dr["makeNl"].ToString(), ServiceSh = dr["serviceSh"].ToString(), ReputSh = dr["reputSh"].ToString(), ProductJc = dr["productJc"].ToString(), ProcedtSy = dr["procedtSy"].ToString(), MaterialFs = Convert.ToDecimal(dr["materialFs"].ToString()), QualityFS = Convert.ToDecimal(dr["qualityFS"].ToString()), ProjectFS = Convert.ToDecimal(dr["projectFS"].ToString()), TryFS = Convert.ToDecimal(dr["tryFS"].ToString()), PrjDeptFS = Convert.ToDecimal(dr["prjDeptFS"].ToString()), Appraise = dr["appraise"].ToString(), 
                AppraJg = Convert.ToInt32(dr["appraJg"].ToString()), Manager = dr["manager"].ToString(), AppreDate = dr["appreDate"].ToString(), TablePeo = dr["tablePeo"].ToString()
             };
        }

        public Reputation selReputation(int reputId)
        {
            string str = "select * from pt_reputation as a ";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + " left join XPM_Basic_ContactCorp as b on a.dwId = b.CorpID where reputID = " + reputId))
            {
                return this.GetReputationFromDataRow(table.Rows[0]);
            }
        }

        public int UpReputation(Reputation rep, int reputID)
        {
            object obj2 = ((" " + " update pt_reputation ") + " set requtBh = '" + rep.RequtBh + "',") + " requtBgbh = '" + rep.RequtBgbh + "',";
            object obj3 = (((((string.Concat(new object[] { obj2, " dwId = ", rep.DwId, "," }) + " productMc = '" + rep.ProductMc + "',") + " makeNl = '" + rep.MakeNl + "',") + " serviceSh = '" + rep.ServiceSh + "',") + " reputSh ='" + rep.ReputSh + "',") + " productJc = '" + rep.ProductJc + "',") + " procedtSy = '" + rep.ProcedtSy + "',";
            object obj4 = string.Concat(new object[] { obj3, " materialFs =", rep.MaterialFs, "," });
            object obj5 = string.Concat(new object[] { obj4, " qualityFS = ", rep.QualityFS, " ," });
            object obj6 = string.Concat(new object[] { obj5, " projectFS = ", rep.ProjectFS, "," });
            object obj7 = string.Concat(new object[] { obj6, " tryFS=", rep.TryFS, "," });
            object obj8 = string.Concat(new object[] { obj7, " prjDeptFS = ", rep.PrjDeptFS, "," }) + " appraise = '" + rep.Appraise + "',";
            return publicDbOpClass.ExecSqlString((((string.Concat(new object[] { obj8, " appraJg = ", rep.AppraJg, "," }) + " manager = '" + rep.Manager + "',") + " appreDate = '" + rep.AppreDate + "',") + " tablePeo = '" + rep.TablePeo + "'") + " where reputID = " + reputID);
        }
    }
}

