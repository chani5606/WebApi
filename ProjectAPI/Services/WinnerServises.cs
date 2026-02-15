using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;

namespace ProjectAPI.Services
{
    public class WinnerServises : IWinnerServises
    {
        private readonly IWinnerRepository _winnerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WinnerServises> _logger;
        private readonly ISaleReapository _saleReapository;

        public WinnerServises(
            IWinnerRepository winnerRepository,
            IUserRepository userR,
            IGiftRepository giftR,
            IMapper mapper,
            ILogger<WinnerServises> logger,
            ISaleReapository saleReapository

            )
        {
            _winnerRepository = winnerRepository;
            _userRepository = userR;
            _giftRepository = giftR;
            _mapper = mapper;
            _logger = logger;
            _saleReapository = saleReapository;
        }

        public async Task<WinnerResponseDTO> Create(WinnerCreatedDTO w)
        {
            if ( await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to create winner while sale is closed");
                throw new InvalidOperationException("Sale is currently closed. Cannot create winner.");

            }
            _logger.LogInformation("Creating winner for UserId {UserId} and GiftId {GiftId}", w.UserId, w.GiftId);

            if (await _userRepository.GetUsersByID(w.UserId) == null)
            {
                _logger.LogWarning("User with ID {UserId} does not exist", w.UserId);
                throw new KeyNotFoundException("User does not exist");
            }

            if (await _giftRepository.GetGiftsByID(w.GiftId) == null)
            {
                _logger.LogWarning("Gift with ID {GiftId} does not exist", w.GiftId);
                throw new KeyNotFoundException("Gift does not exist");
            }

            var createWinner = _mapper.Map<Winner>(w);
            var Winner = await _winnerRepository.Create(createWinner);

            _logger.LogInformation("Winner created successfully with ID {WinnerId}", Winner.Id);

            return _mapper.Map<WinnerResponseDTO>(Winner);
        }

        public async Task<List<WinnerResponseDTO>> GetAll()
        {
           
            _logger.LogInformation("Retrieving all winners");
            var ListWinner = await _winnerRepository.GetAll();
            return ListWinner.Select(b => _mapper.Map<WinnerResponseDTO>(b)).ToList();
        }

        public async Task<WinnerResponseDTO?> GetById(int id)
        {
            if ( await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to get winner while sale is closed");
                throw new InvalidOperationException("Sale is currently closed. Cannot get winner.");

            }
            _logger.LogInformation("Retrieving winner(s) by ID {WinnerId}", id);
            var w = await _winnerRepository.GetById(id);
            if (w == null)
            {
                _logger.LogWarning("No winner(s) found with ID {WinnerId}", id);
                return null;
            }

            return _mapper.Map<WinnerResponseDTO>(w);
        }
    }
}
