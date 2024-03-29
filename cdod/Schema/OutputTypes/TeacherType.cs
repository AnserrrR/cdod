﻿using System.ComponentModel.DataAnnotations.Schema;
using cdod.Models;
using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class TeacherType
    {
        private readonly User _user;

        public TeacherType(User user)
        {
            _user = user;
        }

        [IsProjected]
        public int Id { get; set; }

        public string? FirstName => _user.Firstname;
        public string? LastName => _user.Lastname;
        public string? Patronymic => _user.Patronymic;
        public string? PhoneNumber => _user.PhoneNumber;
        public string? Email => _user.Email;
        public string? Password => _user.Password;
        public DateOnly? Birthday => _user.Birthday;
        public string? Address => _user.Address;
        public string? Education => _user.Education;
        public string? Inn => _user.Inn;
        public string? Snils => _user.Snils;
        public string? passportNo => _user.passportNo;
        public string? passportIssue => _user.passportIssue;
        public DateOnly? passportDate => _user.passportDate;
        public string? passportCode => _user.passportCode;
        public bool IsAdmin => _user.IsAdmin;

        public string? WorkPlace { get; set; }


        // ЖОРА  ДОБАВЬ ВАГЕ РАТЕ ИД и раскоменть в мутации учителя 52 строку 
        [IsProjected]
        public int? WageRateId { get; set; }

        public async Task<WageRateType?> WageRate([Service] WageRateDataLoader wageRateDataLoader)
        {
            if (WageRateId is int wageRateId)
            {
                var wageRate = await wageRateDataLoader.LoadAsync(wageRateId);
                return new WageRateType
                {
                    Id = wageRate.Id,
                    Rate = wageRate.Rate,
                };
            }

            return null;
        }

        [IsProjected]
        public int? PostId { get; set; }

        public async Task<string?> Post([Service] PostDataLoader postDataLoader)
        {
            if (PostId is int postId)
            {
                var post = await postDataLoader.LoadAsync(postId);
                return post.Name;
            }

            return null;
        }
    }
}
