using Cars.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cars.Api.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static async Task<ActionResult<ServerResponse<T>>> HandleException<T>(this ControllerBase controller, Func<Task<T>> action)
        {
            try
            {
                var result = await action();
                return controller.Ok(new ServerResponse<T>
                {
                    HasError = false,
                    Data = result,
                });
            }
            catch (Exception ex)
            {
                var response = new ServerResponse<T>
                {
                    HasError = true,
                    Message = ex.Message
                };
                return controller.StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
