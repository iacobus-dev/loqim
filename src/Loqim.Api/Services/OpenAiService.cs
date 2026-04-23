using Loqim.Api.Dtos.Chat;
using Loqim.Domain.Entities;

namespace Loqim.Api.Services;

public class OpenAiService : IOpenAiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OpenAiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GenerateReplyAsync(
        string systemPrompt,
        List<ConversationMessage> recentMessages,
        string currentUserMessage)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var messages = new List<object>
        {
            new { role = "system", content = systemPrompt }
        };

        foreach (var msg in recentMessages.TakeLast(10))
        {
            messages.Add(new
            {
                role = msg.Role,
                content = msg.Content
            });
        }

        messages.Add(new
        {
            role = "user",
            content = currentUserMessage
        });

        var requestBody = new
        {
            model = "gpt-4.1-mini",
            messages = messages,
            temperature = 0.4
        };

        var response = await _httpClient.PostAsJsonAsync(
            "https://api.openai.com/v1/chat/completions",
            requestBody);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<OpenAiChatCompletionResponse>();

        return json?.Choices?.FirstOrDefault()?.Message?.Content
               ?? "Desculpe, não consegui responder agora.";
    }
}
