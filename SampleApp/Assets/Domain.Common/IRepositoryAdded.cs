using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sylveed.SampleApp
{
	public interface IRepositoryAdded<T>
	{
		T Entity { get; }
	}

}