namespace TopoCaptureScreen.Utilities
{
    public static class NumberHelper
    {
        public static int? secureIntParse(string tmp)
        {
            int? returnValue = null;
            int tmpValue;
            if (int.TryParse(tmp, out tmpValue))
            {
                returnValue = int.Parse(tmp);
            }
            return returnValue;
        }
    }
}
