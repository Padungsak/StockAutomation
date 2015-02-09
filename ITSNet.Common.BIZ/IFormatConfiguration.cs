using System;
namespace ITSNet.Common.BIZ
{
	public interface IFormatConfiguration
	{
		MessageFormatElementCollection GetFormaterFormType(string formatType);
	}
}
