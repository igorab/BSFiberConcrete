using System;
using System.ComponentModel;

namespace BSFiberConcrete
{
    [Flags, Description("Сечение балки")]
    public enum BeamSection
    {
        [Description("Cечение не задано")]
        None = 0,
        [Description("Тавровое сечение")]
        TBeam = 1,
        [Description("Двутавровое сечение")]
        IBeam = 2,
        [Description("Кольцевое сечение")]
        Ring = 3,
        [Description("Прямоугольное сечение")]
        Rect = 4,
        [Description("Тавр нижняя полка")]
        LBeam = 5
    }
}
