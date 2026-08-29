using Core.Modules.Manifest;
using Summary.Telegram;

[assembly: Feature(
    Id = Telegram.Features.Telegram,
    Name = Telegram.Localize.SubjectOfTelegram,
    Description =Telegram.Localize.DescriptionOfTelegram,
    Category = Telegram.Public.Category,
    Dependencies = new[] { "Core.Workflows" },
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = Telegram.Features.SendMessageText,
    Name = Telegram.Localize.SubjectOfSendMessageText,
    Description =Telegram.Localize.DescriptionOfSendMessageText,
    Category = Telegram.Public.Category,
    Dependencies = new[] { Telegram.Features.Telegram },
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = Telegram.Features.SendMessageDocument,
    Name = Telegram.Localize.SubjectOfSendMessageDocument,
    Description =Telegram.Localize.DescriptionOfSendMessageDocument,
    Category = Telegram.Public.Category,
    Dependencies = new[] { Telegram.Features.Telegram },
    Version = "1.0.0"
)]