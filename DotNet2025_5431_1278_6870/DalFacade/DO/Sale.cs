namespace DO;

/// :אובייקט מבצע המכיל את התכונות
/// מזהה מבצע - מספור אוטומתי
/// מזהה מוצר
/// כמות נדרשת לקבלת המבצע
/// מחיר כולל מבצע
/// האם המבצע מיועד ללקוחות מועדון
/// תאריך תחילת המבצע
/// תאריך סיום המבצע
public record Sale
    (int SaleCode,
     int ProductId,
     int QuantityForSale,
     double SalePrice,
     bool? IsClub,
     DateTime? StartSale = null,
     DateTime? EndSale = null)
{
    public Sale(): this(0,0,0,0,false)
    {
        
    }
}
