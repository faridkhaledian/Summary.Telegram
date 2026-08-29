using System.Runtime.ConstrainedExecution;
namespace Summary.Telegram
{
    public class BaseRequestInfo 
    {
        public string Token { get; set; }
    }

    public class BaseSendMessageRequestInfo : BaseRequestInfo
    {
        public string ChatId { get; set; }
    }

    public class SendMessageTextRequestInfo : BaseSendMessageRequestInfo
    {
        public string Text { get; set; }
    }

    public class SendMessageDocumentRequestInfo : BaseSendMessageRequestInfo
    {
        public string Document { get; set; }
        public string Caption { get; set; }
    }
}