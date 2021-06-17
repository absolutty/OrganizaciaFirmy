using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizaciaFirmy.Controllers.Exceptions;
using OrganizaciaFirmy.Data;
using OrganizaciaFirmy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrganizaciaFirmy.Controllers
{
    [Route("firma/divizie/{idDivizie}/projekty/{idProjektu}/{controller}")]
    [ApiController]
    public class OddeleniaController : ControllerBase
    {
        public static readonly string _errEmptyDb = "/Zoznam oddeleni je prazdny/"; //databaza neobsahuje ziadne oddelenia
        public static readonly string _errOddelenieNotFound = "/Toto oddelenie nebolo najdenie/"; //databaza neobsahuje taketo oddelenie

        private ApiDbContext _dbcontext;

        public OddeleniaController(ApiDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetOddeleniaProjektu(int idDivizie, int idProjektu)
        {
            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ((proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errOddelenieNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errOddelenieNotFound); }

            DbSet<Oddelenie> oddelenia = _dbcontext.ZoznamOddeleni; //zoznam vsetkych projektov
            IEnumerable<Oddelenie> query = oddelenia.AsQueryable().Where(o => o.IdProjektu == idProjektu);

            return (query.Any()) ? (Ok(query)) : (Ok(_errEmptyDb));
        }

        [HttpGet("{idOddelenia}/zamestnanciOddelenia")]
        public IActionResult GetZamestnancovVOddelenii(int idDivizie, int idProjektu, int idOddelenia)
        {
            Oddelenie odde = (Oddelenie)ModelVzor.Existuje<Oddelenie>(_dbcontext, idOddelenia);
            //dane oddelenie nebolo najdene
            if ((odde == null) || (odde.IdProjektu != idProjektu)) { return BadRequest(_errOddelenieNotFound); }

            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ( (proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errOddelenieNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errOddelenieNotFound); }

            DbSet<Zamestnanec> zamestnanci = _dbcontext.ZoznamZamestnancov; //zoznam vsetkych projektov
            IEnumerable<Zamestnanec> query = zamestnanci.AsQueryable().Where(z => z.OddelenieID == idOddelenia);

            return (query.Any()) ? (Ok(query)) : (Ok(ZamestnanciController._errEmptyDb));
        }

        [HttpPut("{idOddelenia}")]
        public IActionResult PridajDoOddelenia(int idDivizie, int idProjektu, int idOddelenia, [FromForm] int idZamestnanca)
        {
            Oddelenie odde = (Oddelenie)ModelVzor.Existuje<Oddelenie>(_dbcontext, idOddelenia);
            //dane oddelenie nebolo najdene
            if ((odde == null) || (odde.IdProjektu != idProjektu)) { return BadRequest(_errOddelenieNotFound); }

            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ((proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errOddelenieNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errOddelenieNotFound); }

            Zamestnanec zam = null;
            try { zam = Zamestnanec.existuje(_dbcontext, idZamestnanca); }
            catch (ItemNenajdenyException ex) { return BadRequest(ex.Message); }

            zam.DiviziaId = diviz.Id; //zamestnanec priradeny do divizie
            zam.ProjektId = proj.Id;  //zamestnanec priradeny do projektu
            zam.OddelenieID = odde.Id;

            _dbcontext.SaveChanges();
            return Ok();

        }

        [HttpDelete("{idOddelenia}")]
        public IActionResult OdstranZOddelenia(int idDivizie, int idProjektu, int idOddelenia, [FromForm] int idZamestnanca)
        {
            Oddelenie odde = (Oddelenie)ModelVzor.Existuje<Oddelenie>(_dbcontext, idOddelenia);
            //dane oddelenie nebolo najdene
            if ((odde == null) || (odde.IdProjektu != idProjektu)) { return BadRequest(_errOddelenieNotFound); }

            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ((proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errOddelenieNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errOddelenieNotFound); }

            Zamestnanec zam = null;
            try { zam = Zamestnanec.existuje(_dbcontext, idZamestnanca); }
            catch (ItemNenajdenyException ex) { return BadRequest(ex.Message); }

            //JE tento zamestnanec pridany v tomto projekte
            if (zam.OddelenieID == odde.Id)
            {
                zam.OddelenieID = 0;
                _dbcontext.SaveChanges();
                return Ok();
            }
            //NIE JE tento zamestnanec pridany v tomto projekte
            else
            {
                return BadRequest(ZamestnanciController._errNotFoundMSG);
            }
        }
    }
}
