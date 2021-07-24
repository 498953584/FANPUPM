namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class AlertMessage
    {
        private bool _IsValid;
        private string _LinkUrl;
        private string _ManInput;
        private string _Memo;
        private string _MenAlertTo;
        private string _Message;
        private int _pkID;
        private int _PresentimentID;
        private string _PrjCode;
        private string _ResourceCode;
        private int _RiskID;
        private DateTime _TimeInput;
        private DateTime _TimeOutput;
        private DateTime _TimeOver;

        public AlertMessage()
        {
            this._IsValid = true;
        }

        public AlertMessage(int pkID)
        {
            this._IsValid = true;
            this._pkID = pkID;
            this.GetInfo(this._pkID);
        }

        private static void AssignValueFromDataRow(AlertMessage msg, DataRow dr)
        {
            msg._IsValid = (bool) dr["IsValid"];
            msg._LinkUrl = (string) dr["LinkUrl"];
            msg._ManInput = (string) dr["ManInput"];
            msg._Memo = (string) dr["Memo"];
            msg._MenAlertTo = (string) dr["MenAlertTo"];
            msg._Message = (string) dr["Message"];
            msg._PrjCode = (string) dr["PrjCode"];
            msg._ResourceCode = (string) dr["ResourceCode"];
            msg._TimeInput = (DateTime) dr["TimeInput"];
            msg._TimeOutput = (DateTime) dr["TimeOutput"];
            msg._TimeOver = (DateTime) dr["TimeOver"];
            msg._RiskID = (int) dr["RiskID"];
            msg._PresentimentID = (int) dr["PresentimentID"];
        }

        public static void Disabled(int AlertMessageID)
        {
            if (AlertMessageID > 0)
            {
                publicDbOpClass.NonQuerySqlString("update prj_AlertMessage set IsValid=0 where [ID]=" + AlertMessageID);
            }
        }

        public static AlertMessage[] GetCurrentMessagesOfUser(string yhdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from prj_AlertMessage where IsValid=1 and ManAlertTo='" + yhdm + "' and TimeOutput < getDate() and TimeOver > getDate()");
            int count = table.Rows.Count;
            AlertMessage[] messageArray = new AlertMessage[count];
            for (int i = 0; i < count; i++)
            {
                AlertMessage msg = new AlertMessage();
                AssignValueFromDataRow(msg, table.Rows[i]);
                messageArray[i] = msg;
            }
            return messageArray;
        }

        private void GetInfo(int pkID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from prj_AlertMessage where [ID]=" + pkID);
            if (table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                AssignValueFromDataRow(this, dr);
            }
        }

        public void Send()
        {
            char[] separator = new char[] { ',' };
            string[] strArray = this._MenAlertTo.Trim(separator).Split(separator);
            int length = strArray.Length;
            StringBuilder builder = new StringBuilder("insert into prj_AlertMessage(ManAlertTo,PrjCode,ResourceCode,Message,LinkUrl,ManInput,Memo,TimeInput,TimeOutput,TimeOver,RiskID,PresentimentID) ");
            for (int i = 0; i < length; i++)
            {
                string str = string.Format(" select '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11} union", new object[] { strArray[i], this._PrjCode, this._ResourceCode, this._Message, this._LinkUrl, this._ManInput, this._Memo, this._TimeInput, this._TimeOutput, this._TimeOver, this._RiskID, this._PresentimentID });
                builder.Append(str);
            }
            if (length > 0)
            {
                builder.Length -= 5;
            }
            publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public string LinkUrl
        {
            get
            {
                return this._LinkUrl;
            }
            set
            {
                this._LinkUrl = value;
            }
        }

        public string ManInput
        {
            get
            {
                return this._ManInput;
            }
            set
            {
                this._ManInput = value;
            }
        }

        public string Memo
        {
            get
            {
                return this._Memo;
            }
            set
            {
                this._Memo = value;
            }
        }

        public string MenAlertTo
        {
            get
            {
                return this._MenAlertTo;
            }
            set
            {
                this._MenAlertTo = value;
            }
        }

        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }

        public int PresentimentID
        {
            get
            {
                return this._PresentimentID;
            }
            set
            {
                this._PresentimentID = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this._ResourceCode = value;
            }
        }

        public int RiskID
        {
            get
            {
                return this._RiskID;
            }
            set
            {
                this._RiskID = value;
            }
        }

        public DateTime TimeInput
        {
            get
            {
                return this._TimeInput;
            }
            set
            {
                this._TimeInput = value;
            }
        }

        public DateTime TimeOutput
        {
            get
            {
                return this._TimeOutput;
            }
            set
            {
                this._TimeOutput = value;
            }
        }

        public DateTime TimeOver
        {
            get
            {
                return this._TimeOver;
            }
            set
            {
                this._TimeOver = value;
            }
        }
    }
}

