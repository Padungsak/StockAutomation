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
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), HelpKeyword("vs.data.DataSet"), DesignerCategory("code"), ToolboxItem(true), XmlRoot("AlertCustomerExpressionDS"), XmlSchemaProvider("GetTypedDataSetSchema")]
	[Serializable]
	public class AlertCustomerExpressionDS : DataSet
	{
		public delegate void AlertCollectionRowChangeEventHandler(object sender, AlertCustomerExpressionDS.AlertCollectionRowChangeEvent e);
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class AlertCollectionDataTable : DataTable, IEnumerable
		{
			private DataColumn columnSymbol;
			private DataColumn columnValues;
			private DataColumn columnOperator;
			private DataColumn columnColumnsAlert;
			private DataColumn columnIsFirstTimeOnly;
			private DataColumn columnIsPrepareOrder;
			private DataColumn columnOrderParam;
			private DataColumn columnAlertTime;
			private DataColumn columnAlertValue;
			public  AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler _AlertCollectionRowChanging;
			public event AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler AlertCollectionRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._AlertCollectionRowChanging += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._AlertCollectionRowChanging -= value;
                }
			}
			public  AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler _AlertCollectionRowChanged;
			public event AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler AlertCollectionRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._AlertCollectionRowChanged += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._AlertCollectionRowChanged -= value;
                }
			}
			public  AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler _AlertCollectionRowDeleting;
			public event AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler AlertCollectionRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._AlertCollectionRowDeleting += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._AlertCollectionRowDeleting -= value;
                }
			}
            public AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler _AlertCollectionRowDeleted;
			public event AlertCustomerExpressionDS.AlertCollectionRowChangeEventHandler AlertCollectionRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._AlertCollectionRowDeleted += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._AlertCollectionRowDeleted -= value;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn SymbolColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnSymbol;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn ValuesColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnValues;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn OperatorColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnOperator;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn ColumnsAlertColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnColumnsAlert;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn IsFirstTimeOnlyColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnIsFirstTimeOnly;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn IsPrepareOrderColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnIsPrepareOrder;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn OrderParamColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnOrderParam;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn AlertTimeColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnAlertTime;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn AlertValueColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnAlertValue;
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
			public AlertCustomerExpressionDS.AlertCollectionRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (AlertCustomerExpressionDS.AlertCollectionRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public AlertCollectionDataTable()
			{
				base.TableName = "AlertCollection";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal AlertCollectionDataTable(DataTable table)
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
			protected AlertCollectionDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddAlertCollectionRow(AlertCustomerExpressionDS.AlertCollectionRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public AlertCustomerExpressionDS.AlertCollectionRow AddAlertCollectionRow(string Symbol, string Values, string Operator, string ColumnsAlert, bool IsFirstTimeOnly, bool IsPrepareOrder, string OrderParam, string AlertTime, string AlertValue)
			{
				AlertCustomerExpressionDS.AlertCollectionRow alertCollectionRow = (AlertCustomerExpressionDS.AlertCollectionRow)base.NewRow();
				object[] itemArray = new object[]
				{
					Symbol,
					Values,
					Operator,
					ColumnsAlert,
					IsFirstTimeOnly,
					IsPrepareOrder,
					OrderParam,
					AlertTime,
					AlertValue
				};
				alertCollectionRow.ItemArray = itemArray;
				base.Rows.Add(alertCollectionRow);
				return alertCollectionRow;
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
				AlertCustomerExpressionDS.AlertCollectionDataTable alertCollectionDataTable = (AlertCustomerExpressionDS.AlertCollectionDataTable)base.Clone();
				alertCollectionDataTable.InitVars();
				return alertCollectionDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new AlertCustomerExpressionDS.AlertCollectionDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnSymbol = base.Columns["Symbol"];
				this.columnValues = base.Columns["Values"];
				this.columnOperator = base.Columns["Operator"];
				this.columnColumnsAlert = base.Columns["ColumnsAlert"];
				this.columnIsFirstTimeOnly = base.Columns["IsFirstTimeOnly"];
				this.columnIsPrepareOrder = base.Columns["IsPrepareOrder"];
				this.columnOrderParam = base.Columns["OrderParam"];
				this.columnAlertTime = base.Columns["AlertTime"];
				this.columnAlertValue = base.Columns["AlertValue"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnSymbol = new DataColumn("Symbol", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnSymbol);
				this.columnValues = new DataColumn("Values", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnValues);
				this.columnOperator = new DataColumn("Operator", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnOperator);
				this.columnColumnsAlert = new DataColumn("ColumnsAlert", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnColumnsAlert);
				this.columnIsFirstTimeOnly = new DataColumn("IsFirstTimeOnly", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnIsFirstTimeOnly);
				this.columnIsPrepareOrder = new DataColumn("IsPrepareOrder", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnIsPrepareOrder);
				this.columnOrderParam = new DataColumn("OrderParam", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnOrderParam);
				this.columnAlertTime = new DataColumn("AlertTime", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnAlertTime);
				this.columnAlertValue = new DataColumn("AlertValue", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnAlertValue);
				this.columnSymbol.DefaultValue = "";
				this.columnValues.DefaultValue = "";
				this.columnOperator.DefaultValue = "";
				this.columnColumnsAlert.DefaultValue = "";
				this.columnIsFirstTimeOnly.DefaultValue = false;
				this.columnIsPrepareOrder.AllowDBNull = false;
				this.columnIsPrepareOrder.DefaultValue = false;
				this.columnOrderParam.AllowDBNull = false;
				this.columnOrderParam.DefaultValue = "";
				this.columnAlertTime.DefaultValue = "";
				this.columnAlertValue.DefaultValue = "";
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public AlertCustomerExpressionDS.AlertCollectionRow NewAlertCollectionRow()
			{
				return (AlertCustomerExpressionDS.AlertCollectionRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new AlertCustomerExpressionDS.AlertCollectionRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(AlertCustomerExpressionDS.AlertCollectionRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._AlertCollectionRowChanged != null)
				{
					this._AlertCollectionRowChanged(this, new AlertCustomerExpressionDS.AlertCollectionRowChangeEvent((AlertCustomerExpressionDS.AlertCollectionRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._AlertCollectionRowChanging != null)
				{
					this._AlertCollectionRowChanging(this, new AlertCustomerExpressionDS.AlertCollectionRowChangeEvent((AlertCustomerExpressionDS.AlertCollectionRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._AlertCollectionRowDeleted != null)
				{
					this._AlertCollectionRowDeleted(this, new AlertCustomerExpressionDS.AlertCollectionRowChangeEvent((AlertCustomerExpressionDS.AlertCollectionRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._AlertCollectionRowDeleting != null)
				{
					this._AlertCollectionRowDeleting(this, new AlertCustomerExpressionDS.AlertCollectionRowChangeEvent((AlertCustomerExpressionDS.AlertCollectionRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveAlertCollectionRow(AlertCustomerExpressionDS.AlertCollectionRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				AlertCustomerExpressionDS alertCustomerExpressionDS = new AlertCustomerExpressionDS();
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
				xmlSchemaAttribute.FixedValue = alertCustomerExpressionDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "AlertCollectionDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = alertCustomerExpressionDS.GetSchemaSerializable();
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
		public class AlertCollectionRow : DataRow
		{
			private AlertCustomerExpressionDS.AlertCollectionDataTable tableAlertCollection;
			[DebuggerNonUserCode]
			public string Symbol
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsSymbolNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableAlertCollection.SymbolColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.SymbolColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string Values
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsValuesNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableAlertCollection.ValuesColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.ValuesColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string Operator
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsOperatorNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableAlertCollection.OperatorColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.OperatorColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string ColumnsAlert
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsColumnsAlertNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableAlertCollection.ColumnsAlertColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.ColumnsAlertColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool IsFirstTimeOnly
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					bool result;
					try
					{
						result = (bool)base[this.tableAlertCollection.IsFirstTimeOnlyColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'IsFirstTimeOnly' in table 'AlertCollection' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.IsFirstTimeOnlyColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool IsPrepareOrder
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (bool)base[this.tableAlertCollection.IsPrepareOrderColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.IsPrepareOrderColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string OrderParam
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (string)base[this.tableAlertCollection.OrderParamColumn];
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.OrderParamColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string AlertTime
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsAlertTimeNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableAlertCollection.AlertTimeColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.AlertTimeColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string AlertValue
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsAlertValueNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableAlertCollection.AlertValueColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableAlertCollection.AlertValueColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal AlertCollectionRow(DataRowBuilder rb) : base(rb)
			{
				this.tableAlertCollection = (AlertCustomerExpressionDS.AlertCollectionDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsSymbolNull()
			{
				return base.IsNull(this.tableAlertCollection.SymbolColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetSymbolNull()
			{
				base[this.tableAlertCollection.SymbolColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsValuesNull()
			{
				return base.IsNull(this.tableAlertCollection.ValuesColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetValuesNull()
			{
				base[this.tableAlertCollection.ValuesColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsOperatorNull()
			{
				return base.IsNull(this.tableAlertCollection.OperatorColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetOperatorNull()
			{
				base[this.tableAlertCollection.OperatorColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsColumnsAlertNull()
			{
				return base.IsNull(this.tableAlertCollection.ColumnsAlertColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetColumnsAlertNull()
			{
				base[this.tableAlertCollection.ColumnsAlertColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsIsFirstTimeOnlyNull()
			{
				return base.IsNull(this.tableAlertCollection.IsFirstTimeOnlyColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetIsFirstTimeOnlyNull()
			{
				base[this.tableAlertCollection.IsFirstTimeOnlyColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsAlertTimeNull()
			{
				return base.IsNull(this.tableAlertCollection.AlertTimeColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetAlertTimeNull()
			{
				base[this.tableAlertCollection.AlertTimeColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsAlertValueNull()
			{
				return base.IsNull(this.tableAlertCollection.AlertValueColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetAlertValueNull()
			{
				base[this.tableAlertCollection.AlertValueColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class AlertCollectionRowChangeEvent : EventArgs
		{
			private AlertCustomerExpressionDS.AlertCollectionRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public AlertCustomerExpressionDS.AlertCollectionRow Row
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
			public AlertCollectionRowChangeEvent(AlertCustomerExpressionDS.AlertCollectionRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		private AlertCustomerExpressionDS.AlertCollectionDataTable tableAlertCollection;
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public AlertCustomerExpressionDS.AlertCollectionDataTable AlertCollection
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableAlertCollection;
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
		public AlertCustomerExpressionDS()
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
		protected AlertCustomerExpressionDS(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
					if (dataSet.Tables["AlertCollection"] != null)
					{
						base.Tables.Add(new AlertCustomerExpressionDS.AlertCollectionDataTable(dataSet.Tables["AlertCollection"]));
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
			AlertCustomerExpressionDS alertCustomerExpressionDS = (AlertCustomerExpressionDS)base.Clone();
			alertCustomerExpressionDS.InitVars();
			alertCustomerExpressionDS.SchemaSerializationMode = this.SchemaSerializationMode;
			return alertCustomerExpressionDS;
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
				if (dataSet.Tables["AlertCollection"] != null)
				{
					base.Tables.Add(new AlertCustomerExpressionDS.AlertCollectionDataTable(dataSet.Tables["AlertCollection"]));
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
			this.tableAlertCollection = (AlertCustomerExpressionDS.AlertCollectionDataTable)base.Tables["AlertCollection"];
			if (initTable)
			{
				if (this.tableAlertCollection != null)
				{
					this.tableAlertCollection.InitVars();
				}
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitClass()
		{
			base.DataSetName = "AlertCustomerExpressionDS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/AlertCustomerExpressionDS.xsd";
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.tableAlertCollection = new AlertCustomerExpressionDS.AlertCollectionDataTable();
			base.Tables.Add(this.tableAlertCollection);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeAlertCollection()
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
			AlertCustomerExpressionDS alertCustomerExpressionDS = new AlertCustomerExpressionDS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = alertCustomerExpressionDS.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = alertCustomerExpressionDS.GetSchemaSerializable();
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
