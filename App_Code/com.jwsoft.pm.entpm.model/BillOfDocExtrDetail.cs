namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class BillOfDocExtrDetail
    {
        private int _billofdocextrdetailid;
        private int _billofdocextrid;
        private string _billofdocextrtype;
        private string _isInfluence;
        private int _materialid;
        private int _mdResourceId;
        private int _projectid;
        private decimal _scalar;
        private int _taskid;
        private decimal _unitprice;

        public int BillOfDocExtrDetailID
        {
            get
            {
                return this._billofdocextrdetailid;
            }
            set
            {
                this._billofdocextrdetailid = value;
            }
        }

        public int BillOfDocExtrID
        {
            get
            {
                return this._billofdocextrid;
            }
            set
            {
                this._billofdocextrid = value;
            }
        }

        public string BillOfDocExtrType
        {
            get
            {
                return this._billofdocextrtype;
            }
            set
            {
                this._billofdocextrtype = value;
            }
        }

        public string IsInfluence
        {
            get
            {
                return this._isInfluence;
            }
            set
            {
                this._isInfluence = value;
            }
        }

        public int MaterialId
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

        public int MdResourceId
        {
            get
            {
                return this._mdResourceId;
            }
            set
            {
                this._mdResourceId = value;
            }
        }

        public int ProjectId
        {
            get
            {
                return this._projectid;
            }
            set
            {
                this._projectid = value;
            }
        }

        public decimal Scalar
        {
            get
            {
                return this._scalar;
            }
            set
            {
                this._scalar = value;
            }
        }

        public int TaskId
        {
            get
            {
                return this._taskid;
            }
            set
            {
                this._taskid = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._unitprice;
            }
            set
            {
                this._unitprice = value;
            }
        }
    }
}

