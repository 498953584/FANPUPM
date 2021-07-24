namespace cn.justwin.Domain.Services
{
    using cn.justwin.DAL;
    using cn.justwin.Domain;
    using System;
    using System.Linq;

    public class DepartmentServices
    {
        public cn.justwin.Domain.Department GetById(int id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                cn.justwin.Domain.Department department = new cn.justwin.Domain.Department();
                PT_d_bm _bm = (from d in entities.PT_d_bm
                    where d.i_bmdm == id
                    select d).FirstOrDefault<PT_d_bm>();
                if (_bm != null)
                {
                    department.DepartmentId = _bm.i_bmdm;
                    department.DepartmentName = _bm.V_BMMC;
                }
                return department;
            }
        }
    }
}

