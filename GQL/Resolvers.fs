namespace GQL

open Schemas
open Queries


module Db = 
    let clients = [{ClientId=1; Name="Vasile"};{ClientId=2; Name="Ion"}]
    let contracts = [{ContractId=1; Value=5000m; ClientId=1};{ContractId=2; Value=3000m; ClientId=2}]

module Resolvers =
    let getAllClientsResolver (_:GetAllClientsQuery) = Db.clients
    let getClientByIdResolver (q:GetClientByIdQuery) = Db.clients |> List.find (fun c-> c.ClientId = q.ClientId)

    let getAllContractsResolver (_:GetAllContractsQuery) = Db.contracts
    let getContractByIdResolver (q:GetContractByIdQuery) = Db.contracts |> List.find (fun c-> c.ContractId = q.ContractId)
    let getContractByClientIdResolver (q:GetContractsByClientIdQuery) = Db.contracts |> List.filter (fun c-> c.ClientId = q.ClientId)
        
    

