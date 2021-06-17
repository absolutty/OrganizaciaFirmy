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
    [Route("firma/divizie/{idDivizie}/{controller}")]
    [ApiController]
    public class ProjektyController : ControllerBase
    {
        public static readonly string _errEmptyDb = "/Zoznam projektov je prazdny/"; //databaza neobsahuje ziadne projekty
        public static readonly string _errProjektNotFound = "/Tento projekt nebol najdeny/"; //databaza neobsahuje taketo oddelenie

        private ApiDbContext _dbcontext;

        public ProjektyController(ApiDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetProjektyDivizie(int idDivizie)
        {
            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errProjektNotFound); }

            DbSet<Projekt> projekty = _dbcontext.ZoznamProjektov; //zoznam vsetkych projektov
            IEnumerable<Projekt> query = projekty.AsQueryable().Where(p => p.IdDivizie == idDivizie);

            return (query.Any()) ? Ok(query) : Ok(_errEmptyDb);
        }

        [HttpGet("{idProjektu}/zamestnanciProjektu")]
        public IActionResult GetZamestnancovVProjekte(int idDivizie, int idProjektu)
        {
            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ((proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errProjektNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errProjektNotFound); }

            DbSet<Zamestnanec> zamestnanci = _dbcontext.ZoznamZamestnancov; //zoznam vsetkych projektov
            IEnumerable<Zamestnanec> query = zamestnanci.AsQueryable().Where(z => z.ProjektId == idProjektu);

            return (query.Any()) ? Ok(query) : Ok(ZamestnanciController._errEmptyDb);
        }

        [HttpPut("{idProjektu}")]
        public IActionResult PridajDoProjektu(int idDivizie, int idProjektu, [FromForm] int idZamestnanca)
        {
            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ((proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errProjektNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errProjektNotFound); }

            Zamestnanec zam = null;
            try { zam = Zamestnanec.existuje(_dbcontext, idZamestnanca); }
            catch (ItemNenajdenyException ex) { return BadRequest(ex.Message); }

            zam.DiviziaId = diviz.Id; //zamestnanec priradeny do divizie
            zam.ProjektId = proj.Id;  //zamestnanec priradeny do projektu

            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idProjektu}")]
        public IActionResult OdstranZProjektu(int idDivizie, int idProjektu, [FromForm] int idZamestnanca)
        {
            Projekt proj = (Projekt)ModelVzor.Existuje<Projekt>(_dbcontext, idProjektu);
            //dany projekt nebol najdeny
            if ((proj == null) || (proj.IdDivizie != idDivizie)) { return BadRequest(_errProjektNotFound); }

            Divizia diviz = (Divizia)ModelVzor.Existuje<Divizia>(_dbcontext, idDivizie);
            //dana divizia nebola najdena 
            if (diviz == null) { return BadRequest(_errProjektNotFound); }

            Zamestnanec zam = null;
            try { zam = Zamestnanec.existuje(_dbcontext, idZamestnanca); }
            catch (ItemNenajdenyException ex) { return BadRequest(ex.Message); }

            //JE tento zamestnanec pridany v tomto projekte
            if (zam.ProjektId == proj.Id)
            {
                zam.ProjektId = 0;
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
