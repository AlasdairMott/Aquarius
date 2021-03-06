using System;
using System.Drawing;

using Grasshopper.Kernel;
using Grasshopper;

using Rhino.PlugIns;
using RhinoWindows.Controls;
using System.Collections.Generic;

namespace Aquarius
{
	public class AquariusInfo : GH_AssemblyInfo
	{
		static AquariusMenu _canvasViewport_handle;


		//public bool IsToolbarVisible { get { return _canvasViewport.IsVisible; } }

		public AquariusInfo()
		{
			//if (_canvasViewport == null)
			//{

			//}
			_canvasViewport_handle = new AquariusMenu();
			_canvasViewport_handle.AddToMenu();

			Settings.UsedParts = new List<Part>();
			Settings.ActiveCell = null;
			Settings.RandomChoice = false;
		}

		

		public override string Name
		{
			get
			{
				return "Aquarius";
			}
		}
		public override Bitmap Icon
		{
			get
			{
				//Return a 24x24 pixel bitmap to represent this GHA library.
				return null;
			}
		}
		public override string Description
		{
			get
			{
				//Return a short string describing the purpose of this GHA library.
				return "";
			}
		}
		public override Guid Id
		{
			get
			{
				return new Guid("9bed4175-e6e8-4154-87a5-3ba0ba7a6ec0");
			}
		}

		public override string AuthorName
		{
			get
			{
				//Return a string identifying you or your company.
				return "";
			}
		}
		public override string AuthorContact
		{
			get
			{
				//Return a string representing your preferred contact details.
				return "";
			}
		}
	}

	public static class Settings
	{
		static public Classes.Cell ActiveCell { get; set; }

		static public Part ActivePart { get; set; }

		static public List<Part> UsedParts { get; set; }

		static public List<Part> ActiveParts 
		{
			get 
			{
				List<Part> parts = new List<Part>();
				foreach (var v in Grasshopper.Instances.ActiveCanvas.Document.Objects)
				{
					if (v.ComponentGuid == new Guid("bd735664-457a-4d7f-9dbe-95f30183a472"))
					{
						AddOption addOption = v as AddOption;

						parts.AddRange(addOption.Parts);

						//v.ObjectChanged +=
						//v.
						//this.Params.Output[0].
					}
				}
				return parts;
			}
		}

		static public bool RandomChoice { get; set; }

	}


}
