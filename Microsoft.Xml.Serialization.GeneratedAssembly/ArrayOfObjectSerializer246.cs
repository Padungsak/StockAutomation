using System;
using System.Xml;
using System.Xml.Serialization;
namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
	public sealed class ArrayOfObjectSerializer246 : XmlSerializer1
	{
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("ViewOrderByOrderNoInHeaders", "http://tempuri.org/");
		}
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriter1)writer).Write124_ViewOrderByOrderNoInHeaders((object[])objectToSerialize);
		}
	}
}