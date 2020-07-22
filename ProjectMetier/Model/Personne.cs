using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMetier.Model
{
    public class Personne
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Nom"),MaxLength(80),Required(ErrorMessage ="***")]
        public string Nom { get; set; }

        [Display(Name = "Prénom"), MaxLength(80), Required(ErrorMessage = "***")]
        public string Prenom { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email"), MaxLength(80), Required(ErrorMessage = "***")]
        public string Email { get; set; }

        [Display(Name = "Téléphone"), MaxLength(80), Required(ErrorMessage = "***")]
        public string Tel { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance"), Required(ErrorMessage = "***")]
        public DateTime DateNaissance { get; set; }
    }
}