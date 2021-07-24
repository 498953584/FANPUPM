using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.UI.WebControls;

public class ProjectTree
{
    public void BindDlistYears(DropDownList ddlYear, object pmSet, string userCode, string selectedYearValue)
    {
        DataTable prjYears = null;
        if (pmSet == null)
        {
            throw new ArgumentNullException("系统登录员未记录session", "null转换字符串!");
        }
        if (pmSet.ToString() == "1")
        {
            prjYears = PMAction.GetPrjYears(userCode);
        }
        else
        {
            prjYears = PMAction.GetProjectYears();
        }
        if (prjYears.Rows.Count > 0)
        {
            int num = (prjYears.Rows[0]["BeginYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(prjYears.Rows[0]["BeginYear"].ToString());
            int num2 = (prjYears.Rows[0]["EndYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(prjYears.Rows[0]["EndYear"].ToString());
            for (int i = num; i <= num2; i++)
            {
                string text = i + "年度";
                ddlYear.Items.Add(new ListItem(text, i.ToString()));
            }
        }
        try
        {
            if (string.IsNullOrEmpty(selectedYearValue))
            {
                ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            }
            else
            {
                ddlYear.SelectedValue = selectedYearValue;
            }
        }
        catch
        {
        }
    }

    public void BindProject(TreeView trvw, string pmSet, string userCode, string year)
    {
    }

    private void BindTreeByType(TreeNode rootNode, string userCode, string selectedYearValue, string selectedTreeViewValue, bool isNewProject, string prjState)
    {
        TreeNode node = null;
        int selyear = int.Parse(selectedYearValue);
        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        if (string.IsNullOrEmpty(prjState))
        {
            table = PMAction.GetPrjState(userCode, selyear, isNewProject);
            table2 = PMAction.GetProject(userCode, selyear, isNewProject);
        }
        else
        {
            table = PMAction.GetPrjState(userCode, selyear, prjState);
            table2 = PMAction.GetProject(userCode, selyear, prjState);
        }
        if (table.Rows.Count > 0)
        {
            foreach (DataRow row in table.Rows)
            {
                TreeNode child = new TreeNode {
                    Text = row["prjstateName"].ToString(),
                    Value = row["PrjState"].ToString(),
                    SelectAction = TreeNodeSelectAction.None
                };
                rootNode.ChildNodes.Add(child);
                foreach (DataRow row2 in table2.Select("PrjState=" + row["PrjState"].ToString()))
                {
                    TreeNode node3 = new TreeNode(row2["PrjName"].ToString(), row2["PrjGuid"].ToString()) {
                        ToolTip = row2["PrjGuid"].ToString()
                    };
                    child.ChildNodes.Add(node3);
                    if ((node == null) && string.IsNullOrEmpty(selectedTreeViewValue))
                    {
                        node = node3;
                    }
                    else if (string.Compare(node3.Value, selectedTreeViewValue, true) == 0)
                    {
                        node3.Select();
                    }
                }
            }
            if (node != null)
            {
                node.Selected = true;
            }
        }
    }

    public void BindTreeNodes(TreeView tvProject, object pmSet, string userCode, string selectedTreeViewValue, string selectedYearText, string selectedYearValue)
    {
        tvProject.Nodes.Clear();
        string str = ConfigHelper.Get("IsNewProject");
        TreeNode child = new TreeNode(selectedYearText + "年度所有项目", string.Empty) {
            SelectAction = TreeNodeSelectAction.None
        };
        tvProject.Nodes.Add(child);
        if (pmSet == null)
        {
            throw new ArgumentNullException("系统登录员未记录session", "null转换字符串!");
        }
        if (pmSet.ToString() == "1")
        {
            this.BindTreeByType(child, userCode, selectedYearValue, selectedTreeViewValue, str != "0", null);
        }
        else
        {
            this.BindTreesByRank(child, userCode, selectedYearValue, selectedTreeViewValue, null);
        }
        tvProject.ExpandAll();
    }

    public void BindTreeNodes(TreeView tvProject, object pmSet, string userCode, string selectedTreeViewValue, string selectedYearText, string selectedYearValue, string prjState)
    {
        tvProject.Nodes.Clear();
        TreeNode child = new TreeNode(selectedYearText + "所有项目", string.Empty) {
            SelectAction = TreeNodeSelectAction.None
        };
        tvProject.Nodes.Add(child);
        if (pmSet == null)
        {
            throw new ArgumentNullException("系统登录员未记录session", "null转换字符串!");
        }
        if (pmSet.ToString() == "1")
        {
            this.BindTreeByType(child, userCode, selectedYearValue, selectedTreeViewValue, true, prjState);
        }
        else
        {
            this.BindTreesByRank(child, userCode, selectedYearValue, selectedTreeViewValue, prjState);
        }
        tvProject.ExpandAll();
    }

    private void BindTreesByRank(TreeNode rootNode, string userCode, string selectedYearValue, string selectedTreeViewValue, string prjState)
    {
        TreeNode node = null;
        DataTable table = PMAction.GetPrjsubTreebyUserandYear(userCode, int.Parse(selectedYearValue), prjState);
        DataRow[] rowArray = table.Select("LEN(TypeCode)=5  ", "  StartDate DESC");
        for (int i = 0; i < rowArray.Length; i++)
        {
            TreeNode child = new TreeNode {
                ToolTip = rowArray[i]["PrjGuid"].ToString(),
                Text = rowArray[i]["PrjName"].ToString()
            };
            if (rowArray[i]["Permission"].ToString() != "0")
            {
                if (((((rowArray[i]["SetUpFlowState"].ToString() == "1") && (rowArray[i]["PrjState"].ToString() != "17")) && ((rowArray[i]["PrjState"].ToString() != "1") && (rowArray[i]["PrjState"].ToString() != "2"))) && (((rowArray[i]["PrjState"].ToString() != "3") && (rowArray[i]["PrjState"].ToString() != "4")) && ((rowArray[i]["PrjState"].ToString() != "6") && (rowArray[i]["PrjState"].ToString() != "14")))) && (((rowArray[i]["PrjState"].ToString() != "15") && (rowArray[i]["PrjState"].ToString() != "16")) && (rowArray[i]["PrjState"].ToString() != "18")))
                {
                    child.Value = rowArray[i]["PrjGuid"].ToString();
                    if ((node == null) && string.IsNullOrEmpty(selectedTreeViewValue))
                    {
                        node = child;
                    }
                    else if (string.Compare(child.Value, selectedTreeViewValue, true) == 0)
                    {
                        child.Select();
                    }
                }
                else
                {
                    child.SelectAction = TreeNodeSelectAction.None;
                    child.ToolTip = "无权限";
                    child.Value = string.Empty;
                }
            }
            else
            {
                child.SelectAction = TreeNodeSelectAction.None;
                child.ToolTip = "无权限";
                child.Value = string.Empty;
            }
            rootNode.ChildNodes.Add(child);
            DataRow[] rowArray2 = table.Select("TypeCode LIKE '" + rowArray[i]["TypeCode"].ToString() + "%' AND LEN(TypeCode)=10 ", "  StartDate DESC");
            for (int j = 0; j < rowArray2.Length; j++)
            {
                TreeNode node3 = new TreeNode {
                    ToolTip = rowArray2[j]["PrjGuid"].ToString(),
                    Text = rowArray2[j]["PrjName"].ToString()
                };
                if (rowArray2[j]["Permission"].ToString() != "0")
                {
                    if (((((rowArray2[j]["SetUpFlowState"].ToString() == "1") && (rowArray2[j]["PrjState"].ToString() != "17")) && ((rowArray2[j]["PrjState"].ToString() != "1") && (rowArray2[j]["PrjState"].ToString() != "2"))) && (((rowArray2[j]["PrjState"].ToString() != "3") && (rowArray2[j]["PrjState"].ToString() != "4")) && ((rowArray2[j]["PrjState"].ToString() != "6") && (rowArray2[j]["PrjState"].ToString() != "14")))) && (((rowArray2[j]["PrjState"].ToString() != "15") && (rowArray2[j]["PrjState"].ToString() != "16")) && (rowArray2[j]["PrjState"].ToString() != "18")))
                    {
                        node3.Value = rowArray2[j]["PrjGuid"].ToString();
                        if ((node == null) && string.IsNullOrEmpty(selectedTreeViewValue))
                        {
                            node = node3;
                        }
                        else if (string.Compare(node3.Value, selectedTreeViewValue, true) == 0)
                        {
                            node3.Select();
                        }
                    }
                    else
                    {
                        child.SelectAction = TreeNodeSelectAction.None;
                        child.ToolTip = "无权限";
                        child.Value = string.Empty;
                    }
                }
                else
                {
                    node3.SelectAction = TreeNodeSelectAction.None;
                    node3.ToolTip = "无权限";
                    node3.Value = string.Empty;
                }
                child.ChildNodes.Add(node3);
            }
        }
        if (node != null)
        {
            node.Select();
        }
    }
}

