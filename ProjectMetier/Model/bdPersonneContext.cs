using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectMetier.Model
{
    public class bdPersonneContext:DbContext
    {
        public bdPersonneContext():base("Conn")
        {
        }

        public DbSet<Personne> personnes { get; set; }
    }
}