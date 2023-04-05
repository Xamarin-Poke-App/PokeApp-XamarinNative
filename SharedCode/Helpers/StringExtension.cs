using System;
namespace SharedCode.Helpers
{
	public static class StringExtension
	{
		public static string FormatedName(this string str)
        {
            var list = str.Split('-');
            string newName = "";
            for (var i = 0; i < list.Length; i++)
            {
                var aux = list[i];
                newName += (char.ToUpper(aux[0]) + aux.Substring(1)) + " ";
            }

            return newName.Trim();
        }
    }
}

