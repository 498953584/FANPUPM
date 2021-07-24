namespace cn.justwin.opm
{
    using System;

    public class PrjStageSetModel
    {
        private DateTime addTime;
        private string addUser;
        private int childCount;
        private int I_xh;
        private string isFlow;
        private string isValid;
        private string parentID;
        private string parentStageCode;
        private string podepom;
        private string prjID;
        private string prjStageID;
        private string prjStageName;
        private string readWrite;
        private string remark;
        private string stageCode;

        public DateTime AddTime
        {
            get
            {
                return this.addTime;
            }
            set
            {
                this.addTime = value;
            }
        }

        public string AddUser
        {
            get
            {
                return this.addUser;
            }
            set
            {
                this.addUser = value;
            }
        }

        public int ChildCount
        {
            get
            {
                return this.childCount;
            }
            set
            {
                this.childCount = value;
            }
        }

        public int I_xh1
        {
            get
            {
                return this.I_xh;
            }
            set
            {
                this.I_xh = value;
            }
        }

        public string IsFlow
        {
            get
            {
                return this.isFlow;
            }
            set
            {
                this.isFlow = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        public string ParentID
        {
            get
            {
                return this.parentID;
            }
            set
            {
                this.parentID = value;
            }
        }

        public string ParentStageCode
        {
            get
            {
                return this.parentStageCode;
            }
            set
            {
                this.parentStageCode = value;
            }
        }

        public string Podepom
        {
            get
            {
                return this.podepom;
            }
            set
            {
                this.podepom = value;
            }
        }

        public string PrjID
        {
            get
            {
                return this.prjID;
            }
            set
            {
                this.prjID = value;
            }
        }

        public string PrjStageID
        {
            get
            {
                return this.prjStageID;
            }
            set
            {
                this.prjStageID = value;
            }
        }

        public string PrjStageName
        {
            get
            {
                return this.prjStageName;
            }
            set
            {
                this.prjStageName = value;
            }
        }

        public string ReadWrite
        {
            get
            {
                return this.readWrite;
            }
            set
            {
                this.readWrite = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        public string StageCode
        {
            get
            {
                return this.stageCode;
            }
            set
            {
                this.stageCode = value;
            }
        }
    }
}

