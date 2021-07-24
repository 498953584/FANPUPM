namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class inputReceiptReportCollectin : CollectionBase
    {
        public int Add(inputReceiptReportInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(inputReceiptReportInfo value)
        {
            return base.List.Contains(value);
        }

        public decimal GetAllInputAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                d += this[i].InputAmount;
            }
            return decimal.Round(d, 4);
        }

        public decimal GetAllInputCompAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                d += this[i].InputCompAmount;
            }
            return decimal.Round(d, 4);
        }

        public decimal GetAllReceiptAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                d += this[i].ReceiptAmount;
            }
            return decimal.Round(d, 4);
        }

        public decimal GetAllReceiptCompAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                d += this[i].ReceiptCompAmount;
            }
            return decimal.Round(d, 4);
        }

        public decimal GetPrjInputAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                if (this[i].IsProject)
                {
                    d += this[i].InputAmount;
                }
            }
            return decimal.Round(d, 4);
        }

        public decimal GetPrjInputCompAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                if (this[i].IsProject)
                {
                    d += this[i].InputCompAmount;
                }
            }
            return decimal.Round(d, 4);
        }

        public decimal GetPrjReceiptAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                if (this[i].IsProject)
                {
                    d += this[i].ReceiptAmount;
                }
            }
            return decimal.Round(d, 4);
        }

        public decimal GetPrjReceiptCompAmount()
        {
            decimal d = 0M;
            for (int i = 0; i < base.Count; i++)
            {
                if (this[i].IsProject)
                {
                    d += this[i].ReceiptCompAmount;
                }
            }
            return decimal.Round(d, 4);
        }

        public int IndexOf(inputReceiptReportInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, inputReceiptReportInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(inputReceiptReportInfo value)
        {
            base.List.Remove(value);
        }

        public inputReceiptReportInfo this[int index]
        {
            get
            {
                return (inputReceiptReportInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        public ArrayList PlanItems
        {
            get
            {
                return base.InnerList;
            }
        }
    }
}

