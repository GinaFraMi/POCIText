using System.Globalization;

namespace POCIText{
  public class Information {
    private DateTime _expirationDate = DateTime.Today;
    public string price {get;set;} = string.Empty;
    public string certificadeCode {get;set;} = string.Empty;
    public string points {get;set;} = string.Empty;
    private DateTime date { get{return _expirationDate;} set { _expirationDate = _expirationDate.AddDays(7);}}
  
    public string expirationDate { get {
      return $" {date.Day} de {date.ToString("MMMM", new CultureInfo("es-ES"))} del {date.Year}";
    }}
  }
}