namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CodingAction
    {
        public int AddBasicCode(CodingInfo codingInfo)
        {
            string str = "";
            int codeMaxID = this.GetCodeMaxID(codingInfo.TypeID);
            str = str + " begin ";
            if (codingInfo.IsDefault)
            {
                str = str + " update XPM_Basic_CodeList set IsDefault = 0,VersioniTime = getdate() where (TypeID=" + codingInfo.TypeID.ToString() + ")and(IsDefault=1) ";
            }
            string str2 = str;
            string str3 = str2 + " insert into XPM_Basic_CodeList values(" + codeMaxID.ToString() + "," + codingInfo.TypeID.ToString();
            string str4 = str3 + "," + codingInfo.ParentCodeID.ToString() + ",'" + codingInfo.ParentCodeList + codeMaxID.ToString() + ",','" + codingInfo.CodeName;
            string[] strArray3 = new string[] { str4, "',0,", (codingInfo.IsFixed ? 1 : 0).ToString(), ",", (codingInfo.IsDefault ? 1 : 0).ToString(), ",1,1,'000000',getdate())" };
            string str5 = string.Concat(strArray3) + " update XPM_Basic_CodeList set ChildNumber = (select count(1) from XPM_Basic_CodeList ";
            string str6 = str5 + " where (ParentCodeID=" + codingInfo.ParentCodeID.ToString() + ")and(IsValid = 1)and(TypeID=" + codingInfo.TypeID.ToString() + ")),VersionTime=getdate() ";
            return publicDbOpClass.ExecSqlString((str6 + " where (TypeID=" + codingInfo.TypeID.ToString() + ")and(CodeID=" + codingInfo.ParentCodeID.ToString() + ")") + " end ");
        }

        public int AddBasicCode(CodingInfo model, string _new)
        {
            int codeMaxID = this.GetCodeMaxID(model.TypeID);
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(model.SignCode2))
            {
                CodingType type = this.QuerySingleCodingType(model.TypeID);
                if (type != null)
                {
                    model.SignCode2 = type.SignCode;
                }
            }
            builder.Append(" begin \n");
            if (model.IsDefault)
            {
                builder.Append(" update XPM_Basic_CodeList set IsDefault=0, VersionTime=getdate()\n");
                builder.Append(" where TypeID=@TypeID AND IsDefault=1; \n");
            }
            builder.Append(" insert into XPM_Basic_CodeList values( \n");
            builder.Append("\t\t@CodeID,@TypeID1,@ParentCodeID,@ParentCodeList,@CodeName,0,@IsFixed,@IsDefault,1,1,@Owner,getdate(),@I_xh, @SignCode2) \n");
            builder.Append(" update XPM_Basic_CodeList set ChildNumber = (select count(1) from XPM_Basic_CodeList  \n");
            builder.Append(" where (ParentCodeID=@ParentCodeID1 \n");
            builder.Append(" ) and (IsValid = 1) and (TypeID=@TypeID2 \n");
            builder.Append(" )),VersionTime=getdate() \n");
            builder.Append(" where (TypeID=@TypeID3 \n");
            builder.Append(" ) and (CodeID=@ParentCodeID3) \n");
            builder.Append(" end \n");
            int num2 = 0;
            if (model.IsDefault)
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", model.TypeID), new SqlParameter("@CodeID", codeMaxID), new SqlParameter("@TypeID1", model.TypeID), new SqlParameter("@ParentCodeID", model.ParentCodeID), new SqlParameter("@ParentCodeList", model.ParentCodeList + codeMaxID.ToString() + ","), new SqlParameter("@CodeName", model.CodeName), new SqlParameter("@IsFixed", model.IsFixed ? 1 : 0), new SqlParameter("@IsDefault", model.IsDefault ? 1 : 0), new SqlParameter("@ParentCodeID1", model.ParentCodeID), new SqlParameter("@TypeID2", model.TypeID), new SqlParameter("@TypeID3", model.TypeID), new SqlParameter("@ParentCodeID3", model.ParentCodeID), new SqlParameter("@I_xh", model.I_xh), new SqlParameter("@Owner", model.Owner), new SqlParameter("@SignCode2", model.SignCode2) };
                num2 = publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@CodeID", codeMaxID), new SqlParameter("@TypeID1", model.TypeID), new SqlParameter("@ParentCodeID", model.ParentCodeID), new SqlParameter("@ParentCodeList", model.ParentCodeList + codeMaxID.ToString() + ","), new SqlParameter("@CodeName", model.CodeName), new SqlParameter("@IsFixed", model.IsFixed ? 1 : 0), new SqlParameter("@IsDefault", model.IsDefault ? 1 : 0), new SqlParameter("@ParentCodeID1", model.ParentCodeID), new SqlParameter("@TypeID2", model.TypeID), new SqlParameter("@TypeID3", model.TypeID), new SqlParameter("@ParentCodeID3", model.ParentCodeID), new SqlParameter("@I_xh", model.I_xh), new SqlParameter("@Owner", model.Owner), new SqlParameter("@SignCode2", model.SignCode2) };
                num2 = publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), parameterArray2);
            }
            if (num2 > 0)
            {
                return 1;
            }
            return 0;
        }

        public static DataTable CodeInfoList(string typeID)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + " select * from XPM_Basic_CodeList where (TypeID=" + typeID.ToString() + ") and (IsVisible=1) and(IsValid=1) ORDER BY IsDefault DESC,I_xh asc");
        }

        public static DataTable CodeInfoListType(string strSignCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + " select * from XPM_Basic_CodeType where (SignCode='" + strSignCode.ToString() + "') and (IsVisible=1) and(IsValid=1)");
        }

        public int CodingTypeAdd(CodingType ct)
        {
            string sqlString = "select max(TypeID) + 1 from XPM_Basic_CodeType";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            string str2 = "";
            string str3 = str2 + " insert into XPM_Basic_CodeType (TypeID,TypeName,IsVisible,IsValid,Remark,SignCode,Owner,VersionTime) values('" + table.Rows[0][0].ToString() + "','";
            string str4 = str3 + ct.TypeName + "',1,1,'" + ct.Remark + "','" + ct.SignCode + "','000000','" + DateTime.Now.ToString() + "')";
            return publicDbOpClass.ExecSqlString(str4 + " insert into XPM_Basic_NextID (SignCode,MaxID) values('" + ct.SignCode + "','" + table.Rows[0][0].ToString() + "') ");
        }

        public int CodingTypeUp(CodingType ct)
        {
            string str = "";
            string str2 = str + " update XPM_Basic_CodeType set ";
            return publicDbOpClass.ExecSqlString((str2 + " TypeName='" + ct.TypeName + "',Remark='" + ct.Remark + "',SignCode='" + ct.SignCode + "'") + " where TypeID=" + ct.TypeID);
        }

        public int DelBasicCode(int typeID, int CodeID)
        {
            CodingInfo info = this.QuerySingleCodeInfo(CodeID, typeID);
            if (info.IsValid)
            {
                string str = "";
                string str2 = str + " begin ";
                string str3 = (str2 + " update XPM_Basic_CodeList set IsValid = 0,VersionTime=getdate() where TypeID = " + typeID.ToString() + " and CodeID = " + CodeID.ToString() + " and IsFixed = 0 and IsDefault = 0") + " update XPM_Basic_CodeList set ChildNumber = (select count(1) from XPM_Basic_CodeList ";
                object obj2 = str3 + " where (ParentCodeID=" + info.ParentCodeID.ToString() + ")and(IsValid = 1)and(TypeID=" + typeID.ToString() + ")),VersionTime=getdate() ";
                return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " where (TypeID=", typeID, ")and(CodeID=", info.ParentCodeID.ToString(), ")" }) + " end ");
            }
            return 1;
        }

        private int GetCodeMaxID(int typeID)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar(" SELECT (ISNULL(MAX(CodeID), 0) + 1) AS Expr1  FROM XPM_Basic_CodeList where TypeID = " + typeID.ToString()));
        }

        private CodingInfo GetCodingInfoFromDataRow(DataRow dr)
        {
            return new CodingInfo { NodeID = (int) dr["NoteID"], CodeID = (int) dr["CodeID"], TypeID = (int) dr["TypeID"], ParentCodeID = (int) dr["ParentCodeID"], ParentCodeList = dr["ParentCodeList"].ToString(), CodeName = dr["CodeName"].ToString(), ChildNumber = (int) dr["ChildNumber"], IsDefault = (bool) dr["IsDefault"], IsFixed = (bool) dr["IsFixed"], IsValid = (bool) dr["IsValid"], Owner = (string) dr["Owner"], I_xh = (int) dr["I_xh"] };
        }

        private CodingType GetCodingTypeFromDataRow(DataRow dr)
        {
            return new CodingType { TypeID = (int) dr["TypeID"], TypeName = dr["TypeName"].ToString(), IsVisible = (bool) dr["IsVisible"], IsValid = (bool) dr["IsValid"], Remark = dr["Remark"].ToString(), SignCode = dr["SignCode"].ToString() };
        }

        public static DataTable getTypebyID(string ID)
        {
            return publicDbOpClass.DataTableQuary("select 0 as CodeID ,'全部' as CodeName  union Select CodeID,CodeName From XPM_Basic_CodeList WHERE (TypeID =" + ID + " AND isValid=1)");
        }

        public CodingInfoCollection QueryCodeInfoList(int typeID, ValidState vState)
        {
            CodingInfoCollection infos = new CodingInfoCollection();
            string str = "";
            str = str + " select * from XPM_Basic_CodeList where (TypeID=" + typeID.ToString() + ") and (IsVisible=1)";
            switch (vState)
            {
                case ValidState.InValid:
                    str = str + "and(IsValid=0) ";
                    break;

                case ValidState.Valid:
                    str = str + "and(IsValid=1) ";
                    break;
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(str + " ORDER BY IsDefault DESC,I_xh asc"))
            {
                int count = table.Rows.Count;
                if (count <= 0)
                {
                    return infos;
                }
                for (int i = 0; i < count; i++)
                {
                    infos.Add(this.GetCodingInfoFromDataRow(table.Rows[i]));
                }
            }
            return infos;
        }

        public CodingInfoCollection QueryCodeInfoList(int typeID, string userCode)
        {
            CodingInfoCollection infos = new CodingInfoCollection();
            StringBuilder builder = new StringBuilder();
            if (userCode != "00000000")
            {
                builder.AppendFormat("\r\nWITH cteallprivate \r\n     AS (SELECT T1.*\r\n         FROM   XPM_Basic_CodeList AS T1 \r\n                LEFT JOIN Basic_Privilege AS T2 \r\n                  ON T1.NoteId = T2.RelationsKey \r\n         WHERE  TypeId = '{0}' \r\n                AND IsVisible = 1 \r\n                AND IsValid = 1\r\n                AND UserCode = '{1}'\r\n                AND RelationsTable = 'XPM_Basic_CodeList'\r\n                 ), \r\n     ctechildrend \r\n     AS (SELECT * \r\n         FROM   cteallprivate \r\n         UNION ALL \r\n         SELECT T2.*\r\n         FROM   XPM_Basic_CodeList AS T2 \r\n                INNER JOIN ctechildrend \r\n                  ON cteChildrend.CodeID = T2.ParentCodeID \r\n         WHERE  T2.TypeId = '{0}' \r\n                AND T2.IsVisible = 1 \r\n                AND T2.IsValid = 1) \r\nSELECT * \r\nFROM   ctechildrend \r\nORDER  BY IsDefault, \r\n          CodeID ASC ", typeID, userCode);
                using (DataTable table = publicDbOpClass.DataTableQuary(builder.ToString()))
                {
                    int count = table.Rows.Count;
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            infos.Add(this.GetCodingInfoFromDataRow(table.Rows[i]));
                        }
                    }
                    return infos;
                }
            }
            return this.QueryCodeInfoList(typeID, ValidState.Valid);
        }

        public CodingInfoCollection QueryCodeInfoList(int typeID, ValidState vState, string CodeID)
        {
            CodingInfoCollection infos = new CodingInfoCollection();
            string sqlString = "";
            string str2 = "";
            if (CodeID.ToString() == "-3")
            {
                str2 = "-3,-2";
            }
            else if (CodeID.ToString() == "-2")
            {
                str2 = "-3,-2,-1";
            }
            else if (CodeID.ToString() == "-1")
            {
                str2 = "4,6,7,8,9";
            }
            string str3 = sqlString;
            sqlString = str3 + " select * from XPM_Basic_CodeList where (TypeID=" + typeID.ToString() + ") and (CodeID in (" + str2 + ")) and (IsVisible=1)";
            switch (vState)
            {
                case ValidState.InValid:
                    sqlString = sqlString + "and(IsValid=0) ";
                    break;

                case ValidState.Valid:
                    sqlString = sqlString + "and(IsValid=1) ";
                    break;
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                int count = table.Rows.Count;
                if (count <= 0)
                {
                    return infos;
                }
                for (int i = 0; i < count; i++)
                {
                    infos.Add(this.GetCodingInfoFromDataRow(table.Rows[i]));
                }
            }
            return infos;
        }

        public CodingInfoCollection QueryCodeInfoList(int typeID, int noteID, ValidState vState)
        {
            CodingInfoCollection infos = new CodingInfoCollection();
            string sqlString = "";
            string str2 = sqlString;
            sqlString = str2 + " select * from XPM_Basic_CodeList where (TypeID=" + typeID.ToString() + ") and ParentCodeList like '%," + noteID.ToString() + ",%' and (IsVisible=1)";
            switch (vState)
            {
                case ValidState.InValid:
                    sqlString = sqlString + "and(IsValid=0) ";
                    break;

                case ValidState.Valid:
                    sqlString = sqlString + "and(IsValid=1) ";
                    break;
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                int count = table.Rows.Count;
                if (count <= 0)
                {
                    return infos;
                }
                for (int i = 0; i < count; i++)
                {
                    infos.Add(this.GetCodingInfoFromDataRow(table.Rows[i]));
                }
            }
            return infos;
        }

        public static DataTable QueryCodeTypeDT()
        {
            string sqlString = "";
            sqlString = " select * from XPM_Basic_CodeType where IsVisible = 1 and SignCode!='Img' order by TypeID asc";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public CodingTypeCollection QueryCodeTypeList()
        {
            CodingTypeCollection types = new CodingTypeCollection();
            string sqlString = "";
            sqlString = " select * from XPM_Basic_CodeType where IsVisible = 1 order by TypeID asc";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                int count = table.Rows.Count;
                if (count <= 0)
                {
                    return types;
                }
                for (int i = 0; i < count; i++)
                {
                    types.Add(this.GetCodingTypeFromDataRow(table.Rows[i]));
                }
            }
            return types;
        }

        public CodingInfoCollection QueryCorpTypeList(string codeTerm)
        {
            CodingInfoCollection infos = new CodingInfoCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_CodeList a join (select rtrim(Convert(varchar(10),CodeID)) as CodeS from XPM_Basic_CodeList where (CodeID in (" + codeTerm + ")) and (TypeID=4) and (IsVisible=1)) b on substring(a.ParentCodelist,2,charindex('','',a.ParentCodeList,2)-2) = b.CodeS and (a.typeID = 4) and (IsVisible=1)"))
            {
                int count = table.Rows.Count;
                if (count <= 0)
                {
                    return infos;
                }
                for (int i = 0; i < count; i++)
                {
                    infos.Add(this.GetCodingInfoFromDataRow(table.Rows[i]));
                }
            }
            return infos;
        }

        public DataTable QueryDtbCodeInfo(int typeID, ValidState vState)
        {
            new CodingInfoCollection();
            StringBuilder builder = new StringBuilder();
            builder.Append("select distinct XPM_Basic_CodeList.*,(\r\n\t\t                    select stuff((select '、'+[v_xm] from Basic_Privilege priv1\r\n                                  left join PT_yhmc ON PT_yhmc.v_yhdm=priv1.UserCode\r\n                                  where priv1.RelationsKey=priv2.RelationsKey\r\n                                  for xml path('')),1,1,'')\r\n                          from Basic_Privilege priv2\r\n                          where priv2.RelationsKey=XPM_Basic_CodeList.NoteID\r\n                          group by [RelationsKey]\r\n                       ) as  privilegeNames\r\n                     from XPM_Basic_CodeList\r\n                        left join Basic_Privilege as priv3 on priv3.RelationsKey=XPM_Basic_CodeList.NoteID");
            builder.AppendFormat(" where (TypeID='{0}') and (IsVisible=1)", typeID);
            switch (vState)
            {
                case ValidState.InValid:
                    builder.Append(" and (IsValid=0) ");
                    break;

                case ValidState.Valid:
                    builder.Append("and (IsValid=1) ");
                    break;
            }
            builder.Append(" ORDER BY IsDefault DESC,I_xh asc");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public CodingInfo QuerySingleCodeInfo(int codeID, int typeID)
        {
            CodingInfo codingInfoFromDataRow = new CodingInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary(" select * from XPM_Basic_CodeList where (CodeID=" + codeID.ToString() + ")and(TypeID=" + typeID.ToString() + ") and (IsVisible=1) "))
            {
                if (table.Rows.Count == 1)
                {
                    codingInfoFromDataRow = this.GetCodingInfoFromDataRow(table.Rows[0]);
                }
            }
            return codingInfoFromDataRow;
        }

        public CodingInfo QuerySingleCodeInfo(string codeName, int typeID)
        {
            CodingInfo codingInfoFromDataRow = new CodingInfo();
            string spName = "";
            spName = " select * from XPM_Basic_CodeList where CodeName=@codeName and TypeID=@typeID AND IsValid=1 and IsVisible=1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@codeName", SqlDbType.VarChar), new SqlParameter("@typeID", SqlDbType.Int, 4) };
            commandParameters[0].Value = codeName;
            commandParameters[1].Value = typeID;
            using (DataTable table = publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters))
            {
                if (table.Rows.Count == 1)
                {
                    codingInfoFromDataRow = this.GetCodingInfoFromDataRow(table.Rows[0]);
                }
            }
            return codingInfoFromDataRow;
        }

        public CodingType QuerySingleCodingType(int typeID)
        {
            CodingType codingTypeFromDataRow = new CodingType();
            using (DataTable table = publicDbOpClass.DataTableQuary(" select * from XPM_Basic_CodeType where TypeID = " + typeID.ToString()))
            {
                if (table.Rows.Count == 1)
                {
                    codingTypeFromDataRow = this.GetCodingTypeFromDataRow(table.Rows[0]);
                }
            }
            return codingTypeFromDataRow;
        }

        public int RecycleCode(CodingInfo codingInfo)
        {
            object obj2 = ("" + " begin ") + " update XPM_Basic_CodeList set ParentCodeID = " + codingInfo.ParentCodeID.ToString();
            object[] objArray = new object[] { obj2, " ,ParentCodeList='", codingInfo.ParentCodeList, codingInfo.CodeID, ",',IsDefault=", (codingInfo.IsDefault ? 1 : 0).ToString() };
            string str2 = string.Concat(objArray) + " ,IsValid = 1,VersionTime = getdate() ";
            string str3 = (str2 + " where (TypeID = " + codingInfo.TypeID.ToString() + ")and(CodeName='" + codingInfo.CodeName + "')and(IsValid=0)") + " update XPM_Basic_CodeList set ChildNumber = (select count(1) from XPM_Basic_CodeList ";
            string str4 = str3 + " where (ParentCodeID=" + codingInfo.ParentCodeID.ToString() + ")and(IsValid = 1)and(TypeID=" + codingInfo.TypeID.ToString() + ")),VersionTime=getdate() ";
            return publicDbOpClass.ExecSqlString((str4 + " where (TypeID=" + codingInfo.TypeID.ToString() + ")and(CodeID=" + codingInfo.ParentCodeID.ToString() + ")") + " end ");
        }

        public int UpdBasicCode(CodingInfo model)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            if (model.IsDefault)
            {
                builder.Append("update XPM_Basic_CodeList set ");
                builder.Append("IsDefault=0,");
                builder.Append("VersionTime=getdate()");
                builder.Append(" where TypeID=@TypeID AND IsDefault=1 ");
            }
            builder.Append("  update XPM_Basic_CodeList set CodeName=@CodeName ,IsDefault=@IsDefault,VersionTime = getdate(),Owner=@Owner,I_xh=@I_xh ");
            builder.Append(" where CodeID =@CodeID and TypeID =@TypeID1 and IsFixed = 0 ");
            builder.Append("  update XPM_Basic_CodeList set ChildNumber = (select count(1) from XPM_Basic_CodeList  ");
            builder.Append(" where (ParentCodeID=@ParentCodeID1");
            builder.Append(" )and(IsValid = 1)and(TypeID=@TypeID2");
            builder.Append("  )),VersionTime=getdate() ");
            builder.Append(" where (TypeID=@TypeID3");
            builder.Append(" )and(CodeID=@ParentCodeID3)  END");
            if (model.IsDefault)
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.Int, 4), new SqlParameter("@CodeName", SqlDbType.VarChar), new SqlParameter("@IsDefault", SqlDbType.Int, 4), new SqlParameter("@CodeID", SqlDbType.Int, 4), new SqlParameter("@TypeID1", SqlDbType.Int, 4), new SqlParameter("@ParentCodeID1", SqlDbType.Int, 4), new SqlParameter("@TypeID2", SqlDbType.Int, 4), new SqlParameter("@TypeID3", SqlDbType.Int, 4), new SqlParameter("@ParentCodeID3", SqlDbType.Int, 4), new SqlParameter("@Owner", SqlDbType.VarChar), new SqlParameter("@I_xh", SqlDbType.Int) };
                commandParameters[0].Value = model.TypeID;
                commandParameters[1].Value = model.CodeName;
                commandParameters[2].Value = model.IsDefault ? 1 : 0;
                commandParameters[3].Value = model.CodeID;
                commandParameters[4].Value = model.TypeID;
                commandParameters[5].Value = model.ParentCodeID;
                commandParameters[6].Value = model.TypeID;
                commandParameters[7].Value = model.TypeID;
                commandParameters[8].Value = model.ParentCodeID;
                commandParameters[9].Value = model.Owner;
                commandParameters[10].Value = model.I_xh;
                num = publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@CodeName", SqlDbType.VarChar), new SqlParameter("@IsDefault", SqlDbType.Int, 4), new SqlParameter("@CodeID", SqlDbType.Int, 4), new SqlParameter("@TypeID1", SqlDbType.Int, 4), new SqlParameter("@ParentCodeID1", SqlDbType.Int, 4), new SqlParameter("@TypeID2", SqlDbType.Int, 4), new SqlParameter("@TypeID3", SqlDbType.Int, 4), new SqlParameter("@ParentCodeID3", SqlDbType.Int, 4), new SqlParameter("@Owner", SqlDbType.VarChar), new SqlParameter("@I_xh", SqlDbType.Int) };
                parameterArray2[0].Value = model.CodeName;
                parameterArray2[1].Value = model.IsDefault ? 1 : 0;
                parameterArray2[2].Value = model.CodeID;
                parameterArray2[3].Value = model.TypeID;
                parameterArray2[4].Value = model.ParentCodeID;
                parameterArray2[5].Value = model.TypeID;
                parameterArray2[6].Value = model.TypeID;
                parameterArray2[7].Value = model.ParentCodeID;
                parameterArray2[8].Value = model.Owner;
                parameterArray2[9].Value = model.I_xh;
                num = publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), parameterArray2);
            }
            if (num > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}

