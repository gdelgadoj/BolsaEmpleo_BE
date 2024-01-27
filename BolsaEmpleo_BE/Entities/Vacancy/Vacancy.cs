namespace BolsaEmpleo_BE.Entities.Vacancy
{
    public class Vacancy
    {
        public required int ID { get; set; }
        public required string Code { get; set; }

        public required string Position { get; set; }
        public required string Description { get; set; }
        public required string Company { get; set; }
        public required int Salary { get; set; }
    }
}
