using TD.WebApi.Application.Common.Mailing;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using TD.WebApi.Application.Common.Interfaces;
using TD.WebApi.Application.Common.TelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;

namespace TD.WebApi.Infrastructure.TelegramBot;

public class TelegramBotService : ITransientService
{
    private readonly TelegramBotSettings _settings;
    private readonly ILogger<TelegramBotService> _logger;
    private readonly ITelegramBotClient _telegramBotClient;



  
    public TelegramBotService(IOptions<TelegramBotSettings> settings, ILogger<TelegramBotService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
        _telegramBotClient = new TelegramBotClient(settings.Value.Token!);


       
    }

 

    public async Task SendAsync(MessageRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _telegramBotClient.SendTextMessageAsync(
                      chatId: request.ChatId!,
                      text: request.Text!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}