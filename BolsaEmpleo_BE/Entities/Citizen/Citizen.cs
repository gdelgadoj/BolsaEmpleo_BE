using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BolsaEmpleo_BE.Entities.Citizen
{
    public class Citizen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [JsonPropertyName("document_number")]
        public required string Document_Number { get; set; }
        [JsonPropertyName("document_type_id")]
        public required int Document_Type_Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        [JsonPropertyName("second_surname")]
        public required string Second_Surname { get; set; }
        [JsonPropertyName("birth_date")]
        public required DateOnly Birth_Date { get; set; }
        public required string Profession { get; set; }
        [JsonPropertyName("salary_expectation")]
        public required int Salary_Expectation { get; set; }
        public required string Email { get; set; }
        [JsonPropertyName("vacancy_id")]
        public int? VacancyID { get; set; }


    }

    public class CitizenAssignmentPatchRequest
    {
        [JsonPropertyName("user_id")]
        public int UserID { get; set; }
        [JsonPropertyName("vacancy_id")]
        public int VacancyID { get; set; }
    }
}
