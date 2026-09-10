namespace Fabulous.Avalonia

//open System.Runtime.CompilerServices
//open Avalonia.Media
//open Fabulous

type IFabGeometry =
    inherit IFabElement

//module Geometry =

//    let Transform = Attributes.defineAvaloniaPropertyWidget Geometry.TransformProperty


//type GeometryModifiers =
//    /// <summary>Sets the Transform property.</summary>
//    /// <param name="this">Current widget.</param>
//    /// <param name="value">The TileMode value.</param>
//    [<Extension>]
//    static member inline transform(this: WidgetBuilder<'msg, #IFabGeometry>, value: WidgetBuilder<'msg, #IFabTransform>) =
//        let widget = Geometry.Transform.WithValue(value.Compile())
//        this.AddWidget(&widget)

//type GeometryAttachedModifiers =
//    /// <summary>Sets the Clip property.</summary>
//    /// <param name="this">Current widget.</param>
//    /// <param name="value">The Clip value.</param>
//    [<Extension>]
//    static member inline clip(this: WidgetBuilder<'msg, #IFabVisual>, value: WidgetBuilder<'msg, #IFabGeometry>) =
//        let widget = Visual.ClipWidget.WithValue(value.Compile())
//        this.AddWidget(&widget)
