using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.General
{
	public interface IAppVersionAndBuild
	{
		string GetVersionNumber();
		string GetBuildNumber();
	}
}
