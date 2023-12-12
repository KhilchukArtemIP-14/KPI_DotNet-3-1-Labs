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
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;

        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<Guid>> AddAsync(RequestDTO dto)
        {
            ValidationContext context = new ValidationContext(dto, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, validationResults, true))
            {
                string message = String.Format("Creating request dto error: {0}", string.Join(", ", validationResults.Select(res => res.ErrorMessage).ToList()));

                return BaseResponse<Guid>.ErrorResponse(message);
            }

            try
            {
                var result = await _repository.AddAsync(dto);
                return BaseResponse<Guid>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<Guid>.ErrorResponse($"Creating request exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<RequestDTO>> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (result == null) return BaseResponse<RequestDTO>.ErrorResponse("Deleting request error: element not found");
                return BaseResponse<RequestDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<RequestDTO>.ErrorResponse($"Deleting request exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<IEnumerable<RequestDTO>>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, string userId = null, bool? activeOnly = false)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BaseResponse<IEnumerable<RequestDTO>>.ErrorResponse($"Getting requests error: bad pagination");
            }

            try
            {
                var result = await _repository.GetAllAsync(pageNumber, pageSize, userId, activeOnly);
                return BaseResponse<IEnumerable<RequestDTO>>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<IEnumerable<RequestDTO>>.ErrorResponse($"Getting all requests exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<RequestDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                if (result == null) return BaseResponse<RequestDTO>.ErrorResponse("Getting request by ID error: element not found");
                return BaseResponse<RequestDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<RequestDTO>.ErrorResponse($"Getting request by ID exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<RequestDTO>> UpdateAsync(Guid id, RequestDTO dto)
        {
            ValidationContext context = new ValidationContext(dto, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, validationResults, true))
            {
                string message = String.Format("Updating request dto error: {0}", string.Join(", ", validationResults.Select(res => res.ErrorMessage).ToList()));

                return BaseResponse<RequestDTO>.ErrorResponse(message);
            }

            try
            {
                var result = await _repository.UpdateAsync(id, dto);
                if (result == null) return BaseResponse<RequestDTO>.ErrorResponse("Updating request error: element not found");
                return BaseResponse<RequestDTO>.OkResponse(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<RequestDTO>.ErrorResponse($"Updating request exception: {ex.Message}");
            }
        }
    }

}
