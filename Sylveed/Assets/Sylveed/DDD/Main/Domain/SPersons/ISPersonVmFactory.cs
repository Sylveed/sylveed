using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
    public interface ISPersonVmFactory
    {
        SPersonVm Create(SPersonVmId id, string name);
    }
}
