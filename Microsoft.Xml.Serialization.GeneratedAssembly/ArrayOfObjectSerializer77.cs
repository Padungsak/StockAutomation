using System;
using System.Xml;
using System.Xml.Serialization;
namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
	public sealed class ArrayOfObjectSerializer77 : XmlSerializer1
	{
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("SendEditOrderResponse", "http://tempuri.org/");
		}
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReader1)reader).Read39_SendEditOrderResponse();
		}
	}
}