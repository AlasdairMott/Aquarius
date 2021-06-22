using System;
using System.Collections.Generic;
using System.Linq;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Aquarius.Classes;

namespace Aquarius
{
	public class AquariusGrid : GH_Component
	{
		#region fields
		private Grid grid;
		private Rhino.Display.DisplayMaterial display_material;
		#endregion

		/// <summary>
		/// Initializes a new instance of the AquariusGrid class.
		/// </summary>
		public AquariusGrid()
		  : base("Aquarius Grid", "AG","Create grid to place components","Aquarius", "Create")
		{
			display_material = new Rhino.Display.DisplayMaterial(System.Drawing.Color.Yellow);
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddIntegerParameter("Size", "S", "Size of the grid", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Count", "C", "Number of cells along each dimension", GH_ParamAccess.item);
			pManager.AddBooleanParameter("Enabled", "E", "Select grid cells enabled", GH_ParamAccess.item);
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.AddGenericParameter("Grid", "G", "Grid", GH_ParamAccess.item);
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			int size = 0;
			int count = 0;
			if (!DA.GetData(0, ref size)) return;
			if (!DA.GetData(1, ref count)) return;

			bool run = false;
			if (!DA.GetData("Enabled", ref run)) return;

			if (grid == null) grid = new Grid(size, count);

			if (run) grid.MouseSelector.Enabled = true;
			else
			{
				grid.MouseSelector.Enabled = false;
				grid.SelectionRetrigger = true;
			}

			DA.SetData(0, grid);

			if (grid.MouseSelector.Enabled) grid.SelectionRetrigger = false;
		}

		protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
		{
			base.AppendAdditionalComponentMenuItems(menu);
			Menu_AppendItem(menu, "Reset", menu_ResetGrid);
		}

		private void menu_ResetGrid(object sender, EventArgs e)
		{
			grid.Unsubscribe();
			grid = null;
			this.ExpireSolution(true); 
		}

		/// <summary>
		/// Provides an Icon for the component.
		/// </summary>
		protected override System.Drawing.Bitmap Icon
		{
			get
			{
				return Properties.Resources.Grid;
			}
		}

		/// <summary>
		/// Gets the unique ID for this component. Do not change this ID after release.
		/// </summary>
		public override Guid ComponentGuid
		{
			get { return new Guid("e29471d1-da15-47df-9939-d63319a42fad"); }
		}

		public override BoundingBox ClippingBox
		{
			get
			{
				if (grid != null)
				{
					BoundingBox boundingBox = new BoundingBox();
					List<Brep> breps = grid.DisplayBrep;
					breps.ForEach(x => boundingBox.Union(x.GetBoundingBox(false)));
					return boundingBox;
				}
				else return BoundingBox.Empty;
			}
		}

		public override void DrawViewportMeshes(IGH_PreviewArgs args)
		{
			//base.DrawViewportMeshes(args);
			if (grid != null)
			{
				List<Brep> breps = new List<Brep>();
				grid.Cells.ForEach(x => { if (x.Activated) breps.Add(x.DisplayBrep); });
				breps.ForEach(x => args.Display.DrawBrepShaded(x, display_material));
			}
		}

		public override void DrawViewportWires(IGH_PreviewArgs args)
		{
			//base.DrawViewportWires(args);
			if (grid != null)
			{
				List<Brep> breps = grid.DisplayBrep;
				System.Drawing.Color black = System.Drawing.Color.Black;
				breps.ForEach(x => args.Display.DrawBrepWires(x, black, -1));
			}
			
		}

		public override bool IsPreviewCapable { get { return true; } }


	
	}
}