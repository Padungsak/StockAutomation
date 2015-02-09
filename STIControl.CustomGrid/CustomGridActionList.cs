using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.CustomGrid
{
	internal class CustomGridActionList : DesignerActionList
	{
		private DesignerActionUIService designerActionSvc;
		private IntzaCustomGrid table;
		public Color BackColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.BackColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("BackColor").SetValue(this.table, value);
			}
		}
		public Font Font
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.Font;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("Font").SetValue(this.table, value);
			}
		}
		public int Height
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.Height;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("Height").SetValue(this.table, value);
			}
		}
		public List<ItemGrid> Items
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.Items;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("Items").SetValue(this.table, value);
			}
		}
		public int Width
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.Width;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("Width").SetValue(this.table, value);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CustomGridActionList(IComponent component) : base(component)
		{
			this.designerActionSvc = null;
			this.table = (component as IntzaCustomGrid);
			this.designerActionSvc = (DesignerActionUIService)base.GetService(typeof(DesignerActionUIService));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private PropertyDescriptor GetPropertyByName(string propName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.table)[propName];
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("Matching ColorLabel property not found!", propName);
			}
			return propertyDescriptor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionHeaderItem("Appearance"),
				new DesignerActionHeaderItem("Size"),
				new DesignerActionPropertyItem("Width", "Width", "Size", "Sets the width of the control"),
				new DesignerActionPropertyItem("Height", "Height", "Size", "Sets the height of the control"),
				new DesignerActionPropertyItem("BackColor", "BackColor", "Appearance", "Sets the display back color."),
				new DesignerActionPropertyItem("Items", "Edit Items", "Appearance", "Sets the display items."),
				new DesignerActionPropertyItem("Font", "Font", "Appearance", "Sets the display font.")
			};
		}
	}
}
