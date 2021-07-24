namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.pm.util;
    using System;
    using System.Data;

    public class TempWbsAction
    {
        public bool ClearProject(string projectCode)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.NonQuerySqlString((((str + " delete from EPM_Task_Resource where projectCode='" + projectCode + "' ") + " delete from EPM_Task_TaskRelation where projectCode='" + projectCode + "' ") + " delete from EPM_Task_TaskList where projectCode='" + projectCode + "' ") + " end ");
        }

        public bool ImportTask(string projectCode)
        {
            this.ClearProject(projectCode);
            new TempResourceInfoCollection();
            this.QueryImportResource(projectCode);
            string str = "";
            string str2 = " begin ";
            this.QueryImportResource(projectCode);
            DataTable table = this.QueryImportTask(projectCode);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str = table.Rows[i]["TaskCode"].ToString().Substring(0, table.Rows[i]["TaskCode"].ToString().Length - 3);
                object obj2 = (str2 + " insert into EPM_Task_TaskList(ProjectCode,Remark,TaskCode,ParentTaskCode,TaskName,") + " Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost, " + " SynthPrice,StartDate,EndDate,Remark,TaskState,Pivotal,WorkDay,CompleteCount,OrigCode,SumPrice,WbsType,Content,ContractPrice)";
                string str3 = string.Concat(new object[] { obj2, " values('", projectCode.ToString(), "','", table.Rows[i]["Remark"], "','", table.Rows[i]["TaskCode"], "','", str, "'," });
                object obj3 = str3 + "'" + table.Rows[i]["TaskName"].ToString().Replace(",", "，") + "'," + ((table.Rows[i]["Quantity"].ToString().Trim() == "") ? "0" : table.Rows[i]["Quantity"].ToString()) + ",'" + table.Rows[i]["Unit"].ToString() + "',";
                object[] objArray2 = new object[] { obj3, (table.Rows[i]["TaskCode"].ToString().Length / 3).ToString(), ",0,1,0,", (table.Rows[i]["UnitPrice"].ToString().Trim() == "") ? "0" : table.Rows[i]["UnitPrice"].ToString(), ",getdate(),getdate(),'',1,0,1,0,'", table.Rows[i]["TaskCode1"].ToString(), "',", table.Rows[i]["SumPrice"], ",1,'", table.Rows[i]["Content"], "',", (table.Rows[i]["UnitPrice"].ToString().Trim() == "") ? "0" : table.Rows[i]["UnitPrice"].ToString(), ")" };
                string str4 = string.Concat(objArray2);
                str2 = str4 + " update EPM_Task_TaskList set ChildNum = (select count(1) from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and ParentTaskCode='" + str + "' and IsValid=1 and WbsType=1) where ProjectCode = '" + projectCode.ToString() + "' and TaskCode = '" + str + "' and IsValid=1 and WbsType=1";
            }
            object obj4 = str2 + " insert into  EPM_Task_Resource(ProjectCode,TaskCode,VersionCode,RationItem,ResourceCode,Quantity,Wastage,UnitPrice,Fee,Fee1,ResourceStyle,StepCode,WbsType,ResourceName,ResourceUnit,Content) ";
            return publicDbOpClass.NonQuerySqlString(((((string.Concat(new object[] { obj4, " (select '", projectCode, "',b.TaskCode,'", Guid.Empty, "','',a.ResourceCode,a.Quantity,0,a.SourcePrice,a.Fee,a.Fee1," }) + " case a.FeeStyle when   '机械费' then '3' when '材料费' then '2' when '人工费' then '1' when '0' then '0'  end,'',1,a.SourceName,a.SourceUnit,a.Content " + " from TMP_ListSource a left join TMP_TaskList b on a.ProjectCode=b.ProjectCode and a.ListCode=b.TaskCode1 ") + " where a.ProjectCode='" + projectCode + "' and b.TaskCode is not null)") + " delete from TMP_ListSource where ProjectCode='" + projectCode + "' ") + " delete from TMP_TaskList where ProjectCode='" + projectCode + "' ") + " end ");
        }

        public bool ImportTask(string projectCode, int imptype)
        {
            this.ClearProject(projectCode);
            new TempResourceInfoCollection();
            this.QueryImportResource(projectCode);
            string str = "";
            string str2 = " begin ";
            this.QueryImportResource(projectCode);
            DataTable table = this.QueryImportTask(projectCode);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str = table.Rows[i]["TaskCode"].ToString().Substring(0, table.Rows[i]["TaskCode"].ToString().Length - 3);
                object obj2 = (str2 + " insert into EPM_Task_TaskList(ProjectCode,Remark,TaskCode,ParentTaskCode,TaskName,") + " Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost, " + " SynthPrice,StartDate,EndDate,TaskState,Pivotal,WorkDay,CompleteCount,OrigCode,SumPrice,WbsType,Content,ContractPrice)";
                string str3 = string.Concat(new object[] { obj2, " values('", projectCode.ToString(), "','", table.Rows[i]["Remark"].ToString(), "','", table.Rows[i]["TaskCode"], "','", str, "'," });
                object obj3 = str3 + "'" + table.Rows[i]["TaskName"].ToString().Replace(",", "，") + "'," + ((table.Rows[i]["Quantity"].ToString().Trim() == "") ? "0" : table.Rows[i]["Quantity"].ToString()) + ",'" + table.Rows[i]["Unit"].ToString() + "',";
                object[] objArray2 = new object[] { obj3, (table.Rows[i]["TaskCode"].ToString().Length / 3).ToString(), ",0,1,0,", (table.Rows[i]["UnitPrice"].ToString().Trim() == "") ? "0" : table.Rows[i]["UnitPrice"].ToString(), ",getdate(),getdate(),1,0,1,0,'", table.Rows[i]["TaskCode1"].ToString(), "',", table.Rows[i]["SumPrice"], ",1,'", table.Rows[i]["Content"], "',", (table.Rows[i]["UnitPrice"].ToString().Trim() == "") ? "0" : table.Rows[i]["UnitPrice"].ToString(), ") ;" };
                string str4 = string.Concat(objArray2);
                str2 = str4 + " update EPM_Task_TaskList set ChildNum = (select count(1) from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and ParentTaskCode='" + str + "' and IsValid=1 and WbsType=1) where ProjectCode = '" + projectCode.ToString() + "' and TaskCode = '" + str + "' and IsValid=1 and WbsType=1 ;";
            }
            if ((imptype == 1) || (imptype == 3))
            {
                object obj4 = str2 + " insert into  EPM_Task_Resource(ProjectCode,TaskCode,VersionCode,RationItem,ResourceCode,Quantity,Wastage,UnitPrice,Fee,Fee1,ResourceStyle,StepCode,WbsType,ResourceName,ResourceUnit,Content) ";
                str2 = (string.Concat(new object[] { obj4, " (select '", projectCode, "',(SELECT TOP 1 TaskCode FROM TMP_TaskList  WHERE (TaskCode1 = a.ListCode and ProjectCode='", projectCode, "')),'", Guid.Empty, "','',a.ResourceCode,a.Quantity,0,a.SourcePrice,a.Fee,a.Fee1," }) + " case a.FeeStyle when   '机械费' then '3' when '材料费' then '2' when '人工费' then '1' when '0' then '0'  end,'',1,a.SourceName,a.SourceUnit,a.Content " + " from TMP_ListSource a   ") + " where a.ProjectCode='" + projectCode + "' ) ;";
            }
            if (imptype == 2)
            {
                object obj5 = str2 + " insert into  EPM_Task_Resource(ProjectCode,TaskCode,VersionCode,RationItem,ResourceCode,Quantity,Wastage,UnitPrice,Fee,Fee1,ResourceStyle,StepCode,WbsType,ResourceName,ResourceUnit,Content) ";
                str2 = (string.Concat(new object[] { obj5, " (select '", projectCode, "',b.TaskCode,'", Guid.Empty, "','',a.ResourceCode,a.Quantity,0,a.SourcePrice,a.Fee,a.Fee1," }) + " case a.FeeStyle when   '机械费' then '3' when '材料费' then '2' when '人工费' then '1' when '0' then '0'  end,'',1,a.SourceName,a.SourceUnit,a.Content " + " from TMP_ListSource a left join TMP_TaskList b on a.ProjectCode=b.ProjectCode and a.ListCode=b.TaskCode1 ") + " where a.ProjectCode='" + projectCode + "' and b.TaskCode is not null) ;";
            }
            return publicDbOpClass.NonQuerySqlString(((str2 + " delete from TMP_ListSource where ProjectCode='" + projectCode + "' ;") + " delete from TMP_TaskList where ProjectCode='" + projectCode + "' ;") + " end ");
        }

        public DataTable QueryImportResource(string projectCode)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.TaskCode from TMP_ListSource a left join TMP_TaskList b on a.ProjectCode=b.ProjectCode and a.ListCode=b.TaskCode1 where a.projectcode='" + projectCode + "'");
        }

        public DataTable QueryImportTask(string projectCode)
        {
            return publicDbOpClass.DataTableQuary("select ProjectCode,Remark,TaskCode,TaskCode1,replace(replace(TaskName,char(10),''),char(13),'') as TaskName,Unit,Quantity,UnitPrice,SumPrice,Content from TMP_TaskList where projectCode='" + projectCode + "'");
        }

        public bool TempTaskInsert(TempWbsInfoCollection taskList, TempResourceInfoCollection resourceList, string projectCode)
        {
            int num = 1;
            int num2 = 1;
            int num3 = 1;
            string str = "";
            string str2 = "";
            string str3 = "";
            string format = "";
            format = " insert into TMP_TaskList Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}') ";
            string str5 = " begin ";
            str5 = (str5 + " delete from TMP_TaskList where ProjectCode='" + projectCode + "' ") + " delete from TMP_ListSource where ProjectCode='" + projectCode + "'";
            for (int i = 0; i < taskList.Count; i++)
            {
                if (i == 0)
                {
                    str = num.ToString().PadLeft(3, '0');
                    str5 = str5 + string.Format(format, new object[] { projectCode, str, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                    num++;
                    num2 = 1;
                    num3 = 1;
                }
                else if (taskList[i].Unit.Trim() == "")
                {
                    if (VerifyHelper.IsInt32(taskList[i].TaskCode))
                    {
                        str2 = str + num2.ToString().PadLeft(3, '0');
                        str5 = str5 + string.Format(format, new object[] { projectCode, str2, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                        num2++;
                        num3 = 1;
                    }
                    else
                    {
                        str = num.ToString().PadLeft(3, '0');
                        str5 = str5 + string.Format(format, new object[] { projectCode, str, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                        num++;
                        num2 = 1;
                        num3 = 1;
                    }
                }
                else
                {
                    str3 = str2 + num3.ToString().PadLeft(3, '0');
                    str5 = str5 + string.Format(format, new object[] { projectCode, str3, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                    num3++;
                }
            }
            for (int j = 0; j < resourceList.Count; j++)
            {
                string sqlString = "select top 1  ResourceCode,ResourceStyle from EPM_Res_Resource where (ResourceName+specification) = '" + resourceList[j].SourceName.Trim() + " '";
                string str7 = "";
                string str8 = "0";
                DataTable table = publicDbOpClass.DataTableQuary(sqlString);
                if (table.Rows.Count != 0)
                {
                    str7 = table.Rows[0][0].ToString();
                    string str9 = table.Rows[0][1].ToString();
                    if (str9 == null)
                    {
                        goto Label_04C8;
                    }
                    if (!(str9 == "1"))
                    {
                        if (str9 == "2")
                        {
                            goto Label_04B6;
                        }
                        if (str9 == "3")
                        {
                            goto Label_04BF;
                        }
                        goto Label_04C8;
                    }
                    str8 = "人工费";
                }
                goto Label_04CF;
            Label_04B6:
                str8 = "材料费";
                goto Label_04CF;
            Label_04BF:
                str8 = "机械费";
                goto Label_04CF;
            Label_04C8:
                str8 = "0";
            Label_04CF:
                format = " insert into TMP_ListSource values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                str5 = str5 + string.Format(format, new object[] { projectCode, resourceList[j].ListCode, resourceList[j].ListName, str8, resourceList[j].SourceName, resourceList[j].SourceUnit, resourceList[j].SourcePrice, resourceList[j].Quantity, resourceList[j].Fee, resourceList[j].Fee1, str7, resourceList[j].Content });
            }
            return publicDbOpClass.NonQuerySqlString(str5 + " end ");
        }

        public bool TempTaskInsert(TempWbsInfoCollection taskList, TempResourceInfoCollection resourceList, string projectCode, string Type)
        {
            int num = 1;
            int num2 = 1;
            int num3 = 1;
            string str = "";
            string str2 = "";
            string str3 = "";
            string format = "";
            string sqlString = "";
            format = " insert into TMP_TaskList Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}') ";
            string str6 = " begin ";
            str6 = (str6 + " delete from TMP_TaskList where ProjectCode='" + projectCode + "' ") + " delete from TMP_ListSource where ProjectCode='" + projectCode + "'";
            for (int i = 0; i < taskList.Count; i++)
            {
                if (i == 0)
                {
                    str = num.ToString().PadLeft(3, '0');
                    str6 = str6 + string.Format(format, new object[] { projectCode, str, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                    num++;
                    num2 = 1;
                    num3 = 1;
                }
                else if (taskList[i].Unit.Trim() == "")
                {
                    if (VerifyHelper.IsInt32(taskList[i].TaskCode))
                    {
                        str2 = str + num2.ToString().PadLeft(3, '0');
                        str6 = str6 + string.Format(format, new object[] { projectCode, str2, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                        num2++;
                        num3 = 1;
                    }
                    else
                    {
                        str = num.ToString().PadLeft(3, '0');
                        str6 = str6 + string.Format(format, new object[] { projectCode, str, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                        num++;
                        num2 = 1;
                        num3 = 1;
                    }
                }
                else
                {
                    str3 = str2 + num3.ToString().PadLeft(3, '0');
                    str6 = str6 + string.Format(format, new object[] { projectCode, str3, taskList[i].TaskCode, taskList[i].TaskName, taskList[i].Unit, taskList[i].Quantity, taskList[i].UnitPrice, taskList[i].SumPrice, taskList[i].Content, taskList[i].Remark });
                    num3++;
                }
            }
            for (int j = 0; j < resourceList.Count; j++)
            {
                if (Type == "1")
                {
                    sqlString = "select top 1  ResourceCode,ResourceStyle from EPM_Res_Resource where (ResourceName+specification) = '" + resourceList[j].SourceName.Trim() + " '";
                }
                else if (Type == "3")
                {
                    sqlString = "select top 1  ResourceCode,ResourceStyle from EPM_Res_Resource where ResourceName = '" + resourceList[j].SourceName.Trim() + " '";
                }
                string str7 = "";
                string str8 = "0";
                DataTable table = publicDbOpClass.DataTableQuary(sqlString);
                if (table.Rows.Count != 0)
                {
                    str7 = table.Rows[0][0].ToString();
                    string str9 = table.Rows[0][1].ToString();
                    if (str9 == null)
                    {
                        goto Label_0510;
                    }
                    if (!(str9 == "1"))
                    {
                        if (str9 == "2")
                        {
                            goto Label_04FE;
                        }
                        if (str9 == "3")
                        {
                            goto Label_0507;
                        }
                        goto Label_0510;
                    }
                    str8 = "人工费";
                }
                goto Label_0517;
            Label_04FE:
                str8 = "材料费";
                goto Label_0517;
            Label_0507:
                str8 = "机械费";
                goto Label_0517;
            Label_0510:
                str8 = "0";
            Label_0517:
                format = " insert into TMP_ListSource values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                str6 = str6 + string.Format(format, new object[] { projectCode, resourceList[j].ListCode, resourceList[j].ListName, str8, resourceList[j].SourceName, resourceList[j].SourceUnit, resourceList[j].SourcePrice, resourceList[j].Quantity, resourceList[j].Fee, resourceList[j].Fee1, str7, resourceList[j].Content });
            }
            return publicDbOpClass.NonQuerySqlString(str6 + " end ");
        }
    }
}

