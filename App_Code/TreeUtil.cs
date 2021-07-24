using System;
using System.Collections;

public class TreeUtil
{
    private static void SyncTreeNodes(ArrayList nodes, int outlineLevel, string outlineNumber, string childrenField)
    {
        int num = 0;
        int count = nodes.Count;
        while (num < count)
        {
            Hashtable hashtable = nodes[num] as Hashtable;
            hashtable["OutlineLevel"] = outlineLevel;
            hashtable["OutlineNumber"] = outlineNumber + (num + 1);
            ArrayList list = (ArrayList) hashtable[childrenField];
            if ((list != null) && (list.Count > 0))
            {
                SyncTreeNodes(list, outlineLevel + 1, hashtable["OutlineNumber"].ToString() + ".", childrenField);
            }
            num++;
        }
    }

    public static ArrayList ToList(ArrayList tree, string parentId, string childrenField, string idField, string parentIdField)
    {
        ArrayList list = new ArrayList();
        int num = 0;
        int count = tree.Count;
        while (num < count)
        {
            Hashtable hashtable = (Hashtable) tree[num];
            hashtable[parentIdField] = parentId;
            list.Add(hashtable);
            ArrayList list2 = (ArrayList) hashtable[childrenField];
            if ((list2 != null) && (list2.Count > 0))
            {
                ArrayList c = ToList(list2, hashtable[idField].ToString(), childrenField, idField, parentIdField);
                list.AddRange(c);
            }
            hashtable.Remove(childrenField);
            num++;
        }
        return list;
    }

    public static ArrayList ToTree(ArrayList table, string childrenField, string idField, string parentIdField)
    {
        ArrayList nodes = new ArrayList();
        Hashtable hashtable = new Hashtable();
        int num = 0;
        int count = table.Count;
        while (num < count)
        {
            Hashtable hashtable2 = (Hashtable) table[num];
            hashtable[hashtable2[idField]] = hashtable2;
            num++;
        }
        int num3 = 0;
        int num4 = table.Count;
        while (num3 < num4)
        {
            Hashtable hashtable3 = (Hashtable) table[num3];
            object obj2 = hashtable3[parentIdField];
            if ((obj2 == null) || (obj2.ToString() == "-1"))
            {
                nodes.Add(hashtable3);
            }
            else
            {
                Hashtable hashtable4 = (Hashtable) hashtable[obj2];
                if (hashtable4 == null)
                {
                    nodes.Add(hashtable3);
                }
                else
                {
                    ArrayList list2 = (ArrayList) hashtable4[childrenField];
                    if (list2 == null)
                    {
                        list2 = new ArrayList();
                        hashtable4[childrenField] = list2;
                    }
                    list2.Add(hashtable3);
                }
            }
            num3++;
        }
        SyncTreeNodes(nodes, 1, "", childrenField);
        return nodes;
    }
}

