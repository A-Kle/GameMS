using GamesMS.Core.Services;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GamesMS.Api.Services
{
    public interface IRandomNameGeneratorService : IDependency
    {
        string GenerateName(int length, GenerationMode generationMode);
    }

    public class RandomNameGeneratorService : IRandomNameGeneratorService
    {
        public string GenerateName(int length, GenerationMode generationMode)
        {
            if (length > 255 || length <= 0)
                length = 255;

            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz-_.1234567890";

            if (length > 0 && length < 3)
                return chars[random.Next(0, chars.Length - 1)].ToString() + chars[random.Next(0, chars.Length - 1)].ToString();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "GamesMS.Api.Resources.words.txt";
            var result = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            var words = result.Split(";");

            var name = "";

            while(name.Length < 255)
            {
                var randomWord = words[random.Next(0, words.Length - 1)];

                if ((name + randomWord).Length + 1 > length)
                {
                    return name;
                }
                else
                {
                    if (generationMode == GenerationMode.Mixed)
                        AppendMixedModeString(ref name, randomWord, random.Next(0, 2));
                    else
                        AppendMixedModeString(ref name, randomWord, (int)generationMode);
                }

            }

            return name;
        }

        private void AppendMixedModeString(ref string name, string word, int random)
        {
            switch(random)
            {
                case 0:
                    name += word.First().ToString().ToUpper() + word.Substring(1);
                    break;
                case 1:
                    name += $" {word}";
                    break;
                case 2:
                    name += $"-{word}";
                    break;
            }
        }
    }

    public enum GenerationMode
    {
        WordsStartUppercase,
        WordsSpaced,
        WordsDashed,
        Mixed
    }
}
