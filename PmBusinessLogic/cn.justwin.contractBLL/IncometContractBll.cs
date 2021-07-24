namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncometContractBll
    {
        private IncometContract incometContract = new IncometContract();

        public int Add(IncometContractModel model)
        {
            return this.incometContract.Add(model);
        }

        public int Delete(SqlTransaction trans, string ContractID)
        {
            return this.incometContract.Delete(trans, ContractID);
        }

        public int GetCountByParam(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string project, string userName, string ownerNam)
        {
            List<SqlParameter> listParm = new List<SqlParameter>();
            string strWhere = this.getQueryCondition(contractCode, contractName, type, startSignedTime, endSignedTime, startContractPrice, endContractPrice, project, userName, ownerNam, listParm);
            return this.incometContract.GetCountByParam(strWhere, listParm.ToArray());
        }

        public DataTable GetInfoByParam(string contractId, string tbName, string userCodes, string strWhere)
        {
            return this.incometContract.GetInfoByParam(contractId, tbName, userCodes, strWhere);
        }

        public List<IncometContractModel> GetListArray(string strWhere)
        {
            return this.incometContract.GetListArray(strWhere);
        }

        public IncometContractModel GetModel(string ContractID)
        {
            return this.incometContract.GetModel(ContractID);
        }

        private string getQueryCondition(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string project, string userName, string ownerNam, List<SqlParameter> listParm)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                builder.Append(" AND p1.contractCode LIKE '%'+ @contractCode +'%' ");
                listParm.Add(new SqlParameter("@contractCode", contractCode));
            }
            if (!string.IsNullOrWhiteSpace(contractName))
            {
                builder.Append(" AND p1.contractName LIKE '%'+ @contractName + '%' ");
                listParm.Add(new SqlParameter("@contractName", contractName));
            }
            if (!string.IsNullOrEmpty(type))
            {
                builder.Append(" AND p3.TypeName LIKE '%'+ @type +'%'");
                listParm.Add(new SqlParameter("@type", type));
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" AND p2.prjName LIKE '%' + @project+ '%'");
                listParm.Add(new SqlParameter("@project", project));
            }
            if (!string.IsNullOrEmpty(startSignedTime))
            {
                builder.Append(" AND p1.SignedTime >=@startSignedTime");
                listParm.Add(new SqlParameter("@startSignedTime", startSignedTime));
            }
            if (!string.IsNullOrEmpty(endSignedTime))
            {
                builder.Append(" AND p1.SignedTime<=@endSignedTime");
                listParm.Add(new SqlParameter("@endSignedTime", endSignedTime));
            }
            if (!string.IsNullOrEmpty(startContractPrice))
            {
                startContractPrice = startContractPrice.Replace(",", "");
                builder.Append(" AND p1.ContractPrice>=@startContractPrice");
                listParm.Add(new SqlParameter("@startContractPrice", startContractPrice));
            }
            if (!string.IsNullOrEmpty(endContractPrice))
            {
                endContractPrice = endContractPrice.Replace(",", "");
                builder.Append(" AND p1.ContractPrice<=@endContractPrice");
                listParm.Add(new SqlParameter("@endContractPrice", endContractPrice));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%'");
                listParm.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(ownerNam))
            {
                builder.Append(" AND Party LIKE '%'+@ownerName+'%'");
                listParm.Add(new SqlParameter("@ownerName", ownerNam));
            }
            return builder.ToString();
        }

        public DataTable GetReportTb(string contractType, string project, string contractCode, string contractName, string startTime, string endTime, string Party, string tbName, string strWhere, string prjTypeCode)
        {
            return this.incometContract.GetReportTb(contractType, project, contractCode, contractName, startTime, endTime, Party, tbName, strWhere, prjTypeCode);
        }

        public DataTable GetReportTb(string contractType, string project, string contractCode, string contractName, string startTime, string endTime, string Party, string tbName, string strWhere, string prjTypeCode, int pageIndex, int pageSize)
        {
            return this.incometContract.GetReportTb(contractType, project, contractCode, contractName, startTime, endTime, Party, tbName, strWhere, prjTypeCode, pageIndex, pageSize);
        }

        public DataTable GetSelectContract(string contractCode, string contractName, string userCode, string isAudit)
        {
            return this.incometContract.GetSelectContract(contractCode, contractName, userCode, isAudit);
        }

        public DataTable GetTbByParam(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string project, string userCodes, string strWhere)
        {
            contractCode = contractCode.Trim();
            contractName = contractName.Trim();
            project = project.Trim();
            userCodes = userCodes.Trim();
            return this.incometContract.GetTbByParam(contractCode, contractName, type, startSignedTime, endSignedTime, startContractPrice, endContractPrice, project, userCodes, strWhere);
        }

        public DataTable GetTbByParam(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string project, string userName, string ownerNam, int pageSize, int pageNo)
        {
            DataTable table = new DataTable();
            List<SqlParameter> listParm = new List<SqlParameter>();
            string strWhere = this.getQueryCondition(contractCode, contractName, type, startSignedTime, endSignedTime, startContractPrice, endContractPrice, project, userName, ownerNam, listParm);
            return this.incometContract.GetTbByParam(strWhere, listParm.ToArray(), pageSize, pageNo);
        }

        public DataTable GetTbByParamSort(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string project, string userCodes, string strWhere, int pageIndex, int pageSize)
        {
            contractCode = contractCode.Trim();
            contractName = contractName.Trim();
            project = project.Trim();
            userCodes = userCodes.Trim();
            return this.incometContract.GetTbByParam(contractCode, contractName, type, startSignedTime, endSignedTime, startContractPrice, endContractPrice, project, userCodes, strWhere, pageIndex, pageSize);
        }

        public bool IsDel(string ContractID)
        {
            return this.incometContract.IsDel(ContractID);
        }

        public int Update(IncometContractModel model)
        {
            return this.incometContract.Update(model);
        }

        public void updateConState(List<string> contractIds, int state)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                Action<string> action = null;
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    if (action == null)
                    {
                        action = delegate (string id) {
                            string sql = string.Concat(new object[] { "update Con_Incomet_Contract set conState=", state, " where contractid='", id, "'" });
                            this.incometContract.UpConState(sql, trans);
                        };
                    }
                    contractIds.ForEach(action);
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw new Exception("状态修改失败");
                }
            }
        }
    }
}

