// Learn more about F# at http://fsharp.org

open GQL.Queries
open GQL.Infra



[<EntryPoint>]
let main argv =
    //getClientByContractId 1 |> show
    //getClientWithContracts 1 |> show
    //getAllClientsWithContracts() |> show
    getContractWithClient 1 |> show
    0 // return an integer exit code
