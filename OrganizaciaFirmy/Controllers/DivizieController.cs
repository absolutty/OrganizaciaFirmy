using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizaciaFirmy.Data;
using OrganizaciaFirmy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaciaFirmy.Controllers
{
    /**
     * ukony:
     *      vratenie vsetkych divizii
     *      vratenie zamestnancov v konkretnej divizii
     *      pridanie novej divizie
     *      pridanie zamestnanca do divizie
     *      vymazanie zamestnanca z divizie
     *      nastavenie veduceho
     *      vymazanie veduceho
     */
    [Route("firma/[controller]")]
    [ApiController]
    public class DivizieController : ControllerBase
    {
        public static readonly string _errEmptyDb = "/Zoznam divizii je prazdny/"; //databaza neobsahuje ziadne divizie
        public static readonly string _errDiviziaNotFound = "/Divizia nebola najdena/"; //id tejto divizie nie je v databaze
        public static readonly string _errVDiviziiNotFound = "/V divizii sa takyto zamestnanec nenachadza/"; //v tejto divizii sa takyto zamestnanec nenachadza
        public static readonly string _errNullAtr = "/NOT NULL atr nemozu byt null/"; //niektore NOT NULL collums su null
        public static readonly string _errNemaVeducich = "/Tato divizia nema ziadneho veduceho/"; //id veduceho neexistuje

        private ApiDbContext _dbcontext;

        public DivizieController(ApiDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: firma/<ValuesController>
        [HttpGet] //vrati vsetky divizie
        public IActionResult GetVsetkyDivizie()
        {
            IEnumerable<Divizia> listDivizii = _dbcontext.ZoznamDivizii.ToList();

            // v zozname su pridane nejake divizie:
            //    - AK ANO (list divizii)
            //    - AK NIE (chybovy vypis) 
            return (listDivizii.Any()) ? Ok(listDivizii) :  Ok(_errEmptyDb);
        }

        // GET firma/<ValuesController>/5
        [HttpGet("{id}")] //vrati zamestnancov, kt. su v danej divizii
        public IActionResult GetZamestnancovVDivizii(int id)
        {
            Divizia d = _dbcontext.Find<Divizia>(id);

            if (d != null)
            {
                DbSet<Zamestnanec> zamestnanci = _dbcontext.ZoznamZamestnancov;

                IEnumerable<Zamestnanec> query = zamestnanci.AsQueryable().Where(zam => zam.DiviziaId == id);

                return Ok(query);
            }
            else {
                return NotFound(_errDiviziaNotFound);
            }
        }

        // POST firma/<ValuesController>
        [HttpPost] //prida novu diviziu 
        public IActionResult PridajDiviziu([FromBody] Divizia novaDivizia)
        {
            try
            {
                _dbcontext.Add(novaDivizia);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DbUpdateException ex) //ak NOT NULL collumns nie su splnene, zamestnanca neprida do db
            {
                return BadRequest(_errNullAtr + ex.Message);
            }
        }

        // PUT firma/<ValuesController>/5
        [HttpPut("{idDivizie}")]//prida zamestnanca do divizie na zaklade jeho id
        public IActionResult PridajZamestnancaDoDivizie(int idDivizie, [FromForm] int idZamestnanca)
        {
            Zamestnanec z = _dbcontext.Find<Zamestnanec>(idZamestnanca); //pridavany zamestnanec
            Divizia d = _dbcontext.Find<Divizia>(idDivizie); //do ktorej divizie je pridany

            if (z == null)
            { //zamestnanec nebol najdeny
                return BadRequest(ZamestnanciController._errNotFoundMSG);
            }
            else if (d == null) { //divizia nebola najdena
                return BadRequest(_errDiviziaNotFound);
            }
            
            z.DiviziaId = idDivizie; //zamestnanec ma pridelene nove Id
            _dbcontext.SaveChanges();

            return Ok();
        }

        // DELETE firma/<ValuesController>/5
        [HttpDelete("{idDivizie}")]//vymaze zamestnanca na zaklade jeho id
        public IActionResult VymazZDivizie(int idDivizie, [FromForm] int idZamestnanca)
        {
            Divizia d = _dbcontext.Find<Divizia>(idDivizie);
            Zamestnanec z = _dbcontext.Find<Zamestnanec>(idZamestnanca);

            if (d == null)
            { //divizia nebola najdena
                return BadRequest(_errDiviziaNotFound);
            } else if (z == null)
            { //zamestnanec nebol najdeny
                return BadRequest(ZamestnanciController._errNotFoundMSG);
            } else if (z.DiviziaId != idDivizie) 
            { //zamestnanec sa nenachadza v tejto divizii 
                return BadRequest(_errVDiviziiNotFound);
            }

            z.DiviziaId = 0;
            _dbcontext.SaveChanges();

            return Ok();
        }

        // PUT firma/<ValuesController>/5
        [HttpPut("{idDivizie}/novyVeduci")]//prida zamestnanca na poziciu veduceho na zaklade jeho id
        public IActionResult SetVeducehoDivizie(int idDivizie, [FromForm] int idZamestnanca)
        {
            Zamestnanec z = _dbcontext.Find<Zamestnanec>(idZamestnanca); //urcovany zamestnanec
            Divizia d = _dbcontext.Find<Divizia>(idDivizie); //do ktorej divizie je pridany

            if (z == null)
            { //zamestnanec nebol najdeny
                return BadRequest(ZamestnanciController._errNotFoundMSG);
            }
            else if (d == null)
            { //divizia nebola najdena
                return BadRequest(_errDiviziaNotFound);
            }

            d.IdVeducehoDivizie = idZamestnanca;
            _dbcontext.SaveChanges();

            return Ok();
        }

        // GET firma/<ValuesController>/5
        [HttpGet("{idDivizie}/veduci")] //vrati veduceho danej divizie
        public IActionResult GetVeduci(int idDivizie)
        {
            Divizia d = _dbcontext.Find<Divizia>(idDivizie);

            if (d != null)
            {
                DbSet<Zamestnanec> zamestnanci = _dbcontext.ZoznamZamestnancov;

                if (d.IdVeducehoDivizie != 0)
                { //MA nejakeho veduceho urceneho
                    return Ok(zamestnanci.AsQueryable().Where(zam => (zam.Id == d.IdVeducehoDivizie)));
                }
                else
                { //NEMA ziadneho veduceho urceneho
                    return Ok(_errNemaVeducich);
                }
            }
            else {
                return BadRequest(_errDiviziaNotFound);
            }
            
        }
    }
}
