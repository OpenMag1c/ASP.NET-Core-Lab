using Business.DTO;

namespace WebAPI.Tests.Extensions
{
    public static class UserControllerTestData
    {
        public static UserDTO _validUserDTO = new()
        {
            Age = 18,
            AddressDelivery = "Minsk",
            Email = "testing.email@gmail.com",
            PhoneNumber = "375444706068",
            UserName = "admin"
        };
    }
}
