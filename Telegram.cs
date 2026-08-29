namespace Summary.Telegram
{
    public class Telegram
    {
        internal class Features
        {
            internal const string Telegram = "Summary.Telegram";
            internal const string SendMessageText = "Summary.Telegram.SendMessageTextInTelegramTask";
            internal const string SendMessageDocument = "Summary.Telegram.SendMessageDocumentInTelegramTask";
        }

        internal class Workflows
        {
            internal const string Done = "Done";
            internal const string Failed = "Failed"; 
        }

        internal class Public
        {
            internal const string Category = "Telegram";
        }

        public class Localize
        {
            public const string SubjectOfTelegram = "تلگرام ربات";
            public const string DescriptionOfTelegram = "مجموعه ای از رخداد و تسک‌ها جهت ارتباط با ربات تلگرام.";

            public const string SubjectOfSendMessageText = "ارسال پیام متنی";
            public const string DescriptionOfSendMessageText = "فعالیتی جهت ارسال پیام متنی از طریق ربات تلگرام.";

            public const string SubjectOfSendMessageDocument = "ارسال پیام سند";
            public const string DescriptionOfSendMessageDocument = "فعالیتی جهت ارسال پیام سند از طریق ربات تلگرام.";
        }
    }
}