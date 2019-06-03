using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DocumentProcessing
{
    public class MappedPaginatedList<TResult> : List<TResult> where TResult : new()
    {
        public TResult ItemType { get; set; } = new TResult();
        
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public MappedPaginatedList(IEnumerable<TResult> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<MappedPaginatedList<TResult>> CreateAsync<TSource>(IQueryable<TSource> source, IMapper mapper, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var mappedItems = mapper.Map<IEnumerable<TResult>>(items);

            return new MappedPaginatedList<TResult>(mappedItems, count, pageIndex, pageSize);
        }
    }
}