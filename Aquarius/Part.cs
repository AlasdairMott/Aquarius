using System;
using System.Collections.Generic;
using System.Linq;

using GH_IO.Serialization;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

using Rhino.Geometry;
using Rhino.FileIO;
using Rhino.Runtime;

namespace Aquarius
{
    /// <summary>
    /// Class describing a Part.
    /// </summary>
    public class Part
    {
        #region fields
        private string name;
        private GeometryBase[] geometry;
		#endregion

		#region properties
        public string Name { get { return name; } }
        public GeometryBase[] Geometry { get { return geometry; } }
        #endregion

        public Part() { }

		public Part(string name, GeometryBase[] geometry)
        {
            this.name = name;
            this.geometry = geometry;
        }

        public Part Duplicate() 
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("Aquarius.Part({0})", name);
        }
    }

    /// <summary>
    /// An IGH_Goo wrapper around Part.
    /// </summary>
    public sealed class PartGoo : GH_Goo<Part>
    {
        #region constructors
        public PartGoo()
         : this(null)
        { }
        public PartGoo(Part part)
        {
            Value = part;
        }
        //public PartGoo(PartGoo PartGooSource)
        //{
        //    this.Value = PartGooSource.Value;
        //}

        public override IGH_Goo Duplicate()
        {
            return new PartGoo(Value);
        }
        #endregion

        #region properties
        public override string ToString()
        {
            if (Value == null) return "No part";
            return Value.ToString();
        }

        public override string TypeName => "Part";
        public override string TypeDescription => "Part to attach to a cell";
        public override bool IsValid
        {
            get
            {
                if (Value == null) return false;
                if (Value.Geometry.Length == 0) return false;
                if (Value.Name == null) return false;
                return true;
            }
        }
        public override string IsValidWhyNot
        {
            get
            {
                if (Value == null) return "No data";
                if (Value.Geometry.Length == 0) return "No geometry data";
                if (Value.Name == null) return "No name";
                return string.Empty;
            }
        }
		#endregion

		#region casting
		public override bool CastFrom(object source)
        {
            if (source == null) return false;

            //Cast from Part
            if (typeof(Part).IsAssignableFrom(source.GetType()))
            {
                Value = (Part)source;
                return true;
            }

            if (source is Brep brep)
            {
                Value = new Part("part_" + Guid.NewGuid().ToString(), new GeometryBase[1] { brep });
                return true;
            }
            return false;
        }
        public override bool CastTo<TQ>(ref TQ target)
        {
            if (Value == null)
                return false;

            //Cast to Part
            if (typeof(TQ).IsAssignableFrom(typeof(Part)))
            {
                if (Value == null)
                    target = default(TQ);
                else
                    target = (TQ)(object)Value;
                return true;
            }

            if (typeof(TQ) == typeof(string))
            {
                target = (TQ)(object)Value.Name;
                return true;
            }
            if (typeof(TQ) == typeof(GH_String))
            {
                target = (TQ)(object)new GH_String(Value.Name);
                return true;
            }

            ////Cast to Brep.
            //if (typeof(TQ).IsAssignableFrom(typeof(GH_Brep)))
            //{
            //    if (Value == null)
            //        target = default(TQ);
            //    else if (Value.Geometry == null)
            //        target = default(TQ);
            //    else
            //    {
            //        Value.Geometry.ToList().Select(o => )

            //        Component brep = Value.LinkComponent;
            //        GH_Brep gh_brep = new GH_Brep();
            //        target = (TQ)(object)gh_brep;
            //    }
            //    return true;
            //}


            //if (typeof(TQ) == typeof(GH_Brep))
            //{
            //    target = (TQ)(object)new GH_Brep(Value.Geometry);
            //    return true;
            //}

            return false;
        }
        #endregion

        #region (de)serialisation
        private const string IoNameKey = "Name";
        private const string IoGeometryKey = "Geometry";
        public override bool Write(GH_IWriter writer)
        {
            if (Value != null)
            {
                //SerializationOptions options = new SerializationOptions();

                //writer.SetString(IoNameKey, Value.Name);

                //List<string> geo_string_list = Value.Geometry.ToList().Select(o => (o as Brep).ToJSON(options)).ToList();
                //string geo_string_joined = string.Join("_", geo_string_list);

                //writer.SetString(IoGeometryKey, geo_string_joined);
            }
            return true;
        }
        public override bool Read(GH_IReader reader)
        {
            Value = null;

            //string name = null;
            //if (reader.ItemExists(IoNameKey))
            //    name = reader.GetString(IoNameKey);

            //string geo_string_joined = null;
            //if (reader.ItemExists(IoGeometryKey))
            //    geo_string_joined = reader.GetString(IoGeometryKey);

            //List<string> geo_string_list = geo_string_joined.Split('_').ToList();
            //List<GeometryBase> geometry_list = geo_string_list.Select(o => (CommonObject.FromJSON(o)) as GeometryBase).ToList();

            //GeometryBase[] geometry = geometry_list.ToArray<GeometryBase>();

            //Value = new Part(name, geometry);

            return true;
        }
        #endregion
    }

    public sealed class PartParameter : GH_PersistentParam<PartGoo>
    {
        public PartParameter()
        : this("Part", "P", "A collection of Part data", "Aquarius", "Part")
        { }
        public PartParameter(GH_InstanceDescription tag) : base(tag) { }
        public PartParameter(string name, string nickname, string description, string category, string subcategory)
          : base(name, nickname, description, category, subcategory) { }

        public override Guid ComponentGuid => new Guid("{d340ad4b-b556-49c5-8eb3-e0f3fbc03efa}");
       
        protected override GH_GetterResult Prompt_Singular(ref PartGoo value)
        {
            return GH_GetterResult.cancel;
        }
        protected override GH_GetterResult Prompt_Plural(ref List<PartGoo> values)
        {
            return GH_GetterResult.cancel;
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Component;
            }
        }
    }
}