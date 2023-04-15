﻿using System.Drawing;
using LazZiya.ImageResize;

namespace AdapterPattern.Services;

public class AdvanceImageProcess:IAdvanceImageProcess
{
    public void AddWatermarkImage(Stream stream, string text, string filePath, Color color, Color outlineColor)
    {
        using (var img = Image.FromStream(stream))
        {
            var tOps = new TextWatermarkOptions
            {
                TextColor = color,

                OutlineColor = outlineColor
            };

            img.AddTextWatermark(text, tOps)
                .SaveAs(filePath);
        }
    }
}