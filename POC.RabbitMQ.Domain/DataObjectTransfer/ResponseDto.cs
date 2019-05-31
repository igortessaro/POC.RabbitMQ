namespace POC.RabbitMQ.Domain.DataObjectTransfer
{
    public class ResponseDto<T> : IDto
    {
        public ResponseDto()
        {
        }

        public ResponseDto(bool success, T body)
            : this()
        {
            this.Success = success;
            this.Body = body;
        }

        public ResponseDto(bool success, T body, string[] infos, string[] warnings, string[] errors)
            : this()
        {
            this.Success = success;
            this.Body = body;
            this.Infos = infos;
            this.Warnings = warnings;
            this.Errors = errors;
        }

        public bool Success { get; set; }

        public T Body { get; set; }

        public string[] Infos { get; set; }

        public string[] Warnings { get; set; }

        public string[] Errors { get; set; }
    }
}
