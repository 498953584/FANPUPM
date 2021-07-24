namespace cn.justwin.stockBLL.Contarct
{
    using cn.justwin.stockModel;
    using System;

    public class ContractMain
    {
        private readonly ContractMain contractMain = new ContractMain();

        public ContractMainModel GetModel(string contractCode)
        {
            return this.contractMain.GetModel(contractCode);
        }
    }
}

