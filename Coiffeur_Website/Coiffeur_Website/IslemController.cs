using Coiffeur_Website.Data;
using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Coiffeur_Website
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslemController : ControllerBase
    {

        private readonly CoiffeurDbContext dbContext;

        public IslemController(CoiffeurDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllIslem()
        {
            var allIslem = (from islem in dbContext.Islemler
                            select islem).ToList();
            return Ok(allIslem);
        }

        [HttpPost]
        public IActionResult AddIslem(AddIslem addIslem)
        {
            

            var IslemEntity = new Islem()
            {
                IslemAdi = addIslem.IslemAdi,
                Sure = addIslem.Sure,
                Ucret = addIslem.Ucret,
            };

            dbContext.Islemler.Add(IslemEntity);
            dbContext.SaveChanges();

            return Ok(IslemEntity);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetIslemById(int id)
        {
            var islem = (from i in dbContext.Islemler
                         where i.IslemId == id
                         select i).FirstOrDefault();

            if (islem is null)
            {
                return NotFound();
            }

            return Ok(islem);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateIslem(int id, UpdateIslem updateIslem)
        {
            var islem = (from i in dbContext.Islemler
                         where i.IslemId == id
                         select i).FirstOrDefault();

            if (islem is null)
            {
                return  NotFound();
            }

            islem.IslemAdi= updateIslem.IslemAdi;
            islem.Sure= updateIslem.Sure;
            islem.Ucret= updateIslem.Ucret;

            dbContext.SaveChanges();
            return Ok(islem);
        }

    }
}
