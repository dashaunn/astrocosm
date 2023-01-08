using System;
namespace Astrocosm.Models
{
    public class SunInfo
    {
        public int Id { get; set; }

        public string Glyph { get; set; }

        public string Image { get; set; }

        public string Intro { get; set; }

        public string Symbolism { get; set; }

        public SunInfo(string glyph, string image, string intro, string symbolism)
        {
            Glyph = glyph;
            Image = image;
            Intro = intro;
            Symbolism = symbolism;
        }

        public SunInfo()
        {
        }
    }
}