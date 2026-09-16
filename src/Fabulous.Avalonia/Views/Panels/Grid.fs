namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabGrid =
    inherit IFabPanel

type Dimension =
    /// Use a size that fits the children of the row or column.
    | Auto
    /// Use a proportional size of 1
    | Star
    /// Use a proportional size defined by the associated value
    | Stars of float
    /// Use the associated value as the number of device-specific units.
    | Pixel of float
    /// Use the associated value as the shared size group name.
    | SharedSizeGroup of string

module GridUpdaters =
    let updateGridColumnDefinitions _ (newValue: ScalarValue<Dimension[]>) (node: IViewNode) =
        let grid = node.Target :?> Grid

        if not newValue.HasValue then
            grid.ColumnDefinitions.Clear()
        else
            let coll = newValue.Value
            grid.ColumnDefinitions.Clear()

            for c in coll do
                let columnDef =
                    match c with
                    | Auto -> ColumnDefinition(Width = GridLength.Auto)
                    | Star -> ColumnDefinition(GridLength.Star)
                    | Stars x -> ColumnDefinition(Width = GridLength(x, GridUnitType.Star))
                    | Pixel x -> ColumnDefinition(Width = GridLength(x, GridUnitType.Pixel))
                    | SharedSizeGroup x -> ColumnDefinition(SharedSizeGroup = x)

                grid.ColumnDefinitions.Add(columnDef)

    let updateGridRowDefinitions _ (newValue: ScalarValue<Dimension[]>) (node: IViewNode) =
        let grid = node.Target :?> Grid

        if not newValue.HasValue then
            grid.RowDefinitions.Clear()
        else
            let coll = newValue.Value
            grid.RowDefinitions.Clear()

            for c in coll do
                let rowDef =
                    match c with
                    | Auto -> RowDefinition(Height = GridLength.Auto)
                    | Star -> RowDefinition(GridLength.Star)
                    | Stars x -> RowDefinition(GridLength(x, GridUnitType.Star))
                    | Pixel x -> RowDefinition(GridLength(x, GridUnitType.Pixel))
                    | SharedSizeGroup x -> RowDefinition(SharedSizeGroup = x)

                grid.RowDefinitions.Add(rowDef)

module Grid =
    let WidgetKey = Widgets.register<Grid>()

    let ColumnDefinitions =
        Attributes.defineSimpleScalarWithEquality<Dimension array> "Grid_ColumnDefinitions" GridUpdaters.updateGridColumnDefinitions

    let RowDefinitions =
        Attributes.defineSimpleScalarWithEquality<Dimension array> "Grid_RowDefinitions" GridUpdaters.updateGridRowDefinitions

    let ShowGridLines =
        Attributes.defineAvaloniaPropertyBool Grid.ShowGridLinesProperty

    let Column = Attributes.defineAvaloniaPropertyInt Grid.ColumnProperty

    let ColumnSpacing =
        Attributes.defineAvaloniaPropertyFloat Grid.ColumnSpacingProperty

    let Row = Attributes.defineAvaloniaPropertyInt Grid.RowProperty

    let RowSpacing =
        Attributes.defineAvaloniaPropertyFloat Grid.RowSpacingProperty

    let ColumnSpan =
        Attributes.defineAvaloniaPropertyInt Grid.ColumnSpanProperty

    let RowSpan = Attributes.defineAvaloniaPropertyInt Grid.RowSpanProperty

    let IsSharedSizeScope =
        Attributes.defineAvaloniaPropertyBool Grid.IsSharedSizeScopeProperty

[<AutoOpen>]
module GridBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Grid widget.</summary>
        /// <param name="coldefs">Column definitions.</param>
        /// <param name="rowdefs">Row definitions.</param>
        static member Grid(coldefs: Dimension[], rowdefs: Dimension[]) =
            let s1 = Grid.ColumnDefinitions.WithValue(coldefs)
            let s2 = Grid.RowDefinitions.WithValue(rowdefs)
            let scalars = StackList.two(s1, s2)
            let attr = Panel.Children
            CollectionBuilder<'msg, IFabGrid, IFabControl>(
                Grid.WidgetKey,
                scalars,
                attr
            )

type GridModifiers =
    /// <summary>Sets the ShowGridLines property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ShowGridLines value.</param>
    [<Extension>]
    static member inline showGridLines(this: WidgetBuilder<'msg, #IFabGrid>, value: bool) =
        this.AddScalar(Grid.ShowGridLines.WithValue(value))

    /// <summary>Sets the ColumnSpacing property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ColumnSpacing value.</param>
    [<Extension>]
    static member inline columnSpacing(this: WidgetBuilder<'msg, #IFabGrid>, value: float) =
        this.AddScalar(Grid.ColumnSpacing.WithValue(value))

    /// <summary>Sets the RowSpacing property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The RowSpacing value.</param>
    [<Extension>]
    static member inline rowSpacing(this: WidgetBuilder<'msg, #IFabGrid>, value: float) =
        this.AddScalar(Grid.RowSpacing.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Grid control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabGrid>, value: ViewRef<Grid>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

type GridAttachedModifiers =
    /// <summary>Sets the Column property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Column value.</param>
    [<Extension>]
    static member inline gridColumn(this: WidgetBuilder<'msg, #IFabControl>, value: int) =
        this.AddScalar(Grid.Column.WithValue(value))

    /// <summary>Sets the Row property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Row value.</param>
    [<Extension>]
    static member inline gridRow(this: WidgetBuilder<'msg, #IFabControl>, value: int) =
        this.AddScalar(Grid.Row.WithValue(value))

    /// <summary>Sets the ColumnSpan property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ColumnSpan value.</param>
    [<Extension>]
    static member inline gridColumnSpan(this: WidgetBuilder<'msg, #IFabControl>, value: int) =
        this.AddScalar(Grid.ColumnSpan.WithValue(value))

    /// <summary>Sets the RowSpan property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The RowSpan value.</param>
    [<Extension>]
    static member inline gridRowSpan(this: WidgetBuilder<'msg, #IFabControl>, value: int) =
        this.AddScalar(Grid.RowSpan.WithValue(value))

    /// <summary>Sets the IsSharedSizeScope property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsSharedSizeScope value.</param>
    [<Extension>]
    static member inline isSharedSizeScope(this: WidgetBuilder<'msg, #IFabControl>, value: bool) =
        this.AddScalar(Grid.IsSharedSizeScope.WithValue(value))
