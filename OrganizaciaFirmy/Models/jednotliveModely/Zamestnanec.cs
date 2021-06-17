using OrganizaciaFirmy.Controllers.Exceptions;
using OrganizaciaFirmy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Models
{
    public class Zamestnanec
    {
        public int Id { get; set; }

        public string Titul { get; set; }

        public string Meno { get; set; }

        public string Priezvisko { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public bool jeRiaditelom { get; set; }

        public int DiviziaId { get; set; } //id divizie v ktorej je zamestnanec zaradeny

        public int ProjektId { get; set; } //id projektu na ktorom zamestnanec pracuje

        public int OddelenieID { get; set; } //id oddelenia na ktorom sa zamestnanec nachadza

        /**
         * upravi atributy zamestnanca
         */
        public void upravHodnoty(Zamestnanec podlaTohto)
        {
            //(ak upravovany atr nie je null) ? (uprav hodnotu) : (ponechaj povodnu hodnotu)
            Titul = (podlaTohto.Titul != null) ? (podlaTohto.Titul) : (Titul);
            Meno = (podlaTohto.Meno != null) ? (podlaTohto.Meno) : (Meno);
            Priezvisko = (podlaTohto.Priezvisko != null) ? (podlaTohto.Priezvisko) : (Priezvisko);
            Telefon = (podlaTohto.Telefon != null) ? (podlaTohto.Telefon) : (Telefon);
            Email = (podlaTohto.Email != null) ? (podlaTohto.Email) : (Email);

            //jednotlive id (DiviziaId, ProjektId, OddelenieID) nemozu byt zaporne
            if ((podlaTohto.DiviziaId >= 0) && (podlaTohto.ProjektId >= 0) && (podlaTohto.OddelenieID >= 0))
            {
                DiviziaId = (podlaTohto.DiviziaId != 0) ? (podlaTohto.DiviziaId) : (DiviziaId);
                ProjektId = (podlaTohto.ProjektId != 0) ? (podlaTohto.ProjektId) : (ProjektId);
                OddelenieID = (podlaTohto.OddelenieID != 0) ? (podlaTohto.OddelenieID) : (OddelenieID);
            }
            else
            {
                throw new ArgumentException();//aspon jedno z menenych id je zaporne
            }
        }

        public override string ToString()
        {
            return String.Format("[{0}]: {1} {2}, tel.: {3}, mail: {4}", Id, Meno, Priezvisko, Telefon, Email);
        }

        /**
        * pomocna metóda kt. zisti ci zadane idProjektu je spravne (či taka je v databáze)
        */
        public static Zamestnanec existuje(ApiDbContext databaza, int idZamestnanca)
        {
            Zamestnanec z  = databaza.ZoznamZamestnancov.Find(idZamestnanca);

            if (z == null)
            { //Zamestnanec nebol najdeny, throw exception
                throw new ItemNenajdenyException("Zamestnanec");
            }

            return z;
        }
    }
}
