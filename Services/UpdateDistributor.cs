using Telegram.Bot;
using Telegram.Bot.Types;
using WebHookTgBot.Interfaces;

namespace WebHookTgBot.Services;

public class UpdateDistributor<T>
    where T : ITelegramUpdateListener, new() // W: 'ITelegramUpdateListener'. Why not create one to differ from rest?
{
    private Dictionary<long, T> _listeners;

    public UpdateDistributor()
    {
        _listeners = new Dictionary<long, T>();
    }

    public async Task GetUpdate(Update update)
    {
        long chatId = update.Message!.Chat.Id;
        T? listener = _listeners.GetValueOrDefault(chatId);

        if (listener == null)
        {

            listener = new T();
            _listeners.Add(chatId, listener);
            await listener.GetUpdate(update);
            return;
        }

        await listener.GetUpdate(update);
    }
}
