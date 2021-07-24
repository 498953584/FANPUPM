namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class AffairAction
    {
        public static int ADD(AffairModel AM)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO EPM_Datum_Affair (");
            builder.Append("i_id, PrjCode,AffairClass,AffairTitle,AddDate,Remark,Context,DatumClass,CA,mark,filesType) ");
            builder.Append("VALUES(");
            builder.Append("'").Append(AM.i_id).Append("',");
            builder.Append("'").Append(AM.PrjCode).Append("',");
            builder.Append("'").Append(AM.AffairClass).Append("',");
            builder.Append("'").Append(AM.AffairTitle).Append("',");
            builder.Append("'").Append(AM.Date).Append("',");
            builder.Append("'").Append(AM.Remark).Append("',");
            builder.Append("'").Append(AM.Context).Append("',");
            builder.Append("").Append(AM.Flage).Append(",");
            builder.Append("").Append(AM.CA).Append(" ,");
            builder.Append("").Append(AM.Mark).Append(" ,");
            builder.Append("").Append(AM.FilesType).Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static int DEL(string ID)
        {
            return publicDbOpClass.ExecSqlString(" delete from EPM_Datum_Affair where i_id = '" + ID + "'");
        }

        private AffairModel GetFromDataRow(DataRow dr)
        {
            AffairModel model = new AffairModel {
                i_id = (Guid) dr["i_id"],
                PrjCode = (Guid) dr["PrjCode"],
                AffairClass = dr["AffairClass"].ToString(),
                AffairTitle = dr["AffairTitle"].ToString(),
                Date = (DateTime) dr["AddDate"],
                Context = dr["Context"].ToString(),
                Remark = dr["Remark"].ToString()
            };
            if ((dr["CA"] != null) && (dr["CA"].ToString() != ""))
            {
                model.CA = int.Parse(dr["CA"].ToString());
            }
            if ((dr["Mark"] != null) && (dr["Mark"].ToString() != ""))
            {
                model.Mark = int.Parse(dr["Mark"].ToString());
            }
            if ((dr["FilesType"] != null) && (dr["FilesType"].ToString() != ""))
            {
                model.FilesType = int.Parse(dr["FilesType"].ToString());
                return model;
            }
            model.FilesType = 0;
            return model;
        }

        public static DataTable GetPageData(Guid PrjCode, string Flage, string DDLLookup, string TxtLookup)
        {
            string sqlWhere = GetSqlWhere(Flage, DDLLookup, TxtLookup, PrjCode);
            new StringBuilder();
            return publicDbOpClass.GetPageData(sqlWhere, "EPM_Datum_Affair", "adddate desc");
        }

        public AffairModel GetSingleAffair(string ID)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Datum_Affair where i_id='" + ID + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetFromDataRow(table.Rows[0]);
                }
            }
            return new AffairModel();
        }

        private static string GetSqlWhere(string Flage, string DDLLookup, string TextLookup, Guid PrjCode)
        {
            string str = string.Concat(new object[] { " DatumClass = '", Flage, "' and PrjCode = '", PrjCode, "'" });
            string str2 = DDLLookup;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return (str + " and Remark like '%" + TextLookup + "%'");
                }
            }
            else
            {
                return (str + " and AffairTitle like '%" + TextLookup + "%'");
            }
            return (str + " and Context like '%" + TextLookup + "%'");
        }

        public static int Update(AffairModel AM)
        {
            object obj2 = " update EPM_Datum_Affair set AffairClass = '" + AM.AffairClass + "',";
            object obj3 = string.Concat(new object[] { obj2, " AffairTitle = '", AM.AffairTitle, "', AddDate = '", AM.Date, "'," });
            object obj4 = string.Concat(new object[] { obj3, " Remark = '", AM.Remark, "',Context = '", AM.Context, "' ,CA=", AM.CA, " ,mark=", AM.Mark, ",filesType=", AM.FilesType });
            string str = string.Concat(new object[] { obj4, " where i_id = '", AM.i_id, "'" });
            StringBuilder builder = new StringBuilder();
            builder.Append("update EPM_Datum_Affair set ");
            Guid prjCode = AM.PrjCode;
            builder.Append("PrjCode='" + AM.PrjCode + "',");
            if (AM.AffairClass != null)
            {
                builder.Append("AffairClass='" + AM.AffairClass + "',");
            }
            if (AM.AffairTitle != null)
            {
                builder.Append("AffairTitle='" + AM.AffairTitle + "',");
            }
            DateTime date = AM.Date;
            builder.Append("AddDate='" + AM.Date + "',");
            if (AM.Remark != null)
            {
                builder.Append("Remark='" + AM.Remark + "',");
            }
            else
            {
                builder.Append("Remark= null ,");
            }
            if (AM.Context != null)
            {
                builder.Append("Context='" + AM.Context + "',");
            }
            else
            {
                builder.Append("Context= null ,");
            }
            if (AM.Flage != null)
            {
                builder.Append("DatumClass=" + AM.Flage + ",");
            }
            builder.Append("CA=" + AM.CA + ",");
            builder.Append("mark=" + AM.Mark + ",");
            builder.Append("filesType=" + AM.FilesType + ",");
            int startIndex = builder.ToString().LastIndexOf(",");
            builder.Remove(startIndex, 1);
            builder.Append(" where i_id='" + AM.i_id + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

