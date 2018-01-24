﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
    public interface ISPersonFactory
    {
        SPerson Create(SPersonId id, string name);
    }
}
