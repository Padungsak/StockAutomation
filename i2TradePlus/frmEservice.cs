using i2TradePlus.Information;
using ITSNet.CefBrowser.WindowsForms;
using ITSNet.Common.BIZ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmEservice : ClientBaseForm, IRealtimeMessage
	{
		private IContainer components = null;
		private CefWebBrowser cefWebBrowser1;
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
			this.cefWebBrowser1 = new CefWebBrowser();
			base.SuspendLayout();
			this.cefWebBrowser1.Location = new Point(12, 12);
			this.cefWebBrowser1.Name = "cefWebBrowser1";
			this.cefWebBrowser1.Size = new Size(152, 93);
			this.cefWebBrowser1.TabIndex = 55;
			this.cefWebBrowser1.Text = "cefWebBrowser1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(284, 262);
			base.Controls.Add(this.cefWebBrowser1);
			base.Name = "frmEservice";
			this.Text = "frmEservice";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmEservice_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmEservice_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmEservice_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmEservice_IDoCustomSizeChanged);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmEservice_IDoReActivated);
			base.Controls.SetChildIndex(this.cefWebBrowser1, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmEservice()
		{
			this.InitializeComponent();
			this.cefWebBrowser1.Visible = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmEservice(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
			try
			{
				if (!base.DesignMode)
				{
					this.LoadEservice();
					this.cefWebBrowser1.Visible = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmStockChart", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadEservice()
		{
			try
			{
				string text = string.Empty;
				text = "https://203.144.229.41/login_eservice.asp?txtParam=ae00111547a1247800916d0bbd9db4f45d5099a62b6b02ca97b142ecfd3a534f954acb182cbbf70279364ce13bc1d1cb55bc377839fa47cfafbf3dd2692dd475";
				if (text != null && this.cefWebBrowser1 != null && this.cefWebBrowser1.Browser != null && this.cefWebBrowser1.Browser.GetMainFrame() != null)
				{
					this.cefWebBrowser1.Browser.GetMainFrame().LoadUrl(text);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (!base.IsLoadingData)
				{
					try
					{
					}
					catch (Exception ex)
					{
						this.ShowError("ReceiveMessage", ex);
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmEservice_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			if (isChanged)
			{
				this.cefWebBrowser1.SetBounds(0, 0, base.Width, base.Height);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmEservice_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmEservice_IDoLoadData()
		{
			try
			{
				try
				{
					this.LoadEservice();
				}
				catch
				{
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmStockHistory_IDoLoadData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReloadData()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmEservice_IDoShownDelay()
		{
			this.SetResize(true);
			base.Show();
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmEservice_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(true);
				base.Show();
				this.LoadEservice();
			}
		}
	}
}
