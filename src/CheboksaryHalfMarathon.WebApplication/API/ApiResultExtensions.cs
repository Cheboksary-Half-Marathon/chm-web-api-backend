using CheboksaryHalfMarathon.WebAplication.DTO;

namespace CheboksaryHalfMarathon.WebAplication.API
{
    public static class ApiResultExtensions
    {
        public static ApiResultT<T> ToApiResult<T>(this T dto)
        {
            return ApiResultT<T>.Succeeded(dto);
        }
        public static PageDto<T> ToPageDto<T>(
            this IEnumerable<T> items,
            int totalCount)
        {
            return new PageDto<T>()
            {
                Items = items.ToArray(),
                TotalCount = totalCount
            };
        }
    }
}
