using Telegram.Bot;
using WebHookTgBot.Configurations;

namespace WebHookTgBot.Services;

public class Bot
{
    private static TelegramBotClient? _client { get; set; }

    public static TelegramBotClient GetTelegramBot()
    {
        if (_client != null)
        {
            return _client;
        }

        _client = new TelegramBotClient(BotConfig.Token);

        return _client;
    }
}
