using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Application
{
    public class ResourceProvider
    {
        ResourceSet resourceSet;

        public ResourceSet ResourceSet
        {
            get
            {
                if (resourceSet == null)
                    resourceSet = ResourceSet.Load();
                return resourceSet;
            }
        }
    }
}
