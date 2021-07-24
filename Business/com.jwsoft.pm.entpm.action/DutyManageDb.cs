namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class DutyManageDb
    {
        private string _messageString;

        public bool AddDuty(int iDeptID, string strDutyName)
        {
            bool flag = false;
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_duty", "i_dutyID")) + 1;
            if (publicDbOpClass.NonQuerySqlString("insert into pt_Duty(i_DutyID,i_DeptID,v_DutyName) values(" + num.ToString() + "," + iDeptID.ToString() + ",'" + strDutyName + "')"))
            {
                return true;
            }
            flag = false;
            this._messageString = "增加失败！";
            return flag;
        }

        public bool DelDuty(int iDutyID)
        {
            bool flag = false;
            if (publicDbOpClass.NonQuerySqlString("delete from pt_Duty where i_DutyID =" + iDutyID.ToString()))
            {
                return true;
            }
            flag = false;
            this._messageString = "删除失败！";
            return flag;
        }

        public DataTable GetDeptDuty(int iDeptID)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_Duty where i_DeptID = " + iDeptID.ToString());
        }

        public bool ModifyDuty(int iDutyID, string strDutyName)
        {
            bool flag = false;
            if (publicDbOpClass.NonQuerySqlString("update pt_Duty set v_DutyName='" + strDutyName + "' where i_DutyID = " + iDutyID.ToString()))
            {
                return true;
            }
            flag = false;
            this._messageString = "更新失败！";
            return flag;
        }

        public string MessageString
        {
            get
            {
                return this._messageString;
            }
            set
            {
                this._messageString = value;
            }
        }
    }
}

