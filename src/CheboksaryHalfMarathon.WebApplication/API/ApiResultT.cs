using Newtonsoft.Json;

namespace CheboksaryHalfMarathon.WebAplication.API
{
    public sealed class ApiResultT<T> : ApiResult
    {
        [JsonIgnore]
        public bool HasError => !ReferenceEquals(Error, null);
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; private set; }
        public bool ShouldSerializeData() { return !HasError; }
        private ApiResultT() : base() { }
        public static ApiResultT<T> Succeeded(T value)
        {
            if (!(value is bool) && EqualityComparer<T>.Default.Equals(value, default(T)))
                throw new ArgumentNullException(nameof(value));
            return new ApiResultT<T> { Data = value };
        }
        public new static ApiResultT<T> CreateFailed(string errorCode,
            string errorMessage, IEnumerable<ApiResult> errors)
        {
            return new ApiResultT<T>
            {
                Error = ApiError.CreateFailed(errorCode, errorMessage, errors)
            };
        }
        public static new ApiResultT<T> CreateFailed(string errorCode, string errorMessage)
        {
            return CreateFailed(errorCode, errorMessage,
                Enumerable.Empty<ApiResult>());
        }
        public static new ApiResultT<T> CreateFormatedFailed(string errorCode, string errorMessage,
            params object[] args)
        {
            return CreateFailed(errorCode,
                string.Format(errorMessage, args), Enumerable.Empty<ApiResult>());
        }
    }
}
