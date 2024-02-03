using Telegram.Bot.Types;
using WebHookTgBot.Services;

namespace WebHookTgBot.Interfaces;

public interface IListener
{
    CommandExecutor Executor { get; }
    Task GetUpdate(Update update);
}