using System;
using System.Xml;
using System.Xml.Serialization;
namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
	public sealed class ArrayOfObjectSerializer560 : XmlSerializer1
	{
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("TopBBOTFEXad", "http://tempuri.org/");
		}
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriter1)writer).Write533_TopBBOTFEXad((object[])objectToSerialize);
		}
	}
}
