using System;
using System.Collections.Generic;
namespace STIControl.SortTableGrid
{
	public interface IColumnContainer
	{
		List<ColumnItem> Columns
		{
			get;
		}
	}
}
