using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Implementation.Characters;

namespace Assets.Sylveed.DDD.Main.Application
{
    public class ResourceSet : ScriptableObject
    {
        [SerializeField]
        CharacterView personView;

        public CharacterView PersonView => personView;

        public static ResourceSet Load()
        {
            return (ResourceSet)Resources.Load("Sylveed/DDD/Main/ResourceSet");
        }
    }
}
