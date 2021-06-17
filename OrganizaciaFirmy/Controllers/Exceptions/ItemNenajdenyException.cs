using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Controllers.Exceptions
{
    public class ItemNenajdenyException : Exception 
    {
        public ItemNenajdenyException(string coNeboloNajdene) : base (String.Format("/Item {0} nebol najdeny/", coNeboloNajdene))
        {

        }
    }
}
