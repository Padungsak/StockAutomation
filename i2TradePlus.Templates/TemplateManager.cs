using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.Templates
{
	internal class TemplateManager
	{
		private static TemplateManager instance = null;
		private frmMain mainForm = null;
		private Dictionary<string, TemplateView> templateViews = null;
		private TemplateView currentActiveTemplateView = null;
		private string defaultTemplateName = string.Empty;
		private string defaultTemplateGroup = string.Empty;
		public static TemplateManager Instance
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (TemplateManager.instance == null)
				{
					TemplateManager.instance = new TemplateManager();
				}
				return TemplateManager.instance;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				TemplateManager.instance = value;
			}
		}
		public frmMain MainForm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.mainForm;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.mainForm = value;
			}
		}
		public Dictionary<string, TemplateView> TemplateViews
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.templateViews;
			}
		}
		public TemplateView CurrentActiveTemplateView
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.templateViews.Count > 0 && this.currentActiveTemplateView == null)
				{
					KeyValuePair<string, TemplateView> current = this.templateViews.GetEnumerator().Current;
					this.currentActiveTemplateView = current.Value;
				}
				return this.currentActiveTemplateView;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.currentActiveTemplateView = value;
			}
		}
		public string DefaultTemplateName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.defaultTemplateName;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.defaultTemplateName = value;
			}
		}
		public string DefaultTemplateGroup
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.defaultTemplateGroup;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.defaultTemplateGroup = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplateManager()
		{
			if (this.templateViews == null)
			{
				this.templateViews = new Dictionary<string, TemplateView>();
			}
			else
			{
				this.templateViews.Clear();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool IsDefaultTemplate(string templateName)
		{
			return TemplateManager.Instance.DefaultTemplateName == templateName;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplateView GetTemplate(string templateName)
		{
			TemplateView result;
			if (this.templateViews.ContainsKey(templateName))
			{
				result = this.templateViews[templateName];
			}
			else
			{
				result = null;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Show(string templateViewName)
		{
			try
			{
				TemplateView templateView = null;
				if (this.currentActiveTemplateView != null)
				{
					if (this.currentActiveTemplateView.Name != templateViewName)
					{
						templateView = this.currentActiveTemplateView;
					}
				}
				if (this.templateViews.ContainsKey(templateViewName))
				{
					this.currentActiveTemplateView = this.templateViews[templateViewName];
					this.currentActiveTemplateView.Show();
				}
				if (templateView != null)
				{
					templateView.Hide();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendSymbolLink(object sender, SymbolLinkSource source, string newStock)
		{
			try
			{
				if (this.currentActiveTemplateView != null)
				{
					this.currentActiveTemplateView.SendSymbolLink1(sender, source, newStock);
				}
				if (source == SymbolLinkSource.SmartStock)
				{
					this.mainForm.SetSmartStock(sender, source, newStock);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DeleteTemplate(string templateViewName)
		{
			try
			{
				if (this.templateViews.ContainsKey(templateViewName))
				{
					TemplateView templateView = this.templateViews[templateViewName];
					if (templateView != null)
					{
						templateView.CurrentState = TemplateView.ContentState.OpeningOrClosing;
						templateView.Hide();
						if (templateView.FormObj.GetType().BaseType == typeof(ClientBaseForm))
						{
							frmMain.OnMessageReceived -= new frmMain.OnMessageRecievedEventHendler((templateView.FormObj as IRealtimeMessage).ReceiveMessage);
							((ClientBaseForm)templateView.FormObj).FormState = ClientBaseForm.ClientBaseFormState.Closing;
							templateView.FormObj.Dispose();
							templateView.FormObj = null;
						}
						if (this.currentActiveTemplateView == this.templateViews[templateViewName])
						{
							this.currentActiveTemplateView = null;
						}
						this.templateViews.Remove(templateViewName);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Dictionary<string, object> GetTemplateProperties(string templateName)
		{
			Dictionary<string, object> result = null;
			if (this.templateViews.ContainsKey(templateName))
			{
				result = new TemplatePropeties(this.templateViews[templateName]).GetTemplateProperties();
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public List<Dictionary<string, object>> GetAllFormProperties(string templateName)
		{
			List<Dictionary<string, object>> list = null;
			if (this.templateViews.ContainsKey(templateName))
			{
				list = new List<Dictionary<string, object>>();
				list.Add(new FormProperties(this.templateViews[templateName].FormObj).GetFormProperties());
			}
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Dictionary<string, object> GetFormPropertyFields(string templateName, string formName)
		{
			Dictionary<string, object> result = null;
			if (this.templateViews.ContainsKey(templateName))
			{
				Form formObj = this.templateViews[templateName].FormObj;
				if (formObj != null && formObj.GetType().BaseType == typeof(ClientBaseForm))
				{
					result = (formObj as ClientBaseForm).PackProperties();
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool IsTemplateContains(string templateName)
		{
			return this.templateViews.ContainsKey(templateName);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplateView CreateTemplateView(string templateName)
		{
			TemplateView result;
			if (this.IsTemplateContains(templateName))
			{
				result = null;
			}
			else
			{
				TemplateView templateView = new TemplateView(templateName);
				this.templateViews.Add(templateView.Name, templateView);
				result = templateView;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void AddFormToTemplate(string templateName, Form formClient, int formIndex)
		{
			if (this.templateViews.ContainsKey(templateName))
			{
				this.templateViews[templateName].FormObj = formClient;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplateView.ContentState GetTemplateState(string templateName)
		{
			TemplateView.ContentState result;
			if (this.templateViews.ContainsKey(templateName))
			{
				result = this.templateViews[templateName].CurrentState;
			}
			else
			{
				result = TemplateView.ContentState.Detached;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetTabControl(string templateName)
		{
			if (this.templateViews.ContainsKey(templateName))
			{
				TemplateView templateView = this.templateViews[templateName];
			}
		}
	}
}
