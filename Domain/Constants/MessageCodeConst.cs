namespace Domain.Constants
{
    public static class MessageCodeConst
    {
        private static readonly string GeneralCodePrefix = "GC-0-";
        private static readonly string SuccessCodePrefix = "SC-1-";
        private static readonly string ErrorCodePrefix = "EC-2-";
        private static readonly string WarningCodePrefix = "WC-3-";
        private static readonly string InformationCodePrefix = "IC-4-";

        public static class Success
        {
            public static readonly string OK = SuccessCodePrefix + "0";
            public static readonly string Get = SuccessCodePrefix + "1";
            public static readonly string Created = SuccessCodePrefix + "2";
            public static readonly string Edited = SuccessCodePrefix + "3";
            public static readonly string Deleted = SuccessCodePrefix + "4";
        }

        public static class Error
        {
            public static readonly string NotOK = ErrorCodePrefix + "0";
            public static readonly string Get = ErrorCodePrefix + "1";
            public static readonly string Created = ErrorCodePrefix + "2";
            public static readonly string Edited = ErrorCodePrefix + "3";
            public static readonly string Deleted = ErrorCodePrefix + "4";
            public static readonly string NotFound = ErrorCodePrefix + "5";
            public static readonly string UnAuthorize = ErrorCodePrefix + "6";
            public static readonly string Forbidden = ErrorCodePrefix + "7";
        }
    }
}