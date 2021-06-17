using OrganizaciaFirmy.Controllers.Exceptions;
using OrganizaciaFirmy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Models
{
    public class Projekt : ModelVzor
    {
        public string Zameranie { get; set; }

        public int IdVeducehoProjektu { get; set; }

        public int IdDivizie { get; set; }
    }
}
