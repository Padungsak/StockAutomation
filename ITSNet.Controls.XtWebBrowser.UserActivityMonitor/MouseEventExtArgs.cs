using System;
using System.Windows.Forms;
namespace ITSNet.Controls.XtWebBrowser.UserActivityMonitor
{
	public class MouseEventExtArgs : MouseEventArgs
	{
		private bool m_Handled;
		public bool Handled
		{
			get
			{
				return this.m_Handled;
			}
			set
			{
				this.m_Handled = value;
			}
		}
		public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta) : base(buttons, clicks, x, y, delta)
		{
		}
		internal MouseEventExtArgs(MouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
		{
		}
	}
}
