using BankSystem.API.model;
public static class BankAccountModelMapper
{


    public static BankAccountModel ToModel(BankAccount entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new BankAccountModel
        {

            Id = entity.Id,
            Number = entity.Number,
            Holder = entity.Holder,
            Balance = entity.Balance,
            CreatedAt = entity.CreatedAt,
            Type = entity.Type,
            Status = entity.Status,
            ClientId = entity.ClientId,

        };
    }

    public static List<BankAccountModel> ToModel(List<BankAccount> entities)
    {
        if (entities == null)
        {
            return null;
        }

        return entities.Select(ToModel).ToList();
    }


    public static BankAccount ToEntity(BankAccountModel model)
    {
        if (model == null)
        {
            return null;
        }


        return new BankAccount(
            id: model.Id,
            number: model.Number,
            balance: model.Balance,
            type: model.Type,
            holder: model.Holder,
            createdAt: model.CreatedAt,
            status: model.Status,
            clientId: model.ClientId
        );


    }

    public static List<BankAccount> ToEntity(List<BankAccountModel> models)
    {

        if (models == null)
        {
            return null;
        }


        return models
            .Select(model => new BankAccount(
                id: model.Id,
                number: model.Number,
                balance: model.Balance,
                type: model.Type,
                holder: model.Holder,
                createdAt: model.CreatedAt,
                status: model.Status,
                clientId: model.ClientId
            ))
            .ToList();
    }

    public static AccountOutputDto ToOutputDto(BankAccount entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new AccountOutputDto
        {

            Numero = entity.Number,
            Saldo = entity.Balance,
            Titular = entity.Holder,


            Tipo = entity.Type.ToString()
        };
    }

    public static List<AccountOutputDto> ToOutputDto(List<BankAccount> entities)
    {
        if (entities == null)
        {
            return null;
        }


        return entities.Select(ToOutputDto).ToList();
    }
}