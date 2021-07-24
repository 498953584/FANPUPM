namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class FlowChartAction
    {
        public static bool CreateCurveChart(int rows, int xpos, int ypos, string frontNodeString, int nodeID, int templateID, string nodeName, string condition, string types)
        {
            string[] strArray = new string[3];
            Hashtable[] hashtableArray = new Hashtable[3];
            int index = 0;
            for (int i = (2 * rows) + 1; i > ((2 * (rows - 1)) + 1); i--)
            {
                index++;
                hashtableArray[index] = new Hashtable();
                hashtableArray[index].Add("TemplateID", templateID.ToString());
                hashtableArray[index].Add("RowNum", i.ToString());
                if (index == 1)
                {
                    for (int j = xpos; j < (xpos + 4); j++)
                    {
                        if (j == (xpos + 2))
                        {
                            strArray[index] = "2";
                        }
                        else if (j == (xpos + 1))
                        {
                            if ((condition != "") && (condition != null))
                            {
                                strArray[index] = condition.ToString();
                            }
                            else
                            {
                                strArray[index] = "7";
                            }
                        }
                        else if (j == xpos)
                        {
                            strArray[index] = "10";
                        }
                        else
                        {
                            strArray[index] = "3" + types.ToString() + ";" + nodeID.ToString() + ";" + nodeName.ToString() + ";" + frontNodeString.ToString();
                        }
                        hashtableArray[index].Add("Column" + j.ToString(), SqlStringConstructor.GetQuotedString(strArray[index].ToString()));
                    }
                }
                else
                {
                    hashtableArray[index].Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString("8"));
                }
                if (!FlowTemplateAction.AddFlowChart(hashtableArray[index]))
                {
                    return false;
                }
            }
            if (rows > 1)
            {
                Hashtable flowChartInfo = new Hashtable();
                string where = "";
                int num4 = (2 * (rows - 1)) + 1;
                flowChartInfo.Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString("9"));
                where = " where TemplateID=" + templateID.ToString() + " and RowNum = " + num4.ToString();
                if (!FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CreateFlowChart(int xpos, int ypos, string frontNodeString, int nodeID, int templateID, string nodeName, string condition, string types)
        {
            string pStr = "";
            string where = "";
            DataTable table = FlowTemplateAction.QueryNodeInfo(templateID, nodeID, frontNodeString);
            if (table.Rows.Count > 0)
            {
                CreateCurveChart(table.Rows.Count, xpos, ypos, frontNodeString, nodeID, templateID, nodeName, condition, types);
            }
            else
            {
                Hashtable flowChartInfo = new Hashtable();
                for (int i = xpos + 1; i < (xpos + 4); i++)
                {
                    if (i == (xpos + 2))
                    {
                        pStr = "2";
                    }
                    else if (i == (xpos + 1))
                    {
                        if ((condition != "") && (condition != null))
                        {
                            pStr = condition.ToString();
                        }
                        else
                        {
                            pStr = "7";
                        }
                    }
                    else
                    {
                        pStr = "3" + types.ToString() + ";" + nodeID.ToString() + ";" + nodeName.ToString() + ";" + frontNodeString.ToString();
                    }
                    flowChartInfo.Add("Column" + i.ToString(), SqlStringConstructor.GetQuotedString(pStr));
                }
                where = " where TemplateID=" + templateID.ToString() + " and RowNum = " + ypos.ToString();
                if (!FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CreateNewFlowChart(int xpos, int ypos, string frontNodeString, int nodeID, int templateID, string nodeName, string types)
        {
            DataTable table = publicDbOpClass.DataTableQuary(" SELECT * FROM WF_FlowChart WHERE TemplateID=" + templateID + " ");
            string str = table.Rows[0][xpos + 4].ToString();
            int num = str.LastIndexOf(";");
            str = str.Substring(0, num + 1) + nodeID;
            int num2 = 0;
            for (int i = xpos; i <= 60; i++)
            {
                if (string.IsNullOrEmpty(table.Rows[0][i].ToString()))
                {
                    num2 = i - 2;
                    break;
                }
            }
            table.Rows[0]["Column" + num2].ToString();
            string str2 = "3" + types.ToString() + ";" + nodeID.ToString() + ";" + nodeName.ToString() + ";" + frontNodeString.ToString();
            string str3 = "begin ";
            if (num2 == xpos)
            {
                object obj2 = str3;
                object[] objArray = new object[] { obj2, " UPDATE WF_FlowChart SET Column", (xpos + 1).ToString(), "='7',Column", (xpos + 2).ToString(), "='2',Column", (xpos + 3).ToString(), "='", str2, "' where TemplateID=", templateID, " " };
                str3 = string.Concat(objArray);
            }
            else if ((num2 == (xpos + 3)) && (table.Rows[0][num2 + 1].ToString() == "4;00;结束"))
            {
                object obj3 = str3;
                object[] objArray2 = new object[] { obj3, " UPDATE WF_FlowChart SET Column", num2, "='", str2, "',Column", (num2 + 1).ToString(), "='7',Column", (num2 + 2).ToString(), "='2',Column", (num2 + 3).ToString(), "='4;00;结束'  where TemplateID=", templateID, " " };
                str3 = string.Concat(objArray2);
            }
            else if (table.Rows[0][num2 + 1].ToString() == "4;00;结束")
            {
                if (num2 == (xpos + 6))
                {
                    object obj4 = str3;
                    object[] objArray3 = new object[] { 
                        obj4, " UPDATE WF_FlowChart SET Column", (xpos + 3).ToString(), "='", str2, "',Column", (xpos + 6).ToString(), "='", str, "', Column", (xpos + 7).ToString(), "='7',Column", (xpos + 8).ToString(), "='2',Column", (xpos + 9).ToString(), "='4;00;结束'  where TemplateID=", 
                        templateID, " "
                     };
                    str3 = string.Concat(objArray3);
                }
                else
                {
                    object obj5 = str3;
                    object[] objArray4 = new object[] { obj5, " UPDATE WF_FlowChart SET Column", (xpos + 3).ToString(), "='", str2, "',Column", (xpos + 6).ToString(), "='", str, "' where TemplateID=", templateID, " " };
                    str3 = string.Concat(objArray4);
                    for (int j = xpos + 9; j <= num2; j += 3)
                    {
                        object obj6 = str3;
                        object[] objArray5 = new object[8];
                        objArray5[0] = obj6;
                        objArray5[1] = " UPDATE WF_FlowChart SET Column";
                        objArray5[2] = j;
                        objArray5[3] = "='";
                        int num19 = j - 3;
                        objArray5[4] = table.Rows[0]["Column" + num19.ToString()].ToString();
                        objArray5[5] = "' where TemplateID=";
                        objArray5[6] = templateID;
                        objArray5[7] = " ";
                        str3 = string.Concat(objArray5);
                    }
                    object obj7 = str3;
                    object[] objArray6 = new object[10];
                    objArray6[0] = obj7;
                    objArray6[1] = " UPDATE WF_FlowChart SET Column";
                    objArray6[2] = (num2 + 1).ToString();
                    objArray6[3] = "='7',Column";
                    objArray6[4] = (num2 + 2).ToString();
                    objArray6[5] = "='2',Column";
                    objArray6[6] = (num2 + 3).ToString();
                    objArray6[7] = "='4;00;结束' where TemplateID=";
                    objArray6[8] = templateID;
                    objArray6[9] = " ";
                    str3 = string.Concat(objArray6);
                }
            }
            else if (num2 == (xpos + 3))
            {
                object obj8 = str3;
                object[] objArray7 = new object[] { obj8, " UPDATE WF_FlowChart SET Column", num2, "='", str2, "',Column", (num2 + 1).ToString(), "='7',Column", (num2 + 2).ToString(), "='2',Column", (num2 + 3).ToString(), "='", str, "' where TemplateID=", templateID, " " };
                str3 = string.Concat(objArray7);
            }
            else
            {
                object obj9 = str3;
                object[] objArray8 = new object[] { obj9, " UPDATE WF_FlowChart SET Column", (xpos + 3).ToString(), "='", str2, "',Column", (xpos + 6).ToString(), "='", str, "' where TemplateID=", templateID, " " };
                str3 = string.Concat(objArray8);
                for (int k = xpos + 9; k <= num2; k += 3)
                {
                    object obj10 = str3;
                    object[] objArray9 = new object[8];
                    objArray9[0] = obj10;
                    objArray9[1] = " UPDATE WF_FlowChart SET Column";
                    objArray9[2] = k;
                    objArray9[3] = "='";
                    int num28 = k - 3;
                    objArray9[4] = table.Rows[0]["Column" + num28.ToString()].ToString();
                    objArray9[5] = "' where TemplateID=";
                    objArray9[6] = templateID;
                    objArray9[7] = " ";
                    str3 = string.Concat(objArray9);
                }
                object obj11 = str3;
                object[] objArray10 = new object[12];
                objArray10[0] = obj11;
                objArray10[1] = " UPDATE WF_FlowChart SET Column";
                objArray10[2] = (num2 + 1).ToString();
                objArray10[3] = "='7',Column";
                objArray10[4] = (num2 + 2).ToString();
                objArray10[5] = "='2',Column";
                objArray10[6] = (num2 + 3).ToString();
                objArray10[7] = "='";
                objArray10[8] = table.Rows[0]["Column" + num2].ToString();
                objArray10[9] = "' where TemplateID=";
                objArray10[10] = templateID;
                objArray10[11] = " ";
                str3 = string.Concat(objArray10);
            }
            return (publicDbOpClass.ExecuteSQL(str3 + " end ") > 0);
        }

        public static bool CreatePoolNode(string pos, string frontNode, int nodeID, int templateID, string nodeName, string condition, string flag, string types)
        {
            string str = "";
            int num = 0;
            int index = 0;
            str = pos;
            index = str.IndexOf(",");
            while (index > 0)
            {
                index = str.Substring(index + 1, (str.Length - index) - 1).IndexOf(",");
                num++;
            }
            int[] numArray = new int[num];
            int[] numArray2 = new int[num];
            int length = 0;
            int num4 = 0;
            string str2 = "";
            index = pos.IndexOf(",");
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            while (index > 0)
            {
                int num8 = 0;
                str2 = pos;
                length = str2.IndexOf(";");
                while (length > 0)
                {
                    string str3 = "";
                    str3 = str2.Substring(0, length);
                    switch (num8)
                    {
                        case 0:
                            numArray[num4] = Convert.ToInt32(str3);
                            break;

                        case 1:
                            numArray2[num4] = Convert.ToInt32(str3);
                            break;
                    }
                    length = str2.Substring(length + 1, (str2.Length - length) - 1).IndexOf(";");
                    num8++;
                }
                pos = pos.Substring(index + 1, (pos.Length - index) - 1);
                index = pos.IndexOf(",");
                num4++;
            }
            num5 = getBigValue(numArray);
            num6 = getSmaillValue(numArray2);
            num7 = getBigValue(numArray2);
            if (flag == "Mod")
            {
                int num9 = 0;
                for (int j = 0; j < num; j++)
                {
                    if (numArray2[j] == 1)
                    {
                        num9 = numArray[j];
                    }
                }
                if (num5 > num9)
                {
                    string where = " where TemplateID=" + templateID.ToString() + " and RowNum = 1";
                    Hashtable flowChartInfo = new Hashtable();
                    DataRow row = FlowTemplateAction.QueryTopFlowChart(templateID, 1).Rows[0];
                    for (int k = num9 + 1; k < 0x3d; k++)
                    {
                        if ((row["Column" + k.ToString()] != null) && (row["Column" + k.ToString()].ToString() != ""))
                        {
                            flowChartInfo.Add("Column" + ((((num5 - num9) + 3) + k)).ToString(), SqlStringConstructor.GetQuotedString(row["Column" + k.ToString()].ToString()));
                        }
                    }
                    if (!FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
                    {
                        return false;
                    }
                }
            }
            string[] strArray = new string[(num7 - num6) + 2];
            Hashtable[] hashtableArray = new Hashtable[(num7 - num6) + 2];
            int num12 = 0;
            for (int i = num6; i < (num7 + 1); i++)
            {
                string str5 = "";
                num12++;
                str5 = " where TemplateID=" + templateID.ToString() + " and RowNum = " + i.ToString();
                hashtableArray[num12] = new Hashtable();
                if ((i % 2) == 1)
                {
                    for (int m = 0; m < num; m++)
                    {
                        if (i == numArray2[m])
                        {
                            for (int n = numArray[m] + 1; n < (num5 + 4); n++)
                            {
                                if (n == (num5 + 3))
                                {
                                    if (i == num6)
                                    {
                                        if (nodeName.ToString() == "结束")
                                        {
                                            strArray[num12] = "4;00;结束";
                                        }
                                        else
                                        {
                                            strArray[num12] = "3" + types.ToString() + ";" + nodeID.ToString() + ";" + nodeName.ToString() + ";" + frontNode.ToString();
                                        }
                                    }
                                    else if (i == num7)
                                    {
                                        strArray[num12] = "12";
                                    }
                                    else
                                    {
                                        strArray[num12] = "11";
                                    }
                                }
                                else if (n == (num5 + 2))
                                {
                                    if (i == num6)
                                    {
                                        strArray[num12] = "2";
                                    }
                                    else
                                    {
                                        strArray[num12] = "7";
                                    }
                                }
                                else
                                {
                                    strArray[num12] = "7";
                                }
                                hashtableArray[num12].Add("Column" + n.ToString(), SqlStringConstructor.GetQuotedString(strArray[num12].ToString()));
                                if (!FlowTemplateAction.UpdFlowChart(hashtableArray[num12], str5))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (i == (num6 + 1))
                    {
                        strArray[num12] = "6";
                    }
                    else
                    {
                        strArray[num12] = "8";
                    }
                    int num17 = num5 + 3;
                    hashtableArray[num12].Add("Column" + num17.ToString(), SqlStringConstructor.GetQuotedString(strArray[num12].ToString()));
                    if (!FlowTemplateAction.UpdFlowChart(hashtableArray[num12], str5))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool DelBeginEndNode(int xpos, int ypos, int templateID, int nodeID, string frontNodeStr, DataRow dr)
        {
            string pStr = "";
            string where = "";
            Hashtable[] hashtableArray = new Hashtable[2];
            int num2 = xpos + 3;
            string str3 = dr["Column" + num2.ToString()].ToString();
            int length = 0;
            bool flag = IsPoolNode(templateID, nodeID);
            length = str3.LastIndexOf(";");
            if (length > 0)
            {
                pStr = str3.Substring(0, length) + ";" + frontNodeStr;
            }
            else
            {
                pStr = str3;
            }
            hashtableArray[0] = new Hashtable();
            int num3 = xpos - 2;
            hashtableArray[0].Add("Column" + num3.ToString(), SqlStringConstructor.GetQuotedString(""));
            int num4 = xpos - 1;
            hashtableArray[0].Add("Column" + num4.ToString(), SqlStringConstructor.GetQuotedString(""));
            if (flag)
            {
                hashtableArray[0].Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString("13"));
            }
            else
            {
                hashtableArray[0].Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString(""));
            }
            int num5 = xpos + 3;
            hashtableArray[0].Add("Column" + num5.ToString(), SqlStringConstructor.GetQuotedString(pStr));
            where = " where TemplateID = " + templateID.ToString() + " and RowNum = " + ypos.ToString();
            if (!FlowTemplateAction.UpdFlowChart(hashtableArray[0], where))
            {
                return false;
            }
            if (flag)
            {
                hashtableArray[1] = new Hashtable();
                hashtableArray[1].Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString("8"));
                where = " where TemplateID = " + templateID.ToString() + " and RowNum = " + ((ypos + 1)).ToString();
                if (!FlowTemplateAction.UpdFlowChart(hashtableArray[1], where))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeleteCompleteNode(int templateID)
        {
            if (Convert.ToString(FlowTemplateAction.QueryTemplateState(templateID)) == "1")
            {
                if (isExistKeyNode(templateID))
                {
                    if (!IsPoolNode(templateID))
                    {
                        if (!DeletePoolEndNode(templateID))
                        {
                            return false;
                        }
                    }
                    else if (!DeleteEndNode(templateID))
                    {
                        return false;
                    }
                }
                else if (!DeleteEndNode(templateID))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeleteEndNode(int templateID)
        {
            int num = 0;
            string where = "";
            DataTable table = FlowTemplateAction.QueryTopFlowChart(templateID, 1);
            if (table.Rows.Count > 0)
            {
                for (int i = table.Columns.Count - 2; i > 2; i--)
                {
                    if ((table.Rows[0].ItemArray[i] != null) && (table.Rows[0].ItemArray[i].ToString() != ""))
                    {
                        if (table.Rows[0].ItemArray[i].ToString().IndexOf(";") > 0)
                        {
                            num = i - 1;
                            break;
                        }
                    }
                }
            }
            Hashtable flowChartInfo = new Hashtable();
            flowChartInfo.Add("Column" + num.ToString(), SqlStringConstructor.GetQuotedString(""));
            flowChartInfo.Add("Column" + ((num - 1)).ToString(), SqlStringConstructor.GetQuotedString(""));
            flowChartInfo.Add("Column" + ((num - 2)).ToString(), SqlStringConstructor.GetQuotedString(""));
            where = " where TemplateID=" + templateID.ToString() + " and RowNum = 1";
            if (FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
            {
                Hashtable templateInfo = new Hashtable();
                templateInfo.Add("IsComplete", SqlStringConstructor.GetQuotedString("0"));
                string str3 = " where TemplateID = " + templateID.ToString();
                if (!FlowTemplateAction.UpdTemplate(templateInfo, str3))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeleteFlowChart(int templateID)
        {
            string key = "";
            Hashtable flowChartInfo = new Hashtable();
            for (int i = 2; i < 0x3d; i++)
            {
                key = "Column" + i.ToString();
                flowChartInfo.Add(key, SqlStringConstructor.GetQuotedString(""));
            }
            string where = " Where templateId = " + templateID.ToString() + " and RowNum = 1";
            if (FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
            {
                Hashtable templateInfo = new Hashtable();
                where = " where TemplateID = " + templateID.ToString();
                templateInfo.Add("IsComplete", SqlStringConstructor.GetQuotedString("0"));
                if (FlowTemplateAction.UpdTemplate(templateInfo, where) && (publicDbOpClass.ExecuteSQL("Delete wf_FlowChart where templateId=" + templateID.ToString() + " and RowNum <> 1") == -1))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeleteNode(int xpos, int ypos, int nodeID, int templateID, int postPosition)
        {
            string pStr = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            int length = 0;
            int num3 = 0;
            int num4 = 0;
            DataTable table = FlowTemplateAction.QueryNodeList(templateID);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["NodeID"].ToString()) == nodeID)
                    {
                        str3 = row["FrontNode"].ToString();
                    }
                    str2 = row["FrontNode"].ToString();
                    length = str2.IndexOf(",");
                    if (length > 0)
                    {
                        while (length > 0)
                        {
                            str4 = str2.Substring(0, length);
                            if (Convert.ToInt32(str4) == nodeID)
                            {
                                num4 = Convert.ToInt32(row["NodeID"].ToString());
                                str5 = str5 + str3 + ",";
                                num3++;
                            }
                            else
                            {
                                str5 = str5 + str4 + ",";
                            }
                            str2 = str2.Substring(length + 1, (str2.Length - length) - 1);
                            length = str2.IndexOf(",");
                        }
                        if (num3 == 1)
                        {
                            pStr = str5 + str2;
                        }
                        else if ((num3 == 0) && (Convert.ToInt32(str2) == nodeID))
                        {
                            num4 = Convert.ToInt32(row["NodeID"].ToString());
                            pStr = str5 + str3;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToInt32(str2) == nodeID)
                            {
                                num4 = Convert.ToInt32(row["NodeID"].ToString());
                                pStr = str3;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (FlowTemplateAction.DelTemplateNode(nodeID, templateID))
            {
                string where = "";
                Hashtable nodeInfo = new Hashtable();
                nodeInfo.Add("FrontNode", SqlStringConstructor.GetQuotedString(pStr));
                where = " where TemplateID = " + templateID.ToString() + " and NodeID = " + num4.ToString();
                if (!FlowTemplateAction.UpdNode(nodeInfo, where))
                {
                    return false;
                }
            }
            object obj2 = FlowTemplateAction.QueryTemplateState(templateID);
            DataTable table2 = FlowTemplateAction.QueryTopFlowChart(templateID, ypos);
            if (table2.Rows.Count > 0)
            {
                DataRow dr = table2.Rows[0];
                Hashtable[] flowChartInfo = new Hashtable[3];
                string str7 = "";
                if (postPosition == 0)
                {
                    if (Convert.ToString(obj2) != "1")
                    {
                        if (ypos != 1)
                        {
                            if (!DelOffsetNodeNoAfter(templateID, xpos, ypos, flowChartInfo))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            flowChartInfo[0] = new Hashtable();
                            int num5 = xpos - 2;
                            flowChartInfo[0].Add("Column" + num5.ToString(), SqlStringConstructor.GetQuotedString(""));
                            int num6 = xpos - 1;
                            flowChartInfo[0].Add("Column" + num6.ToString(), SqlStringConstructor.GetQuotedString(""));
                            flowChartInfo[0].Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString(""));
                            str7 = " where TemplateID = " + templateID.ToString() + " and RowNum = " + ypos.ToString();
                            if (!FlowTemplateAction.UpdFlowChart(flowChartInfo[0], str7))
                            {
                                return false;
                            }
                        }
                    }
                    else if (!DelBeginEndNode(xpos, ypos, templateID, nodeID, pStr, dr))
                    {
                        return false;
                    }
                }
                else if (!DelBeginEndNode(xpos, ypos, templateID, nodeID, pStr, dr))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeletePoolEndNode(int templateID)
        {
            DataTable table = FlowTemplateAction.QueryFlowChart(templateID);
            Hashtable[] hashtableArray = new Hashtable[table.Rows.Count];
            string str = "";
            string where = "";
            int num = 0;
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    num = 0;
                    hashtableArray[i] = new Hashtable();
                    where = " where TemplateID = " + templateID.ToString() + " and RowNum = " + ((i + 1)).ToString();
                    for (int j = table.Columns.Count - 2; j > 2; j--)
                    {
                        if ((table.Rows[i].ItemArray[j] == null) || !(table.Rows[i].ItemArray[j].ToString() != ""))
                        {
                            continue;
                        }
                        str = table.Rows[i].ItemArray[j].ToString();
                        if (((i + 1) % 2) == 1)
                        {
                            if (str.IndexOf(";") > 0)
                            {
                                num++;
                            }
                            if (i == 0)
                            {
                                if (num == 2)
                                {
                                    break;
                                }
                            }
                            else if (num == 1)
                            {
                                break;
                            }
                            int num6 = j - 1;
                            hashtableArray[i].Add("Column" + num6.ToString(), SqlStringConstructor.GetQuotedString(""));
                            continue;
                        }
                        int num7 = j - 1;
                        hashtableArray[i].Add("Column" + num7.ToString(), SqlStringConstructor.GetQuotedString(""));
                        break;
                    }
                    if (FlowTemplateAction.UpdFlowChart(hashtableArray[i], where))
                    {
                        Hashtable templateInfo = new Hashtable();
                        templateInfo.Add("IsComplete", SqlStringConstructor.GetQuotedString("0"));
                        string str3 = " where TemplateID = " + templateID.ToString();
                        if (!FlowTemplateAction.UpdTemplate(templateInfo, str3))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static bool DelNode(int xpos, int ypos, int nodeID, int templateID)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " SELECT FrontNode FROM WF_TemplateNode WHERE TemplateID=", templateID, " AND NodeID=", nodeID, " " }));
            string str = "begin";
            DataTable table2 = publicDbOpClass.DataTableQuary(" SELECT * FROM WF_FlowChart WHERE TemplateID=" + templateID + " ");
            int num = 0;
            for (int i = xpos; i <= 60; i++)
            {
                if (string.IsNullOrEmpty(table2.Rows[0][i].ToString()))
                {
                    num = i - 2;
                    break;
                }
            }
            if (num == xpos)
            {
                object obj2 = str;
                object obj3 = string.Concat(new object[] { obj2, " DELETE FROM WF_TemplateNode WHERE NodeID=", nodeID, " " });
                object[] objArray3 = new object[] { obj3, " UPDATE WF_FlowChart SET Column", (num - 2).ToString(), " =NULL, Column", (num - 1).ToString(), "=NULL,Column", num, "=NULL WHERE TemplateID=", templateID, " " };
                str = string.Concat(objArray3);
            }
            else if ((xpos == (num - 3)) && (table2.Rows[0][num + 1].ToString() == "4;00;结束"))
            {
                object obj4 = str;
                str = string.Concat(new object[] { obj4, " DELETE FROM WF_TemplateNode WHERE NodeID=", nodeID, " " });
                if (table.Rows[0][0].ToString() == "0")
                {
                    object obj5 = str;
                    object[] objArray5 = new object[] { obj5, " UPDATE WF_FlowChart SET Column", xpos, "=NULL,Column", (xpos - 2).ToString(), "=NULL,Column", (xpos - 1).ToString(), "=NULL,Column", (xpos + 1).ToString(), "=NULL,Column", (xpos + 2).ToString(), "=NULL,Column", (xpos + 3).ToString(), "=NULL WHERE TemplateID=", templateID, " " };
                    object obj6 = string.Concat(objArray5);
                    str = string.Concat(new object[] { obj6, " UPDATE WF_Template SET IsComplete='0' WHERE TemplateID=", templateID, " " });
                }
                else
                {
                    object obj7 = str;
                    object[] objArray7 = new object[] { obj7, " UPDATE WF_FlowChart SET Column", xpos, "='4;00;结束',Column", (num - 2).ToString(), " =NULL, Column", (num - 1).ToString(), "=NULL,Column", num, "=NULL WHERE TemplateID=", templateID, " " };
                    str = string.Concat(objArray7);
                }
            }
            else
            {
                object obj8 = str;
                object obj9 = string.Concat(new object[] { obj8, " UPDATE WF_TemplateNode SET FrontNode='", table.Rows[0][0].ToString(), "'  WHERE FrontNode='", nodeID, "' AND TemplateID=", templateID, " " });
                str = string.Concat(new object[] { obj9, " DELETE FROM WF_TemplateNode WHERE NodeID=", nodeID, " " });
                int num15 = xpos + 3;
                string str2 = table2.Rows[0]["Column" + num15.ToString()].ToString();
                int num3 = str2.LastIndexOf(";");
                str2 = str2.Substring(0, num3 + 1) + table.Rows[0][0].ToString();
                object obj10 = str;
                str = string.Concat(new object[] { obj10, " UPDATE  WF_FlowChart SET Column", xpos, "='", str2, "' WHERE TemplateID=", templateID, " " });
                for (int j = xpos + 3; j <= (num - 3); j += 3)
                {
                    object obj11 = str;
                    object[] objArray11 = new object[8];
                    objArray11[0] = obj11;
                    objArray11[1] = " UPDATE WF_FlowChart SET Column";
                    objArray11[2] = j;
                    objArray11[3] = "='";
                    int num16 = j + 3;
                    objArray11[4] = table2.Rows[0]["Column" + num16.ToString()].ToString();
                    objArray11[5] = "' WHERE TemplateID=";
                    objArray11[6] = templateID;
                    objArray11[7] = " ";
                    str = string.Concat(objArray11);
                }
                object obj12 = str;
                object[] objArray12 = new object[] { obj12, " UPDATE WF_FlowChart SET Column", (num - 2).ToString(), " =NULL, Column", (num - 1).ToString(), "=NULL,Column", num, "=NULL WHERE TemplateID=", templateID, " " };
                str = string.Concat(objArray12);
            }
            return (publicDbOpClass.ExecuteSQL(str + " end ") > 0);
        }

        public static bool DelOffsetNodeNoAfter(int templateID, int xpos, int ypos, Hashtable[] flowChartInfo)
        {
            for (int i = ypos; i > (ypos - 3); i--)
            {
                int num2 = 0;
                string key = "";
                string where = "";
                bool flag = false;
                DataTable table = FlowTemplateAction.QueryTopFlowChart(templateID, i);
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    flowChartInfo[ypos - i] = new Hashtable();
                    for (int j = xpos - 3; j < (xpos + 1); j++)
                    {
                        key = "Column" + j.ToString();
                        if (j == (xpos - 3))
                        {
                            if (row[key].ToString() == "9")
                            {
                                flowChartInfo[ypos - i].Add(key, SqlStringConstructor.GetQuotedString("8"));
                                num2++;
                            }
                            else if ((row[key].ToString() == "10") || (row[key].ToString() == "8"))
                            {
                                flag = true;
                            }
                            else
                            {
                                num2++;
                                if (i == ypos)
                                {
                                    continue;
                                }
                            }
                            break;
                        }
                        if (i == ypos)
                        {
                            flowChartInfo[ypos - i].Add(key, SqlStringConstructor.GetQuotedString(""));
                        }
                    }
                    where = " where TemplateID = " + templateID.ToString() + " and RowNum = " + i.ToString();
                    if (flag)
                    {
                        if (publicDbOpClass.ExecuteSQL("delete wf_flowchart " + where) == -1)
                        {
                            return false;
                        }
                    }
                    else if (!FlowTemplateAction.UpdFlowChart(flowChartInfo[ypos - i], where))
                    {
                        return false;
                    }
                    if (num2 > 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        public static bool display_FlowChart(HtmlTable tbFlowChart, int templateID)
        {
            DataTable table = FlowTemplateAction.QueryFlowChart(templateID);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    HtmlTableRow row2 = new HtmlTableRow();
                    DataRow row = table.Rows[i];
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        int length = 0;
                        int num4 = 0;
                        string str = "";
                        string text = "";
                        string str3 = "";
                        string str4 = "";
                        string str5 = "";
                        if (j > 1)
                        {
                            if ((row.ItemArray[j].ToString() != "") && (row.ItemArray[j] != null))
                            {
                                cell.Attributes["style"] = "cursor:hand";
                                cell.Attributes["width"] = "45";
                                cell.Attributes["height"] = "32";
                                cell.Attributes["wrap"] = "true";
                                str = row.ItemArray[j].ToString();
                                length = str.IndexOf(";");
                                if (length > 0)
                                {
                                    while (length > 0)
                                    {
                                        switch (num4)
                                        {
                                            case 0:
                                                str3 = str.Substring(0, length);
                                                break;

                                            case 1:
                                                str4 = str.Substring(0, length);
                                                break;

                                            case 2:
                                                str5 = str.Substring(0, length);
                                                break;
                                        }
                                        str = str.Substring(length + 1, (str.Length - length) - 1);
                                        length = str.IndexOf(";");
                                        num4++;
                                    }
                                    if (num4 == 2)
                                    {
                                        str5 = str;
                                    }
                                    int posPositionNode = 0;
                                    if (isExistKeyNode(templateID))
                                    {
                                        posPositionNode = GetPosPositionNode(Convert.ToInt32(str4), templateID);
                                    }
                                    else
                                    {
                                        posPositionNode = 0;
                                    }
                                    cell.Attributes["onclick"] = "if (parent.getFrontNode) parent.getFrontNode(this);";
                                    cell.Attributes["tag"] = str4 + ";" + Convert.ToString((int) (j - 1)) + "," + Convert.ToString((int) (i + 1)) + ";" + Convert.ToString(posPositionNode);
                                    cell.Attributes["types"] = str3.Substring(str3.Length - 1, 1);
                                    switch (str4)
                                    {
                                        case "0":
                                        case "00":
                                            text = "<img src=\"workflow_icon/img" + str3 + ".gif\" width=\"32\" height=\"32\">";
                                            goto Label_038A;
                                    }
                                    switch (str3)
                                    {
                                        case "35":
                                        case "_edit_5":
                                            text = "<div align=\"center\"><img src=\"workflow_icon/img" + str3 + ".gif\" width=\"25\" height=\"25\"><br><font size=\"-1\">" + str5 + "</font></div>";
                                            goto Label_038A;
                                    }
                                    text = "<div align=\"center\"><img src=\"workflow_icon/img" + str3 + ".gif\" width=\"20\" height=\"20\"><br><font size=\"-1\">" + str5 + "</font></div>";
                                }
                                else
                                {
                                    cell.Attributes["style"] = "cursor:default";
                                    if (str.Length > 2)
                                    {
                                        text = "<font size=\"-1\">" + str + "</font>";
                                    }
                                    else
                                    {
                                        text = "<img src=\"workflow_icon/img" + str + ".gif\" width=\"45\" height=\"32\">";
                                    }
                                }
                            }
                            else
                            {
                                cell.Attributes["style"] = "cursor:default";
                                text = "";
                            }
                        }
                    Label_038A:
                        cell.Controls.Add(new LiteralControl(text));
                        row2.Cells.Add(cell);
                    }
                    tbFlowChart.Rows.Add(row2);
                }
            }
            return true;
        }

        public static bool display_FlowChartView(HtmlTable tbFlowChart, int templateID)
        {
            DataTable table = FlowTemplateAction.QueryFlowChart(templateID);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    HtmlTableRow row2 = new HtmlTableRow();
                    DataRow row = table.Rows[i];
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        int length = 0;
                        int num4 = 0;
                        string str = "";
                        string text = "";
                        string str3 = "";
                        string str4 = "";
                        string str5 = "";
                        if (j > 1)
                        {
                            if ((row.ItemArray[j].ToString() != "") && (row.ItemArray[j] != null))
                            {
                                cell.Attributes["style"] = "cursor:hand";
                                cell.Attributes["width"] = "32";
                                cell.Attributes["height"] = "32";
                                cell.Attributes["wrap"] = "true";
                                str = row.ItemArray[j].ToString();
                                length = str.IndexOf(";");
                                if (length > 0)
                                {
                                    while (length > 0)
                                    {
                                        switch (num4)
                                        {
                                            case 0:
                                                str3 = str.Substring(0, length);
                                                break;

                                            case 1:
                                                str4 = str.Substring(0, length);
                                                break;

                                            case 2:
                                                str5 = str.Substring(0, length);
                                                break;
                                        }
                                        str = str.Substring(length + 1, (str.Length - length) - 1);
                                        length = str.IndexOf(";");
                                        num4++;
                                    }
                                    if (num4 == 2)
                                    {
                                        str5 = str;
                                    }
                                    int posPositionNode = 0;
                                    if (isExistKeyNode(templateID))
                                    {
                                        posPositionNode = GetPosPositionNode(Convert.ToInt32(str4), templateID);
                                    }
                                    else
                                    {
                                        posPositionNode = 0;
                                    }
                                    cell.Attributes["tag"] = str4 + ";" + Convert.ToString((int) (j - 1)) + "," + Convert.ToString((int) (i + 1)) + ";" + Convert.ToString(posPositionNode);
                                    cell.Attributes["types"] = str3.Substring(str3.Length - 1, 1);
                                    switch (str4)
                                    {
                                        case "0":
                                        case "00":
                                            text = "<img src=\"workflow_icon/img" + str3 + ".gif\" width=\"32\" height=\"32\">";
                                            goto Label_031B;
                                    }
                                    text = "<div align=\"center\"><img src=\"workflow_icon/img" + str3 + ".gif\" width=\"32\" height=\"32\"><br><font size=\"-1\">" + str5 + "</font></div>";
                                }
                                else
                                {
                                    cell.Attributes["style"] = "cursor:default";
                                    if (str.Length > 2)
                                    {
                                        text = "<font size=\"-1\">" + str + "</font>";
                                    }
                                    else
                                    {
                                        text = "<img src=\"workflow_icon/img" + str + ".gif\" width=\"32\" height=\"32\">";
                                    }
                                }
                            }
                            else
                            {
                                cell.Attributes["style"] = "cursor:default";
                                text = "";
                            }
                        }
                    Label_031B:
                        cell.Controls.Add(new LiteralControl(text));
                        row2.Cells.Add(cell);
                    }
                    tbFlowChart.Rows.Add(row2);
                }
            }
            return true;
        }

        public static string FlowChartDeal()
        {
            try
            {
                string sqlString = "DELETE FROM WF_FlowChart WHERE TemplateID IN (SELECT TemplateID  FROM WF_FlowChart WHERE RowNum !=1) ";
                publicDbOpClass.ExecuteSQL(sqlString);
            }
            catch
            {
            }
            DataTable table = publicDbOpClass.DataTableQuary(" SELECT * FROM WF_FlowChart WHERE RowNum=1 ");
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                List<string> list = new List<string>();
                for (int j = 3; j < table.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(table.Rows[i][j].ToString()) && table.Rows[i][j].ToString().Contains(";"))
                    {
                        list.Add("7");
                        list.Add("2");
                        list.Add(table.Rows[i][j].ToString());
                    }
                }
                for (int k = 0; k < list.Count; k++)
                {
                    string[] strArray = new string[] { " UPDATE WF_FlowChart SET Column", (k + 2).ToString(), "= '", list[k], "' WHERE TemplateID=", table.Rows[i][0].ToString(), " " };
                    builder.Append(string.Concat(strArray));
                    builder.AppendLine();
                }
                for (int m = list.Count + 2; m <= 60; m++)
                {
                    builder.Append(string.Concat(new object[] { " UPDATE WF_FlowChart SET Column", m, "=NULL WHERE TemplateID=", table.Rows[i][0].ToString(), " " }));
                    builder.AppendLine();
                }
                if (publicDbOpClass.ExecuteSQL(builder.ToString()) < 0x3b)
                {
                    return ("false" + table.Rows[i][0].ToString());
                }
            }
            return "true";
        }

        public static bool FlowComplete(int templateID)
        {
            int num = 0;
            string where = "";
            DataTable table = FlowTemplateAction.QueryTopFlowChart(templateID, 1);
            if (table.Rows.Count > 0)
            {
                for (int i = table.Columns.Count - 2; i > 2; i--)
                {
                    if ((table.Rows[0].ItemArray[i] != null) && (table.Rows[0].ItemArray[i].ToString() != ""))
                    {
                        if (table.Rows[0].ItemArray[i].ToString().IndexOf(";") > 0)
                        {
                            num = i - 1;
                            break;
                        }
                    }
                }
            }
            Hashtable flowChartInfo = new Hashtable();
            flowChartInfo.Add("Column" + ((num + 1)).ToString(), SqlStringConstructor.GetQuotedString("7"));
            flowChartInfo.Add("Column" + ((num + 2)).ToString(), SqlStringConstructor.GetQuotedString("2"));
            flowChartInfo.Add("Column" + ((num + 3)).ToString(), SqlStringConstructor.GetQuotedString("4;00;结束"));
            where = " where TemplateID=" + templateID.ToString() + " and RowNum = 1";
            if (!FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
            {
                return false;
            }
            return true;
        }

        public static int getBigValue(int[] value)
        {
            int length = value.Length;
            int num2 = 0;
            for (int i = 0; i < length; i++)
            {
                if (num2 < value[i])
                {
                    num2 = value[i];
                }
            }
            return num2;
        }

        public static int GetPosPositionNode(int nodeID, int templateID)
        {
            string str = "";
            int num = 0;
            int length = 0;
            DataTable table = FlowTemplateAction.QueryNodeList(templateID);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    str = row["FrontNode"].ToString();
                    length = str.IndexOf(",");
                    if (length > 0)
                    {
                        while (length > 0)
                        {
                            if (str.Substring(0, length) == Convert.ToString(nodeID))
                            {
                                num++;
                                break;
                            }
                            str = str.Substring(length + 1, (str.Length - length) - 1);
                            length = str.IndexOf(",");
                        }
                        if (str == Convert.ToString(nodeID))
                        {
                            num++;
                        }
                        continue;
                    }
                    if (str == Convert.ToString(nodeID))
                    {
                        num++;
                    }
                }
                if ((num == 0) && (Convert.ToString(FlowTemplateAction.QueryTemplateState(templateID)) == "1"))
                {
                    num++;
                }
            }
            return num;
        }

        public static int getSmaillValue(int[] value)
        {
            int length = value.Length;
            int num2 = value[0];
            for (int i = 0; i < length; i++)
            {
                if (num2 > value[i])
                {
                    num2 = value[i];
                }
            }
            return num2;
        }

        public static bool isExistKeyNode(int templateID)
        {
            bool flag = false;
            DataTable table = FlowTemplateAction.QueryNodeList(templateID);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (GetPosPositionNode(Convert.ToInt32(row["NodeID"].ToString()), templateID) > 1)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public static bool IsHaveNode(int templateId)
        {
            if (publicDbOpClass.DataTableQuary("SELECT COUNT(*) FROM WF_TemplateNode WHERE TemplateID=" + templateId).Rows[0][0].ToString() == "0")
            {
                return false;
            }
            return true;
        }

        public static bool IsPoolNode(int templateID)
        {
            bool flag = false;
            if (FlowTemplateAction.QueryFlowChart(templateID).Rows.Count > 1)
            {
                DataTable table2 = FlowTemplateAction.QueryNodeList(templateID);
                if (table2.Rows.Count <= 0)
                {
                    return flag;
                }
                foreach (DataRow row in table2.Rows)
                {
                    if (row["FrontNode"].ToString().IndexOf(",") > 0)
                    {
                        return true;
                    }
                }
            }
            return flag;
        }

        public static bool IsPoolNode(int templateID, int nodeID)
        {
            bool flag = false;
            DataTable table = FlowTemplateAction.QueryNodeList(templateID, nodeID);
            if ((table.Rows.Count > 0) && (table.Rows[0]["FrontNode"].ToString().IndexOf(",") > 0))
            {
                flag = true;
            }
            return flag;
        }

        public static bool UpdateFlowChart(int xpos, int ypos, string pos, string frontNodeOld, string frontNode, int nodeID, int templateID, string nodeName, string condition, string types)
        {
            string pStr = "";
            string where = "";
            Hashtable flowChartInfo = new Hashtable();
            if (frontNodeOld == frontNode)
            {
                where = " where TemplateID = " + templateID.ToString() + " and RowNum = " + ypos.ToString();
                pStr = "_edit_" + types + ";" + nodeID.ToString() + ";" + nodeName.ToString() + ";" + frontNode.ToString();
                flowChartInfo.Add("Column" + xpos.ToString(), SqlStringConstructor.GetQuotedString(pStr));
                if ((condition != null) && (condition != ""))
                {
                    flowChartInfo.Add("Column" + ((xpos - 2)).ToString(), SqlStringConstructor.GetQuotedString(condition));
                }
                else
                {
                    flowChartInfo.Add("Column" + ((xpos - 2)).ToString(), SqlStringConstructor.GetQuotedString("7"));
                }
                if (!FlowTemplateAction.UpdFlowChart(flowChartInfo, where))
                {
                    return false;
                }
            }
            else if ((frontNode.IndexOf(",") > 0) && !CreatePoolNode(pos, frontNode, nodeID, templateID, nodeName, condition, "Mod", types))
            {
                return false;
            }
            return true;
        }
    }
}

