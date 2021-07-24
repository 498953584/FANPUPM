using System;

public class PlanDetail
{
    private string wkpchief;
    private string wkpcontents;
    private Guid wkpdetailsId;
    private DateTime wkpendtime;
    private Guid wkpid;
    private string wkppersons;
    private DateTime wkpsarttime;
    private string wkpstandard;

    public string WkpChief
    {
        get
        {
            return this.wkpchief;
        }
        set
        {
            this.wkpchief = value;
        }
    }

    public string WkpContents
    {
        get
        {
            return this.wkpcontents;
        }
        set
        {
            this.wkpcontents = value;
        }
    }

    public Guid WkpDetailsId
    {
        get
        {
            return this.wkpdetailsId;
        }
        set
        {
            this.wkpdetailsId = value;
        }
    }

    public DateTime WkpEndTime
    {
        get
        {
            return this.wkpendtime;
        }
        set
        {
            this.wkpendtime = value;
        }
    }

    public Guid WkpId
    {
        get
        {
            return this.wkpid;
        }
        set
        {
            this.wkpid = value;
        }
    }

    public string WkpPersons
    {
        get
        {
            return this.wkppersons;
        }
        set
        {
            this.wkppersons = value;
        }
    }

    public string WkpStandard
    {
        get
        {
            return this.wkpstandard;
        }
        set
        {
            this.wkpstandard = value;
        }
    }

    public DateTime WkpStartTime
    {
        get
        {
            return this.wkpsarttime;
        }
        set
        {
            this.wkpsarttime = value;
        }
    }
}

