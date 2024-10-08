﻿namespace ToDoApp.API.Authentication.Dtos
{
    public record LoginDto
    {
        [Required, MinLength(1)]
        public string Username { get; init; } = null!;

        [Required, MinLength(1)]
        public string Password { get; init; } = null!;
    }
}
