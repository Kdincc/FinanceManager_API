﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.ExpenseTypes;
using Task11.Application.ExpenseTypes.Commands.Create;
using Task11.Application.ExpenseTypes.Commands.Delete;
using Task11.Application.ExpenseTypes.Commands.Update;
using Task11.Application.ExpenseTypes.Queries.GetExpenseTypes;
using Task11.Contracts.ExpenseType;
using Task11.Presentation.ApiRoutes;

namespace Task11.Presentation.Controllers
{
    public sealed class ExpenseTypesController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet(Routes.ExpenseType.GetAll)]
        [ProducesResponseType<IEnumerable<ExpenseTypesResult>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<ExpenseTypesResult> results = await _sender.Send(new GetExpenseTypesQuery(), cancellationToken);

            return Ok(results);
        }

        [HttpPost(Routes.ExpenseType.Create)]
        [ProducesResponseType<ExpenseTypesResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateExpenseType(CreateExpenseTypeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateExpenseTypeCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpPut(Routes.ExpenseType.Update)]
        [ProducesResponseType<ExpenseTypesResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateExpenseType(UpdateExpenseTypeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateExpenseTypeCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpDelete(Routes.ExpenseType.Delete)]
        [ProducesResponseType<ExpenseTypesResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteExpenseType(DeleteExpenseTypeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteExpenseTypeCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }

    }
}
