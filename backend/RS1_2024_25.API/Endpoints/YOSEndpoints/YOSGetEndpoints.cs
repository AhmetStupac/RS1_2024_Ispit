using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;

namespace RS1_2024_25.API.Endpoints.YOSEndpoints
{
    [Route("yos")]
    [ApiController]
    public class YOSGetEndpoints(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("get/{id}")] // dodaj admin permisije
        public async Task<ActionResult<List<YOSGetResponse>>> YOSGetById(int id,CancellationToken cancellationToken)
        {
            await db.AcademicYears.LoadAsync(cancellationToken);
            await db.MyAppUsers.LoadAsync(cancellationToken);

            var result = await db.YearsOfStudy.Where(yos => yos.StudentId == id).Select(yos => new YOSGetResponse
            { 
              Id = yos.Id,
              obnova = yos.Obnova,
              DatumUpisa = DateOnly.FromDateTime(yos.DatumUpisa),
              GodinaStudija = yos.GodinaStudija,
              Snimio =  yos.Snimio != null ? yos.Snimio.Email : "",
              AkademskaGodina = yos.AkademskaGodina != null ? yos.AkademskaGodina.Description : "",
               AkademskaGodinaId = yos.AkademskaGodinaId,
               StudentId = yos.StudentId
            }).ToListAsync(cancellationToken);

            return Ok(result);
        }

        public class YOSGetResponse
        {
            public int Id { get; set; }
            public DateOnly DatumUpisa { get; set; }
            public int GodinaStudija { get; set; }
            public int AkademskaGodinaId { get; set; }
            public string AkademskaGodina { get; set; } 
            public float cijenaSkolarine { get; set; }
            public bool obnova { get; set; }
            public int StudentId { get; set; }
            public string Snimio { get; set; }
        }
    }
}
