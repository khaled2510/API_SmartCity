using System;
namespace model
{
    public class BusinessException:Exception
    {
        public BusinessException(string message)
            :base(message)
        {
        }
    }
}