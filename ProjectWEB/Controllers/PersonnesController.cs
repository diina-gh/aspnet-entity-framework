using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProjectMetier.Model;
using ProjectWEB.App_Start;

namespace ProjectWEB.Controllers
{
    public class PersonnesController : Controller
    {
        //private bdPersonneContext db = new bdPersonneContext();
        ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();

        // GET: Personnes
        public ActionResult Index()
        {
             return View(service.ListPersonne("","","",""));
            // return View(ApiGetListPersonne("", "", "", ""));
        }
        
        // GET: Personnes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = service.GetPersonne((int)id);
            //Personne personne = ApiGetPersonne((int)id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // GET: Personnes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Email,Tel,DateNaissance")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                service.AddPersonne(personne);
               // ApiAddPersonne(personne);
                return RedirectToAction("Index");
            }

            return View(personne);
        }

        // GET: Personnes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             Personne personne = service.GetPersonne((int)id);
            // Personne personne = ApiGetPersonne((int)id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Email,Tel,DateNaissance")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                service.UpdatePersonne(personne);
                return RedirectToAction("Index");
            }
            return View(personne);
        }

        // GET: Personnes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = service.GetPersonne((int)id);
            // Personne personne = ApiGetPersonne((int)id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeletePersonne(id);
            return RedirectToAction("Index");
        }

        #region apiPersonne

        public List<Personne> ApiGetListPersonne(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            HttpClient client = new HttpClient();
            var services = new List<Personne>();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["urlProjectAPI"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(string.Format("project22/values/ListPersonne?email={0}&tel={1}&debutDateNaissance={2}&finDateNaissance{3}",email,tel,debutDateNaissance,finDateNaissance)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = JsonConvert.DeserializeObject<List<Personne>>(responseData);
            }

            return services;
        }


        public Personne ApiGetPersonne(int id)
        {
            HttpClient client = new HttpClient();
            var services = new Personne();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["urlProjectAPI"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(string.Format("project22/values/GetPersonne?id={0}", id)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = JsonConvert.DeserializeObject<Personne>(responseData);
            }

            return services;
        }

        public void ApiAddPersonne (Personne personne)
        {
            var values = new Dictionary<string, string>
            {
                {"Id","0" },
                {"Nom", personne.Nom},
                {"Prenom", personne.Prenom},
                {"Email", personne.Email},
                {"Tel", personne.Tel},
                {"DateNaissance", personne.DateNaissance!=null?personne.DateNaissance.ToString():string.Empty}
            };

            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["urlProjectAPI"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("project22/values/AddPersonne", content).Result;
                }
            }catch(Exception ex)
            {
                Helper.WriteLogSystem(ex.ToString());
            }
        }

        public void ApiEditPersonne(Personne personne)
        {

        }

        #endregion

    }
}
