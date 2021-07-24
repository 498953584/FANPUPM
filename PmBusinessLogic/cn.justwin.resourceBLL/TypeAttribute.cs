namespace cn.justwin.resourceBLL
{
    using cn.justwin.DAL;
    using System;
    using System.Linq;

    public class TypeAttribute
    {
        private string resourctTypeId = string.Empty;

        public TypeAttribute(string resourceTypeId)
        {
            this.resourctTypeId = resourceTypeId;
        }

        public bool IsExists(string attrName, string attrId)
        {
            int num = 0;
            using (pm2Entities entities = new pm2Entities())
            {
                if (string.IsNullOrEmpty(attrId))
                {
                    num = (from a in entities.Res_Attribute
                        where (a.Res_ResourceType.ResourceTypeId == this.resourctTypeId) && (a.AttributeName == attrName)
                        select a).Count<Res_Attribute>();
                }
                else
                {
                    num = (from a in entities.Res_Attribute
                        where ((a.Res_ResourceType.ResourceTypeId == this.resourctTypeId) && (a.AttributeName == attrName)) && (a.AttributeId != attrId)
                        select a).Count<Res_Attribute>();
                }
            }
            return (num > 0);
        }
    }
}

