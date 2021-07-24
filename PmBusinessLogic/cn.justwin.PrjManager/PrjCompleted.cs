namespace cn.justwin.PrjManager
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PrjCompleted
    {
        public void Add(PrjCompleted model)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (model != null)
                {
                    PT_Prj_Completed_Detail detail = new PT_Prj_Completed_Detail {
                        ID = model.ID,
                        PrjGuid = new Guid?(model.PrjGuid),
                        PrepareStatus = model.PrepareStatus,
                        UncompletedTrans = model.UncompletedTrans,
                        Rectification = model.Rectification,
                        InputDate = model.InputDate
                    };
                    PT_Prj_Completed completed = (from pc in entities.PT_Prj_Completed
                        where pc.ID == model.PrjCompletedId
                        select pc).FirstOrDefault<PT_Prj_Completed>();
                    detail.PT_Prj_Completed = completed;
                    PT_yhmc _yhmc = (from m in entities.PT_yhmc
                        where m.userCode == model.InputUser
                        select m).FirstOrDefault<PT_yhmc>();
                    detail.PT_yhmc = _yhmc;
                    entities.AddToPT_Prj_Completed_Detail(detail);
                    entities.SaveChanges();
                }
            }
        }

        public static void AddCompleted(string prjguid)
        {
            Guid guid = new Guid(prjguid);
            using (pm2Entities entities = new pm2Entities())
            {
                int num = (from m in entities.PT_Prj_Completed_Detail
                    where m.PrjGuid == guid
                    select m).Count<PT_Prj_Completed_Detail>();
                List<string> list = (from n in entities.PT_Prj_Completed select n.ID).ToList<string>();
                if (num == 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        PrjCompleted model = new PrjCompleted {
                            ID = Guid.NewGuid().ToString(),
                            PrjGuid = guid,
                            PrjCompletedId = list[i],
                            InputDate = DateTime.Now
                        };
                        model.Add(model);
                    }
                }
            }
        }

        public static DataTable GetAllPrj(string prjCode, string prjName, string prjManager, string prjKindClass, string startDate, string endDate, string owner, string prjState, string flowState, string userCode, int pageNo, int pageSize, ref int refRowCount)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" AND PrjCode like  '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat(" AND PrjName like  '%{0}%' ", prjName);
            }
            if (!string.IsNullOrEmpty(prjManager))
            {
                builder.AppendFormat(" AND PrjMangerName like  '%{0}%' ", prjManager);
            }
            if (!string.IsNullOrEmpty(prjKindClass))
            {
                builder.AppendFormat(" AND  PrjKindClass='{0}'", prjKindClass);
            }
            if (!string.IsNullOrEmpty(owner))
            {
                builder.AppendFormat(" AND Owner like  '%{0}%' ", owner);
            }
            if (!string.IsNullOrEmpty(prjState))
            {
                builder.AppendFormat(" AND PrjState={0}", prjState);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND StartDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND StartDate <= '{0}' ", endDate);
            }
            if (!string.IsNullOrEmpty(flowState))
            {
                builder.AppendFormat(" AND CompletedFlowState ={0} ", flowState);
            }
            string str = "TypeCode,Primit,i_ChildNum,PrjGuid,PrjCode,PrjName,PrjState,InputDate,StartDate,EndDate,PrjCost,Duration,Owner,PrjMangerName,PrjStateName,PrjKindName,CompletedFlowState";
            SqlParameter parameter = new SqlParameter("@rowCount", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@isTender", DBNull.Value), new SqlParameter("@columns", str), new SqlParameter("@condition", builder.ToString()), new SqlParameter("@pageIndex", pageNo), new SqlParameter("@pageSize", pageSize), parameter };
            table = SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "uspGetProject", commandParameters);
            refRowCount = Convert.ToInt32(parameter.Value);
            return table;
        }

        public static string GetCompletedDate(string prjguid)
        {
            Guid guid = new Guid(prjguid);
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.PT_PrjInfo_ZTB_Detail
                    where m.PrjGuid == guid
                    select m.CompletedDate).FirstOrDefault<DateTime?>().ToString();
            }
        }

        public static DataTable GetCompletedManageList(string prjCode, string prjName, string prjManage, string prjState, string startDate, string endDate, string completedDate, string userCode, int pageNo, int pageSize, ref int refRowCount)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" AND PrjCode like  '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat(" AND PrjName like  '%{0}%' ", prjName);
            }
            if (!string.IsNullOrEmpty(prjManage))
            {
                builder.AppendFormat(" AND PrjMangerName like  '%{0}%' ", prjManage);
            }
            if (!string.IsNullOrEmpty(prjState))
            {
                builder.AppendFormat(" AND PrjState={0}", prjState);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND StartDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND StartDate <= '{0}' ", endDate);
            }
            if (!string.IsNullOrEmpty(completedDate))
            {
                builder.AppendFormat(" AND CompletedDate >='{0}' ", completedDate);
            }
            string str = "TypeCode,Primit,i_ChildNum,PrjGuid,PrjState,PrjCode,PrjName,InputDate,StartDate,EndDate,PrjMangerName,PrjStateName,CompletedFlowState,CompletedDate,CompletedNote";
            SqlParameter parameter = new SqlParameter("@rowCount", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@isTender", DBNull.Value), new SqlParameter("@columns", str), new SqlParameter("@condition", builder.ToString()), new SqlParameter("@pageIndex", pageNo), new SqlParameter("@pageSize", pageSize), parameter };
            table = SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "uspGetProject", commandParameters);
            refRowCount = Convert.ToInt32(parameter.Value);
            return table;
        }

        public static string GetCompletedNote(string prjguid)
        {
            Guid guid = new Guid(prjguid);
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.PT_PrjInfo_ZTB_Detail
                    where m.PrjGuid == guid
                    select m.CompletedNote).FirstOrDefault<string>();
            }
        }

        public static int GetFilesCount(string directoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM F_FileInfo WHERE ParentId='{0}' AND FileType='0' AND IsValid='0'", directoryId);
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()).ToString());
        }

        public static DataTable GetPrjComplete(string prjGuid)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjGuid", prjGuid) };
            return SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "uspPrjCompleted", commandParameters);
        }

        public static bool IsCompleted(string prjguid)
        {
            Guid guid = new Guid(prjguid);
            using (pm2Entities entities = new pm2Entities())
            {
                return ((from m in entities.PT_PrjInfo
                    where m.PrjGuid == guid
                    select m.PrjState).FirstOrDefault<int?>() == 10);
            }
        }

        public static bool IsSave(string prjGuid)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                Guid guid = new Guid(prjGuid);
                if ((from m in entities.PT_Prj_Completed_Detail
                    where m.PrjGuid == guid
                    select m).FirstOrDefault<PT_Prj_Completed_Detail>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public void Update(PrjCompleted model)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                PT_Prj_Completed_Detail detail = (from m in entities.PT_Prj_Completed_Detail
                    where (m.PrjGuid == model.PrjGuid) && (m.PT_Prj_Completed.ID == model.PrjCompletedId)
                    select m).FirstOrDefault<PT_Prj_Completed_Detail>();
                detail.InputDate = model.InputDate;
                detail.PrepareStatus = model.PrepareStatus;
                detail.Rectification = model.Rectification;
                detail.UncompletedTrans = model.UncompletedTrans;
                PT_yhmc _yhmc = (from n in entities.PT_yhmc
                    where n.v_yhdm == model.InputUser
                    select n).FirstOrDefault<PT_yhmc>();
                detail.PT_yhmc = _yhmc;
                entities.SaveChanges();
            }
        }

        public static void UpdateAnnexAddress(string prjId, string completeId, string directoryId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Guid guid = new Guid(prjId);
                PT_Prj_Completed_Detail detail = (from m in entities.PT_Prj_Completed_Detail
                    where (m.PrjGuid == guid) && (m.PT_Prj_Completed.ID == completeId)
                    select m).FirstOrDefault<PT_Prj_Completed_Detail>();
                if (detail != null)
                {
                    detail.AnnexAddress = directoryId;
                    entities.SaveChanges();
                }
            }
        }

        public static void UpdateCompleted(string prjGuid, string completedDate, string completedNote)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Guid prjguid = new Guid(prjGuid);
                (from m in entities.PT_PrjInfo
                    where m.PrjGuid == prjguid
                    select m).FirstOrDefault<PT_PrjInfo>().PrjState = 10;
                entities.SaveChanges();
                PT_PrjInfo_ZTB_Detail detail = (from n in entities.PT_PrjInfo_ZTB_Detail
                    where n.PrjGuid == prjguid
                    select n).FirstOrDefault<PT_PrjInfo_ZTB_Detail>();
                if (completedDate != "")
                {
                    detail.CompletedDate = new DateTime?(Convert.ToDateTime(completedDate));
                }
                detail.CompletedNote = completedNote;
                entities.SaveChanges();
            }
        }

        public string AnnexAddress { get; set; }

        public string ID { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        public string PrepareStatus { get; set; }

        public string PrjCompletedId { get; set; }

        public Guid PrjGuid { get; set; }

        public string Rectification { get; set; }

        public string UncompletedTrans { get; set; }
    }
}

