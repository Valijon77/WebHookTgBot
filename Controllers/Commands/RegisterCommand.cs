using Telegram.Bot;
using Telegram.Bot.Types;
using WebHookTgBot.Interfaces;
using WebHookTgBot.Services;

namespace WebHookTgBot.Controllers.Commands;

public class RegisterCommand : ICommand, IListener
{
    public CommandExecutor Executor { get; }
    public TelegramBotClient Client => Bot.GetTelegramBot();
    public string Name => "/register";

    public RegisterCommand(CommandExecutor executor)
    {
        Executor = executor;
    }

    private string? phone = null;
    private string? name = null; // ?

    public async Task Execute(Update update)
    {
        long chatId = update.Message!.Chat.Id;

        Executor.StartListen(this); // I: means now we are listening for updates

        await Client.SendTextMessageAsync(chatId, "Type in your phone number:");
    }

    public async Task GetUpdate(Update update)
    {
        long chatId = update.Message!.Chat.Id;

        if (update.Message.Text == null)
            return;

        if (phone == null)
        {
            phone = update.Message.Text;
            await Client.SendTextMessageAsync(chatId, "Now type in your name:");
        }
        else
        {
            name = update.Message.Text;
            await Client.SendTextMessageAsync(
                chatId,
                $"Registration was successful! \n Your name 📛: {name}\n Your phone number 📱: {phone}"
            );
            Executor.StopListen();
        }
    }
}
