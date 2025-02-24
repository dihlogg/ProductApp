﻿using AutoMapper;
using System.Dynamic;
using WavesOfFoodDemo.Server.Dtos;
using WavesOfFoodDemo.Server.Dtos.CartDetails;
using WavesOfFoodDemo.Server.Entities;
using WavesOfFoodDemo.Server.Infrastructures;

namespace WavesOfFoodDemo.Server.Services
{
    public class CartInfoService : ICartInfoService
    {
        private readonly ILogger<CartInfoService> _logger;
        private readonly ICartInfoRepository _cartInfoRepository;
        private readonly ICartDetailsRepository _cartDetailsRepository;
        private readonly IMapper _mapper;

        public CartInfoService(
            ICartInfoRepository cartInfoRepository, 
            ILogger<CartInfoService> logger,
            IMapper mapper, 
            ICartDetailsRepository cartDetailsRepository)
        {
            _cartInfoRepository = cartInfoRepository;
            _logger = logger;
            _mapper = mapper;
            _cartDetailsRepository = cartDetailsRepository;
        }

        public async Task<bool> AddCartInfoAsync(CartInfoCreateDto cartInfoCreateDto)
        {
            try
            {
                var info = _mapper.Map<CartInfo>(cartInfoCreateDto);
                return await _cartInfoRepository.AddAsync(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool?> EditCartInfoAsync(CartInfoDto cartInfoDto)
        {
            try
            {
                var productInfo = await _cartInfoRepository.GetByIdAsync(cartInfoDto.Id);
                if (productInfo == null)
                {
                    return null;
                }
                var infoUpdate = _mapper.Map<CartInfo>(cartInfoDto);
                var result = await _cartInfoRepository.UpdateAsync(infoUpdate);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<CartInfoDto>> GetCartInfoDtosAsync()
        {
            try
            {
                var data = await _cartInfoRepository.GetAllAsync();
                return _mapper.Map<List<CartInfoDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool?> RemoveCartInfoDtosAsync(Guid id)
        {
            try
            {
                return await _cartInfoRepository.DeleteByKey(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<CartHistoryDto>> GetTransactions(Guid userId)
        {
            try
            {
                return await _cartInfoRepository.GetTransactions(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool?> PostCartDetailInfo(CartDetailInfoDto cartInfoCreateDto)
        {
            bool result = false;
            // update
            if (cartInfoCreateDto?.CartInfoId != null)
            {
                var cartInfo = await _cartInfoRepository.GetCartInfoDetail(cartInfoCreateDto.CartInfoId.Value);
                if (cartInfo == null)
                {
                    return null;
                }
                cartInfo.Status = cartInfoCreateDto.Status;
                //if(DateTime.TryParse(cartInfoCreateDto.DateOrder, out var dateOrder))
                //{
                //    cartInfo.DateOrder = dateOrder;
                //}
                //else
                //{
                //    cartInfo.DateOrder = DateTime.UtcNow;
                //}
                cartInfo.DateOrder = cartInfoCreateDto.DateOrder;
                // remove cart detail
                cartInfo.CartDetails.Clear();
                result = await _cartInfoRepository.UpdateAsync(cartInfo);
                foreach (var item in cartInfoCreateDto.CartDetailDtos)
                {
                    var cdt = new CartDetails()
                    {
                        ProductId = item.ProductId,
                        CartId = cartInfo.Id,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                    };
                    _cartDetailsRepository.Add(cdt);
                }
            }
            // create new gio hang
            else
            {
                var cartInfo = new CartInfo();
                cartInfo.Id = Guid.NewGuid();
                cartInfo.Status = cartInfoCreateDto.Status;
                cartInfo.DateOrder = cartInfoCreateDto.DateOrder;
                //if (DateTime.TryParse(cartInfoCreateDto.DateOrder, out var dateOrder))
                //{
                //    cartInfo.DateOrder = dateOrder;
                //}
                //else
                //{
                //    cartInfo.DateOrder = DateTime.UtcNow;
                //}
                cartInfo.UserId = cartInfoCreateDto.UserId;
                result = await _cartInfoRepository.AddAsync(cartInfo);
                foreach (var item in cartInfoCreateDto.CartDetailDtos)
                {
                    var cdt = new CartDetails()
                    {
                        ProductId = item.ProductId,
                        CartId = cartInfo.Id,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                    };
                    _cartDetailsRepository.Add(cdt);
                }
            }
            return result;
        }
    }
}
