

namespace DO;

/// :אובייקט מוצר המכיל את התכונות
/// מספר הברקוד של המוצר
/// שם המוצר
/// קטגוריה
/// מחיר
/// כמות במלאי
public record Product
    (int ProductCode,
    string ProductName,
    double Price,
    int? Quantity,
    Categories? Category
    )
{
    public Product() : this(0,"",0.0,0,null)
    {

    }
}
