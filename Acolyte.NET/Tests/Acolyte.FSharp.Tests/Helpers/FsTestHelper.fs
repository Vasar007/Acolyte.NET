module Acolyte.Functional.Tests.Helpers.FsTestHelper

open System
open System.Threading.Tasks
open Xunit


let internal actionShouldThrowWithParamName<'T when 'T :> ArgumentException>
    (paramName: string) (throwFunc: unit -> unit) =
    Assert.Throws<'T>(paramName, throwFunc)

let internal funcShouldThrowWithParamName<'T when 'T :> ArgumentException>
    (paramName: string) (throwFunc: unit -> obj) =
    Assert.Throws<'T>(paramName, throwFunc)

let internal funcShouldThrowWithParamNameAsync<'T when 'T :> ArgumentException>
    (paramName: string) (throwFunc: unit -> Task) =
    Assert.ThrowsAsync<'T>(paramName, fun () -> throwFunc())
