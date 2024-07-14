using Application.Models.Enums;

namespace Application.Utilities.Helpers
{
    public static class EnumConverters
    {
        public static string ConvertWriterLevelToString(WriterLevel writerLevel)
        {
            string result = "";
            switch (writerLevel)
            {
                case WriterLevel.Newbie:
                    result = "Yeni Uye";
                    break;
                case WriterLevel.Junior:
                    result = "Junior Yazar";
                    break;
                case WriterLevel.Mid:
                    result = "Mid Yazar";
                    break;
                case WriterLevel.Senior:
                    result = "Senior Yazar";
                    break;
                case WriterLevel.Master:
                    result = "Master Yazar";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
