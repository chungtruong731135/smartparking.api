namespace TD.WebApi.Application.Common.TelegramBot;

public interface ITelegramBotService : ITransientService
{
    Task SendAsync(MessageRequest request, CancellationToken ct);
}