global using MediatR;
global using FluentValidation;

global using Blocks.MediatR;
global using Articles.Abstractions;
global using Articles.Abstractions.Enums;

global using Submission.Domain.Entities;
global using Submission.Domain.Enums;

global using Submission.Application.Features.Shared;

global using Submission.Persistence.Repositories;

global using AssetTypeDefinitionRepository = Blocks.EntityFramework.CachedRepository<
    Submission.Persistence.SubmissionDbContext,
    Submission.Domain.Entities.AssetTypeDefinition,
    Articles.Abstractions.Enums.AssetType>;