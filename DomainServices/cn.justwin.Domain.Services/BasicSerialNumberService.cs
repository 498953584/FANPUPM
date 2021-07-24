namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BasicSerialNumberService : Repository<BasicSerialNumber>
    {
        private void AddNo(string no, string tableName, string keyValue)
        {
            BasicSerialNumber item = new BasicSerialNumber {
                No = no,
                TableName = tableName,
                KeyValue = keyValue,
                InTime = DateTime.Now
            };
            base.Add(item);
        }

        private BasicSerialNumber GetByKey(string tableName, string keyValue)
        {
            return (from n in this
                where (n.TableName == tableName) && (n.KeyValue == keyValue)
                select n).FirstOrDefault<BasicSerialNumber>();
        }

        private string GetMaxNo()
        {
            BasicSerialNumber number = (from n in this
                orderby n.InTime descending
                select n).FirstOrDefault<BasicSerialNumber>();
            if (number == null)
            {
                return "00000000";
            }
            return number.No;
        }

        private string GetNextNo(string no)
        {
            int num = Convert.ToInt32(no) + 1;
            return num.ToString().PadLeft(8, '0');
        }

        public string GetNo(string tableName, string keyValue)
        {
            BasicSerialNumber byKey = this.GetByKey(tableName, keyValue);
            if (byKey != null)
            {
                return byKey.No;
            }
            string maxNo = this.GetMaxNo();
            string nextNo = this.GetNextNo(maxNo);
            while (this.IsExists(nextNo))
            {
                nextNo = this.GetNextNo(nextNo);
            }
            this.AddNo(nextNo, tableName, keyValue);
            return nextNo;
        }

        private bool IsExists(string no)
        {
            return ((from n in this
                where n.No == no
                select n).Count<BasicSerialNumber>() > 0);
        }
    }
}

