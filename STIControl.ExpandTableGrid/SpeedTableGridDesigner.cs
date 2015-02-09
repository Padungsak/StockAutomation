using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms.Design;
namespace STIControl.ExpandTableGrid
{
	internal class SpeedTableGridDesigner : ControlDesigner
	{
		private IComponentChangeService componentChangeService;
		private bool isDisposed;
		private DesignerActionListCollection lists;
		public override DesignerActionListCollection ActionLists
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.lists == null)
				{
					this.lists = new DesignerActionListCollection();
					this.lists.Add(new GridActionList2(base.Component));
				}
				return this.lists;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ComponentChangeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			try
			{
				if (this.Control != null)
				{
					if (e.Member != null && this.Control != null)
					{
						this.Control.Invalidate();
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing && this.componentChangeService != null)
				{
					this.componentChangeService.ComponentChanged -= new ComponentChangedEventHandler(this.ComponentChangeService_ComponentChanged);
				}
				this.isDisposed = true;
				base.Dispose(disposing);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.componentChangeService = (this.GetService(typeof(IComponentChangeService)) as IComponentChangeService);
			if (this.componentChangeService != null)
			{
				this.componentChangeService.ComponentChanged += new ComponentChangedEventHandler(this.ComponentChangeService_ComponentChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SpeedTableGridDesigner()
		{
		}
	}
}
