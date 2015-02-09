using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace i2TradePlus.MyDataSet
{
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), HelpKeyword("vs.data.DataSet"), DesignerCategory("code"), ToolboxItem(true), XmlRoot("ApplicationInfoDS"), XmlSchemaProvider("GetTypedDataSetSchema")]
	[Serializable]
	public class ApplicationInfoDS : DataSet
	{
		public delegate void InfoRowChangeEventHandler(object sender, ApplicationInfoDS.InfoRowChangeEvent e);
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class InfoDataTable : DataTable, IEnumerable
		{
			private DataColumn columnTPBlinkColor;
			private DataColumn columnValidatePolicy;
			private DataColumn columnAlertEnable;
			private DataColumn columnAlertAutoPopup;
			private DataColumn columnAlertSound;
			public  ApplicationInfoDS.InfoRowChangeEventHandler _InfoRowChanging;
			public event ApplicationInfoDS.InfoRowChangeEventHandler InfoRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._InfoRowChanging += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._InfoRowChanging -= value;
				}
			}
			public  ApplicationInfoDS.InfoRowChangeEventHandler _InfoRowChanged;
			public event ApplicationInfoDS.InfoRowChangeEventHandler InfoRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._InfoRowChanged += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._InfoRowChanged -= value;
				}
			}
			public  ApplicationInfoDS.InfoRowChangeEventHandler _InfoRowDeleting;
			public event ApplicationInfoDS.InfoRowChangeEventHandler InfoRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._InfoRowDeleting += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._InfoRowDeleting -= value;
				}
			}
            public ApplicationInfoDS.InfoRowChangeEventHandler _InfoRowDeleted;
			public event ApplicationInfoDS.InfoRowChangeEventHandler InfoRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._InfoRowDeleted += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._InfoRowDeleted -= value;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn TPBlinkColorColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnTPBlinkColor;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn ValidatePolicyColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnValidatePolicy;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn AlertEnableColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnAlertEnable;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn AlertAutoPopupColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnAlertAutoPopup;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn AlertSoundColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnAlertSound;
				}
			}
			[Browsable(false), DebuggerNonUserCode]
			public int Count
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return base.Rows.Count;
				}
			}
			[DebuggerNonUserCode]
			public ApplicationInfoDS.InfoRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (ApplicationInfoDS.InfoRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public InfoDataTable()
			{
				base.TableName = "Info";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal InfoDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected InfoDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddInfoRow(ApplicationInfoDS.InfoRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ApplicationInfoDS.InfoRow AddInfoRow(string TPBlinkColor, bool ValidatePolicy, bool AlertEnable, bool AlertAutoPopup, bool AlertSound)
			{
				ApplicationInfoDS.InfoRow infoRow = (ApplicationInfoDS.InfoRow)base.NewRow();
				object[] itemArray = new object[]
				{
					TPBlinkColor,
					ValidatePolicy,
					AlertEnable,
					AlertAutoPopup,
					AlertSound
				};
				infoRow.ItemArray = itemArray;
				base.Rows.Add(infoRow);
				return infoRow;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public virtual IEnumerator GetEnumerator()
			{
				return base.Rows.GetEnumerator();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public override DataTable Clone()
			{
				ApplicationInfoDS.InfoDataTable infoDataTable = (ApplicationInfoDS.InfoDataTable)base.Clone();
				infoDataTable.InitVars();
				return infoDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new ApplicationInfoDS.InfoDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnTPBlinkColor = base.Columns["TPBlinkColor"];
				this.columnValidatePolicy = base.Columns["ValidatePolicy"];
				this.columnAlertEnable = base.Columns["AlertEnable"];
				this.columnAlertAutoPopup = base.Columns["AlertAutoPopup"];
				this.columnAlertSound = base.Columns["AlertSound"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnTPBlinkColor = new DataColumn("TPBlinkColor", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnTPBlinkColor);
				this.columnValidatePolicy = new DataColumn("ValidatePolicy", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnValidatePolicy);
				this.columnAlertEnable = new DataColumn("AlertEnable", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnAlertEnable);
				this.columnAlertAutoPopup = new DataColumn("AlertAutoPopup", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnAlertAutoPopup);
				this.columnAlertSound = new DataColumn("AlertSound", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnAlertSound);
				this.columnTPBlinkColor.AllowDBNull = false;
				this.columnTPBlinkColor.DefaultValue = "";
				this.columnValidatePolicy.AllowDBNull = false;
				this.columnValidatePolicy.DefaultValue = false;
				this.columnAlertEnable.AllowDBNull = false;
				this.columnAlertEnable.DefaultValue = false;
				this.columnAlertAutoPopup.AllowDBNull = false;
				this.columnAlertAutoPopup.DefaultValue = false;
				this.columnAlertSound.AllowDBNull = false;
				this.columnAlertSound.DefaultValue = true;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ApplicationInfoDS.InfoRow NewInfoRow()
			{
				return (ApplicationInfoDS.InfoRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new ApplicationInfoDS.InfoRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(ApplicationInfoDS.InfoRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._InfoRowChanged != null)
				{
					this._InfoRowChanged(this, new ApplicationInfoDS.InfoRowChangeEvent((ApplicationInfoDS.InfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._InfoRowChanging != null)
				{
					this._InfoRowChanging(this, new ApplicationInfoDS.InfoRowChangeEvent((ApplicationInfoDS.InfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._InfoRowDeleted != null)
				{
					this._InfoRowDeleted(this, new ApplicationInfoDS.InfoRowChangeEvent((ApplicationInfoDS.InfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._InfoRowDeleting != null)
				{
					this._InfoRowDeleting(this, new ApplicationInfoDS.InfoRowChangeEvent((ApplicationInfoDS.InfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveInfoRow(ApplicationInfoDS.InfoRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				ApplicationInfoDS applicationInfoDS = new ApplicationInfoDS();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = 79228162514264337593543950335m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = applicationInfoDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "InfoDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = applicationInfoDS.GetSchemaSerializable();
				XmlSchemaComplexType result;
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
						while (enumerator.MoveNext())
						{
							XmlSchema xmlSchema = (XmlSchema)enumerator.Current;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									result = xmlSchemaComplexType;
									return result;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				result = xmlSchemaComplexType;
				return result;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class InfoRow : DataRow
		{
			private ApplicationInfoDS.InfoDataTable tableInfo;
			[DebuggerNonUserCode]
			public string TPBlinkColor
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (string)base[this.tableInfo.TPBlinkColorColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableInfo.TPBlinkColorColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool ValidatePolicy
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (bool)base[this.tableInfo.ValidatePolicyColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableInfo.ValidatePolicyColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool AlertEnable
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (bool)base[this.tableInfo.AlertEnableColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableInfo.AlertEnableColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool AlertAutoPopup
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (bool)base[this.tableInfo.AlertAutoPopupColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableInfo.AlertAutoPopupColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool AlertSound
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (bool)base[this.tableInfo.AlertSoundColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableInfo.AlertSoundColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal InfoRow(DataRowBuilder rb) : base(rb)
			{
				this.tableInfo = (ApplicationInfoDS.InfoDataTable)base.Table;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class InfoRowChangeEvent : EventArgs
		{
			private ApplicationInfoDS.InfoRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public ApplicationInfoDS.InfoRow Row
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.eventRow;
				}
			}
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.eventAction;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public InfoRowChangeEvent(ApplicationInfoDS.InfoRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		private ApplicationInfoDS.InfoDataTable tableInfo;
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public ApplicationInfoDS.InfoDataTable Info
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableInfo;
			}
		}
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DebuggerNonUserCode]
		public override SchemaSerializationMode SchemaSerializationMode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._schemaSerializationMode;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._schemaSerializationMode = value;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
		public new DataTableCollection Tables
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.Tables;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
		public new DataRelationCollection Relations
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.Relations;
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ApplicationInfoDS()
		{
			base.BeginInit();
			this.InitClass();
			CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			base.EndInit();
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected ApplicationInfoDS(SerializationInfo info, StreamingContext context) : base(info, context, false)
		{
			if (base.IsBinarySerialized(info, context))
			{
				this.InitVars(false);
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
				this.Tables.CollectionChanged += value;
				this.Relations.CollectionChanged += value;
			}
			else
			{
				string s = (string)info.GetValue("XmlSchema", typeof(string));
				if (base.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
				{
					DataSet dataSet = new DataSet();
					dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
					if (dataSet.Tables["Info"] != null)
					{
						base.Tables.Add(new ApplicationInfoDS.InfoDataTable(dataSet.Tables["Info"]));
					}
					base.DataSetName = dataSet.DataSetName;
					base.Prefix = dataSet.Prefix;
					base.Namespace = dataSet.Namespace;
					base.Locale = dataSet.Locale;
					base.CaseSensitive = dataSet.CaseSensitive;
					base.EnforceConstraints = dataSet.EnforceConstraints;
					base.Merge(dataSet, false, MissingSchemaAction.Add);
					this.InitVars();
				}
				else
				{
					base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
				}
				base.GetSerializationData(info, context);
				CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.SchemaChanged);
				base.Tables.CollectionChanged += value2;
				this.Relations.CollectionChanged += value2;
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void InitializeDerivedDataSet()
		{
			base.BeginInit();
			this.InitClass();
			base.EndInit();
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override DataSet Clone()
		{
			ApplicationInfoDS applicationInfoDS = (ApplicationInfoDS)base.Clone();
			applicationInfoDS.InitVars();
			applicationInfoDS.SchemaSerializationMode = this.SchemaSerializationMode;
			return applicationInfoDS;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (base.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				this.Reset();
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(reader);
				if (dataSet.Tables["Info"] != null)
				{
					base.Tables.Add(new ApplicationInfoDS.InfoDataTable(dataSet.Tables["Info"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				base.Merge(dataSet, false, MissingSchemaAction.Add);
				this.InitVars();
			}
			else
			{
				base.ReadXml(reader);
				this.InitVars();
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			base.WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void InitVars()
		{
			this.InitVars(true);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void InitVars(bool initTable)
		{
			this.tableInfo = (ApplicationInfoDS.InfoDataTable)base.Tables["Info"];
			if (initTable)
			{
				if (this.tableInfo != null)
				{
					this.tableInfo.InitVars();
				}
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitClass()
		{
			base.DataSetName = "ApplicationInfoDS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/ApplicationInfoDS.xsd";
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.tableInfo = new ApplicationInfoDS.InfoDataTable();
			base.Tables.Add(this.tableInfo);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeInfo()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				this.InitVars();
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			ApplicationInfoDS applicationInfoDS = new ApplicationInfoDS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = applicationInfoDS.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = applicationInfoDS.GetSchemaSerializable();
			XmlSchemaComplexType result;
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					schemaSerializable.Write(memoryStream);
					IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
					while (enumerator.MoveNext())
					{
						XmlSchema xmlSchema = (XmlSchema)enumerator.Current;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								result = xmlSchemaComplexType;
								return result;
							}
						}
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
					if (memoryStream2 != null)
					{
						memoryStream2.Close();
					}
				}
			}
			xs.Add(schemaSerializable);
			result = xmlSchemaComplexType;
			return result;
		}
	}
}
