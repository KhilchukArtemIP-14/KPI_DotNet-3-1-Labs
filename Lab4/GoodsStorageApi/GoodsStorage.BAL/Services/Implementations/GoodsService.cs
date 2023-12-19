using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.BAL.Models;

namespace GoodsStorage.BAL.Services.Implementations
{
    public class GoodsService : IGoodsService
    {
        private readonly IGoodRepository _repository;

        public GoodsService(IGoodRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse<Guid>> AddAsync(ModifyGoodDTO dto)
        {
            ValidationContext context = new ValidationContext(dto, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, validationResults, true))
            {
                string message = String.Format("Creating good dto error: {0}", string.Join(", ", validationResults.Select(res => res.ErrorMessage).ToList()));

                return BaseResponse<Guid>.ErrorResponse(message);
            }

            try
            {
                var result = await _repository.AddAsync(dto);
                return BaseResponse<Guid>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<Guid>.ErrorResponse($"Creating good exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<GoodDTO>> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (result == null) return BaseResponse<GoodDTO>.ErrorResponse("Deleting good error: element not found");
                return BaseResponse<GoodDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<GoodDTO>.ErrorResponse($"Deleting good exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<IEnumerable<GoodDTO>>> GetAllAsync(int pageNumber = 1, int pageSize = 5)
        {
            if(pageNumber < 1 || pageSize < 1) return BaseResponse<IEnumerable<GoodDTO>>.ErrorResponse($"Getting goods error: bad pagination");
            try
            {
                var result = await _repository.GetAllAsync(pageNumber,pageSize);
                return BaseResponse<IEnumerable<GoodDTO>>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<IEnumerable<GoodDTO>>.ErrorResponse($"Getting all exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<GoodDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                if (result == null) return BaseResponse<GoodDTO>.ErrorResponse("Getting good by ID error: element not found");
                return BaseResponse<GoodDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<GoodDTO>.ErrorResponse($"Getting good by ID exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<GoodDTO>> UpdateAsync(Guid id, ModifyGoodDTO dto)
        {
            ValidationContext context = new ValidationContext(dto, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, validationResults, true))
            {
                string message = String.Format("Updating good dto error: {0}", string.Join(", ", validationResults.Select(res => res.ErrorMessage).ToList()));

                return BaseResponse<GoodDTO>.ErrorResponse(message);
            }

            try
            {
                var result = await _repository.UpdateAsync(id, dto);
                if (result == null) return BaseResponse<GoodDTO>.ErrorResponse("Updating good error: element not found");
                return BaseResponse<GoodDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<GoodDTO>.ErrorResponse($"Updating good exception: {ex.Message}");
            }
        }
    }
}
