namespace denePathParser;

public static class denePathParser
{
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

