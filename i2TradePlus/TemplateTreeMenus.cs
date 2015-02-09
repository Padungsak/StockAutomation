using i2TradePlus.MyDataSet;
using i2TradePlus.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class TemplateTreeMenus : UserControl
	{
		public delegate void OnOpenTemplateHandler(string templateName, string templateGroup);
		private const string INVESTOR_GROUP = "Fixed";
		private const string ORDER_GROUP = "Order";
		private bool showContextMenu = true;
		private IContainer components = null;
		private TreeView tvTemplatesList;
		private ImageList ImageList;
		private Panel panelToolbarContainer;
		private ToolStrip tsTempateEditor;
		private ToolStripButton tsbtnClearDefault;
		private ToolStripButton tsbtnUseDefault;
		private ToolStripSeparator tssepLast;
		private Panel panelTemplateListContainer;
		private Panel panelDefaultTemplateInfo;
		private Label lblDefaultTemplateName;
		private Label lbDefault;
		private ToolStripButton tsbtnView;
		private ToolStripSeparator tssepNodeMoveDownUseDefault;
		private Timer tmRefresh;
		private ToolStripButton tsbtnHotkey;
		public UserWorkingProfileDS tdsUserProfile;
        public TemplateTreeMenus.OnOpenTemplateHandler _OnOpenTemplate;
		public event TemplateTreeMenus.OnOpenTemplateHandler OnOpenTemplate
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._OnOpenTemplate += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnOpenTemplate -= value;
			}
		}
		public string HeaderText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "Templates";
			}
		}
		[DefaultValue(true)]
		public bool ShowContextMenu
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.showContextMenu;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.showContextMenu != value)
				{
					this.showContextMenu = value;
				}
			}
		}
		[DefaultValue(true)]
		public bool ToolbarVisible
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.panelToolbarContainer.Visible;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.panelToolbarContainer.Visible = value;
			}
		}
		[DefaultValue(true)]
		public bool ToolbarEnabled
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.panelToolbarContainer.Enabled;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.panelToolbarContainer.Enabled = value;
			}
		}
		[DefaultValue(DockStyle.Right)]
		public DockStyle ToolbarDocking
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.panelToolbarContainer.Dock;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.panelToolbarContainer.Dock != value && value != DockStyle.None && value != DockStyle.Fill)
				{
					if (value == DockStyle.Top || value == DockStyle.Bottom)
					{
						this.panelToolbarContainer.Height = 25;
						this.tsTempateEditor.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
					}
					else
					{
						this.tsTempateEditor.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
					}
					this.panelToolbarContainer.Dock = value;
					this.panelToolbarContainer.SendToBack();
					this.panelDefaultTemplateInfo.BringToFront();
					this.panelTemplateListContainer.BringToFront();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenTemplate(string templateName, string templateGroup)
		{
			if (this._OnOpenTemplate != null)
			{
				this._OnOpenTemplate(templateName, templateGroup);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplateTreeMenus()
		{
			this.InitializeComponent();
			this.tvTemplatesList.BorderStyle = BorderStyle.None;
			this.tvTemplatesList.ExpandAll();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Initial()
		{
			try
			{
				TemplateManager.Instance.DefaultTemplateName = string.Empty;
				TemplateManager.Instance.DefaultTemplateGroup = string.Empty;
				this.tvTemplatesList.Nodes.Clear();
				this.tvTemplatesList.Nodes.Add("Fixed", "Investor Menu");
				if (ApplicationInfo.UserLoginMode != "I")
				{
					this.tvTemplatesList.Nodes.Add("Order", "Order Menu");
				}
				this.DefaultEnalbedToolbarItem();
				this.LoadUserTemplate();
				this.DisplayFixedTemplatesToControl("Fixed", false);
				this.DisplayFixedTemplatesToControl("Order", false);
				this.DisplayDefaultTemplateName();
				this.tvTemplatesList.ExpandAll();
			}
			catch (Exception ex)
			{
				this.ShowError("TemplateMenu.Initial", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaveUserProfileFromTreeView()
		{
			string templatePathByUser = ApplicationInfo.GetTemplatePathByUser();
			string fileName = templatePathByUser + "\\UserProfile.xml";
			if (this.tdsUserProfile == null)
			{
				this.tdsUserProfile = new UserWorkingProfileDS();
			}
			else
			{
				this.tdsUserProfile.Clear();
			}
			for (int i = 0; i < this.tvTemplatesList.Nodes.Count; i++)
			{
				UserWorkingProfileDS.TemplateRootsRow templateRootsRow = this.tdsUserProfile.TemplateRoots.NewTemplateRootsRow();
				templateRootsRow.TemplateName = this.tvTemplatesList.Nodes[i].Name;
				templateRootsRow.ListIndex = i;
				this.tdsUserProfile.TemplateRoots.AddTemplateRootsRow(templateRootsRow);
				for (int j = 0; j < this.tvTemplatesList.Nodes[i].Nodes.Count; j++)
				{
					UserWorkingProfileDS.TemplateItemRow templateItemRow = this.tdsUserProfile.TemplateItem.NewTemplateItemRow();
					templateItemRow.ItemName = this.tvTemplatesList.Nodes[i].Nodes[j].Name.Trim();
					templateItemRow.ListIndex = j;
					templateItemRow.IsDefalutTemplate = (this.tvTemplatesList.Nodes[i].Nodes[j].ImageKey == "ItemDefault");
					templateItemRow.RootTemplateName = templateRootsRow.TemplateName;
					if (this.tvTemplatesList.Nodes[i].Nodes[j].Tag != null)
					{
						int hotKey = 0;
						int.TryParse(this.tvTemplatesList.Nodes[i].Nodes[j].Tag.ToString(), out hotKey);
						templateItemRow.HotKey = hotKey;
					}
					else
					{
						templateItemRow.HotKey = 0;
					}
					this.tdsUserProfile.TemplateItem.AddTemplateItemRow(templateItemRow);
				}
			}
			if (!Directory.Exists(templatePathByUser))
			{
				Directory.CreateDirectory(templatePathByUser);
			}
			this.tdsUserProfile.WriteXml(fileName);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private TreeNode GetNodeByName(string groupName, string templateName)
		{
			TreeNode result = null;
			if (this.tvTemplatesList.Nodes[groupName].Nodes.ContainsKey(templateName))
			{
				for (int i = 0; i < this.tvTemplatesList.Nodes[groupName].Nodes.Count; i++)
				{
					if (this.tvTemplatesList.Nodes[groupName].Nodes[i].Name == templateName)
					{
						result = this.tvTemplatesList.Nodes[groupName].Nodes[i];
						break;
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void EditHotkeyAtTreeControl(string groupName, string templateName, int hotKey)
		{
			TreeNode nodeByName = this.GetNodeByName(groupName, templateName);
			if (nodeByName != null)
			{
				nodeByName.Tag = hotKey;
				string text = templateName;
				if (hotKey > 0)
				{
					text = "[" + this.DisplayKeyFormat((Keys)hotKey) + "] - " + text;
				}
				nodeByName.Text = text;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tvTemplatesList_MouseDown(object sender, MouseEventArgs e)
		{
			this.tvTemplatesList.SelectedNode = this.tvTemplatesList.GetNodeAt(e.X, e.Y);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tvTemplatesList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Level > 0)
			{
				if (this.tvTemplatesList.SelectedNode != this.tvTemplatesList.Nodes["Fixed"] && this.tvTemplatesList.SelectedNode != this.tvTemplatesList.Nodes["Order"])
				{
					if (e.Node.Parent == this.tvTemplatesList.Nodes["Fixed"])
					{
						this.OpenTemplate(e.Node.Name, "Fixed");
					}
					else
					{
						if (e.Node.Parent == this.tvTemplatesList.Nodes["Order"])
						{
							this.OpenTemplate(e.Node.Name, "Order");
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tvTemplatesList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.DefaultEnalbedToolbarItem();
			if (e.Node.Level > 0)
			{
				this.tsbtnView.Enabled = true;
				this.tsbtnHotkey.Enabled = (e.Node.Parent == this.tvTemplatesList.Nodes["Fixed"] || e.Node.Parent == this.tvTemplatesList.Nodes["Order"]);
				if (e.Node.ImageKey != "ItemDefault" && !(e.Node.Name == "Buy Order") && !(e.Node.Name == "Sell Order") && !(e.Node.Name == "Cover Buy Order") && !(e.Node.Name == "Short Sell Order"))
				{
					this.tsbtnUseDefault.Enabled = true;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tvTemplatesList_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.tvTemplatesList.SelectedNode != null)
			{
				if (this.tvTemplatesList.SelectedNode.Level > 0)
				{
					if (e.Control && !e.Shift && !e.Alt)
					{
						if (e.KeyCode != Keys.C)
						{
							if (e.KeyCode == Keys.Up)
							{
								this.MoveCurrentNodeUp();
							}
							else
							{
								if (e.KeyCode == Keys.Down)
								{
									this.MoveCurrentNodeDown();
								}
							}
						}
					}
					else
					{
						if (e.KeyCode != Keys.F2)
						{
							if (e.KeyCode != Keys.Delete)
							{
								if (e.KeyCode == Keys.Return)
								{
									this.ViewTemplateAtSelectedNode();
								}
							}
						}
					}
				}
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Escape)
				{
					base.Hide();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnNew_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnOpen_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSave_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSaveAll_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSaveAs_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnView_Click(object sender, EventArgs e)
		{
			this.ViewTemplateAtSelectedNode();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnRename_Click(object sender, EventArgs e)
		{
			this.RenameTemplateAtSelectedNode();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnDelete_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnRefresh_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnNodeMoveUp_Click(object sender, EventArgs e)
		{
			this.MoveCurrentNodeUp();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnNodeMoveDown_Click(object sender, EventArgs e)
		{
			this.MoveCurrentNodeDown();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnUseDefault_Click(object sender, EventArgs e)
		{
			string name = this.tvTemplatesList.SelectedNode.Name;
			if (name != null)
			{
				if (name == "Buy Order" || name == "Sell Order" || name == "Short Sell Order" || name == "Cover Buy Order")
				{
					return;
				}
			}
			this.SetDefalutTemplate(this.tvTemplatesList.SelectedNode.Parent.Name, this.tvTemplatesList.SelectedNode.Name);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnClearDefault_Click(object sender, EventArgs e)
		{
			this.ClearDefaultTemplate();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReadStandardTemplates()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReadMyFavorites()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReadMyHistory()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadUserTemplate()
		{
			try
			{
				this.tdsUserProfile = new UserWorkingProfileDS();
				string templatePathByUser = ApplicationInfo.GetTemplatePathByUser();
				string text = templatePathByUser + "\\UserProfile.xml";
				if (File.Exists(text))
				{
					this.tdsUserProfile.ReadXml(text);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoadUserTemplate", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string DisplayKeyFormat(Keys key)
		{
			string[] array = key.ToString().Split(new char[]
			{
				','
			});
			string text;
			if (array.Length > 1)
			{
				text = array[1].Trim().ToLower();
				if (text != null)
				{
					if (text == "control")
					{
						array[1] = "Ctrl";
					}
				}
			}
			string text2 = (array.Length > 1) ? string.Format("{0}+{1}", array[1], array[0]) : array[0];
			text = text2;
			if (text != null)
			{
				if (!(text == "Subtract"))
				{
					if (!(text == "Divide"))
					{
						if (text == "Multiply")
						{
							text2 = "*";
						}
					}
					else
					{
						text2 = "/";
					}
				}
				else
				{
					text2 = "-";
				}
			}
			return text2.Trim();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DisplayFixedTemplatesToControl(string groupName, bool isAddHotkey)
		{
			try
			{
				this.tvTemplatesList.Nodes[groupName].Nodes.Clear();
				List<MainFixedTemplate.TemplateProperty> list = null;
				if (groupName == "Fixed")
				{
					list = MainFixedTemplate.GetFixedTemplateName();
				}
				else
				{
					if (groupName == "Order")
					{
						list = MainFixedTemplate.GetOrderTemplateName();
					}
				}
				UserWorkingProfileDS.TemplateItemRow[] array = (UserWorkingProfileDS.TemplateItemRow[])this.tdsUserProfile.TemplateItem.Select("RootTemplateName='" + groupName + "'", this.tdsUserProfile.TemplateItem.ListIndexColumn.ColumnName);
				string b = string.Empty;
				if (array.Length > 0)
				{
					UserWorkingProfileDS.TemplateItemRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						UserWorkingProfileDS.TemplateItemRow templateItemRow = array2[i];
						if (templateItemRow != null)
						{
							foreach (MainFixedTemplate.TemplateProperty current in list)
							{
								if (current.TempateName == templateItemRow.ItemName)
								{
									current.Hotkey = templateItemRow.HotKey;
									break;
								}
							}
							if (templateItemRow.IsDefalutTemplate)
							{
								b = templateItemRow.ItemName;
							}
						}
					}
				}
				foreach (MainFixedTemplate.TemplateProperty current2 in list)
				{
					bool isDefault = false;
					if (current2.TempateName == b)
					{
						isDefault = true;
					}
					this.SetDisplayNameOfTreeView(current2.TempateName, groupName, current2.Hotkey, isDefault, isAddHotkey);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("DisplayFixedTemplatesToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private TreeNode SetDisplayNameOfTreeView(string templateName, string templateGroup, int hotKey, bool isDefault, bool isAddHotKey)
		{
			TreeNode treeNode = this.tvTemplatesList.Nodes[templateGroup].Nodes[templateName];
			if (treeNode == null)
			{
				treeNode = this.tvTemplatesList.Nodes[templateGroup].Nodes.Add(templateName, templateName);
			}
			if (isDefault)
			{
				treeNode.ImageKey = "ItemDefault";
				treeNode.SelectedImageKey = "ItemDefault";
				TemplateManager.Instance.DefaultTemplateName = templateName;
				TemplateManager.Instance.DefaultTemplateGroup = templateGroup;
			}
			else
			{
				treeNode.ImageKey = "Item";
				treeNode.SelectedImageKey = "Item";
			}
			if (hotKey >= 0)
			{
				string text = templateName;
				if (hotKey > 0)
				{
					text = "[" + this.DisplayKeyFormat((Keys)hotKey) + "] - " + text;
				}
				treeNode.Text = text;
				treeNode.Tag = hotKey;
				try
				{
					if (isAddHotKey)
					{
						if (!HotKeyManager.IsHotKeyDefined((Keys)hotKey))
						{
							HotKeyManager.AddTemplateHotkey(templateName, templateGroup, (Keys)hotKey);
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetDisplayNameOfTreeView", ex);
				}
			}
			return treeNode;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DefaultEnalbedToolbarItem()
		{
			this.tsbtnView.Enabled = false;
			this.tsbtnUseDefault.Enabled = false;
			this.tsbtnClearDefault.Enabled = true;
			this.tsbtnHotkey.Enabled = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewTemplateAtSelectedNode()
		{
			if (this.tvTemplatesList.SelectedNode != null && this.tvTemplatesList.SelectedNode.Level > 0)
			{
				this.OpenTemplate(this.tvTemplatesList.SelectedNode.Name, this.tvTemplatesList.SelectedNode.Parent.Name);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RenameTemplateAtSelectedNode()
		{
			if (this.tvTemplatesList.SelectedNode != null && this.tvTemplatesList.SelectedNode.Level > 0)
			{
				this.tvTemplatesList.LabelEdit = true;
				this.tvTemplatesList.SelectedNode.BeginEdit();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DeleteTempateAtSelectedNode()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetDefalutTemplate(string groupName, string nodeName)
		{
			TreeNode nodeByName = this.GetNodeByName(groupName, nodeName);
			if (nodeByName != null)
			{
				this.ClearDefaultTemplateTreeNode();
				nodeByName.ImageKey = "ItemDefault";
				nodeByName.SelectedImageKey = "ItemDefault";
				TemplateManager.Instance.DefaultTemplateName = nodeByName.Text;
				TemplateManager.Instance.DefaultTemplateGroup = nodeByName.Parent.Name;
				this.DisplayDefaultTemplateName();
				this.SaveUserProfileFromTreeView();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ClearDefaultTemplate()
		{
			this.ClearDefaultTemplateTreeNode();
			TemplateManager.Instance.DefaultTemplateName = string.Empty;
			TemplateManager.Instance.DefaultTemplateGroup = string.Empty;
			this.DisplayDefaultTemplateName();
			this.SaveUserProfileFromTreeView();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ClearDefaultTemplateTreeNode()
		{
			foreach (TreeNode treeNode in this.tvTemplatesList.Nodes)
			{
				foreach (TreeNode treeNode2 in treeNode.Nodes)
				{
					if (treeNode2.ImageKey == "ItemDefault")
					{
						treeNode2.ImageKey = "Item";
						treeNode2.SelectedImageKey = "Item";
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetToolStripItemDisplayStyle(ToolStripItemDisplayStyle style)
		{
			foreach (ToolStripItem toolStripItem in this.tsTempateEditor.Items)
			{
				if (toolStripItem.GetType() != typeof(ToolStripSeparator))
				{
					toolStripItem.DisplayStyle = style;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DisplayDefaultTemplateName()
		{
			if (!string.IsNullOrEmpty(TemplateManager.Instance.DefaultTemplateName))
			{
				this.lblDefaultTemplateName.Text = TemplateManager.Instance.DefaultTemplateName;
			}
			else
			{
				this.lblDefaultTemplateName.Text = "Blank";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MoveCurrentNodeUp()
		{
			if (this.tvTemplatesList.SelectedNode != null && this.tvTemplatesList.SelectedNode.Level > 0)
			{
				TreeNode selectedNode = this.tvTemplatesList.SelectedNode;
				TreeNode parent = this.tvTemplatesList.SelectedNode.Parent;
				int index = this.tvTemplatesList.SelectedNode.Index - 1;
				parent.Nodes.Remove(this.tvTemplatesList.SelectedNode);
				parent.Nodes.Insert(index, selectedNode);
				this.tvTemplatesList.SelectedNode = selectedNode;
				this.SaveUserProfileFromTreeView();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MoveCurrentNodeDown()
		{
			if (this.tvTemplatesList.SelectedNode != null && this.tvTemplatesList.SelectedNode.Level > 0)
			{
				TreeNode selectedNode = this.tvTemplatesList.SelectedNode;
				TreeNode parent = this.tvTemplatesList.SelectedNode.Parent;
				int index = this.tvTemplatesList.SelectedNode.Index + 1;
				parent.Nodes.Remove(this.tvTemplatesList.SelectedNode);
				parent.Nodes.Insert(index, selectedNode);
				this.tvTemplatesList.SelectedNode = selectedNode;
				this.SaveUserProfileFromTreeView();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmRefresh_Tick(object sender, EventArgs e)
		{
			try
			{
				this.tmRefresh.Enabled = false;
				if (this.tvTemplatesList.SelectedNode.Parent == null)
				{
					this.tvTemplatesList.Refresh();
				}
			}
			finally
			{
				this.tvTemplatesList.Refresh();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiHotkey_Click(object sender, EventArgs e)
		{
			frmTemplateInfo frmTemplateInfo = new frmTemplateInfo(frmTemplateInfo.ShowMode.Hotkeys, this.tvTemplatesList.SelectedNode.Name, TemplateManager.Instance.IsDefaultTemplate(this.tvTemplatesList.SelectedNode.Name));
			if (frmTemplateInfo.ShowDialog(this) == DialogResult.OK)
			{
				string templateGroupName = this.GetTemplateGroupName(frmTemplateInfo.TemplateName);
				this.EditHotkeyAtTreeControl(templateGroupName, frmTemplateInfo.TemplateName, frmTemplateInfo.TempalteHotkey);
				HotKeyManager.AddTemplateHotkey(frmTemplateInfo.TemplateName, templateGroupName, (Keys)frmTemplateInfo.TempalteHotkey);
				this.SaveUserProfileFromTreeView();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetTemplateGroupName(string tempateName)
		{
			string result = string.Empty;
			TreeNode[] array = this.tvTemplatesList.Nodes.Find(tempateName, true);
			if (array.Length > 0)
			{
				result = array[0].Parent.Name;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			this.components = new Container();
			TreeNode treeNode = new TreeNode("Fixed Templates");
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TemplateTreeMenus));
			this.tvTemplatesList = new TreeView();
			this.ImageList = new ImageList(this.components);
			this.panelToolbarContainer = new Panel();
			this.tsTempateEditor = new ToolStrip();
			this.tsbtnView = new ToolStripButton();
			this.tssepNodeMoveDownUseDefault = new ToolStripSeparator();
			this.tsbtnHotkey = new ToolStripButton();
			this.tsbtnUseDefault = new ToolStripButton();
			this.tsbtnClearDefault = new ToolStripButton();
			this.tssepLast = new ToolStripSeparator();
			this.panelTemplateListContainer = new Panel();
			this.panelDefaultTemplateInfo = new Panel();
			this.lbDefault = new Label();
			this.lblDefaultTemplateName = new Label();
			this.tmRefresh = new Timer(this.components);
			this.tdsUserProfile = new UserWorkingProfileDS();
			this.panelToolbarContainer.SuspendLayout();
			this.tsTempateEditor.SuspendLayout();
			this.panelTemplateListContainer.SuspendLayout();
			this.panelDefaultTemplateInfo.SuspendLayout();
			((ISupportInitialize)this.tdsUserProfile).BeginInit();
			base.SuspendLayout();
			this.tvTemplatesList.Dock = DockStyle.Fill;
			this.tvTemplatesList.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.tvTemplatesList.ImageIndex = 4;
			this.tvTemplatesList.ImageList = this.ImageList;
			this.tvTemplatesList.Location = new Point(0, 0);
			this.tvTemplatesList.Margin = new Padding(4);
			this.tvTemplatesList.Name = "tvTemplatesList";
			treeNode.Name = "treeNodeFixedTemplates";
			treeNode.Text = "Fixed Templates";
			this.tvTemplatesList.Nodes.AddRange(new TreeNode[]
			{
				treeNode
			});
			this.tvTemplatesList.SelectedImageIndex = 4;
			this.tvTemplatesList.ShowRootLines = false;
			this.tvTemplatesList.Size = new Size(209, 312);
			this.tvTemplatesList.TabIndex = 0;
			this.tvTemplatesList.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.tvTemplatesList_NodeMouseDoubleClick);
			this.tvTemplatesList.AfterSelect += new TreeViewEventHandler(this.tvTemplatesList_AfterSelect);
			this.tvTemplatesList.MouseDown += new MouseEventHandler(this.tvTemplatesList_MouseDown);
			this.tvTemplatesList.KeyDown += new KeyEventHandler(this.tvTemplatesList_KeyDown);
			this.ImageList.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("ImageList.ImageStream");
			this.ImageList.TransparentColor = Color.Transparent;
			this.ImageList.Images.SetKeyName(0, "Drafts.bmp");
			this.ImageList.Images.SetKeyName(1, "Outbox.bmp");
			this.ImageList.Images.SetKeyName(2, "Recycle.bmp");
			this.ImageList.Images.SetKeyName(3, "Send.bmp");
			this.ImageList.Images.SetKeyName(4, "Collapsed");
			this.ImageList.Images.SetKeyName(5, "Expanded");
			this.ImageList.Images.SetKeyName(6, "Item");
			this.ImageList.Images.SetKeyName(7, "ItemDefault");
			this.ImageList.Images.SetKeyName(8, "ItemWarning");
			this.panelToolbarContainer.BackColor = SystemColors.Info;
			this.panelToolbarContainer.BorderStyle = BorderStyle.FixedSingle;
			this.panelToolbarContainer.Controls.Add(this.tsTempateEditor);
			this.panelToolbarContainer.Dock = DockStyle.Left;
			this.panelToolbarContainer.Location = new Point(0, 0);
			this.panelToolbarContainer.Margin = new Padding(4);
			this.panelToolbarContainer.Name = "panelToolbarContainer";
			this.panelToolbarContainer.Size = new Size(29, 342);
			this.panelToolbarContainer.TabIndex = 9;
			this.tsTempateEditor.BackgroundImageLayout = ImageLayout.None;
			this.tsTempateEditor.Dock = DockStyle.Fill;
			this.tsTempateEditor.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnView,
				this.tssepNodeMoveDownUseDefault,
				this.tsbtnHotkey,
				this.tsbtnUseDefault,
				this.tsbtnClearDefault,
				this.tssepLast
			});
			this.tsTempateEditor.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.tsTempateEditor.Location = new Point(0, 0);
			this.tsTempateEditor.Name = "tsTempateEditor";
			this.tsTempateEditor.RenderMode = ToolStripRenderMode.System;
			this.tsTempateEditor.Size = new Size(27, 340);
			this.tsTempateEditor.TabIndex = 7;
			this.tsTempateEditor.Text = "toolStrip1";
			this.tsbtnView.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnView.Image = (Image)componentResourceManager.GetObject("tsbtnView.Image");
			this.tsbtnView.ImageAlign = ContentAlignment.MiddleLeft;
			this.tsbtnView.ImageTransparentColor = Color.Magenta;
			this.tsbtnView.Name = "tsbtnView";
			this.tsbtnView.Size = new Size(25, 20);
			this.tsbtnView.Text = "View";
			this.tsbtnView.TextAlign = ContentAlignment.MiddleLeft;
			this.tsbtnView.ToolTipText = "View(Enter)";
			this.tsbtnView.Click += new EventHandler(this.tsbtnView_Click);
			this.tssepNodeMoveDownUseDefault.Name = "tssepNodeMoveDownUseDefault";
			this.tssepNodeMoveDownUseDefault.Size = new Size(25, 6);
			this.tsbtnHotkey.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnHotkey.Image = (Image)componentResourceManager.GetObject("tsbtnHotkey.Image");
			this.tsbtnHotkey.ImageAlign = ContentAlignment.MiddleLeft;
			this.tsbtnHotkey.ImageTransparentColor = Color.Magenta;
			this.tsbtnHotkey.Name = "tsbtnHotkey";
			this.tsbtnHotkey.Size = new Size(25, 20);
			this.tsbtnHotkey.Text = "Hotkey";
			this.tsbtnHotkey.TextAlign = ContentAlignment.MiddleLeft;
			this.tsbtnHotkey.Click += new EventHandler(this.tsmiHotkey_Click);
			this.tsbtnUseDefault.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnUseDefault.Image = (Image)componentResourceManager.GetObject("tsbtnUseDefault.Image");
			this.tsbtnUseDefault.ImageAlign = ContentAlignment.MiddleLeft;
			this.tsbtnUseDefault.ImageTransparentColor = Color.Magenta;
			this.tsbtnUseDefault.Name = "tsbtnUseDefault";
			this.tsbtnUseDefault.Size = new Size(25, 20);
			this.tsbtnUseDefault.Text = "Set Default";
			this.tsbtnUseDefault.TextAlign = ContentAlignment.MiddleLeft;
			this.tsbtnUseDefault.Click += new EventHandler(this.tsbtnUseDefault_Click);
			this.tsbtnClearDefault.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnClearDefault.Image = (Image)componentResourceManager.GetObject("tsbtnClearDefault.Image");
			this.tsbtnClearDefault.ImageAlign = ContentAlignment.MiddleLeft;
			this.tsbtnClearDefault.ImageTransparentColor = Color.Magenta;
			this.tsbtnClearDefault.Name = "tsbtnClearDefault";
			this.tsbtnClearDefault.Size = new Size(25, 20);
			this.tsbtnClearDefault.Text = "Clear Default";
			this.tsbtnClearDefault.TextAlign = ContentAlignment.MiddleLeft;
			this.tsbtnClearDefault.Click += new EventHandler(this.tsbtnClearDefault_Click);
			this.tssepLast.Name = "tssepLast";
			this.tssepLast.Size = new Size(25, 6);
			this.panelTemplateListContainer.BorderStyle = BorderStyle.FixedSingle;
			this.panelTemplateListContainer.Controls.Add(this.tvTemplatesList);
			this.panelTemplateListContainer.Dock = DockStyle.Fill;
			this.panelTemplateListContainer.Location = new Point(29, 0);
			this.panelTemplateListContainer.Margin = new Padding(4);
			this.panelTemplateListContainer.Name = "panelTemplateListContainer";
			this.panelTemplateListContainer.Size = new Size(211, 314);
			this.panelTemplateListContainer.TabIndex = 10;
			this.panelDefaultTemplateInfo.BorderStyle = BorderStyle.FixedSingle;
			this.panelDefaultTemplateInfo.Controls.Add(this.lbDefault);
			this.panelDefaultTemplateInfo.Controls.Add(this.lblDefaultTemplateName);
			this.panelDefaultTemplateInfo.Dock = DockStyle.Bottom;
			this.panelDefaultTemplateInfo.Location = new Point(29, 314);
			this.panelDefaultTemplateInfo.Margin = new Padding(4);
			this.panelDefaultTemplateInfo.Name = "panelDefaultTemplateInfo";
			this.panelDefaultTemplateInfo.Size = new Size(211, 28);
			this.panelDefaultTemplateInfo.TabIndex = 11;
			this.lbDefault.AutoSize = true;
			this.lbDefault.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.lbDefault.Location = new Point(4, 2);
			this.lbDefault.Margin = new Padding(4, 0, 4, 0);
			this.lbDefault.Name = "lbDefault";
			this.lbDefault.Size = new Size(52, 13);
			this.lbDefault.TabIndex = 1;
			this.lbDefault.Text = "Default:";
			this.lblDefaultTemplateName.AutoSize = true;
			this.lblDefaultTemplateName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lblDefaultTemplateName.Location = new Point(81, 2);
			this.lblDefaultTemplateName.Margin = new Padding(4, 0, 4, 0);
			this.lblDefaultTemplateName.Name = "lblDefaultTemplateName";
			this.lblDefaultTemplateName.Size = new Size(34, 13);
			this.lblDefaultTemplateName.TabIndex = 0;
			this.lblDefaultTemplateName.Text = "Blank";
			this.tmRefresh.Tick += new EventHandler(this.tmRefresh_Tick);
			this.tdsUserProfile.DataSetName = "UserProfileDS";
			this.tdsUserProfile.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panelTemplateListContainer);
			base.Controls.Add(this.panelDefaultTemplateInfo);
			base.Controls.Add(this.panelToolbarContainer);
			this.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.Margin = new Padding(4);
			base.Name = "TemplateTreeMenus";
			base.Size = new Size(240, 342);
			this.panelToolbarContainer.ResumeLayout(false);
			this.panelToolbarContainer.PerformLayout();
			this.tsTempateEditor.ResumeLayout(false);
			this.tsTempateEditor.PerformLayout();
			this.panelTemplateListContainer.ResumeLayout(false);
			this.panelDefaultTemplateInfo.ResumeLayout(false);
			this.panelDefaultTemplateInfo.PerformLayout();
			((ISupportInitialize)this.tdsUserProfile).EndInit();
			base.ResumeLayout(false);
		}
	}
}
