using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms.Design;
namespace STIControl
{
	internal class ScrollbarControlDesigner : ControlDesigner
	{
		public override SelectionRules SelectionRules
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				SelectionRules result = base.SelectionRules;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["AutoSize"];
				if (propertyDescriptor != null)
				{
					bool flag = (bool)propertyDescriptor.GetValue(base.Component);
					if (flag)
					{
						result = (SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
					}
					else
					{
						result = (SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
					}
				}
				return result;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ScrollbarControlDesigner()
		{
		}
	}
}
