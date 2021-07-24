namespace cn.justwin.PrjManager
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CompletedAnnex
    {
        private void Add(string prjId, string prjCompletedId, List<string> annexAddress)
        {
            if (annexAddress != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Guid prjGuid = new Guid(prjId);
                    PT_Prj_Completed_Detail detail = (from m in entities.PT_Prj_Completed_Detail
                        where (m.PrjGuid == prjGuid) && (m.PT_Prj_Completed.ID == prjCompletedId)
                        select m).FirstOrDefault<PT_Prj_Completed_Detail>();
                    if (detail != null)
                    {
                        CompletedAnnex annex = new CompletedAnnex();
                        foreach (string str in annexAddress)
                        {
                            annex.AddSingle(detail, str, entities);
                        }
                        entities.SaveChanges();
                    }
                }
            }
        }

        private void AddSingle(PT_Prj_Completed_Detail detail, string annexAddress, pm2Entities context)
        {
            PT_Prj_Completed_Annex annex = new PT_Prj_Completed_Annex {
                ID = Guid.NewGuid().ToString(),
                PT_Prj_Completed_Detail = detail,
                AnnexAddress = annexAddress
            };
            context.AddToPT_Prj_Completed_Annex(annex);
        }

        public static bool ExistAnnexAddress(string prjId, string prjCompletedId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                string detailId = new CompletedAnnex().GetDetailId(prjId, prjCompletedId);
                if (!string.IsNullOrEmpty((from m in entities.PT_Prj_Completed_Annex
                    where m.PT_Prj_Completed_Detail.ID == detailId
                    select m.ID).FirstOrDefault<string>()))
                {
                    flag = true;
                }
            }
            return flag;
        }

        private List<string> GetAnnex(string detailId)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.PT_Prj_Completed_Annex
                    where m.PT_Prj_Completed_Detail.ID == detailId
                    select m.AnnexAddress).ToList<string>();
            }
        }

        public static List<string> GetAnnexAddress(string prjId, string prjCompletedId)
        {
            CompletedAnnex annex = new CompletedAnnex();
            string detailId = annex.GetDetailId(prjId, prjCompletedId);
            return annex.GetAnnex(detailId);
        }

        private string GetDetailId(string prjId, string prjCompletedId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Guid prjGuid = new Guid(prjId);
                return (from m in entities.PT_Prj_Completed_Detail
                    where (m.PrjGuid == prjGuid) && (m.PT_Prj_Completed.ID == prjCompletedId)
                    select m.ID).FirstOrDefault<string>();
            }
        }

        public static int GetFilesCount(string prjId, string prjCompletedId)
        {
            string detailId = new CompletedAnnex().GetDetailId(prjId, prjCompletedId);
            string inParameterSql = DBHelper.GetInParameterSql(new CompletedAnnex().GetAnnex(detailId).ToArray());
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM F_FileInfo WHERE ParentId IN ({0}) AND FileType='0' AND IsValid='0'", inParameterSql);
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()).ToString());
        }

        public static DataTable GetFilesInfo(List<string> directoryId)
        {
            DataTable table = new DataTable();
            if (directoryId == null)
            {
                return table;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < directoryId.Count; i++)
            {
                builder.AppendFormat(" SELECT Id,FileName,FileType,CreateTime FROM F_FileInfo WHERE FileType='0' AND ParentId ='{0}' AND IsValid=0 ", directoryId[i]);
                if (i < (directoryId.Count - 1))
                {
                    builder.Append(" UNION ALL ");
                }
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetFilesName(string prjId, string prjCompletedId)
        {
            string detailId = new CompletedAnnex().GetDetailId(prjId, prjCompletedId);
            string inParameterSql = DBHelper.GetInParameterSql(new CompletedAnnex().GetAnnex(detailId).ToArray());
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT FileName,FileNewName FROM F_FileInfo WHERE ParentId IN ({0}) AND FileType='0' AND IsValid='0'", inParameterSql);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static string GetFirstAnnexAddress(string prjId, string prjCompletedId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                string detailId = new CompletedAnnex().GetDetailId(prjId, prjCompletedId);
                return (from m in entities.PT_Prj_Completed_Annex
                    orderby m.AnnexAddress
                    where m.PT_Prj_Completed_Detail.ID == detailId
                    select m.AnnexAddress).FirstOrDefault<string>();
            }
        }

        public static void SetAnnex(string prjId, string prjCompletedId, List<string> annexAddress, string type)
        {
            if (string.IsNullOrEmpty(prjId))
            {
                throw new Exception("竣工管理准备项目设置目录参数 prjId 不能为空或空字符串");
            }
            if (string.IsNullOrEmpty(prjCompletedId))
            {
                throw new Exception("竣工管理准备项目设置目录参数 prjCompletedId 不能为空或空字符串");
            }
            if (string.IsNullOrEmpty(type))
            {
                throw new Exception("竣工管理准备项目设置目录参数 type 不能为空或空字符串");
            }
            CompletedAnnex annex = new CompletedAnnex();
            if (type == "add")
            {
                annex.Add(prjId, prjCompletedId, annexAddress);
            }
            else
            {
                if (type != "edit")
                {
                    throw new Exception("竣工管理准备项目设置目录参数 type 值只能是add或edit");
                }
                annex.Update(prjId, prjCompletedId, annexAddress);
            }
        }

        private void Update(string prjId, string prjCompletedId, List<string> annexAddress)
        {
            if (annexAddress != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    string detailId = new CompletedAnnex().GetDetailId(prjId, prjCompletedId);
                    if (detailId != "")
                    {
                        List<string> list = this.GetAnnex(detailId);
                        using (List<string>.Enumerator enumerator = list.GetEnumerator())
                        {
                            string address;
                            while (enumerator.MoveNext())
                            {
                                address = enumerator.Current;
                                if (!annexAddress.Contains(address))
                                {
                                    PT_Prj_Completed_Annex entity = (from m in entities.PT_Prj_Completed_Annex
                                        where (m.PT_Prj_Completed_Detail.ID == detailId) && (m.AnnexAddress == address)
                                        select m).FirstOrDefault<PT_Prj_Completed_Annex>();
                                    if (entity != null)
                                    {
                                        entities.DeleteObject(entity);
                                    }
                                }
                            }
                        }
                        PT_Prj_Completed_Detail detail = (from m in entities.PT_Prj_Completed_Detail
                            where m.ID == detailId
                            select m).FirstOrDefault<PT_Prj_Completed_Detail>();
                        if (detail != null)
                        {
                            CompletedAnnex annex2 = new CompletedAnnex();
                            foreach (string str in annexAddress)
                            {
                                if (!list.Contains(str))
                                {
                                    annex2.AddSingle(detail, str, entities);
                                }
                            }
                        }
                        entities.SaveChanges();
                    }
                }
            }
        }

        public string AnnexAddress { get; set; }

        public string DetailID { get; set; }

        public string Id { get; set; }
    }
}

