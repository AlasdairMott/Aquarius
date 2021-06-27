namespace Aquarius.Forms
{
	partial class AquariusToolBar
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.partPicker = new System.Windows.Forms.ToolStripComboBox();
			this.Add = new System.Windows.Forms.ToolStripButton();
			this.Rotate = new System.Windows.Forms.ToolStripButton();
			this.RandomiseChoice = new System.Windows.Forms.ToolStripButton();
			this.RandomiseSpin = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add,
            this.Rotate,
            this.partPicker,
            this.RandomiseChoice,
            this.RandomiseSpin});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(404, 50);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
			// 
			// partPicker
			// 
			this.partPicker.DropDownWidth = 150;
			this.partPicker.Name = "partPicker";
			this.partPicker.Size = new System.Drawing.Size(180, 50);
			this.partPicker.DropDown += new System.EventHandler(this.updateDropdown);
			this.partPicker.SelectedIndexChanged += new System.EventHandler(this.selectedPart);
			// 
			// Add
			// 
			this.Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Add.Image = global::Aquarius.Properties.Resources.Add;
			this.Add.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Add.Name = "Add";
			this.Add.Size = new System.Drawing.Size(46, 44);
			this.Add.Text = "Add";
			// 
			// Rotate
			// 
			this.Rotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Rotate.Image = global::Aquarius.Properties.Resources.Rotate;
			this.Rotate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Rotate.Name = "Rotate";
			this.Rotate.Size = new System.Drawing.Size(46, 44);
			this.Rotate.Text = "toolStripButton2";
			this.Rotate.Click += new System.EventHandler(this.RotateCell);
			// 
			// RandomiseChoice
			// 
			this.RandomiseChoice.CheckOnClick = true;
			this.RandomiseChoice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RandomiseChoice.Image = global::Aquarius.Properties.Resources.RandomChoice;
			this.RandomiseChoice.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RandomiseChoice.Name = "RandomiseChoice";
			this.RandomiseChoice.Size = new System.Drawing.Size(46, 44);
			this.RandomiseChoice.Text = "toolStripButton3";
			this.RandomiseChoice.Click += new System.EventHandler(this.ToggleRandom);
			// 
			// RandomiseSpin
			// 
			this.RandomiseSpin.CheckOnClick = true;
			this.RandomiseSpin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RandomiseSpin.Image = global::Aquarius.Properties.Resources.RandomRotate;
			this.RandomiseSpin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RandomiseSpin.Name = "RandomiseSpin";
			this.RandomiseSpin.Size = new System.Drawing.Size(46, 44);
			this.RandomiseSpin.Text = "toolStripButton4";
			// 
			// AquariusToolBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.Controls.Add(this.toolStrip1);
			this.Name = "AquariusToolBar";
			this.Size = new System.Drawing.Size(404, 118);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton Add;
		private System.Windows.Forms.ToolStripButton Rotate;
		private System.Windows.Forms.ToolStripComboBox partPicker;
		private System.Windows.Forms.ToolStripButton RandomiseChoice;
		private System.Windows.Forms.ToolStripButton RandomiseSpin;

		public System.Windows.Forms.ToolStripComboBox PartPicker { get { return partPicker; } }
	}
}
