using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Aquarius
{
	public class GetOptions : GH_Component
	{
		private int count;
		private bool subscribed;

		/// <summary>
		/// Initializes a new instance of the GetOptions class.
		/// </summary>
		public GetOptions()
		  : base("Get Options", "G","Get the list of all options","Aquarius", "Create")
		{
			count = 0;
			subscribed = false;
		}

		private void Subscribe(bool enabled)
		{
			//if (enabled)
			//{
			//	Grasshopper.Instances.ActiveCanvas.Document.ObjectsAdded += Refresh;
			//	Grasshopper.Instances.ActiveCanvas.Document.ObjectsDeleted += Refresh;
			//}
			//else
			//{
			//	Grasshopper.Instances.ActiveCanvas.Document.ObjectsAdded -= Refresh;
			//	Grasshopper.Instances.ActiveCanvas.Document.ObjectsDeleted -= Refresh;
			//}

			//subscribed = enabled;
		}

		private void Refresh(object sender, EventArgs args) 
		{
			//count = 0;
			//foreach (var v in Grasshopper.Instances.ActiveCanvas.Document.Objects)
			//{
			//	if (v.ComponentGuid == new Guid("bd735664-457a-4d7f-9dbe-95f30183a472"))
			//	{
			//		count += 1;
			//		//v.ObjectChanged +=
			//		//v.
			//		//this.Params.Output[0].
					
			//	}
			//}
			//ExpireSolution(true);
		}

		public override void RemovedFromDocument(GH_Document document) 
		{
			base.RemovedFromDocument(document);
			Subscribe(false);
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.AddTextParameter("Options", "O", "Global options specified", GH_ParamAccess.list);
			pManager.AddNumberParameter("Number", "N", "Number of active instances", GH_ParamAccess.item);
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			DA.SetDataList(0, Settings.ActiveOptionNames);

			if (!subscribed) Subscribe(true);

			DA.SetData(1, count);
		}

		/// <summary>
		/// Provides an Icon for the component.
		/// </summary>
		protected override System.Drawing.Bitmap Icon
		{
			get
			{
				return Properties.Resources.ViewPool;
			}
		}

		/// <summary>
		/// Gets the unique ID for this component. Do not change this ID after release.
		/// </summary>
		public override Guid ComponentGuid
		{
			get { return new Guid("a15a318a-7ba8-46ca-9680-9911e5024604"); }
		}

		

		public class CanvasModifiedHandler : EventArgs
		{
		}
	}
}