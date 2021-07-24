using System;

public class SummModel
{
    private Guid wkpdetailsid;
    private Guid wkpid;
    private string wkppercent;
    private DateTime wkprecorddate;
    private string wkpsmcontents;
    private int wkpsmid;

    public Guid WkpDetailsId
    {
        get
        {
            return this.wkpdetailsid;
        }
        set
        {
            this.wkpdetailsid = value;
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

    public string WkpPercent
    {
        get
        {
            return this.wkppercent;
        }
        set
        {
            this.wkppercent = value;
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

    public string WkpSmContents
    {
        get
        {
            return this.wkpsmcontents;
        }
        set
        {
            this.wkpsmcontents = value;
        }
    }

    public int WkpSmId
    {
        get
        {
            return this.wkpsmid;
        }
        set
        {
            this.wkpsmid = value;
        }
    }
}

