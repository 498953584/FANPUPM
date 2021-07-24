namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class ConferenceManage
    {
        public static object AddApplyInfo(Hashtable applyInfo)
        {
            string select = "";
            select = " SELECT SCOPE_IDENTITY() AS [IDENTITY] ";
            publicDbOpClass class2 = new publicDbOpClass();
            return class2.Insert("[OA_MeetingRoom_Apply]", applyInfo, select);
        }

        public static bool AddBoardroom(Hashtable htInfo)
        {
            return publicDbOpClass.Insert("[OA_MeetingRoom_Info]", htInfo);
        }

        public static bool AddConferenceTopic(Hashtable htInfo)
        {
            return publicDbOpClass.Insert("[OA_Meeting_Topic]", htInfo);
        }

        public static bool AddFromTemplate(int templateId, string userCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@templateid", templateId), new SqlParameter("@userCode", userCode) };
            bool flag = true;
            try
            {
                publicDbOpClass.ExecuteProcedure("p_oa_AddFromMeetingTemplate", commandParameters);
            }
            catch (SqlException)
            {
                flag = false;
            }
            return flag;
        }

        public static bool AddMeetingInfo(Hashtable htInfo)
        {
            return publicDbOpClass.Insert("[OA_Meeting_Info]", htInfo);
        }

        public static bool AddMeetingTemplate(Hashtable htTemplate)
        {
            return publicDbOpClass.Insert("[OA_Meeting_Templet]", htTemplate);
        }

        public static bool AddSubsection(Hashtable htInfo)
        {
            return publicDbOpClass.Insert("[OA_Meeting_AttendMan]", htInfo);
        }

        public static bool AddTemplateFraeInfo(string tableName, Hashtable htFraeInfo)
        {
            return publicDbOpClass.Insert(tableName, htFraeInfo);
        }

        public static bool ApplyEquipment(int applyRecordId, int[] equipmentId)
        {
            string sqlString = " Delete from OA_MeetingRoom_ApplyDetail where ApplyRecordID = " + applyRecordId.ToString();
            for (int i = 0; i < equipmentId.Length; i++)
            {
                object obj2 = sqlString + " Insert into OA_MeetingRoom_ApplyDetail(ApplyRecordID,EquipmentIRecordID) ";
                sqlString = string.Concat(new object[] { obj2, "Values(", applyRecordId.ToString(), ",", equipmentId[i], ") " });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool CreateStateTable(HtmlTable tbState, int meetingRoomId, int year, int month)
        {
            int num = getDays(year, month);
            string text = "";
            tbState.Rows.Clear();
            for (int i = 0; i < num; i++)
            {
                HtmlTableRow row = new HtmlTableRow();
                for (int j = 0; j < 0x2d; j++)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    if (j == 0)
                    {
                        cell.Attributes["width"] = "60px";
                        cell.Attributes["height"] = "15px";
                        text = "<font size=\"-1\">" + ((i + 1)).ToString() + "日</font>";
                        cell.Attributes["align"] = "center";
                        cell.Controls.Add(new LiteralControl(text));
                    }
                    else
                    {
                        cell.Attributes["width"] = "15px";
                        cell.Attributes["height"] = "15px";
                        cell.Attributes["style"] = "cursor:hand";
                        foreach (DataRow row2 in QueryMeetingRoomApply(meetingRoomId, year, month).Rows)
                        {
                            if (((int) row2["row"]) == (i + 1))
                            {
                                int num4 = ((((int) row2["BeginHour"]) - 8) * 4) + (((int) row2["BeginMinute"]) / 15);
                                int num5 = ((((int) row2["EndHour"]) - 8) * 4) + (((int) row2["EndMinute"]) / 15);
                                if ((j > num4) && (j <= num5))
                                {
                                    cell.Attributes["bgcolor"] = "red";
                                }
                            }
                        }
                    }
                    row.Cells.Add(cell);
                }
                tbState.Rows.Add(row);
            }
            return true;
        }

        public static bool DelApplyInfo(int recordId)
        {
            return publicDbOpClass.NonQuerySqlString((" delete from OA_MeetingRoom_ApplyDetail where ApplyRecordID = " + recordId.ToString()) + " delete from OA_MeetingRoom_Apply where RecordID = " + recordId.ToString());
        }

        public static bool DelBoardroom(int recordId)
        {
            string str = " begin ";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, " Delete from OA_MeetingRoom_ApplyDetail where ApplyRecordID in(select RecordID from OA_MeetingRoom_Apply where MeetingRoomID = ", recordId, " )" });
            return publicDbOpClass.NonQuerySqlString(((string.Concat(new object[] { obj3, " Delete from OA_MeetingRoom_Apply where MeetingRoomID = ", recordId, " " }) + " Delete from OA_MeetingRoom_Equipment where MeetingRoomID = " + recordId.ToString()) + " Delete from OA_MeetingRoom_Info where RecordID = " + recordId.ToString()) + " end ");
        }

        public static bool DelConferenceTopic(int recordId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete from OA_Meeting_Topic where RecordID = " + recordId.ToString());
        }

        public static bool DelEquipment(int recordId)
        {
            return publicDbOpClass.NonQuerySqlString(" Delete from OA_MeetingRoom_Equipment where RecordID = " + recordId.ToString());
        }

        public static bool DelMeetingInfo(int recordId)
        {
            string str = "";
            return publicDbOpClass.NonQuerySqlString((((((str + " delete from OA_Meeting_AttendMan where MeetingInfoID = " + recordId.ToString()) + " delete from OA_Meeting_Project where MeetingInfoID = " + recordId.ToString()) + " delete from OA_Meeting_Waiter where MeetingInfoID = " + recordId.ToString()) + " delete from OA_Meeting_Equipment where MeetingInfoID = " + recordId.ToString()) + " delete from OA_Meeting_Topic where MeetingInfoID = " + recordId.ToString()) + " delete from OA_Meeting_Info where RecordId = " + recordId.ToString());
        }

        public static bool DelMeetingTemplate(int templateId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete from OA_Meeting_Templet where RecordID = " + templateId.ToString());
        }

        public static bool DelSubsection(int recordId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete from OA_Meeting_AttendMan where RecordID = " + recordId.ToString());
        }

        public static bool DelTemplateFraeInfo(string tableName, int recordId)
        {
            return publicDbOpClass.NonQuerySqlString(" delete from " + tableName + " where RecordID = " + recordId.ToString());
        }

        private static int getDays(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 0x1f;

                case 2:
                    if (!DateTime.IsLeapYear(year))
                    {
                        return 0x1c;
                    }
                    return 0x1d;

                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
            }
            return 0;
        }

        public static ConferenceInfoCollection GetEquipment(int meetingroomId)
        {
            ConferenceInfoCollection infos = new ConferenceInfoCollection();
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + "select * from OA_MeetingRoom_Equipment where MeetingRoomID=" + meetingroomId.ToString()))
            {
                if (table.Rows.Count <= 0)
                {
                    return infos;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    infos.Add(GetEquipmentFromDataRow(table.Rows[i]));
                }
            }
            return infos;
        }

        public static ConferenceInfo GetEquipmentFromDataRow(DataRow dr)
        {
            return new ConferenceInfo { RecordID = (int) dr["RecordID"], MeetingRoomID = (int) dr["MeetingRoomID"], EquipmentName = dr["EquipmentName"].ToString(), Model = dr["Model"].ToString(), Number = (int) dr["Number"], Content = dr["Content"].ToString(), IsValid = dr["IsValid"].ToString() };
        }

        public static bool PigeOnHole(string userCode, string corpCode, int meetingInfoId)
        {
            string str2 = " Insert Into OA_eFile_Info(corpCode,FileTitle,submitMan,SubmitDate,Remark,FilePath,OriginalName,UserCode,RecordDate) ";
            return publicDbOpClass.NonQuerySqlString((str2 + " select '" + corpCode + "',SummaryName,'" + userCode + "',getDate(),Content,FilePath,OriginalName,UserCode,RecordDate from OA_Meeting_Info where RecordID = " + meetingInfoId.ToString()) + " Update OA_Meeting_Info set PigeonholeState = '1' where RecordID = " + meetingInfoId.ToString());
        }

        public static DataTable QueryApplyEquipment(int applyRecordId)
        {
            return publicDbOpClass.DataTableQuary(" select * from OA_MeetingRoom_ApplyDetail where ApplyRecordID = " + applyRecordId.ToString());
        }

        public static int QueryApplyEquipmentList(int equipmentId)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar(" select count(*) as value from OA_MeetingRoom_ApplyDetail where EquipmentIRecordID = " + equipmentId.ToString()));
        }

        public static DataTable QueryApplyInfo(int recordId)
        {
            return publicDbOpClass.DataTableQuary("select *,(select MeetingRoom from OA_MeetingRoom_Info where RecordID = MeetingRoomID) as MeetingRoom,(select ManagerCode from OA_MeetingRoom_Info where RecordID = MeetingRoomID) as ManagerCode,(select v_xm from pt_yhmc where v_yhdm = UserCode) as UserName from OA_MeetingRoom_Apply where RecordID = " + recordId.ToString());
        }

        public static DataTable QueryConferenceTopic(int recordId)
        {
            return publicDbOpClass.DataTableQuary(" select * from OA_Meeting_Topic where RecordID = " + recordId.ToString());
        }

        public static DataTable QueryCorpCode()
        {
            string sqlString = " select corpcode,corpname from pt_d_corpcode ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryEquipmentByID(int meetingroomId)
        {
            return publicDbOpClass.DataTableQuary(" select from  OA_MeetingRoom_Equipment where MeetingRoomID = " + meetingroomId.ToString());
        }

        public static DataTable QueryMeetingInfo(int recordId)
        {
            return publicDbOpClass.DataTableQuary(" select * from OA_Meeting_Info where RecordId = " + recordId.ToString());
        }

        public static DataTable QueryMeetingRoomApply(int meetingRoomId, int year, int month)
        {
            return publicDbOpClass.DataTableQuary("select *,day(UserDate) as row from OA_MeetingRoom_Apply where state = 2 and MeetingRoomID = " + meetingRoomId.ToString() + " and year(UserDate) = " + year.ToString() + " and month(UserDate) = " + month.ToString());
        }

        public static object QueryMeetingroomID()
        {
            string sqlString = " select isnull(max(RecordID),0) from OA_MeetingRoom_Info ";
            return publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static DataTable QueryMeetingTemplate(int templateId)
        {
            return publicDbOpClass.DataTableQuary(" select * from OA_Meeting_Templet where RecordID = " + templateId.ToString());
        }

        public static DataTable QueryOneBoardroom(int recordId)
        {
            return publicDbOpClass.DataTableQuary((" Select *,(select corpName from pt_d_corpcode where corpcode = (select CorpCode from OA_MeetingRoom_Info where RecordID = " + recordId.ToString() + ")) as corpName, ") + " (select v_xm from pt_yhmc where v_yhdm = ManagerCode) as ManagerName from OA_MeetingRoom_Info where RecordID = " + recordId.ToString());
        }

        public static DataTable QuerySubsection(int recordId)
        {
            return publicDbOpClass.DataTableQuary(" select * from OA_Meeting_AttendMan where RecordID = " + recordId.ToString());
        }

        public static DataTable QuerySubsectionList(int meetingInfoId)
        {
            return publicDbOpClass.DataTableQuary(" select * from OA_Meeting_AttendMan where MeetingInfoID = " + meetingInfoId.ToString());
        }

        public static DataTable QueryTemplateFraeInfo(string tableName, int recordId)
        {
            return publicDbOpClass.DataTableQuary(" select * from " + tableName + " where RecordID = " + recordId.ToString());
        }

        public static bool SetApplyState(int state, int applyRecordId)
        {
            return publicDbOpClass.NonQuerySqlString(" update OA_MeetingRoom_Apply set State = " + state.ToString() + " where RecordID = " + applyRecordId.ToString());
        }

        public static bool SetLaunchState(int meetingInfoId, string state)
        {
            return publicDbOpClass.NonQuerySqlString(" update OA_Meeting_Info set State = '" + state + "' where RecordID = " + meetingInfoId.ToString());
        }

        public static bool UpdApplyInfo(Hashtable applyInfo, string where)
        {
            return publicDbOpClass.Update("[OA_MeetingRoom_Apply]", applyInfo, where);
        }

        public static int UpdateEquipment(ConferenceInfoCollection tc, int meetingroomId)
        {
            string sqlString = " ";
            if (tc.Count > 0)
            {
                for (int i = 0; i < tc.Count; i++)
                {
                    int recordID = tc[i].RecordID;
                    if (QueryApplyEquipmentList(recordID) > 0)
                    {
                        sqlString = ((((((sqlString + " update OA_MeetingRoom_Equipment Set MeetingRoomID = " + tc[i].MeetingRoomID) + ", EquipmentName = '" + tc[i].EquipmentName + "' ") + ", Model = '" + tc[i].EquipmentName + "' ") + ", Number = " + tc[i].Number) + ", Content = '" + tc[i].Content + "' ") + ", IsValid = '" + tc[i].IsValid + "' ") + " where RecordID = " + recordID.ToString();
                    }
                    else
                    {
                        object obj2 = (sqlString + " delete from OA_MeetingRoom_Equipment where RecordID = " + recordID.ToString()) + " insert into OA_MeetingRoom_Equipment(MeetingRoomID,EquipmentName,Model,Number,Content,IsValid) " + " values( ";
                        object obj3 = (string.Concat(new object[] { obj2, " ", tc[i].MeetingRoomID, ", " }) + " '" + tc[i].EquipmentName + "', ") + " '" + tc[i].Model + "', ";
                        sqlString = ((string.Concat(new object[] { obj3, " ", tc[i].Number, ", " }) + " '" + tc[i].Content + "', ") + " '" + tc[i].IsValid + "' ") + " ) ";
                    }
                }
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public static bool UpdBoardroom(Hashtable htInfo, string where)
        {
            return publicDbOpClass.Update("[OA_MeetingRoom_Info]", htInfo, where);
        }

        public static bool UpdConferenceTopic(Hashtable htInfo, string where)
        {
            return publicDbOpClass.Update("[OA_Meeting_Topic]", htInfo, where);
        }

        public static bool UpdMeetingInfo(Hashtable htInfo, string where)
        {
            return publicDbOpClass.Update("[OA_Meeting_Info]", htInfo, where);
        }

        public static bool UpdMeetingTemplate(Hashtable htTemplate, string where)
        {
            return publicDbOpClass.Update("[OA_Meeting_Templet]", htTemplate, where);
        }

        public static bool UpdSubsection(Hashtable htInfo, string where)
        {
            return publicDbOpClass.Update("[OA_Meeting_AttendMan]", htInfo, where);
        }

        public static bool UpdTemplateFraeInfo(string tableName, Hashtable htFraeInfo, string where)
        {
            return publicDbOpClass.Update(tableName, htFraeInfo, where);
        }
    }
}

