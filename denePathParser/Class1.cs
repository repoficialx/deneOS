namespace denePathParser;

public static class denePathParser
{
    public static string Parse(string input)
    {
        string fixedInput = input.Replace("/", "\\").Trim();
        if (fixedInput.StartsWith("C:\\DNUSR"))
        {
            if (fixedInput.Length == 8)
                return "~\\";

            return "~\\" + fixedInput.Substring(9);
        }
        if (fixedInput.StartsWith("C:\\SOFTWARE"))
        {
            if (fixedInput.Length == 11)
                return "~S\\";

            return "~S\\" + fixedInput.Substring(12);
        }
        if (fixedInput.StartsWith("C:\\DENEOS") || fixedInput.StartsWith("C:\\OSFILES"))
            throw new UnauthorizedAccessException("⛔ Acceso a sistema denegado");
        return fixedInput;
    }
    public static string Resolve(string input)
    {
        string fixedInput = input.Replace("/", "\\").Trim();

        if (fixedInput.StartsWith("~\\"))
            return "C:\\DNUSR" + fixedInput.Substring(2);
        if (fixedInput.StartsWith("~S\\"))
            return "C:\\SOFTWARE" + fixedInput.Substring(3);

        // Prohibidas rutas al sistema
        if (fixedInput.StartsWith("C:\\DENEOS") || fixedInput.StartsWith("C:\\OSFILES"))
            throw new UnauthorizedAccessException("⛔ Acceso a sistema denegado");

        // Si no es ruta válida personalizada
        if (fixedInput.StartsWith("C:\\"))
            throw new UnauthorizedAccessException("⛔ Solo rutas personalizadas con ~\\ o ~S\\");

        return fixedInput;
    }
}

