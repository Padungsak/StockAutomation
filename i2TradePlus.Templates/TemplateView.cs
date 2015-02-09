using i2TradePlus.Properties;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.Templates
{
	internal class TemplateView
	{
		public enum ContentState
		{
			Detached,
			New,
			OpeningOrClosing,
			Unchanged
		}
		public const Keys KEYS_SWITCH_CONTROL = Keys.Tab;
		private TemplateView.ContentState currentState = TemplateView.ContentState.Detached;
		private string name = string.Empty;
		private Form formObj = null;
		private bool isFirstOpen = true;
		public TemplateView.ContentState CurrentState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.currentState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.currentState = value;
			}
		}
		public string Name
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.name;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.name = value;
			}
		}
		public Form FormObj
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.formObj;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.formObj = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplateView(string templateName)
		{
			this.name = templateName;
			this.currentState = TemplateView.ContentState.New;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Show()
		{
			TemplateView.ContentState contentState = this.currentState;
			this.currentState = TemplateView.ContentState.OpeningOrClosing;
			(this.formObj as ClientBaseForm).OpenForm(this.isFirstOpen);
			this.currentState = contentState;
			this.isFirstOpen = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Hide()
		{
			TemplateView.ContentState contentState = this.currentState;
			this.currentState = TemplateView.ContentState.OpeningOrClosing;
			((ClientBaseForm)this.formObj).HideForm();
			this.currentState = contentState;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendSymbolLink1(object sender, SymbolLinkSource source, string newStock)
		{
			if (this.formObj.GetType() != sender.GetType())
			{
				((ClientBaseForm)this.formObj).SetSymbolLink(sender, source, newStock);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void FormKeyUp(KeyEventArgs e)
		{
			try
			{
				(this.formObj as ClientBaseForm).ReceiveKeyupMainForm(e);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFont()
		{
			if (this.formObj.GetType().BaseType == typeof(ClientBaseForm))
			{
				((ClientBaseForm)this.formObj).SetFontToControl(Settings.Default.Default_Font);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFormSize()
		{
			if (this.formObj.GetType().BaseType == typeof(ClientBaseForm))
			{
				((ClientBaseForm)this.formObj).SetFormSize();
			}
		}
	}
}
