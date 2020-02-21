namespace GQL

open NBB.Core.Effects
open NBB.Core.Effects.FSharp

module Infra =
    type SideEffectHandlerFactory() =
        interface ISideEffectHandlerFactory with
            member this.GetSideEffectHandlerFor(sideEffect: ISideEffect<'TOutput>): ISideEffectHandler<ISideEffect<'TOutput>,'TOutput> = 
                new SideEffectHandlerWrapper<'TOutput>(sideEffect:?>ISideEffectHandler) :> ISideEffectHandler<ISideEffect<'TOutput>,'TOutput>

    let interpreter = Interpreter(SideEffectHandlerFactory())

    let show eff = eff |> Effect.interpret interpreter |> Async.RunSynchronously |> printfn "%A"

