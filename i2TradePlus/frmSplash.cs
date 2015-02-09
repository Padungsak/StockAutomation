using ITSNet.Wins.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmSplash : Form
	{
		private delegate void SetCurrentTaskCallBack(string currentTask);
		private IContainer components = null;
		private Label lblTask;
		private LoadingCircleControl lccSplash;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmSplash()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DisposeMe()
		{
			if (this.lccSplash.InvokeRequired)
			{
				this.lccSplash.BeginInvoke(new MethodInvoker(this.DisposeMe));
			}
			else
			{
				if (!base.IsDisposed)
				{
					this.lccSplash.Enabled = false;
					this.lccSplash.Dispose();
					base.Close();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetCurrentTask(string currentTask)
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new frmSplash.SetCurrentTaskCallBack(this.SetCurrentTask), new object[]
				{
					currentTask
				});
			}
			else
			{
				if (!base.IsDisposed)
				{
					this.lblTask.Text = currentTask;
					base.Show();
					base.BringToFront();
					this.lccSplash.Active = true;
					this.Refresh();
				}
			}
		}
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
			this.lblTask = new Label();
			this.lccSplash = new LoadingCircleControl();
			base.SuspendLayout();
			this.lblTask.Dock = DockStyle.Fill;
			this.lblTask.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.lblTask.ForeColor = Color.Black;
			this.lblTask.Location = new Point(40, 0);
			this.lblTask.Margin = new Padding(4, 0, 4, 0);
			this.lblTask.Name = "lblTask";
			this.lblTask.Size = new Size(324, 46);
			this.lblTask.TabIndex = 3;
			this.lblTask.Text = "..";
			this.lblTask.TextAlign = ContentAlignment.MiddleCenter;
			this.lccSplash.Active = false;
			this.lccSplash.Color = SystemColors.InactiveCaption;
			this.lccSplash.Dock = DockStyle.Left;
			this.lccSplash.InnerCircleRadius = 8;
			this.lccSplash.Location = new Point(0, 0);
			this.lccSplash.Margin = new Padding(4, 4, 4, 4);
			this.lccSplash.Name = "lccSplash";
			this.lccSplash.NumberSpoke = 10;
			this.lccSplash.OuterCircleRadius = 10;
			this.lccSplash.RotationSpeed = 100;
			this.lccSplash.Size = new Size(40, 46);
			this.lccSplash.SpokeThickness = 4;
			this.lccSplash.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new Size(364, 46);
			base.ControlBox = false;
			base.Controls.Add(this.lblTask);
			base.Controls.Add(this.lccSplash);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Margin = new Padding(4, 4, 4, 4);
			base.Name = "frmSplash";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			base.TopMost = true;
			base.ResumeLayout(false);
		}
	}
}
