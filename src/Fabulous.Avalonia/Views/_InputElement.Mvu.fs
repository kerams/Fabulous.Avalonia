namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Input
open Avalonia.Input.TextInput
open Avalonia.Interactivity
open Fabulous
open Fabulous.ScalarAttributeDefinitions

// Definitions are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps the
// definitions whose property the app reads. Registered keys always carry a kind bit, so 0 means not created yet.
[<AbstractClass; Sealed>]
type MvuInputElement =
    [<DefaultValue>]
    static val mutable private keyDown: SimpleScalarAttributeDefinition<KeyEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private keyUp: SimpleScalarAttributeDefinition<KeyEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private textInput: SimpleScalarAttributeDefinition<TextInputEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private textInputMethodClientRequested: SimpleScalarAttributeDefinition<TextInputMethodClientRequestedEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerEntered: SimpleScalarAttributeDefinition<PointerEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerExited: SimpleScalarAttributeDefinition<PointerEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerMoved: SimpleScalarAttributeDefinition<PointerEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerPressed: SimpleScalarAttributeDefinition<PointerPressedEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerReleased: SimpleScalarAttributeDefinition<PointerReleasedEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerCaptureLost: SimpleScalarAttributeDefinition<PointerCaptureLostEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private pointerWheelChanged: SimpleScalarAttributeDefinition<PointerWheelEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private tapped: SimpleScalarAttributeDefinition<TappedEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private holding: SimpleScalarAttributeDefinition<HoldingRoutedEventArgs -> MsgValue>

    [<DefaultValue>]
    static val mutable private doubleTapped: SimpleScalarAttributeDefinition<TappedEventArgs -> MsgValue>

    static member KeyDown =
        if int MvuInputElement.keyDown.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.keyDown.Key = 0 then
                    MvuInputElement.keyDown <-
                        Attributes.Mvu.defineEvent<KeyEventArgs> "InputElement_KeyDown" (fun target -> (target :?> InputElement).KeyDown)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.keyDown

    static member KeyUp =
        if int MvuInputElement.keyUp.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.keyUp.Key = 0 then
                    MvuInputElement.keyUp <-
                        Attributes.Mvu.defineEvent<KeyEventArgs> "InputElement_KeyUp" (fun target -> (target :?> InputElement).KeyUp)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.keyUp

    static member TextInput =
        if int MvuInputElement.textInput.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.textInput.Key = 0 then
                    MvuInputElement.textInput <-
                        Attributes.Mvu.defineEvent<TextInputEventArgs> "InputElement_TextInput" (fun target -> (target :?> InputElement).TextInput)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.textInput

    static member TextInputMethodClientRequested =
        if int MvuInputElement.textInputMethodClientRequested.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.textInputMethodClientRequested.Key = 0 then
                    MvuInputElement.textInputMethodClientRequested <-
                        Attributes.Mvu.defineEvent<TextInputMethodClientRequestedEventArgs> "InputElement_TextInputMethodClientRequested" (fun target ->
                            (target :?> InputElement).TextInputMethodClientRequested)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.textInputMethodClientRequested

    static member PointerEntered =
        if int MvuInputElement.pointerEntered.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerEntered.Key = 0 then
                    MvuInputElement.pointerEntered <-
                        Attributes.Mvu.defineEvent<PointerEventArgs> "InputElement_PointerEntered" (fun target -> (target :?> InputElement).PointerEntered)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerEntered

    static member PointerExited =
        if int MvuInputElement.pointerExited.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerExited.Key = 0 then
                    MvuInputElement.pointerExited <-
                        Attributes.Mvu.defineEvent<PointerEventArgs> "InputElement_PointerExited" (fun target -> (target :?> InputElement).PointerExited)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerExited

    static member PointerMoved =
        if int MvuInputElement.pointerMoved.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerMoved.Key = 0 then
                    MvuInputElement.pointerMoved <-
                        Attributes.Mvu.defineEvent<PointerEventArgs> "InputElement_PointerMoved" (fun target -> (target :?> InputElement).PointerMoved)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerMoved

    static member PointerPressed =
        if int MvuInputElement.pointerPressed.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerPressed.Key = 0 then
                    MvuInputElement.pointerPressed <-
                        Attributes.Mvu.defineEvent<PointerPressedEventArgs> "InputElement_PointerPressed" (fun target -> (target :?> InputElement).PointerPressed)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerPressed

    static member PointerReleased =
        if int MvuInputElement.pointerReleased.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerReleased.Key = 0 then
                    MvuInputElement.pointerReleased <-
                        Attributes.Mvu.defineEvent<PointerReleasedEventArgs> "InputElement_PointerReleased" (fun target -> (target :?> InputElement).PointerReleased)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerReleased

    static member PointerCaptureLost =
        if int MvuInputElement.pointerCaptureLost.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerCaptureLost.Key = 0 then
                    MvuInputElement.pointerCaptureLost <-
                        Attributes.Mvu.defineEvent<PointerCaptureLostEventArgs> "InputElement_PointerCaptureLost" (fun target -> (target :?> InputElement).PointerCaptureLost)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerCaptureLost

    static member PointerWheelChanged =
        if int MvuInputElement.pointerWheelChanged.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.pointerWheelChanged.Key = 0 then
                    MvuInputElement.pointerWheelChanged <-
                        Attributes.Mvu.defineEvent<PointerWheelEventArgs> "InputElement_PointerWheelChanged" (fun target -> (target :?> InputElement).PointerWheelChanged)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.pointerWheelChanged

    static member Tapped =
        if int MvuInputElement.tapped.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.tapped.Key = 0 then
                    MvuInputElement.tapped <-
                        Attributes.Mvu.defineEvent<TappedEventArgs> "InputElement_Tapped" (fun target -> (target :?> InputElement).Tapped)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.tapped

    static member Holding =
        if int MvuInputElement.holding.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.holding.Key = 0 then
                    MvuInputElement.holding <-
                        Attributes.Mvu.defineEvent<HoldingRoutedEventArgs> "InputElement_Holding" (fun target -> (target :?> InputElement).Holding)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.holding

    static member DoubleTapped =
        if int MvuInputElement.doubleTapped.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int MvuInputElement.doubleTapped.Key = 0 then
                    MvuInputElement.doubleTapped <-
                        Attributes.Mvu.defineEvent<TappedEventArgs> "InputElement_DoubleTapped" (fun target -> (target :?> InputElement).DoubleTapped)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuInputElement.doubleTapped

type MvuInputElementModifiers =

    /// <summary>Listens to the InputElement KeyDown event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a key is pressed while the control has focus.</param>
    [<Extension>]
    static member inline onKeyDown(this: WidgetBuilder<'msg, #IFabInputElement>, fn: KeyEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.KeyDown.WithValue(fn))

    /// <summary>Listens to the InputElement KeyUp event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a key is released while the control has focus.</param>
    [<Extension>]
    static member inline onKeyUp(this: WidgetBuilder<'msg, #IFabInputElement>, fn: KeyEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.KeyUp.WithValue(fn))

    /// <summary>Listens to the InputElement TextInput event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a user typed some text while the control has focus.</param>
    [<Extension>]
    static member inline onTextInput(this: WidgetBuilder<'msg, #IFabInputElement>, fn: TextInputEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.TextInput.WithValue(fn))

    /// <summary>Listens to the InputElement TextInputMethodClientRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when an input element gains input focus and input method is looking for the corresponding client.</param>
    [<Extension>]
    static member inline onTextInputMethodClientRequested(this: WidgetBuilder<'msg, #IFabInputElement>, fn: TextInputMethodClientRequestedEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.TextInputMethodClientRequested.WithValue(fn))

    /// <summary>Listens to the InputElement PointerEntered event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer enters the control.</param>
    [<Extension>]
    static member inline onPointerEntered(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerEntered.WithValue(fn))

    /// <summary>Listens to the InputElement PointerExited event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer leaves the control.</param>
    [<Extension>]
    static member inline onPointerExited(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerExited.WithValue(fn))

    /// <summary>Listens to the InputElement PointerMoved event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer moves over the control.</param>
    [<Extension>]
    static member inline onPointerMoved(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerMoved.WithValue(fn))

    /// <summary>Listens to the InputElement PointerPressed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer is pressed over the control.</param>
    [<Extension>]
    static member inline onPointerPressed(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerPressedEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerPressed.WithValue(fn))

    /// <summary>Listens to the InputElement PointerReleased event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer is released over the control.</param>
    [<Extension>]
    static member inline onPointerReleased(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerReleasedEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerReleased.WithValue(fn))

    /// <summary>Listens to the InputElement PointerCaptureLost event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control or its child control loses the pointer capture for any reason event will not be triggered for a parent control if capture was transferred to another child of that parent control.</param>
    [<Extension>]
    static member inline onPointerCaptureLost(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerCaptureLostEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerCaptureLost.WithValue(fn))

    /// <summary>Listens to the InputElement PointerWheelChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer wheel changes.</param>
    [<Extension>]
    static member inline onPointerWheelChanged(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerWheelEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.PointerWheelChanged.WithValue(fn))

    /// <summary>Listens to the InputElement Tapped event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a tap gesture occurs on the control.</param>
    [<Extension>]
    static member inline onTapped(this: WidgetBuilder<'msg, #IFabInputElement>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.Tapped.WithValue(fn))

    /// <summary>Listens to the InputElement Holding event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a holding gesture occurs on the control.</param>
    [<Extension>]
    static member inline onHolding(this: WidgetBuilder<'msg, #IFabInputElement>, fn: HoldingRoutedEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.Holding.WithValue(fn))

    /// <summary>Listens to the InputElement RightTapped event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a double-tap gesture occurs on the control.</param>
    [<Extension>]
    static member inline onDoubleTapped(this: WidgetBuilder<'msg, #IFabInputElement>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuInputElement.DoubleTapped.WithValue(fn))
