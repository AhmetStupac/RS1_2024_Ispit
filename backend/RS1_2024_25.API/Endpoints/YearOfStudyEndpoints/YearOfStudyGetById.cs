using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Endpoints.YearOfStudyEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearOfStudyGetById(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("get/{id}")]
        public async Task<ActionResult<List<YOSGetResponse>>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            await db.AcademicYears.LoadAsync(cancellationToken);
            await db.MyAppUsers.LoadAsync(cancellationToken);

            return Ok
            (await db.YearsOfStudy.Where(yos => yos.StudentId == id).Select(yos => new YOSGetResponse
            {
                Id = yos.Id,
                StudentId = yos.StudentId,
                AkademskaGodinaId = yos.AkademskaGodinaId,
                AkademskaGodina = yos.AkademskaGodina.Description,
                GodinaStudija = yos.GodinaStudija,
                Obnova = yos.Obnova,
                DatumUpisa = DateOnly.FromDateTime(yos.DatumUpisa),
                Snimio = yos.Snimio != null ? yos.Snimio.Email : "",
            }).ToListAsync(cancellationToken));
                
        }

        public class YOSGetResponse
        {
            public int Id { get; set; }
            public int StudentId { get; set; }
            public int AkademskaGodinaId { get; set; }
            public string AkademskaGodina { get; set; }
            public int GodinaStudija { get; set; }
            public bool Obnova { get; set; }
            public DateOnly DatumUpisa { get; set; }
            public string Snimio { get; set; }
        }
    }
}
