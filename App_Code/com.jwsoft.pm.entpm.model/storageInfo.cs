namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class storageInfo
    {
        private int id;
        private string inStorageId;
        private string itemName;
        private string outStorageName;
        private string outStroageid;
        private string pmid;
        private string supplier;
        private string validateDepartment;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string InStorageId
        {
            get
            {
                return this.inStorageId;
            }
            set
            {
                this.inStorageId = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }

        public string OutStorageName
        {
            get
            {
                return this.outStorageName;
            }
            set
            {
                this.outStorageName = value;
            }
        }

        public string OutStroageid
        {
            get
            {
                return this.outStroageid;
            }
            set
            {
                this.outStroageid = value;
            }
        }

        public string Pmid
        {
            get
            {
                return this.pmid;
            }
            set
            {
                this.pmid = value;
            }
        }

        public string Supplier
        {
            get
            {
                return this.supplier;
            }
            set
            {
                this.supplier = value;
            }
        }

        public string ValidateDepartment
        {
            get
            {
                return this.validateDepartment;
            }
            set
            {
                this.validateDepartment = value;
            }
        }
    }
}

