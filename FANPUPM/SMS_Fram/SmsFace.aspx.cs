using ASP;
using cn.justwin.BLL;
using cn.justwin.SMS;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SMS_Fram_SmsFace : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		this.BtnSave.Attributes["onclick"] = "javascript:if(document.getElementById('txtPersons').value=='') {alert('接收人不能为空!');return false;} else return true;";
		this.txtWriteText.Attributes["onkeyup"] = "showTextAreaStyle()";
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.hddPeoples.Value == "")
		{
			if (this.txtPersons.Text != "")
			{
				string text = this.txtPersons.Text;
				string[] array = text.Split(new char[]
				{
					','
				});
				int num = 0;
				if (!(this.txtWriteText.Text != "") || this.txtWriteText.Text.Length >= 100)
				{
					string innerHtml = "<font color='red' >消息内容不能为空且字数不能多于100字</font><img alt='关闭' src='images/hh.gif' align='right' onclick='downClose()' style='padding-top:5px;padding-right:8px;' id='imgClose'/>";
					this.idRemark.InnerHtml = innerHtml;
					return;
				}
				for (int i = 0; i < array.Length; i++)
				{
					SMS sMS = new SMS();
					bool flag = (bool)sMS.Send("", array[i], this.txtWriteText.Text, "", "", "")[0];
					if (flag)
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					base.RegisterScript("alert('全部发送成功!')");
				}
				if (num < array.Length && num > 0)
				{
					base.RegisterScript("alert('未能全部成功,部分发送失败!非本软件问题!')");
				}
				if (num == 0)
				{
					base.RegisterScript("alert('发送失败,请检查网络设备,稍后再发或另行通知!')");
					return;
				}
			}
		}
		else
		{
			string[] array2 = this.hddPeoples.Value.Split(new char[]
			{
				','
			});
			string[] array3 = new string[array2.Length - 1];
			int num2 = 0;
			for (int j = 0; j < array2.Length - 1; j++)
			{
				string[] array4 = array2[j].Split(new char[]
				{
					'('
				});
				array3[j] = array4[0];
			}
			if (this.txtPersons.Text.Replace(this.hddPeoples.Value, "").Trim() == "")
			{
				if (!(this.txtWriteText.Text != "") || this.txtWriteText.Text.Length >= 100)
				{
					string innerHtml2 = "<font color='red' >消息内容不能为空且字数不能多于100字</font><img alt='关闭' src='images/hh.gif' align='right' onclick='downClose()' style='padding-top:5px;padding-right:8px;' id='imgClose'/>";
					this.idRemark.InnerHtml = innerHtml2;
					return;
				}
				for (int k = 0; k < array3.Length; k++)
				{
					SMS sMS2 = new SMS();
					bool flag2 = (bool)sMS2.Send("", array3[k], this.txtWriteText.Text, "", "", "")[0];
					if (flag2)
					{
						num2++;
					}
				}
				if (num2 == array3.Length)
				{
					base.RegisterScript("alert('全部发送成功!')");
				}
				if (num2 < array3.Length && num2 > 0)
				{
					base.RegisterScript("alert('未能全部成功,部分发送失败!非本软件问题!')");
				}
				if (num2 == 0)
				{
					base.RegisterScript("alert('发送失败,请检查网络设备,稍后再发或另行通知!')");
					return;
				}
			}
			else
			{
				string text2 = this.txtPersons.Text.Replace(this.hddPeoples.Value, "").Trim();
				string[] array5 = text2.Split(new char[]
				{
					','
				});
				string[] array6 = new string[array3.Length + array5.Length];
				array3.CopyTo(array6, 0);
				array5.CopyTo(array6, array3.Length);
				if (this.txtWriteText.Text != "" && this.txtWriteText.Text.Length < 100)
				{
					for (int l = 0; l < array6.Length; l++)
					{
						SMS sMS3 = new SMS();
						bool flag3 = (bool)sMS3.Send("", array6[l], this.txtWriteText.Text, "", "", "")[0];
						if (flag3)
						{
							num2++;
						}
					}
					if (num2 == array6.Length)
					{
						base.RegisterScript("alert('全部发送成功!')");
					}
					if (num2 < array6.Length && num2 > 0)
					{
						base.RegisterScript("alert('未能全部成功,部分发送失败!非本软件问题!')");
					}
					if (num2 == 0)
					{
						base.RegisterScript("alert('发送失败,请检查网络设备,稍后再发或另行通知!')");
						return;
					}
				}
				else
				{
					string innerHtml3 = "<font color='red' >消息内容不能为空且字数不能多于100字</font><img alt='关闭' src='images/hh.gif' align='right' onclick='downClose()' style='padding-top:5px;padding-right:8px;' id='imgClose'/>";
					this.idRemark.InnerHtml = innerHtml3;
				}
			}
		}
	}
}


