using BolsaEmpleo_BE.Data;
using BolsaEmpleo_BE.Entities.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BolsaEmpleo_BE.Controllers
{
    [Route("api/vacancy")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly DataContext _context;

        public VacancyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vacancy>>> GetAllVacancies()
        {
            var vacancies = await _context.Vacancies.ToListAsync();
            if (vacancies.IsNullOrEmpty()) return NotFound("Vacantes no encontradas");

            return Ok(vacancies);
        }
    }
}
