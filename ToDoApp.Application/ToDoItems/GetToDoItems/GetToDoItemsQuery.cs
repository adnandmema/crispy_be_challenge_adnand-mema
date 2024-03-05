using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Common.Mapping;
using ToDoApp.Application.Common.Models;
using ToDoApp.Application.ToDoLists.GetToDoLists;

namespace ToDoApp.Application.ToDoItems.GetToDoItems
{
    public record GetToDoItemsQuery : IRequest<PaginatedList<ToDoItemDto>>
    {
        public int ListId { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, PaginatedList<ToDoItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetToDoItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ToDoItemDto>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _context.ToDoItems
                .Where(x => x.ToDoListId == request.ListId)
                .OrderBy(x => x.Id)
                .ProjectTo<ToDoItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
