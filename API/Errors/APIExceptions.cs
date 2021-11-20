
namespace API.Errors
{
    public class APIException
    {
		public APIException(int statusCode, string message = null, string details = null)
		{
			StatusCode = statusCode;
			Message = message;
			this.details = details;
		}

		public int StatusCode { get; set; }
        public string Message { get; set; }
        public string details { get; set; }
    }
}