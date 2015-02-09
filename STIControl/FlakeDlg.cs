using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl
{
	public class FlakeDlg : Form
	{
		private IContainer components;
		private Bitmap bmpFlake;
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(50, 20);
			base.FormBorderStyle = FormBorderStyle.None;
			base.Name = "FlakeDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "FlakeDlg";
			base.TopMost = true;
			base.ResumeLayout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FlakeDlg()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FlakeDlg(Bitmap bitmap)
		{
			this.InitializeComponent();
			if (bitmap != null)
			{
				this.bmpFlake = bitmap;
				BitmapRegion.CreateControlRegion(this, this.bmpFlake);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FlakeDlg(string message, Color fontColor)
		{
			this.InitializeComponent();
			try
			{
				SizeF sizeF = base.CreateGraphics().MeasureString(message, this.Font);
				Bitmap bitmap = new Bitmap((int)sizeF.Width + 5, (int)sizeF.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawString(message, new Font(this.Font, FontStyle.Bold), new SolidBrush(fontColor), 0.5f, 0.5f);
				}
				if (bitmap != null)
				{
					this.bmpFlake = bitmap;
					BitmapRegion.CreateControlRegion(this, this.bmpFlake);
				}
				base.Size = new Size((int)sizeF.Width + 5, (int)sizeF.Height);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void Show(int x, int y)
		{
			base.Left = x;
			base.Top = y;
			base.Show();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnClosed(EventArgs e)
		{
			if (this.bmpFlake != null)
			{
				this.bmpFlake.Dispose();
				this.bmpFlake = null;
			}
			base.OnClosed(e);
		}
	}
}
