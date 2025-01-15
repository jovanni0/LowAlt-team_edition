namespace LowAlt_team_edition;

public static class VerificareCnpService
{
    public static bool verificare(string cnp)
    {
        List<char> cnpList = new List<char>{'0','1','2','3','4','5','6','7','8','9'};
        if (cnp.Length != 13)
        {
            return false;
        }

        foreach (char caracter  in cnp)
        {
            if (!cnpList.Contains(caracter))
            {
                return false;
            }
        }

        return true;
    }
    
    
    
}