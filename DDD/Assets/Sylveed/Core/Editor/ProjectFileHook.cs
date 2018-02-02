using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEditor;

namespace Assets.Sylveed.Core.Editor
{
	[InitializeOnLoad]
	public class ProjectFileHook
	{
		// necessary for XLinq to save the xml project file in utf8
		private class Utf8StringWriter : StringWriter
		{
			public override Encoding Encoding
			{ 
				get { return Encoding.UTF8; }
			}
		}

		static ProjectFileHook()
		{
			SyntaxTree.VisualStudio.Unity.Bridge.ProjectFilesGenerator.ProjectFileGeneration += (string name, string content) =>
			{
				var newContent = XDocument.Parse(content);
				var ns = newContent.Root.Name.Namespace;
				newContent.Descendants(ns + "TargetFrameworkVersion").ForEach(x => x.SetValue("v3.5"));
				
				using (var sw = new Utf8StringWriter())
				{
					newContent.Save(sw);

					return sw.ToString();
				}
			};
		}
	}
}

