namespace cn.justwin.opm.CallBids.Bid
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class BidCorpAction
    {
        public bool AddCallBidsCrop(BidCorpModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into OPM_CallBids_BidCorp(UID,CorpID,CallBidID,ConstructionTime,BidMoney,BidMark,BidDeptAddr,BidTime,BidDept,BidUser,BidUserTel,BidUserMobile,AddUser,AddTime,Remark,I_xh,FlowState,IsValid) values");
            builder.Append("(");
            builder.Append("'" + model.UID + "',");
            builder.Append(model.CorpID + ",");
            builder.Append("'" + model.CallBidID + "',");
            builder.Append("'" + model.ConstructionTime + "',");
            builder.Append("'" + model.BidMoney + "',");
            builder.Append("'" + model.BidMark + "',");
            builder.Append("'" + model.BidDept + "',");
            builder.Append("'" + model.BidTime + "',");
            builder.Append("'" + model.BidDept + "',");
            builder.Append("'" + model.BidUser + "',");
            builder.Append("'" + model.BidUserTel + "',");
            builder.Append("'" + model.BidUserMobile + "',");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append(model.I_xh + ",");
            builder.Append(model.FlowState + ",");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public int CallBidSuccess(string uid, string callbidid, string reason)
        {
            return publicDbOpClass.ExecSqlString(string.Format("update OPM_CallBids set UID='{0}',BidSucTime='{1}',BidSucReason='{2}'  where CallBidID='{3}'", new object[] { uid, DateTime.Now, reason, callbidid }));
        }

        public bool DelCallBidsCrop(string[] guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from  OPM_CallBids_BidCorp where UID in ");
            builder.Append("(");
            for (int i = 0; i < guid.Length; i++)
            {
                builder.Append("'" + guid[i] + "'" + ((i == (guid.Length - 1)) ? "" : ","));
            }
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool EditCallBidsCrop(BidCorpModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OPM_CallBids_BidCorp set ");
            builder.AppendFormat("CorpID={0},", model.CorpID);
            builder.AppendFormat("CallBidID='{0}',", model.CallBidID);
            builder.AppendFormat("ConstructionTime='{0}',", model.ConstructionTime);
            builder.AppendFormat("BidMoney={0},", model.BidMoney);
            builder.AppendFormat("BidMark='{0}',", model.BidMark);
            builder.AppendFormat("BidDeptAddr='{0}',", model.BidDeptAddr);
            builder.AppendFormat("BidTime='{0}',", model.BidTime);
            builder.AppendFormat("BidDept='{0}',", model.BidDept);
            builder.AppendFormat("BidUser='{0}',", model.BidUser);
            builder.AppendFormat("BidUserTel='{0}',", model.BidUserTel);
            builder.AppendFormat("BidUserMobile='{0}',", model.BidUserMobile);
            builder.AppendFormat("AddUser='{0}',", model.AddUser);
            builder.AppendFormat("AddTime='{0}',", model.AddTime);
            builder.AppendFormat("Remark='{0}',", model.Remark);
            builder.AppendFormat("I_xh={0},", model.I_xh);
            builder.AppendFormat("FlowState={0},", model.FlowState);
            builder.AppendFormat("IsValid='{0}'", model.IsValid);
            builder.AppendFormat(" where UID='{0}'", model.UID);
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public DataTable GetCallBidsCrop()
        {
            string sqlString = " select OPM_CallBids.UID CUID,OPM_CallBids.CallBidName,OPM_CallBids.CallBidCode,PT_PrjInfo.PrjName,XPM_Basic_ContactCorp.CorpName,OPM_CallBids.prjID,OPM_CallBids_BidCorp.*  from OPM_CallBids,PT_PrjInfo,OPM_CallBids_BidCorp,XPM_Basic_ContactCorp  where OPM_CallBids.CallBidID=OPM_CallBids_BidCorp.CallBidID  and OPM_CallBids.prjID=PT_PrjInfo.PrjGuid  and XPM_Basic_ContactCorp.CorpID=OPM_CallBids_BidCorp.CorpID";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetCallBidsCropByCallId(Guid callbidid)
        {
            return publicDbOpClass.DataTableQuary(" select OPM_CallBids.UID CUID,OPM_CallBids.CallBidName,OPM_CallBids.CallBidCode,PT_PrjInfo.PrjName,XPM_Basic_ContactCorp.CorpName,OPM_CallBids.prjID,OPM_CallBids_BidCorp.*  from OPM_CallBids,PT_PrjInfo,OPM_CallBids_BidCorp,XPM_Basic_ContactCorp  where OPM_CallBids.CallBidID=OPM_CallBids_BidCorp.CallBidID  and OPM_CallBids.prjID=PT_PrjInfo.PrjGuid  and XPM_Basic_ContactCorp.CorpID=OPM_CallBids_BidCorp.CorpID and OPM_CallBids_BidCorp.CallBidID='" + callbidid + "' ");
        }

        public DataTable GetCallBidsCropByUId(Guid uid)
        {
            return publicDbOpClass.DataTableQuary(" select OPM_CallBids.BidSucTime,OPM_CallBids.BidSucReason,OPM_CallBids.UID CUID,OPM_CallBids.CallBidName,OPM_CallBids.CallBidCode,PT_PrjInfo.PrjName,XPM_Basic_ContactCorp.CorpName,OPM_CallBids.prjID,OPM_CallBids_BidCorp.*  from OPM_CallBids,PT_PrjInfo,OPM_CallBids_BidCorp,XPM_Basic_ContactCorp  where OPM_CallBids.CallBidID=OPM_CallBids_BidCorp.CallBidID  and OPM_CallBids.prjID=PT_PrjInfo.PrjGuid  and XPM_Basic_ContactCorp.CorpID=OPM_CallBids_BidCorp.CorpID and OPM_CallBids_BidCorp.UID='" + uid + "' ");
        }

        public bool IsCallBidsCrop(int cropid, Guid callbidid)
        {
            return (publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select * from OPM_CallBids_BidCorp where CropId=", cropid, " and CallBidId='", callbidid, "'  " })).Rows.Count > 0);
        }

        public DataTable QueryCallBidsCrop(string callbidscode, string callBidName, string prjname, string callBidDutyMan, string bidbegintime, string bidendtime)
        {
            DateTime time;
            DateTime time2;
            StringBuilder builder = new StringBuilder();
            builder.Append(" select OPM_CallBids.UID CUID,OPM_CallBids.CallBidName,OPM_CallBids.CallBidCode,PT_PrjInfo.PrjName,XPM_Basic_ContactCorp.CorpName,OPM_CallBids.prjID,OPM_CallBids_BidCorp.* from OPM_CallBids,PT_PrjInfo,OPM_CallBids_BidCorp,XPM_Basic_ContactCorp  ");
            builder.Append(" where OPM_CallBids.CallBidID=OPM_CallBids_BidCorp.CallBidID and OPM_CallBids.prjID=PT_PrjInfo.PrjGuid and XPM_Basic_ContactCorp.CorpID=OPM_CallBids_BidCorp.CorpID ");
            if (!callbidscode.Equals(string.Empty))
            {
                builder.AppendFormat(" and OPM_CallBids.CallBidCode like'%{0}%' ", callbidscode);
            }
            if (!callBidName.Equals(string.Empty))
            {
                builder.AppendFormat(" and OPM_CallBids.CallBidName like '%{0}%' ", callBidName);
            }
            if (!prjname.Equals(string.Empty))
            {
                builder.AppendFormat(" and PT_PrjInfo.PrjName like '%{0}%' ", prjname);
            }
            if (!callBidDutyMan.Equals(string.Empty))
            {
                builder.AppendFormat(" and OPM_CallBids_BidCorp.BidUser like '%{0}%' ", callBidDutyMan);
            }
            if ((bidbegintime != "") && DateTime.TryParse(bidbegintime, out time))
            {
                builder.AppendFormat(" and OPM_CallBids_BidCorp.BidTime >= '{0}' ", bidbegintime);
            }
            if ((bidendtime != "") && DateTime.TryParse(bidendtime, out time2))
            {
                builder.AppendFormat(" and OPM_CallBids_BidCorp.BidTime <= '{0}' ", bidendtime);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }
    }
}

