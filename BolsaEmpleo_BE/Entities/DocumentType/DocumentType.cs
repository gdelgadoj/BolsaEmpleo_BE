using System.Text.Json.Serialization;

namespace BolsaEmpleo_BE.Entities.DocumentType
{
    public class DocumentType
    {
        public required int ID { get; set; }
        [JsonPropertyName("document_type_name")]
        public required string Document_Type_Name { get; set; }
    }
}
