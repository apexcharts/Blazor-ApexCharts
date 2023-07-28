namespace ApexCharts
{
    /// <summary>
    /// Class to receive the result from <see cref="ApexChart{TItem}.GetDataUriAsync(DataUriOptions)"/>
    /// </summary>
    public class DataUriResult
    {
        /// <summary>
        /// The base64 data to generate a PDF with
        /// </summary>
        public string ImgURI { get; set; }
    }
}
