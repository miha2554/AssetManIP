
namespace AssetManip
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnNew = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.assetView = new System.Windows.Forms.ListView();
			this.btnSwitchType = new System.Windows.Forms.Button();
			this.btnAddFinal = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.assetEdit = new System.Windows.Forms.DataGridView();
			this.assetSubtypeSwitch = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.assetEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(12, 12);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(115, 27);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "Новый";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(133, 12);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(115, 27);
			this.btnRemove.TabIndex = 1;
			this.btnRemove.Text = "Удалить";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Location = new System.Drawing.Point(254, 12);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(115, 27);
			this.btnEdit.TabIndex = 2;
			this.btnEdit.Text = "Редактировать";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// assetView
			// 
			this.assetView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.assetView.HideSelection = false;
			this.assetView.Location = new System.Drawing.Point(12, 45);
			this.assetView.Name = "assetView";
			this.assetView.Size = new System.Drawing.Size(760, 325);
			this.assetView.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.assetView.TabIndex = 3;
			this.assetView.UseCompatibleStateImageBehavior = false;
			this.assetView.View = System.Windows.Forms.View.Details;
			this.assetView.SelectedIndexChanged += new System.EventHandler(this.assetsView_SelectedIndexChanged);
			// 
			// btnSwitchType
			// 
			this.btnSwitchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSwitchType.Location = new System.Drawing.Point(575, 12);
			this.btnSwitchType.Name = "btnSwitchType";
			this.btnSwitchType.Size = new System.Drawing.Size(197, 27);
			this.btnSwitchType.TabIndex = 5;
			this.btnSwitchType.Text = "Вид: Денежные активы";
			this.btnSwitchType.UseVisualStyleBackColor = true;
			this.btnSwitchType.Click += new System.EventHandler(this.btnSwitchType_Click);
			// 
			// btnAddFinal
			// 
			this.btnAddFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddFinal.Enabled = false;
			this.btnAddFinal.Location = new System.Drawing.Point(12, 376);
			this.btnAddFinal.Name = "btnAddFinal";
			this.btnAddFinal.Size = new System.Drawing.Size(105, 31);
			this.btnAddFinal.TabIndex = 7;
			this.btnAddFinal.Text = "Сохранить";
			this.btnAddFinal.UseVisualStyleBackColor = true;
			this.btnAddFinal.Click += new System.EventHandler(this.btnAddFinal_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(123, 376);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(99, 31);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Отменить";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// assetEdit
			// 
			this.assetEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.assetEdit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.assetEdit.BackgroundColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.assetEdit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.assetEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.assetEdit.Enabled = false;
			this.assetEdit.Location = new System.Drawing.Point(12, 413);
			this.assetEdit.Name = "assetEdit";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.assetEdit.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.assetEdit.Size = new System.Drawing.Size(760, 137);
			this.assetEdit.TabIndex = 9;
			this.assetEdit.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.assetEdit_CellContentClick);
			// 
			// assetSubtypeSwitch
			// 
			this.assetSubtypeSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.assetSubtypeSwitch.Enabled = false;
			this.assetSubtypeSwitch.FormattingEnabled = true;
			this.assetSubtypeSwitch.ItemHeight = 13;
			this.assetSubtypeSwitch.Items.AddRange(new object[] {
            "Предмет",
            "Здание"});
			this.assetSubtypeSwitch.Location = new System.Drawing.Point(228, 382);
			this.assetSubtypeSwitch.Name = "assetSubtypeSwitch";
			this.assetSubtypeSwitch.Size = new System.Drawing.Size(180, 21);
			this.assetSubtypeSwitch.TabIndex = 10;
			this.assetSubtypeSwitch.SelectedIndexChanged += new System.EventHandler(this.assetSubtypeSwitch_SelectedIndexChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.assetSubtypeSwitch);
			this.Controls.Add(this.assetEdit);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAddFinal);
			this.Controls.Add(this.btnSwitchType);
			this.Controls.Add(this.assetView);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnNew);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(600, 600);
			this.Name = "MainForm";
			this.Text = "AssetView";
			((System.ComponentModel.ISupportInitialize)(this.assetEdit)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.ListView assetView;
		private System.Windows.Forms.Button btnSwitchType;
		private System.Windows.Forms.Button btnAddFinal;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridView assetEdit;
		private System.Windows.Forms.ComboBox assetSubtypeSwitch;
	}
}

