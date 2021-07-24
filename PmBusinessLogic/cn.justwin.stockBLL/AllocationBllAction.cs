namespace cn.justwin.stockBLL
{
    using cn.justwin.BLL;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class AllocationBllAction
    {
        public int DelAllocationStock(string acode)
        {
            int num = 0;
            if (acode != "")
            {
                num = AllocationAction.DelAllocationStock(acode);
            }
            return num;
        }

        public int DelAllocationStockByAcode(SqlTransaction trans, string acode)
        {
            int num = 0;
            if (acode != "")
            {
                num = AllocationAction.DelAllocationStockByAcode(trans, acode);
            }
            return num;
        }

        public int Delete(SqlTransaction trans, string acode)
        {
            int num = 0;
            if (acode != "")
            {
                num = AllocationAction.Delete(trans, acode);
            }
            return num;
        }

        public int ExistSameData(string acode, string scode, string corp, decimal price)
        {
            int num = 0;
            if ((acode != "") && (corp != ""))
            {
                object obj2 = AllocationAction.ExistData(acode, scode, corp, price);
                if (obj2 != null)
                {
                    num = int.Parse(obj2.ToString());
                }
            }
            return num;
        }

        public DataTable GetAllocationList(string strWhere)
        {
            return AllocationAction.GetAllocationList(strWhere);
        }

        public DataTable GetAllocationStockList(string depositoryId, string strWhere)
        {
            return AllocationAction.GetAllocationStockList(depositoryId, strWhere);
        }

        public DataTable GetMaterialOfDepositoryList(string depositoryId, string strWhere)
        {
            DataTable materialOfDepositoryList = new DataTable();
            if (depositoryId != "")
            {
                materialOfDepositoryList = AllocationAction.GetMaterialOfDepositoryList(depositoryId, strWhere);
            }
            return materialOfDepositoryList;
        }

        public DataTable GetTreasuryList(int tflag)
        {
            DataTable table = new DataTable();
            if (!(tflag.ToString() != ""))
            {
                return table;
            }
            if (tflag != -1)
            {
                return AllocationAction.GetTreasuryList(tflag.ToString());
            }
            return AllocationAction.GetTreasuryList(null);
        }

        public DataTable GetTreasuryNameByCode(string code)
        {
            DataTable treasuryNameByCode = new DataTable();
            if (code != "")
            {
                treasuryNameByCode = AllocationAction.GetTreasuryNameByCode(code);
            }
            return treasuryNameByCode;
        }

        public string GetUserNameByCode(string code)
        {
            object userNameByCode = null;
            if (code != "")
            {
                userNameByCode = AllocationAction.GetUserNameByCode(code);
            }
            if (userNameByCode != null)
            {
                return userNameByCode.ToString();
            }
            return "";
        }

        public int InDepositoryConfirm(string acode, string yhdm)
        {
            DataTable table = AllocationAction.GetAllocation_StockList("sma.acode='" + acode + "' and flowstate=1 and isouta=1 ");
            int num = 0;
            TreasuryPermitBll bll = new TreasuryPermitBll();
            AllocationModel model = new AllocationModel();
            model = this.ReturnAllocatonModel(" acode='" + acode + "' ");
            if (!bll.IsPermitAccept(model.Acode, yhdm))
            {
                return 0;
            }
            if (table.Rows.Count > 0)
            {
                TreasuryStock stock = new TreasuryStock();
                TreasuryStockModel model2 = new TreasuryStockModel();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    model2.corp = table.Rows[i]["corp"].ToString();
                    model2.incode = acode;
                    model2.intime = DateTime.Now;
                    model2.intype = 0;
                    model2.Type = "A";
                    model2.isfirst = false;
                    model2.scode = table.Rows[i]["scode"].ToString();
                    model2.snumber = decimal.Parse(table.Rows[i]["number"].ToString());
                    model2.sprice = decimal.Parse(table.Rows[i]["sprice"].ToString());
                    model2.tcode = table.Rows[i]["tcodeb"].ToString();
                    model2.tsid = Guid.NewGuid().ToString();
                    if (stock.Add(model2) <= 0)
                    {
                        return -1;
                    }
                    AllocationAction.UpdateState(true, true, acode, "In");
                    num = 1;
                }
            }
            return num;
        }

        public int Insert(SqlTransaction trans, AllocationModel model)
        {
            int num = 0;
            if (model.Acode != "")
            {
                num = AllocationAction.Insert(trans, model);
            }
            return num;
        }

        public int Insert(SqlTransaction trans, AllocationStockModel stockModel)
        {
            int num = 0;
            if ((stockModel.ACode != "") && (stockModel.SCode != ""))
            {
                num = AllocationAction.Insert(trans, stockModel);
            }
            return num;
        }

        public int OutDepositoryConfirm(string acode, string yhdm)
        {
            DataTable table = AllocationAction.GetAllocation_StockList("sma.acode='" + acode + "' and flowstate=1 ");
            DataTable materialDetailsOfDeposity = new DataTable();
            int num = 0;
            decimal num2 = 0M;
            TreasuryPermitBll bll = new TreasuryPermitBll();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                decimal num4 = 0M;
                bool flag = bll.IsPermitBool(table.Rows[i]["tcodea"].ToString(), yhdm);
                object materialNumberOfDepository = AllocationAction.GetMaterialNumberOfDepository(table.Rows[i]["tcodea"].ToString(), " scode='" + table.Rows[i]["scode"].ToString() + "' and sprice='" + table.Rows[i]["sprice"].ToString() + "' and corp='" + table.Rows[i]["corp"].ToString() + "' ");
                if (!flag)
                {
                    num = -3;
                    break;
                }
                if (materialNumberOfDepository != null)
                {
                    num4 = decimal.Parse(materialNumberOfDepository.ToString());
                }
                num2 = decimal.Parse(table.Rows[i]["number"].ToString());
                if (num4 < num2)
                {
                    num = -2;
                    break;
                }
            }
            switch (num)
            {
                case -2:
                case -3:
                    return num;

                default:
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        decimal num6 = 0M;
                        num6 = decimal.Parse(table.Rows[j]["number"].ToString());
                        materialDetailsOfDeposity = AllocationAction.GetMaterialDetailsOfDeposity("tcode='" + table.Rows[j]["tcodea"].ToString() + "' and scode='" + table.Rows[j]["scode"].ToString() + "' and sprice='" + table.Rows[j]["sprice"].ToString() + "' and corp='" + table.Rows[j]["corp"].ToString() + "' order by intime asc ");
                        for (int k = 0; k < materialDetailsOfDeposity.Rows.Count; k++)
                        {
                            decimal num8 = 0M;
                            num8 = decimal.Parse(materialDetailsOfDeposity.Rows[k]["snumber"].ToString());
                            if (num8 > num6)
                            {
                                if (AllocationAction.UpdateNumberOfTreasury(materialDetailsOfDeposity.Rows[k]["tsid"].ToString(), num8 - num6) <= 0)
                                {
                                    num = -1;
                                }
                                else
                                {
                                    Common2.AlarmMethod(table.Rows[j]["tcodea"].ToString(), table.Rows[j]["scode"].ToString());
                                }
                                break;
                            }
                            if (num8 == num6)
                            {
                                if (new TreasuryStock().Delete(materialDetailsOfDeposity.Rows[k]["tsid"].ToString()) <= 0)
                                {
                                    num = -1;
                                }
                                else
                                {
                                    Common2.AlarmMethod(table.Rows[j]["tcodea"].ToString(), table.Rows[j]["scode"].ToString());
                                }
                                break;
                            }
                            if (num8 < num6)
                            {
                                if (new TreasuryStock().Delete(materialDetailsOfDeposity.Rows[k]["tsid"].ToString()) <= 0)
                                {
                                    num = -1;
                                    break;
                                }
                                num6 -= num8;
                                Common2.AlarmMethod(table.Rows[j]["tcodea"].ToString(), table.Rows[j]["scode"].ToString());
                            }
                        }
                        if (num == -1)
                        {
                            break;
                        }
                        num = 1;
                    }
                    break;
            }
            if (num == 1)
            {
                AllocationAction.UpdateState(true, false, acode, "Out");
            }
            return num;
        }

        public AllocationModel ReturnAllocatonModel(string strWhere)
        {
            AllocationModel model = new AllocationModel();
            DataTable allocationList = new DataTable();
            allocationList = AllocationAction.GetAllocationList(strWhere);
            if (allocationList.Rows.Count > 0)
            {
                model.Aid = allocationList.Rows[0]["aid"].ToString();
                model.Acode = allocationList.Rows[0]["acode"].ToString();
                model.TCodea = allocationList.Rows[0]["tcodea"].ToString();
                model.TCodeb = allocationList.Rows[0]["tcodeb"].ToString();
                model.IsOutA = Convert.ToBoolean(allocationList.Rows[0]["isouta"].ToString());
                model.IsInB = Convert.ToBoolean(allocationList.Rows[0]["isinb"].ToString());
                model.Person = allocationList.Rows[0]["person"].ToString();
                model.InTime = allocationList.Rows[0]["intime"].ToString();
                model.Explain = allocationList.Rows[0]["explain"].ToString();
                model.OutAllocationPerson = allocationList.Rows[0]["OutAllocationPerson"].ToString();
                model.InAllocationPerson = allocationList.Rows[0]["InAllocationPerson"].ToString();
            }
            return model;
        }

        public int Update(AllocationStockModel stockModel)
        {
            int num = 0;
            if (stockModel.Asid != "")
            {
                num = AllocationAction.Update(stockModel);
            }
            return num;
        }

        public int Update(SqlTransaction trans, AllocationModel model)
        {
            int num = 0;
            if (model.Acode != "")
            {
                num = AllocationAction.Update(trans, model);
            }
            return num;
        }
    }
}

