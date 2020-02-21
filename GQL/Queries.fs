namespace GQL

open NBB.Core.Effects
open NBB.Core.Effects.FSharp
open Schema
open Resolvers

module Queries =

    let getAllClients = Effect.Of << GetAllClientsResolver
    let getClientById = Effect.Of << GetClientByIdResolver
    let getAllContracts = Effect.Of << GetAllContractsResolver
    let getContractsByClientId = Effect.Of << GetContractsByClientIdResolver
    let getContractById = Effect.Of << GetContractByIdResolver

    let getContractWithClient clientId = 
        effect {
            let! contract = getContractById clientId
            let! client = getClientById contract.ClientId
            return ContractWithClient.from contract client
        }

    let getClientByContractId = 
        let getClientId = fun (c:Contract) -> c.ClientId
        getContractById >=> (getClientId >> getClientById)
    
    let getClientWithContracts clientID =
        effect {
            let! client = getClientById clientID
            let! contracts = getContractsByClientId client.ClientId
            return (client, contracts)
        }
    
    let getAllClientsWithContracts =
        let getContracts (c:Client) = getContractsByClientId c.ClientId |> Effect.map (fun contracts -> (c,contracts))
        getAllClients >=> (List.traverseEffect getContracts)

