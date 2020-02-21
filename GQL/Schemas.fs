namespace GQL

module Schemas = 
    type Client = {
        ClientId: int
        Name: string
    }

    type Contract = {
        ContractId: int
        Value: decimal
        ClientId: int
}
