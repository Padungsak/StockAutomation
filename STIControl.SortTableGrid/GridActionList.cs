using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.SortTableGrid
{
	public class GridActionList : DesignerActionList
	{
		private DesignerActionUIService designerActionSvc;
		private SortGrid table;
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
		public List<ColumnItem> Columns
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.Columns;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("Columns").SetValue(this.table, value);
			}
		}
		public int Rows
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.table.Rows;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.GetPropertyByName("Rows").SetValue(this.table, value);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public GridActionList(IComponent component) : base(component)
		{
			this.designerActionSvc = null;
			this.table = (component as SortGrid);
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
				new DesignerActionPropertyItem("BackColor", "BackColor", "Appearance", "Sets the display back color."),
				new DesignerActionPropertyItem("Rows", "Rows", "Appearance", "Sets the display rows."),
				new DesignerActionPropertyItem("Columns", "Columns", "Appearance", "Sets the display columns.")
			};
		}
	}
}
