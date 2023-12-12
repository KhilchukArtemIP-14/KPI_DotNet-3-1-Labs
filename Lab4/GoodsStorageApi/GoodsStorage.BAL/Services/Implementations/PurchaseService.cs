using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;

        public PurchaseService(IPurchaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<Guid>> AddAsync(PurchaseDTO dto)
        {
            ValidationContext context = new ValidationContext(dto, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, validationResults, true))
            {
                string message = String.Format("Creating purchase dto error: {0}", string.Join(", ", validationResults.Select(res => res.ErrorMessage).ToList()));

                return BaseResponse<Guid>.ErrorResponse(message);
            }

            try
            {
                var result = await _repository.AddAsync(dto);
                return BaseResponse<Guid>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<Guid>.ErrorResponse($"Creating purchase exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<IEnumerable<PurchaseDTO>>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, string userId = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BaseResponse<IEnumerable<PurchaseDTO>>.ErrorResponse($"Getting purchases error: bad pagination");
            }

            try
            {
                var result = await _repository.GetAllAsync(pageNumber, pageSize, userId);
                return BaseResponse<IEnumerable<PurchaseDTO>>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<IEnumerable<PurchaseDTO>>.ErrorResponse($"Getting all purchases exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<PurchaseDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                if (result == null) return BaseResponse<PurchaseDTO>.ErrorResponse("Getting purchase by ID error: element not found");
                return BaseResponse<PurchaseDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<PurchaseDTO>.ErrorResponse($"Getting purchase by ID exception: {ex.Message}");
            }
        }
    }

}
