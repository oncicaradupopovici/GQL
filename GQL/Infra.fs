namespace GQL

open NBB.Core.Effects
open System.Threading.Tasks
open Queries
open System


module Infra =

    type SideEffectHandler() = 
        member _.Handle (sideEffect:obj) cancellationToken = 
            match sideEffect with 
                | :? Queries.GetAllClientsQuery as q -> Resolvers.getAllClientsResolver(q):>obj
                | :? Queries.GetClientByIdQuery as q -> Resolvers.getClientByIdResolver(q) :>obj
                | :? Queries.GetAllContractsQuery as q -> Resolvers.getAllContractsResolver(q):>obj
                | :? Queries.GetContractByIdQuery as q -> Resolvers.getContractByIdResolver(q):>obj
                | :? Queries.GetContractsByClientIdQuery as q -> Resolvers.getContractByClientIdResolver(q) :>obj
                | _  -> raise (Exception("sdsds"))

        interface ISideEffectHandler


    type SideEffectHandlerFactory() =
        interface ISideEffectHandlerFactory with
            member this.GetSideEffectHandlerFor(sideEffect: ISideEffect<'TOutput>): ISideEffectHandler<ISideEffect<'TOutput>,'TOutput> = 
                new SideEffectHandlerWrapper<'TOutput>(SideEffectHandler()) :> ISideEffectHandler<ISideEffect<'TOutput>,'TOutput>

    let interpreter = Interpreter(SideEffectHandlerFactory())

    //type SideEffectHandler<'TSideEffect, 'TOutput when 'TSideEffect :> ISideEffect<'TOutput>>() =
    //    interface ISideEffectHandler<'TSideEffect, 'TOutput> with
    //        member this.Handle(sideEffect, ct) =

    //            let result = 
    //                if sideEffect.GetType() = Queries.GetAllClientsQuery
    //                    then (sideEffect :?> Queries.GetAllClientsQuery) |> Resolvers.getAllClientsResolver
    //                    else if sideEffect.GetType() = Queries.GetClientByIdQuery 
    //                            then (sideEffect :?> Queries.GetClientByIdQuery) |> Resolvers.getClientByIdResolver
    //                            else null

    //            Task.FromResult((result :>'TOutput))

    //            //let handleType x = 
    //            //    match x with
    //            //    | As (x:int) -> printfn "is integer: %d" x
    //            //    | As (s:string) -> printfn "is string: %s" s
    //            //    | _ -> printfn "Is neither integer nor string"



    //            //let res = if (sideEffect :? GetAllClientsQuery) then sideEffect :> GetAllClientsQuery else null
    //            //let result = 
    //            //    match sideEffect with
    //            //        | GetAllClients q -> getAllClientsResolver(q) :> obj
    //            //        | GetClientById q -> getClientByIdResolver(q) :> obj

    //            //Task.FromResult((result :?> 'TOutput))

