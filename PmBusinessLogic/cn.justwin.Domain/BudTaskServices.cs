namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Excel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class BudTaskServices
    {
        public BudTaskTypeServices budTaskTypeServices;
        public static Dictionary<string, string> codeAndTaskId = new Dictionary<string, string>();
        public int codeIndex = -1;
        public int constructionPeriodIndex = -1;
        public int endDateIndex = -1;
        public int featureDescriptionIndex = -1;
        public int inputDateIndex = -1;
        public int inputUserIndex = -1;
        public int laborIndex = -1;
        public int mainMaterialIndex = -1;
        public int nameIndex = -1;
        public int noteIndex = -1;
        public string prjId;
        public int quantityIndex = -1;
        public int rankIndex = -1;
        public int startDateIndex = -1;
        public int startIndex;
        public int subMaterialIndex = -1;
        public string taskId;
        private DataTable taskTable;
        public int taskTypeIndex = -1;
        public int totalIndex = -1;
        public int unitIndex = -1;
        public int unitPriceIndex = -1;
        public int version;

        public BudTaskServices(string taskId, string prjId, int version, DataTable taskTable)
        {
            this.taskId = taskId;
            this.prjId = prjId;
            this.version = version;
            this.taskTable = taskTable;
        }

        public void AddCodeAndTaskId(string code, string taskId)
        {
            bool flag = false;
            foreach (string str in codeAndTaskId.Keys)
            {
                if (code == str)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                codeAndTaskId.Add(code, taskId);
            }
        }

        public void AddSerialNoAndTaskId(string serialNo, string taskId)
        {
            bool flag = false;
            foreach (string str in codeAndTaskId.Keys)
            {
                if (serialNo == str)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                codeAndTaskId.Add(serialNo, taskId);
            }
        }

        public int ConverBudTaskList(string[] colArray, string inputUser, List<string> errors, string IsWBSRelevance)
        {
            int num = 0;
            List<cn.justwin.Domain.BudTask> list = new List<cn.justwin.Domain.BudTask>();
            IDictionary<string, int> relation = ExcelUtility.GetRelation(colArray);
            List<string> orderNumberByLayer = new List<string>();
            try
            {
                this.ParseTaskTable(relation);
                List<int> layerList = new List<int>();
                if (this.rankIndex == -1)
                {
                    List<string> codeList = this.GetCodeList();
                    layerList = this.GetLayersByCode(codeList);
                }
                else
                {
                    layerList = this.GetLayers();
                }
                orderNumberByLayer = this.GetOrderNumberByLayer(layerList);
            }
            catch (Exception exception)
            {
                errors.Add(exception.Message);
            }
            string code = string.Empty;
            string name = string.Empty;
            string unit = string.Empty;
            decimal quantity = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            DateTime? startDate = null;
            DateTime? endDate = null;
            string note = string.Empty;
            string orderNumber = cn.justwin.Domain.BudTask.GetOrderNumber(this.prjId, this.taskId);
            string str6 = string.Empty;
            int num5 = 0;
            string str7 = string.Empty;
            string str8 = string.Empty;
            for (int i = this.startIndex; i < this.taskTable.Rows.Count; i++)
            {
                bool flag = true;
                DataRow row = this.taskTable.Rows[i];
                code = (row[this.codeIndex] == null) ? "" : row[this.codeIndex].ToString();
                string item = string.Empty;
                name = (row[this.nameIndex] == null) ? "" : row[this.nameIndex].ToString();
                quantity = 0M;
                if (this.quantityIndex != -1)
                {
                    try
                    {
                        string str10 = row[this.quantityIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str10))
                        {
                            quantity = decimal.Parse(str10);
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + code + ",工程量存在非有效的数字,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (IsWBSRelevance == "0")
                {
                    if (this.unitPriceIndex != -1)
                    {
                        try
                        {
                            string str11 = row[this.unitPriceIndex].ToString().Trim();
                            if (!string.IsNullOrEmpty(str11))
                            {
                                num3 = decimal.Parse(str11);
                            }
                        }
                        catch
                        {
                            item = "在您的Excel中：编码" + code + ",综合单价存在非有效的数字,请修改！";
                            errors.Add(item);
                            flag = false;
                        }
                    }
                    if (this.totalIndex != -1)
                    {
                        try
                        {
                            num4 = quantity * num3;
                        }
                        catch
                        {
                            item = "在您的Excel中：编码" + code + ",综合单价存在非有效的数字,请修改！";
                            errors.Add(item);
                            flag = false;
                        }
                    }
                }
                unit = string.Empty;
                if (this.unitIndex != -1)
                {
                    unit = row[this.unitIndex].ToString();
                }
                if (this.startDateIndex != -1)
                {
                    try
                    {
                        string str12 = row[this.startDateIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str12))
                        {
                            startDate = new DateTime?(Convert.ToDateTime(str12));
                        }
                        else
                        {
                            startDate = null;
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + code + ",存在非有效时间类型,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.endDateIndex != -1)
                {
                    try
                    {
                        string str13 = row[this.endDateIndex].ToString();
                        if (!string.IsNullOrEmpty(str13))
                        {
                            endDate = new DateTime?(Convert.ToDateTime(str13));
                        }
                        else
                        {
                            endDate = null;
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + code + ",存在非有效时间类型,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.noteIndex != -1)
                {
                    note = row[this.noteIndex].ToString();
                }
                if (this.constructionPeriodIndex != -1)
                {
                    try
                    {
                        num5 = Convert.ToInt32(row[this.constructionPeriodIndex]);
                    }
                    catch
                    {
                    }
                }
                if (this.featureDescriptionIndex != -1)
                {
                    str7 = row[this.featureDescriptionIndex].ToString();
                }
                if (row.Table.Columns.Contains("序号"))
                {
                    str8 = row["序号"].ToString();
                }
                if (!flag)
                {
                    item = "在您的Excel中：编码" + code + ",存在错误数据导致其子节点不能导入！";
                    errors.Add(item);
                    int num7 = 0;
                    string str14 = orderNumberByLayer[i];
                    for (int j = i + 1; j < orderNumberByLayer.Count; j++)
                    {
                        if (orderNumberByLayer[j].StartsWith(str14))
                        {
                            num7++;
                        }
                    }
                    i += num7;
                }
                else
                {
                    num++;
                    string id = Guid.NewGuid().ToString();
                    string parentTaskId = (this.taskId == "") ? null : this.taskId;
                    cn.justwin.Domain.BudTask task = null;
                    if (IsWBSRelevance == "1")
                    {
                        decimal? unitPrice = null;
                        decimal? total = null;
                        task = cn.justwin.Domain.BudTask.Create(id, parentTaskId, null, this.prjId, code, name, unit, quantity, startDate, endDate, true, note, inputUser, DateTime.Now, unitPrice, total);
                    }
                    else
                    {
                        task = cn.justwin.Domain.BudTask.Create(id, parentTaskId, null, this.prjId, code, name, unit, quantity, startDate, endDate, true, note, inputUser, DateTime.Now, new decimal?(num3), new decimal?(num4));
                    }
                    string str17 = orderNumberByLayer[i].Substring(3);
                    if (string.IsNullOrEmpty(str17))
                    {
                        if (i > this.startIndex)
                        {
                            orderNumber = cn.justwin.Domain.BudTask.GetOrderNumber(this.prjId, this.taskId);
                        }
                        task.OrderNumber = orderNumber;
                    }
                    else
                    {
                        task.OrderNumber = orderNumber + str17;
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
                                    int num10 = list.FindLastIndex(t => t.OrderNumber.Length == (length + differOrderNumber));
                                    task.ParentId = list[num10].ParentId;
                                }
                            }
                        }
                    }
                    str6 = orderNumberByLayer[i];
                    list.Add(task);
                    cn.justwin.Domain.Entities.BudTask task2 = null;
                    BudTaskService service = new BudTaskService();
                    task2 = new cn.justwin.Domain.Entities.BudTask {
                        TaskId = task.Id,
                        ParentId = task.ParentId,
                        OrderNumber = task.OrderNumber,
                        PrjId = task.Prj,
                        InputUser = task.InputUser,
                        InputDate = task.InputDate,
                        IsValid = true,
                        Version = 1,
                        Modified = null,
                        TaskCode = task.Code,
                        TaskName = task.Name,
                        StartDate = task.StartDate,
                        EndDate = task.EndDate,
                        Quantity = new decimal?(task.Quantity),
                        UnitPrice = task.UnitPrice,
                        Total = task.Total,
                        Unit = task.Unit,
                        Note = task.Note,
                        ConstructionPeriod = new int?(num5),
                        FeatureDescription = str7,
                        TaskType = "",
                        Total2 = 0
                    };
                    service.Add(task2);
                    if (!string.IsNullOrEmpty(str8))
                    {
                        this.AddSerialNoAndTaskId(str8, task.Id);
                    }
                    else
                    {
                        this.AddCodeAndTaskId(task.Code, task.Id);
                    }
                }
            }
            return num;
        }

        public int ConverConTaskList(string[] colArray, string inputUser, List<string> errors)
        {
            int num = 0;
            List<cn.justwin.Domain.Entities.BudContractTask> list = new List<cn.justwin.Domain.Entities.BudContractTask>();
            IDictionary<string, int> relation = ExcelUtility.GetRelation(colArray);
            List<string> orderNumberByLayer = new List<string>();
            try
            {
                this.ParseTaskTable(relation);
                List<int> layerList = new List<int>();
                if (this.rankIndex == -1)
                {
                    List<string> codeList = this.GetCodeList();
                    layerList = this.GetLayersByCode(codeList);
                }
                else
                {
                    layerList = this.GetLayers();
                }
                orderNumberByLayer = this.GetOrderNumberByLayer(layerList);
            }
            catch (Exception exception)
            {
                errors.Add(exception.Message);
            }
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            DateTime? nullable = null;
            DateTime? nullable2 = null;
            string str4 = string.Empty;
            string orderNumber = cn.justwin.Domain.BudContractTask.GetOrderNumber(this.prjId, this.taskId);
            string str6 = string.Empty;
            int num5 = 0;
            string str7 = string.Empty;
            decimal num6 = 0M;
            decimal num7 = 0M;
            decimal num8 = 0M;
            string str8 = string.Empty;
            for (int i = this.startIndex; i < this.taskTable.Rows.Count; i++)
            {
                bool flag = true;
                DataRow row = this.taskTable.Rows[i];
                str = (row[this.codeIndex] == null) ? "" : row[this.codeIndex].ToString();
                string item = string.Empty;
                str2 = (row[this.nameIndex] == null) ? "" : row[this.nameIndex].ToString();
                num2 = 0M;
                num3 = 0M;
                num4 = 0M;
                if (this.quantityIndex != -1)
                {
                    try
                    {
                        string str10 = row[this.quantityIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str10))
                        {
                            num2 = decimal.Parse(str10);
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + str + ",存在非有效的数字,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.unitPriceIndex != -1)
                {
                    try
                    {
                        string str11 = row[this.unitPriceIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str11))
                        {
                            num3 = decimal.Parse(str11);
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + str + ",存在非有效的数字,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.totalIndex != -1)
                {
                    try
                    {
                        string str12 = row[this.totalIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str12))
                        {
                            num4 = decimal.Parse(str12);
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + str + ",存在非有效的数字,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.unitIndex != -1)
                {
                    str3 = row[this.unitIndex].ToString();
                }
                if (this.startDateIndex != -1)
                {
                    try
                    {
                        string str13 = row[this.startDateIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str13))
                        {
                            nullable = new DateTime?(Convert.ToDateTime(str13));
                        }
                        else
                        {
                            nullable = null;
                        }
                    }
                    catch
                    {
                    }
                }
                if (this.endDateIndex != -1)
                {
                    try
                    {
                        string str14 = row[this.endDateIndex].ToString().Trim();
                        if (!string.IsNullOrEmpty(str14))
                        {
                            nullable2 = new DateTime?(Convert.ToDateTime(str14));
                        }
                        else
                        {
                            nullable2 = null;
                        }
                    }
                    catch
                    {
                    }
                }
                if (this.noteIndex != -1)
                {
                    str4 = row[this.noteIndex].ToString();
                }
                if (this.constructionPeriodIndex != -1)
                {
                    try
                    {
                        num5 = Convert.ToInt32(row[this.constructionPeriodIndex]);
                    }
                    catch
                    {
                    }
                }
                if (this.featureDescriptionIndex != -1)
                {
                    try
                    {
                        str7 = row[this.featureDescriptionIndex].ToString();
                    }
                    catch
                    {
                    }
                }
                if (this.mainMaterialIndex != -1)
                {
                    try
                    {
                        string str15 = row[this.mainMaterialIndex].ToString();
                        if (!string.IsNullOrEmpty(str15))
                        {
                            num6 = decimal.Parse(str15);
                        }
                        else
                        {
                            num6 = decimal.Parse("0.00");
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + str + ",存在非有效数字类型,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.subMaterialIndex != -1)
                {
                    try
                    {
                        string str16 = row[this.subMaterialIndex].ToString();
                        if (!string.IsNullOrEmpty(str16))
                        {
                            num7 = decimal.Parse(str16);
                        }
                        else
                        {
                            num7 = decimal.Parse("0.00");
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + str + ",存在非有效数字类型,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (this.laborIndex != -1)
                {
                    try
                    {
                        string str17 = row[this.laborIndex].ToString();
                        if (!string.IsNullOrEmpty(str17))
                        {
                            num8 = decimal.Parse(str17);
                        }
                        else
                        {
                            num8 = decimal.Parse("0.00");
                        }
                    }
                    catch
                    {
                        item = "在您的Excel中：编码" + str + ",存在非有效数字类型,请修改！";
                        errors.Add(item);
                        flag = false;
                    }
                }
                if (row.Table.Columns.Contains("序号"))
                {
                    str8 = row["序号"].ToString();
                }
                if (!flag)
                {
                    item = "在您的Excel中：编码" + str + ",存在错误数据导致其子节点不能导入！";
                    errors.Add(item);
                    int num10 = 0;
                    string str18 = orderNumberByLayer[i];
                    for (int j = i + 1; j < orderNumberByLayer.Count; j++)
                    {
                        if (orderNumberByLayer[j].StartsWith(str18))
                        {
                            num10++;
                        }
                    }
                    i += num10;
                }
                else
                {
                    num++;
                    string str19 = Guid.NewGuid().ToString();
                    string str20 = (this.taskId == "") ? null : this.taskId;
                    cn.justwin.Domain.Entities.BudContractTask task = new cn.justwin.Domain.Entities.BudContractTask {
                        TaskId = str19,
                        TaskCode = str,
                        TaskName = str2,
                        Unit = str3,
                        UnitPrice = new decimal?(num3),
                        Quantity = num2,
                        Total = new decimal?(num4),
                        Note = str4,
                        ParentId = str20,
                        PrjId = this.prjId,
                        OrderNumber = null,
                        StartDate = nullable,
                        EndDate = nullable2,
                        InputDate = DateTime.Now,
                        InputUser = inputUser,
                        ConstructionPeriod = new int?(num5),
                        TaskType = "",
                        FeatureDescription = str7,
                        MainMaterial = new decimal?(num6),
                        SubMaterial = new decimal?(num7),
                        Labor = new decimal?(num8)
                    };
                    string str21 = orderNumberByLayer[i].Substring(3);
                    if (string.IsNullOrEmpty(str21))
                    {
                        if (i > this.startIndex)
                        {
                            orderNumber = cn.justwin.Domain.BudContractTask.GetOrderNumber(this.prjId, this.taskId);
                        }
                        task.OrderNumber = orderNumber;
                    }
                    else
                    {
                        task.OrderNumber = orderNumber + str21;
                    }
                    if (i > 0)
                    {
                        int count = list.Count;
                        if (count > 0)
                        {
                            if ((orderNumberByLayer[i].Length - str6.Length) == 3)
                            {
                                task.ParentId = list[count - 1].TaskId;
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
                                    int num13 = list.FindLastIndex(t => t.OrderNumber.Length == (length + differOrderNumber));
                                    task.ParentId = list[num13].ParentId;
                                }
                            }
                        }
                    }
                    str6 = orderNumberByLayer[i];
                    new BudContractTaskService().Add(task);
                    list.Add(task);
                    if (!string.IsNullOrEmpty(str8))
                    {
                        this.AddSerialNoAndTaskId(str8, task.TaskId);
                    }
                    else
                    {
                        this.AddCodeAndTaskId(task.TaskCode, task.TaskId);
                    }
                }
            }
            return num;
        }

        private List<string> GetCodeList()
        {
            List<string> list = new List<string>();
            foreach (DataRow row in this.taskTable.Rows)
            {
                list.Add(row[this.codeIndex].ToString());
            }
            return list;
        }

        private List<int> GetLayers()
        {
            List<int> sameLayer = new List<int>();
            foreach (DataRow row in this.taskTable.Rows)
            {
                try
                {
                    sameLayer.Add(Convert.ToInt32(row[this.rankIndex]));
                }
                catch
                {
                    sameLayer.Clear();
                    sameLayer = this.GetSameLayer(this.taskTable.Rows.Count);
                }
            }
            return sameLayer;
        }

        private List<int> GetLayersByCode(List<string> codeList)
        {
            List<int> list = new List<int>();
            try
            {
                return this.budTaskTypeServices.GetLayerByCode(codeList);
            }
            catch
            {
                return this.GetSameLayer(codeList.Count);
            }
        }

        private List<string> GetOrderNumberByLayer(List<int> layerList)
        {
            List<string> list = new List<string>();
            int minLayer = layerList.Min<int>((System.Func<int, int>) (lay => lay));
            layerList.Max<int>((System.Func<int, int>) (lay => lay));
            layerList.Count<int>(lay => lay == minLayer);
            string prev = string.Empty;
            int num = 0;
            System.Func<string, bool> predicate = null;
            for (int i = num; i < layerList.Count; i++)
            {
                string item = "001";
                if (i > 0)
                {
                    if (layerList[i] > layerList[i - 1])
                    {
                        item = prev + "001";
                    }
                    else if (layerList[i] == layerList[i - 1])
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
                        string str3 = list.GetRange(0, i - 1).Last<string>(predicate);
                        item = this.GetOrderNumberByPrev(str3);
                    }
                }
                prev = item;
                list.Add(item);
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

        protected List<int> GetSameLayer(int count)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= count; i++)
            {
                list.Add(1);
            }
            return list;
        }

        public void ParseTaskTable(IDictionary<string, int> colRelation)
        {
            foreach (KeyValuePair<string, int> pair in colRelation)
            {
                switch (pair.Key)
                {
                    case "TaskCode":
                        this.codeIndex = pair.Value;
                        break;

                    case "TaskName":
                        this.nameIndex = pair.Value;
                        break;

                    case "TaskType":
                        this.taskTypeIndex = pair.Value;
                        break;

                    case "Unit":
                        this.unitIndex = pair.Value;
                        break;

                    case "Quantity":
                        this.quantityIndex = pair.Value;
                        break;

                    case "UnitPrice":
                        this.unitPriceIndex = pair.Value;
                        break;

                    case "StartDate":
                        this.startDateIndex = pair.Value;
                        break;

                    case "EndDate":
                        this.endDateIndex = pair.Value;
                        break;

                    case "Note":
                        this.noteIndex = pair.Value;
                        break;

                    case "InputUser":
                        this.inputUserIndex = pair.Value;
                        break;

                    case "InputDate":
                        this.inputDateIndex = pair.Value;
                        break;

                    case "Total":
                        this.totalIndex = pair.Value;
                        break;

                    case "Rank":
                        this.rankIndex = pair.Value;
                        break;

                    case "ConstructionPeriod":
                        this.constructionPeriodIndex = pair.Value;
                        break;

                    case "FeatureDescription":
                        this.featureDescriptionIndex = pair.Value;
                        break;

                    case "MainMaterial":
                        this.mainMaterialIndex = pair.Value;
                        break;

                    case "SubMaterial":
                        this.subMaterialIndex = pair.Value;
                        break;

                    case "Labor":
                        this.laborIndex = pair.Value;
                        break;
                }
            }
            List<string> codeList = this.GetCodeList();
            this.budTaskTypeServices = new BudTaskTypeServices(codeList);
        }
    }
}

