using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Services;

namespace RS1_2024_25.API.Endpoints.YearOfStudyEndpoints
{
    [Route("yos")]
    public class AcademicYearGetAllEndpoint : MyEndpointBaseAsync.WithoutRequest.WithActionResult<List<AcademicYearGetResponse>>
    {
        private readonly ApplicationDbContext _db;

        public AcademicYearGetAllEndpoint(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("academic-years")]
        public override async Task<ActionResult<List<AcademicYearGetResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            return Ok(await _db.AcademicYears.Select(ay => new AcademicYearGetResponse
            {
                Id = ay.ID,
                Name = ay.Description
            }).ToListAsync(cancellationToken));
        }
    }

    public class AcademicYearGetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
