using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

namespace Aquarius.Classes
{
	public class Grid
	{
		#region fields
		private int cellWidth;
		private int cellHeight;
		private int count;
		private Cell[,,] array3D;
		private HashSet<Cell> active;
		private MouseSelector mouseSelector;
		#endregion

		#region properties
		public int CellWidth => cellWidth;
		public int CellHeight => cellHeight;
		public List<Cell> Cells { get { return array3D.Cast<Cell>().ToList(); } }
		public HashSet<Cell> Active { get { return active; } set { active = value; } }
		public List<Brep> DisplayBrep { get { return Cells.Select(o => o.DisplayBrep).ToList(); } }
		public MouseSelector MouseSelector { get { return mouseSelector; } }
		public bool SelectionRetrigger;
		#endregion

		#region constructors
		public Grid(int cellWidth, int cellHeight, int count) 
		{
			this.cellWidth = cellWidth;
			this.cellHeight = cellHeight;
			this.count = count;

			mouseSelector = new MouseSelector(this);

			array3D = new Cell[count, count, count];
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++) 
				{
					for (int k = 0; k < count; k++)
					{
						array3D[i, j, k] = new Cell((i,j,k), this);
					}
				}
			}

			array3D[0, 0, 0].Activated = true;

			active = new HashSet<Cell>();
		}
		#endregion

		#region methods
		public void Unsubscribe() 
		{
			Cells.ForEach(x => x.Unsubscribe());
		}
		#endregion
	}
}
