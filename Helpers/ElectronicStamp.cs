using System;
using System.Globalization;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;

namespace DocumentProcessing.Helpers
{
    public interface IElectronicStamp
    {
        void Process(Image<Rgba32> img, string entryNumber, DateTime dateTime);
        void Process(Image<Rgba32> img, string entryNumber, DateTime dateTime, bool stumpForReceipt);
    }
    
    public class ElectronicStamp : IElectronicStamp
    {   
        private const string Title = "БА САРРАЁСАТИ КОНСУЛИИ ВАЗОРАТИ КОРҲОИ ХОРИҶИИ ҶУМҲУРИИ ТОҶИКИСТОН";
        const string BottomTitle = "Ворид шуд №";
        private const int Padding = 20;

        public void Process(Image<Rgba32> img, string entryNumber, DateTime dateTime)
        {
            Process(img, entryNumber, dateTime, false);
        }
        
        public void Process(Image<Rgba32> img, string entryNumber, DateTime dateTime, bool stumpForReceipt)
        {
            var mainRectSize = new SizeF(260, 180);
            PointF mainRectPoint;
            if (stumpForReceipt)
            {
                mainRectPoint = new PointF(0, 0);
            }
            else
            {
                mainRectPoint = new PointF(img.Width - mainRectSize.Width - Padding,
                    img.Height - mainRectSize.Height - Padding);
            }
            
            var mainRect = new RectangleF(mainRectPoint, mainRectSize);


            var centerRectSize = new SizeF(140, 60);
            var centerRectPoint = new PointF(
                mainRect.X + (mainRectSize.Width / 2) - (centerRectSize.Width / 2),
                mainRect.Y + (mainRectSize.Height / 2) - (centerRectSize.Height / 2));
            var centerRect = new RectangleF(centerRectPoint, centerRectSize);
            
            var titleOptions = new TextGraphicsOptions()
            {
                WrapTextWidth = mainRect.Width,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var titleFont = SystemFonts.CreateFont("Arial", 14, FontStyle.Regular);
            var titlePoint = new PointF(mainRectPoint.X, mainRectPoint.Y + 10);

            var dateOptions = new TextGraphicsOptions()
            {
                WrapTextWidth = centerRectSize.Width,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var datePoint = new PointF(centerRectPoint.X, centerRectPoint.Y + centerRectSize.Height / 2);
            var dateFont = SystemFonts.CreateFont("Arial", 20, FontStyle.Regular);


            var bottomTitleFont = SystemFonts.CreateFont("Arial", 14, FontStyle.Regular);
            var bottomTitleGraphicsOptions =
                new TextGraphicsOptions(true) // draw the text along the path wrapping at the end of the line
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
            var bottomTitlePoint = new PointF(mainRectPoint.X + 10, mainRectPoint.Y + mainRectSize.Height - 15);

            var bottomTitleGlyphs = TextBuilder.GenerateGlyphs(BottomTitle,
                new RendererOptions(bottomTitleFont, bottomTitlePoint)
                {
                    HorizontalAlignment = bottomTitleGraphicsOptions.HorizontalAlignment,
                    TabWidth = bottomTitleGraphicsOptions.TabWidth,
                    VerticalAlignment = bottomTitleGraphicsOptions.VerticalAlignment,
                    WrappingWidth = bottomTitleGraphicsOptions.WrapTextWidth,
                    ApplyKerning = bottomTitleGraphicsOptions.ApplyKerning
                });

            var entryNumberFont = SystemFonts.CreateFont("Arial", 38);
            var entryNumberGraphicsOptions =
                new TextGraphicsOptions(true) // draw the text along the path wrapping at the end of the line
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
            var entryNumberPoint = new PointF(mainRectPoint.X + bottomTitleGlyphs.Bounds.Width + 20,
                mainRectPoint.Y + mainRectSize.Height - 25);

            var entryNumberGlyphs = TextBuilder.GenerateGlyphs(entryNumber,
                new RendererOptions(entryNumberFont, entryNumberPoint)
                {
                    HorizontalAlignment = entryNumberGraphicsOptions.HorizontalAlignment,
                    TabWidth = entryNumberGraphicsOptions.TabWidth,
                    VerticalAlignment = entryNumberGraphicsOptions.VerticalAlignment,
                    WrappingWidth = entryNumberGraphicsOptions.WrapTextWidth,
                    ApplyKerning = entryNumberGraphicsOptions.ApplyKerning
                });


            img.Mutate(ctx => ctx
                .Draw(Rgba32.Red, 1, mainRect)
                .Draw(Rgba32.Red, 1, centerRect)
                .DrawText(titleOptions, Title, titleFont, Rgba32.Red, titlePoint)
                .DrawText(dateOptions, dateTime.ToString("dd MMMM yyyy",
                    CultureInfo.GetCultureInfoByIetfLanguageTag("tg")), dateFont, Rgba32.Red, datePoint)
                .Fill(Rgba32.Red, bottomTitleGlyphs)
                .Fill(Rgba32.Blue, entryNumberGlyphs));
        }
    }
}