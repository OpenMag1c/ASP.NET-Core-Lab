﻿namespace Business.DTO
{
    public class ConfirmEmailDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
