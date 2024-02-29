using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmailSenderApp.Domain.Entites.Models
{
    public class EmailModel
    {
        [EmailAddress]
        public string To { get; set; }
        public string Subject { get; set; }

        [JsonIgnore]
        public string? Body { get; set; }
    }
}
