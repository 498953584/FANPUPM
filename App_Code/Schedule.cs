using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class Schedule
{
    public IList<Action> ActionList = new List<Action>();
    private int millisecond;

    public Schedule(int millisecond)
    {
        this.millisecond = millisecond;
    }

    public void CreateChartImageHandlerPath(object state)
    {
        try
        {
            string str = ConfigHelper.Get("ChartImageHandler");
            string path = str.Substring(str.IndexOf("dir") + 4);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        catch
        {
        }
    }

    public void CreateSQLBackupPath(object state)
    {
        try
        {
            string path = ConfigHelper.Get("BackupSql");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        catch
        {
        }
    }

    public void Run(object state)
    {
        try
        {
            if (this.millisecond <= 0)
            {
                foreach (Action action in this.ActionList)
                {
                    try
                    {
                        action();
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                while (true)
                {
                    foreach (Action action2 in this.ActionList)
                    {
                        try
                        {
                            action2();
                        }
                        catch
                        {
                        }
                    }
                    Thread.Sleep(this.millisecond);
                }
            }
        }
        catch
        {
        }
    }
}

