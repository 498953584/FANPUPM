namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Data;
    using System.Web.UI;

    public class DutyUserAction
    {
        private string _messageString;
        private Page _objPage;

        public bool AddDepartment(string strDeptName, int iNumber, int iUpDeptCode, string strDeptCode, string strBMBM)
        {
            string str = "";
            int num = 0;
            int num2 = 0;
            strDeptName = PublicClass.CheckString(strDeptName);
            strDeptCode = PublicClass.CheckString(strDeptCode);
            string str2 = "01";
            DataTable deptmentDetail = this.GetDeptmentDetail(iUpDeptCode);
            if (deptmentDetail.Rows.Count > 0)
            {
                str = deptmentDetail.Rows[0]["v_bmqc"].ToString();
                num = Convert.ToInt32(deptmentDetail.Rows[0]["i_jb"].ToString());
                if (deptmentDetail.Rows[0]["i_sjdm"].ToString() != "0")
                {
                    strBMBM = deptmentDetail.Rows[0]["CorpCode"].ToString();
                    int num3 = Convert.ToInt32(deptmentDetail.Rows[0]["i_xjbm"].ToString()) + 1;
                    str2 = deptmentDetail.Rows[0]["v_bmbm"].ToString() + num3.ToString().PadLeft(2, '0');
                }
                else
                {
                    int num4 = Convert.ToInt32(deptmentDetail.Rows[0]["i_xjbm"].ToString()) + 1;
                    str2 = num4.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                this._messageString = "上级部门不存在，部门增加失败！";
                return false;
            }
            string str3 = str;
            str = str + @"\" + strDeptName;
            num2 = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_d_bm", "i_bmdm")) + 1;
            num++;
            string str5 = "";
            string str6 = str5 + " begin insert into pt_d_bm (i_bmdm,CorpCode,v_bmmc,v_bmqc,v_bmjx,i_xh,i_sjdm,i_xjbm,i_jb,c_sfyx,v_bmbm) values(" + num2.ToString() + ",'" + strBMBM + "','" + strDeptName + "','" + str + "','" + strDeptCode + "'," + iNumber.ToString() + "," + iUpDeptCode.ToString() + ",0," + num.ToString() + ",'y','" + str2 + "');";
            string str7 = str6 + " insert into  pt_RtxDeptInit (InitState,Dept,ParentDept)values('0','" + strDeptName + "','" + str3 + "')";
            if (publicDbOpClass.NonQuerySqlString((str7 + " update pt_d_bm set i_xjbm = (select count(1) from pt_d_bm where (i_sjdm=" + iUpDeptCode.ToString() + ")and(c_sfyx='y')) where i_bmdm = " + iUpDeptCode.ToString() + ";") + " end"))
            {
                return true;
            }
            this._messageString = "部门增加失败！";
            return false;
        }

        public DataTable AllDepNoDutyUser(string yhdm, string DutyCode)
        {
            return publicDbOpClass.DataTableQuary("SELECT    a.v_xm, a.v_yhdm, b.v_bmbm FROM PT_yhmc AS a INNER JOIN dbo.PT_d_bm AS b ON a.i_bmdm = b.i_bmdm   WHERE  a.v_xm like '%" + yhdm + "%' AND (a.c_sfyx = 'y') and  a.v_yhdm not in(select v_yhdm from pt_login where c_sfyx = 'y'and charindex('" + DutyCode + ",',DutyCodeS)>0 ) ORDER BY a.v_xm");
        }

        private DeptInfo DataTableConvertDeptInfo(DataRow dr)
        {
            return new DeptInfo { DeptCode = dr["i_bmdm"].ToString(), ParentCode = dr["i_sjdm"].ToString(), DeptName = dr["v_bmmc"].ToString(), DeptFullName = (dr["v_bmqc"] == DBNull.Value) ? "" : dr["v_bmqc"].ToString(), DeptSerial = (dr["i_xh"] == DBNull.Value) ? 0 : ((int) dr["i_xh"]), DeptShortName = (dr["v_bmjx"] == DBNull.Value) ? "" : dr["v_bmjx"].ToString(), DeptLevel = (dr["i_jb"] == DBNull.Value) ? "" : dr["i_jb"].ToString(), IsValid = (dr["c_sfyx"] == DBNull.Value) ? "y" : dr["c_sfyx"].ToString(), ChildCount = (dr["i_xjbm"] == DBNull.Value) ? 0 : ((int) dr["i_xjbm"]), DeptIdentify = (dr["V_BMMC"] == DBNull.Value) ? "" : dr["V_BMMC"].ToString() };
        }

        public bool DelDepartment(int iDeptCode)
        {
            int num = 0;
            string str = "";
            int num2 = 0;
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) as humanCount from pt_yhmc where (i_bmdm=" + iDeptCode.ToString() + ")and(c_sfyx='y')")) > 0)
            {
                this._messageString = "该部门有人员不能删除！";
                return false;
            }
            DataTable deptmentDetail = this.GetDeptmentDetail(iDeptCode);
            if (deptmentDetail.Rows.Count > 0)
            {
                num = Convert.ToInt32(deptmentDetail.Rows[0]["i_bmdm"].ToString());
                str = deptmentDetail.Rows[0]["v_bmmc"].ToString();
                string str3 = deptmentDetail.Rows[0]["v_bmqc"].ToString();
                num2 = Convert.ToInt32(deptmentDetail.Rows[0]["i_sjdm"].ToString());
                string str4 = "begin update pt_d_bm set i_sjdm = " + num2.ToString() + " where i_sjdm = " + num.ToString() + ";";
                string str5 = (str4 + " update pt_d_bm set i_xjbm = (select count(1)-1 from pt_d_bm where (i_sjdm=" + num2.ToString() + ")and(c_sfyx='y')) where i_bmdm=" + num2.ToString() + ";") + " update pt_d_bm set c_sfyx='n' where i_bmdm = " + num.ToString() + ";";
                if (publicDbOpClass.NonQuerySqlString((str5 + " insert into  pt_RtxDeptInit (InitState,Dept,ParentDept)values('2','" + str + "','" + str3 + "')") + " end"))
                {
                    return true;
                }
                this._messageString = "删除部门出错!";
                return false;
            }
            this._messageString = "该部门不存在!";
            return false;
        }

        public DataSet DepNoDutyUser(string bmdm, string DutyCode)
        {
            if (bmdm == "0")
            {
                bmdm = "1";
            }
            return publicDbOpClass.DataSetQuary("SELECT    a.v_xm, a.v_yhdm, b.v_bmbm FROM PT_yhmc AS a INNER JOIN dbo.PT_d_bm AS b ON a.i_bmdm = b.i_bmdm   WHERE  (b.i_bmdm = '" + bmdm + "') AND (a.c_sfyx = 'y') and  a.v_yhdm not in(select v_yhdm from pt_login where c_sfyx = 'y'and charindex('" + DutyCode + ",',DutyCodeS)>0 ) ORDER BY a.v_xm");
        }

        public int DutyUserDel(string DutyCode, string v_yhdm)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("  select top 1 DutyCodes from  pt_login   WHERE  v_yhdm= " + v_yhdm);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0][0].ToString();
            }
            int num = 0;
            if (str.Length > 1)
            {
                num = publicDbOpClass.ExecuteSQL("  update pt_login set DutyCodes='" + str.Replace(DutyCode + ',', "") + "'  WHERE  v_yhdm= " + v_yhdm);
            }
            return num;
        }

        public int DutyUserUpdate(string DutyCode, string v_yhdm)
        {
            return publicDbOpClass.ExecuteSQL("  update pt_login set DutyCodes=isnull(DutyCodes,'') +'" + DutyCode + ",'  WHERE  charindex(v_yhdm,'" + v_yhdm + "')>0  ");
        }

        public DataTable GetAllDepartment()
        {
            DataTable deptDTable = new DataTable();
            deptDTable.Columns.Add("i_bmdm");
            deptDTable.Columns.Add("v_bmmc");
            deptDTable.Columns.Add("i_jb");
            deptDTable.Columns.Add("i_sjdm");
            deptDTable.Columns.Add("i_xjbm");
            this.GetAllSubDepartment(deptDTable, 0);
            return deptDTable;
        }

        public DeptCollection GetAllDepartmentLists()
        {
            DeptCollection depts = new DeptCollection();
            string sqlString = "select * from pt_d_bm order by i_sjdm,i_xh";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                foreach (DataRow row in table.Rows)
                {
                    depts.Add(this.DataTableConvertDeptInfo(row));
                }
            }
            return depts;
        }

        public DataSet GetAllDepartmentSet()
        {
            string sqlString = "select * from pt_d_bm where c_sfyx='y' order by i_sjdm,i_xh";
            return publicDbOpClass.DataSetQuary(sqlString);
        }

        public DataSet GetAllGroupSet(string UserCode)
        {
            string str = " ";
            if (UserCode != "00000000")
            {
                str = " WHERE addByUser= '" + UserCode + "'  or addByUser='00000000' or Groupid=1 ";
            }
            else
            {
                str = " WHERE addByUser= '" + UserCode + "' or Groupid=1 ";
            }
            return publicDbOpClass.DataSetQuary("select * from PT_Group   " + str + " order by Groupid ");
        }

        public DataTable GetAllParentLevelDept(int iDeptCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_d_bm where (c_sfyx='y')and(i_sjdm = (select i_sjdm from pt_d_bm where i_bmdm = " + iDeptCode + "))");
        }

        public DataTable GetAllSameLevelDept(int iDeptCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_d_bm where (c_sfyx='y')and(i_sjdm = (select i_sjdm from pt_d_bm where i_bmdm = " + iDeptCode + ")) order by i_xh");
        }

        public void GetAllSubDepartment(DataTable deptDTable, int iDeptCode)
        {
            DataTable subDepartment = new DataTable();
            subDepartment = this.GetSubDepartment(iDeptCode);
            if (subDepartment.Rows.Count > 0)
            {
                for (int i = 0; i < subDepartment.Rows.Count; i++)
                {
                    DataRow row = deptDTable.NewRow();
                    row[0] = subDepartment.Rows[i]["i_bmdm"].ToString();
                    row[1] = subDepartment.Rows[i]["v_bmmc"].ToString();
                    row[2] = subDepartment.Rows[i]["i_jb"].ToString();
                    row[3] = subDepartment.Rows[i]["i_sjdm"].ToString();
                    row[4] = subDepartment.Rows[i]["i_xjbm"].ToString();
                    deptDTable.Rows.Add(row);
                    if (subDepartment.Rows[i]["i_xjbm"].ToString() != "0")
                    {
                        this.GetAllSubDepartment(deptDTable, Convert.ToInt32(subDepartment.Rows[i]["i_bmdm"].ToString()));
                    }
                }
            }
        }

        public DataTable GetDepSJBM()
        {
            string sqlString = "select a.*,b.i_sjdm,b.i_bmdm from PT_d_CorpCode a left join PT_d_bm b  on a.CorpCode=b.v_bmbm";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDeptmentDetail(int iDeptCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_d_bm where i_bmdm = " + iDeptCode.ToString());
        }

        public DataTable GetDeptName(string strWhere, bool b)
        {
            if (b)
            {
                return publicDbOpClass.DataTableQuary("select * from PT_d_bm where i_bmdm=" + strWhere);
            }
            string str2 = "select a.CorpName,b.CorpCode from PT_d_CorpCode a left join pt_d_bm b on a.CorpCode=b.CorpCode ";
            return publicDbOpClass.DataTableQuary(str2 + " where b.CorpCode in(select CorpCode from pt_d_bm where i_bmdm='" + strWhere + "')");
        }

        public DataTable GetDepTree()
        {
            string sqlString = "select * from pt_d_bm where (i_sjdm=1 or i_sjdm=0) and c_sfyx='y'";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDepTree1()
        {
            string sqlString = "select * from pt_d_bm where i_sjdm=1  and c_sfyx='y'";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDetDDL()
        {
            string sqlString = "select * from PT_d_CorpCode";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDutybyDept(string bmdm)
        {
            string sqlString = "  SELECT * FROM [PT_DUTY]";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDutyInDept()
        {
            string sqlString = "SELECT * FROM  PT_d_bm WHERE I_BMDM IN(  SELECT I_BMDM FROM [PT_DUTY])";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDutyInfobyid(string I_DUTYID)
        {
            return publicDbOpClass.DataTableQuary("select *, a.DutyNumber-a.HaveNumber as Vacancy  from ( SELECT     a.I_DUTYID, a.i_bmdm, b.V_BMMC,a.DutyCode, a.DutyNumber, (SELECT  count(*) from pt_login where c_sfyx = 'y'and charindex(convert(varchar(5),a.I_DUTYID)+',',DutyCodeS)>0 ) AS HaveNumber , a.Remark   FROM   dbo.PT_DUTY AS a LEFT OUTER JOIN    dbo.PT_d_bm AS b ON a.i_bmdm = b.i_bmdm  WHERE a.I_DUTYID = " + I_DUTYID + "  ) a");
        }

        public string GetDutyNamebyCodes(string DutyCodeS)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT * FROM [PT_DUTY] where charindex(convert(varchar(5),a.I_DUTYID)+','," + DutyCodeS + ")>0 ) ");
            string str2 = "";
            foreach (DataRow row in table.Rows)
            {
                str2 = str2 + row["DutyCode"].ToString() + ",";
            }
            return str2.TrimEnd(new char[] { ',' });
        }

        public DataTable GetDutyNameListbyCodes(string DutyCodeS)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM [PT_DUTY] where charindex(convert(varchar(5),a.I_DUTYID)+','," + DutyCodeS + ")>0 ) ");
        }

        public DataTable GetHumBYGroupID(string GroupID)
        {
            return publicDbOpClass.DataTableQuary("SELECT [v_yhdm],[v_xm],[i_bmdm] FROM [PT_yhmc] WHERE  v_yhdm IN  (select v_yhdm from PT_GroupHum  WHERE GroupID=" + GroupID + " ) order by v_xm ");
        }

        public DataTable GetHumListBYDutyID(string DutyID, string strwhere)
        {
            return publicDbOpClass.DataTableQuary("  SELECT   v_xm, V_BMMC,v_yhdm ,'" + DutyID + "' AS DutyCode  FROM v_UserInfoList   WHERE  v_yhdm in (select v_yhdm from pt_login where  charindex('" + DutyID + "',DutyCodeS)>0 )   " + strwhere + "   order by i_bmdm, v_xm ");
        }

        public DataTable Getjb(int strWhere)
        {
            return publicDbOpClass.DataTableQuary(" select i_jb from pt_d_bm where i_bmdm=" + strWhere);
        }

        public DataTable GetSubDepartment(int iDeptCode)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.CorpName from pt_d_bm a left join pt_d_CorpCode b on a.CorpCode=b.CorpCode where (i_sjdm=" + iDeptCode.ToString() + ")and(c_sfyx='y') order by i_xh");
        }

        public DataTable GetSubDepartment1(int iDeptCode, bool b)
        {
            string sqlString = "";
            if (b)
            {
                sqlString = "select * from pt_d_bm where (i_sjdm=" + iDeptCode.ToString() + ")and(c_sfyx='y') order by i_xh";
            }
            if (!b)
            {
                sqlString = "select * from pt_d_bm where (i_bmdm='" + iDeptCode.ToString() + "')and(c_sfyx='y') order by i_xh";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetTopDepartmentSet()
        {
            string sqlString = "select * from pt_d_bm where (i_sjdm = 0)and(c_sfyx='y')";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public bool MergeDepartment(int iDestDept, int iSourDept)
        {
            if (iDestDept != iSourDept)
            {
                this.GetDeptmentDetail(iSourDept).Rows[0]["v_bmmc"].ToString();
                this.GetDeptmentDetail(iDestDept).Rows[0]["v_bmmc"].ToString();
                string str2 = "begin update pt_yhmc set i_bmdm = " + iDestDept.ToString() + " where i_bmdm = " + iSourDept.ToString() + ";";
                string str3 = str2 + " update pt_d_bm set i_sjdm = " + iDestDept.ToString() + ",i_jb = (select i_jb+1 from pt_d_bm where i_bmdm=" + iDestDept.ToString() + ") where i_sjdm = " + iSourDept.ToString() + ";";
                string str4 = str3 + " update pt_d_bm set i_xjbm = (select count(1) from pt_d_bm where (i_sjdm=" + iDestDept.ToString() + ")and(c_sfyx='y')) where i_bmdm = " + iDestDept.ToString() + ";";
                if (publicDbOpClass.NonQuerySqlString(((str4 + " update pt_d_bm set i_xjbm = (select count(1)-1 from pt_d_bm where (i_sjdm=(select i_sjdm from pt_d_bm where i_bmdm= " + iSourDept.ToString() + "))and(c_sfyx='y')) where i_bmdm=(select i_sjdm from pt_d_bm where i_bmdm = " + iSourDept.ToString() + ");") + " update pt_d_bm set c_sfyx='n' where i_bmdm = " + iSourDept.ToString() + ";") + " end"))
                {
                    return true;
                }
                this._messageString = "部门合并操作失败！";
                return false;
            }
            this._messageString = "该操作无效！";
            return false;
        }

        public bool ModifyDepartment(int iDeptCode, string strDeptName, int iNumber, int iUpDeptCode, string strDeptCode)
        {
            string str = "";
            strDeptName = PublicClass.CheckString(strDeptName);
            strDeptCode = PublicClass.CheckString(strDeptCode);
            string str3 = "";
            DataTable deptmentDetail = this.GetDeptmentDetail(iUpDeptCode);
            if (deptmentDetail.Rows.Count > 0)
            {
                str = deptmentDetail.Rows[0]["v_bmqc"].ToString() + @"\" + strDeptName;
                DataTable table2 = this.GetDeptmentDetail(iDeptCode);
                if (table2.Rows.Count > 0)
                {
                    str3 = table2.Rows[0]["v_bmqc"].ToString();
                }
            }
            else
            {
                this._messageString = "取得部门新全称出错!";
                return false;
            }
            this.GetDeptmentDetail(iDeptCode).Rows[0]["v_bmmc"].ToString();
            string str4 = "begin update pt_d_bm set v_bmmc='" + strDeptName + "',v_bmqc='" + str + "',v_bmjx='" + strDeptCode + "',i_xh=" + iNumber.ToString() + " where i_bmdm = " + iDeptCode.ToString() + ";";
            if (publicDbOpClass.NonQuerySqlString((str4 + " insert into  pt_RtxDeptInit (InitState,Dept,ParentDept)values('1','" + strDeptName + "','" + str3 + "')") + " end"))
            {
                return true;
            }
            this._messageString = "部门更新失败!";
            return false;
        }

        public bool MoveDeptartment(int iDestDept, int iSourceDept)
        {
            string str = "";
            this.GetDeptmentDetail(iSourceDept).Rows[0]["v_bmmc"].ToString();
            this.GetDeptmentDetail(iDestDept).Rows[0]["v_bmmc"].ToString();
            str = "begin";
            object obj2 = str;
            string str2 = string.Concat(new object[] { obj2, " update pt_d_bm set i_xjbm =i_xjbm-1 where i_bmdm = (select i_sjdm from pt_d_bm where i_bmdm = ", iSourceDept, ");" }) + " update pt_d_bm set i_xjbm = i_xjbm + 1 where i_bmdm = " + iDestDept.ToString() + ";";
            if (publicDbOpClass.NonQuerySqlString((str2 + " update pt_d_bm set i_sjdm = " + iDestDept.ToString() + ",i_jb=(select i_jb+1 from pt_d_bm where i_bmdm=" + iDestDept.ToString() + ") where i_bmdm=" + iSourceDept.ToString() + ";") + " end"))
            {
                return true;
            }
            this._messageString = "更新部门出错，操作失败！";
            return false;
        }

        public string MessageString
        {
            get
            {
                return this._messageString;
            }
        }

        public Page ObjPage
        {
            get
            {
                return this._objPage;
            }
            set
            {
                this._objPage = value;
            }
        }
    }
}

