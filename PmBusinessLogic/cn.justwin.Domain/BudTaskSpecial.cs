namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class BudTaskSpecial
    {
        private int codeIndex = 1;
        private int contentIndex = 5;
        private int nameIndex = 2;
        private int noteIndex = 4;
        private int priceIndex = 7;
        private int quantityIndex = 6;
        private int signNumberIndex;
        private int startIndex;
        private int totalIndex = 8;
        private int unitIndex = 3;

        public BudTaskSpecial(string taskId, string prjId, string inputUser, DataTable taskTable)
        {
            this.taskId = taskId;
            this.prjId = prjId;
            this.version = 1;
            this.inputUser = inputUser;
            this.taskTable = taskTable;
        }

        private void AddTaskResource(string resCode, string resName, decimal price, decimal quanity, string taskId, List<string> errors)
        {
            Resource byId = Resource.GetById(Resource.GetResourceId(resCode));
            if (byId != null)
            {
                TaskResource taskResource = new TaskResource {
                    Resource = byId,
                    InputUser = this.inputUser,
                    InputDate = new DateTime?(DateTime.Now),
                    Price = price,
                    Quantity = quanity
                };
                BudTask.AddResource(taskId, taskResource);
            }
            else
            {
                string item = "资源编号：" + resCode + " 在资源库不存在，已添加到资源映射！";
                if (!errors.Contains(item))
                {
                    errors.Add(item);
                    ResourceTemp.Add(ResourceTemp.Create(string.Empty, taskId, resCode, resName, new decimal?(price), new decimal?(quanity), new decimal?(price * quanity), this.prjId));
                }
            }
        }

        public int ConverBudTaskList(List<string> errors)
        {
            int num = 0;
            List<BudTask> list = new List<BudTask>();
            List<string> codeList = this.GetCodeList();
            List<int> layerByCode = this.GetLayerByCode(codeList);
            List<string> orderNumberByLayer = this.GetOrderNumberByLayer(layerByCode);
            string code = string.Empty;
            string name = string.Empty;
            string unit = string.Empty;
            decimal quantity = 0M;
            string note = string.Empty;
            string orderNumber = BudTask.GetOrderNumber(this.prjId, this.taskId);
            string str6 = string.Empty;
            for (int i = this.startIndex; i < this.taskTable.Rows.Count; i++)
            {
                DataRow row = this.taskTable.Rows[i];
                object obj2 = row[this.taskTable.Columns.Count - 1];
                code = (row[this.codeIndex] == null) ? "" : row[this.codeIndex].ToString();
                string item = string.Empty;
                name = (row[this.nameIndex] == null) ? "" : row[this.nameIndex].ToString();
                quantity = 0M;
                DateTime? startDate = null;
                DateTime? endDate = null;
                if ((obj2 == null) || string.IsNullOrEmpty(obj2.ToString()))
                {
                    bool flag = true;
                    if (this.quantityIndex != -1)
                    {
                        try
                        {
                            string str8 = row[this.quantityIndex].ToString();
                            if (!string.IsNullOrEmpty(str8))
                            {
                                quantity = decimal.Parse(str8);
                            }
                        }
                        catch
                        {
                            item = "在您的Excel中：编码" + code + ",存在非有效的数字,请修改！";
                            errors.Add(item);
                            flag = false;
                        }
                    }
                    unit = string.Empty;
                    if (this.unitIndex != -1)
                    {
                        unit = row[this.unitIndex].ToString();
                    }
                    if (this.noteIndex != -1)
                    {
                        note = row[this.noteIndex].ToString();
                    }
                    if (!flag)
                    {
                        item = "在您的Excel中：编码" + code + ",存在错误数据导致其子节点不能导入！";
                        errors.Add(item);
                        int num4 = 0;
                        string str9 = orderNumberByLayer[i];
                        for (int j = i + 1; j < orderNumberByLayer.Count; j++)
                        {
                            if (orderNumberByLayer[j].StartsWith(str9))
                            {
                                num4++;
                            }
                        }
                        i += num4;
                    }
                    else
                    {
                        num++;
                        string id = Guid.NewGuid().ToString();
                        string parentTaskId = (this.taskId == "") ? null : this.taskId;
                        decimal? unitPrice = null;
                        decimal? total = null;
                        BudTask task = BudTask.Create(id, parentTaskId, null, this.prjId, code, name, unit, quantity, startDate, endDate, true, note, this.inputUser, DateTime.Now, unitPrice, total);
                        string str12 = orderNumberByLayer[i].Substring(3);
                        if (string.IsNullOrEmpty(str12))
                        {
                            if (i > this.startIndex)
                            {
                                orderNumber = BudTask.GetOrderNumber(this.prjId, this.taskId);
                            }
                            task.OrderNumber = orderNumber;
                        }
                        else
                        {
                            task.OrderNumber = orderNumber + str12;
                        }
                        if (i > 0)
                        {
                            int count = list.Count;
                            if (count > 0)
                            {
                                if ((orderNumberByLayer[i].Length - str6.Length) == 3)
                                {
                                    task.ParentId = list[count - 1].Id;
                                }
                                else if ((orderNumberByLayer[i].Length - str6.Length) == 0)
                                {
                                    task.ParentId = list[count - 1].ParentId;
                                }
                                else
                                {
                                    int length = orderNumberByLayer[i].Length;
                                    if (length == 3)
                                    {
                                        task.ParentId = list[0].ParentId;
                                    }
                                    else if (length > 3)
                                    {
                                        int differOrderNumber = list[0].OrderNumber.Length - orderNumberByLayer[0].Length;
                                        int num7 = list.FindLastIndex(t => t.OrderNumber.Length == (length + differOrderNumber));
                                        task.ParentId = list[num7].ParentId;
                                    }
                                }
                            }
                        }
                        str6 = orderNumberByLayer[i];
                        list.Add(task);
                        BudTask.Add(task, false);
                    }
                }
                else
                {
                    decimal result = 0M;
                    object obj3 = row[this.priceIndex];
                    if (obj3 != null)
                    {
                        decimal.TryParse(obj3.ToString(), out result);
                    }
                    obj3 = row[this.quantityIndex];
                    if (obj3 != null)
                    {
                        decimal.TryParse(obj3.ToString(), out quantity);
                    }
                    this.AddTaskResource(code, name, result, quantity, list[list.Count - 1].Id, errors);
                }
            }
            return num;
        }

        private List<string> GetCodeList()
        {
            List<string> list = new List<string>();
            foreach (DataRow row in this.taskTable.Rows)
            {
                list.Add(row[this.signNumberIndex].ToString().Trim());
            }
            return list;
        }

        private List<int> GetLayerByCode(List<string> codeList)
        {
            List<int> layerList = new List<int>();
            Regex regex = new Regex("^[一二三四五六七八九十]+$");
            Regex regex2 = new Regex(@"^[\(\（]\d+[\)\）]$");
            Regex regex3 = new Regex("^[0-9]+$");
            Regex regex4 = new Regex("^[0-9]+.[0-9]+$");
            Regex regex5 = new Regex("^[0-9]+.[0-9]+.[0-9]+$");
            foreach (string str in codeList)
            {
                if (regex.IsMatch(str))
                {
                    layerList.Add(1);
                }
                else if (regex2.IsMatch(str))
                {
                    layerList.Add(2);
                }
                else if (regex3.IsMatch(str))
                {
                    layerList.Add(3);
                }
                else if (regex4.IsMatch(str))
                {
                    layerList.Add(4);
                }
                else if (regex5.IsMatch(str))
                {
                    layerList.Add(5);
                }
                else
                {
                    layerList.Add(0);
                }
            }
            return this.GetStandardLayer(layerList);
        }

        public List<string> GetOrderNumberByLayer(List<int> layerList)
        {
            List<string> list = new List<string>();
            int minLayer = layerList.Min<int>((System.Func<int, int>) (lay => lay));
            layerList.Max<int>((System.Func<int, int>) (lay => lay));
            layerList.Count<int>(lay => lay == minLayer);
            string prev = string.Empty;
            int num = 0;
            int num2 = 0;
            System.Func<string, bool> predicate = null;
            for (int i = num; i < layerList.Count; i++)
            {
                if (layerList[i] != 5)
                {
                    string item = "001";
                    if (i > 0)
                    {
                        if (layerList[i] > layerList[(i - 1) - num2])
                        {
                            item = prev + "001";
                        }
                        else if (layerList[i] == layerList[(i - 1) - num2])
                        {
                            item = this.GetOrderNumberByPrev(prev);
                        }
                        else if (layerList[i] == minLayer)
                        {
                            item = this.GetOrderNumberByPrev(prev.Substring(0, 3));
                        }
                        else
                        {
                            if (predicate == null)
                            {
                                predicate = o => (o.Length / 3) == layerList[i];
                            }
                            string str3 = list.GetRange(0, (i - 1) - num2).Last<string>(predicate);
                            item = this.GetOrderNumberByPrev(str3);
                        }
                    }
                    prev = item;
                    list.Add(item);
                    num2 = 0;
                }
                else
                {
                    list.Add("5");
                    num2++;
                }
            }
            return list;
        }

        private string GetOrderNumberByPrev(string prev)
        {
            string str2 = prev.Substring(0, prev.Length - 3);
            string str4 = (Convert.ToInt32(prev.Substring(prev.Length - 3, 3)) + 1).ToString();
            if (str4.Length == 1)
            {
                str4 = "00" + str4;
            }
            if (str4.Length == 2)
            {
                str4 = "0" + str4;
            }
            return (str2 + str4);
        }

        private List<int> GetStandardLayer(List<int> layerList)
        {
            List<int> list = new List<int>();
            int num = 0;
            for (int i = 0; i < layerList.Count; i++)
            {
                System.Func<int, bool> predicate = null;
                int currentLayer = layerList[i];
                if (currentLayer != 5)
                {
                    int item = 1;
                    if (i > 0)
                    {
                        int num4 = layerList[(i - 1) - num];
                        int num5 = list[(i - 1) - num];
                        if (currentLayer == num4)
                        {
                            item = num5;
                        }
                        else if (currentLayer > num4)
                        {
                            item = num5 + 1;
                        }
                        else if (currentLayer == 1)
                        {
                            item = 1;
                        }
                        else
                        {
                            if (predicate == null)
                            {
                                predicate = c => c == currentLayer;
                            }
                            if (layerList.GetRange(0, (i - 1) - num).Any<int>(predicate))
                            {
                                int num6 = layerList.GetRange(0, (i - 1) - num).LastIndexOf(currentLayer);
                                item = list[num6];
                            }
                            else
                            {
                                item = num5;
                            }
                        }
                    }
                    list.Add(item);
                    num = 0;
                }
                else
                {
                    num++;
                    list.Add(5);
                }
            }
            return list;
        }

        private string inputUser { get; set; }

        private string prjId { get; set; }

        private string taskId { get; set; }

        private DataTable taskTable { get; set; }

        private int version { get; set; }
    }
}

