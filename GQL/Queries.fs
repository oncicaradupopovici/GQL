namespace GQL

open NBB.Core.Effects
open Schemas

module Queries =
    type GetAllClientsQuery() =
        interface ISideEffect<Client list>

    type GetClientByIdQuery = {
        ClientId: int
    } with interface ISideEffect<Client>

    type GetAllContractsQuery() =
        interface ISideEffect<Contract list>

    type GetContractsByClientIdQuery = {
        ClientId: int
    } with interface ISideEffect<Contract list>

    type GetContractByIdQuery = {
        ContractId: int
    } with interface ISideEffect<Contract>


    let getAllClients() = Effect.Of (GetAllClientsQuery())
    let getClientById clientId = Effect.Of {GetClientByIdQuery.ClientId = clientId}
    let getAllContracts() = Effect.Of (GetAllContractsQuery())
    let getContractsByClientId clientId = Effect.Of ({GetContractsByClientIdQuery.ClientId = clientId})
    let getContractById contractId = Effect.Of {GetContractByIdQuery.ContractId = contractId}

