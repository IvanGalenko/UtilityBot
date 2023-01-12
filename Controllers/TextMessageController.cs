using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;
using UtilityBot.Operations;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        TextAction action = new TextAction();

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Количество" , $"count"),
                        InlineKeyboardButton.WithCallbackData($" Сумма" , $"summ")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот считает количество символов в строке, " +
                        $"либо сумму двух чисел через пробел.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Выберите опцию и отправьте сообщение.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    Console.WriteLine($"Контроллер {GetType().Name} получил сообщение {message.Text}");
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, action.GetText(_memoryStorage.GetSession(message.Chat.Id).OptionCode, message.Text), cancellationToken: ct);
                    break;
            }
        }
    }
}