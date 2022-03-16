using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.ViewModels.Error;

namespace TotallyNotOLX.Controllers
{
    public class ErrorController:Controller
    {
        [Route("error/{statusCode}")]
        ///<summary>
        ///This method is used to descripe the most commmon types of errors that can appear.
        /// </summary>
        /// <param name="statusCode"> This is the number of the error.</param>
        public IActionResult StatusCodeError(int statusCode)
        {
            StatusCodeErrorViewModel error = new StatusCodeErrorViewModel();
            error.ErrorCode = statusCode.ToString() ;
            switch (statusCode)
            {
                case 404:
                    error.ErrorDescription = "Sorry, the resource you requested could not be found.";
                    break;
                case 403:
                    error.ErrorDescription = "Page is forbidden!";
                    break;
                case 405:
                    error.ErrorDescription = "Not allowed.";
                    break;
                case 500:
                    error.ErrorDescription = "Internal Server Error";
                    break;
                case 505:
                    error.ErrorDescription = "HTTP Version Not Suppported";
                    break;
            }

            return View(error);
        }
        [Route("error/notfound")]
        ///<summary>
        ///This method returns message that the serached product cannot be found.
        /// </summary>
        public IActionResult NotFound(NotFoundErrorViewModel errorData)
        {
            return View(errorData);
        }     
    }
}
