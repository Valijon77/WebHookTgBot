using Telegram.Bot.Types;

namespace WebHookTgBot.Interfaces;

public interface ITelegramUpdateListener
{
    Task GetUpdate(Update update);
}