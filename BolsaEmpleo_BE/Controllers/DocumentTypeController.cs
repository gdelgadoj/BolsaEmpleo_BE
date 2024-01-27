using BolsaEmpleo_BE.Data;
using BolsaEmpleo_BE.Entities.DocumentType;
using BolsaEmpleo_BE.Entities.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BolsaEmpleo_BE.Controllers
{
    [Route("api/document-types")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentTypeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentType>>> GetAllVacancies()
        {
            var documentTypes = await _context.Document_Types.ToListAsync();
            if (documentTypes.IsNullOrEmpty()) return NotFound("Vacantes no encontradas");

            return Ok(documentTypes);
        }
    }
}
