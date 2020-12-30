using BusinessLogic.Services.Contracts;
using AutoMapper;
using BusinessLogic.Contracts.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        public BaseController(
            IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IActionResult ProcessOperationResult<T>(OperationResult<T> operationResult)
        {
            if (!operationResult.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, operationResult.GetErrors());
            }
            return Ok(operationResult.Result);
        }

        protected async Task<IActionResult> ProcessOperationResult<T>
                         (Func<Task<OperationResult<T>>> bllAction)
        {
            try
            {
                var operationResult = await bllAction.Invoke();
                if (!operationResult.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        operationResult.GetErrors());
                }

                return Ok(operationResult.Result);
            }
            catch (BusinessException e)
            {
                Log.Logger.Information("{Error}", e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.Message);
            }
            catch (EntityNotFoundException e)
            {
                Log.Logger.Information("{Error}", e);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                Log.Logger.Error("{Error}", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла непредвиденная ошибка. Обратитесь к администратору.");
            }

        }

        protected IActionResult ProcessOperationResult<T>(Func<OperationResult<T>> bllAction)
        {
            try
            {
                var operationResult = bllAction.Invoke();
                if (!operationResult.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, operationResult.GetErrors());
                }

                return Ok(operationResult.Result);
            }
            catch (BusinessException e)
            {
                Log.Logger.Information("{Error}", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                Log.Logger.Information("{Error}", e);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (ApplicationException e)
            {
                Log.Logger.Information("{Error}", e);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                Log.Logger.Error("{Error}", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла непредвиденная ошибка. Обратитесь к администратору.");
            }

        }
    }
}
