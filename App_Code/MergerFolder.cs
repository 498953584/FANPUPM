using cn.justwin.Web;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[Serializable]
public class MergerFolder
{
    [NonSerialized]
    private string _path;

    public MergerFolder(string className, bool readOnly)
    {
        this.ClassName = className;
        this.readOnly = readOnly;
    }

    public string ClassName
    {
        set
        {
            this._path = ConfigHelper.Get(value);
        }
    }

    [DataMember]
    public string Path
    {
        get
        {
            return this._path;
        }
        set
        {
            this._path = value;
        }
    }

    [DataMember]
    public bool readOnly { get; set; }
}

