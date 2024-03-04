using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ToDoApp.UI.Models;

namespace ToDoApp.UI.Services
{
    

    public class ToDoAppService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContext;

        public ToDoAppService(HttpClient httpClient, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://todoapp.api:5001/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContext.HttpContext.Session.GetString("JWTToken"));

        }

        public async Task<LoginResponseDto> Login(LoginViewModel model)
        {
            var loginData = new { username = model.Username, password = model.Password };
            var response = await _httpClient.PostAsJsonAsync("v1/account/login", loginData);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            else return null;

            throw new Exception($"{response.StatusCode} Code: An error occured!");
        }

        public async Task<PaginatedList<ToDoListDto>> GetDoDoLists()
        {
            var response = await _httpClient.GetAsync("todolist");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<PaginatedList<ToDoListDto>>();

            throw new Exception($"{response.StatusCode} Code: An error occured!");
        }

        public async Task<bool> DeleteToDoList(int id) 
        {
            var response = await _httpClient.DeleteAsync($"todolist/{id}");

            if (response.IsSuccessStatusCode)
                return response.StatusCode == HttpStatusCode.NoContent
                    ? true
                    : throw new Exception($"{response.StatusCode} Code: An error occured!");

            throw new Exception($"{response.StatusCode} Code: An error occured!");
        }

        public async Task<bool> CreateToDoList(CreateToDoListDto createToDoListDto)
        {
            var response = await _httpClient.PostAsJsonAsync("todolist", createToDoListDto);

            if (response.IsSuccessStatusCode)
                return response.StatusCode == HttpStatusCode.OK
                    ? true
                    : throw new Exception($"{response.StatusCode} Code: An error occured!");

            throw new Exception($"{response.StatusCode} Code: An error occured!");
        }
    }
}
