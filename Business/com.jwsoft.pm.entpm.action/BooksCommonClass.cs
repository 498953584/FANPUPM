namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class BooksCommonClass
    {
        public static string GetBookAuthor(string strSIBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 Author from OA_Books_Storage ");
            if (strSIBN.Trim() != "")
            {
                builder.Append(" where ISBN='" + strSIBN + "' ");
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static DataTable GetBookInfo(string isbn)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_Books_Storage ");
            if (isbn.Trim() != "")
            {
                builder.Append(" where ISBN='" + isbn + "' ");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static int GetBookLeaveNum(string strSIBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sum(isnull(LeaveCopy,0)) from OA_Books_Storage ");
            if (strSIBN.Trim() != "")
            {
                builder.Append(" where ISBN='" + strSIBN + "' ");
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (int) obj2;
            }
            return 0;
        }

        public static DataTable GetBooksClass()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from PT_MultiClasses where ClassTypeCode='001' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetBooksClass(string ClassTypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from PT_MultiClasses where ClassTypeCode='" + ClassTypeCode + "' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static int GetBookTotalNum(string strSIBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sum(isnull(SumCopy,0)) from OA_Books_Storage ");
            if (strSIBN.Trim() != "")
            {
                builder.Append(" where ISBN='" + strSIBN + "' ");
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (int) obj2;
            }
            return 0;
        }

        public static string GetDepartmentName(int intDeptCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select V_BMMC from PT_d_bm where i_bmdm = '" + intDeptCode + "'");
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetDepartmentName(string deptCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select CorpName from PT_d_CorpCode where CorpCode = '" + deptCode + "'");
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetLibraryName(string lc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select LibraryName FROM OA_Books_Library ");
            builder.Append(" where LibraryCode='" + lc + "' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetUserCode(string yhmc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 v_yhdm from PT_yhmc ");
            builder.Append(" where v_xm='" + yhmc + "' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetUserName(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 v_xm from PT_yhmc ");
            builder.Append(" where v_yhdm='" + yhdm + "' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }
    }
}

