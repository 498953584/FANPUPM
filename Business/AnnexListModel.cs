using System;

[Serializable]
public class AnnexListModel
{
    private DateTime _adddate;
    private Guid _annexcode;
    private string _annexname;
    private int _annextype;
    private string _filecode;
    private string _filepath;
    private int _filesize;
    private string _humancode;
    private int _moduleid;
    private string _originalname;
    private string _projectuniquecode;
    private string _recordcode;
    private string _remark;
    private int _state;

    public DateTime AddDate
    {
        get
        {
            return this._adddate;
        }
        set
        {
            this._adddate = value;
        }
    }

    public Guid AnnexCode
    {
        get
        {
            return this._annexcode;
        }
        set
        {
            this._annexcode = value;
        }
    }

    public string AnnexName
    {
        get
        {
            return this._annexname;
        }
        set
        {
            this._annexname = value;
        }
    }

    public int AnnexType
    {
        get
        {
            return this._annextype;
        }
        set
        {
            this._annextype = value;
        }
    }

    public string FileCode
    {
        get
        {
            return this._filecode;
        }
        set
        {
            this._filecode = value;
        }
    }

    public string FilePath
    {
        get
        {
            return this._filepath;
        }
        set
        {
            this._filepath = value;
        }
    }

    public int FileSize
    {
        get
        {
            return this._filesize;
        }
        set
        {
            this._filesize = value;
        }
    }

    public string HumanCode
    {
        get
        {
            return this._humancode;
        }
        set
        {
            this._humancode = value;
        }
    }

    public int ModuleID
    {
        get
        {
            return this._moduleid;
        }
        set
        {
            this._moduleid = value;
        }
    }

    public string OriginalName
    {
        get
        {
            return this._originalname;
        }
        set
        {
            this._originalname = value;
        }
    }

    public string ProjectUniqueCode
    {
        get
        {
            return this._projectuniquecode;
        }
        set
        {
            this._projectuniquecode = value;
        }
    }

    public string RecordCode
    {
        get
        {
            return this._recordcode;
        }
        set
        {
            this._recordcode = value;
        }
    }

    public string Remark
    {
        get
        {
            return this._remark;
        }
        set
        {
            this._remark = value;
        }
    }

    public int State
    {
        get
        {
            return this._state;
        }
        set
        {
            this._state = value;
        }
    }
}

