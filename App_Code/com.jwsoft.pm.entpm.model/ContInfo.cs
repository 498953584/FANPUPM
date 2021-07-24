namespace com.jwsoft.pm.entpm.Model
{
    using System;

    public class ContInfo
    {
        private int _auditstate;
        private DateTime _beginexecutedate = DateTime.Parse("1900-01-01");
        private string _buildingnumber = "";
        private decimal _cenewcontmoney;
        private string _conclassattr;
        private DateTime _constitutedate = DateTime.Parse("1900-01-01");
        private int _contclassid;
        private string _contCode;
        private decimal _contmoney;
        private string _contname;
        private string _contnumber;
        private int _contractid;
        private DateTime _endexecutedate = DateTime.Parse("1900-01-01");
        private string _firstparty;
        private DateTime _givendate = DateTime.Parse("1900-01-01");
        private string _giventranperson;
        private string _groundnumber = "";
        private int _i_pschildnum;
        private string _parentidcoll;
        private int _ProjectId;
        private string _projectname;
        private DateTime _recorddate = DateTime.Parse("1900-01-01");
        private int _RelationInfoID;
        private string _remark;
        private string _residencecommunity = "";
        private string _roomnumber = "";
        private string _secondparty;
        private int _secondpartyid;
        private string _secondpartytype;
        private string _usercode;

        public int AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
            }
        }

        public DateTime BeginExecuteDate
        {
            get
            {
                return this._beginexecutedate;
            }
            set
            {
                this._beginexecutedate = value;
            }
        }

        public string BuildingNumber
        {
            get
            {
                return this._buildingnumber;
            }
            set
            {
                this._buildingnumber = value;
            }
        }

        public decimal CenewContMoney
        {
            get
            {
                return this._cenewcontmoney;
            }
            set
            {
                this._cenewcontmoney = value;
            }
        }

        public string ConClassAttr
        {
            get
            {
                return this._conclassattr;
            }
            set
            {
                this._conclassattr = value;
            }
        }

        public DateTime ConstituteDate
        {
            get
            {
                return this._constitutedate;
            }
            set
            {
                this._constitutedate = value;
            }
        }

        public int ContClassID
        {
            get
            {
                return this._contclassid;
            }
            set
            {
                this._contclassid = value;
            }
        }

        public string ContCode
        {
            get
            {
                return this._contCode;
            }
            set
            {
                this._contCode = value;
            }
        }

        public decimal ContMoney
        {
            get
            {
                return this._contmoney;
            }
            set
            {
                this._contmoney = value;
            }
        }

        public string ContName
        {
            get
            {
                return this._contname;
            }
            set
            {
                this._contname = value;
            }
        }

        public string ContNumber
        {
            get
            {
                return this._contnumber;
            }
            set
            {
                this._contnumber = value;
            }
        }

        public int ContractID
        {
            get
            {
                return this._contractid;
            }
            set
            {
                this._contractid = value;
            }
        }

        public DateTime EndExecuteDate
        {
            get
            {
                return this._endexecutedate;
            }
            set
            {
                this._endexecutedate = value;
            }
        }

        public string FirstParty
        {
            get
            {
                return this._firstparty;
            }
            set
            {
                this._firstparty = value;
            }
        }

        public DateTime GivenDate
        {
            get
            {
                return this._givendate;
            }
            set
            {
                this._givendate = value;
            }
        }

        public string GivenTranPerson
        {
            get
            {
                return this._giventranperson;
            }
            set
            {
                this._giventranperson = value;
            }
        }

        public string GroundNumber
        {
            get
            {
                return this._groundnumber;
            }
            set
            {
                this._groundnumber = value;
            }
        }

        public int I_pschildnum
        {
            get
            {
                return this._i_pschildnum;
            }
            set
            {
                this._i_pschildnum = value;
            }
        }

        public string Parentidcoll
        {
            get
            {
                return this._parentidcoll;
            }
            set
            {
                this._parentidcoll = value;
            }
        }

        public int ProjectId
        {
            get
            {
                return this._ProjectId;
            }
            set
            {
                this._ProjectId = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this._projectname;
            }
            set
            {
                this._projectname = value;
            }
        }

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
            }
        }

        public int RelationInfoID
        {
            get
            {
                return this._RelationInfoID;
            }
            set
            {
                this._RelationInfoID = value;
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

        public string ResidenceCommunity
        {
            get
            {
                return this._residencecommunity;
            }
            set
            {
                this._residencecommunity = value;
            }
        }

        public string RoomNumber
        {
            get
            {
                return this._roomnumber;
            }
            set
            {
                this._roomnumber = value;
            }
        }

        public string SecondParty
        {
            get
            {
                return this._secondparty;
            }
            set
            {
                this._secondparty = value;
            }
        }

        public int SecondPartyID
        {
            get
            {
                return this._secondpartyid;
            }
            set
            {
                this._secondpartyid = value;
            }
        }

        public string SecondPartyType
        {
            get
            {
                return this._secondpartytype;
            }
            set
            {
                this._secondpartytype = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }
    }
}

