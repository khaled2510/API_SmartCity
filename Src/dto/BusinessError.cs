using System;
namespace dto
{
    public class BusinessError
    {
        public string Message { get; set; }

        public BusinessError(string message){
            Message = message;
        }

        public BusinessError(){}
    }
}