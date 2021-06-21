using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.UI;
using Rhino.Display;

namespace Aquarius.Classes
{
	public class MouseSelector : MouseCallback
	{
		public event EventHandler<MouseSelectHandler> MousePressed;
		private Grid parent;

		public List<Cell> selected;
		public MouseSelector(Grid p) { parent = p; }

		protected override void OnMouseDown(MouseCallbackEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.MouseButton == MouseButton.Right) return;

			selected = new List<Cell>();

			MouseSelectHandler mouseSelect = new MouseSelectHandler(e.View.ActiveViewport, e.ViewportPoint, e.CtrlKeyDown);
			MousePressed?.Invoke(this, mouseSelect);

			if (selected.Count > 0)
			{
				double min = System.Double.PositiveInfinity;
				int index_closest = 0;
				for (int i = 0; i < selected.Count; i++)
				{
					if (selected[i].parameter < min)
					{
						min = selected[i].parameter;
						index_closest = i;
					}
				}

				if (!e.CtrlKeyDown)
				{
					if (parent.selectThroughObjects == true) for (int i = 0; i < selected.Count; i++)
						{
							selected[i].selected = true;
							parent.selectionGeometriesStack.Push(selected[i]);
						}
					else
					{
						selected[index_closest].selected = true;
						parent.selectionGeometriesStack.Push(selected[index_closest]);
					}


				}
				else
				{
					if (parent.selectThroughObjects == true) for (int i = 0; i < selected.Count; i++)
						{
							selected[i].selected = false;

							List<SelectionGeometry> selected_list = parent.selectionGeometriesStack.ToList<SelectionGeometry>();
							selected_list.Remove(selected[i]);
							parent.selectionGeometriesStack = new Stack<SelectionGeometry>(selected_list);
						}
					else
					{
						selected[index_closest].selected = false;

						List<SelectionGeometry> selected_list = parent.selectionGeometriesStack.ToList<SelectionGeometry>();
						selected_list.Remove(selected[index_closest]);
						parent.selectionGeometriesStack = new Stack<SelectionGeometry>(selected_list);
					}

				}

				parent.selectionRetrigger = true;
				parent.ExpireSolution(true);
				e.View.Redraw();
			}
		}
	}

	public class MouseSelectHandler : EventArgs
	{
		private readonly RhinoViewport _viewport;
		private readonly System.Drawing.Point _point;
		private readonly bool _remove;

		public MouseSelectHandler(RhinoViewport vp, System.Drawing.Point pt, bool r)
		{
			_viewport = vp;
			_point = pt;
			_remove = r;
		}

		public RhinoViewport viewport { get { return _viewport; } }
		public System.Drawing.Point point { get { return _point; } }
		public bool remove { get { return _remove; } }
	}
}