using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Aquarius.Classes;

namespace Aquarius
{
	public class DecomposeGrid : GH_Component
	{
		/// <summary>
		/// Initializes a new instance of the DecomposeGrid class.
		/// </summary>
		public DecomposeGrid()
		  : base("Decompose Grid", "DG",
			  "Decompose Grid",
			  "Aquarius", "Decompose")
		{
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddGenericParameter("Grid", "G", "Grid to decompose", GH_ParamAccess.item);
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.AddIntegerParameter("Cell Width", "W", "Size of the grid", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Cell Height", "H", "Size of the grid", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Count", "C", "Number of cells along each dimension", GH_ParamAccess.item);
			pManager.AddGenericParameter("Cells", "C", "Grid cells", GH_ParamAccess.list); 
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			Grasshopper.Kernel.Types.GH_ObjectWrapper grid_object = new Grasshopper.Kernel.Types.GH_ObjectWrapper();
			if (!DA.GetData(0, ref grid_object)) return;

			Grid grid = grid_object.Value as Grid;

			DA.SetData(0, grid.CellWidth);
			DA.SetData(1, grid.CellHeight);


			DA.SetDataList(3, grid.Cells);
		}

		/// <summary>
		/// Provides an Icon for the component.
		/// </summary>
		protected override System.Drawing.Bitmap Icon
		{
			get
			{
				return Properties.Resources.ExplodeGrid;
			}
		}

		/// <summary>
		/// Gets the unique ID for this component. Do not change this ID after release.
		/// </summary>
		public override Guid ComponentGuid
		{
			get { return new Guid("f3264ff4-e84d-44d4-8fed-4385df7af2db"); }
		}
	}
}