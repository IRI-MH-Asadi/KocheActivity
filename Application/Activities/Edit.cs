
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;
public class Edit
{
    public class Command : IRequest
    {
        public Activity Activity { get; set; }
    }

    class Handler : IRequestHandler<Command>
    {
        DataContext _context;
        private IMapper _mapper { get; }

        public Handler(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
            _mapper.Map(request.Activity, activity);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }


}
