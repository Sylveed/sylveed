using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
    public class SPersonRepository : RepositoryBase<SPersonId, SPerson>
    {
        public SPersonRepository() : base(x => x.Id)
        {

        }
    }
}
