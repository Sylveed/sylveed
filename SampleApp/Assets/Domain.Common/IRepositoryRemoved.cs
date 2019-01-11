using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sylveed.SampleApp
{
	public interface IRepositoryRemoved<T>
	{
		T Entity { get; }
	}

}