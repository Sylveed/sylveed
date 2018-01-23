using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {

    }
}
