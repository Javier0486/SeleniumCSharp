using System.Collections;

namespace SortByEnum.Utils
{
    public enum SortBy
    {
        AtoZ,
        ZtoA,
        LowToHigh,
        HighToLow
    }

    public static class sortByExtension
    {
        public static string GetSort(this SortBy sortBy)
        {
            return sortBy switch
            {
                SortBy.AtoZ => "Name (A to Z)",
                SortBy.ZtoA => "Name (Z to A)",
                SortBy.LowToHigh => "Price (low to high)",
                SortBy.HighToLow => "Price (high to low)", 
                _ => throw new System.NotImplementedException()
            };
        }
    }
}