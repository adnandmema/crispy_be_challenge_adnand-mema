using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.Queries.GetHelloWorldToDoItem
{
    public record GetHelloWorldToDoItemQuery : IRequest<ToDoItemDto>
    {
    }

    public class GetHelloWorldToDoItemQueryHandler : IRequestHandler<GetHelloWorldToDoItemQuery, ToDoItemDto>
    {
        private readonly IMapper _mapper;

        public GetHelloWorldToDoItemQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ToDoItemDto> Handle(GetHelloWorldToDoItemQuery request, CancellationToken cancellationToken)
        {
            var item = new ToDoItem();
            return _mapper.Map<ToDoItemDto>(item);
        }
    }
}
