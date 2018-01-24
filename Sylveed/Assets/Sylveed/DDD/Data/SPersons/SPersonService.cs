using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Data.SPersons
{
	public class SPersonService
	{
        [Inject]
        readonly SPersonStorage storage;

		public IEnumerable<SPerson> Items
		{
			get { return storage.Items; }
		}
	}
}
