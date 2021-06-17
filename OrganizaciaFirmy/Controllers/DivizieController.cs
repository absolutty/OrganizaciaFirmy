using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizaciaFirmy.Data;
using OrganizaciaFirmy.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrganizaciaFirmy.Controllers
{
    /**
     * funkcie:
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

        [HttpGet] 
        public IActionResult GetVsetkyDivizie()
        {
            IEnumerable<Divizia> listDivizii = _dbcontext.ZoznamDivizii.ToList();

            // v zozname su pridane nejake divizie:
            //    - AK ANO (list divizii)
            //    - AK NIE (chybovy vypis) 
            return (listDivizii.Any()) ? Ok(listDivizii) :  Ok(_errEmptyDb);
        }

        [HttpGet("{id}")] 
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

        [HttpPost]
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

        [HttpPut("{idDivizie}")]
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

        [HttpDelete("{idDivizie}")]
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

        [HttpPut("{idDivizie}/novyVeduci")]
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

        [HttpGet("{idDivizie}/veduci")] 
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
