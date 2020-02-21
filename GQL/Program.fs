// Learn more about F# at http://fsharp.org

open GQL.Queries
open NBB.Core.Effects.FSharp
open GQL.Schemas
open GQL.Infra

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

let show eff = eff |> Effect.interpret interpreter |> Async.RunSynchronously |> printfn "%A"


[<EntryPoint>]
let main argv =
    //getClientByContractId 1 |> show
    //getClientWithContracts 1 |> show
    getAllClientsWithContracts() |> show

    0 // return an integer exit code
