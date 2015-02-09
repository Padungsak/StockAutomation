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
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), HelpKeyword("vs.data.DataSet"), DesignerCategory("code"), ToolboxItem(true), XmlRoot("TemplateDS"), XmlSchemaProvider("GetTypedDataSetSchema")]
	[Serializable]
	public class TemplateDS : DataSet
	{
		public delegate void TemplatePropertyRowChangeEventHandler(object sender, TemplateDS.TemplatePropertyRowChangeEvent e);
		public delegate void FormInfoRowChangeEventHandler(object sender, TemplateDS.FormInfoRowChangeEvent e);
		public delegate void FormPropertyRowChangeEventHandler(object sender, TemplateDS.FormPropertyRowChangeEvent e);
		public delegate void FormRememberFieldRowChangeEventHandler(object sender, TemplateDS.FormRememberFieldRowChangeEvent e);
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class TemplatePropertyDataTable : DataTable, IEnumerable
		{
			private DataColumn columnPropertyName;
			private DataColumn columnPropertyValue;
			public TemplateDS.TemplatePropertyRowChangeEventHandler _TemplatePropertyRowChanging;
			public event TemplateDS.TemplatePropertyRowChangeEventHandler TemplatePropertyRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplatePropertyRowChanging += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplatePropertyRowChanging -= value;
                }
			}
			public  TemplateDS.TemplatePropertyRowChangeEventHandler _TemplatePropertyRowChanged;
			public event TemplateDS.TemplatePropertyRowChangeEventHandler TemplatePropertyRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplatePropertyRowChanged += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplatePropertyRowChanged -= value;
                }
			}
			public  TemplateDS.TemplatePropertyRowChangeEventHandler _TemplatePropertyRowDeleting;
			public event TemplateDS.TemplatePropertyRowChangeEventHandler TemplatePropertyRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplatePropertyRowDeleting += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplatePropertyRowDeleting -= value;
                }
			}
            public TemplateDS.TemplatePropertyRowChangeEventHandler _TemplatePropertyRowDeleted;
			public event TemplateDS.TemplatePropertyRowChangeEventHandler TemplatePropertyRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplatePropertyRowDeleted += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplatePropertyRowDeleted -= value;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn PropertyNameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnPropertyName;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn PropertyValueColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnPropertyValue;
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
			public TemplateDS.TemplatePropertyRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (TemplateDS.TemplatePropertyRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplatePropertyDataTable()
			{
				base.TableName = "TemplateProperty";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal TemplatePropertyDataTable(DataTable table)
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
			protected TemplatePropertyDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddTemplatePropertyRow(TemplateDS.TemplatePropertyRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.TemplatePropertyRow AddTemplatePropertyRow(string PropertyName, string PropertyValue)
			{
				TemplateDS.TemplatePropertyRow templatePropertyRow = (TemplateDS.TemplatePropertyRow)base.NewRow();
				object[] itemArray = new object[]
				{
					PropertyName,
					PropertyValue
				};
				templatePropertyRow.ItemArray = itemArray;
				base.Rows.Add(templatePropertyRow);
				return templatePropertyRow;
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
				TemplateDS.TemplatePropertyDataTable templatePropertyDataTable = (TemplateDS.TemplatePropertyDataTable)base.Clone();
				templatePropertyDataTable.InitVars();
				return templatePropertyDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new TemplateDS.TemplatePropertyDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnPropertyName = base.Columns["PropertyName"];
				this.columnPropertyValue = base.Columns["PropertyValue"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnPropertyName = new DataColumn("PropertyName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnPropertyName);
				this.columnPropertyValue = new DataColumn("PropertyValue", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnPropertyValue);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.TemplatePropertyRow NewTemplatePropertyRow()
			{
				return (TemplateDS.TemplatePropertyRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TemplateDS.TemplatePropertyRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(TemplateDS.TemplatePropertyRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._TemplatePropertyRowChanged != null)
				{
					this._TemplatePropertyRowChanged(this, new TemplateDS.TemplatePropertyRowChangeEvent((TemplateDS.TemplatePropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._TemplatePropertyRowChanging != null)
				{
					this._TemplatePropertyRowChanging(this, new TemplateDS.TemplatePropertyRowChangeEvent((TemplateDS.TemplatePropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._TemplatePropertyRowDeleted != null)
				{
					this._TemplatePropertyRowDeleted(this, new TemplateDS.TemplatePropertyRowChangeEvent((TemplateDS.TemplatePropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._TemplatePropertyRowDeleting != null)
				{
					this._TemplatePropertyRowDeleting(this, new TemplateDS.TemplatePropertyRowChangeEvent((TemplateDS.TemplatePropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveTemplatePropertyRow(TemplateDS.TemplatePropertyRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TemplateDS templateDS = new TemplateDS();
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
				xmlSchemaAttribute.FixedValue = templateDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "TemplatePropertyDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = templateDS.GetSchemaSerializable();
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
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class FormInfoDataTable : DataTable, IEnumerable
		{
			private DataColumn columnFormID;
			private DataColumn columnType;
			private DataColumn columnName;
			public TemplateDS.FormInfoRowChangeEventHandler _FormInfoRowChanging;
			public event TemplateDS.FormInfoRowChangeEventHandler FormInfoRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._FormInfoRowChanging += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormInfoRowChanging -= value;
                }
			}
			public TemplateDS.FormInfoRowChangeEventHandler _FormInfoRowChanged;
			public event TemplateDS.FormInfoRowChangeEventHandler FormInfoRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormInfoRowChanged += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormInfoRowChanged -= value;
                }
			}
			public TemplateDS.FormInfoRowChangeEventHandler _FormInfoRowDeleting;
			public event TemplateDS.FormInfoRowChangeEventHandler FormInfoRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormInfoRowDeleting += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormInfoRowDeleting -= value;
                }
			}
            public TemplateDS.FormInfoRowChangeEventHandler _FormInfoRowDeleted;
			public event TemplateDS.FormInfoRowChangeEventHandler FormInfoRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormInfoRowDeleted += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormInfoRowDeleted -= value;
                }
			}
			[DebuggerNonUserCode]
			public DataColumn FormIDColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnFormID;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn TypeColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnType;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn NameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnName;
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
			public TemplateDS.FormInfoRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (TemplateDS.FormInfoRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public FormInfoDataTable()
			{
				base.TableName = "FormInfo";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal FormInfoDataTable(DataTable table)
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
			protected FormInfoDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddFormInfoRow(TemplateDS.FormInfoRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.FormInfoRow AddFormInfoRow(int FormID, string Type, string Name)
			{
				TemplateDS.FormInfoRow formInfoRow = (TemplateDS.FormInfoRow)base.NewRow();
				object[] itemArray = new object[]
				{
					FormID,
					Type,
					Name
				};
				formInfoRow.ItemArray = itemArray;
				base.Rows.Add(formInfoRow);
				return formInfoRow;
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
				TemplateDS.FormInfoDataTable formInfoDataTable = (TemplateDS.FormInfoDataTable)base.Clone();
				formInfoDataTable.InitVars();
				return formInfoDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new TemplateDS.FormInfoDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnFormID = base.Columns["FormID"];
				this.columnType = base.Columns["Type"];
				this.columnName = base.Columns["Name"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnFormID = new DataColumn("FormID", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnFormID);
				this.columnType = new DataColumn("Type", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnType);
				this.columnName = new DataColumn("Name", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnName);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.FormInfoRow NewFormInfoRow()
			{
				return (TemplateDS.FormInfoRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TemplateDS.FormInfoRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(TemplateDS.FormInfoRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._FormInfoRowChanged != null)
				{
					this._FormInfoRowChanged(this, new TemplateDS.FormInfoRowChangeEvent((TemplateDS.FormInfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._FormInfoRowChanging != null)
				{
					this._FormInfoRowChanging(this, new TemplateDS.FormInfoRowChangeEvent((TemplateDS.FormInfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._FormInfoRowDeleted != null)
				{
					this._FormInfoRowDeleted(this, new TemplateDS.FormInfoRowChangeEvent((TemplateDS.FormInfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._FormInfoRowDeleting != null)
				{
					this._FormInfoRowDeleting(this, new TemplateDS.FormInfoRowChangeEvent((TemplateDS.FormInfoRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveFormInfoRow(TemplateDS.FormInfoRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TemplateDS templateDS = new TemplateDS();
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
				xmlSchemaAttribute.FixedValue = templateDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "FormInfoDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = templateDS.GetSchemaSerializable();
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
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class FormPropertyDataTable : DataTable, IEnumerable
		{
			private DataColumn columnFormID;
			private DataColumn columnPropertyName;
			private DataColumn columnPropertyValue;
            public TemplateDS.FormPropertyRowChangeEventHandler _FormPropertyRowChanging;
			public event TemplateDS.FormPropertyRowChangeEventHandler FormPropertyRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._FormPropertyRowChanging +=  value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormPropertyRowChanging -= value;
                }
			}
			public  TemplateDS.FormPropertyRowChangeEventHandler _FormPropertyRowChanged;
			public event TemplateDS.FormPropertyRowChangeEventHandler FormPropertyRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormPropertyRowChanged += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormPropertyRowChanged -= value;
                }
			}
			public  TemplateDS.FormPropertyRowChangeEventHandler _FormPropertyRowDeleting;
			public event TemplateDS.FormPropertyRowChangeEventHandler FormPropertyRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormPropertyRowDeleting += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormPropertyRowDeleting -= value;
                }
			}
			public  TemplateDS.FormPropertyRowChangeEventHandler _FormPropertyRowDeleted;
			public event TemplateDS.FormPropertyRowChangeEventHandler FormPropertyRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormPropertyRowDeleted += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormPropertyRowDeleted -= value;
                }
			}
			[DebuggerNonUserCode]
			public DataColumn FormIDColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnFormID;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn PropertyNameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnPropertyName;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn PropertyValueColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnPropertyValue;
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
			public TemplateDS.FormPropertyRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (TemplateDS.FormPropertyRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public FormPropertyDataTable()
			{
				base.TableName = "FormProperty";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal FormPropertyDataTable(DataTable table)
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
			protected FormPropertyDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddFormPropertyRow(TemplateDS.FormPropertyRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.FormPropertyRow AddFormPropertyRow(int FormID, string PropertyName, string PropertyValue)
			{
				TemplateDS.FormPropertyRow formPropertyRow = (TemplateDS.FormPropertyRow)base.NewRow();
				object[] itemArray = new object[]
				{
					FormID,
					PropertyName,
					PropertyValue
				};
				formPropertyRow.ItemArray = itemArray;
				base.Rows.Add(formPropertyRow);
				return formPropertyRow;
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
				TemplateDS.FormPropertyDataTable formPropertyDataTable = (TemplateDS.FormPropertyDataTable)base.Clone();
				formPropertyDataTable.InitVars();
				return formPropertyDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new TemplateDS.FormPropertyDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnFormID = base.Columns["FormID"];
				this.columnPropertyName = base.Columns["PropertyName"];
				this.columnPropertyValue = base.Columns["PropertyValue"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnFormID = new DataColumn("FormID", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnFormID);
				this.columnPropertyName = new DataColumn("PropertyName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnPropertyName);
				this.columnPropertyValue = new DataColumn("PropertyValue", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnPropertyValue);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.FormPropertyRow NewFormPropertyRow()
			{
				return (TemplateDS.FormPropertyRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TemplateDS.FormPropertyRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(TemplateDS.FormPropertyRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._FormPropertyRowChanged != null)
				{
					this._FormPropertyRowChanged(this, new TemplateDS.FormPropertyRowChangeEvent((TemplateDS.FormPropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._FormPropertyRowChanging != null)
				{
					this._FormPropertyRowChanging(this, new TemplateDS.FormPropertyRowChangeEvent((TemplateDS.FormPropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._FormPropertyRowDeleted != null)
				{
					this._FormPropertyRowDeleted(this, new TemplateDS.FormPropertyRowChangeEvent((TemplateDS.FormPropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._FormPropertyRowDeleting != null)
				{
					this._FormPropertyRowDeleting(this, new TemplateDS.FormPropertyRowChangeEvent((TemplateDS.FormPropertyRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveFormPropertyRow(TemplateDS.FormPropertyRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TemplateDS templateDS = new TemplateDS();
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
				xmlSchemaAttribute.FixedValue = templateDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "FormPropertyDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = templateDS.GetSchemaSerializable();
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
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class FormRememberFieldDataTable : DataTable, IEnumerable
		{
			private DataColumn columnFormID;
			private DataColumn columnFieldName;
			private DataColumn columnFieldValue;
			public  TemplateDS.FormRememberFieldRowChangeEventHandler _FormRememberFieldRowChanging;
			public event TemplateDS.FormRememberFieldRowChangeEventHandler FormRememberFieldRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormRememberFieldRowChanging += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormRememberFieldRowChanging -= value;
                }
			}
			public  TemplateDS.FormRememberFieldRowChangeEventHandler _FormRememberFieldRowChanged;
			public event TemplateDS.FormRememberFieldRowChangeEventHandler FormRememberFieldRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormRememberFieldRowChanged += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormRememberFieldRowChanged -= value;
                }
			}
			public  TemplateDS.FormRememberFieldRowChangeEventHandler _FormRememberFieldRowDeleting;
			public event TemplateDS.FormRememberFieldRowChangeEventHandler FormRememberFieldRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormRememberFieldRowDeleting += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._FormRememberFieldRowDeleting -= value;
                }
			}
            public TemplateDS.FormRememberFieldRowChangeEventHandler _FormRememberFieldRowDeleted;
			public event TemplateDS.FormRememberFieldRowChangeEventHandler FormRememberFieldRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._FormRememberFieldRowDeleted += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._FormRememberFieldRowDeleted -= value;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn FormIDColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnFormID;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn FieldNameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnFieldName;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn FieldValueColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnFieldValue;
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
			public TemplateDS.FormRememberFieldRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (TemplateDS.FormRememberFieldRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public FormRememberFieldDataTable()
			{
				base.TableName = "FormRememberField";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal FormRememberFieldDataTable(DataTable table)
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
			protected FormRememberFieldDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddFormRememberFieldRow(TemplateDS.FormRememberFieldRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.FormRememberFieldRow AddFormRememberFieldRow(int FormID, string FieldName, string FieldValue)
			{
				TemplateDS.FormRememberFieldRow formRememberFieldRow = (TemplateDS.FormRememberFieldRow)base.NewRow();
				object[] itemArray = new object[]
				{
					FormID,
					FieldName,
					FieldValue
				};
				formRememberFieldRow.ItemArray = itemArray;
				base.Rows.Add(formRememberFieldRow);
				return formRememberFieldRow;
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
				TemplateDS.FormRememberFieldDataTable formRememberFieldDataTable = (TemplateDS.FormRememberFieldDataTable)base.Clone();
				formRememberFieldDataTable.InitVars();
				return formRememberFieldDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new TemplateDS.FormRememberFieldDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnFormID = base.Columns["FormID"];
				this.columnFieldName = base.Columns["FieldName"];
				this.columnFieldValue = base.Columns["FieldValue"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnFormID = new DataColumn("FormID", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnFormID);
				this.columnFieldName = new DataColumn("FieldName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnFieldName);
				this.columnFieldValue = new DataColumn("FieldValue", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnFieldValue);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateDS.FormRememberFieldRow NewFormRememberFieldRow()
			{
				return (TemplateDS.FormRememberFieldRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TemplateDS.FormRememberFieldRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(TemplateDS.FormRememberFieldRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._FormRememberFieldRowChanged != null)
				{
					this._FormRememberFieldRowChanged(this, new TemplateDS.FormRememberFieldRowChangeEvent((TemplateDS.FormRememberFieldRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._FormRememberFieldRowChanging != null)
				{
					this._FormRememberFieldRowChanging(this, new TemplateDS.FormRememberFieldRowChangeEvent((TemplateDS.FormRememberFieldRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._FormRememberFieldRowDeleted != null)
				{
					this._FormRememberFieldRowDeleted(this, new TemplateDS.FormRememberFieldRowChangeEvent((TemplateDS.FormRememberFieldRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._FormRememberFieldRowDeleting != null)
				{
					this._FormRememberFieldRowDeleting(this, new TemplateDS.FormRememberFieldRowChangeEvent((TemplateDS.FormRememberFieldRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveFormRememberFieldRow(TemplateDS.FormRememberFieldRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TemplateDS templateDS = new TemplateDS();
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
				xmlSchemaAttribute.FixedValue = templateDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "FormRememberFieldDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = templateDS.GetSchemaSerializable();
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
		public class TemplatePropertyRow : DataRow
		{
			private TemplateDS.TemplatePropertyDataTable tableTemplateProperty;
			[DebuggerNonUserCode]
			public string PropertyName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableTemplateProperty.PropertyNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PropertyName' in table 'TemplateProperty' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateProperty.PropertyNameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string PropertyValue
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableTemplateProperty.PropertyValueColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PropertyValue' in table 'TemplateProperty' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateProperty.PropertyValueColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal TemplatePropertyRow(DataRowBuilder rb) : base(rb)
			{
				this.tableTemplateProperty = (TemplateDS.TemplatePropertyDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsPropertyNameNull()
			{
				return base.IsNull(this.tableTemplateProperty.PropertyNameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetPropertyNameNull()
			{
				base[this.tableTemplateProperty.PropertyNameColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsPropertyValueNull()
			{
				return base.IsNull(this.tableTemplateProperty.PropertyValueColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetPropertyValueNull()
			{
				base[this.tableTemplateProperty.PropertyValueColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class FormInfoRow : DataRow
		{
			private TemplateDS.FormInfoDataTable tableFormInfo;
			[DebuggerNonUserCode]
			public int FormID
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableFormInfo.FormIDColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FormID' in table 'FormInfo' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormInfo.FormIDColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string Type
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableFormInfo.TypeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Type' in table 'FormInfo' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormInfo.TypeColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string Name
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableFormInfo.NameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Name' in table 'FormInfo' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormInfo.NameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal FormInfoRow(DataRowBuilder rb) : base(rb)
			{
				this.tableFormInfo = (TemplateDS.FormInfoDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsFormIDNull()
			{
				return base.IsNull(this.tableFormInfo.FormIDColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetFormIDNull()
			{
				base[this.tableFormInfo.FormIDColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsTypeNull()
			{
				return base.IsNull(this.tableFormInfo.TypeColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetTypeNull()
			{
				base[this.tableFormInfo.TypeColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsNameNull()
			{
				return base.IsNull(this.tableFormInfo.NameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetNameNull()
			{
				base[this.tableFormInfo.NameColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class FormPropertyRow : DataRow
		{
			private TemplateDS.FormPropertyDataTable tableFormProperty;
			[DebuggerNonUserCode]
			public int FormID
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableFormProperty.FormIDColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FormID' in table 'FormProperty' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormProperty.FormIDColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string PropertyName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableFormProperty.PropertyNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PropertyName' in table 'FormProperty' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormProperty.PropertyNameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string PropertyValue
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableFormProperty.PropertyValueColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PropertyValue' in table 'FormProperty' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormProperty.PropertyValueColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal FormPropertyRow(DataRowBuilder rb) : base(rb)
			{
				this.tableFormProperty = (TemplateDS.FormPropertyDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsFormIDNull()
			{
				return base.IsNull(this.tableFormProperty.FormIDColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetFormIDNull()
			{
				base[this.tableFormProperty.FormIDColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsPropertyNameNull()
			{
				return base.IsNull(this.tableFormProperty.PropertyNameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetPropertyNameNull()
			{
				base[this.tableFormProperty.PropertyNameColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsPropertyValueNull()
			{
				return base.IsNull(this.tableFormProperty.PropertyValueColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetPropertyValueNull()
			{
				base[this.tableFormProperty.PropertyValueColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class FormRememberFieldRow : DataRow
		{
			private TemplateDS.FormRememberFieldDataTable tableFormRememberField;
			[DebuggerNonUserCode]
			public int FormID
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableFormRememberField.FormIDColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FormID' in table 'FormRememberField' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormRememberField.FormIDColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string FieldName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableFormRememberField.FieldNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FieldName' in table 'FormRememberField' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormRememberField.FieldNameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string FieldValue
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableFormRememberField.FieldValueColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FieldValue' in table 'FormRememberField' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableFormRememberField.FieldValueColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal FormRememberFieldRow(DataRowBuilder rb) : base(rb)
			{
				this.tableFormRememberField = (TemplateDS.FormRememberFieldDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsFormIDNull()
			{
				return base.IsNull(this.tableFormRememberField.FormIDColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetFormIDNull()
			{
				base[this.tableFormRememberField.FormIDColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsFieldNameNull()
			{
				return base.IsNull(this.tableFormRememberField.FieldNameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetFieldNameNull()
			{
				base[this.tableFormRememberField.FieldNameColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsFieldValueNull()
			{
				return base.IsNull(this.tableFormRememberField.FieldValueColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetFieldValueNull()
			{
				base[this.tableFormRememberField.FieldValueColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class TemplatePropertyRowChangeEvent : EventArgs
		{
			private TemplateDS.TemplatePropertyRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public TemplateDS.TemplatePropertyRow Row
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
			public TemplatePropertyRowChangeEvent(TemplateDS.TemplatePropertyRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class FormInfoRowChangeEvent : EventArgs
		{
			private TemplateDS.FormInfoRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public TemplateDS.FormInfoRow Row
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
			public FormInfoRowChangeEvent(TemplateDS.FormInfoRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class FormPropertyRowChangeEvent : EventArgs
		{
			private TemplateDS.FormPropertyRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public TemplateDS.FormPropertyRow Row
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
			public FormPropertyRowChangeEvent(TemplateDS.FormPropertyRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class FormRememberFieldRowChangeEvent : EventArgs
		{
			private TemplateDS.FormRememberFieldRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public TemplateDS.FormRememberFieldRow Row
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
			public FormRememberFieldRowChangeEvent(TemplateDS.FormRememberFieldRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		private TemplateDS.TemplatePropertyDataTable tableTemplateProperty;
		private TemplateDS.FormInfoDataTable tableFormInfo;
		private TemplateDS.FormPropertyDataTable tableFormProperty;
		private TemplateDS.FormRememberFieldDataTable tableFormRememberField;
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public TemplateDS.TemplatePropertyDataTable TemplateProperty
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableTemplateProperty;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public TemplateDS.FormInfoDataTable FormInfo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableFormInfo;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public TemplateDS.FormPropertyDataTable FormProperty
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableFormProperty;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public TemplateDS.FormRememberFieldDataTable FormRememberField
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableFormRememberField;
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
		public TemplateDS()
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
		protected TemplateDS(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
					if (dataSet.Tables["TemplateProperty"] != null)
					{
						base.Tables.Add(new TemplateDS.TemplatePropertyDataTable(dataSet.Tables["TemplateProperty"]));
					}
					if (dataSet.Tables["FormInfo"] != null)
					{
						base.Tables.Add(new TemplateDS.FormInfoDataTable(dataSet.Tables["FormInfo"]));
					}
					if (dataSet.Tables["FormProperty"] != null)
					{
						base.Tables.Add(new TemplateDS.FormPropertyDataTable(dataSet.Tables["FormProperty"]));
					}
					if (dataSet.Tables["FormRememberField"] != null)
					{
						base.Tables.Add(new TemplateDS.FormRememberFieldDataTable(dataSet.Tables["FormRememberField"]));
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
			TemplateDS templateDS = (TemplateDS)base.Clone();
			templateDS.InitVars();
			templateDS.SchemaSerializationMode = this.SchemaSerializationMode;
			return templateDS;
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
				if (dataSet.Tables["TemplateProperty"] != null)
				{
					base.Tables.Add(new TemplateDS.TemplatePropertyDataTable(dataSet.Tables["TemplateProperty"]));
				}
				if (dataSet.Tables["FormInfo"] != null)
				{
					base.Tables.Add(new TemplateDS.FormInfoDataTable(dataSet.Tables["FormInfo"]));
				}
				if (dataSet.Tables["FormProperty"] != null)
				{
					base.Tables.Add(new TemplateDS.FormPropertyDataTable(dataSet.Tables["FormProperty"]));
				}
				if (dataSet.Tables["FormRememberField"] != null)
				{
					base.Tables.Add(new TemplateDS.FormRememberFieldDataTable(dataSet.Tables["FormRememberField"]));
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
			this.tableTemplateProperty = (TemplateDS.TemplatePropertyDataTable)base.Tables["TemplateProperty"];
			if (initTable)
			{
				if (this.tableTemplateProperty != null)
				{
					this.tableTemplateProperty.InitVars();
				}
			}
			this.tableFormInfo = (TemplateDS.FormInfoDataTable)base.Tables["FormInfo"];
			if (initTable)
			{
				if (this.tableFormInfo != null)
				{
					this.tableFormInfo.InitVars();
				}
			}
			this.tableFormProperty = (TemplateDS.FormPropertyDataTable)base.Tables["FormProperty"];
			if (initTable)
			{
				if (this.tableFormProperty != null)
				{
					this.tableFormProperty.InitVars();
				}
			}
			this.tableFormRememberField = (TemplateDS.FormRememberFieldDataTable)base.Tables["FormRememberField"];
			if (initTable)
			{
				if (this.tableFormRememberField != null)
				{
					this.tableFormRememberField.InitVars();
				}
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitClass()
		{
			base.DataSetName = "TemplateDS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/TemplateDS.xsd";
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.tableTemplateProperty = new TemplateDS.TemplatePropertyDataTable();
			base.Tables.Add(this.tableTemplateProperty);
			this.tableFormInfo = new TemplateDS.FormInfoDataTable();
			base.Tables.Add(this.tableFormInfo);
			this.tableFormProperty = new TemplateDS.FormPropertyDataTable();
			base.Tables.Add(this.tableFormProperty);
			this.tableFormRememberField = new TemplateDS.FormRememberFieldDataTable();
			base.Tables.Add(this.tableFormRememberField);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeTemplateProperty()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeFormInfo()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeFormProperty()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeFormRememberField()
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
			TemplateDS templateDS = new TemplateDS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = templateDS.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = templateDS.GetSchemaSerializable();
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
