namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class PayoutTargetModel
    {
        public string contractId;
        private string id;
        public decimal? signAmount;
        public string taskId;

        public string ContractId
        {
            get
            {
                return this.contractId;
            }
            set
            {
                this.contractId = value;
            }
        }

        public string Id
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

        public decimal? SignAmount
        {
            get
            {
                return this.signAmount;
            }
            set
            {
                this.signAmount = value;
            }
        }

        public string TaskId
        {
            get
            {
                return this.taskId;
            }
            set
            {
                this.taskId = value;
            }
        }
    }
}

