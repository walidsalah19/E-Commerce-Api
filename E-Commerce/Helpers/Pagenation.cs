namespace E_Commerce.Helpers
{
    public static class Pagenation
    {
        public static async Task<List<T>> PaginationFun<T>(this List<T> data,int page = 1, int pageSize = 10)
        {
            var totalItems = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pageDate = data.Skip((page - 1) * pageSize).Take(pageSize);

            return pageDate.ToList();
        }
    }
}
