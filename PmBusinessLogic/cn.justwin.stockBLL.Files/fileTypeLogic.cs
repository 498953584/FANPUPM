namespace cn.justwin.stockBLL.Files
{
    using cn.justwin.Files;
    using System;

    public class fileTypeLogic
    {
        private readonly fileTypeService dal = new fileTypeService();

        public bool veriFileType(int tid, int _typeid)
        {
            return this.dal.veriFileType(tid, _typeid);
        }
    }
}

