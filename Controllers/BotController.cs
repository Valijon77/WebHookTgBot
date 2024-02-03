using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using WebHookTgBot.Services;

namespace WebHookTgBot.Controllers;

[ApiController]
[Route("/")]
public class BotController : ControllerBase
{
    private TelegramBotClient bot = Bot.GetTelegramBot();
    private readonly UpdateDistributor<CommandExecutor> _updateDistributor;

    public BotController(UpdateDistributor<CommandExecutor> updateDistributor)
    {
        _updateDistributor = updateDistributor;
    }

    [HttpPost]
    public async Task Post(Update update)
    {
        if (update.Message == null)
        {
            return;
        }

        await _updateDistributor.GetUpdate(update);
    }

    [HttpGet]
    public string Get()
    {
        return "Telegram bot was started.";
    }
}
