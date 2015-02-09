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
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), HelpKeyword("vs.data.DataSet"), DesignerCategory("code"), ToolboxItem(true), XmlRoot("UserWorkingProfileDS"), XmlSchemaProvider("GetTypedDataSetSchema")]
	[Serializable]
	public class UserWorkingProfileDS : DataSet
	{
		public delegate void TemplateRootsRowChangeEventHandler(object sender, UserWorkingProfileDS.TemplateRootsRowChangeEvent e);
		public delegate void TemplateItemRowChangeEventHandler(object sender, UserWorkingProfileDS.TemplateItemRowChangeEvent e);
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class TemplateRootsDataTable : DataTable, IEnumerable
		{
			private DataColumn columnTemplateName;
			private DataColumn columnListIndex;
            public UserWorkingProfileDS.TemplateRootsRowChangeEventHandler _TemplateRootsRowChanging;
			public event UserWorkingProfileDS.TemplateRootsRowChangeEventHandler TemplateRootsRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplateRootsRowChanging += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._TemplateRootsRowChanging -= value;
                }
			}
			public UserWorkingProfileDS.TemplateRootsRowChangeEventHandler _TemplateRootsRowChanged;
			public event UserWorkingProfileDS.TemplateRootsRowChangeEventHandler TemplateRootsRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._TemplateRootsRowChanged += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._TemplateRootsRowChanged -= value;
                }
			}
			public UserWorkingProfileDS.TemplateRootsRowChangeEventHandler _TemplateRootsRowDeleting;
			public event UserWorkingProfileDS.TemplateRootsRowChangeEventHandler TemplateRootsRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._TemplateRootsRowDeleting += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._TemplateRootsRowDeleting -= value;
                }
			}
			public UserWorkingProfileDS.TemplateRootsRowChangeEventHandler _TemplateRootsRowDeleted;
			public event UserWorkingProfileDS.TemplateRootsRowChangeEventHandler TemplateRootsRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._TemplateRootsRowDeleted += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._TemplateRootsRowDeleted -= value;
                }
			}
			[DebuggerNonUserCode]
			public DataColumn TemplateNameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnTemplateName;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn ListIndexColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnListIndex;
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
			public UserWorkingProfileDS.TemplateRootsRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (UserWorkingProfileDS.TemplateRootsRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateRootsDataTable()
			{
				base.TableName = "TemplateRoots";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal TemplateRootsDataTable(DataTable table)
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
			protected TemplateRootsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddTemplateRootsRow(UserWorkingProfileDS.TemplateRootsRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public UserWorkingProfileDS.TemplateRootsRow AddTemplateRootsRow(string TemplateName, int ListIndex)
			{
				UserWorkingProfileDS.TemplateRootsRow templateRootsRow = (UserWorkingProfileDS.TemplateRootsRow)base.NewRow();
				object[] itemArray = new object[]
				{
					TemplateName,
					ListIndex
				};
				templateRootsRow.ItemArray = itemArray;
				base.Rows.Add(templateRootsRow);
				return templateRootsRow;
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
				UserWorkingProfileDS.TemplateRootsDataTable templateRootsDataTable = (UserWorkingProfileDS.TemplateRootsDataTable)base.Clone();
				templateRootsDataTable.InitVars();
				return templateRootsDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new UserWorkingProfileDS.TemplateRootsDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnTemplateName = base.Columns["TemplateName"];
				this.columnListIndex = base.Columns["ListIndex"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnTemplateName = new DataColumn("TemplateName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnTemplateName);
				this.columnListIndex = new DataColumn("ListIndex", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnListIndex);
				this.columnTemplateName.DefaultValue = "";
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public UserWorkingProfileDS.TemplateRootsRow NewTemplateRootsRow()
			{
				return (UserWorkingProfileDS.TemplateRootsRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new UserWorkingProfileDS.TemplateRootsRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(UserWorkingProfileDS.TemplateRootsRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._TemplateRootsRowChanged != null)
				{
					this._TemplateRootsRowChanged(this, new UserWorkingProfileDS.TemplateRootsRowChangeEvent((UserWorkingProfileDS.TemplateRootsRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._TemplateRootsRowChanging != null)
				{
					this._TemplateRootsRowChanging(this, new UserWorkingProfileDS.TemplateRootsRowChangeEvent((UserWorkingProfileDS.TemplateRootsRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._TemplateRootsRowDeleted != null)
				{
					this._TemplateRootsRowDeleted(this, new UserWorkingProfileDS.TemplateRootsRowChangeEvent((UserWorkingProfileDS.TemplateRootsRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._TemplateRootsRowDeleting != null)
				{
					this._TemplateRootsRowDeleting(this, new UserWorkingProfileDS.TemplateRootsRowChangeEvent((UserWorkingProfileDS.TemplateRootsRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveTemplateRootsRow(UserWorkingProfileDS.TemplateRootsRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				UserWorkingProfileDS userWorkingProfileDS = new UserWorkingProfileDS();
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
				xmlSchemaAttribute.FixedValue = userWorkingProfileDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "TemplateRootsDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = userWorkingProfileDS.GetSchemaSerializable();
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
		public class TemplateItemDataTable : DataTable, IEnumerable
		{
			private DataColumn columnItemName;
			private DataColumn columnListIndex;
			private DataColumn columnIsDefalutTemplate;
			private DataColumn columnRootTemplateName;
			private DataColumn columnHotKey;
			public  UserWorkingProfileDS.TemplateItemRowChangeEventHandler _TemplateItemRowChanging;
			public event UserWorkingProfileDS.TemplateItemRowChangeEventHandler TemplateItemRowChanging
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplateItemRowChanging += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplateItemRowChanging -= value;
				}
			}
			public  UserWorkingProfileDS.TemplateItemRowChangeEventHandler _TemplateItemRowChanged;
			public event UserWorkingProfileDS.TemplateItemRowChangeEventHandler TemplateItemRowChanged
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplateItemRowChanged += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplateItemRowChanged -= value;
				}
			}
			public  UserWorkingProfileDS.TemplateItemRowChangeEventHandler _TemplateItemRowDeleting;
			public event UserWorkingProfileDS.TemplateItemRowChangeEventHandler TemplateItemRowDeleting
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
					this._TemplateItemRowDeleting += value;
				}
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
					this._TemplateItemRowDeleting -= value;
				}
			}
            public UserWorkingProfileDS.TemplateItemRowChangeEventHandler _TemplateItemRowDeleted;
			public event UserWorkingProfileDS.TemplateItemRowChangeEventHandler TemplateItemRowDeleted
			{
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				add
				{
                    this._TemplateItemRowDeleted += value;
                }
				[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
				remove
				{
                    this._TemplateItemRowDeleted -= value;
                }
			}
			[DebuggerNonUserCode]
			public DataColumn ItemNameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnItemName;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn ListIndexColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnListIndex;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn IsDefalutTemplateColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnIsDefalutTemplate;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn RootTemplateNameColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnRootTemplateName;
				}
			}
			[DebuggerNonUserCode]
			public DataColumn HotKeyColumn
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.columnHotKey;
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
			public UserWorkingProfileDS.TemplateItemRow this[int index]
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return (UserWorkingProfileDS.TemplateItemRow)base.Rows[index];
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateItemDataTable()
			{
				base.TableName = "TemplateItem";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal TemplateItemDataTable(DataTable table)
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
			protected TemplateItemDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void AddTemplateItemRow(UserWorkingProfileDS.TemplateItemRow row)
			{
				base.Rows.Add(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public UserWorkingProfileDS.TemplateItemRow AddTemplateItemRow(string ItemName, int ListIndex, bool IsDefalutTemplate, string RootTemplateName, int HotKey)
			{
				UserWorkingProfileDS.TemplateItemRow templateItemRow = (UserWorkingProfileDS.TemplateItemRow)base.NewRow();
				object[] itemArray = new object[]
				{
					ItemName,
					ListIndex,
					IsDefalutTemplate,
					RootTemplateName,
					HotKey
				};
				templateItemRow.ItemArray = itemArray;
				base.Rows.Add(templateItemRow);
				return templateItemRow;
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
				UserWorkingProfileDS.TemplateItemDataTable templateItemDataTable = (UserWorkingProfileDS.TemplateItemDataTable)base.Clone();
				templateItemDataTable.InitVars();
				return templateItemDataTable;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataTable CreateInstance()
			{
				return new UserWorkingProfileDS.TemplateItemDataTable();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal void InitVars()
			{
				this.columnItemName = base.Columns["ItemName"];
				this.columnListIndex = base.Columns["ListIndex"];
				this.columnIsDefalutTemplate = base.Columns["IsDefalutTemplate"];
				this.columnRootTemplateName = base.Columns["RootTemplateName"];
				this.columnHotKey = base.Columns["HotKey"];
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void InitClass()
			{
				this.columnItemName = new DataColumn("ItemName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnItemName);
				this.columnListIndex = new DataColumn("ListIndex", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnListIndex);
				this.columnIsDefalutTemplate = new DataColumn("IsDefalutTemplate", typeof(bool), null, MappingType.Element);
				base.Columns.Add(this.columnIsDefalutTemplate);
				this.columnRootTemplateName = new DataColumn("RootTemplateName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnRootTemplateName);
				this.columnHotKey = new DataColumn("HotKey", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnHotKey);
				this.columnItemName.DefaultValue = "";
				this.columnIsDefalutTemplate.DefaultValue = false;
				this.columnRootTemplateName.DefaultValue = "";
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public UserWorkingProfileDS.TemplateItemRow NewTemplateItemRow()
			{
				return (UserWorkingProfileDS.TemplateItemRow)base.NewRow();
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new UserWorkingProfileDS.TemplateItemRow(builder);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override Type GetRowType()
			{
				return typeof(UserWorkingProfileDS.TemplateItemRow);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._TemplateItemRowChanged != null)
				{
					this._TemplateItemRowChanged(this, new UserWorkingProfileDS.TemplateItemRowChangeEvent((UserWorkingProfileDS.TemplateItemRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._TemplateItemRowChanging != null)
				{
					this._TemplateItemRowChanging(this, new UserWorkingProfileDS.TemplateItemRowChangeEvent((UserWorkingProfileDS.TemplateItemRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._TemplateItemRowDeleted != null)
				{
					this._TemplateItemRowDeleted(this, new UserWorkingProfileDS.TemplateItemRowChangeEvent((UserWorkingProfileDS.TemplateItemRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._TemplateItemRowDeleting != null)
				{
					this._TemplateItemRowDeleting(this, new UserWorkingProfileDS.TemplateItemRowChangeEvent((UserWorkingProfileDS.TemplateItemRow)e.Row, e.Action));
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void RemoveTemplateItemRow(UserWorkingProfileDS.TemplateItemRow row)
			{
				base.Rows.Remove(row);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				UserWorkingProfileDS userWorkingProfileDS = new UserWorkingProfileDS();
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
				xmlSchemaAttribute.FixedValue = userWorkingProfileDS.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "TemplateItemDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = userWorkingProfileDS.GetSchemaSerializable();
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
		public class TemplateRootsRow : DataRow
		{
			private UserWorkingProfileDS.TemplateRootsDataTable tableTemplateRoots;
			[DebuggerNonUserCode]
			public string TemplateName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsTemplateNameNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableTemplateRoots.TemplateNameColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateRoots.TemplateNameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public int ListIndex
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableTemplateRoots.ListIndexColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ListIndex' in table 'TemplateRoots' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateRoots.ListIndexColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal TemplateRootsRow(DataRowBuilder rb) : base(rb)
			{
				this.tableTemplateRoots = (UserWorkingProfileDS.TemplateRootsDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsTemplateNameNull()
			{
				return base.IsNull(this.tableTemplateRoots.TemplateNameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetTemplateNameNull()
			{
				base[this.tableTemplateRoots.TemplateNameColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsListIndexNull()
			{
				return base.IsNull(this.tableTemplateRoots.ListIndexColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetListIndexNull()
			{
				base[this.tableTemplateRoots.ListIndexColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class TemplateItemRow : DataRow
		{
			private UserWorkingProfileDS.TemplateItemDataTable tableTemplateItem;
			[DebuggerNonUserCode]
			public string ItemName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsItemNameNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableTemplateItem.ItemNameColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateItem.ItemNameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public int ListIndex
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableTemplateItem.ListIndexColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ListIndex' in table 'TemplateItem' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateItem.ListIndexColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public bool IsDefalutTemplate
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					bool result;
					try
					{
						result = (bool)base[this.tableTemplateItem.IsDefalutTemplateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'IsDefalutTemplate' in table 'TemplateItem' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateItem.IsDefalutTemplateColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public string RootTemplateName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					string result;
					if (this.IsRootTemplateNameNull())
					{
						result = string.Empty;
					}
					else
					{
						result = (string)base[this.tableTemplateItem.RootTemplateNameColumn];
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateItem.RootTemplateNameColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			public int HotKey
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableTemplateItem.HotKeyColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'HotKey' in table 'TemplateItem' is DBNull.", innerException);
					}
					return result;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					base[this.tableTemplateItem.HotKeyColumn] = value;
				}
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal TemplateItemRow(DataRowBuilder rb) : base(rb)
			{
				this.tableTemplateItem = (UserWorkingProfileDS.TemplateItemDataTable)base.Table;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsItemNameNull()
			{
				return base.IsNull(this.tableTemplateItem.ItemNameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetItemNameNull()
			{
				base[this.tableTemplateItem.ItemNameColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsListIndexNull()
			{
				return base.IsNull(this.tableTemplateItem.ListIndexColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetListIndexNull()
			{
				base[this.tableTemplateItem.ListIndexColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsIsDefalutTemplateNull()
			{
				return base.IsNull(this.tableTemplateItem.IsDefalutTemplateColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetIsDefalutTemplateNull()
			{
				base[this.tableTemplateItem.IsDefalutTemplateColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsRootTemplateNameNull()
			{
				return base.IsNull(this.tableTemplateItem.RootTemplateNameColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetRootTemplateNameNull()
			{
				base[this.tableTemplateItem.RootTemplateNameColumn] = Convert.DBNull;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public bool IsHotKeyNull()
			{
				return base.IsNull(this.tableTemplateItem.HotKeyColumn);
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void SetHotKeyNull()
			{
				base[this.tableTemplateItem.HotKeyColumn] = Convert.DBNull;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class TemplateRootsRowChangeEvent : EventArgs
		{
			private UserWorkingProfileDS.TemplateRootsRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public UserWorkingProfileDS.TemplateRootsRow Row
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
			public TemplateRootsRowChangeEvent(UserWorkingProfileDS.TemplateRootsRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
		public class TemplateItemRowChangeEvent : EventArgs
		{
			private UserWorkingProfileDS.TemplateItemRow eventRow;
			private DataRowAction eventAction;
			[DebuggerNonUserCode]
			public UserWorkingProfileDS.TemplateItemRow Row
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
			public TemplateItemRowChangeEvent(UserWorkingProfileDS.TemplateItemRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}
		}
		private UserWorkingProfileDS.TemplateRootsDataTable tableTemplateRoots;
		private UserWorkingProfileDS.TemplateItemDataTable tableTemplateItem;
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public UserWorkingProfileDS.TemplateRootsDataTable TemplateRoots
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableTemplateRoots;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
		public UserWorkingProfileDS.TemplateItemDataTable TemplateItem
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tableTemplateItem;
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
		public UserWorkingProfileDS()
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
		protected UserWorkingProfileDS(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
					if (dataSet.Tables["TemplateRoots"] != null)
					{
						base.Tables.Add(new UserWorkingProfileDS.TemplateRootsDataTable(dataSet.Tables["TemplateRoots"]));
					}
					if (dataSet.Tables["TemplateItem"] != null)
					{
						base.Tables.Add(new UserWorkingProfileDS.TemplateItemDataTable(dataSet.Tables["TemplateItem"]));
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
			UserWorkingProfileDS userWorkingProfileDS = (UserWorkingProfileDS)base.Clone();
			userWorkingProfileDS.InitVars();
			userWorkingProfileDS.SchemaSerializationMode = this.SchemaSerializationMode;
			return userWorkingProfileDS;
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
				if (dataSet.Tables["TemplateRoots"] != null)
				{
					base.Tables.Add(new UserWorkingProfileDS.TemplateRootsDataTable(dataSet.Tables["TemplateRoots"]));
				}
				if (dataSet.Tables["TemplateItem"] != null)
				{
					base.Tables.Add(new UserWorkingProfileDS.TemplateItemDataTable(dataSet.Tables["TemplateItem"]));
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
			this.tableTemplateRoots = (UserWorkingProfileDS.TemplateRootsDataTable)base.Tables["TemplateRoots"];
			if (initTable)
			{
				if (this.tableTemplateRoots != null)
				{
					this.tableTemplateRoots.InitVars();
				}
			}
			this.tableTemplateItem = (UserWorkingProfileDS.TemplateItemDataTable)base.Tables["TemplateItem"];
			if (initTable)
			{
				if (this.tableTemplateItem != null)
				{
					this.tableTemplateItem.InitVars();
				}
			}
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitClass()
		{
			base.DataSetName = "UserWorkingProfileDS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/UserWorkingProfileDS.xsd";
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.tableTemplateRoots = new UserWorkingProfileDS.TemplateRootsDataTable();
			base.Tables.Add(this.tableTemplateRoots);
			this.tableTemplateItem = new UserWorkingProfileDS.TemplateItemDataTable();
			base.Tables.Add(this.tableTemplateItem);
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeTemplateRoots()
		{
			return false;
		}
		[DebuggerNonUserCode]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ShouldSerializeTemplateItem()
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
			UserWorkingProfileDS userWorkingProfileDS = new UserWorkingProfileDS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = userWorkingProfileDS.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = userWorkingProfileDS.GetSchemaSerializable();
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
