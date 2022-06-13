using AutoMapper;
using FormBuilder.Domains.Translation.Models;
using FormBuilder.Services.Translation;
using FormBuilder.Services.Translation.Models;
using MediatR;

namespace FormBuilder.Domains.Translation.Queries.GetTranslatedText;

public class GetTranslatedTextQueryHandler : IRequestHandler<GetTranslatedTextQuery,TranslatedModel>
{
    private readonly ITranslationService _translationService;
    private readonly IMapper _mapper;
    
    public GetTranslatedTextQueryHandler(ITranslationService translationService, IMapper mapper)
    {
        _translationService = translationService;
        _mapper = mapper;
    }

    public async Task<TranslatedModel> Handle(GetTranslatedTextQuery request,
        CancellationToken cancellationToken = default)
    {
        var translationRequest = _mapper.Map<TranslationRequest>(request);
        var translationResponse = await _translationService.Translate(translationRequest, cancellationToken);

        return _mapper.Map<TranslatedModel>(translationResponse);
    }
}