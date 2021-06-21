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
		private int size;
		private int count;
		Cell[,,] array3D;
		#endregion

		#region properties
		public int Size { get { return size; } }
		public List<Cell> Cells { get { return array3D.Cast<Cell>().ToList(); } }
		public List<Brep> DisplayBrep { get { return Cells.Select(o => o.DisplayBrep).ToList(); } }
		public Classes.MouseSelector mouseSelector;
		#endregion

		#region constructors
		public Grid(int s, int c) 
		{
			size = s;
			count = c;

			array3D = new Cell[c, c, c];
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
		}
		#endregion
	}
}
