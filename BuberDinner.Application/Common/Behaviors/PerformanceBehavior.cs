﻿using System.Diagnostics;
using BuberDinner.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuberDinner.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(IDateTimeProvider dateTimeProvider, ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds <= 500) return response;
        var requestName = typeof(TRequest).Name;
        _logger.LogWarning("Buber Dinner Long Running Request: {Name} ({ElapsedMilliseconds} miliseconds) {@Request}"
            ,requestName, elapsedMilliseconds, request);

        return response;
    }
}