using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.Data.Seeders;

public class LanguageSeeder : DataSeederBase
{
    public LanguageSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override bool NeedToSeed()
    {
        var hasLanguages = _dbContext.Languages.Any();

        return !hasLanguages;
    }

    public override int Seed(bool bypass = false)
    {
        if (NeedToSeed() && !bypass)
        {
            _dbContext.Languages.Add(CreateLanguage("English", "en", 1, true));
            _dbContext.Languages.Add(CreateLanguage("Korean", "ko", 2));
            _dbContext.Languages.Add(CreateLanguage("Russian", "ru", 3));
            _dbContext.Languages.Add(CreateLanguage("Chinese", "zh", 4));

            return _dbContext.SaveChanges();
        }

        return 0;
    }

    private Language CreateLanguage(string name, string code, int ordinal = 1, bool isDefault = false)
    {
        return new Language
        {
            Code = code,
            Name = name,
            Ordinal = ordinal,
            IsDefault = isDefault,
        };
    }

    private readonly AppDbContext _dbContext;
}