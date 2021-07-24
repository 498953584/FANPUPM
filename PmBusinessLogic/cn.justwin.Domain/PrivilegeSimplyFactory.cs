namespace cn.justwin.Domain
{
    using Spring.Context;
    using Spring.Context.Support;
    using System;

    public static class PrivilegeSimplyFactory
    {
        public static IPrivilege CreatePrivilege(string userCode, string relationsTable)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            object[] arguments = new object[] { userCode, relationsTable };
            return (context.GetObject("Privilege", arguments) as IPrivilege);
        }
    }
}

