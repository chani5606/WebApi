using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;
using ProjectFinal.Dto;

namespace ProjectAPI.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserServices> _logger;

        public UserServices(
            IUserRepository repository,
            IMapper mapper,
            ITokenService tokenService,
            IConfiguration configuration,
            ILogger<UserServices> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<UserResponseDTO?> CreateUser(UserCreateDTO d)
        {
            _logger.LogInformation("Creating user with email {Email}", d.Email);

            if (await _repository.EmailExists(d.Email))
            {
                _logger.LogWarning("Email {Email} is already in use", d.Email);
                throw new Exception("Email already in use");
            }

            var createUser = new User
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Password = HashPassword(d.Password),
                Phone = d.Phone,
                City = d.City,
                Nieghbrhood = d.Nieghbrhood,
                Street = d.Street
            };

            var user = await _repository.CreateUser(createUser);
            _logger.LogInformation("User created successfully with ID {UserId}", user.Id);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<List<UserResponseDTO>> GetAllUsers()
        {
            _logger.LogInformation("Retrieving all users");
            var ListUsers = await _repository.GetAllUsers();
            return ListUsers.Select(d => _mapper.Map<UserResponseDTO>(d)).ToList();
        }

        public async Task<UserResponseDTO?> GetUserById(int id)
        {
            _logger.LogInformation("Retrieving user by ID {UserId}", id);
            var user = await _repository.GetUsersByID(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return null;
            }
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> UpdateUser(UserUpdateDTO d, int id)
        {
            _logger.LogInformation("Updating user with ID {UserId}", id);
            var existingUser = await _repository.GetUsersByID(id);
            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return null;
            }

            if (d.Email != null && d.Email != existingUser.Email)
            {
                if (await _repository.EmailExists(d.Email))
                {
                    _logger.LogWarning("Email {Email} is already registered", d.Email);
                    throw new ArgumentException($"Email {d.Email} is already registered.");
                }
                existingUser.Email = d.Email;
            }

            if (d.FirstName != null) existingUser.FirstName = d.FirstName;
            if (d.LastName != null) existingUser.LastName = d.LastName;
            if (d.Phone != null) existingUser.Phone = d.Phone;
            if (d.City != null) existingUser.City = d.City;
            if (d.Nieghbrhood != null) existingUser.Nieghbrhood = d.Nieghbrhood;
            if (d.Street != null) existingUser.Street = d.Street;

            var updateUser = await _repository.UpdateGifts(existingUser);
            _logger.LogInformation("User with ID {UserId} updated successfully", id);
            return _mapper.Map<UserResponseDTO>(existingUser);
        }

        public async Task<bool> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user with ID {UserId}", id);
            var result = await _repository.DeleteUser(id);
            if (result)
                _logger.LogInformation("User with ID {UserId} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete user with ID {UserId}", id);

            return result;
        }

        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<AuthResponseDTO?> Authenticate(string email, string password)
        {
            _logger.LogInformation("Authenticating user with email {Email}", email);
            var user = await _repository.GetByEmail(email);
            if (user == null)
            {
                _logger.LogWarning("Authentication failed: user with email {Email} not found", email);
                return null;
            }

            var hashedPassword = HashPassword(password);
            if (user.Password != hashedPassword)
            {
                _logger.LogWarning("Authentication failed: invalid password for user {Email}", email);
                return null;
            }

            var token = _tokenService.GenerateToken(user.Id, user.Email, user.FirstName, user.LastName, user.Role);
            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes", 60);

            _logger.LogInformation("User {UserId} authenticated successfully", user.Id);

            return new AuthResponseDTO
            {
                Token = token,
                //TokenType = "Bearer",
                //ExpiresIn = expiryMinutes * 60,
                //User = _mapper.Map<UserResponseDTO>(user)
            };
        }
    }
}
