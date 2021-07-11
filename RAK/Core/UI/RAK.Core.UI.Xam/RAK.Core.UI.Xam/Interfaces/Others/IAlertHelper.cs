using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam
{
	public interface IAlertHelper
	{
		Task<bool> CreateIconAlertAccept(string icon, string title, string text, string cancel, string ok, string color = "");

		Task CreateIconAlert(string icon, string title, string text, string cancel, string color = "");
	}
}
