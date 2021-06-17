using OrganizaciaFirmy.Controllers.Exceptions;
using OrganizaciaFirmy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Models
{
    public class Divizia : ModelVzor
    {
        public string Popis { get; set; }

        public int IdVeducehoDivizie { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Id, Nazov);
        }
    }
}
