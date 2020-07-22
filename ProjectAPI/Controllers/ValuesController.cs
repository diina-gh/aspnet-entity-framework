using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        private bdPersonneEntities db = new bdPersonneEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void AddPersonne([FromBody] Personnes value)
        {
            try
            {
                db.Personnes.Add(value);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Personnes GetPersonne(int id)
        {
            return db.Personnes.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void UpdatePersonne([FromBody] Personnes value)
        {
            try
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void DeletePersonne(int id)
        {
            try
            {
                var pers = db.Personnes.Find(id);
                db.Personnes.Remove(pers);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <param name="debutDateNaissance"></param>
        /// <param name="finDateNaissance"></param>
        /// <returns></returns>
        public List<Personnes> ListPersonne(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            var list = db.Personnes.ToList();
            if (!string.IsNullOrEmpty(email))
            {
                list = list.Where(a => a.Email.ToUpper().Contains(email.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(tel))
            {
                list = list.Where(a => a.Tel.ToUpper().Contains(tel.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(debutDateNaissance))
            {
                DateTime laDate = DateTime.Parse(debutDateNaissance);
                list = list.Where(a => a.DateNaissance >= laDate).ToList();
            }
            if (!string.IsNullOrEmpty(finDateNaissance))
            {
                DateTime laDate = DateTime.Parse(finDateNaissance);
                list = list.Where(a => a.DateNaissance <= laDate).ToList();
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="erreur"></param>
        public static void WriteLogSystem(string erreur)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Project22";
                eventLog.WriteEntry(string.Format("date:{0}, libelle:{1}, description:{2}", DateTime.Now, "Project22", erreur));
            }
        }

    }
}
