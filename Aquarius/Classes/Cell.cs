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
		private Part part;
		#endregion

		#region properties
		public Brep	  DisplayBrep {get { return displayBrep; } }
		public double Parameter { get; set; }
		public bool	  Activated { get { return activated; } set { activated = value;} }
		public List<GeometryBase> PartGeometry 
		{ 
			get 
			{
				if (part == null) return null;

				Vector3d vector = new Vector3d(GetBox().Center);
				Transform transform = Transform.Translation(vector);

				List<GeometryBase> shifted_geometry = new List<GeometryBase>();
				foreach (GeometryBase geo in part.Geometry) 
				{
					GeometryBase g = geo.Duplicate();
					g.Transform(transform);
					shifted_geometry.Add(g);
				}

				return shifted_geometry;
			} 
		}
		public Part Part => part;
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

			//Object selection
			Parameter = Rhino.Geometry.Intersect.Intersection.MeshRay(selectionMesh, parent.MouseSelector.Ray);
			
			if (Parameter > 0.0)
			{
				Point3d meshRayPoint = parent.MouseSelector.Ray.PointAt(Parameter);

				double distance_to_zbuffer   = parent.MouseSelector.MouseDepth;
				double distance_to_selection = meshRayPoint.DistanceTo(e.viewport.CameraLocation);

				if (distance_to_zbuffer < distance_to_selection) return;

				parent.MouseSelector.selected.Add(this);
			}

			part = Settings.ActivePart;
			
		}
		public Box GetBox()
		{
			Interval x = new Interval(0, parent.CellWidth);
			Interval y = new Interval(0, parent.CellWidth);
			Interval z = new Interval(0, parent.CellHeight);

			Plane plane = Plane.WorldXY;
			plane.Origin = (new Point3d(coordinates.Item1 * parent.CellWidth, 
										coordinates.Item2 * parent.CellWidth,
										coordinates.Item3 * parent.CellHeight));

			Box b = new Box(plane, x, y, z);
			return b;
		}
		#endregion
	}
}
