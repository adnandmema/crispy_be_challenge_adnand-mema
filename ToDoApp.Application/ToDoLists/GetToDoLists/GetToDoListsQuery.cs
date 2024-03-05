using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Common.Mapping;
using ToDoApp.Application.Common.Models;

namespace ToDoApp.Application.ToDoLists.GetToDoLists
{
    public record GetToDoListsQuery : IRequest<PaginatedList<ToDoListDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetToDoListsQueryHandler : IRequestHandler<GetToDoListsQuery, PaginatedList<ToDoListDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetToDoListsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ToDoListDto>> Handle(GetToDoListsQuery request, CancellationToken cancellationToken)
        {
            return await _context.ToDoLists
            .OrderBy(x => x.Id)
            .ProjectTo<ToDoListDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
