namespace FastAPI.Layers.Infrastructure.Email;

internal class EmailConstants
{
    internal class ErrorMessages
    {
        public const string ReceiverEmailRequired = "Receiver email is required!";
        public const string SubjectRequired = "Email subject is required!";
        public const string NoReponse = "No response from the mail server!";
        public const string ApiKeyMissing = "Sender service authentication key is not provided!";
        public const string SenderEmailMissing = "Sender email address is not provided!";
        public const string SenderNameMissing = "Sender name is not provided!";
    }
}
