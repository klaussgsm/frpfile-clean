using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DxQ_003D
{
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	internal class DhQ_003D
	{
		private static ResourceManager EBQ_003D;

		private static CultureInfo ERQ_003D;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ExQ_003D
		{
			get
			{
				if (EBQ_003D == null)
				{
					ResourceManager resourceManager = EBQ_003D = new ResourceManager("DxQ=.DhQ=", typeof(DhQ_003D).Assembly);
				}
				return EBQ_003D;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo FhQ_003D
		{
			get
			{
				return ERQ_003D;
			}
			set
			{
				ERQ_003D = value;
			}
		}

		internal static Bitmap GBQ_003D
		{
			get
			{
				object @object = ExQ_003D.GetObject("icon-donate", ERQ_003D);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap GhQ_003D
		{
			get
			{
				object @object = ExQ_003D.GetObject("icons8-facebook-64", ERQ_003D);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap HBQ_003D
		{
			get
			{
				object @object = ExQ_003D.GetObject("icons8-paypal-48", ERQ_003D);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap HhQ_003D
		{
			get
			{
				object @object = ExQ_003D.GetObject("icons8-website-48", ERQ_003D);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap IBQ_003D
		{
			get
			{
				object @object = ExQ_003D.GetObject("icons8-youtube-30", ERQ_003D);
				return (Bitmap)@object;
			}
		}

		internal DhQ_003D()
		{
		}

		static DhQ_003D()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
		}
	}
}
