using System;
using System.Collections.Generic;
using System.Text;

namespace ODataClientApp.DataModel
{
    public class SecurityTemplate
    {
        public string metadata;
        public ResponseResult[] value;
    }

    public class ResponseResult
    {
        public string StatusCode;
        public string TotalFound;
        public string DateTime;
        public string Details;
    }

}
