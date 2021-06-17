using OrganizaciaFirmy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Models
{
    public class ModelVzor
    {
        public int Id { get; set; }

        public string Nazov { get; set; }

        public static ModelVzor Existuje<T>(ApiDbContext databaza, int naZakladeId) where T : ModelVzor
        {
            Type druhTriedy = typeof(T);

            if (druhTriedy == typeof(Oddelenie)) //idem vyhladavat oddelenie
            {
                Oddelenie o = databaza.ZoznamOddeleni.Find(naZakladeId);
                return o;
            }
            else if (druhTriedy == typeof(Projekt))  //idem vyhladavat projekt
            {
                Projekt p = databaza.ZoznamProjektov.Find(naZakladeId);
                return p;
            }
            else if (druhTriedy == typeof(Divizia))  //idem vyhladavat diviziu
            {
                Divizia d = databaza.ZoznamDivizii.Find(naZakladeId);
                return d;
            }
            else {
                throw new ArgumentException("/Tento druh objektov nie je v databaze/");
            }

        }
    }
}
