using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

namespace Aquarius.Classes
{
	public class Cell
	{
		#region fields
		(int, int, int) coordinates;
		int size;
		private Brep displayBrep;
		private Mesh selectionMesh;
		private Grid parent;
		#endregion

		#region properties
		public Brep DisplayBrep {get { return displayBrep; } }
		public double parameter;
		#endregion

		#region constructors
		public Cell((int, int, int) coord, Grid p) 
		{
			coordinates = coord;
			parent = p;

			displayBrep = GetBox().ToBrep();

			//Create selection mesh
			selectionMesh = new Mesh();
			Mesh[] meshes = Mesh.CreateFromBrep(displayBrep, MeshingParameters.FastRenderMesh);
			foreach (Mesh m in meshes) selectionMesh.Append(m);
		}
		#endregion

		#region methods
		public void Subscribe() { parent.mouseSelector.MousePressed += OnMouseDown; }
		public void Unsubscribe() { parent.mouseSelector.MousePressed -= OnMouseDown; }
		private void OnMouseDown(object sender, MouseSelectHandler e)
		{
			if ((selected && !e.remove) || (!selected && e.remove)) return;

			//Find Screen Point
			Point2d mouse_pt = new Point2d(e.point.X, e.point.Y);
			Line l = e.viewport.ClientToWorld(mouse_pt);
			Plane projection_plane = e.viewport.GetConstructionPlane().Plane;
			Point3d viewport_point;
			if (Rhino.Geometry.Intersect.Intersection.LinePlane(l, projection_plane, out double p)) viewport_point = l.PointAt(p);
			else return;

			//Create Ray from camera
			Point3d camera_pt = e.viewport.CameraLocation;
			Vector3d direction = new Vector3d(viewport_point - camera_pt);
			Ray3d r = new Rhino.Geometry.Ray3d(camera_pt, direction);

			//Object selection
			parameter = Rhino.Geometry.Intersect.Intersection.MeshRay(selectionMesh, r);
			if (parameter > 0.0)
			{
				parent.mouseSelector.selected.Add(this);
			}
		}
		public Box GetBox()
		{
			Interval x = new Interval(0, parent.Size);
			Interval y = new Interval(0, parent.Size);
			Interval z = new Interval(0, parent.Size);

			Plane plane = Plane.WorldXY;
			plane.Origin = (new Point3d(coordinates.Item1, coordinates.Item2, coordinates.Item3)) * parent.Size;

			Box b = new Box(plane, x, y, z);
			return b;
		}
		#endregion
	}
}
