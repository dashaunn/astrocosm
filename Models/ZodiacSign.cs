using System;
using System.Collections.Generic;

namespace Astrocosm.Models
{
    public class ZodiacSign
    {
        public int Id { get; set; }
        public string LatinName { get; set; }
        public string EnglishTranslation { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RulingPlanet { get; set; }
        public string Triplicity { get; set; }
        public string Modality { get; set; }
        public string Glyph { get; set; }
        public string TranslationImage { get; set; }
        public string RulingPlanetGlyph { get; set; }
        public string RulingPlanetImage { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string Traits { get; set; }
        public string FamousExamples { get; set; }


        public ZodiacSign()
        {
        }

        public ZodiacSign(string latinName, string englishTranslation, string startDate, string endDate, string rulingPlanet, string triplicity, string modality, string glyph, string translationImage, string rulingPlanetGlyph, string rulingPlanetImage, string paragraph1, string paragraph2, string traits, string famousExamples)
        {
            LatinName = latinName;
            EnglishTranslation = englishTranslation;
            StartDate = startDate;
            EndDate = endDate;
            RulingPlanet = rulingPlanet;
            Triplicity = triplicity;
            Modality = modality;
            Glyph = glyph;
            TranslationImage = translationImage;
            RulingPlanetGlyph = rulingPlanetGlyph;
            RulingPlanetImage = rulingPlanetImage;
            Paragraph1 = paragraph1;
            Paragraph2 = paragraph2;
            Traits = traits;
            FamousExamples = famousExamples;
        }
    }
}

