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
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), HelpKeyword("vs.data.DataSet"), DesignerCategory("code"), ToolboxItem(true), XmlRoot("OrderQueueDS"), XmlSchemaProvider("GetTypedDataSetSchema")]
	[Serializable]
	public class OrderQueueDS : DataSet
	{
		public delegate void OrderRecordRowChangeEventHandler(object sender, OrderQueueDS.OrderRecordRowChangeEvent e);
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class OrderRecordDataTable : DataTable, IEnumerable
		{
			private DataColumn columnside;
			private DataColumn columnstock;
			private DataColumn columnttf;
			private DataColumn columnvolume;
			private DataColumn columnprice;
			private DataColumn columnpubvol;
			private DataColumn columncondition;
			private DataColumn columndeposit;
			public  OrderQueueDS.OrderRecordRowChangeEventHandler _OrderRecordRowChanging;
			public event OrderQueueDS.OrderRecordRowChangeEventHandler OrderRecordRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._OrderRecordRowChanging += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._OrderRecordRowChanging -= value;
				}
			}
			public  OrderQueueDS.OrderRecordRowChangeEventHandler _OrderRecordRowChanged;
			public event OrderQueueDS.OrderRecordRowChangeEventHandler OrderRecordRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._OrderRecordRowChanged += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._OrderRecordRowChanged -= value;
				}
			}
			public  OrderQueueDS.OrderRecordRowChangeEventHandler _OrderRecordRowDeleting;
			public event OrderQueueDS.OrderRecordRowChangeEventHandler OrderRecordRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._OrderRecordRowDeleting += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._OrderRecordRowDeleting -= value;
				}
			}
            public OrderQueueDS.OrderRecordRowChangeEventHandler _OrderRecordRowDeleted;
			public event OrderQueueDS.OrderRecordRowChangeEventHandler OrderRecordRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._OrderRecordRowDeleted += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._OrderRecordRowDeleted -= value;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn sideColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnside;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn stockColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnstock;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn ttfColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnttf;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn volumeColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnvolume;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn priceColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnprice;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn pubvolColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnpubvol;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn conditionColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columncondition;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn depositColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columndeposit;
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
			public OrderQueueDS.OrderRecordRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (OrderQueueDS.OrderRecordRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public OrderRecordDataTable()
			{
				base.TableName = "OrderRecord";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal OrderRecordDataTable(DataTable table)
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
			protected OrderRecordDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddOrderRecordRow(OrderQueueDS.OrderRecordRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public OrderQueueDS.OrderRecordRow AddOrderRecordRow(string side, string stock, string ttf, string volume, string price, string pubvol, string condition, string deposit)
			{
				OrderQueueDS.OrderRecordRow orderRecordRow = (OrderQueueDS.OrderRecordRow)base.NewRow();
				object[] itemArray = new object[]
				{
					side,
					stock,
					ttf,
					volume,
					price,
					pubvol,
					condition,
					deposit
				};
				orderRecordRow.ItemArray = itemArray;
				base.Rows.Add(orderRecordRow);
				return orderRecordRow;
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
				OrderQueueDS.OrderRecordDataTable orderRecordDataTable = (OrderQueueDS.OrderRecordDataTable)base.Clone();
				orderRecordDataTable.InitVars();
				return orderRecordDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new OrderQueueDS.OrderRecordDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnside = base.Columns["side"];
				this.columnstock = base.Columns["stock"];
				this.columnttf = base.Columns["ttf"];
				this.columnvolume = base.Columns["volume"];
				this.columnprice = base.Columns["price"];
				this.columnpubvol = base.Columns["pubvol"];
				this.columncondition = base.Columns["condition"];
				this.columndeposit = base.Columns["deposit"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnside = new DataColumn("side", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnside);
				this.columnstock = new DataColumn("stock", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnstock);
				this.columnttf = new DataColumn("ttf", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnttf);
				this.columnvolume = new DataColumn("volume", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnvolume);
				this.columnprice = new DataColumn("price", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnprice);
				this.columnpubvol = new DataColumn("pubvol", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnpubvol);
				this.columncondition = new DataColumn("condition", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columncondition);
				this.columndeposit = new DataColumn("deposit", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columndeposit);
				this.columnside.DefaultValue = "";
				this.columnstock.DefaultValue = "";
				this.columnttf.DefaultValue = "";
				this.columnvolume.DefaultValue = "";
				this.columnprice.DefaultValue = "";
				this.columnpubvol.DefaultValue = "";
				this.columncondition.DefaultValue = "";
				this.columndeposit.DefaultValue = "";
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public OrderQueueDS.OrderRecordRow NewOrderRecordRow()
			{
				return (OrderQueueDS.OrderRecordRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new OrderQueueDS.OrderRecordRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(OrderQueueDS.OrderRecordRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._OrderRecordRowChanged != null)
				{
					this._OrderRecordRowChanged(this, new OrderQueueDS.OrderRecordRowChangeEvent((OrderQueueDS.OrderRecordRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._OrderRecordRowChanging != null)
				{
					this._OrderRecordRowChanging(this, new OrderQueueDS.OrderRecordRowChangeEvent((OrderQueueDS.OrderRecordRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._OrderRecordRowDeleted != null)
				{
					this._OrderRecordRowDeleted(this, new OrderQueueDS.OrderRecordRowChangeEvent((OrderQueueDS.OrderRecordRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._OrderRecordRowDeleting != null)
				{
					this._OrderRecordRowDeleting(this, new OrderQueueDS.OrderRecordRowChangeEvent((OrderQueueDS.OrderRecordRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveOrderRecordRow(OrderQueueDS.OrderRecordRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				OrderQueueDS orderQueueDS = new OrderQueueDS();
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
				xmlSchemaAttribute.FixedValue = orderQueueDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "OrderRecordDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = orderQueueDS.GetSchemaSerializable();
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
		public class OrderRecordRow : DataRow
		{
			private OrderQueueDS.OrderRecordDataTable tableOrderRecord;
			[DebuggerNonUserCode]
			public string side
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.sideColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'side' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.sideColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string stock
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.stockColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'stock' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.stockColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string ttf
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.ttfColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ttf' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.ttfColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string volume
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.volumeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'volume' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.volumeColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string price
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.priceColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'price' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.priceColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string pubvol
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.pubvolColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'pubvol' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.pubvolColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string condition
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.conditionColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'condition' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.conditionColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string deposit
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableOrderRecord.depositColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'deposit' in table 'OrderRecord' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableOrderRecord.depositColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal OrderRecordRow(DataRowBuilder rb) : base(rb)
			{
				this.tableOrderRecord = (OrderQueueDS.OrderRecordDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IssideNull()
			{
				return base.IsNull(this.tableOrderRecord.sideColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetsideNull()
			{
				base[this.tableOrderRecord.sideColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsstockNull()
			{
				return base.IsNull(this.tableOrderRecord.stockColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetstockNull()
			{
				base[this.tableOrderRecord.stockColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsttfNull()
			{
				return base.IsNull(this.tableOrderRecord.ttfColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetttfNull()
			{
				base[this.tableOrderRecord.ttfColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsvolumeNull()
			{
				return base.IsNull(this.tableOrderRecord.volumeColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetvolumeNull()
			{
				base[this.tableOrderRecord.volumeColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IspriceNull()
			{
				return base.IsNull(this.tableOrderRecord.priceColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetpriceNull()
			{
				base[this.tableOrderRecord.priceColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IspubvolNull()
			{
				return base.IsNull(this.tableOrderRecord.pubvolColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetpubvolNull()
			{
				base[this.tableOrderRecord.pubvolColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsconditionNull()
			{
				return base.IsNull(this.tableOrderRecord.conditionColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetconditionNull()
			{
				base[this.tableOrderRecord.conditionColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsdepositNull()
			{
				return base.IsNull(this.tableOrderRecord.depositColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetdepositNull()
			{
				base[this.tableOrderRecord.depositColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class OrderRecordRowChangeEvent : EventArgs
		{
			private OrderQueueDS.OrderRecordRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public OrderQueueDS.OrderRecordRow Row
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
			public OrderRecordRowChangeEvent(OrderQueueDS.OrderRecordRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		private OrderQueueDS.OrderRecordDataTable tableOrderRecord;
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public OrderQueueDS.OrderRecordDataTable OrderRecord
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableOrderRecord;
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
		public OrderQueueDS()
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
		protected OrderQueueDS(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
					if (dataSet.Tables["OrderRecord"] != null)
					{
						base.Tables.Add(new OrderQueueDS.OrderRecordDataTable(dataSet.Tables["OrderRecord"]));
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
			OrderQueueDS orderQueueDS = (OrderQueueDS)base.Clone();
			orderQueueDS.InitVars();
			orderQueueDS.SchemaSerializationMode = this.SchemaSerializationMode;
			return orderQueueDS;
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
				if (dataSet.Tables["OrderRecord"] != null)
				{
					base.Tables.Add(new OrderQueueDS.OrderRecordDataTable(dataSet.Tables["OrderRecord"]));
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
			this.tableOrderRecord = (OrderQueueDS.OrderRecordDataTable)base.Tables["OrderRecord"];
			if (initTable)
			{
				if (this.tableOrderRecord != null)
				{
					this.tableOrderRecord.InitVars();
				}
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitClass()
		{
			base.DataSetName = "OrderQueueDS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/OrderQueueDS.xsd";
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.tableOrderRecord = new OrderQueueDS.OrderRecordDataTable();
			base.Tables.Add(this.tableOrderRecord);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeOrderRecord()
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
			OrderQueueDS orderQueueDS = new OrderQueueDS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = orderQueueDS.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = orderQueueDS.GetSchemaSerializable();
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
