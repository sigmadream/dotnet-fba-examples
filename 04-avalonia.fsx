#!/usr/bin/env -S dotnet fsi
// ============================================================================
// F# Avalonia Calculator — Avalonia.FuncUI/Elmish MVU 패턴
// ============================================================================
// F# .fsx 스크립트에서는 소스 제너레이터를 사용할 수 없으므로
// Avalonia.FuncUI의 Elmish(MVU) 패턴을 사용합니다.
// ============================================================================

#r "nuget: Avalonia, 11.3.8"
#r "nuget: Avalonia.Desktop, 11.3.8"
#r "nuget: Avalonia.Themes.Simple, 11.3.8"
#r "nuget: Avalonia.FuncUI, 1.5.1"

open System
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.FuncUI.Hosts
open Avalonia.Layout
open Avalonia.Themes.Simple

// ============================================================================
// MVU (Model-View-Update) 패턴
// ============================================================================

type Model =
    { Display: string
      FirstNumber: string
      Operation: string
      IsNewNumber: bool }

type Msg =
    | InputNumber of string
    | InputOperation of string
    | Calculate
    | Clear

let init () =
    { Display = "0"
      FirstNumber = ""
      Operation = ""
      IsNewNumber = true }

let calculate (first: string) (second: string) (op: string) =
    match Double.TryParse(first), Double.TryParse(second) with
    | (true, n1), (true, n2) ->
        match op with
        | "+" -> string (n1 + n2)
        | "-" -> string (n1 - n2)
        | "×" -> string (n1 * n2)
        | "÷" -> if n2 <> 0.0 then string (n1 / n2) else "오류"
        | _ -> "0"
    | _ -> "오류"

let update (msg: Msg) (model: Model) =
    match msg with
    | InputNumber number ->
        if model.IsNewNumber then
            { model with
                Display = number
                IsNewNumber = false }
        else
            let newDisplay =
                if model.Display = "0" then
                    number
                else
                    model.Display + number

            { model with Display = newDisplay }

    | InputOperation op ->
        let newModel =
            if not (String.IsNullOrEmpty(model.Operation)) then
                let result = calculate model.FirstNumber model.Display model.Operation

                { model with
                    Display = result
                    FirstNumber = result
                    Operation = op
                    IsNewNumber = true }
            else
                { model with
                    FirstNumber = model.Display
                    Operation = op
                    IsNewNumber = true }

        newModel

    | Calculate ->
        if String.IsNullOrEmpty(model.Operation) || String.IsNullOrEmpty(model.FirstNumber) then
            model
        else
            let result = calculate model.FirstNumber model.Display model.Operation

            { Display = result
              FirstNumber = ""
              Operation = ""
              IsNewNumber = true }

    | Clear -> init ()

// ============================================================================
// View (FuncUI DSL)
// ============================================================================

let view (model: Model) (dispatch: Msg -> unit) =
    Grid.create
        [ Grid.margin 10.0
          Grid.rowDefinitions (RowDefinitions("Auto,*,*,*,*"))
          Grid.columnDefinitions (ColumnDefinitions("*,*,*,*"))
          Grid.children
              [
                // 디스플레이
                TextBlock.create
                    [ TextBlock.fontSize 32.0
                      TextBlock.textAlignment Avalonia.Media.TextAlignment.Right
                      TextBlock.verticalAlignment VerticalAlignment.Center
                      TextBlock.margin (Thickness(5.0, 5.0, 5.0, 15.0))
                      TextBlock.text model.Display
                      Grid.row 0
                      Grid.columnSpan 4 ]

                // Row 1: 7, 8, 9, ÷
                Button.create
                    [ Button.content "7"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "7"))
                      Grid.row 1
                      Grid.column 0 ]
                Button.create
                    [ Button.content "8"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "8"))
                      Grid.row 1
                      Grid.column 1 ]
                Button.create
                    [ Button.content "9"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "9"))
                      Grid.row 1
                      Grid.column 2 ]
                Button.create
                    [ Button.content "÷"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputOperation "÷"))
                      Grid.row 1
                      Grid.column 3 ]

                // Row 2: 4, 5, 6, ×
                Button.create
                    [ Button.content "4"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "4"))
                      Grid.row 2
                      Grid.column 0 ]
                Button.create
                    [ Button.content "5"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "5"))
                      Grid.row 2
                      Grid.column 1 ]
                Button.create
                    [ Button.content "6"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "6"))
                      Grid.row 2
                      Grid.column 2 ]
                Button.create
                    [ Button.content "×"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputOperation "×"))
                      Grid.row 2
                      Grid.column 3 ]

                // Row 3: 1, 2, 3, -
                Button.create
                    [ Button.content "1"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "1"))
                      Grid.row 3
                      Grid.column 0 ]
                Button.create
                    [ Button.content "2"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "2"))
                      Grid.row 3
                      Grid.column 1 ]
                Button.create
                    [ Button.content "3"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "3"))
                      Grid.row 3
                      Grid.column 2 ]
                Button.create
                    [ Button.content "-"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputOperation "-"))
                      Grid.row 3
                      Grid.column 3 ]

                // Row 4: 0, C, =, +
                Button.create
                    [ Button.content "0"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputNumber "0"))
                      Grid.row 4
                      Grid.column 0 ]
                Button.create
                    [ Button.content "C"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch Clear)
                      Grid.row 4
                      Grid.column 1 ]
                Button.create
                    [ Button.content "="
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch Calculate)
                      Grid.row 4
                      Grid.column 2 ]
                Button.create
                    [ Button.content "+"
                      Button.fontSize 20.0
                      Button.margin 2.0
                      Button.onClick (fun _ -> dispatch (InputOperation "+"))
                      Grid.row 4
                      Grid.column 3 ] ] ]

// ============================================================================
// 애플리케이션 시작
// ============================================================================

if OperatingSystem.IsWindows() then
    Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.Unknown)
    |> ignore

    Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.STA)
    |> ignore

// MVU를 FuncUI Component로 호스팅 (view는 IView<Grid> 반환)
let calculatorView () =
    Component(fun ctx ->
        let state = ctx.useState (init ())
        let dispatch msg = state.Set(update msg state.Current)
        view state.Current dispatch
    )

type MainWindow() =
    inherit HostWindow()
    do
        base.Title <- "계산기 (F#)"
        base.Width <- 320.0
        base.Height <- 420.0
        base.MinWidth <- 280.0
        base.MinHeight <- 380.0
        base.Content <- calculatorView ()

type App() =
    inherit Application()
    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
            desktop.ShutdownMode <- ShutdownMode.OnMainWindowClose
            desktop.MainWindow <- MainWindow()
        | _ -> ()

AppBuilder
    .Configure<App>()
    .UsePlatformDetect()
    .LogToTrace(Logging.LogEventLevel.Warning)
    .StartWithClassicDesktopLifetime([||])
