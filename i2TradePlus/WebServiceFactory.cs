using System;
using System.Runtime.CompilerServices;
using System.Web.Services.Protocols;
namespace i2TradePlus
{
	internal class WebServiceFactory
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SoapHttpClientProtocol Create(ITSNetWS ITSNetWS)
		{
			SoapHttpClientProtocol result;
			try
			{
				SoapHttpClientProtocol soapHttpClientProtocol;
				if (ITSNetWS == ITSNetWS.TfexService)
				{
					soapHttpClientProtocol = new BusinessServiceFactory().CreateTFEX();
				}
				else
				{
					soapHttpClientProtocol = new BusinessServiceFactory().CreateSET(ITSNetWS);
				}
				result = soapHttpClientProtocol;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public WebServiceFactory()
		{
		}
	}
}
