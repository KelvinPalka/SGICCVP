using System;
using System.Text.RegularExpressions;

namespace WPF_Projeto_BD.Utils
{
    public static class Masks
    {
        // remove tudo que não for dígito
        public static string Unmask(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return Regex.Replace(value, @"\D", "");
        }

        public static string MaskCPF(string input)
        {
            string digits = Unmask(input);
            if (digits.Length > 11) digits = digits.Substring(0, 11);

            if (digits.Length <= 3)
                return digits;
            if (digits.Length <= 6)
                return digits.Substring(0, 3) + "." + digits.Substring(3);
            if (digits.Length <= 9)
                return digits.Substring(0, 3) + "." + digits.Substring(3, 3) + "." + digits.Substring(6);
            // 10-11
            return digits.Substring(0, 3) + "." + digits.Substring(3, 3) + "." + digits.Substring(6, 3) + "-" + digits.Substring(9);
        }

        public static string MaskCNPJ(string input)
        {
            string digits = Unmask(input);
            if (digits.Length > 14) digits = digits.Substring(0, 14);

            if (digits.Length <= 2)
                return digits;
            if (digits.Length <= 5)
                return digits.Substring(0, 2) + "." + digits.Substring(2);
            if (digits.Length <= 8)
                return digits.Substring(0, 2) + "." + digits.Substring(2, 3) + "." + digits.Substring(5);
            if (digits.Length <= 12)
                return digits.Substring(0, 2) + "." + digits.Substring(2, 3) + "." + digits.Substring(5, 3) + "/" + digits.Substring(8);
            // 13-14
            return digits.Substring(0, 2) + "." + digits.Substring(2, 3) + "." + digits.Substring(5, 3) + "/" + digits.Substring(8, 4) + "-" + digits.Substring(12);
        }

        public static string MaskPhone(string input)
        {
            string digits = Unmask(input);
            if (digits.Length > 11) digits = digits.Substring(0, 11);

            if (digits.Length <= 2)
                return "(" + digits;
            if (digits.Length <= 6)
                return "(" + digits.Substring(0, 2) + ") " + digits.Substring(2);
            if (digits.Length <= 10)
                return "(" + digits.Substring(0, 2) + ") " + digits.Substring(2, 4) + "-" + digits.Substring(6);
            // 11 digits (DDD + 5 + 4)
            return "(" + digits.Substring(0, 2) + ") " + digits.Substring(2, 5) + "-" + digits.Substring(7);
        }

        // auto detecta CPF ou CNPJ baseado no tamanho dos dígitos
        public static string MaskCpfOrCnpj(string input)
        {
            string digits = Unmask(input);
            if (digits.Length <= 11) return MaskCPF(digits);
            return MaskCNPJ(digits);
        }
    }
}
