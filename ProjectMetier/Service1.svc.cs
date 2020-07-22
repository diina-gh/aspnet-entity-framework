using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ProjectMetier.Model;

namespace ProjectMetier
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        private bdPersonneContext db = new bdPersonneContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        public bool AddPersonne(Personne personne)
        {
            bool rep = false;
            try{
                db.personnes.Add(personne);
                db.SaveChanges();
                rep = true;
            }catch(Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
            return rep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Personne GetPersonne(int id)
        {
            return db.personnes.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        public bool UpdatePersonne(Personne personne)
        {
            bool rep = false;
            try
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
            return rep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePersonne(int id)
        {
            bool rep = false;
            try
            {
                var pers = db.personnes.Find(id);
                db.personnes.Remove(pers);
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
            return rep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <param name="debutDateNaissance"></param>
        /// <param name="finDateNaissance"></param>
        /// <returns></returns>
        public List<Personne> ListPersonne(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            var list = db.personnes.ToList();
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
