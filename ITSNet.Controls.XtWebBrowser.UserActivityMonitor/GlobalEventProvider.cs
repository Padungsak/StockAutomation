using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace ITSNet.Controls.XtWebBrowser.UserActivityMonitor
{
	public class GlobalEventProvider : Component
	{
		private event MouseEventHandler m_MouseMove
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseMove = (MouseEventHandler)Delegate.Combine(this.m_MouseMove, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseMove = (MouseEventHandler)Delegate.Remove(this.m_MouseMove, value);
			}
		}
		public event MouseEventHandler MouseMove
		{
			add
			{
				if (this.m_MouseMove == null)
				{
					HookManager.MouseMove += new MouseEventHandler(this.HookManager_MouseMove);
				}
				this.m_MouseMove = (MouseEventHandler)Delegate.Combine(this.m_MouseMove, value);
			}
			remove
			{
				this.m_MouseMove = (MouseEventHandler)Delegate.Remove(this.m_MouseMove, value);
				if (this.m_MouseMove == null)
				{
					HookManager.MouseMove -= new MouseEventHandler(this.HookManager_MouseMove);
				}
			}
		}
		private event MouseEventHandler m_MouseClick
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseClick = (MouseEventHandler)Delegate.Combine(this.m_MouseClick, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseClick = (MouseEventHandler)Delegate.Remove(this.m_MouseClick, value);
			}
		}
		public event MouseEventHandler MouseClick
		{
			add
			{
				if (this.m_MouseClick == null)
				{
					HookManager.MouseClick += new MouseEventHandler(this.HookManager_MouseClick);
				}
				this.m_MouseClick = (MouseEventHandler)Delegate.Combine(this.m_MouseClick, value);
			}
			remove
			{
				this.m_MouseClick = (MouseEventHandler)Delegate.Remove(this.m_MouseClick, value);
				if (this.m_MouseClick == null)
				{
					HookManager.MouseClick -= new MouseEventHandler(this.HookManager_MouseClick);
				}
			}
		}
		private event MouseEventHandler m_MouseDown
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseDown = (MouseEventHandler)Delegate.Combine(this.m_MouseDown, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseDown = (MouseEventHandler)Delegate.Remove(this.m_MouseDown, value);
			}
		}
		public event MouseEventHandler MouseDown
		{
			add
			{
				if (this.m_MouseDown == null)
				{
					HookManager.MouseDown += new MouseEventHandler(this.HookManager_MouseDown);
				}
				this.m_MouseDown = (MouseEventHandler)Delegate.Combine(this.m_MouseDown, value);
			}
			remove
			{
				this.m_MouseDown = (MouseEventHandler)Delegate.Remove(this.m_MouseDown, value);
				if (this.m_MouseDown == null)
				{
					HookManager.MouseDown -= new MouseEventHandler(this.HookManager_MouseDown);
				}
			}
		}
		private event MouseEventHandler m_MouseUp
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseUp = (MouseEventHandler)Delegate.Combine(this.m_MouseUp, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseUp = (MouseEventHandler)Delegate.Remove(this.m_MouseUp, value);
			}
		}
		public event MouseEventHandler MouseUp
		{
			add
			{
				if (this.m_MouseUp == null)
				{
					HookManager.MouseUp += new MouseEventHandler(this.HookManager_MouseUp);
				}
				this.m_MouseUp = (MouseEventHandler)Delegate.Combine(this.m_MouseUp, value);
			}
			remove
			{
				this.m_MouseUp = (MouseEventHandler)Delegate.Remove(this.m_MouseUp, value);
				if (this.m_MouseUp == null)
				{
					HookManager.MouseUp -= new MouseEventHandler(this.HookManager_MouseUp);
				}
			}
		}
		private event MouseEventHandler m_MouseDoubleClick
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseDoubleClick = (MouseEventHandler)Delegate.Combine(this.m_MouseDoubleClick, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseDoubleClick = (MouseEventHandler)Delegate.Remove(this.m_MouseDoubleClick, value);
			}
		}
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				if (this.m_MouseDoubleClick == null)
				{
					HookManager.MouseDoubleClick += new MouseEventHandler(this.HookManager_MouseDoubleClick);
				}
				this.m_MouseDoubleClick = (MouseEventHandler)Delegate.Combine(this.m_MouseDoubleClick, value);
			}
			remove
			{
				this.m_MouseDoubleClick = (MouseEventHandler)Delegate.Remove(this.m_MouseDoubleClick, value);
				if (this.m_MouseDoubleClick == null)
				{
					HookManager.MouseDoubleClick -= new MouseEventHandler(this.HookManager_MouseDoubleClick);
				}
			}
		}
		private event EventHandler<MouseEventExtArgs> m_MouseMoveExt
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseMoveExt = (EventHandler<MouseEventExtArgs>)Delegate.Combine(this.m_MouseMoveExt, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseMoveExt = (EventHandler<MouseEventExtArgs>)Delegate.Remove(this.m_MouseMoveExt, value);
			}
		}
		public event EventHandler<MouseEventExtArgs> MouseMoveExt
		{
			add
			{
				if (this.m_MouseMoveExt == null)
				{
					HookManager.MouseMoveExt += new EventHandler<MouseEventExtArgs>(this.HookManager_MouseMoveExt);
				}
				this.m_MouseMoveExt = (EventHandler<MouseEventExtArgs>)Delegate.Combine(this.m_MouseMoveExt, value);
			}
			remove
			{
				this.m_MouseMoveExt = (EventHandler<MouseEventExtArgs>)Delegate.Remove(this.m_MouseMoveExt, value);
				if (this.m_MouseMoveExt == null)
				{
					HookManager.MouseMoveExt -= new EventHandler<MouseEventExtArgs>(this.HookManager_MouseMoveExt);
				}
			}
		}
		private event EventHandler<MouseEventExtArgs> m_MouseClickExt
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_MouseClickExt = (EventHandler<MouseEventExtArgs>)Delegate.Combine(this.m_MouseClickExt, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_MouseClickExt = (EventHandler<MouseEventExtArgs>)Delegate.Remove(this.m_MouseClickExt, value);
			}
		}
		public event EventHandler<MouseEventExtArgs> MouseClickExt
		{
			add
			{
				if (this.m_MouseClickExt == null)
				{
					HookManager.MouseClickExt += new EventHandler<MouseEventExtArgs>(this.HookManager_MouseClickExt);
				}
				this.m_MouseClickExt = (EventHandler<MouseEventExtArgs>)Delegate.Combine(this.m_MouseClickExt, value);
			}
			remove
			{
				this.m_MouseClickExt = (EventHandler<MouseEventExtArgs>)Delegate.Remove(this.m_MouseClickExt, value);
				if (this.m_MouseClickExt == null)
				{
					HookManager.MouseClickExt -= new EventHandler<MouseEventExtArgs>(this.HookManager_MouseClickExt);
				}
			}
		}
		private event KeyPressEventHandler m_KeyPress
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_KeyPress = (KeyPressEventHandler)Delegate.Combine(this.m_KeyPress, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_KeyPress = (KeyPressEventHandler)Delegate.Remove(this.m_KeyPress, value);
			}
		}
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				if (this.m_KeyPress == null)
				{
					HookManager.KeyPress += new KeyPressEventHandler(this.HookManager_KeyPress);
				}
				this.m_KeyPress = (KeyPressEventHandler)Delegate.Combine(this.m_KeyPress, value);
			}
			remove
			{
				this.m_KeyPress = (KeyPressEventHandler)Delegate.Remove(this.m_KeyPress, value);
				if (this.m_KeyPress == null)
				{
					HookManager.KeyPress -= new KeyPressEventHandler(this.HookManager_KeyPress);
				}
			}
		}
		private event KeyEventHandler m_KeyUp
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_KeyUp = (KeyEventHandler)Delegate.Combine(this.m_KeyUp, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_KeyUp = (KeyEventHandler)Delegate.Remove(this.m_KeyUp, value);
			}
		}
		public event KeyEventHandler KeyUp
		{
			add
			{
				if (this.m_KeyUp == null)
				{
					HookManager.KeyUp += new KeyEventHandler(this.HookManager_KeyUp);
				}
				this.m_KeyUp = (KeyEventHandler)Delegate.Combine(this.m_KeyUp, value);
			}
			remove
			{
				this.m_KeyUp = (KeyEventHandler)Delegate.Remove(this.m_KeyUp, value);
				if (this.m_KeyUp == null)
				{
					HookManager.KeyUp -= new KeyEventHandler(this.HookManager_KeyUp);
				}
			}
		}
		private event KeyEventHandler m_KeyDown
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.m_KeyDown = (KeyEventHandler)Delegate.Combine(this.m_KeyDown, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.m_KeyDown = (KeyEventHandler)Delegate.Remove(this.m_KeyDown, value);
			}
		}
		public event KeyEventHandler KeyDown
		{
			add
			{
				if (this.m_KeyDown == null)
				{
					HookManager.KeyDown += new KeyEventHandler(this.HookManager_KeyDown);
				}
				this.m_KeyDown = (KeyEventHandler)Delegate.Combine(this.m_KeyDown, value);
			}
			remove
			{
				this.m_KeyDown = (KeyEventHandler)Delegate.Remove(this.m_KeyDown, value);
				if (this.m_KeyDown == null)
				{
					HookManager.KeyDown -= new KeyEventHandler(this.HookManager_KeyDown);
				}
			}
		}
		protected override bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}
		private void HookManager_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.m_MouseMove != null)
			{
				this.m_MouseMove(this, e);
			}
		}
		private void HookManager_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.m_MouseClick != null)
			{
				this.m_MouseClick(this, e);
			}
		}
		private void HookManager_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.m_MouseDown != null)
			{
				this.m_MouseDown(this, e);
			}
		}
		private void HookManager_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.m_MouseUp != null)
			{
				this.m_MouseUp(this, e);
			}
		}
		private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.m_MouseDoubleClick != null)
			{
				this.m_MouseDoubleClick(this, e);
			}
		}
		private void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
		{
			if (this.m_MouseMoveExt != null)
			{
				this.m_MouseMoveExt(this, e);
			}
		}
		private void HookManager_MouseClickExt(object sender, MouseEventExtArgs e)
		{
			if (this.m_MouseClickExt != null)
			{
				this.m_MouseClickExt(this, e);
			}
		}
		private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (this.m_KeyPress != null)
			{
				this.m_KeyPress(this, e);
			}
		}
		private void HookManager_KeyUp(object sender, KeyEventArgs e)
		{
			if (this.m_KeyUp != null)
			{
				this.m_KeyUp(this, e);
			}
		}
		private void HookManager_KeyDown(object sender, KeyEventArgs e)
		{
			this.m_KeyDown(this, e);
		}
	}
}
