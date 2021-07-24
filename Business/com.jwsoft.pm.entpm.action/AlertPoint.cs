namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;
    using System.Reflection;

    public class AlertPoint
    {
        private string _AlertCode;
        private DataRow _InfoRow;
        private AlertOptionCollection _Options;
        private int _pkID;

        public AlertPoint(int pkID)
        {
            this._pkID = pkID;
            this._InfoRow = this.GetInfoRow(this._pkID);
        }

        public AlertPoint(string AlertCode)
        {
            this._AlertCode = AlertCode;
            this._InfoRow = this.GetInfoRow(this._AlertCode);
        }

        private DataRow GetInfoRow(int ID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select MainID,Code,Title,Depict,ToPeople,[ToPeopleNames]=dbo.prj_f_getUsersName(ToPeople) from prj_presentiment where MainID=" + ID);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0];
            }
            return null;
        }

        private DataRow GetInfoRow(string Code)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select MainID,Code,Title,Depict,ToPeople,[ToPeopleNames]=dbo.prj_f_getUsersName(ToPeople),[ValidTimeLong]=isnull(ValidTimeLong,3) from prj_presentiment a  where Code='" + Code + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0];
            }
            return null;
        }

        private AlertOptionCollection GetOptions()
        {
            AlertOptionCollection options = new AlertOptionCollection();
            DataTable table = publicDbOpClass.DataTableQuary("select OptionID,OptionText,MainID,IsValid,OptionCode from prj_prsentiment_options where MainID=" + this.pkID);
            int count = table.Rows.Count;
            if (count == 0)
            {
                return null;
            }
            for (int i = 0; i < count; i++)
            {
                AlertOption opt = new AlertOption {
                    pkID = (int) table.Rows[i]["OptionID"],
                    OptionID = (int) table.Rows[i]["OptionCode"],
                    OptionText = (string) table.Rows[i]["OptionText"],
                    IsSelected = (bool) table.Rows[i]["IsValid"]
                };
                options.Add(opt);
            }
            return options;
        }

        public AlertOption getSelectedOption()
        {
            DataTable table = publicDbOpClass.DataTableQuary("select OptionID,OptionText,MainID,IsValid,OptionCode from prj_prsentiment_options where IsValid=1 and MainID=" + this.pkID);
            AlertOption option = new AlertOption();
            if (table.Rows.Count > 0)
            {
                option.pkID = (int) table.Rows[0]["OptionID"];
                option.OptionID = (int) table.Rows[0]["OptionCode"];
                option.OptionText = (string) table.Rows[0]["OptionText"];
                option.IsSelected = (bool) table.Rows[0]["IsValid"];
            }
            return option;
        }

        public string Code
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return "";
                }
                return (string) this._InfoRow["Code"];
            }
        }

        public string Depict
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return "";
                }
                return (string) this._InfoRow["Depict"];
            }
        }

        public string Name
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return "";
                }
                return (string) this._InfoRow["Title"];
            }
        }

        public string NamesOfPeopleAlertTo
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return "";
                }
                return (string) this._InfoRow["ToPeopleNames"];
            }
        }

        public AlertOptionCollection Options
        {
            get
            {
                if (this._Options == null)
                {
                    this._Options = this.GetOptions();
                }
                return this._Options;
            }
        }

        public int pkID
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return 0;
                }
                return (int) this._InfoRow["MainID"];
            }
        }

        public int RiskId
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return 0;
                }
                return (int) this._InfoRow["RiskID"];
            }
        }

        public string RiskName
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return "";
                }
                return (string) this._InfoRow["RiskName"];
            }
        }

        public double ValidTimeLong
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return 0.0;
                }
                return (double) ((float) this._InfoRow["ValidTimeLong"]);
            }
        }

        public string YHDMsOfPeopleAlertTo
        {
            get
            {
                if (this._InfoRow == null)
                {
                    return "";
                }
                return (string) this._InfoRow["ToPeople"];
            }
        }

        public class AlertOption
        {
            private bool _IsCurrentValid;
            private int _OptionID = -1;
            private string _OptionText = "";
            private int _pkID;

            public bool IsSelected
            {
                get
                {
                    return this._IsCurrentValid;
                }
                set
                {
                    this._IsCurrentValid = value;
                }
            }

            public int OptionID
            {
                get
                {
                    return this._OptionID;
                }
                set
                {
                    this._OptionID = value;
                }
            }

            public string OptionText
            {
                get
                {
                    return this._OptionText;
                }
                set
                {
                    this._OptionText = value;
                }
            }

            public int pkID
            {
                get
                {
                    return this._pkID;
                }
                set
                {
                    this._pkID = value;
                }
            }
        }

        public class AlertOptionCollection
        {
            private Hashtable _Options = new Hashtable();

            internal void Add(AlertPoint.AlertOption Opt)
            {
                this._Options.Add(this._Options.Count, Opt);
            }

            public int Count
            {
                get
                {
                    return this._Options.Count;
                }
            }

            public AlertPoint.AlertOption this[int idx]
            {
                get
                {
                    return (AlertPoint.AlertOption) this._Options[idx];
                }
                set
                {
                    this._Options[idx] = value;
                }
            }
        }
    }
}

