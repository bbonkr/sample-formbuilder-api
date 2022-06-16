# Sample formbuilder api

Please see with [bbonkr/sample-react-formbuiler](https://github.com/bbonkr/sample-react-formbuiler)

## Entities

```mermaid
erDiagram
    Forms ||--o{ FormItems : items
    Forms ||--o{ FormLocaled : locales
    Forms {
        string Id
        string Title
    }
    FormLocaled {
        string FormId
        string LanguageId
        string Title
    }
    FormItems ||--o{ FormItemOptions : options
    FormItems ||--o{ FormItemLocaled : locales
    FormItems {
        string Id
        string FormId
        string ElementType
        string Name
        string Label
        string Description
        string Placeholder
        bit IsRequired
        int Ordinal
    }
    FormItemLocaled {
        string FormItemId 
        string LanguageId
        string Label
        string Description
        string Placeholder
    }
    FormItemOptions ||--o{ FormItemOptionLocaled : locales
    FormItemOptions {
        string Id
        string FormItemId
        string Value
        string Text
        int Ordinal
    }
    FormItemOptionLocaled {
        string FormItemOptionId
        string LanguageId
        string Text
    }
```

## Migrations

```bash
$ dotnet new tool-manifest
$ dotnet tool install dotnet-ef --local
```

### Migrations add

```bash
$ cd src/FormBuilder.Data
$ dotnet ef migrations add "Initialize" --startup-project ../FormBuilderApp --project ../FormBuilder.Data.SqlServer
```

