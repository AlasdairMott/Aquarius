using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.UI;
using Rhino.Display;
using Rhino.Geometry;

namespace Aquarius.Classes
{
	public class MouseSelector : MouseCallback
	{
		#region fields
		private Grid parent;
		private ZBufferCapture zBufferCapture;
		private double mouseZDepth;
		private double mouseDepth;
		private Point3d mousePoint;
		private Ray3d ray;
		private AquariusGrid parentComponent;
		#endregion

		#region properties
		public event EventHandler<MouseSelectHandler> MousePressed;
		public AquariusGrid ParentComponent => parentComponent;
		public List<Cell> selected;
		public ZBufferCapture ZBufferCapture { get { return zBufferCapture; } }
		public double	MouseZDepth { get { return mouseZDepth; } }
		public double   MouseDepth { get { return mouseDepth; } }
		public Point3d  MousePoint { get { return mousePoint; } }
		public Ray3d	Ray { get { return ray; } }
		#endregion

		#region constructors
		public MouseSelector(Grid p) { parent = p; }
		#endregion

		protected override void OnMouseDown(MouseCallbackEventArgs e)
		{
			base.OnMouseDown(e);
			e.Cancel = true;

			if (e.MouseButton == MouseButton.Right){e.Cancel = false; return;}

			zBufferCapture = new ZBufferCapture(e.View.ActiveViewport);
			mouseZDepth = zBufferCapture.ZValueAt((int)e.ViewportPoint.X, (int)e.ViewportPoint.Y);
			//if (mouseZDepth == 1.0) return;

			//Find position of mouse click in world space
			Point3d cameraLocation = e.View.ActiveViewport.CameraLocation;
			mousePoint = zBufferCapture.WorldPointAt((int)e.ViewportPoint.X, (int)e.ViewportPoint.Y);
			mouseDepth = mousePoint.DistanceTo(cameraLocation);
			zBufferCapture.Dispose();

			//Create ray
			Vector3d direction = new Vector3d(mousePoint - cameraLocation);
			ray = new Ray3d(cameraLocation, direction);

			//Create empty list of canditates
			selected = new List<Cell>();

			//Invoke selection geometry
			MouseSelectHandler mouseSelect = new MouseSelectHandler(e.View.ActiveViewport, e.ViewportPoint, e.CtrlKeyDown);
			MousePressed?.Invoke(this, mouseSelect);

			if (selected.Count > 0)
			{
				double max = 0;
				int index_closest = 0;

				for (int i = 0; i < selected.Count; i++)
				{
					if (selected[i].Parameter > max)
					{
						max = selected[i].Parameter;
						index_closest = i;
					}
				}

				if (!e.CtrlKeyDown)
				{
					selected[index_closest].Activated = true;
					parent.Active.Add(selected[index_closest]);
					Settings.UsedParts.Add(selected[index_closest].Part);
					Settings.ActiveCell = selected[index_closest];
				}
				else
				{
					selected[index_closest].Activated = false;
					parent.Active.Remove(selected[index_closest]);
				}

				parent.SelectionRetrigger = true;
				parentComponent.ExpireSolution(true);

				e.View.Redraw();
				e.Cancel = false;
			}
		}

		internal void AttachGHComponent(AquariusGrid parentComponent)
		{
			this.parentComponent = parentComponent;
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