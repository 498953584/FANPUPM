namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResApplicationCollectDetail
    {
        private int _acdetailid;
        private Guid _acrecordid;
        private int _applynumber;
        private int _materialid;

        public int ACDetailID
        {
            get
            {
                return this._acdetailid;
            }
            set
            {
                this._acdetailid = value;
            }
        }

        public Guid ACRecordID
        {
            get
            {
                return this._acrecordid;
            }
            set
            {
                this._acrecordid = value;
            }
        }

        public int ApplyNumber
        {
            get
            {
                return this._applynumber;
            }
            set
            {
                this._applynumber = value;
            }
        }

        public int MaterialID
        {
            get
            {
                return this._materialid;
            }
            set
            {
                this._materialid = value;
            }
        }
    }
}

