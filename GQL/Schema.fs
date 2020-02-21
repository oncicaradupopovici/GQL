namespace GQL

module Schema = 
    type Client = {
        ClientId: int
        Name: string
    }

    type Contract = {
        ContractId: int
        Value: decimal
        ClientId: int
    }

    type ContractWithClient = {
        ContractId: int
        Value: decimal
        Client: Client
    }
    module ContractWithClient = 
        let from (contract:Contract) (client:Client) = {
            ContractId = contract.ContractId
            Value = contract.Value
            Client = client
        }


