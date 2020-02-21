namespace GQL

open Schema
open NBB.Core.Effects


module Db = 
    let clients = [{ClientId=1; Name="Vasile"};{ClientId=2; Name="Ion"}]
    let contracts = [{ContractId=1; Value=5000m; ClientId=1};{ContractId=2; Value=3000m; ClientId=2}]

module Resolvers =
    type GetAllClientsResolver() =
        member this.Handle(sideEffect, cancellationToken) = Db.clients
        interface ISideEffect<Client list>
        interface ISideEffectHandler

    type GetClientByIdResolver(clientId:int) =
        member this.Handle(sideEffect, cancellationToken) = Db.clients |> List.find (fun c-> c.ClientId = clientId)
        interface ISideEffect<Client>
        interface ISideEffectHandler

    type GetAllContractsResolver() =
        member this.Handle(sideEffect, cancellationToken) = Db.contracts
        interface ISideEffect<Contract list>
        interface ISideEffectHandler

    type GetContractsByClientIdResolver(clientId:int) =
        member this.Handle(sideEffect, cancellationToken) = Db.contracts |> List.filter (fun c-> c.ClientId = clientId)
        interface ISideEffect<Contract list>
        interface ISideEffectHandler

    type GetContractByIdResolver(contractId:int) = 
        member this.Handle(sideEffect, cancellationToken) = Db.contracts |> List.find (fun c-> c.ContractId = contractId)
        interface ISideEffect<Contract>
        interface ISideEffectHandler
        
    

