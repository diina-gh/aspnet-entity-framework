using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ProjectWEB.App_Start
{
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="erreur"></param>
        public static void WriteLogSystem(string erreur)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Project22Web";
                eventLog.WriteEntry(string.Format("date:{0}, libelle:{1}, description:{2}", DateTime.Now, "Project22", erreur));
            }
        }
    }
}