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
	public partial class AquariusToolBar : UserControl
	{
		public AquariusToolBar()
		{
			InitializeComponent();
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			
		}

		private void updateDropdown(object sender, EventArgs e)
		{
			partPicker.Items.Clear();

			List<string> partNames = Settings.ActiveParts.Select(o => o.Name).ToList();

			partPicker.Items.AddRange(partNames.ToArray<object>());
		}

		private void selectedPart(object sender, EventArgs e)
		{
			Settings.ActivePart = Settings.ActiveParts[partPicker.SelectedIndex];
		}

		private void RotateCell(object sender, EventArgs e)
		{
			if (Settings.ActiveCell == null) return;
			Settings.ActiveCell.Spin += 1;
			Settings.ActiveCell.Parent.MouseSelector.ParentComponent.ExpireSolution(true);
		}

		private void ToggleRandom(object sender, EventArgs e)
		{
			Settings.RandomChoice = !Settings.RandomChoice;
		}
	}
}
