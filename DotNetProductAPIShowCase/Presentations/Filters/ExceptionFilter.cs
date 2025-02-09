using System;
using DotNetProductAPIShowCase.Applications.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetProductAPIShowCase.Presentations.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private ErrorModel Error;

    public void OnException(ExceptionContext context)
    {

        //Can custom more base on each type of exception
        if (context.Exception is NotFoundException notFoundException)
        {
            this.Error = new ErrorModel
            (
                404,
                notFoundException.Message,
                notFoundException.StackTrace?.ToString()
            );
        }
        else if (context.Exception is BadRequestException badRequestException)
        {
            this.Error = new ErrorModel
            (
                400,
                badRequestException.Message,
                badRequestException.StackTrace?.ToString()
            );
        }
        else
        {
            this.Error = new ErrorModel
            (
                500,
                context.Exception.Message,
                context.Exception.StackTrace?.ToString()
            );
        }

        context.Result = new ObjectResult(this.Error) { StatusCode = this.Error.StatusCode };
    }
}
