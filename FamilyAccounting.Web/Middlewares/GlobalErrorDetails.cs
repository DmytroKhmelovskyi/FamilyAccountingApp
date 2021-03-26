using System.Text.Json;

namespace FamilyAccounting.Web.Services
{
    internal class GlobalErrorDetails
    {
        public GlobalErrorDetails()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}