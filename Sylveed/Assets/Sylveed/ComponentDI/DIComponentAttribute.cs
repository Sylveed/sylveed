using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.ComponentDI
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public abstract class DIComponentAttribute : Attribute
	{
		readonly string parentName;

		public string ParentName { get { return parentName; } }

		public DIComponentAttribute()
		{
		}

		public DIComponentAttribute(string parentName)
		{
			this.parentName = parentName;
		}
	}
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class DITypedComponentAttribute : DIComponentAttribute
	{
		public DITypedComponentAttribute()
		{
		}

		public DITypedComponentAttribute(string parentName) : base(parentName)
		{
		}
	}
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class DINamedComponentAttribute : DIComponentAttribute
	{
		public DINamedComponentAttribute()
		{
		}

		public DINamedComponentAttribute(string parentName) : base(parentName)
		{
		}
	}
}
