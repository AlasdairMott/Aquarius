using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Aquarius
{
	public class CreatePart : GH_Component
	{
		/// <summary>
		/// Initializes a new instance of the CreatePart class.
		/// </summary>
		public CreatePart()
		  : base("CreatePart", "Nickname",
			  "Description",
			  "Aquarius", "Create")
		{
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddTextParameter("Name", "N", "Name of the component", GH_ParamAccess.item);
			pManager.AddGeometryParameter("Geometry", "G", "Geometry attached to component", GH_ParamAccess.list);
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.AddParameter(new PartParameter());
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			string name = "";
			List<GeometryBase> geometry = new List<GeometryBase>();

			if (!DA.GetData(0, ref name)) return;
			if (!DA.GetDataList(1, geometry)) return;

			Part part = new Part(name, geometry.ToArray());

			DA.SetData(0, part);
		}

		/// <summary>
		/// Provides an Icon for the component.
		/// </summary>
		protected override System.Drawing.Bitmap Icon
		{
			get
			{
				return Properties.Resources.CreateComponent;
			}
		}

		/// <summary>
		/// Gets the unique ID for this component. Do not change this ID after release.
		/// </summary>
		public override Guid ComponentGuid
		{
			get { return new Guid("af05a2d8-f599-4b1e-97e3-bffc4819b72e"); }
		}
	}
}