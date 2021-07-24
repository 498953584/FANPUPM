using System;

public class MainPlan
{
    private DateTime inputTime;
    private string wkpcheckeruser;
    private string wkpdeptid;
    private Guid wkpId;
    private int wkpisreport;
    private int wkpistrue;
    private DateTime wkprecorddate;
    private string wkpregisteruser;
    private int wkpreporttype;
    private string wkpreportuser;
    private string wkpusercode;

    public DateTime InputTime
    {
        get
        {
            return this.inputTime;
        }
        set
        {
            this.inputTime = value;
        }
    }

    public string WkpCheckerUser
    {
        get
        {
            return this.wkpcheckeruser;
        }
        set
        {
            this.wkpcheckeruser = value;
        }
    }

    public string WkpDeptId
    {
        get
        {
            return this.wkpdeptid;
        }
        set
        {
            this.wkpdeptid = value;
        }
    }

    public Guid WkpId
    {
        get
        {
            return this.wkpId;
        }
        set
        {
            this.wkpId = value;
        }
    }

    public int WkpIsReport
    {
        get
        {
            return this.wkpisreport;
        }
        set
        {
            this.wkpisreport = value;
        }
    }

    public int WkpIstrue
    {
        get
        {
            return this.wkpistrue;
        }
        set
        {
            this.wkpistrue = value;
        }
    }

    public DateTime WkpRecordDate
    {
        get
        {
            return this.wkprecorddate;
        }
        set
        {
            this.wkprecorddate = value;
        }
    }

    public string WkpRegisterUser
    {
        get
        {
            return this.wkpregisteruser;
        }
        set
        {
            this.wkpregisteruser = value;
        }
    }

    public int WkpReportType
    {
        get
        {
            return this.wkpreporttype;
        }
        set
        {
            this.wkpreporttype = value;
        }
    }

    public string WkpReportUser
    {
        get
        {
            return this.wkpreportuser;
        }
        set
        {
            this.wkpreportuser = value;
        }
    }

    public string WkpUserCode
    {
        get
        {
            return this.wkpusercode;
        }
        set
        {
            this.wkpusercode = value;
        }
    }
}

