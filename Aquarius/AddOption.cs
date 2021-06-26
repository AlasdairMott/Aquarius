using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Eto;
using System.Windows.Forms;

namespace Aquarius
{
	public class AddOption : GH_Component
	{
		private List<Part> parts;
		public List<Part> Parts { get { return parts; } }

		/// <summary>
		/// Each implementation of GH_Component must provide a public 
		/// constructor without any arguments.
		/// Category represents the Tab in which the component will appear, 
		/// Subcategory the panel. If you use non-existing tab or panel names, 
		/// new tabs/panels will automatically be created.
		/// </summary>
		public AddOption(): base("Add Option", "AQ","Add an option to the pool of options","Aquarius", "Create")
		{
			//this.ObjectChanged += 
			parts = new List<Part>();
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddParameter(new PartParameter(), "Part", "P", "Part to add to the pool", GH_ParamAccess.list);
			pManager[0].Optional = true;
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			
		}

		public override void AddedToDocument(GH_Document document)
		{
			base.AddedToDocument(document);
			SolutionExpired += UpdateListener;

			Grasshopper.Instances.ActiveCanvas.Document.ObjectsAdded += UpdateListener;
			Grasshopper.Instances.ActiveCanvas.Document.ObjectsDeleted += UpdateListener;
		}

		public override void RemovedFromDocument(GH_Document document)
		{
			base.RemovedFromDocument(document);
			//Update();

			SolutionExpired -= UpdateListener;

			Grasshopper.Instances.ActiveCanvas.Document.ObjectsAdded -= UpdateListener;
			Grasshopper.Instances.ActiveCanvas.Document.ObjectsDeleted -= UpdateListener;
		}

		private void UpdateListener(object sender, EventArgs args)
		{
			Update();
		}

		private void Update() 
		{
			foreach (var v in Grasshopper.Instances.ActiveCanvas.Document.Objects)
			{
				if (v.ComponentGuid == new Guid("a15a318a-7ba8-46ca-9680-9911e5024604"))
				{
					GetOptions component = v as GetOptions;
					component.ExpireSolution(true);
				}
			}
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object can be used to retrieve data from input parameters and 
		/// to store data in output parameters.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			parts.Clear();
			if (!DA.GetDataList(0, parts))
			{
				
				return;
			}
		}

		protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
		{
			base.AppendAdditionalComponentMenuItems(menu);
		}

		/// <summary>
		/// Provides an Icon for every component that will be visible in the User Interface.
		/// Icons need to be 24x24 pixels.
		/// </summary>
		protected override System.Drawing.Bitmap Icon
		{
			get
			{
				return Properties.Resources.AddToPool;
			}
		}

		/// <summary>
		/// Each component must have a unique Guid to identify it. 
		/// It is vital this Guid doesn't change otherwise old ghx files 
		/// that use the old ID will partially fail during loading.
		/// </summary>
		public override Guid ComponentGuid
		{
			get { return new Guid("bd735664-457a-4d7f-9dbe-95f30183a472"); }
		}
	}
}
