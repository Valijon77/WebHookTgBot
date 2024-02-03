using Telegram.Bot;
using Telegram.Bot.Types;
using WebHookTgBot.Controllers.Commands;
using WebHookTgBot.Interfaces;

namespace WebHookTgBot.Services;

public class CommandExecutor : ITelegramUpdateListener
{
    private List<ICommand> _commands;
    private IListener? _listener = null;

    public CommandExecutor()
    {
        _commands = GetCommands();
    }

    public async Task GetUpdate(Update update)
    {
        if (_listener == null)
        {
            await ExecuteCommand(update);
        }
        else
        {
            await _listener.GetUpdate(update);
        }
    }

    public async Task ExecuteCommand(Update update)
    {
        Message msg = update.Message!;

        // if (msg.Text == null) // W: Added by myself. Why it was removed from article by author?
        //     return;

        foreach (var command in _commands)
        {
            if (command.Name == msg.Text)
            {
                await command.Execute(update);
            }
        }
    }

    public void StartListen(IListener newListener)
    {
        _listener = newListener;
    }

    public void StopListen()
    {
        _listener = null;
    }

    private List<ICommand> GetCommands()
    {
        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(ICommand).IsAssignableFrom(type))
            .Where(type => type.IsClass);

        List<ICommand> commands = new List<ICommand>();

        foreach (var type in types)
        {
            ICommand? command;
            if (typeof(IListener).IsAssignableFrom(type))
            {
                command = Activator.CreateInstance(type, this) as ICommand;
            }
            else
            {
                command = Activator.CreateInstance(type) as ICommand;
            }

            if (command != null)
            {
                commands.Add(command);
            }
        }

        return commands;
    }
}
