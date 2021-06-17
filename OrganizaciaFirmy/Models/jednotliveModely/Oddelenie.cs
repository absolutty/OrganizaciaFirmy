using OrganizaciaFirmy.Controllers.Exceptions;
using OrganizaciaFirmy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Models
{
    public class Oddelenie : ModelVzor
    {
        public int IdVeducehoOddelenia{ get; set; }

        public int IdProjektu { get; set; }


    }
}
