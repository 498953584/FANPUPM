namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Excel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public class BudTaskResourceServices
    {
        public int amountIndex = -1;
        public int brandIndex = -1;
        public int codeIndex = -1;
        public DataTable dtResource;
        public int lossCoefficientIndex = -1;
        public int ModelNumberIndex = -1;
        public int nameIndex = -1;
        public int noteIndex = -1;
        public int quantityIndex = -1;
        public int resourceTypeIndex = -1;
        public int serialNoIndex = -1;
        public int specificationIndex = -1;
        public int taskCodeIndex = -1;
        public int technicalParameterIndex = -1;
        public int unitIndex = -1;
        public int unitPriceIndex = -1;
        public string UserCode;

        public BudTaskResourceServices(DataTable dt, string userCode)
        {
            this.dtResource = dt;
            this.UserCode = userCode;
        }

        public void AddConResource(string[] colArray, string inputUser, List<string> errors, string prjId)
        {
            IDictionary<string, int> relation = ExcelUtility.GetRelation(colArray);
            this.ParseResourceTable(relation);
            string name = string.Empty;
            string brand = string.Empty;
            string specification = string.Empty;
            string modelNumber = string.Empty;
            string str5 = string.Empty;
            decimal resPrice = 0M;
            decimal resQuantity = 0M;
            string str6 = string.Empty;
            string item = string.Empty;
            string str8 = string.Empty;
            foreach (DataRow row in this.dtResource.Rows)
            {
                name = (row[this.nameIndex] == null) ? null : row[this.nameIndex].ToString();
                if (this.taskCodeIndex != -1)
                {
                    str6 = (row[this.taskCodeIndex] == null) ? null : row[this.taskCodeIndex].ToString().Trim();
                }
                if (this.serialNoIndex != -1)
                {
                    str8 = (row[this.serialNoIndex] == null) ? null : row[this.serialNoIndex].ToString().Trim();
                }
                if (this.brandIndex != -1)
                {
                    brand = row[this.brandIndex].ToString();
                }
                if (this.specificationIndex != -1)
                {
                    specification = row[this.specificationIndex].ToString().Trim();
                }
                if (this.ModelNumberIndex != -1)
                {
                    modelNumber = row[this.ModelNumberIndex].ToString().Trim();
                }
                if (this.technicalParameterIndex != -1)
                {
                    row[this.technicalParameterIndex].ToString().Trim();
                }
                if ((this.unitIndex != -1) && (row[this.unitIndex].ToString().Trim().Length == 0))
                {
                    str5 = null;
                }
                if (this.unitPriceIndex != -1)
                {
                    try
                    {
                        resPrice = decimal.Parse(row[this.unitPriceIndex].ToString());
                    }
                    catch
                    {
                        if (!string.IsNullOrEmpty(str6))
                        {
                            item = "在您的Excel中：任务编码" + str6 + ",存在非有效数字,请修改！";
                        }
                        else
                        {
                            item = "在您的Excel中：序号" + str8 + ",存在非有效数字,请修改！";
                        }
                        errors.Add(item);
                    }
                }
                if (this.quantityIndex != -1)
                {
                    try
                    {
                        resQuantity = decimal.Parse(row[this.quantityIndex].ToString());
                    }
                    catch
                    {
                        if (!string.IsNullOrEmpty(str6))
                        {
                            item = "在您的Excel中：任务编码" + str6 + ",存在非有效数字,请修改！";
                        }
                        else
                        {
                            item = "在您的Excel中：序号" + str8 + ",存在非有效数字,请修改！";
                        }
                        errors.Add(item);
                    }
                }
                if (this.noteIndex != -1)
                {
                    row[this.noteIndex].ToString().Trim();
                }
                if (this.codeIndex != -1)
                {
                    row[this.codeIndex].ToString();
                }
                if (this.amountIndex != -1)
                {
                    try
                    {
                        new decimal?(decimal.Parse(row[this.amountIndex].ToString()));
                    }
                    catch
                    {
                        if (!string.IsNullOrEmpty(str6))
                        {
                            item = "在您的Excel中：任务编码" + str6 + ",存在非有效数字,请修改！";
                        }
                        else
                        {
                            item = "在您的Excel中：序号" + str8 + ",存在非有效数字,请修改！";
                        }
                        errors.Add(item);
                    }
                }
                string str9 = Resource.GetResourceId(name, specification, modelNumber, brand);
                string taskId = string.Empty;
                if (string.IsNullOrEmpty(str9))
                {
                    if (!string.IsNullOrEmpty(str6))
                    {
                        item = "在您的Excel中：任务编码" + str6 + ",在资源库中不存在,请修改！";
                    }
                    else
                    {
                        item = "在您的Excel中：序号" + str8 + ",在资源库中不存在,请修改！";
                    }
                    errors.Add(item);
                    try
                    {
                        if (!string.IsNullOrEmpty(str6))
                        {
                            taskId = this.GetRelationTaskId(str6);
                        }
                        if (!string.IsNullOrEmpty(str8))
                        {
                            taskId = this.GetRelationTaskId(str8);
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(str6))
                        {
                            taskId = this.GetRelationTaskId(str6);
                        }
                        if (!string.IsNullOrEmpty(str8))
                        {
                            taskId = this.GetRelationTaskId(str8);
                        }
                        if (!cn.justwin.Domain.BudContractTask.IsLeaf(taskId))
                        {
                            if (!string.IsNullOrEmpty(str6))
                            {
                                item = "在您的Excel中：任务编码" + str6 + ",不允许配置资源,请修改！";
                            }
                            else
                            {
                                item = "在您的Excel中：序号" + str8 + ",不允许配置资源,请修改！";
                            }
                            errors.Add(item);
                        }
                        else if (!string.IsNullOrEmpty(taskId))
                        {
                            decimal? repeatResQuantity = cn.justwin.Domain.BudContractResource.GetRepeatResQuantity(str9, taskId);
                            if (!repeatResQuantity.HasValue)
                            {
                                cn.justwin.Domain.BudContractResource.Add(cn.justwin.Domain.BudContractResource.Create(Guid.NewGuid().ToString(), taskId, str9, resPrice, resQuantity, inputUser, DateTime.Now));
                            }
                            else
                            {
                                this.MergeResQuantity(taskId, str9, repeatResQuantity, resQuantity);
                            }
                        }
                    }
                    catch
                    {
                        if (!string.IsNullOrEmpty(str6))
                        {
                            item = "在您的Excel中：任务编码" + str6 + ",在WBS信息中的不存在,请修改！";
                        }
                        else
                        {
                            item = "在您的Excel中：序号" + str8 + ",在WBS信息中的不存在,请修改！";
                        }
                        errors.Add(item);
                    }
                }
            }
            BudTaskServices.codeAndTaskId.Clear();
        }

        public void AddResource(string[] colArray, string inputUser, List<string> errors, string prjId, string isWBSRelevance, char sep)
        {
            IDictionary<string, int> relation = ExcelUtility.GetRelation(colArray);
            this.ParseResourceTable(relation);
            string name = string.Empty;
            string resourceCode = string.Empty;
            string brand = string.Empty;
            string specification = string.Empty;
            string modelNumber = string.Empty;
            string str6 = string.Empty;
            decimal? unitPrice = 0M;
            decimal? quanity = 0M;
            decimal? amount = 0M;
            string str7 = string.Empty;
            string note = string.Empty;
            string item = string.Empty;
            decimal? nullable4 = 0M;
            string str10 = string.Empty;
            foreach (DataRow row in this.dtResource.Rows)
            {
                Action<object> action2 = null;
                if (this.serialNoIndex != -1)
                {
                    str10 = (row[this.serialNoIndex] == null) ? null : row[this.serialNoIndex].ToString().Trim();
                }
                name = (row[this.nameIndex] == null) ? null : row[this.nameIndex].ToString();
                if ((isWBSRelevance == "1") && (this.taskCodeIndex != -1))
                {
                    str7 = (row[this.taskCodeIndex] == null) ? null : row[this.taskCodeIndex].ToString().Trim();
                }
                if (this.brandIndex != -1)
                {
                    brand = row[this.brandIndex].ToString();
                }
                if (this.specificationIndex != -1)
                {
                    specification = row[this.specificationIndex].ToString().Trim();
                }
                if (this.ModelNumberIndex != -1)
                {
                    modelNumber = row[this.ModelNumberIndex].ToString().Trim();
                }
                if (this.technicalParameterIndex != -1)
                {
                    row[this.technicalParameterIndex].ToString().Trim();
                }
                if ((this.unitIndex != -1) && (row[this.unitIndex].ToString().Trim().Length == 0))
                {
                    str6 = null;
                }
                if (this.unitPriceIndex != -1)
                {
                    try
                    {
                        unitPrice = new decimal?(decimal.Parse(row[this.unitPriceIndex].ToString()));
                    }
                    catch
                    {
                        item = "在您的Excel中：资源名称为" + name + "的资源,单价不是非有效数字,请修改。";
                        errors.Add(item);
                        return;
                    }
                }
                if (this.quantityIndex != -1)
                {
                    try
                    {
                        quanity = new decimal?(decimal.Parse(row[this.quantityIndex].ToString()));
                    }
                    catch
                    {
                        item = "在您的Excel中：资源名称为" + name + "的资源,数量不是有效数字,请修改。";
                        errors.Add(item);
                        return;
                    }
                }
                if (this.noteIndex != -1)
                {
                    note = row[this.noteIndex].ToString().Trim();
                }
                if (this.codeIndex != -1)
                {
                    resourceCode = row[this.codeIndex].ToString();
                }
                if (this.amountIndex != -1)
                {
                    try
                    {
                        amount = new decimal?(decimal.Parse(row[this.amountIndex].ToString()));
                    }
                    catch
                    {
                        item = "在您的Excel中：资源名称为" + name + "的资源,合计金额不是有效数字,请修改。";
                        errors.Add(item);
                        return;
                    }
                }
                if (this.lossCoefficientIndex != -1)
                {
                    try
                    {
                        nullable4 = new decimal?(decimal.Parse(row[this.lossCoefficientIndex].ToString()));
                    }
                    catch
                    {
                        nullable4 = 1;
                    }
                }
                string str11 = new ResResourceService().GetId(name, brand, specification, modelNumber, note, sep);
                if (string.IsNullOrEmpty(str11))
                {
                    item = "在您的Excel中：资源名称" + name + "，在资源库中不存在。";
                    errors.Add(item);
                }
                string taskId = string.Empty;
                if (!string.IsNullOrEmpty(str7))
                {
                    taskId = this.GetRelationTaskId(str7);
                }
                if (!string.IsNullOrEmpty(str10))
                {
                    taskId = this.GetRelationTaskId(str10);
                }
                if (string.IsNullOrEmpty(taskId) && (isWBSRelevance == "1"))
                {
                    item = "在您的Excel中：资源名称" + name + "，关联的分布分项不存在。";
                    errors.Add(item);
                }
                if (cn.justwin.Domain.BudTask.CheckChilds(taskId))
                {
                    item = "在您的Excel中：资源名称" + name + "，不能配置到有子项的分部分项。";
                    errors.Add(item);
                    taskId = string.Empty;
                }
                if (isWBSRelevance == "1")
                {
                    if (!string.IsNullOrEmpty(taskId) && !string.IsNullOrEmpty(str11))
                    {
                        decimal? repeatResQuantity = TaskResource.GetRepeatResQuantity(taskId, str11);
                        if (repeatResQuantity.HasValue)
                        {
                            decimal? nullable7 = quanity;
                            this.MergeResQuantity(taskId, str11, repeatResQuantity.Value, nullable7.HasValue ? nullable7.GetValueOrDefault() : 0M);
                        }
                        else if (cn.justwin.Domain.BudTask.GetById(taskId) != null)
                        {
                            BudTaskResource resource = new BudTaskResource {
                                TaskResourceId = Guid.NewGuid().ToString(),
                                TaskId = taskId,
                                ResourceId = str11,
                                ResourceQuantity = new decimal?(quanity.Value),
                                InputUser = this.UserCode,
                                InputDate = DateTime.Now,
                                ResourcePrice = new decimal?(unitPrice.Value),
                                PrjGuid = prjId,
                                Versions = 1,
                                LossCoefficient = new decimal?(nullable4.Value)
                            };
                            new BudTaskResourceService().Add(resource);
                        }
                        if (action2 == null)
                        {
                            action2 = obj => new BudTaskService().UpdateTotal2(taskId);
                        }
                        Action<object> action = action2;
                        new Task(action, "").Start();
                    }
                    else
                    {
                        ResourceTemp.Add(ResourceTemp.Create(str11, taskId, resourceCode, name, unitPrice, quanity, amount, prjId));
                    }
                }
                else if (!string.IsNullOrEmpty(str11))
                {
                    decimal? resQuantity = TaskResource.GetResQuantity(prjId, str11);
                    if (resQuantity.HasValue)
                    {
                        decimal? nullable8 = quanity;
                        this.MergeResQuantityByPrjId(prjId, str11, resQuantity.Value, nullable8.HasValue ? nullable8.GetValueOrDefault() : 0M);
                    }
                    else
                    {
                        Resource byId = Resource.GetById(str11);
                        TaskResource taskResource = new TaskResource {
                            TaskReourceId = Guid.NewGuid().ToString(),
                            Resource = byId,
                            PrjGuid = prjId,
                            Price = unitPrice.Value,
                            Quantity = quanity.Value,
                            InputDate = new DateTime?(DateTime.Now),
                            InputUser = this.UserCode,
                            LossCoefficient = nullable4
                        };
                        TaskResource.AddResource(taskResource);
                    }
                }
                else
                {
                    ResourceTemp.Add(ResourceTemp.Create(str11, null, resourceCode, name, unitPrice, quanity, amount, prjId));
                }
            }
            BudTaskServices.codeAndTaskId.Clear();
        }

        private List<string> GetCodeList()
        {
            List<string> list = new List<string>();
            foreach (DataRow row in this.dtResource.Rows)
            {
                list.Add(row[this.taskCodeIndex].ToString());
            }
            return list;
        }

        public string GetRelationTaskId(string code)
        {
            return BudTaskServices.codeAndTaskId[code];
        }

        private void MergeResQuantity(string taskId, string resId, decimal quantityDB, decimal newQuantity)
        {
            if (newQuantity > 0M)
            {
                decimal quantity = decimal.Add(quantityDB, newQuantity);
                TaskResource.UpdateResQuantity(taskId, resId, quantity);
            }
        }

        private void MergeResQuantity(string taskId, string resId, decimal? quantityDB, decimal newQuantity)
        {
            if (newQuantity > 0M)
            {
                decimal? nullable = quantityDB;
                decimal quantity = decimal.Add(nullable.HasValue ? nullable.GetValueOrDefault() : 0M, newQuantity);
                cn.justwin.Domain.BudContractResource.UpdateResQuantity(taskId, resId, quantity);
            }
        }

        private void MergeResQuantityByPrjId(string prjId, string resId, decimal quantityDB, decimal newQuantity)
        {
            if (newQuantity > 0M)
            {
                decimal quantity = decimal.Add(quantityDB, newQuantity);
                TaskResource.UpdateResQuantityByPrjId(prjId, resId, quantity);
            }
        }

        private void ParseResourceTable(IDictionary<string, int> colRelation)
        {
            foreach (KeyValuePair<string, int> pair in colRelation)
            {
                switch (pair.Key)
                {
                    case "ResourceCode":
                        this.codeIndex = pair.Value;
                        break;

                    case "ResourceName":
                        this.nameIndex = pair.Value;
                        break;

                    case "Brand":
                        this.brandIndex = pair.Value;
                        break;

                    case "Specification":
                        this.specificationIndex = pair.Value;
                        break;

                    case "ModelNumber":
                        this.ModelNumberIndex = pair.Value;
                        break;

                    case "Unit":
                        this.unitIndex = pair.Value;
                        break;

                    case "UnitPrice":
                        this.unitPriceIndex = pair.Value;
                        break;

                    case "Quantity":
                        this.quantityIndex = pair.Value;
                        break;

                    case "TaskCode":
                        this.taskCodeIndex = pair.Value;
                        break;

                    case "Note":
                        this.noteIndex = pair.Value;
                        break;

                    case "Amount":
                        this.amountIndex = pair.Value;
                        break;

                    case "TechnicalParameter":
                        this.technicalParameterIndex = pair.Value;
                        break;

                    case "LossCoefficient":
                        this.lossCoefficientIndex = pair.Value;
                        break;

                    case "SerialNo":
                        this.serialNoIndex = pair.Value;
                        break;
                }
            }
        }
    }
}

