using BolsaEmpleo_BE.Data;
using BolsaEmpleo_BE.Entities.Citizen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data.Common;

namespace BolsaEmpleo_BE.Controllers
{
    [Route("api/citizen")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly DataContext _context;
        
        public CitizenController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCitizen([FromBody] CitizenDTO citizenDTO)
        {
            var checkinExistingDocument = await _context.Citizens
            .Where(citizen =>
                citizen.Document_Number == citizenDTO.Document_Number ||
                citizen.Email == citizenDTO.Email)
            .ToListAsync();
            Console.WriteLine("       ");
            Console.WriteLine("checkinExistingDocument", JsonConvert.SerializeObject(checkinExistingDocument));
            Console.WriteLine("       ");

            if (checkinExistingDocument.Any()) return BadRequest("El ciudadano que está tratando de añadir ya existe");

            Citizen citizen = new()
            {
                Document_Number = citizenDTO.Document_Number,
                Document_Type_Id = citizenDTO.Document_Type_Id,
                Name = citizenDTO.Name,
                Surname = citizenDTO.Surname,
                Second_Surname = citizenDTO.Second_Surname,
                Birth_Date = citizenDTO.Birth_Date,
                Profession = citizenDTO.Profession,
                Salary_Expectation = citizenDTO.Salary_Expectation,
                Email = citizenDTO.Email,
           };
            
            try
            {
                Console.WriteLine("Before Add: Entity entries being tracked - Count: " + _context.ChangeTracker.Entries().Count());
                _context.Citizens.Add(citizen);
                Console.WriteLine("After Add: Entity entries being tracked - Count: " + _context.ChangeTracker.Entries().Count());
                await _context.SaveChangesAsync();
            } 
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e);
            }
            return Ok(await _context.Citizens.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Citizen>>> GetAllCitizens() 
        {
            var citizens = await _context.Citizens.ToListAsync();
            if(citizens.IsNullOrEmpty()) return NotFound("Ciudadanos no encontrados");

            return Ok(citizens);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCitizen([FromBody] CitizenDTO citizenDTO)
        {
            var citizenFromDB = await _context.Citizens.FindAsync(citizenDTO.Document_Number);
            if (citizenFromDB is null) return BadRequest("Ciudadano no encontrado");
            try
            {
                citizenFromDB.Document_Number = citizenDTO.Document_Number;
                citizenFromDB.Document_Type_Id = citizenDTO.Document_Type_Id;
                citizenFromDB.Name = citizenDTO.Name;
                citizenFromDB.Surname = citizenDTO.Surname;
                citizenFromDB.Second_Surname = citizenDTO.Second_Surname;
                citizenFromDB.Birth_Date = citizenDTO.Birth_Date;
                citizenFromDB.Profession = citizenDTO.Profession;
                citizenFromDB.Salary_Expectation = citizenDTO.Salary_Expectation;
                citizenFromDB.Email = citizenDTO.Email;
                await _context.SaveChangesAsync();
            }
            catch (DbException e)
            {
                return BadRequest(e);
            }
            return Ok(await _context.Citizens.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizen(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen is null) return BadRequest("Ciudadano no encontrado");
            try
            {
                _context.Citizens.Remove(citizen);
                await _context.SaveChangesAsync();
            }
            catch (DbException e)
            {
                return BadRequest(e);
            }
            return Ok(await _context.Citizens.ToListAsync());
        }

        [HttpPatch]
        public async Task<IActionResult> AssignVacancy(CitizenAssignmentPatchRequest citizenAssignmentPatchRequest)
        {       
            var citizen = await _context.Citizens.FindAsync(citizenAssignmentPatchRequest.UserID);
            if (citizen is null) return BadRequest("Ciudadano no encontrado");
            try
            {
                citizen.VacancyID = citizenAssignmentPatchRequest.VacancyID;
                await _context.SaveChangesAsync();
            }
            catch (DbException e)
            {
                return BadRequest(e);
            }
            return Ok(await _context.Citizens.ToListAsync());
        }
    }
}
