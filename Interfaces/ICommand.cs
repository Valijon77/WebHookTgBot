using Telegram.Bot;
using Telegram.Bot.Types;

namespace WebHookTgBot.Interfaces;

public interface ICommand
{
    TelegramBotClient Client { get; }
    string Name{ get; }
    Task Execute(Update update);
}
