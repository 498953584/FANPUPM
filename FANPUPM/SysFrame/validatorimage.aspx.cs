using ASP;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
	public partial class ValidatorImage : Page, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.CreateCheckCodeImage(this.GenerateCheckCode());
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private string GenerateCheckCode()
		{
			string text = string.Empty;
			Random random = new Random();
			for (int i = 0; i < 4; i++)
			{
				int num = random.Next();
				text += ((char)(65 + (ushort)(num % 26))).ToString();
			}
			this.Session["CheckCode"] = text;
			return text;
		}
		private void CreateCheckCodeImage(string checkCode)
		{
			if (checkCode == null || checkCode.Trim() == string.Empty)
			{
				return;
			}
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap((int)Math.Ceiling(Convert.ToDouble(checkCode.Length * 15)), 22);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			try
			{
				Random random = new Random();
				graphics.Clear(System.Drawing.Color.White);
				for (int i = 0; i < 25; i++)
				{
					int x = random.Next(bitmap.Width);
					int x2 = random.Next(bitmap.Width);
					int y = random.Next(bitmap.Height);
					int y2 = random.Next(bitmap.Height);
					graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Silver), x, y, x2, y2);
				}
				System.Drawing.Font font = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
				System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Color.Blue, System.Drawing.Color.DarkRed, 1.2f, true);
				graphics.DrawString(checkCode, font, brush, 2f, 2f);
				for (int j = 0; j < 100; j++)
				{
					int x3 = random.Next(bitmap.Width);
					int y3 = random.Next(bitmap.Height);
					bitmap.SetPixel(x3, y3, System.Drawing.Color.FromArgb(random.Next()));
				}
				graphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Gif);
				base.Response.ClearContent();
				base.Response.ContentType = "image/Gif";
				base.Response.BinaryWrite(memoryStream.ToArray());
			}
			finally
			{
				graphics.Dispose();
				bitmap.Dispose();
			}
		}
	}


