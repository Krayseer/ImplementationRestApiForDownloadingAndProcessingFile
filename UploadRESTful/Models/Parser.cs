namespace UploadRESTful.Models
{
    public class Parser
    {
        public static string ParseText(string[] text)
        {
            var result = "";
            int fixedSectionNumber = 0;
            foreach (var line in text)
            {
                int sectionNumber = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '#') break;
                    sectionNumber++;
                }

                if (sectionNumber != 0)
                {
                    fixedSectionNumber = sectionNumber;
                    result += "\n" + new string('\t', sectionNumber - 1) + line[sectionNumber..] + "\n";
                }

                else
                    result += new string('\t', fixedSectionNumber) + line + "\n";
            }
            return result[1..]; // учитываю что самый первый символ # заменяется на \n - перевод строки
                                // чтобы убрать пустую строку, использую срез
        }
    }
}
