using System;
using System.Xml;
using System.Xml.Serialization;
namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
	public sealed class ArrayOfObjectSerializer143 : XmlSerializer1
	{
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("ViewCustomer_ConfirmByDealIDResponseOutHeaders", "http://tempuri.org/");
		}
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReader1)reader).Read72_Item();
		}
	}
}