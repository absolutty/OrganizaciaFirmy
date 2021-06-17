using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizaciaFirmy.Data;
using OrganizaciaFirmy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrganizaciaFirmy.Controllers
{
    [Route("firma/[controller]")]
    [ApiController]
    public class ZamestnanciController : ControllerBase
    {
        public static readonly string _errNieSuRiaditelia = "/Táto firma nemá určeného riaditeľa/"; 
        public static readonly string _errEmptyDb = "/Zoznam zamestnancov je prazdny/"; //databaza neobsahuje ziadnych zamestnancov
        public static readonly string _errNotFoundMSG = "/Zamestanec nebol najdeny/"; //id tohto zamestnanca nie je v databaze
        public static readonly string _errNullAtr = "/NOT NULL atr nemozu byt null/"; //niektore NOT NULL collums su null
        public static readonly string _errZaporneId = "/Zadane id nemoze byt zaporne/"; //niektore zo zadanych id je zaporne

        private ApiDbContext _dbcontext;
        public ZamestnanciController(ApiDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet] 
        public IActionResult GetVsetkychZamestnancov()
        {
            IEnumerable<Zamestnanec> listZamestnancov = _dbcontext.ZoznamZamestnancov.ToList();

            //minimalne jeden zamestnanec je pridany
            if (listZamestnancov.Count() > 0)
            {
                return Ok(listZamestnancov);
            }
            //nie je pridany ani jeden zamestnanec
            else
            {
                return Ok(_errEmptyDb);
            }
        }

        [HttpGet("{id}")] 
        public IActionResult GetKonkretnehoZamestnanca(int id)
        {
            Zamestnanec z = _dbcontext.Find<Zamestnanec>(id);

            return (z != null) ? Ok(z) : NotFound(_errNotFoundMSG);
        }

        [HttpGet("riaditelFirmy")]
        public IActionResult GetRiaditelovFirmy() {
            DbSet<Zamestnanec> zamestnanci = _dbcontext.ZoznamZamestnancov;

            IEnumerable<Zamestnanec> zoznamRiaditelov = zamestnanci.AsQueryable().Where(zam => (zam.jeRiaditelom == true));

            return zoznamRiaditelov.Any() ? (Ok(zoznamRiaditelov)) : (Ok(_errNieSuRiaditelia));
        }

        [HttpPost]
        public IActionResult PridajDoDB([FromBody] Zamestnanec novyZamestnanec)
        {
            try
            {
                _dbcontext.Add(novyZamestnanec);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DbUpdateException ex) //ak NOT NULL collumns nie su splnene, zamestnanca neprida do db
            {
                return BadRequest(_errNullAtr + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpravVDP(int id, [FromBody] Zamestnanec zamestnanec)
        {
            Zamestnanec upravovanyZamestnanec = _dbcontext.Find<Zamestnanec>(id);

            if (upravovanyZamestnanec != null) //zamestnanec BOL najdeny
            {
                try
                {
                    upravovanyZamestnanec.upravHodnoty(zamestnanec);
                    _dbcontext.SaveChanges();

                    return Ok();
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(_errZaporneId);
                }

            }
            else //zamestnanec NEBOL najdeny
            {
                return NotFound(_errNotFoundMSG);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult VymazZDB(int id)
        {
            Zamestnanec odstranovanyZamestnanec = _dbcontext.Find<Zamestnanec>(id);

            if (odstranovanyZamestnanec != null) //zamestnanec BOL najdeny
            {
                _dbcontext.Remove<Zamestnanec>(odstranovanyZamestnanec);
                _dbcontext.SaveChanges();

                return Ok();
            }
            else //zamestnanec NEBOL najdeny
            {
                return NotFound(_errNotFoundMSG);
            }
        }
    }
}
