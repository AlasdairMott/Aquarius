using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using Rhino.Display;

namespace Aquarius.Classes
{
	public class Cell
	{
		#region fields
		(int, int, int) coordinates;
		private Brep displayBrep;
		private Mesh selectionMesh;
		private Grid parent;
		private bool activated;
		#endregion

		#region properties
		public Brep	  DisplayBrep {get { return displayBrep; } }
		public double Parameter { get; set; }
		public bool	  Activated { get { return activated; } set { activated = value;} }
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

			Subscribe();
		}
		#endregion

		#region methods
		public void Subscribe() { parent.MouseSelector.MousePressed += OnMouseDown; }
		public void Unsubscribe() { parent.MouseSelector.MousePressed -= OnMouseDown; }
		private void OnMouseDown(object sender, MouseSelectHandler e)
		{
			if ((activated && !e.remove) || (!activated && e.remove)) return;

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
			Parameter = Rhino.Geometry.Intersect.Intersection.MeshRay(selectionMesh, r);
			if (Parameter > 0.0)
			{
				ZBufferCapture z_depth_capture = new ZBufferCapture(e.viewport);
				double depth = z_depth_capture.ZValueAt((int)mouse_pt.X, (int)mouse_pt.Y);
				Point3d depth_point = z_depth_capture.WorldPointAt((int)mouse_pt.X, (int)mouse_pt.Y);
				z_depth_capture.Dispose();

				if (depth == 1.0) return;

				//Rhino.DocObjects.ObjectAttributes atttributes_1 = new Rhino.DocObjects.ObjectAttributes();
				//atttributes_1.Name = "depth_pt";

				//Rhino.DocObjects.ObjectAttributes atttributes_2 = new Rhino.DocObjects.ObjectAttributes();
				//atttributes_2.Name = "viewport_point";

				//Rhino.RhinoDoc.ActiveDoc.Objects.AddPoint(depth_point, atttributes_1);
				//Rhino.RhinoDoc.ActiveDoc.Objects.AddPoint(viewport_point, atttributes_2);

				double distance_to_zbuffer   = depth_point.DistanceTo(e.viewport.CameraLocation);
				double distance_to_selection = viewport_point.DistanceTo(e.viewport.CameraLocation);

				if (distance_to_zbuffer < distance_to_selection) return;

				parent.MouseSelector.selected.Add(this);
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
