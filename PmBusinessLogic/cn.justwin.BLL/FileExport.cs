namespace cn.justwin.BLL
{
    using System;
    using System.Data;

    public class FileExport
    {
        private IExportable exporter;
        private string fileName;
        private DataTable table;

        public FileExport(IExportable exporter, DataTable table, string fileName)
        {
            this.exporter = exporter;
            this.table = table;
            this.fileName = fileName;
        }

        public void Export(string browser)
        {
            this.exporter.Export(this.table, this.fileName, browser);
        }

        public IExportable Exporter
        {
            get
            {
                return this.exporter;
            }
            set
            {
                this.exporter = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public DataTable Table
        {
            get
            {
                return this.table;
            }
            set
            {
                this.table = value;
            }
        }
    }
}

