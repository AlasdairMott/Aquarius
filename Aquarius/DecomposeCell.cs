using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Aquarius.Classes;

namespace Aquarius
{
	public class DecomposeCell : GH_Component
	{
		/// <summary>
		/// Initializes a new instance of the DecomposeCell class.
		/// </summary>
		public DecomposeCell()
		  : base("Decompose Cell", "DC",
			  "Decompose Cell",
			  "Aquarius", "Decompose")
		{
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddGenericParameter("Cell", "C", "Cell to decompose", GH_ParamAccess.item);
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.AddBooleanParameter("Active", "A", "Cell is active", GH_ParamAccess.item);
			pManager.AddTextParameter("Coordinates", "C", "Cell coordinates", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Spin", "S", "Cell spin", GH_ParamAccess.item);
			pManager.AddGeometryParameter("Geometry", "G", "Cell Geometry", GH_ParamAccess.list);
			pManager.AddParameter(new PartParameter());
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			Grasshopper.Kernel.Types.GH_ObjectWrapper cell_object = new Grasshopper.Kernel.Types.GH_ObjectWrapper();
			if (!DA.GetData(0, ref cell_object)) return;

			Cell cell = cell_object.Value as Cell;

			DA.SetData(0, cell.Activated);
			DA.SetData(1, cell.ToString());
			DA.SetData(2, cell.Spin);

			if (cell.Activated) DA.SetData(3, cell.PartGeometry);

		}

		/// <summary>
		/// Provides an Icon for the component.
		/// </summary>
		protected override System.Drawing.Bitmap Icon
		{
			get
			{
				return Properties.Resources.ExplodeComponent;
			}
		}

		/// <summary>
		/// Gets the unique ID for this component. Do not change this ID after release.
		/// </summary>
		public override Guid ComponentGuid
		{
			get { return new Guid("50a27776-37a8-482e-aae6-6760c25add85"); }
		}
	}
}