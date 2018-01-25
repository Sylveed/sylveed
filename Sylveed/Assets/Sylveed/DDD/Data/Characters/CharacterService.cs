using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterService
	{
        [Inject]
        readonly CharacterStorage storage;

		public IEnumerable<Character> Items
		{
			get { return storage.Items; }
		}
	}
}
