using Business.Interfaces;
using Data.Entities;
using DataAccess.Repositories;
using Org.BouncyCastle.Crypto.Generators;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int id) => _userRepository.GetById(id);

        public User GetUserByUsername(string username) => _userRepository.GetByUsername(username);

        public bool Register(string username, string password)
        {
            if (_userRepository.GetByUsername(username) != null)
                return false; // User already exists

            var user = new User
            {
                Username = username,
                Password = HashPassword(password) // Simple hashing for security
            };

            _userRepository.Add(user);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            return user != null && VerifyPassword(password, user.Password);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }
    }
}
