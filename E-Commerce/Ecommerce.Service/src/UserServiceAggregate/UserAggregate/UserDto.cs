using System.Net.Mail;
using System.Text.RegularExpressions;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.UserAggregate
{
    public class UserReadDto : BaseReadDto<User>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Is_Admin { get; set; } = false;
        public bool Is_Deleted { get; set; } = false;

        public override void FromEntity(User entity)
        {
            Email = entity.Email;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            PhoneNumber = entity.PhoneNumber;
            Is_Admin = entity.IsAdmin;
            Is_Deleted = entity.IsDeleted;
            base.FromEntity(entity);
        }
    }

    public class UserCreateDto : ICreateDto<User>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public byte[]? Salt { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; } = false;

        public void ToEntity(User entity)
        {
            entity.Email = Email;
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.Password = Password;
            entity.PhoneNumber = PhoneNumber;
            entity.Salt = Salt;
            entity.IsAdmin = IsAdmin;
            entity.IsDeleted = false;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class UserUpdateDto : IUpdateDto<User>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }

        public void UpdateEntity(User entity)
        {
            entity.FirstName = FirstName ?? entity.FirstName;
            entity.LastName = LastName ?? entity.LastName;
            entity.Password = Password ?? entity.Password;
            entity.PhoneNumber = PhoneNumber ?? entity.PhoneNumber;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class UserUpdateValidator : IDataValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(
                x => x.FirstName,
                firstName =>
                    firstName != null
                    && firstName.ToString().Length <= 10
                    && firstName.ToString().Length >= 3,
                "Name must be between 3 and 10 characters."
            );
            RuleFor(
                x => x.LastName,
                lastName =>
                    lastName != null
                    && lastName.ToString().Length <= 10
                    && lastName.ToString().Length >= 3,
                "Name must be between 3 and 10 characters."
            );

            RuleFor(
                x => x.Password,
                password => password != null && IsValidPassword(password.ToString()),
                "Password must be between 6 and 15 characters long and include at least one letter and one digit."
            );
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Regex pattern for password validation:
            // - Length between 6 and 15 characters
            // - At least one letter
            // - At least one digit
            var passwordRegex = @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{6,15}$";
            return Regex.IsMatch(password, passwordRegex);
        }
    }

    public class UserCreateValidator : IDataValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(
                x => x.FirstName,
                firstName =>
                    firstName != null
                    && firstName.ToString().Length <= 10
                    && firstName.ToString().Length >= 3,
                "Name must be between 3 and 10 characters."
            );
            RuleFor(
                x => x.LastName,
                lastName =>
                    lastName != null
                    && lastName.ToString().Length <= 10
                    && lastName.ToString().Length >= 3,
                "Name must be between 3 and 10 characters."
            );
            RuleFor(
                x => x.Email,
                email => email != null && IsValidEmail(email.ToString()),
                "Email must be a valid email address."
            );
            RuleFor(
                x => x.Password,
                password => password != null && IsValidPassword(password.ToString()),
                "Password must be between 6 and 15 characters long and include at least one letter and one digit!"
            );
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            Regex compareValue = new(@"^(?=.*[0-9])(?=.*[a-z]).{6,15}$");
            return compareValue.IsMatch(password);
        }
    }
}
