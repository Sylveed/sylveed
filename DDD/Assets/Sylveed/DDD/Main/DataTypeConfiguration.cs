using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain;
using Assets.Sylveed.DDD.Data;
using Assets.Sylveed.DDD.Data.Characters;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Data.Items;
using Assets.Sylveed.DDD.Main.Implementation;
using Assets.Sylveed.DDD.Main.Implementation.Characters;
using Assets.Sylveed.DDD.Main.Implementation.Skills;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;


namespace Assets.Sylveed.DDD.Main
{
	public static class DataTypeConfiguration
	{
		static readonly Dictionary<RuntimeTypeHandle, Dictionary<object, Type>> idTypeMap =
			new Dictionary<RuntimeTypeHandle, Dictionary<object, Type>>();

		static DataTypeConfiguration()
		{
			Register(new CharacterId(1), typeof(RobotView));
			Register(new CharacterId(2), typeof(RobotView));
			Register(new CharacterId(3), typeof(RobotView));
			Register(new SkillId(1), typeof(ShootBulletView));
			Register(new SkillId(2), typeof(ShootBulletView));
			Register(new SkillId(3), typeof(ShootBulletView));
		}

		static void Register<TId>(TId id, Type instanceType)
		{
			var idTypeHandle = typeof(TId).TypeHandle;

			Dictionary<object, Type> map;
			if (!idTypeMap.TryGetValue(idTypeHandle, out map))
			{
				map = new Dictionary<object, Type>();
				idTypeMap.Add(idTypeHandle, map);
			}

			map.Add(id, instanceType);
		}

		public static Type GetEntry<TId>(TId id)
		{
			var map = idTypeMap[typeof(TId).TypeHandle];
			return map[id];
		}
	}
}
