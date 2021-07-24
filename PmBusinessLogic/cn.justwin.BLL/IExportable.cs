namespace cn.justwin.BLL
{
    using System;
    using System.Data;

    public interface IExportable
    {
        void Export(DataTable table, string fileName, string brower);

        int[] PercentColumns { get; set; }
    }
}

