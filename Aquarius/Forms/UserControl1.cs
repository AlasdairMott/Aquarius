using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aquarius.Forms
{
	public partial class UserControl1 : UserControl
	{
		public UserControl1()
		{
			InitializeComponent();
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			
		}

		private void updateDropdown(object sender, EventArgs e)
		{
			PartPicker.Items.Clear();
			PartPicker.Items.AddRange(Settings.ActiveOptionNames.ToArray<object>());
		}
	}
}
