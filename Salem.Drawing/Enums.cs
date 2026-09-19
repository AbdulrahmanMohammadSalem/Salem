using System;

namespace Salem.Drawing {
    /// <summary>
    /// Specifies the four basic cardinal directions: up, down, left, and right.
    /// </summary>
    /// <remarks>This enumeration is typically used to represent movement or orientation in two-dimensional
    /// space, such as in user interfaces, or navigation logic. The underlying type is byte.</remarks>
    public enum BasicDirections : byte { Up = 1, Down = 2, Left = 3, Right = 4 }

    /// <summary>
    /// Specifies the available styles for rendering arrow shapes in graphical components.
    /// </summary>
    /// <remarks>Use this enumeration to select the visual appearance of arrows, such as chevrons, triangles,
    /// or lines, when drawing or displaying directional indicators. The specific style chosen affects the thickness,
    /// fill, and overall look of the arrow. The meaning of each style value depends on the context in which it is
    /// used.</remarks>
    public enum ArrowStyles : byte { Chevron = 0, ChevronThick = 1, ChevronMed = 2, Thick = 3, Thin = 4, FilledTriangle = 5, Flick = 6 }

    /// <summary>
    /// Specifies the available visual themes for the application user interface.
    /// </summary>
    /// <remarks>Use this enumeration to select between predefined appearance modes, such as light or dark
    /// themes. The selected theme may affect colors, backgrounds, and overall UI styling throughout the
    /// application.</remarks>
    public enum ColorModes : byte { Light, Dark, System }

    /// <summary>
    /// Specifies the visual rendering style for user interface elements.
    /// </summary>
    /// <remarks>Use this enumeration to select the appearance mode for controls or components that support
    /// multiple visual styles. The available modes correspond to common UI themes, such as the system default,
    /// professional, manager, or Office Flat styles. The effect of each mode depends on the control or component using
    /// this enumeration.</remarks>
    public enum RenderModes : byte { System, Professional, Manager, OfficeFlat }

    [Flags]
    public enum BorderSides : byte {
        None   = 0b0000,
        Top    = 0b0001,
        Bottom = 0b0010,
        Left   = 0b0100,
        Right  = 0b1000,
        All    = 0b1111
    }
}
